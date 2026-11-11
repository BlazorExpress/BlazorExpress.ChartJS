async function copyToClipboard(text, dotNetHelper) {
    let isCopied = true;

    // The component that asked for the copy can be gone before a callback runs (the user left the page); that is not an error.
    const notify = (method, ...args) => dotNetHelper.invokeMethodAsync(method, ...args).catch(() => { });

    try {
        await navigator.clipboard.writeText(text);
        notify('OnCopySuccessJS');
    } catch (err) {
        isCopied = false;
        notify('OnCopyFailJS', err);
    }

    setTimeout(() => notify('ResetCopyStatusJS'), isCopied ? 2000 : 3000);
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
    applyDemoChartTheme(state.appliedTheme);
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

    // The four functions below are also shown on their sample pages, from the *_Snippet.js files next to the demos.
    // Keep each pair in step.

    // External (HTML) tooltip: (context) => void. Builds the tooltip from elements and text only, so chart data is never read as markup.
    register('demo.htmlTooltip', context => {
        const { chart, tooltip } = context;
        const parent = chart.canvas.parentNode;

        let box = parent.querySelector('.demo-html-tooltip');
        if (!box) {
            box = document.createElement('div');
            box.className = 'demo-html-tooltip';
            Object.assign(box.style, {
                position: 'absolute', pointerEvents: 'none', transform: 'translate(-50%, 10px)', transition: 'opacity .15s ease',
                background: 'rgba(0, 0, 0, 0.8)', color: '#fff', borderRadius: '6px', padding: '6px 10px',
                font: '12px sans-serif', whiteSpace: 'nowrap', zIndex: '10',
            });
            parent.style.position = 'relative';
            parent.appendChild(box);
        }

        if (tooltip.opacity === 0) {
            box.style.opacity = '0';
            return;
        }

        box.replaceChildren();

        for (const line of tooltip.title ?? []) {
            const title = document.createElement('div');
            title.style.fontWeight = '600';
            title.textContent = line;
            box.appendChild(title);
        }

        (tooltip.body ?? []).forEach((body, index) => {
            const row = document.createElement('div');
            const swatch = document.createElement('span');
            Object.assign(swatch.style, {
                display: 'inline-block', width: '10px', height: '10px', marginRight: '6px', borderRadius: '50%',
                background: tooltip.labelColors[index].borderColor,
            });
            const text = document.createElement('span');
            text.textContent = body.lines.join(' ');
            row.append(swatch, text);
            box.appendChild(row);
        });

        box.style.opacity = '1';
        box.style.left = `${chart.canvas.offsetLeft + tooltip.caretX}px`;
        box.style.top = `${chart.canvas.offsetTop + tooltip.caretY}px`;
    });

    // Animation onProgress and onComplete: (animation) => void. Moves the <progress> element next to the chart.
    register('demo.showAnimationProgress', animation => {
        const bar = document.getElementById('demo-animation-progress');
        if (bar)
            bar.value = animation.numSteps > 0 ? animation.currentStep / animation.numSteps : 1;
    });
    register('demo.completeAnimationProgress', () => {
        const bar = document.getElementById('demo-animation-progress');
        if (bar)
            bar.value = 1;
    });

    // Scriptable background colour for arcs: (context) => colour. Each slice fades from a light centre to its full colour at the rim.
    register('demo.radialGradient', context => {
        const colors = ['0, 114, 178', '230, 159, 0', '0, 158, 115', '204, 121, 167', '86, 180, 233']; // Okabe–Ito
        const rgb = colors[context.dataIndex % colors.length];
        const { ctx, chartArea } = context.chart;
        if (!chartArea)
            return `rgb(${rgb})`;

        const centerX = (chartArea.left + chartArea.right) / 2;
        const centerY = (chartArea.top + chartArea.bottom) / 2;
        const radius = Math.min(chartArea.right - chartArea.left, chartArea.bottom - chartArea.top) / 2;

        const gradient = ctx.createRadialGradient(centerX, centerY, 0, centerX, centerY, radius);
        gradient.addColorStop(0.45, `rgba(${rgb}, 0.25)`);
        gradient.addColorStop(1, `rgb(${rgb})`);
        return gradient;
    });

    // Datalabels formatter: (value, context) => text. Shows each bar as a share of its dataset's total.
    register('demo.percentOfTotal', (value, context) => {
        const total = context.dataset.data.reduce((sum, item) => sum + (item ?? 0), 0);
        return total > 0 ? `${Math.round(value / total * 100)}%` : '';
    });
})();

// One self-check case: passes when the expected and the actual value read the same.
const selfCheckResult = (name, expected, actual) =>
    ({ name, expected: String(expected), actual: String(actual), status: String(expected) === String(actual) ? 'pass' : 'fail' });

