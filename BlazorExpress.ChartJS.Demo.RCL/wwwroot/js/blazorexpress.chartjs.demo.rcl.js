async function copyToClipboard(text, dotNetHelper) {
    let isCopied = true;

    try {
        await navigator.clipboard.writeText(text);
        dotNetHelper.invokeMethodAsync('OnCopySuccessJS');
    } catch (err) {
        isCopied = false;
        dotNetHelper.invokeMethodAsync('OnCopyFailJS', err);
    }

    setTimeout(
        () => { dotNetHelper.invokeMethodAsync('ResetCopyStatusJS'); },
        isCopied ? 2000 : 3000
    );
}

function highlightCode() {
    if (Prism) {
        Prism.plugins.customClass.prefix('prism-');
        Prism.highlightAll();
    }
};

function navigateToHeading() {
    if (window.location.hash) {
        // get hash tag in URL
        let hashTagName = window.location.hash.substring(1);
        let el = document.getElementById(hashTagName);
        if (el) {
            // do the scroll
            el.scrollIntoView();
        }
    }
}

// THEMES
const STORAGE_KEY = "be-bulma-theme";
const DEFAULT_THEME = "light";
const SYSTEM_THEME = "system";

const state = {
    chosenTheme: SYSTEM_THEME, // light|dark|system
    appliedTheme: DEFAULT_THEME // light|dark
};

const showActiveTheme = () => {
    let $themeIndicator = document.querySelector(".be-bulma-theme-indicator i");
    if (state.appliedTheme === "light") {
        $themeIndicator.className = "bi bi-sun";
    } else if (state.appliedTheme === "dark") {
        $themeIndicator.className = "bi bi-moon-stars-fill";
    } else {
        $themeIndicator.className = "bi bi-circle-half";
    }

    let $themeSwitchers = document.querySelectorAll(".be-bulma-theme-item");
    $themeSwitchers.forEach((el) => {
        const dataScheme = el.dataset.scheme;
        if (state.chosenTheme === dataScheme) {
            el.classList.add("is-selected");
        } else {
            el.classList.remove("is-selected");
        }
    });
};

function setTheme(theme, save = true) {
    state.chosenTheme = theme;
    state.appliedTheme = theme;

    if (theme === SYSTEM_THEME) {
        state.appliedTheme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    }

    document.documentElement.setAttribute("data-theme", state.appliedTheme);
    if (save) {
        window.localStorage.setItem(STORAGE_KEY, state.chosenTheme);
    }
    showActiveTheme();
    updateDemoCodeThemeCss(state.appliedTheme);
};

function initializeTheme() {
    const localTheme = window.localStorage.getItem(STORAGE_KEY);
    if (localTheme) {
        setTheme(localTheme, false);
    } else {
        setTheme(SYSTEM_THEME);
    }
}

window
    .matchMedia("(prefers-color-scheme: dark)")
    .addEventListener("change", (event) => {
        const theme = event.matches ? "dark" : "light";
        setTheme(theme);
    });

function updateDemoCodeThemeCss(theme) {
    if (theme === "dark") {
        let prismThemeLightLinkEl = document.getElementById('prismThemeLightLink');
        if (prismThemeLightLinkEl)
            prismThemeLightLinkEl?.remove();

        let prismThemeDarkLinkEl = document.createElement("link");
        prismThemeDarkLinkEl.setAttribute("rel", "stylesheet");
        prismThemeDarkLinkEl.setAttribute("href", "/_content/BlazorExpress.ChartJS.Demo.RCL/css/prism-vsc-dark-plus.min.css");
        prismThemeDarkLinkEl.setAttribute("id", "prismThemeDarkLink");

        document.head.append(prismThemeDarkLinkEl);
    }
    else if (theme === "light") {
        let prismThemeDarkLinkEl = document.getElementById('prismThemeDarkLink');
        if (prismThemeDarkLinkEl)
            prismThemeDarkLinkEl?.remove();

        let prismThemeLightLinkEl = document.createElement("link");
        prismThemeLightLinkEl.setAttribute("rel", "stylesheet");
        prismThemeLightLinkEl.setAttribute("href", "/_content/BlazorExpress.ChartJS.Demo.RCL/css/prism-vs.min.css");
        prismThemeLightLinkEl.setAttribute("id", "prismThemeLightLink");

        document.head.append(prismThemeLightLinkEl);
    }
}

