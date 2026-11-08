if (!window.blazorexpress) {
    window.blazorexpress = {};
}

if (!window.blazorexpress.chartjs) {
    window.blazorexpress.chartjs = {};
}

(() => {
    const minimumChartJsVersion = '4.6.0';
    let isChartJsVersionChecked = false;

    // Image export waits for a running animation, but never longer than this (a looping animation never ends).
    const maxAnimationWaitMs = 5000;

    // Per-chart state the library keeps beside Chart.js: event options, the .NET reference, and the
    // visibility changed at runtime, which `update` re-applies after it hands Chart.js the matched and new datasets.
    const chartStates = new WeakMap();

    const applyDataLabelsBackgroundColor = (options) => {
        options.plugins ??= {};
        options.plugins.datalabels ??= {};
        options.plugins.datalabels.backgroundColor = function (context) {
            return context.dataset.backgroundColor;
        };
    };

    // Options are replaced on every update, so the legend handler is set again each time.
    const applyEventHandlers = (options) => {
        if (!options)
            return;

        options.plugins ??= {};
        options.plugins.legend ??= {};
        options.plugins.legend.onClick = handleLegendClick;
    };

    // Options come from `options.plugins.tooltipLine` (C# `LineChartPlugins.TooltipLine`). Chart.js falls back to these
    // defaults for an inline plugin, so an unset option draws the original grey 2 px dashed line.
    const createTooltipLinePlugin = () => ({
        id: 'tooltipLine',
        defaults: { color: 'grey', dash: [5, 5], display: true, width: 2 },
        beforeDraw: (chart, args, options) => {
            if (!options.display || !chart.tooltip?._active?.length)
                return;

            const ctx = chart.ctx;
            const activePoint = chart.tooltip._active[0];

            ctx.save();
            ctx.setLineDash(Array.isArray(options.dash) ? options.dash : []);
            ctx.lineWidth = options.width;
            ctx.strokeStyle = options.color;

            // Two strokes, so the dash pattern restarts at the hovered point, as it always has.
            ctx.beginPath();
            ctx.moveTo(activePoint.element.x, chart.chartArea.top);
            ctx.lineTo(activePoint.element.x, activePoint.element.y);
            ctx.stroke();

            ctx.beginPath();
            ctx.moveTo(activePoint.element.x, activePoint.element.y);
            ctx.lineTo(activePoint.element.x, chart.chartArea.bottom);
            ctx.stroke();

            ctx.restore();
        },
    });

    const getDataCount = (chart, datasetIndex) =>
        chart.data.labels?.length || chart.data.datasets?.[datasetIndex]?.data?.length || 0;

    const getDatasetCount = (chart) => chart.data.datasets?.length ?? 0;

    const getDataItemCount = (chart) =>
        Math.max(chart.data.labels?.length ?? 0, ...(chart.data.datasets ?? []).map(dataset => dataset?.data?.length ?? 0));

    // A data item is matched across updates by its entry in `labels`. Missing or duplicate labels fall back
    // to the position. Runs in O(n).
    const getKeys = (values) => {
        const counts = new Map();

        for (const value of values)
            if (!isMissing(value))
                counts.set(String(value), (counts.get(String(value)) ?? 0) + 1);

        return values.map((value, i) => !isMissing(value) && counts.get(String(value)) === 1 ? `label:${String(value)}` : `#${i}`);
    };

    // Chart dataset identity across updates (foundation rules): the base is a non-empty `key`, else a non-empty
    // label, else none, followed by the occurrence of that base in array order. A unique label is `label:Sales#0`
    // wherever it sits, and duplicate or missing labels pair in order of occurrence. Runs in O(n).
    const getDatasetIdentityKeys = (datasets) => {
        const counts = new Map();

        return (datasets ?? []).map(dataset => {
            const base = !isMissing(dataset?.key) ? `key:${String(dataset.key)}`
                : !isMissing(dataset?.label) ? `label:${String(dataset.label)}`
                    : 'none';
            const occurrence = counts.get(base) ?? 0;
            counts.set(base, occurrence + 1);

            return `${base}#${occurrence}`;
        });
    };

    const getDatasetKeys = (chart) => getDatasetIdentityKeys(chart.data.datasets);

    const getDataKeys = (chart) => {
        const labels = chart.data.labels ?? [];
        return getKeys(Array.from({ length: getDataItemCount(chart) }, (_, i) => labels[i]));
    };

    const getDefaultLegendClickHandler = (chart) =>
        Chart.overrides?.[chart.config.type]?.plugins?.legend?.onClick ?? Chart.defaults.plugins.legend.onClick;

    const getItemPayload = (chart, element) => {
        const dataset = chart.data.datasets?.[element.datasetIndex];

        return {
            datasetIndex: element.datasetIndex,
            datasetLabel: dataset?.label ?? null,
            index: element.index,
            label: chart.data.labels?.[element.index] ?? null,
            value: dataset?.data?.[element.index] ?? null
        };
    };

    const getState = (chart) => {
        let state = chartStates.get(chart);
        if (!state) {
            state = {
                dataOverrides: new Map(),
                datasetOverrides: new Map(),
                dotNetObjectReference: null,
                eventOptions: { hasHover: false, hasLegendClick: false, legendClickToggles: true },
                isArcChart: false,
                isHoverHandlerAdded: false,
                lastHoverKey: '',
                pointOverrides: new Map(),
            };
            chartStates.set(chart, state);
        }

        return state;
    };

    const handleLegendClick = function (e, legendItem, legend) {
        const chart = legend.chart;
        const state = getState(chart);

        if (state.eventOptions.legendClickToggles !== false) {
            getDefaultLegendClickHandler(chart).call(this, e, legendItem, legend);

            if (state.isArcChart)
                recordDataVisibility(chart, legendItem.index);
            else
                recordDatasetVisibility(chart, legendItem.datasetIndex);
        }

        if (!state.eventOptions.hasLegendClick || !state.dotNetObjectReference)
            return;

        state.dotNetObjectReference.invokeMethodAsync('HandleLegendClickAsync', {
            datasetIndex: state.isArcChart ? null : legendItem.datasetIndex,
            index: state.isArcChart ? legendItem.index : null,
            text: legendItem.text ?? null,
            hidden: state.isArcChart ? !chart.getDataVisibility(legendItem.index) : !chart.isDatasetVisible(legendItem.datasetIndex)
        }).catch(() => { });
    };

    const isDirectUpdateMode = mode => mode === 'none' || mode === 'reset';

    const isInRange = (index, count) => Number.isInteger(index) && index >= 0 && index < count;

    const isMissing = value => value === undefined || value === null || value === '';

    const isObject = value => value !== null && typeof value === 'object';

    const isVersionBelow = (version, minimum) => {
        const actualParts = String(version).split('.').map(part => parseInt(part, 10) || 0);
        const minimumParts = minimum.split('.').map(part => parseInt(part, 10) || 0);

        for (let i = 0; i < minimumParts.length; i++) {
            const actual = actualParts[i] ?? 0;
            if (actual !== minimumParts[i])
                return actual < minimumParts[i];
        }

        return false;
    };

    // Reuses the current dataset object for each new dataset with the same identity key and the same `type`.
    // Chart.js matches metadata by object identity, so it keeps the controller and elements and animates from the
    // current values. Any other dataset goes in as the new object and grows in from its base.
    const matchDatasets = (chart, nextDatasets) => {
        const currentDatasets = chart.data.datasets ?? [];
        const currentKeys = getDatasetIdentityKeys(currentDatasets);
        const currentByKey = new Map(currentKeys.map((key, i) => [key, currentDatasets[i]]));

        return getDatasetIdentityKeys(nextDatasets).map((key, i) => {
            const next = nextDatasets[i];
            const current = currentByKey.get(key);

            if (!isObject(current) || !isObject(next) || current.type !== next.type)
                return next;

            replaceDatasetProperties(current, next);
            return current;
        });
    };

    const recordDataVisibility = (chart, index) =>
        getState(chart).dataOverrides.set(getDataKeys(chart)[index], !chart.getDataVisibility(index));

    const recordDatasetVisibility = (chart, datasetIndex) =>
        getState(chart).datasetOverrides.set(getDatasetKeys(chart)[datasetIndex], !chart.isDatasetVisible(datasetIndex));

    const recordPointVisibility = (chart, datasetIndex, index, hidden) => {
        const datasetKey = getDatasetKeys(chart)[datasetIndex];
        const dataKey = getDataKeys(chart)[index];
        getState(chart).pointOverrides.set(JSON.stringify([datasetKey, dataKey]), { datasetKey, dataKey, hidden });
    };

    // Runs after `chart.data` is replaced and before `chart.update`: unmatched dataset objects get new metadata,
    // so runtime dataset and slice visibility has to be set again (a matched dataset keeps its metadata, so setting
    // it again changes nothing). Overrides whose item is gone are dropped.
    const reapplyVisibilityBeforeUpdate = (chart, previousDataCount) => {
        const state = getState(chart);

        if (state.datasetOverrides.size > 0) {
            const datasetKeys = getDatasetKeys(chart);

            for (const [key, hidden] of [...state.datasetOverrides]) {
                const datasetIndex = datasetKeys.indexOf(key);
                if (datasetIndex < 0)
                    state.datasetOverrides.delete(key);
                else
                    chart.setDatasetVisibility(datasetIndex, !hidden);
            }
        }

        // With no recorded slice change there is no runtime slice flag to keep or clear.
        if (!state.isArcChart || state.dataOverrides.size === 0)
            return;

        const dataKeys = getDataKeys(chart);
        const hiddenIndexes = new Set();

        for (const [key, hidden] of [...state.dataOverrides]) {
            const index = dataKeys.indexOf(key);
            if (index < 0)
                state.dataOverrides.delete(key);
            else if (hidden)
                hiddenIndexes.add(index);
        }

        // Chart.js keeps slice visibility by position, so clear every stale flag too.
        for (let i = 0; i < Math.max(previousDataCount, dataKeys.length); i++) {
            const visible = !hiddenIndexes.has(i);
            if (chart.getDataVisibility(i) !== visible)
                chart.toggleDataVisibility(i);
        }
    };

    // Runs after `chart.update`, once the new bar and point elements exist.
    const reapplyPointVisibilityAfterUpdate = (chart) => {
        const state = getState(chart);
        if (state.isArcChart || state.pointOverrides.size === 0)
            return;

        const datasetKeys = getDatasetKeys(chart);
        const dataKeys = getDataKeys(chart);
        const hiddenByPosition = new Map();

        for (const [key, override] of [...state.pointOverrides]) {
            const datasetIndex = datasetKeys.indexOf(override.datasetKey);
            const index = dataKeys.indexOf(override.dataKey);
            const element = datasetIndex < 0 || index < 0 ? undefined : chart.getDatasetMeta(datasetIndex).data[index];

            if (!element)
                state.pointOverrides.delete(key);
            else
                hiddenByPosition.set(`${datasetIndex}:${index}`, override.hidden);
        }

        // A matched dataset keeps its bar and point elements, and their `hidden` flag stays with the position.
        // So every element is set, to its override or to visible, which also clears a flag whose item has moved or gone.
        let isChanged = false;

        for (let datasetIndex = 0; datasetIndex < datasetKeys.length; datasetIndex++) {
            const elements = chart.getDatasetMeta(datasetIndex).data;

            for (let index = 0; index < elements.length; index++) {
                const hidden = hiddenByPosition.get(`${datasetIndex}:${index}`) ?? false;

                if (elements[index] && !!elements[index].hidden !== hidden) {
                    elements[index].hidden = hidden;
                    isChanged = true;
                }
            }
        }

        if (isChanged)
            chart.render();
    };

    // Makes `target` hold exactly the own properties of `source`, so the result equals a fresh render of the new
    // dataset and a property removed in C# no longer applies (foundation rules: chart dataset identity across
    // updates). `__proto__` is skipped, so a payload property cannot change the object's prototype.
    const replaceDatasetProperties = (target, source) => {
        for (const name of Object.keys(target))
            if (!Object.prototype.hasOwnProperty.call(source, name))
                delete target[name];

        for (const name of Object.keys(source))
            if (name !== '__proto__')
                target[name] = source[name];
    };

    // ResizeAsync sends CSS declarations such as "width:300px"; a style property needs only the value ("300px").
    const toCssValue = (declaration) => {
        const text = String(declaration ?? '');
        const separator = text.indexOf(':');

        return (separator > -1 ? text.slice(separator + 1) : text).replace(/;\s*$/, '').trim();
    };

    const waitForAnimation = async (chart) => {
        const animator = Chart.animator;
        if (!animator?.running)
            return;

        const deadline = performance.now() + maxAnimationWaitMs;
        while (animator.running(chart) && performance.now() < deadline)
            await new Promise(resolve => setTimeout(resolve, 16));
    };

    const warnIfChartJsIsOutdated = () => {
        if (isChartJsVersionChecked)
            return;

        isChartJsVersionChecked = true;

        if (typeof Chart !== 'undefined' && isVersionBelow(Chart.version, minimumChartJsVersion))
            console.warn(`BlazorExpress.ChartJS: Chart.js ${Chart.version} is loaded; ${minimumChartJsVersion} or later is required.`);
    };

    const updateVisibility = (elementId, datasetIndex, dataIndex, visible) => {
        const chart = core.get(elementId);
        if (!chart || !isInRange(datasetIndex, getDatasetCount(chart)))
            return;

        if (dataIndex === undefined || dataIndex === null) {
            if (visible)
                chart.show(datasetIndex);
            else
                chart.hide(datasetIndex);

            recordDatasetVisibility(chart, datasetIndex);
            return;
        }

        const isArcChart = getState(chart).isArcChart;

        // A bar or point exists only for the dataset's own values, which can be fewer than the labels.
        const dataCount = isArcChart ? getDataCount(chart, datasetIndex) : chart.data.datasets[datasetIndex]?.data?.length ?? 0;
        if (!isInRange(dataIndex, dataCount))
            return;

        if (isArcChart) {
            if (chart.getDataVisibility(dataIndex) !== visible) {
                chart.toggleDataVisibility(dataIndex);
                chart.update();
            }

            recordDataVisibility(chart, dataIndex);
            return;
        }

        if (visible)
            chart.show(datasetIndex, dataIndex);
        else
            chart.hide(datasetIndex, dataIndex);

        recordPointVisibility(chart, datasetIndex, dataIndex, !visible);
    };

    // Chart.js resolves options.events to an array; a library event fires only for an event the chart listens to.
    const isChartEvent = (chart, eventName) => {
        const events = chart.options?.events;
        return !Array.isArray(events) || events.includes(eventName);
    };

    // ---------------------------------------------------------------------------------------------------------------
    // Built-in Chart.js date adapter for time and timeseries axes (rule "built-in date adapter and .NET date formats").
    // Dates arrive as the ISO 8601 strings C# `ChartTimeValue` writes (rule "date values and time zones"): without an
    // offset they are wall-clock values in the browser's zone, with `Z` or an offset they are instants. Formats are .NET
    // date format strings; names come from `Intl` for the chart's locale. Nothing here evaluates or injects markup.
    // ---------------------------------------------------------------------------------------------------------------

    const dateUnitMs = { millisecond: 1, second: 1000, minute: 60000, hour: 3600000 };

    const defaultDateFormats = {
        datetime: 'G',
        millisecond: 'HH:mm:ss.fff',
        second: 'T',
        minute: 't',
        hour: 't',
        day: 'd MMM',
        week: 'd MMM',
        month: 'MMM yyyy',
        quarter: 'MMM yyyy',
        year: 'yyyy',
    };

    const isoDatePattern = /^(\d{4})-(\d{2})-(\d{2})(?:T(\d{2}):(\d{2})(?::(\d{2})(?:\.(\d{1,3}))?)?)?(Z|[+-]\d{2}:\d{2})?$/;

    const dateFormatters = new Map();
    const warnedLocales = new Set();

    const getDateFormatter = (locale, options) => {
        const key = `${locale ?? ''}|${JSON.stringify(options)}`;
        let formatter = dateFormatters.get(key);

        if (!formatter) {
            try {
                formatter = new Intl.DateTimeFormat(locale, options);
            } catch {
                if (!warnedLocales.has(locale)) {
                    warnedLocales.add(locale);
                    console.warn(`BlazorExpress.ChartJS: the locale '${locale}' is not valid; the browser's locale is used for dates.`);
                }
                formatter = new Intl.DateTimeFormat(undefined, options);
            }
            dateFormatters.set(key, formatter);
        }

        return formatter;
    };

    const getDatePart = (time, locale, options, type) =>
        getDateFormatter(locale, options).formatToParts(time).find(part => part.type === type)?.value ?? '';

    const padNumber = (value, length) => String(Math.abs(value)).padStart(length, '0');

    const daysInMonth = (year, month) => new Date(year, month + 1, 0).getDate();

    const parseDateValue = (value) => {
        if (value === null || value === undefined)
            return null;

        if (typeof value === 'number')
            return Number.isFinite(value) ? value : null;

        if (value instanceof Date) {
            const time = value.getTime();
            return Number.isNaN(time) ? null : time;
        }

        if (typeof value !== 'string')
            return null;

        const match = isoDatePattern.exec(value.trim());
        if (!match)
            return null;

        const [, y, mo, d, h = '0', mi = '0', s = '0', ms = '0', zone] = match;
        const year = +y, month = +mo - 1, day = +d;

        if (month > 11 || day < 1 || day > daysInMonth(year, month) || +h > 23 || +mi > 59 || +s > 59)
            return null;

        const milliseconds = +ms.padEnd(3, '0');

        if (!zone) {
            // Built from local parts, never Date.parse, which reads a date-only string as UTC.
            const date = new Date(2000, 0, 1);
            date.setFullYear(year, month, day);
            date.setHours(+h, +mi, +s, milliseconds);
            return date.getTime();
        }

        let time = Date.UTC(2000, 0, 1);
        const date = new Date(time);
        date.setUTCFullYear(year, month, day);
        date.setUTCHours(+h, +mi, +s, milliseconds);
        time = date.getTime();

        if (zone !== 'Z') {
            const sign = zone[0] === '-' ? -1 : 1;
            const [offsetHours, offsetMinutes] = zone.slice(1).split(':').map(Number);
            time -= sign * (offsetHours * 60 + offsetMinutes) * 60000;
        }

        return time;
    };

    const addMonths = (time, amount) => {
        const date = new Date(time);
        const day = date.getDate();
        date.setDate(1);
        date.setMonth(date.getMonth() + amount);
        date.setDate(Math.min(day, daysInMonth(date.getFullYear(), date.getMonth())));
        return date.getTime();
    };

    const addDateUnits = (time, amount, unit) => {
        if (unit in dateUnitMs)
            return time + amount * dateUnitMs[unit];

        // Calendar units keep the wall-clock time, so a 23- or 25-hour DST day still counts as one day.
        const date = new Date(time);
        switch (unit) {
            case 'day':
                date.setDate(date.getDate() + amount);
                return date.getTime();
            case 'week':
            case 'isoWeek':
                date.setDate(date.getDate() + amount * 7);
                return date.getTime();
            case 'month':
                return addMonths(time, amount);
            case 'quarter':
                return addMonths(time, amount * 3);
            case 'year':
                return addMonths(time, amount * 12);
            default:
                return time;
        }
    };

    const timeOfDay = (date) => ((date.getHours() * 60 + date.getMinutes()) * 60 + date.getSeconds()) * 1000 + date.getMilliseconds();

    // Whole units between two times, truncated toward zero.
    const diffDateUnits = (max, min, unit) => {
        if (unit in dateUnitMs)
            return Math.trunc((max - min) / dateUnitMs[unit]);

        const a = new Date(max), b = new Date(min);
        const sign = max >= min ? 1 : -1;

        if (unit === 'day' || unit === 'week' || unit === 'isoWeek') {
            let days = Math.round((Date.UTC(a.getFullYear(), a.getMonth(), a.getDate()) - Date.UTC(b.getFullYear(), b.getMonth(), b.getDate())) / 86400000);
            if (sign > 0 && days > 0 && timeOfDay(a) < timeOfDay(b))
                days--;
            else if (sign < 0 && days < 0 && timeOfDay(a) > timeOfDay(b))
                days++;
            return unit === 'day' ? days : Math.trunc(days / 7);
        }

        let months = (a.getFullYear() - b.getFullYear()) * 12 + a.getMonth() - b.getMonth();
        const aRest = a.getDate() * 86400000 + timeOfDay(a), bRest = b.getDate() * 86400000 + timeOfDay(b);
        if (sign > 0 && months > 0 && aRest < bRest)
            months--;
        else if (sign < 0 && months < 0 && aRest > bRest)
            months++;

        switch (unit) {
            case 'month':
                return months;
            case 'quarter':
                return Math.trunc(months / 3);
            case 'year':
                return Math.trunc(months / 12);
            default:
                return 0;
        }
    };

    const startOfDateUnit = (time, unit, weekday) => {
        const date = new Date(time);

        switch (unit) {
            case 'millisecond':
                return time;
            case 'second':
                date.setMilliseconds(0);
                return date.getTime();
            case 'minute':
                date.setSeconds(0, 0);
                return date.getTime();
            case 'hour':
                date.setMinutes(0, 0, 0);
                return date.getTime();
            case 'day':
                date.setHours(0, 0, 0, 0);
                return date.getTime();
            case 'week':
            case 'isoWeek': {
                // Chart.js passes isoWeekday: true means Monday, a number is the first day (0 is Sunday).
                const firstDay = unit === 'week' ? 0 : (weekday === true ? 1 : (Number(weekday) || 0) % 7);
                date.setHours(0, 0, 0, 0);
                date.setDate(date.getDate() - ((date.getDay() - firstDay + 7) % 7));
                return date.getTime();
            }
            case 'month':
                date.setHours(0, 0, 0, 0);
                date.setDate(1);
                return date.getTime();
            case 'quarter':
                date.setHours(0, 0, 0, 0);
                date.setMonth(date.getMonth() - (date.getMonth() % 3), 1);
                return date.getTime();
            case 'year':
                date.setHours(0, 0, 0, 0);
                date.setMonth(0, 1);
                return date.getTime();
            default:
                return time;
        }
    };

    const endOfDateUnit = (time, unit) => {
        const calendarUnit = unit === 'isoWeek' ? 'week' : unit;
        return addDateUnits(startOfDateUnit(time, unit, 1), 1, calendarUnit) - 1;
    };

    // .NET standard date formats, mapped to the locale's own Intl patterns.
    const standardDateFormats = {
        d: { year: 'numeric', month: 'numeric', day: 'numeric' },
        D: { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' },
        f: { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: '2-digit' },
        F: { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: '2-digit', second: '2-digit' },
        g: { year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: '2-digit' },
        G: { year: 'numeric', month: 'numeric', day: 'numeric', hour: 'numeric', minute: '2-digit', second: '2-digit' },
        M: { month: 'long', day: 'numeric' },
        m: { month: 'long', day: 'numeric' },
        t: { hour: 'numeric', minute: '2-digit' },
        T: { hour: 'numeric', minute: '2-digit', second: '2-digit' },
        Y: { year: 'numeric', month: 'long' },
        y: { year: 'numeric', month: 'long' },
    };

    const sortableDateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss";

    const customDateSpecifiers = 'dfFghHKmMstyz';

    const formatOffset = (time, length) => {
        const offset = -new Date(time).getTimezoneOffset();
        const sign = offset < 0 ? '-' : '+';
        const hours = Math.trunc(Math.abs(offset) / 60), minutes = Math.abs(offset) % 60;
        if (length === 1)
            return `${sign}${hours}`;
        if (length === 2)
            return `${sign}${padNumber(hours, 2)}`;
        return `${sign}${padNumber(hours, 2)}:${padNumber(minutes, 2)}`;
    };

    const formatDateToken = (time, date, letter, length, locale) => {
        switch (letter) {
            case 'd':
                if (length === 1) return String(date.getDate());
                if (length === 2) return padNumber(date.getDate(), 2);
                return getDatePart(time, locale, { weekday: length === 3 ? 'short' : 'long' }, 'weekday');
            case 'f':
            case 'F': {
                const digits = (padNumber(date.getMilliseconds(), 3) + '0000').slice(0, Math.min(length, 7));
                return letter === 'f' ? digits : digits.replace(/0+$/, '');
            }
            case 'g':
                return getDatePart(time, locale, { era: 'short' }, 'era');
            case 'h':
                return length === 1 ? String(date.getHours() % 12 || 12) : padNumber(date.getHours() % 12 || 12, 2);
            case 'H':
                return length === 1 ? String(date.getHours()) : padNumber(date.getHours(), 2);
            case 'K':
                return formatOffset(time, 3);
            case 'm':
                return length === 1 ? String(date.getMinutes()) : padNumber(date.getMinutes(), 2);
            case 'M':
                if (length === 1) return String(date.getMonth() + 1);
                if (length === 2) return padNumber(date.getMonth() + 1, 2);
                return getDatePart(time, locale, { month: length === 3 ? 'short' : 'long' }, 'month');
            case 's':
                return length === 1 ? String(date.getSeconds()) : padNumber(date.getSeconds(), 2);
            case 't': {
                const designator = getDatePart(time, locale, { hour: 'numeric', hour12: true }, 'dayPeriod');
                return length === 1 ? designator.slice(0, 1) : designator;
            }
            case 'y': {
                const year = date.getFullYear();
                if (length === 1) return String(year % 100);
                if (length === 2) return padNumber(year % 100, 2);
                return (year < 0 ? '-' : '') + padNumber(year, length);
            }
            case 'z':
                return formatOffset(time, Math.min(length, 3));
            default:
                return letter.repeat(length);
        }
    };

    // Formats `time` with a .NET date format string: a single letter is a standard format, anything else a custom one.
    const formatDateValue = (time, format, locale) => {
        if (typeof format !== 'string' || format.trim() === '')
            format = defaultDateFormats.datetime;

        if (format.length === 1) {
            if (format === 's')
                format = sortableDateFormat;
            else if (standardDateFormats[format])
                return getDateFormatter(locale, standardDateFormats[format]).format(time);
        }

        const date = new Date(time);
        let result = '';

        for (let i = 0; i < format.length;) {
            const char = format[i];

            if (char === "'" || char === '"') {
                const end = format.indexOf(char, i + 1);
                result += end < 0 ? format.slice(i + 1) : format.slice(i + 1, end);
                i = end < 0 ? format.length : end + 1;
            } else if (char === '\\') {
                result += format[i + 1] ?? '';
                i += 2;
            } else if (char === '%') {
                i++;
            } else if (customDateSpecifiers.includes(char)) {
                let length = 1;
                while (format[i + length] === char)
                    length++;

                const token = formatDateToken(time, date, char, length, locale);
                // As in .NET, an all-zero F fraction is dropped together with the separator in front of it.
                result = token === '' && char === 'F' && result.endsWith('.') ? result.slice(0, -1) : result + token;
                i += length;
            } else {
                result += char;
                i++;
            }
        }

        return result;
    };

    const dateAdapter = {
        formats: () => ({ ...defaultDateFormats }),
        parse(value) {
            return parseDateValue(value);
        },
        format(time, format) {
            return formatDateValue(time, format, this.options?.locale);
        },
        add: (time, amount, unit) => addDateUnits(time, amount, unit),
        diff: (max, min, unit) => diffDateUnits(max, min, unit),
        startOf: (time, unit, weekday) => startOfDateUnit(time, unit, weekday),
        endOf: (time, unit) => endOfDateUnit(time, unit),
    };

    // Chart.js has one date adapter per page. Registered on load and before each chart is created, so it is the one in use.
    const registerDateAdapter = () => {
        if (typeof Chart !== 'undefined' && Chart._adapters?._date)
            Chart._adapters._date.override(dateAdapter);
    };

    const isTimeScale = scale => scale && (scale.type === 'time' || scale.type === 'timeseries');

    // Passes the chart locale to the adapter of each time scale, and drops empty formats so the unit default applies.
    const applyTimeScaleOptions = (options) => {
        if (!options?.scales)
            return;

        Object.values(options.scales).filter(isTimeScale).forEach(scale => {
            scale.adapters ??= {};
            scale.adapters.date = { ...(scale.adapters.date ?? {}), locale: options.locale };

            const time = scale.time;
            if (!time)
                return;

            if (typeof time.tooltipFormat === 'string' && time.tooltipFormat.trim() === '')
                delete time.tooltipFormat;

            if (time.displayFormats)
                Object.keys(time.displayFormats)
                    .filter(unit => typeof time.displayFormats[unit] !== 'string' || time.displayFormats[unit].trim() === '')
                    .forEach(unit => delete time.displayFormats[unit]);
        });
    };

    // Shared implementation for every chart type. A per-type module passes hooks for the behavior that differs.
    const core = {
        addClickHandler: (chart, dotNetObjectReference) => {
            if (!chart || !chart.canvas || !dotNetObjectReference || chart.blazorExpressClickHandler) return;

            chart.blazorExpressClickHandler = true;
            chart.canvas.addEventListener('click', (event) => {
                if (!isChartEvent(chart, 'click')) return;

                const activeElements = chart.getElementsAtEventForMode(event, 'nearest', { intersect: true }, true);
                if (!activeElements || activeElements.length === 0) return;

                dotNetObjectReference.invokeMethodAsync('HandleClickAsync', getItemPayload(chart, activeElements[0])).catch(() => { });
            });
        },
        // Calls .NET only when the hovered element changes, and once when the pointer leaves all elements.
        addHoverHandler: (chart) => {
            if (!chart || !chart.canvas) return;

            const state = getState(chart);
            if (state.isHoverHandlerAdded) return;

            state.isHoverHandlerAdded = true;

            const isListening = () => state.eventOptions.hasHover && !!state.dotNetObjectReference;

            const notify = (element) => {
                const key = element ? `${element.datasetIndex}:${element.index}` : '';
                if (key === state.lastHoverKey) return;

                state.lastHoverKey = key;

                const payload = element
                    ? getItemPayload(chart, element)
                    : { datasetIndex: null, datasetLabel: null, index: null, label: null, value: null };

                state.dotNetObjectReference.invokeMethodAsync('HandleHoverAsync', payload).catch(() => { });
            };

            // No hit-test unless an OnHover callback is bound.
            chart.canvas.addEventListener('mousemove', (event) => {
                if (!isListening() || !isChartEvent(chart, 'mousemove')) return;

                const activeElements = chart.getElementsAtEventForMode(event, 'nearest', { intersect: true }, true);
                notify(activeElements?.[0]);
            });
            chart.canvas.addEventListener('mouseout', () => {
                if (isListening() && isChartEvent(chart, 'mouseout')) notify(undefined);
            });
        },
        addDatasetData: (elementId, dataLabel, data) => {
            let chart = core.get(elementId);
            if (chart) {
                const chartData = chart.data;
                const chartDatasetData = data;

                if (!chartData.labels.includes(dataLabel))
                    chartData.labels.push(dataLabel);

                const chartDatasets = chartData.datasets;

                if (chartDatasets.length > 0) {
                    let datasetIndex = chartDatasets.findIndex(dataset => dataset.label === chartDatasetData.datasetLabel);
                    if (datasetIndex > -1) {
                        chartDatasets[datasetIndex].data.push(chartDatasetData.data);
                        chart.update();
                    }
                }
            }
        },
        // Appends a time point ({ x, y } or { x, y, r }) to the dataset with that label; no label is added.
        addDatasetPoint: (elementId, datasetLabel, point) => {
            const chart = core.get(elementId);
            const dataset = chart?.data.datasets?.find(d => d.label === datasetLabel);
            if (!dataset || !point)
                return;

            (dataset.data ??= []).push(point);
            chart.update();
        },
        addDatasetsData: (elementId, dataLabel, data, hooks = {}) => {
            let chart = core.get(elementId);
            if (chart && data) {
                const chartData = chart.data;

                if (!chartData.labels.includes(dataLabel)) {
                    chartData.labels.push(dataLabel);

                    if (chartData.datasets.length > 0 && chartData.datasets.length === data.length) {
                        data.forEach(chartDatasetData => {
                            let datasetIndex = chartData.datasets.findIndex(dataset => dataset.label === chartDatasetData.datasetLabel);
                            chartData.datasets[datasetIndex].data.push(chartDatasetData.data);
                            hooks.afterAddDatasetData?.(chartData.datasets[datasetIndex], chartDatasetData);
                        });
                        chart.update();
                    }
                }
            }
        },
        addDataset: (elementId, newDataset) => {
            let chart = core.get(elementId);
            if (chart) {
                chart.data.datasets.push(newDataset);
                chart.update();
            }
        },
        clear: (elementId) => core.get(elementId)?.clear(),
        create: (elementId, type, data, options, plugins, hooks = {}) => {
            let chartEl = document.getElementById(elementId);
            let _plugins = [];

            if (plugins && plugins.length > 0) {
                // register `ChartDataLabels` plugin
                if (plugins.includes('ChartDataLabels')) {
                    _plugins.push(ChartDataLabels);

                    if (hooks.applyDataLabelsBackgroundColorOnCreate)
                        applyDataLabelsBackgroundColor(options);
                }
            }

            applyEventHandlers(options);
            applyTimeScaleOptions(options);
            registerDateAdapter();

            // https://www.chartjs.org/docs/latest/configuration/#configuration-object-structure
            const config = {
                type: type,
                data: data,
                options: options,
                plugins: _plugins
            };

            hooks.beforeCreate?.(config);

            const chart = new Chart(
                chartEl,
                config
            );
        },
        destroy: (elementId) => {
            let chart = core.get(elementId);
            if (chart) {
                chart.destroy();
            }
        },
        downloadImage: async (elementId, fileName, type, quality) => {
            const chart = core.get(elementId);
            if (!chart) return;

            await waitForAnimation(chart);
            if (!chart.canvas) return; // destroyed while waiting

            const link = document.createElement('a');
            link.href = chart.toBase64Image(type ?? 'image/png', quality ?? undefined);
            link.download = fileName;
            link.click();
        },
        // Walks every instance rather than using Chart.getChart(elementId): Blazor removes the canvas from the DOM
        // before disposing the component, so a lookup through document.getElementById would miss it.
        get: (elementId) => {
            let chart;
            Chart.helpers.each(Chart.instances, function (instance) {
                if (instance.canvas.id === elementId) {
                    chart = instance;
                }
            });

            return chart;
        },
        getDataVisibility: (elementId, index) => {
            const chart = core.get(elementId);
            return !!chart && isInRange(index, getDataCount(chart, 0)) && chart.getDataVisibility(index);
        },
        getVisibleDatasetCount: (elementId) => core.get(elementId)?.getVisibleDatasetCount() ?? 0,
        hide: (elementId, datasetIndex, dataIndex) => updateVisibility(elementId, datasetIndex, dataIndex, false),
        initialize: (elementId, type, data, options, plugins, dotNetObjectReference, eventOptions, hooks = {}) => {
            warnIfChartJsIsOutdated();

            let chart = core.get(elementId);
            if (!chart) {
                core.create(elementId, type, data, options, plugins, hooks);
                chart = core.get(elementId);
            }

            if (!chart) return;

            const state = getState(chart);
            state.dotNetObjectReference = dotNetObjectReference ?? null;
            state.isArcChart = !!hooks.isArcChart;
            if (eventOptions)
                state.eventOptions = { ...state.eventOptions, ...eventOptions };

            core.addClickHandler(chart, dotNetObjectReference);
            core.addHoverHandler(chart);
        },
        isDatasetVisible: (elementId, datasetIndex) => {
            const chart = core.get(elementId);
            return !!chart && isInRange(datasetIndex, getDatasetCount(chart)) && chart.isDatasetVisible(datasetIndex);
        },
        render: (elementId) => core.get(elementId)?.render(),
        reset: (elementId) => core.get(elementId)?.reset(),
        resize: (elementId, width, height) => {
            let chart = core.get(elementId);
            if (chart) {
                chart.canvas.parentNode.style.height = toCssValue(height);
                chart.canvas.parentNode.style.width = toCssValue(width);
                chart.resize();
            }
        },
        // Highlights the given elements and shows their tooltip. An empty list clears both;
        // out-of-range elements are ignored, and a list with none in range changes nothing.
        setActiveElements: (elementId, elements) => {
            const chart = core.get(elementId);
            if (!chart || !Array.isArray(elements)) return;

            const activeElements = elements
                .filter(element => isInRange(element?.datasetIndex, getDatasetCount(chart))
                    && isInRange(element?.index, chart.data.datasets[element.datasetIndex]?.data?.length ?? 0))
                .map(element => ({ datasetIndex: element.datasetIndex, index: element.index }));

            if (elements.length > 0 && activeElements.length === 0) return;

            const area = chart.chartArea;
            chart.setActiveElements(activeElements);
            chart.tooltip?.setActiveElements(activeElements, { x: (area.left + area.right) / 2, y: (area.top + area.bottom) / 2 });
            chart.update();
        },
        setDatasetVisibility: (elementId, datasetIndex, visible) => {
            const chart = core.get(elementId);
            if (!chart || !isInRange(datasetIndex, getDatasetCount(chart))) return;

            chart.setDatasetVisibility(datasetIndex, visible);
            recordDatasetVisibility(chart, datasetIndex);
            chart.update();
        },
        setEventOptions: (elementId, eventOptions) => {
            const chart = core.get(elementId);
            if (!chart || !eventOptions) return;

            const state = getState(chart);
            state.eventOptions = { ...state.eventOptions, ...eventOptions };
        },
        show: (elementId, datasetIndex, dataIndex) => updateVisibility(elementId, datasetIndex, dataIndex, true),
        stop: (elementId) => core.get(elementId)?.stop(),
        // Returns the data URL as UTF-8 bytes so .NET can read it as a stream, which on Blazor Server is not
        // limited by the SignalR message size. An empty array means there is no chart.
        toBase64Image: async (elementId, type, quality) => {
            const chart = core.get(elementId);
            if (!chart) return new Uint8Array(0);

            await waitForAnimation(chart);
            if (!chart.canvas) return new Uint8Array(0); // destroyed while waiting

            return new TextEncoder().encode(chart.toBase64Image(type ?? 'image/png', quality ?? undefined));
        },
        toggleDataVisibility: (elementId, index) => {
            const chart = core.get(elementId);
            if (!chart || !isInRange(index, getDataCount(chart, 0))) return;

            chart.toggleDataVisibility(index);
            chart.update();

            if (getState(chart).isArcChart)
                recordDataVisibility(chart, index);
        },
        update: (elementId, type, data, options, mode) => {
            let chart = core.get(elementId);
            if (chart) {
                if (chart.config.plugins && chart.config.plugins.findIndex(x => x.id == 'datalabels') > -1) {
                    // set datalabel background color
                    applyDataLabelsBackgroundColor(options);
                }

                applyEventHandlers(options);
                applyTimeScaleOptions(options);

                const previousDataCount = getDataItemCount(chart);

                // Matched datasets keep their object, so Chart.js animates them from their current values.
                // The direct modes ('none', 'reset') have no animation to keep, and Chart.js applies a reused
                // controller's cached shared element options in them without refreshing it, so they get the new objects.
                if (data && !isDirectUpdateMode(mode))
                    data.datasets = matchDatasets(chart, data.datasets ?? []);

                chart.data = data;
                getState(chart).lastHoverKey = '';
                chart.options = options;

                reapplyVisibilityBeforeUpdate(chart, previousDataCount);

                if (mode)
                    chart.update(mode);
                else
                    chart.update();

                reapplyPointVisibilityAfterUpdate(chart);
            }
            else {
                console.warn(`The chart is not initialized. Initialize it and then call update.`);
            }
        },
    };

    const createChartTypeModule = (hooks = {}) => ({
        ...core,
        addDatasetsData: (elementId, dataLabel, data) => core.addDatasetsData(elementId, dataLabel, data, hooks),
        create: (elementId, type, data, options, plugins) => core.create(elementId, type, data, options, plugins, hooks),
        initialize: (elementId, type, data, options, plugins, dotNetObjectReference, eventOptions) => core.initialize(elementId, type, data, options, plugins, dotNetObjectReference, eventOptions, hooks),
    });

    const arcChartHooks = {
        afterAddDatasetData: (dataset, chartDatasetData) => dataset.backgroundColor.push(chartDatasetData.backgroundColor),
        applyDataLabelsBackgroundColorOnCreate: true,
        isArcChart: true,
    };

    Object.assign(window.blazorexpress.chartjs, core);

    registerDateAdapter();

    window.blazorexpress.chartjs.bar = createChartTypeModule({ applyDataLabelsBackgroundColorOnCreate: true });
    window.blazorexpress.chartjs.bubble = createChartTypeModule();
    window.blazorexpress.chartjs.doughnut = createChartTypeModule(arcChartHooks);
    window.blazorexpress.chartjs.line = createChartTypeModule({
        applyDataLabelsBackgroundColorOnCreate: true,
        beforeCreate: config => {
            if (config.type === 'line')
                config.plugins.push(createTooltipLinePlugin());
        },
    });
    window.blazorexpress.chartjs.pie = createChartTypeModule(arcChartHooks);
    window.blazorexpress.chartjs.polarArea = createChartTypeModule(arcChartHooks);
    window.blazorexpress.chartjs.radar = createChartTypeModule({ applyDataLabelsBackgroundColorOnCreate: true });
    window.blazorexpress.chartjs.scatter = createChartTypeModule({ applyDataLabelsBackgroundColorOnCreate: true });
})();