// Unlisted JS functions self-check page: registers its test functions, records console errors, and checks live charts.
window.jsFunctionsSelfCheck = (() => {
    const errors = [];
    const counters = { gradient: 0, onComplete: 0, legendFilter: 0 };
    let lastGradientIsCanvasGradient = false;
    let isCapturing = false;

    // Canvas IDs by self-check key (for example 'sc-ticks'), passed in by the page.
    let chartIds = {};
    const charts = key => Chart.getChart(chartIds[key] ?? key);
    const result = selfCheckResult;
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

// Theme-aware demo charts (story 26110206 AC-09). The text, grid and border colours of every chart on the demo site follow
// the site's light or dark theme wherever the chart leaves them at their Chart.js defaults. Colours a demo sets itself, and
// the colours the library always sends (for example the black title colour), are kept. This is demo-site behaviour only:
// the same demo code in another app gets the Chart.js defaults.
//
// Chart.defaults.borderColor and Chart.defaults.backgroundColor are left alone: the Chart.js Colors plugin gives its palette
// only while both still hold their original values. The grid, border and angle lines take their colour from the scale
// defaults instead, which the Colors plugin does not look at.
const demoChartThemes = {
    light: { color: '#666', lineColor: 'rgba(0,0,0,0.1)', backdropColor: 'rgba(255, 255, 255, 0.75)' },
    dark: { color: '#c9d1d9', lineColor: 'rgba(255, 255, 255, 0.18)', backdropColor: 'rgba(0, 0, 0, 0.55)' },
};

// Scale options that take their default from the theme: [group, property, theme colour].
const demoScaleThemeColors = [
    ['ticks', 'color', 'color'], ['ticks', 'backdropColor', 'backdropColor'], ['title', 'color', 'color'], ['pointLabels', 'color', 'color'],
    ['grid', 'color', 'lineColor'], ['border', 'color', 'lineColor'], ['angleLines', 'color', 'lineColor'],
];
let demoChartThemeApplied = null;

// Any value other than 'dark' gets the light colours, which are the Chart.js defaults.
const getDemoChartThemeColors = theme => demoChartThemes[theme] ?? demoChartThemes.light;

function setDemoChartDefaults(colors) {
    Chart.defaults.color = colors.color;
    Chart.defaults.scale.ticks.backdropColor = colors.backdropColor;
    Chart.defaults.scale.grid.color = colors.lineColor;
    Chart.defaults.scale.border.color = colors.lineColor;

    const angleLines = Chart.defaults.scales?.radialLinear?.angleLines;
    if (angleLines)
        angleLines.color = colors.lineColor;
}

// Chart.js copies the scale defaults into each chart when the chart is created, so new defaults alone do not reach the scales
// of a chart that is already drawn. A scale colour that still equals the previous theme's default is removed here, and
// Chart.js fills it in again from the new defaults on the next update. A colour the demo chose is left alone, unless it is
// exactly the previous theme's default.
function releaseDemoScaleColors(chart, previousColors) {
    for (const scale of Object.values(chart.config.options?.scales ?? {})) {
        for (const [group, property, key] of demoScaleThemeColors) {
            if (scale?.[group]?.[property] === previousColors[key])
                delete scale[group][property];
        }
    }
}

// Called by setTheme: redraws every chart with the new defaults and drops the kept gallery thumbnails.
function applyDemoChartTheme(theme) {
    if (!window.Chart || demoChartThemeApplied === theme)
        return;

    // Charts drawn before the first call used the Chart.js defaults, which are the light colours.
    const previousColors = getDemoChartThemeColors(demoChartThemeApplied);
    const isFirst = demoChartThemeApplied === null;
    demoChartThemeApplied = theme;
    setDemoChartDefaults(getDemoChartThemeColors(theme));

    Chart.helpers.each(Chart.instances, chart => {
        try {
            releaseDemoScaleColors(chart, previousColors);
            chart.update('none');
        } catch (error) {
            // One chart that cannot redraw must not keep the others in the old theme.
            console.warn('A chart could not be redrawn for the new theme.', error);
        }
    });

    if (!isFirst)
        window.demoThumbnails?.invalidate();
}

// Keyboard reach for the demo tabs and the copy icon (story 26110206 A-03). The UI package of the demo site renders the
// "Example / View Source" tabs as links without an href and the copy control as an icon, and the Tab key skips both.
// This gives them a tab stop, a role and Enter or Space activation, and tells assistive technology which tab is shown.
// The copy icon sits in the snippet of a demo and in the toolbar of a code block. The package components are unchanged.
(() => {
    const selector = '.tabs li > a:not([href]), :is(.be-bulma-doc-example-snippet, .be-bulma-doc-example-toolbar) i[role="button"]';

    const enhance = () => {
        for (const element of document.querySelectorAll(selector)) {
            if (!element.hasAttribute('tabindex'))
                element.setAttribute('tabindex', '0');
            if (!element.hasAttribute('role'))
                element.setAttribute('role', 'button');
            if (element.tagName === 'I' && !element.hasAttribute('aria-label'))
                element.setAttribute('aria-label', 'Copy the source to the clipboard');

            if (element.tagName === 'A') {
                const pressed = String(element.parentElement.classList.contains('is-active'));
                if (element.getAttribute('aria-pressed') !== pressed)
                    element.setAttribute('aria-pressed', pressed);
            }
        }
    };

    let scheduled = false;
    const schedule = () => {
        if (scheduled)
            return;

        scheduled = true;
        requestAnimationFrame(() => {
            scheduled = false;
            enhance();
        });
    };

    document.addEventListener('keydown', event => {
        if ((event.key === 'Enter' || event.key === ' ') && event.target instanceof Element && event.target.matches(selector)) {
            event.preventDefault();
            event.target.click();
        }
    });

    // The package marks the shown tab with a class on its list item, so class changes are watched as well.
    new MutationObserver(schedule).observe(document.documentElement, { childList: true, subtree: true, attributes: true, attributeFilter: ['class'] });
    schedule();
})();

const demoSleep = ms => new Promise(resolve => setTimeout(resolve, ms));

// Live gallery thumbnails (story 26110206 R-04). A card asks for its thumbnail when it scrolls into view; the page renders
// the sample's demo in an invisible host, and `captureInto` turns its first chart into an image. Images are kept in memory
// per sample and theme, and dropped when the theme changes.
window.demoThumbnails = (() => {
    const images = new Map();   // "<sample url>|<theme>" -> data URL
    const observed = new Map(); // card preview element -> { dotNetRef, key }
    const cacheKey = key => `${key}|${state.appliedTheme}`;
    const thumbnailWidth = 480; // in canvas pixels: enough for a card on a high-density screen, far smaller than the capture

    const show = (container, dataUrl) => {
        const image = container.querySelector('.be-sample-thumb-image');
        if (image)
            image.src = dataUrl;
    };

    const observer = 'IntersectionObserver' in window
        ? new IntersectionObserver(entries => {
            for (const entry of entries) {
                const item = observed.get(entry.target);
                if (!entry.isIntersecting || !item)
                    continue;

                observer.unobserve(entry.target);
                item.dotNetRef.invokeMethodAsync('OnVisibleAsync').catch(() => { /* the card was removed meanwhile */ });
            }
        }, { rootMargin: '200px' })
        : null;

    // Returns true when an image is already kept for this sample and theme; otherwise watches for the card to scroll into view.
    const observe = (container, dotNetRef, key) => {
        if (!container)
            return false;

        observed.set(container, { dotNetRef, key });

        const kept = images.get(cacheKey(key));
        if (kept) {
            show(container, kept);
            return true;
        }

        observer?.observe(container);
        return false;
    };

    const unobserve = container => {
        if (!container)
            return;

        observer?.unobserve(container);
        observed.delete(container);
    };

    // The kept image is the chart scaled down to the width a card needs, not the full capture.
    const toSmallImage = source => {
        const scale = Math.min(1, thumbnailWidth / source.width);
        const small = document.createElement('canvas');
        small.width = Math.max(1, Math.round(source.width * scale));
        small.height = Math.max(1, Math.round(source.height * scale));
        small.getContext('2d').drawImage(source, 0, 0, small.width, small.height);
        return small.toDataURL('image/png');
    };

    // Waits for the first chart in `host`, stops its animation and returns it as a small PNG data URL.
    // Returns null when no chart with a real size appears in time, or when the capture fails.
    const capture = async (host, timeoutMs = 5000) => {
        const end = Date.now() + timeoutMs;

        while (Date.now() < end) {
            // The card left the page (a filter change, or the user moved on): give the capture queue back at once.
            if (!host?.isConnected)
                return null;

            const canvas = host.querySelector('canvas');
            const chart = canvas ? Chart.getChart(canvas) : null;

            if (chart && chart.width > 0 && chart.height > 0) {
                try {
                    chart.stop();
                    chart.options.animation = false;
                    chart.update('none');
                    return toSmallImage(chart.canvas);
                } catch {
                    return null;
                }
            }

            await demoSleep(100);
        }

        return null;
    };

    const captureInto = async (container, host, key, timeoutMs) => {
        const dataUrl = await capture(host, timeoutMs);
        if (!dataUrl || !container?.isConnected)
            return false;

        images.set(cacheKey(key), dataUrl);
        show(container, dataUrl);
        return true;
    };

    // The theme changed: drop the kept images and let the visible cards draw again.
    const invalidate = () => {
        images.clear();

        for (const [container, item] of observed) {
            item.dotNetRef.invokeMethodAsync('OnInvalidated').catch(() => { /* the card was removed meanwhile */ });
            observer?.unobserve(container);
            observer?.observe(container);
        }
    };

    return { capture, captureInto, invalidate, observe, unobserve };
})();

// Unlisted samples self-check page (story 26110206 F-01): drives the sample pages in a frame on that page,
// and reports the charts drawn and the console errors for each one.
window.samplesSelfCheck = (() => {
    const waitFor = async (condition, timeoutMs = 8000, stepMs = 100) => {
        const end = Date.now() + timeoutMs;
        while (Date.now() < end) {
            try {
                if (condition()) return true;
            } catch {
                // The frame is between two pages; try again.
            }
            await demoSleep(stepMs);
        }
        return false;
    };

    const result = selfCheckResult;

    // Records console errors, uncaught errors and unhandled rejections of the frame's window.
    const watchErrors = win => {
        if (win.__samplesSelfCheckErrors)
            return win.__samplesSelfCheckErrors;

        const errors = win.__samplesSelfCheckErrors = [];
        const original = win.console.error;
        win.console.error = (...args) => {
            errors.push(args.map(a => a?.message ?? String(a)).join(' ').slice(0, 300));
            original.apply(win.console, args);
        };
        win.addEventListener('error', e => errors.push(String(e.message).slice(0, 300)));
        win.addEventListener('unhandledrejection', e => errors.push(String(e.reason?.message ?? e.reason).slice(0, 300)));
        return errors;
    };

    // Waits until the demo site has started in the frame. A frame that never starts stops the run with a clear message,
    // which the page shows as a failing case.
    const ready = async frame => {
        const started = await waitFor(() => frame.contentWindow.Blazor?.navigateTo && frame.contentDocument.querySelector('#app')
            && !frame.contentDocument.querySelector('.be-loader-shell'), 60000, 200);
        if (!started)
            throw new Error('The demo site did not start in the frame within 60 seconds.');
    };

    // Marks what the frame shows now, so the next page's content can be told apart from it, then navigates inside the frame.
    const fresh = ':not([data-self-check-old])';
    const navigate = (frame, url) => {
        frame.contentDocument.querySelectorAll('.be-sample-content, .be-sample-grid, .be-sample-not-found-slug')
            .forEach(el => el.setAttribute('data-self-check-old', ''));
        frame.contentWindow.Blazor.navigateTo(url);
    };

    // Routing, old anchors, the Colors plugin and the injection cases. `charts` is [{ key, route, count, anchor }];
    // `colorsUrl` is a sample whose first chart has datasets without colours and the Colors plugin on.
    const runFrameCases = async (frame, charts, galleryUrl, colorsUrl) => {
        const cases = [];
        await ready(frame);
        const doc = () => frame.contentDocument;
        const loc = () => frame.contentWindow.location;
        const literal = '<b>x</b>"\'';

        for (const chart of charts) {
            // The chart page is an overview with one card per sample.
            navigate(frame, chart.route);
            await waitFor(() => loc().pathname === chart.route && doc().querySelector(`.be-sample-grid${fresh}`));
            cases.push(result(`${chart.key}: overview shows one card per sample`, chart.count,
                doc().querySelectorAll(`.be-sample-grid${fresh} .be-sample-card`).length));
            cases.push(result(`${chart.key}: overview draws no chart`, 0, doc().querySelectorAll('.be-sample-content canvas').length));

            // An old anchor opens the sample page (AC-06).
            navigate(frame, galleryUrl);
            await waitFor(() => loc().pathname === galleryUrl);
            navigate(frame, `${chart.route}#${chart.anchor}`);
            await waitFor(() => loc().pathname === `${chart.route}/${chart.anchor}`, 5000);
            cases.push(result(`${chart.key}: old anchor #${chart.anchor} opens the sample page`, `${chart.route}/${chart.anchor}`, loc().pathname));
        }

        if (charts.length > 0) {
            const first = charts[0];

            // An anchor that is not a sample leaves the overview as it is.
            navigate(frame, galleryUrl);
            await waitFor(() => loc().pathname === galleryUrl);
            navigate(frame, `${first.route}#no-such-sample`);
            await waitFor(() => loc().pathname === first.route && doc().querySelector(`.be-sample-grid${fresh}`), 5000);
            cases.push(result('An anchor that is not a sample stays on the overview', first.route, loc().pathname));

            // An unknown slug shows "sample not found", as text (OWASP A05, A10).
            navigate(frame, `${first.route}/${encodeURIComponent(literal)}`);
            await waitFor(() => doc().querySelector(`.be-sample-not-found-slug${fresh}`), 5000);
            const slug = doc().querySelector(`.be-sample-not-found-slug${fresh}`);
            cases.push(result('Unknown slug is shown as literal text', literal, slug?.textContent));
            cases.push(result('Unknown slug adds no element', 0, slug ? slug.children.length : -1));
            cases.push(result('Unknown slug shows the cards of its chart', first.count,
                doc().querySelectorAll(`.be-sample-grid${fresh} .be-sample-card`).length));
        }

        if (colorsUrl) {
            // The Chart.js Colors plugin still gives its palette to datasets without colours, on a chart created in either
            // theme (AC-02, AC-09). The site's chart theme must not switch the plugin off.
            const win = frame.contentWindow;
            const appliedTheme = doc().documentElement.getAttribute('data-theme') === 'dark' ? 'dark' : 'light';
            const firstChart = () => {
                const canvas = doc().querySelector(`.be-sample-content${fresh} canvas`);
                return canvas ? win.Chart.getChart(canvas) : null;
            };

            for (const theme of ['light', 'dark']) {
                win.setTheme(theme, false);
                navigate(frame, galleryUrl);
                await waitFor(() => loc().pathname === galleryUrl);
                navigate(frame, colorsUrl);
                await waitFor(() => loc().pathname === colorsUrl && firstChart()?.data.datasets.some(d => d.backgroundColor), 5000);
                const datasets = firstChart()?.data.datasets ?? [];
                cases.push(result(`Colors plugin gives its palette in the ${theme} theme`, true, datasets.length > 0 && datasets.every(d => !!d.backgroundColor)));
            }

            win.setTheme(appliedTheme, false);
        }

        // The gallery search text comes from the query string and is shown as text (OWASP A05).
        navigate(frame, `${galleryUrl}?q=${encodeURIComponent(literal)}`);
        await waitFor(() => loc().pathname === galleryUrl && doc().querySelector('.be-gallery-search')?.value === literal, 5000);
        await demoSleep(300);
        cases.push(result('Gallery search text is kept as literal text', literal, doc().querySelector('.be-gallery-search')?.value));
        cases.push(result('Gallery search text adds no element', 0, doc().querySelectorAll('.be-gallery b').length));
        cases.push(result('Gallery "no match" message shows the literal text', true,
            (doc().querySelector('.be-gallery-empty')?.textContent ?? '').includes(literal)));

        // A thumbnail that cannot be captured yields nothing, so the card keeps its icon (OWASP A10).
        const capture = window.demoThumbnails?.capture;
        let captured = 'capture helper missing';
        if (capture) {
            const host = document.createElement('div');
            document.body.appendChild(host);
            captured = await capture(host, 400);
            host.remove();
        }
        cases.push(result('A thumbnail capture with no chart returns nothing', 'null', captured === null ? 'null' : captured));

        return cases;
    };

    // Charts get their size and their animation on animation frames. While the page is not drawn (a hidden pane, a
    // background tab, a minimized window) no frame runs, and a chart can sit at no size through no fault of its own.
    const framesRun = () => new Promise(resolve => {
        const timer = setTimeout(() => resolve(false), 1000);
        requestAnimationFrame(() => {
            clearTimeout(timer);
            resolve(true);
        });
    });

    const waitUntilDrawn = async () => {
        while (!(await framesRun()))
            await demoSleep(500);
    };

    const notRendered = 'the sample content did not render';
    const noChart = 'no chart was drawn';

    // Opens one sample page in the frame and reports its charts and its errors.
    const checkSample = async (frame, url) => {
        try {
            // Hooked per sample: a frame that reloaded has a new window with no hook.
            const win = frame.contentWindow;
            const errors = watchErrors(win);

            // Checking a sample again: leave its page first, so the page renders anew.
            if (win.location.pathname === url) {
                const chartUrl = url.slice(0, url.lastIndexOf('/'));
                navigate(frame, chartUrl);
                await waitFor(() => win.location.pathname === chartUrl);
            }

            navigate(frame, url);

            const content = () => win.document.querySelector(`.be-sample-content${fresh}`);
            const canvases = () => [...(content()?.querySelectorAll('canvas') ?? [])];

            const shown = await waitFor(() => win.location.pathname === url && content());
            const drawn = shown && await waitFor(() => canvases().length > 0 && canvases().every(c => win.Chart.getChart(c)));
            const live = () => canvases().map(c => win.Chart.getChart(c)).filter(Boolean);
            const hasSize = c => c.width > 0 && c.height > 0;

            // A chart can report no size for a moment while its container is laid out; a chart in a hidden container never gets one.
            await waitFor(() => live().every(hasSize), 3000);

            // Wait for the first animation to end, so an error raised at its end (for example by an onComplete function)
            // is still recorded for this sample. A looping animation never ends; the wait gives up after a few seconds.
            await waitFor(() => live().every(c => !win.Chart.animator.running(c)), 3500);
            await demoSleep(300);

            // A chart with no size is read again once the page is being drawn: a chart that was only waiting for an
            // animation frame gets its size then.
            const notes = [];
            if (!live().every(hasSize)) {
                await waitUntilDrawn();
                await waitFor(() => live().every(hasSize), 3000);

                // A chart created while its tab was still hidden can keep a resize that Chart.js put off until the next
                // drawing, and no drawing follows. The walk notes the state of such a chart, then completes the resize to
                // learn whether the chart can draw at all. The sample is then reported as "resized", never as passed: its
                // chart did not get a size without help. A chart whose container has no width is left alone and fails.
                live().forEach((chart, index) => {
                    if (!hasSize(chart) && chart.canvas.parentNode?.clientWidth > 0) {
                        notes.push(`chart ${index + 1}: no size, ${chart._resizeBeforeDraw ? 'a resize waiting for a drawing' : 'no resize waiting'}, `
                            + `${win.Chart.animator.running(chart) ? 'animation running' : 'no animation running'}`);
                        chart.resize();
                    }
                });

                if (notes.length > 0)
                    await waitFor(() => live().every(hasSize), 2000);
            }

            const charts = live();
            const zeroSize = charts.filter(c => !hasSize(c)).length;

            // Taken, not copied: an error raised after this point belongs to the next sample, so no error is dropped.
            const found = errors.splice(0);
            if (!shown) found.unshift(notRendered);
            else if (!drawn) found.unshift(noChart);

            const failed = !shown || !drawn || zeroSize > 0 || found.length > 0;
            return { url, charts: charts.length, zeroSize, resized: notes.length, notes, errors: found, status: failed ? 'fail' : notes.length > 0 ? 'resized' : 'pass' };
        } catch (e) {
            return { url, charts: 0, zeroSize: 0, resized: 0, notes: [], errors: [String(e?.message ?? e)], status: 'fail' };
        }
    };

    // Opens every sample in turn. `samples` is [{ url }]. A failing sample is reported and the walk continues.
    // Errors raised while a page is torn down, or between two samples, land on the next sample, so no error is dropped.
    // The walk waits while the page is not drawn, and checks a sample again when drawing stopped during its check,
    // so switching away from the page pauses the walk instead of failing samples.
    const walk = async (frame, samples, dotNetRef) => {
        await ready(frame);

        for (const sample of samples) {
            // Console errors of a check that is thrown away, because drawing stopped during it, still count.
            const earlier = [];
            let outcome;

            for (;;) {
                await waitUntilDrawn();
                outcome = await checkSample(frame, sample.url);
                if (await framesRun())
                    break;

                earlier.push(...outcome.errors.filter(error => error !== notRendered && error !== noChart));
            }

            if (earlier.length > 0) {
                outcome.errors.unshift(...earlier);
                outcome.status = 'fail';
            }

            await dotNetRef.invokeMethodAsync('OnSampleChecked', outcome);
        }

        await dotNetRef.invokeMethodAsync('OnWalkDone');
    };

    return { runFrameCases, walk };
})();

// Unlisted resize self-check page: a responsive chart must always end at its container's size
// (rule "chart size follows its container"). Charts go through the library's own create, so its
// resize handling is what is checked. The "forced" cases recreate the moment Chart.js can drop a
// resize: after an animation's last drawn frame, before the frame that ends the animation. They
// remove Chart.js's own container observer from that chart first, so the injected resize is the
// only one, as when a hidden container is shown in the frame before the last one.
window.resizeSelfCheck = (() => {
    const result = selfCheckResult;
    const library = () => window.blazorexpress.chartjs;
    // A frame that does not come within a second (tab in the background, pane hidden) is counted as a stall,
    // which voids the run instead of hanging it.
    let stalls = 0;
    const frame = () => new Promise(resolve => {
        const timer = setTimeout(() => { stalls++; resolve(); }, 1000);
        requestAnimationFrame(() => { clearTimeout(timer); resolve(); });
    });
    const frames = async count => { for (let i = 0; i < count; i++) await frame(); };
    const delay = ms => new Promise(resolve => setTimeout(resolve, ms));
    const errors = [];
    let host = null;
    let sequence = 0;

    const lineData = (values = [3, 6, 2, 8, 5]) => ({
        labels: values.map((_, index) => `P${index + 1}`),
        datasets: [{ label: 'Values', data: [...values], borderColor: '#4e79a7', backgroundColor: 'rgba(78, 121, 167, 0.2)' }]
    });

    // A container of a fixed size, optionally hidden, holding one canvas.
    const addContainer = ({ hidden = false, width = 320, height = 160 } = {}) => {
        const container = document.createElement('div');
        container.style.position = 'relative';
        container.style.width = `${width}px`;
        container.style.height = `${height}px`;
        container.style.display = hidden ? 'none' : 'block';
        container.style.marginBottom = '8px';

        const canvas = document.createElement('canvas');
        canvas.id = `resize-self-check-${++sequence}`;
        container.appendChild(canvas);
        host.appendChild(container);

        return { container, canvas, id: canvas.id };
    };

    const create = (id, { duration = 300, responsive = true, maintainAspectRatio = false, extra = {} } = {}) => {
        const options = { responsive, maintainAspectRatio, animation: duration === false ? false : { duration }, ...extra };
        library().line.create(id, 'line', lineData(), options, []);
        return Chart.getChart(id);
    };

    // Calls `action` once, from the animator's progress notification of the last drawn frame: every animation
    // item has finished ticking, but the items are still listed, so Chart.js still counts the chart as running.
    // (`currentStep` can stay just below `numSteps` on that frame, so the items are read instead; test-only use
    // of the animator's internal list.) Resolves with undefined when no such frame comes within 5 s.
    const atLastStep = (chart, action) => new Promise(resolve => {
        let done = false;
        const timer = setTimeout(() => { done = true; resolve(undefined); }, 5000);
        Chart.animator.listen(chart, 'progress', () => {
            const items = Chart.animator._charts.get(chart)?.items ?? [];
            if (done || items.length === 0 || items.some(item => item._active)) return;
            done = true;
            clearTimeout(timer);
            resolve(action());
        });
    });

    // Waits until the chart has no animation running, then two more frames.
    const settle = async (chart, timeoutMs = 3000) => {
        const deadline = performance.now() + timeoutMs;
        await frames(2);
        while (chart?.canvas && Chart.animator.running(chart) && performance.now() < deadline)
            await frame();
        await frames(2);
    };

    const removeContainerObserver = chart => chart.platform.removeEventListener(chart, 'resize');
    const sizeOf = chart => `${Math.round(chart?.width ?? -1)}`;
    const widthOf = container => `${container.clientWidth}`;

    const measureFrames = async () => {
        let count = 0;
        const stallsBefore = stalls;
        const end = performance.now() + 500;
        while (performance.now() < end) { await frame(); count++; }
        return count - (stalls - stallsBefore);
    };

    // V-02 / V-03: a hidden chart shown at the last step of its initial animation.
    const forcedShowAtLastStep = async (name, useLibraryResize) => {
        const { container, id } = addContainer({ hidden: true });
        const chart = create(id);
        removeContainerObserver(chart);
        const stored = await atLastStep(chart, () => {
            container.style.display = 'block';
            if (useLibraryResize)
                library().resize(id, 'width:320px', 'height:160px');
            else
                chart.resize();
            return !!chart._resizeBeforeDraw;
        });
        await settle(chart);
        return [
            result(`${name}: the resize landed in the gap (stored, not applied)`, true, stored),
            result(`${name}: chart width equals the container width`, widthOf(container), sizeOf(chart)),
        ];
    };

    // V-02 / V-03: a drawn chart whose container widens at the last step of an update animation.
    const forcedWidenAtLastStepOfUpdate = async () => {
        const { container, id } = addContainer();
        const chart = create(id);
        await settle(chart);
        removeContainerObserver(chart);
        library().update(id, 'line', lineData([5, 1, 7, 3, 9]), { responsive: true, maintainAspectRatio: false, animation: { duration: 300 } });
        const updated = Chart.getChart(id);
        const stored = await atLastStep(updated, () => {
            container.style.width = '420px';
            updated.resize();
            return !!updated._resizeBeforeDraw;
        });
        await settle(updated);
        return [
            result('Forced, update animation: the resize landed in the gap (stored, not applied)', true, stored),
            result('Forced, update animation: chart width equals the new container width', widthOf(container), sizeOf(updated)),
        ];
    };

    // V-03 controls: Chart.js's own observer stays in place.
    const shownDuringAnimation = async () => {
        const { container, id } = addContainer({ hidden: true });
        const chart = create(id, { duration: 600 });
        await delay(150);
        container.style.display = 'block';
        await settle(chart);
        return result('Control: shown during the initial animation', widthOf(container), sizeOf(chart));
    };

    const shownAfterAnimation = async () => {
        const { container, id } = addContainer({ hidden: true });
        const chart = create(id);
        await settle(chart);
        container.style.display = 'block';
        await settle(chart);
        return result('Control: shown after the initial animation ended', widthOf(container), sizeOf(chart));
    };

    const animationOff = async () => {
        const { container, id } = addContainer({ hidden: true });
        const chart = create(id, { duration: false });
        await frames(2);
        container.style.display = 'block';
        await settle(chart);
        return result('Animation off (AC-04 d): shown later', widthOf(container), sizeOf(chart));
    };

    // V-04 edge cases.
    const shownHiddenShown = async () => {
        const { container, id } = addContainer({ hidden: true });
        const chart = create(id, { duration: 600 });
        await delay(100);
        container.style.display = 'block';
        await frames(1);
        container.style.display = 'none';
        await delay(100);
        container.style.display = 'block';
        await settle(chart);
        return result('Shown, hidden, shown again (AC-04 a)', widthOf(container), sizeOf(chart));
    };

    const destroyedWhileStored = async () => {
        const { container, id, canvas } = addContainer({ hidden: true });
        const chart = create(id);
        removeContainerObserver(chart);
        await atLastStep(chart, () => {
            container.style.display = 'block';
            chart.resize();
            container.style.display = 'none';
            library().destroy(id);
        });
        await frames(3);
        return result('Destroyed with a resize pending (AC-04 b): no chart left on the canvas', 'none', Chart.getChart(canvas) ? 'chart' : 'none');
    };

    const changedWhileHidden = async () => {
        const { container, id } = addContainer({ hidden: true });
        const chart = create(id);
        await settle(chart);
        const next = lineData([9, 9, 9, 9, 9]);
        next.datasets.push({ label: 'Second', data: [1, 2, 3, 4, 5] });
        library().update(id, 'line', next, { responsive: true, maintainAspectRatio: false, animation: { duration: 300 } });
        library().hide(id, 1, null);
        await settle(Chart.getChart(id));
        container.style.display = 'block';
        const shown = Chart.getChart(id);
        await settle(shown);
        return [
            result('Changed while hidden (AC-04 c): chart width equals the container width', widthOf(container), sizeOf(shown)),
            result('Changed while hidden (AC-04 c): the new data is drawn', '9,9,9,9,9', shown.data.datasets[0].data.join(',')),
            result('Changed while hidden (AC-04 c): the hidden dataset stays hidden', false, shown.isDatasetVisible(1)),
        ];
    };

    const manyHidden = async () => {
        const items = Array.from({ length: 10 }, () => addContainer({ hidden: true }));
        const charts = items.map(item => create(item.id));
        await delay(120);
        items.forEach(item => { item.container.style.display = 'block'; });
        for (const chart of charts) await settle(chart);
        const sized = items.filter((item, index) => sizeOf(charts[index]) === widthOf(item.container)).length;
        return result('Ten hidden charts shown at once (AC-04 e): charts at their container width', 10, sized);
    };

    // V-05: no extra resize work for a chart that draws correctly, and responsive: false is untouched.
    const noExtraResize = async () => {
        const { id } = addContainer();
        let resizeCalls = 0;
        const chart = create(id, { extra: { onResize: () => { resizeCalls++; } } });
        await settle(chart);
        const before = resizeCalls;
        library().update(id, 'line', lineData([4, 4, 4, 4, 4]), { responsive: true, maintainAspectRatio: false, animation: { duration: 300 }, onResize: () => { resizeCalls++; } });
        await settle(Chart.getChart(id));
        return result('Unchanged size (AC-03): onResize calls after an update animation completes', 0, resizeCalls - before);
    };

    const notResponsive = async () => {
        const { container, id, canvas } = addContainer();
        canvas.width = 200;
        canvas.height = 100;
        const chart = create(id, { responsive: false });
        await settle(chart);
        container.style.width = '420px';
        await delay(100);
        await settle(chart);
        return result('Responsive = false (AC-05): chart keeps its own width', 200, sizeOf(chart));
    };

    // Review F-01: Chart.js follows a `responsive` value changed by an update, so the library must too.
    const updatedToNotResponsive = async () => {
        const { container, id } = addContainer();
        const chart = create(id);
        await settle(chart);
        library().update(id, 'line', lineData([2, 4, 6, 8, 10]), { responsive: false, maintainAspectRatio: false, animation: { duration: 300 } });
        await settle(Chart.getChart(id));
        const before = sizeOf(Chart.getChart(id));
        container.style.width = '420px';
        await delay(100);
        library().update(id, 'line', lineData([10, 8, 6, 4, 2]), { responsive: false, maintainAspectRatio: false, animation: { duration: 300 } });
        await settle(Chart.getChart(id));
        return result('Updated to responsive: false (AC-05): keeps its width after the container widens and an animation ends', before, sizeOf(Chart.getChart(id)));
    };

    const updatedToResponsive = async () => {
        const { container, id, canvas } = addContainer();
        canvas.width = 320;
        canvas.height = 160;
        const chart = create(id, { responsive: false });
        await settle(chart);
        library().update(id, 'line', lineData([1, 3, 5, 7, 9]), { responsive: true, maintainAspectRatio: false, animation: { duration: 300 } });
        await settle(Chart.getChart(id));
        const responsiveChart = Chart.getChart(id);
        removeContainerObserver(responsiveChart);
        library().update(id, 'line', lineData([9, 7, 5, 3, 1]), { responsive: true, maintainAspectRatio: false, animation: { duration: 300 } });
        const stored = await atLastStep(responsiveChart, () => {
            container.style.width = '420px';
            responsiveChart.resize();
            return !!responsiveChart._resizeBeforeDraw;
        });
        await settle(responsiveChart);
        return [
            result('Updated to responsive: true, forced: the resize landed in the gap (stored, not applied)', true, stored),
            result('Updated to responsive: true, forced (AC-01, AC-02): chart width equals the new container width', widthOf(container), sizeOf(responsiveChart)),
        ];
    };

    const run = async (hostId) => {
        host = document.getElementById(hostId);
        errors.length = 0;
        stalls = 0;
        const original = console.error.bind(console);
        console.error = (...args) => {
            errors.push(args.map(a => (a instanceof Error ? a.message : String(a))).join(' '));
            original(...args);
        };
        // An error thrown in an animation frame (for example after destroy) is not a console.error call.
        const onError = event => errors.push(`uncaught: ${event.message}`);
        window.addEventListener('error', onError);

        const cases = [];
        try {
            const drawn = await measureFrames();
            cases.push(result('Frames are drawn (tab in front, pane visible)', 'yes', drawn >= 10 ? 'yes' : `no (${drawn} in 0.5 s)`));
            if (drawn < 10)
                return cases;
            stalls = 0;

            cases.push(...await forcedShowAtLastStep('Forced, chart.resize()', false));
            cases.push(...await forcedShowAtLastStep('Forced, ResizeAsync path (core.resize)', true));
            cases.push(...await forcedWidenAtLastStepOfUpdate());
            cases.push(await shownDuringAnimation());
            cases.push(await shownAfterAnimation());
            cases.push(await animationOff());
            cases.push(await shownHiddenShown());
            cases.push(await destroyedWhileStored());
            cases.push(...await changedWhileHidden());
            cases.push(await manyHidden());
            cases.push(await noExtraResize());
            cases.push(await notResponsive());
            cases.push(await updatedToNotResponsive());
            cases.push(...await updatedToResponsive());
            cases.push(result('Console and uncaught errors during the run', 0, errors.length));
            cases.push(result('No frame stalled during the run (otherwise the run is void)', 0, stalls));
        }
        finally {
            console.error = original;
            window.removeEventListener('error', onError);
        }

        return cases;
    };

    return { run };
})();