// Runs the built-in date adapter's checks in the browser's current time zone, for the unlisted time-axis self-check page.
// Expected values are wall-clock parts or exact instants, so each case holds in every zone; zone-specific DST cases
// run only in their zone and are reported as skipped elsewhere.
function runTimeAxisSelfCheck() {
    const zone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    const Adapter = window.Chart?._adapters?._date;
    const cases = [];

    if (!Adapter)
        return { zone, cases: [{ name: 'Chart.js is loaded', expected: 'true', actual: 'false', status: 'fail' }] };

    const en = new Adapter({ locale: 'en-US' });
    const fr = new Adapter({ locale: 'fr-FR' });
    const invalid = new Adapter({ locale: 'xx-invalid-!!' });
    const p2 = n => String(n).padStart(2, '0');
    const wall = t => {
        if (t === null || t === undefined) return String(t);
        const d = new Date(t);
        return `${d.getFullYear()}-${p2(d.getMonth() + 1)}-${p2(d.getDate())} ${p2(d.getHours())}:${p2(d.getMinutes())}:${p2(d.getSeconds())}.${String(d.getMilliseconds()).padStart(3, '0')}`;
    };
    const normalize = value => String(value).replace(/[  ]/g, ' ');
    const check = (name, run, expected, onlyZones) => {
        if (onlyZones && !onlyZones.includes(zone)) {
            cases.push({ name, expected: normalize(expected), actual: `only in ${onlyZones.join(' or ')}`, status: 'skipped' });
            return;
        }
        let actual;
        try {
            actual = normalize(run());
        } catch (error) {
            actual = `threw ${error?.name ?? 'error'}`;
        }
        cases.push({ name, expected: normalize(expected), actual, status: actual === normalize(expected) ? 'pass' : 'fail' });
    };

    const at = text => en.parse(text);
    const utcZones = ['UTC', 'Etc/UTC', 'Etc/GMT'];
    const newYork = ['America/New_York'];

    // Parse: every wire form C# ChartTimeValue writes, plus numbers, Dates and invalid input.
    check('parse DateOnly "2026-01-05" is local midnight', () => wall(at('2026-01-05')), '2026-01-05 00:00:00.000');
    check('parse wall-clock "2026-01-05T09:00:00.000"', () => wall(at('2026-01-05T09:00:00.000')), '2026-01-05 09:00:00.000');
    check('parse instant "2026-01-05T09:00:00.000Z"', () => at('2026-01-05T09:00:00.000Z'), Date.UTC(2026, 0, 5, 9));
    check('parse offset "2026-01-05T14:30:00+05:30"', () => at('2026-01-05T14:30:00+05:30'), Date.UTC(2026, 0, 5, 9));
    check('parse epoch milliseconds', () => en.parse(1767603600000), 1767603600000);
    check('parse a Date', () => en.parse(new Date(Date.UTC(2026, 0, 5))), Date.UTC(2026, 0, 5));
    check('parse invalid text is null', () => en.parse('not a date'), 'null');
    check('parse 30 Feb is null', () => en.parse('2026-02-30'), 'null');
    check('instant 09:00Z in UTC', () => en.format(at('2026-01-05T09:00:00.000Z'), 'HH:mm'), '09:00', utcZones);
    check('instant 09:00Z in New York', () => en.format(at('2026-01-05T09:00:00.000Z'), 'HH:mm'), '04:00', newYork);
    check('instant 09:00Z in Kolkata', () => en.format(at('2026-01-05T09:00:00.000Z'), 'HH:mm'), '14:30', ['Asia/Kolkata', 'Asia/Calcutta']);
    check('instant 09:00Z in Tokyo', () => en.format(at('2026-01-05T09:00:00.000Z'), 'HH:mm'), '18:00', ['Asia/Tokyo']);

    // add: calendar units keep the wall clock; months clamp to the last day.
    check('add 1 month to 31 Jan 2026', () => wall(en.add(at('2026-01-31'), 1, 'month')), '2026-02-28 00:00:00.000');
    check('add 1 month to 31 Jan 2028 (leap)', () => wall(en.add(at('2028-01-31'), 1, 'month')), '2028-02-29 00:00:00.000');
    check('add 1 year to 29 Feb 2028', () => wall(en.add(at('2028-02-29'), 1, 'year')), '2029-02-28 00:00:00.000');
    check('add 1 quarter to 30 Nov 2026', () => wall(en.add(at('2026-11-30'), 1, 'quarter')), '2027-02-28 00:00:00.000');
    check('add 1 day over 8 Mar 2026', () => wall(en.add(at('2026-03-07T12:00:00.000'), 1, 'day')), '2026-03-08 12:00:00.000');
    check('add 1 day over 1 Nov 2026', () => wall(en.add(at('2026-10-31T12:00:00.000'), 1, 'day')), '2026-11-01 12:00:00.000');
    check('add 1 week', () => wall(en.add(at('2026-03-05T08:00:00.000'), 1, 'week')), '2026-03-12 08:00:00.000');
    check('add 90 minutes', () => en.add(at('2026-01-05T09:00:00.000Z'), 90, 'minute'), Date.UTC(2026, 0, 5, 10, 30));
    check('add 1 hour at the 8 Mar 2026 DST start', () => wall(en.add(at('2026-03-08T01:30:00.000'), 1, 'hour')), '2026-03-08 03:30:00.000', newYork);

    // diff: whole units, truncated; a DST day is one day.
    check('diff days over 8 Mar 2026', () => en.diff(at('2026-03-09'), at('2026-03-08'), 'day'), 1);
    check('diff days over 1 Nov 2026', () => en.diff(at('2026-11-02'), at('2026-11-01'), 'day'), 1);
    check('diff hours of 8 Mar 2026 in New York', () => en.diff(at('2026-03-09'), at('2026-03-08'), 'hour'), 23, newYork);
    check('diff hours of 1 Nov 2026 in New York', () => en.diff(at('2026-11-02'), at('2026-11-01'), 'hour'), 25, newYork);
    check('diff days, partial day truncated', () => en.diff(at('2026-01-06T08:00:00.000'), at('2026-01-05T09:00:00.000'), 'day'), 0);
    check('diff days, negative', () => en.diff(at('2026-01-01'), at('2026-01-05'), 'day'), -4);
    check('diff weeks', () => en.diff(at('2026-01-19'), at('2026-01-05'), 'week'), 2);
    check('diff months 31 Jan to 28 Feb', () => en.diff(at('2026-02-28'), at('2026-01-31'), 'month'), 0);
    check('diff months 15 Jan to 15 Mar', () => en.diff(at('2026-03-15'), at('2026-01-15'), 'month'), 2);
    check('diff quarters', () => en.diff(at('2026-10-01'), at('2026-01-01'), 'quarter'), 3);
    check('diff years', () => en.diff(at('2028-02-29'), at('2026-03-01'), 'year'), 1);
    check('diff seconds', () => en.diff(at('2026-01-05T09:00:30.000'), at('2026-01-05T09:00:00.000'), 'second'), 30);

    // startOf and endOf.
    const sample = at('2026-01-07T15:45:12.345');
    check('startOf second', () => wall(en.startOf(sample, 'second')), '2026-01-07 15:45:12.000');
    check('startOf minute', () => wall(en.startOf(sample, 'minute')), '2026-01-07 15:45:00.000');
    check('startOf hour', () => wall(en.startOf(sample, 'hour')), '2026-01-07 15:00:00.000');
    check('startOf day', () => wall(en.startOf(sample, 'day')), '2026-01-07 00:00:00.000');
    check('startOf week (Sunday)', () => wall(en.startOf(sample, 'week')), '2026-01-04 00:00:00.000');
    check('startOf isoWeek Monday (true)', () => wall(en.startOf(sample, 'isoWeek', true)), '2026-01-05 00:00:00.000');
    check('startOf isoWeek Monday (1)', () => wall(en.startOf(sample, 'isoWeek', 1)), '2026-01-05 00:00:00.000');
    check('startOf isoWeek Saturday (6)', () => wall(en.startOf(sample, 'isoWeek', 6)), '2026-01-03 00:00:00.000');
    check('ISO week of 1 Jan 2026 starts 29 Dec 2025', () => wall(en.startOf(at('2026-01-01'), 'isoWeek', 1)), '2025-12-29 00:00:00.000');
    check('ISO week of 31 Dec 2026 starts 28 Dec 2026', () => wall(en.startOf(at('2026-12-31'), 'isoWeek', 1)), '2026-12-28 00:00:00.000');
    check('startOf month', () => wall(en.startOf(sample, 'month')), '2026-01-01 00:00:00.000');
    check('startOf quarter', () => wall(en.startOf(at('2026-05-20T10:00:00.000'), 'quarter')), '2026-04-01 00:00:00.000');
    check('startOf year', () => wall(en.startOf(sample, 'year')), '2026-01-01 00:00:00.000');
    check('startOf day on 8 Mar 2026', () => wall(en.startOf(at('2026-03-08T12:00:00.000'), 'day')), '2026-03-08 00:00:00.000');
    check('endOf day on 8 Mar 2026', () => wall(en.endOf(at('2026-03-08T12:00:00.000'), 'day')), '2026-03-08 23:59:59.999');
    check('endOf day on 1 Nov 2026', () => wall(en.endOf(at('2026-11-01T12:00:00.000'), 'day')), '2026-11-01 23:59:59.999');
    check('endOf month Feb 2028', () => wall(en.endOf(at('2028-02-10'), 'month')), '2028-02-29 23:59:59.999');
    check('endOf week', () => wall(en.endOf(sample, 'week')), '2026-01-10 23:59:59.999');
    check('endOf year', () => wall(en.endOf(sample, 'year')), '2026-12-31 23:59:59.999');

    // Default formats per unit.
    check('default formats', () => JSON.stringify(en.formats()),
        '{"datetime":"G","millisecond":"HH:mm:ss.fff","second":"T","minute":"t","hour":"t","day":"d MMM","week":"d MMM","month":"MMM yyyy","quarter":"MMM yyyy","year":"yyyy"}');

    // .NET custom format specifiers (Monday 5 Jan 2026, 09:07:03.040 local).
    const t = at('2026-01-05T09:07:03.040');
    const offsetMinutes = -new Date(t).getTimezoneOffset();
    const offset = `${offsetMinutes < 0 ? '-' : '+'}${p2(Math.trunc(Math.abs(offsetMinutes) / 60))}:${p2(Math.abs(offsetMinutes) % 60)}`;
    check('format %d', () => en.format(t, '%d'), '5');
    check('format dd', () => en.format(t, 'dd'), '05');
    check('format ddd', () => en.format(t, 'ddd'), 'Mon');
    check('format dddd', () => en.format(t, 'dddd'), 'Monday');
    check('format %M', () => en.format(t, '%M'), '1');
    check('format MM', () => en.format(t, 'MM'), '01');
    check('format MMM', () => en.format(t, 'MMM'), 'Jan');
    check('format MMMM', () => en.format(t, 'MMMM'), 'January');
    check('format %y', () => en.format(t, '%y'), '26');
    check('format yy', () => en.format(t, 'yy'), '26');
    check('format yyyy', () => en.format(t, 'yyyy'), '2026');
    check('format yyyyy', () => en.format(t, 'yyyyy'), '02026');
    check('format %h and hh', () => en.format(t, '%h hh'), '9 09');
    check('format %H and HH', () => en.format(t, '%H HH'), '9 09');
    check('format mm:ss', () => en.format(t, 'mm:ss'), '07:03');
    check('format %m and %s', () => en.format(t, '%m %s'), '7 3');
    check('format h:mm tt', () => en.format(t, 'h:mm tt'), '9:07 AM');
    check('format %t', () => en.format(t, '%t'), 'A');
    check('format ss.fff', () => en.format(t, 'ss.fff'), '03.040');
    check('format ss.fffffff', () => en.format(t, 'ss.fffffff'), '03.0400000');
    check('format ss.FFF', () => en.format(t, 'ss.FFF'), '03.04');
    check('format ss.FFF with no fraction', () => en.format(at('2026-01-05T09:07:03.000'), 'ss.FFF'), '03');
    check('format zzz', () => en.format(t, 'zzz'), offset);
    check('format K', () => en.format(t, 'K'), offset);
    check('format g (era)', () => en.format(t, 'g'.repeat(2)), 'AD');
    check('format quoted literal', () => en.format(t, "dd 'of' MMMM"), '05 of January');
    check('format double-quoted literal', () => en.format(t, 'dd "at" HH:mm'), '05 at 09:07');
    check('format escaped character', () => en.format(t, 'dd \\M'), '05 M');
    check('format : and / literal', () => en.format(t, 'dd/MM HH:mm'), '05/01 09:07');
    check('format unknown letters copied', () => en.format(t, 'dd Q'), '05 Q');
    check('format unterminated quote', () => en.format(t, "dd 'abc"), '05 abc');
    check('format markup stays text', () => en.format(t, '<b>dd</b>'), '<b>05</b>');
    check('format empty uses G', () => en.format(t, ''), en.format(t, 'G'));

    // .NET standard formats follow the locale's own Intl patterns.
    check('standard d (en-US)', () => en.format(t, 'd'), '1/5/2026');
    check('standard D (en-US)', () => en.format(t, 'D'), 'Monday, January 5, 2026');
    check('standard t (en-US)', () => en.format(t, 't'), '9:07 AM');
    check('standard T (en-US)', () => en.format(t, 'T'), '9:07:03 AM');
    check('standard g (en-US)', () => en.format(t, 'g'), '1/5/2026, 9:07 AM');
    check('standard G (en-US)', () => en.format(t, 'G'), '1/5/2026, 9:07:03 AM');
    check('standard M (en-US)', () => en.format(t, 'M'), 'January 5');
    check('standard Y (en-US)', () => en.format(t, 'Y'), 'January 2026');
    check('standard s', () => en.format(t, 's'), '2026-01-05T09:07:03');
    check('standard f contains the date and time', () => /Monday.*January 5.*2026.*9:07/.test(normalize(en.format(t, 'f'))), 'true');

    // Locales.
    check('fr-FR dd MMM', () => fr.format(t, 'dd MMM'), '05 janv.');
    check('fr-FR dddd d MMMM', () => fr.format(t, 'dddd d MMMM'), 'lundi 5 janvier');
    check('invalid locale falls back without throwing', () => typeof invalid.format(t, 'dd MMM yyyy'), 'string');

    return {
        zone,
        cases,
        passed: cases.filter(c => c.status === 'pass').length,
        failed: cases.filter(c => c.status === 'fail').length,
        skipped: cases.filter(c => c.status === 'skipped').length,
    };
}

// JavaScript functions the demos reference from C# with `new ChartFunction("<name>")`.
// Each function is registered once, after blazorexpress.chartjs.js has loaded.
(() => {
    const register = window.blazorexpress?.chartjs?.registerFunction;
    if (!register)
        return;

    // Tick callback: (value, index, ticks) => label.
    register('demo.thousandsLabel', value => `${value / 1000}k units`);

    // Tooltip label callback: (tooltipItem) => text.
    register('demo.unitsTooltipLabel', tooltipItem => `${tooltipItem.dataset.label}: ${tooltipItem.parsed.y.toLocaleString()} units`);

    // Legend labels filter: (legendItem, data) => keep it?
    register('demo.hideTargetLegendItems', legendItem => !legendItem.text.startsWith('Target'));

    // Legend onClick: replaces Chart.js's default toggle, so call the default only on Shift-click.
    register('demo.toggleOnShiftClick', function (e, legendItem, legend) {
        if (e.native?.shiftKey)
            Chart.defaults.plugins.legend.onClick.call(this, e, legendItem, legend);
    });

    // Line segment styles: (segmentContext) => value. A segment touching a skipped (null) point is drawn dashed and grey.
    const touchesGap = ctx => ctx.p0.skip || ctx.p1.skip;
    register('demo.dashOverGaps', ctx => touchesGap(ctx) ? [6, 6] : undefined);
    register('demo.greyOverGaps', ctx => touchesGap(ctx) ? 'rgb(160, 160, 160)' : undefined);

    // Scriptable background colour: (context) => colour. Returns a vertical gradient once the chart area is known.
    register('demo.verticalGradient', context => {
        const { ctx, chartArea } = context.chart;
        if (!chartArea)
            return 'rgba(54, 162, 235, 0.2)';

        const gradient = ctx.createLinearGradient(0, chartArea.bottom, 0, chartArea.top);
        gradient.addColorStop(0, 'rgba(54, 162, 235, 0)');
        gradient.addColorStop(1, 'rgba(54, 162, 235, 0.6)');
        return gradient;
    });
})();

// Unlisted JS functions self-check page: registers its test functions, records console errors, and checks live charts.
window.jsFunctionsSelfCheck = (() => {
    const errors = [];
    const counters = { gradient: 0, onComplete: 0, legendFilter: 0 };
    let lastGradientIsCanvasGradient = false;
    let isCapturing = false;

    // Canvas IDs by self-check key (for example 'sc-ticks'), passed in by the page.
    let chartIds = {};
    const charts = key => Chart.getChart(chartIds[key] ?? key);
    const result = (name, expected, actual) => ({ name, expected: String(expected), actual: String(actual), status: String(expected) === String(actual) ? 'pass' : 'fail' });
    const errorsFor = text => errors.filter(message => message.includes(text)).length;

    const begin = () => {
        if (!isCapturing) {
            isCapturing = true;
            const original = console.error.bind(console);
            console.error = (...args) => {
                errors.push(args.map(a => (a instanceof Error ? a.message : String(a))).join(' '));
                original(...args);
            };
        }

        const register = window.blazorexpress.chartjs.registerFunction;
        register('selfCheck.currency', value => `$${value}`);
        register('selfCheck.label', () => 'v1');
        register('selfCheck.throws', () => { throw new Error('self-check error'); });
        register('selfCheck.onComplete', () => { counters.onComplete++; });
        register('selfCheck.tooltipLabel', item => `L:${item.formattedValue}`);
        register('selfCheck.legendShift', function (e, legendItem, legend) {
            if (e.native?.shiftKey)
                Chart.defaults.plugins.legend.onClick.call(this, e, legendItem, legend);
        });
        register('selfCheck.legendFilter', legendItem => { counters.legendFilter++; return !legendItem.text.toLowerCase().startsWith('hidden'); });
        register('selfCheck.legendSort', (a, b) => b.text.localeCompare(a.text));
        register('selfCheck.generateLabels', chart =>
            Chart.defaults.plugins.legend.labels.generateLabels(chart).map(item => ({ ...item, text: item.text.toUpperCase() })));
        register('selfCheck.chartClick', () => { });
        register('selfCheck.segmentDash', ctx => (ctx.p0.skip || ctx.p1.skip ? [6, 6] : undefined));
        register('selfCheck.formatter', value => `n=${value}`);
        register('selfCheck.sliceColor', context => ['#4e79a7', '#f28e2b', '#e15759', '#76b7b2', '#59a14f'][context.dataIndex % 5]);
        register('selfCheck.gradient', context => {
            counters.gradient++;
            const { ctx, chartArea } = context.chart;
            if (!chartArea)
                return 'rgba(54, 162, 235, 0.2)';

            const gradient = ctx.createLinearGradient(0, chartArea.bottom, 0, chartArea.top);
            gradient.addColorStop(0, 'rgba(54, 162, 235, 0)');
            gradient.addColorStop(1, 'rgba(54, 162, 235, 0.6)');
            lastGradientIsCanvasGradient = gradient instanceof CanvasGradient;
            return gradient;
        });

        return true;
    };

    // Runs after every chart is initialized.
    const runInitial = ids => {
        chartIds = ids ?? {};
        const cases = [];
        const ticks = charts('sc-ticks');
        const missing = charts('sc-missing');
        const legendChart = charts('sc-legend');
        const datalabels = charts('sc-datalabels');
        const format = charts('sc-format');
        const compact = charts('sc-compact');
        const pie = charts('sc-pie');

        cases.push(result('Tick callback labels (AC-01)', '$0', ticks?.scales.y.ticks[0]?.label));
        cases.push(result('Tooltip label callback resolved (AC-04)', 'L:7', ticks?.options.plugins.tooltip.callbacks.label({ formattedValue: '7' })));
        cases.push(result('Gradient function called (AC-06)', true, counters.gradient > 0));
        cases.push(result('Gradient returns a CanvasGradient (AC-06)', true, lastGradientIsCanvasGradient));
        cases.push(result('Segment dash over a gap (AC-05)', '6,6', String(ticks?.data.datasets[0].segment?.borderDash?.({ p0: { skip: true }, p1: { skip: false } }))));
        cases.push(result('Segment solid elsewhere (AC-05)', 'undefined', String(ticks?.data.datasets[0].segment?.borderDash?.({ p0: { skip: false }, p1: { skip: false } }))));

        cases.push(result('Unregistered tooltip title: one error with path (AC-03)', 1, errors.filter(m => m.includes('"selfCheck.missing"') && m.includes('options.plugins.tooltip.callbacks.title')).length));
        cases.push(result('Unregistered title falls back to Chart.js default (AC-03)', 'function', typeof missing?.options.plugins.tooltip.callbacks.title));
        cases.push(result('"window.open" not resolved (AC-03, AC-07)', false, missing?.options.onClick === window.open));
        cases.push(result('"window.open" logged (AC-03)', 1, errorsFor('"window.open"')));
        cases.push(result('"fetch" not resolved (AC-03, AC-07)', false, missing?.options.onHover === window.fetch));
        cases.push(result('"fetch" logged (AC-03)', 1, errorsFor('"fetch"')));
        cases.push(result('"__proto__" not resolved (AC-03, AC-07)', 1, errorsFor('"__proto__"')));
        cases.push(result('Late function logged before registration (AC-03)', 1, errorsFor('"selfCheck.late"')));
        cases.push(result('Re-registration target starts as v1 (AC-15)', 'v1', missing?.scales.x.ticks[0]?.label));

        cases.push(result('Throwing tick callback logged once (AC-15)', 1, errorsFor('"selfCheck.throws"')));
        cases.push(result('Throwing tick callback: chart still created (AC-15)', true, !!charts('sc-throws')));

        cases.push(result('Legend filter applied (AC-04)', false, legendChart?.legend.legendItems.some(item => item.text.startsWith('HIDDEN'))));
        cases.push(result('Legend generateLabels applied (AC-04)', true, legendChart?.legend.legendItems.every(item => item.text === item.text.toUpperCase())));
        cases.push(result('Legend sort applied (AC-04)', 'BETA,ALPHA', legendChart?.legend.legendItems.map(item => item.text).join(',')));
        cases.push(result('Chart onClick resolved (AC-10)', 'function', typeof legendChart?.options.onClick));

        const legendItem = legendChart?.legend.legendItems.find(item => item.text === 'ALPHA');
        const legend = legendChart?.legend;
        legend?.options.onClick.call(legend, { native: { shiftKey: false } }, legendItem, legend);
        cases.push(result('Legend plain click leaves Alpha shown (AC-11)', true, legendChart?.isDatasetVisible(legendItem?.datasetIndex)));
        legend?.options.onClick.call(legend, { native: { shiftKey: true } }, legendItem, legend);
        cases.push(result('Legend Shift-click hides Alpha (AC-11)', false, legendChart?.isDatasetVisible(legendItem?.datasetIndex)));

        cases.push(result('Datalabels chart-wide formatter (AC-12)', 'n=5', datalabels?.config.options.plugins.datalabels.formatter?.(5)));
        cases.push(result('Datalabels injected background kept (AC-12)', 'function', typeof datalabels?.config.options.plugins.datalabels.backgroundColor));
        cases.push(result('Datalabels per-dataset formatter (AC-12)', 'n=9', datalabels?.data.datasets[0].datalabels?.formatter?.(9)));
        const labelBackground = datalabels?.config.options.plugins.datalabels.backgroundColor;
        cases.push(result('Datalabels background with a function colour (AC-06, AC-08)', '#f28e2b',
            labelBackground?.({ chart: datalabels, dataIndex: 1, datasetIndex: 0, dataset: datalabels.data.datasets[0] })));
        cases.push(result('Datalabels background with a plain colour (AC-08)', 'red',
            labelBackground?.({ chart: datalabels, dataIndex: 0, datasetIndex: 0, dataset: { backgroundColor: 'red' } })));

        // Chart.js's numeric tick formatter always draws the zero tick as "0", so check the next tick.
        cases.push(result('Ticks.Format currency label (AC-13)', '$5,000', format?.scales.y.ticks[1]?.label));
        cases.push(result('Ticks.Format tooltip value (AC-13, A-04)', '$25,000.00', format?.scales.y.getLabelForValue(25000)));
        cases.push(result('Ticks.Format compact (AC-13)', '2.5M', compact?.scales.y.getLabelForValue(2500000)));

        cases.push(result('Pie slice colour function (AC-06)', 'function', typeof pie?.data.datasets[0].backgroundColor));

        return cases;
    };

    // Called between the initial run and the C# UpdateAsync of `sc-missing`.
    const registerLate = () => {
        const register = window.blazorexpress.chartjs.registerFunction;
        register('selfCheck.late', value => `late ${value}`);
        register('selfCheck.label', () => 'v2');
        return true;
    };

    // Runs after the C# updates, add-data and add-dataset calls.
    const runAfterUpdate = async () => {
        // Animations need visible frames; wait up to 3 s for onComplete.
        for (let waited = 0; counters.onComplete === 0 && waited < 3000; waited += 100)
            await new Promise(resolve => setTimeout(resolve, 100));

        const cases = [];
        const missing = charts('sc-missing');
        const ticks = charts('sc-ticks');
        const legendChart = charts('sc-legend');
        const pie = charts('sc-pie');

        cases.push(result('Late registration resolves on UpdateAsync (AC-03)', 'late 0', missing?.scales.y.ticks[0]?.label));
        cases.push(result('Re-registration applies on UpdateAsync (AC-15)', 'v2', missing?.scales.x.ticks[0]?.label));
        cases.push(result('References survive UpdateAsync (AC-02)', '$0', ticks?.scales.y.ticks[0]?.label));
        cases.push(result('Added dataset colour resolved (AC-14)', 'function', typeof ticks?.data.datasets.at(-1)?.borderColor));
        cases.push(result('Legend visibility kept across UpdateAsync (AC-11)', false, legendChart?.isDatasetVisible(legendChart.data.datasets.findIndex(d => d.label === 'Alpha'))));
        cases.push(result('Pie add-data keeps the colour function (AC-14)', 'function', typeof pie?.data.datasets[0].backgroundColor));
        cases.push(result('Pie add-data adds the slice (AC-14)', 4, pie?.data.datasets[0].data.length));
        cases.push(result('Animation onComplete called (AC-10)', true, counters.onComplete > 0));
        cases.push(result('No unexpected console errors', 0, errors.filter(m =>
            !['"selfCheck.missing"', '"window.open"', '"fetch"', '"__proto__"', '"selfCheck.late"', '"selfCheck.throws"'].some(t => m.includes(t))).length));

        return cases;
    };

    return { begin, registerLate, runAfterUpdate, runInitial };
})();
