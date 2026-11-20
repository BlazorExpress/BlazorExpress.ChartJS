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

// True when the visitor's system asks for reduced motion. The home page hero chart then draws without animation (story 26111801 AC-14).
function demoPrefersReducedMotion() {
    return window.matchMedia('(prefers-reduced-motion: reduce)').matches;
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

// One Prism theme link at a time. setTheme runs on every load and theme change, so links from earlier calls are removed:
// a second link with the same id is invalid markup, and a light link left in place leaks light colours into dark (story 26111901, FN-07).
function updateDemoCodeThemeCss(theme) {
    const links = {
        dark: { id: "prismThemeDarkLink", href: "/_content/BlazorExpress.ChartJS.Demo.RCL/css/prism-vsc-dark-plus.min.css" },
        light: { id: "prismThemeLightLink", href: "/_content/BlazorExpress.ChartJS.Demo.RCL/css/prism-vs.min.css" },
    };
    const wanted = links[theme];
    if (!wanted)
        return;

    const existing = [...document.querySelectorAll(`#${links.dark.id}, #${links.light.id}`)];
    const kept = existing.find(link => link.id === wanted.id);
    existing.filter(link => link !== kept).forEach(link => link.remove());
    if (kept)
        return;

    const link = document.createElement("link");
    link.setAttribute("rel", "stylesheet");
    link.setAttribute("href", wanted.href);
    link.setAttribute("id", wanted.id);
    document.head.append(link);
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
        // The rim meets the chart background, so the contrast plugin judges the slice by this colour.
        gradient.demoEdgeColour = `rgb(${rgb})`;
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
        // A line's own stroke is resolved without a data index: it takes the first colour (story 26111901 AC-05).
        register('selfCheck.sliceColor', context => ['#4e79a7', '#f28e2b', '#e15759', '#76b7b2', '#59a14f'][(context.dataIndex ?? 0) % 5]);
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

    // The page background animates to the new theme, and demoMarkContrast measures marks against it: check them again once it
    // has settled.
    setTimeout(() => Chart.helpers.each(Chart.instances, chart => { try { chart.update('none'); } catch { /* see above */ } }), 600);
}

// Chart mark contrast for the demo site (story 26111901 AC-05; rules.md A11Y-004 and A11Y-005). WCAG 1.4.11 needs bars, slices
// and points (or their border) at 3:1 against the chart's background, and a line at 3:1 itself. Where a sample's own colours fall
// short in the current theme, a bar, slice or point gets a border in the nearest passing shade of its own fill's hue, and a line
// gets the nearest passing shade of its own colour. Fills, the samples' C# and the library's palettes are unchanged; the plugin
// only runs on this site. It works before the datasets are resolved, so Chart.js animates the borders like any other option.
const demoMarkContrast = (() => {
    const original = new WeakMap(); // dataset -> { borderColor, borderWidth, applied }
    // Gradients, patterns and scriptable values are left alone (null), except a demo gradient that names its edge colour.
    const toRgb = value => demoSelfCheckColour.canvasColour(value);
    const toHsl = ({ r, g, b }) => {
        r /= 255; g /= 255; b /= 255;
        const max = Math.max(r, g, b), min = Math.min(r, g, b), l = (max + min) / 2;
        if (max === min) return { h: 0, s: 0, l };
        const d = max - min, s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
        const h = max === r ? (g - b) / d + (g < b ? 6 : 0) : max === g ? (b - r) / d + 2 : (r - g) / d + 4;
        return { h: h / 6, s, l };
    };
    const fromHsl = ({ h, s, l }) => {
        if (s === 0) { const v = Math.round(l * 255); return { r: v, g: v, b: v, a: 1 }; }
        const q = l < 0.5 ? l * (1 + s) : l + s - l * s, p = 2 * l - q;
        const f = t => { t = (t + 1) % 1; return t < 1 / 6 ? p + (q - p) * 6 * t : t < 1 / 2 ? q : t < 2 / 3 ? p + (q - p) * (2 / 3 - t) * 6 : p; };
        return { r: Math.round(f(h + 1 / 3) * 255), g: Math.round(f(h) * 255), b: Math.round(f(h - 1 / 3) * 255), a: 1 };
    };
    const css = c => `rgb(${c.r}, ${c.g}, ${c.b})`;

    // The nearest shade of the colour's own hue that reaches `min` (3:1 for marks, 4.5:1 for text): darker on a light
    // background, lighter on a dark one. Text walks whole-percent lightness steps (A11Y-005), which gives the shades the
    // developer approved (I-19); marks keep the walk from the exact lightness that their verified borders came from.
    const passingShade = (colour, bg, min = 3) => {
        const { over, ratio, luminance } = demoSelfCheckColour;
        const hsl = toHsl(over(colour, bg));
        const darker = luminance(bg) > 0.18;
        if (min > 3) {
            for (let percent = Math.round(hsl.l * 100) + (darker ? -1 : 1); percent >= 0 && percent <= 100; percent += darker ? -1 : 1) {
                const shade = fromHsl({ ...hsl, l: percent / 100 });
                if (ratio(shade, bg) >= min) return css(shade);
            }
            return darker ? 'rgb(0, 0, 0)' : 'rgb(255, 255, 255)';
        }
        const step = darker ? -0.01 : 0.01;
        for (let l = hsl.l; l >= 0 && l <= 1; l += step) {
            const shade = fromHsl({ ...hsl, l });
            if (ratio(shade, bg) >= min) return css(shade);
        }
        return darker ? 'rgb(0, 0, 0)' : 'rgb(255, 255, 255)';
    };

    const passes = (colour, bg, min = 3) => !!colour && demoSelfCheckColour.ratio(demoSelfCheckColour.over(colour, bg), bg) >= min;

    // Axis text a sample colours itself (tick labels and axis titles) must reach 4.5:1 (AC-05). A colour that falls short in
    // the current theme is drawn in its nearest passing shade; the sample's own colour is remembered, so it comes back in a
    // theme where it passes. The developer approved the shades this gives for the current samples (I-19).
    // Kept per chart and axis id: Chart.js rebuilds the scale option objects on every update.
    const textOriginal = new WeakMap(); // chart -> Map(axis id -> { ticks: { own, applied }, title: { own, applied } })
    const axisText = (chart, bg) => {
        let axes = textOriginal.get(chart);
        if (!axes) textOriginal.set(chart, axes = new Map());
        for (const [id, scale] of Object.entries(chart.config.options?.scales ?? {})) {
            if (!scale) continue;
            let saved = axes.get(id);
            if (!saved) axes.set(id, saved = {});
            for (const part of ['ticks', 'title']) {
                const options = scale[part];
                if (!options) continue;
                // A colour this plugin did not set was chosen by the sample, the app or the theme code: start from it.
                if (!saved[part] || options.color !== saved[part].applied) saved[part] = { own: options.color, applied: options.color };
                const own = saved[part].own;
                const rgb = typeof own === 'string' ? toRgb(own) : null;
                const drawn = rgb && !passes(rgb, bg, 4.5) ? passingShade(rgb, bg, 4.5) : own;
                saved[part].applied = drawn;
                // Written through the chart's resolved options as well, which Chart.js reads when it lays out this update.
                if (options.color !== drawn) {
                    options.color = drawn;
                    const resolved = chart.options.scales?.[id]?.[part];
                    if (resolved) resolved.color = drawn;
                }
            }
        }
    };

    // A dataset option as Chart.js resolves it for one element: a value, an array indexed by the data index, or a scriptable
    // function. A dataset-level call (the legend) has no data index and takes the first element's value.
    const resolveAt = (value, context) => {
        if (typeof value === 'function') return value(context, context.chart.options);
        if (Array.isArray(value)) return value[(context.dataIndex ?? 0) % value.length];
        return value;
    };

    return {
        id: 'demoMarkContrast',
        beforeUpdate(chart) {
            const win = chart.canvas.ownerDocument.defaultView;
            axisText(chart, demoSelfCheckColour.background(win, chart.canvas));
        },
        beforeDatasetsUpdate(chart) {
            const win = chart.canvas.ownerDocument.defaultView;
            const bg = demoSelfCheckColour.background(win, chart.canvas);
            chart.data.datasets.forEach((dataset, index) => {
                let saved = original.get(dataset);
                // A dataset whose border is not the one this plugin set was replaced or changed by the app: start from it.
                if (!saved || dataset.borderColor !== saved.applied.borderColor || dataset.borderWidth !== saved.applied.borderWidth) {
                    saved = { borderColor: dataset.borderColor, borderWidth: dataset.borderWidth, applied: {} };
                    original.set(dataset, saved);
                }
                saved.bg = bg;
                const type = chart.getDatasetMeta(index).type ?? chart.config.type;
                let borderColor = saved.borderColor, borderWidth = saved.borderWidth;
                if (type === 'line' || type === 'radar') {
                    // The line is the mark: a stroke under 3:1 moves to its nearest passing shade. An array (one colour per
                    // point) is judged per entry and stays an array.
                    const fix = value => { const stroke = toRgb(value); return stroke && !passes(stroke, bg) ? passingShade(stroke, bg) : value; };
                    if (Array.isArray(borderColor)) {
                        if (borderColor.some(value => fix(value) !== value)) borderColor = borderColor.map(fix);
                    } else {
                        const stroke = toRgb(borderColor ?? win.Chart.defaults.borderColor);
                        if (stroke && !passes(stroke, bg)) borderColor = passingShade(stroke, bg);
                    }
                } else {
                    // Bars, slices and points: scriptable border options, so each element is judged on its own resolved fill,
                    // whether the sample gave a colour, an array or a function. An element whose fill or own border already
                    // reaches 3:1 keeps its border. A scriptable's result is final in Chart.js, so where the sample set no border
                    // the element's own default is returned (the white slice separators, the 1 px point outline).
                    const element = type === 'bar' ? 'bar' : type === 'pie' || type === 'doughnut' || type === 'polarArea' ? 'arc' : 'point';
                    const defaults = chart.options.elements?.[element] ?? {};
                    const judge = context => {
                        // No colour of its own (and no Colors plugin colour): Chart.js draws the default fill.
                        const fill = toRgb(resolveAt(context.dataset.backgroundColor, context) ?? win.Chart.defaults.backgroundColor);
                        const ownColour = resolveAt(saved.borderColor, context) ?? resolveAt(defaults.borderColor, context);
                        const ownWidth = resolveAt(saved.borderWidth, context) ?? resolveAt(defaults.borderWidth, context) ?? 0;
                        if (!fill || passes(fill, saved.bg) || (ownWidth >= 1 && passes(toRgb(ownColour), saved.bg))) return { colour: ownColour, width: ownWidth };
                        return { colour: passingShade(fill, saved.bg), width: Math.max(ownWidth, 1) };
                    };
                    borderColor = context => judge(context).colour;
                    borderWidth = context => judge(context).width;
                }
                dataset.borderColor = borderColor;
                dataset.borderWidth = borderWidth;
                saved.applied = { borderColor, borderWidth };
            });
        },
    };
})();

if (window.Chart) Chart.register(demoMarkContrast);

// Charts draw without animation when the visitor's system asks for reduced motion (story 26111901 AC-06). Chart.js does
// not read the setting, so every demo, docs and sample chart would animate. Chart.js has already resolved its options when
// `beforeUpdate` runs, but each dataset reads `chart.options.animation` again before it animates, so setting it to false
// here stops this update's animations and later ones. Turning the setting off again takes effect on the next page load.
const demoReducedMotion = {
    id: 'demoReducedMotion',
    beforeUpdate(chart) {
        if (demoPrefersReducedMotion() && chart.options.animation !== false)
            chart.options.animation = false;
    },
};

if (window.Chart) Chart.register(demoReducedMotion);

// Keyboard reach for the demo tabs and the copy icon (story 26110206 A-03). The UI package of the demo site renders the
// "Example / View Source" tabs as links without an href and the copy control as an icon, and the Tab key skips both.
// This gives them a tab stop, a role and Enter or Space activation, and tells assistive technology which tab is shown.
// The copy icon sits in the snippet of a demo and in the toolbar of a code block; any other package copy icon is covered too
// (story 26111901 FN-06). The package components are unchanged.
(() => {
    // The navbar burger, theme switch and theme items are links without an href too (story 26111801 AC-20).
    const selector = '.tabs li > a:not([href]), :is(.be-bulma-doc-example-snippet, .be-bulma-doc-example-toolbar) i[role="button"], i.bi-clipboard[role="button"], '
        + '.be-demo-navbar .navbar-burger:not([href]), .be-demo-navbar .navbar-link:not([href]), .be-demo-navbar .navbar-dropdown .navbar-item:not([href])';

    const enhance = () => {
        for (const element of document.querySelectorAll(selector)) {
            if (!element.hasAttribute('tabindex'))
                element.setAttribute('tabindex', '0');
            if (!element.hasAttribute('role'))
                element.setAttribute('role', 'button');
            if (element.tagName === 'I' && !element.hasAttribute('aria-label'))
                element.setAttribute('aria-label', 'Copy the source to the clipboard');

            if (element.matches('.tabs li > a')) {
                const pressed = String(element.parentElement.classList.contains('is-active'));
                if (element.getAttribute('aria-pressed') !== pressed)
                    element.setAttribute('aria-pressed', pressed);
            }

            // The package opens the navbar menu with a class and leaves the burger's `aria-expanded` at "false", so the
            // state is kept in step here (story 26111901 AC-06, WCAG 4.1.2).
            if (element.matches('.navbar-burger')) {
                const menu = document.getElementById(element.dataset.target);
                const expanded = String(!!(menu?.classList.contains('is-active') || element.classList.contains('is-active')));
                if (element.getAttribute('aria-expanded') !== expanded)
                    element.setAttribute('aria-expanded', expanded);
            }
        }
    };

    let scheduled = false;
    const schedule = () => {
        if (scheduled)
            return;

        // A timer, not an animation frame: a page in a hidden or background pane draws no frames, and its controls must
        // still get their name and tab stop (FN-06).
        scheduled = true;
        setTimeout(() => {
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
        // Every chart on a page is a named image (rules.md A11Y-002), including the ones this check creates.
        canvas.setAttribute('role', 'img');
        canvas.setAttribute('aria-label', `Line chart: resize check ${sequence}`);
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

// Skip link (story 26111901, A11Y-001): moves keyboard focus to the page's main landmark. Handled before Blazor's link
// interception, so following the link changes neither the route nor the history.
document.addEventListener('click', event => {
    const link = event.target.closest?.('a.be-skip-link');
    if (!link) return;
    const main = document.getElementById('main-content');
    if (!main) return;
    event.preventDefault();
    event.stopPropagation();
    main.focus({ preventScroll: true });
    main.scrollIntoView({ block: 'start' });
}, true);

// Colour maths and focus-ring reading shared by the self-check pages (story 26111901; rules.md A11Y-006).
const demoSelfCheckColour = (() => {
    // Colour maths for WCAG contrast. Backgrounds are composed from the element up to the first opaque ancestor.
    const parse = value => {
        const match = /rgba?\(([^)]+)\)/.exec(value ?? '');
        if (!match) return null;
        const parts = match[1].split(/[\s,/]+/).filter(Boolean).map(Number);
        return { r: parts[0], g: parts[1], b: parts[2], a: parts.length > 3 ? parts[3] : 1 };
    };
    const hex = value => ({ r: parseInt(value.slice(1, 3), 16), g: parseInt(value.slice(3, 5), 16), b: parseInt(value.slice(5, 7), 16), a: 1 });
    // Any CSS colour string (a name, hex, rgb or hsl) as a canvas normalises it; null for gradients, patterns and functions.
    // A demo gradient that names the colour at its edge (`demoEdgeColour`, where it meets the chart background) is read as
    // that colour (story 26111901 AC-05).
    const probe = document.createElement('canvas').getContext('2d');
    const canvasColour = value => {
        if (typeof value?.demoEdgeColour === 'string') value = value.demoEdgeColour;
        if (typeof value !== 'string') return null;
        probe.fillStyle = '#010203';
        probe.fillStyle = value;
        const style = probe.fillStyle;
        return style.startsWith('#') ? hex(style) : parse(style);
    };
    const over = (top, bottom) => ({
        r: top.r * top.a + bottom.r * (1 - top.a), g: top.g * top.a + bottom.g * (1 - top.a), b: top.b * top.a + bottom.b * (1 - top.a), a: 1,
    });
    const luminance = c => {
        const channel = v => { v /= 255; return v <= 0.03928 ? v / 12.92 : ((v + 0.055) / 1.055) ** 2.4; };
        return 0.2126 * channel(c.r) + 0.7152 * channel(c.g) + 0.0722 * channel(c.b);
    };
    const ratio = (a, b) => {
        const [high, low] = [luminance(a), luminance(b)].sort((x, y) => y - x);
        return (high + 0.05) / (low + 0.05);
    };
    const background = (win, element) => {
        const layers = [];
        for (let node = element; node; node = node.parentElement) {
            const colour = parse(win.getComputedStyle(node).backgroundColor);
            if (colour && colour.a > 0) {
                layers.push(colour);
                if (colour.a >= 1) break;
            }
        }
        let colour = layers.length && layers[layers.length - 1].a >= 1 ? layers.pop() : { r: 255, g: 255, b: 255, a: 1 };
        for (let i = layers.length - 1; i >= 0; i--) colour = over(layers[i], colour);
        return colour;
    };
    const textColour = (win, element) => {
        const colour = parse(win.getComputedStyle(element).color);
        let opacity = 1;
        for (let node = element; node; node = node.parentElement) opacity *= Number(win.getComputedStyle(node).opacity);
        return { ...colour, a: colour.a * opacity };
    };

    // CSS specificity (ids, classes and the like, elements) of one selector, enough to rank the focus rules that match a control.
    // :where() counts nothing; :is() and :not() count their argument.
    const specificity = selector => {
        const s = selector.replace(/:where\([^)]*\)/g, '').replace(/:(?:is|not)\(([^)]*)\)/g, ' $1 ');
        const ids = (s.match(/#[\w-]+/g) ?? []).length;
        const classes = (s.match(/\.[\w-]+|\[[^\]]+\]|:(?!:)[\w-]+/g) ?? []).length;
        const elements = (s.replace(/\[[^\]]+\]/g, '').match(/(?:^|[\s>+~])[a-z][\w-]*/gi) ?? []).length + (s.match(/::[\w-]+/g) ?? []).length;
        return ids * 10000 + classes * 100 + elements;
    };

    // The page's :focus and :focus-visible rules that match a control, weakest first (specificity, then document order).
    // Rules in stylesheets from other origins can't be read and are left out.
    function focusRules(doc, win, control) {
        const found = [];
        let order = 0;
        const visit = list => {
            for (const r of list) {
                if (r.cssRules && !r.selectorText) { if (!r.media || win.matchMedia(r.media.mediaText).matches) visit(r.cssRules); continue; }
                order++;
                if (!r.selectorText || !/:focus(?!-within)/.test(r.selectorText)) continue;
                let weight = -1;
                for (const selector of r.selectorText.split(',')) {
                    if (!/:focus(?!-within)/.test(selector)) continue;
                    const base = selector.replace(/:focus-visible|:focus(?!-within)/g, '').trim() || '*';
                    let matches = false;
                    try { matches = control.matches(base); } catch { matches = false; }
                    if (matches) weight = Math.max(weight, specificity(base));
                }
                if (weight >= 0) found.push({ rule: r, weight, order });
            }
        };
        for (const sheet of doc.styleSheets) {
            let rules;
            try { rules = sheet.cssRules; } catch { continue; }
            visit(rules);
        }
        return found.sort((a, b) => a.weight - b.weight || a.order - b.order).map(f => f.rule);
    }

    // Runs read() while the control wears its focus styles, applied inline from its focus rules, then restores it.
    // :focus and :focus-visible match only while the window has system focus, which an unfocused or hidden pane lacks.
    // Each rule's declaration text is appended, weakest first, so a stronger rule wins. Copying longhands one by one would
    // lose a shorthand that uses var() (such as outline: 3px solid var(--bulma-link)): its longhands read as empty strings.
    function withFocusStyles(doc, win, control, read) {
        const rules = focusRules(doc, win, control);
        const saved = control.style.cssText;
        control.style.cssText = [saved, ...rules.map(rule => rule.style.cssText)].filter(Boolean).join('; ');
        try { return read(rules); }
        finally { control.style.cssText = saved; }
    }

    // The focus ring a control shows, or null when no readable rule gives it one (the browser's default ring then applies).
    function focusRing(doc, win, control) {
        return withFocusStyles(doc, win, control, rules => {
            if (!rules.some(r => r.style.outline || r.style.outlineStyle || r.style.outlineColor || r.style.boxShadow)) return null;
            const style = win.getComputedStyle(control);
            return { style: style.outlineStyle, width: parseFloat(style.outlineWidth), colour: parse(style.outlineColor), shadow: style.boxShadow };
        });
    }

    return { parse, hex, canvasColour, over, luminance, ratio, background, textColour, specificity, focusRing, withFocusStyles };
})();

// Unlisted home self-check page (story 26111801; rules.md "W3C and WCAG 2.x conformance"). Switches the hero chart through
// every type, checks reduced motion and theme colours, then loads the home page in frames 320, 375, 768 and 1280 pixels wide
// to check layout, content, links, contrast, target size and markup structure.
window.homeSelfCheck = (() => {
    const result = selfCheckResult;
    const chartJsType = {
        'bar-chart': 'bar', 'bubble-chart': 'bubble', 'doughnut-chart': 'doughnut', 'line-chart': 'line',
        'pie-chart': 'pie', 'polar-area-chart': 'polarArea', 'radar-chart': 'radar', 'scatter-chart': 'scatter',
    };
    let stalls = 0;
    const frame = () => new Promise(resolve => {
        const timer = setTimeout(() => { stalls++; resolve(); }, 1000);
        requestAnimationFrame(() => { clearTimeout(timer); resolve(); });
    });
    const frames = async count => { for (let i = 0; i < count; i++) await frame(); };
    const until = async (test, timeout = 5000) => {
        const end = performance.now() + timeout;
        while (performance.now() < end) {
            const value = test();
            if (value) return value;
            await demoSleep(50);
        }
        return test();
    };
    const heroChart = (host, win = window) => {
        const canvas = host?.querySelector('.be-home-hero-chart canvas');
        return canvas && win.Chart ? win.Chart.getChart(canvas) : undefined;
    };
    const componentName = key => key.split('-').map(part => part[0].toUpperCase() + part.slice(1)).join('');
    const path = link => link ? new URL(link.href).pathname + new URL(link.href).hash : '';

    const { parse, hex, over, luminance, ratio, background, textColour, focusRing } = demoSelfCheckColour;

    async function chartTypes(host, expected) {
        const cases = [];
        const buttons = () => [...host.querySelectorAll('.be-home-chart-type')];
        const pressed = () => buttons().filter(b => b.getAttribute('aria-pressed') === 'true');
        const first = await until(() => heroChart(host));
        cases.push(result('S-01 Bar is drawn and selected on load', 'bar, Bar', `${first?.config.type}, ${pressed().map(b => b.textContent.trim()).join(',')}`));
        cases.push(result('S-01 Buttons in catalogue order', expected.charts.map(c => c.label).join(', '), buttons().map(b => b.textContent.trim()).join(', ')));

        for (const chart of expected.charts) {
            buttons().find(b => b.dataset.chartKey === chart.key).click();
            const drawn = await until(() => { const c = heroChart(host); return c?.config.type === chartJsType[chart.key] ? c : null; });
            await frame();
            const name = componentName(chart.key);
            const link = host.querySelector('.be-home-hero-samples-link');
            cases.push(result(`S-01 ${chart.label}: chart type, pressed button, excerpt, link text, link route`,
                [chartJsType[chart.key], chart.key, `<${name} @ref="${name[0].toLowerCase()}${name.slice(1)}" />`, `Open ${chart.count} ${chart.label.toLowerCase()} chart samples`, chart.route].join(' | '),
                [drawn?.config.type, pressed().map(b => b.dataset.chartKey).join(','), host.querySelector('.be-home-hero-code')?.textContent, link?.textContent.trim(), path(link)].join(' | ')));
        }

        return cases;
    }

    async function quickSwitching(host, expected) {
        host.querySelectorAll('.be-home-chart-type').forEach(button => button.click());
        const last = expected.charts[expected.charts.length - 1].key;
        await until(() => heroChart(host)?.config.type === chartJsType[last]);
        await frames(5);
        const canvases = host.querySelectorAll('.be-home-hero-chart-area canvas').length;
        return [result('S-02 Quick clicks on all 8 buttons end on the last type, with one chart', `${chartJsType[last]}, 1`, `${heroChart(host)?.config.type}, ${canvases}`)];
    }

    async function reducedMotion() {
        const reduced = await until(() => heroChart(document.querySelector('[data-self-check=hero-reduced]')));
        const normal = heroChart(document.querySelector('[data-self-check=hero-motion]'));
        return [result('S-03 Animation duration: reduced motion is 0, normal is above 0', '0, true', `${reduced?.options.animation?.duration}, ${(normal?.options.animation?.duration ?? 0) > 0}`)];
    }

    async function themeColours(host) {
        const before = document.documentElement.dataset.theme ?? 'light';
        host.querySelector('.be-home-chart-type[data-chart-key="bar-chart"]').click();
        await until(() => heroChart(host)?.config.type === 'bar');
        await frames(3);
        const read = () => { const c = heroChart(host); return `${c?.scales.x.options.ticks.color}, ${c?.scales.y.options.grid.color}`; };
        setTheme('dark', false);
        await frames(3);
        const dark = read();
        setTheme('light', false);
        await frames(3);
        const light = read();
        setTheme(before, false);
        await frames(2);
        return [
            result('S-04 After a switch to dark, the hero axis text and grid use the dark theme colours', `${demoChartThemes.dark.color}, ${demoChartThemes.dark.lineColor}`, dark),
            result('S-04 After a switch back to light, they use the light theme colours', `${demoChartThemes.light.color}, ${demoChartThemes.light.lineColor}`, light),
        ];
    }

    // Loads an address of the site in a frame. The home page is ready when its hero chart is drawn; another page when its navbar and h1 are.
    async function loadFrame(hostId, width, path = '/') {
        const iframe = document.createElement('iframe');
        iframe.title = `${path} at ${width} pixels`;
        // An outline, not a border, so the frame's viewport is exactly the width under test.
        iframe.style.cssText = `display:block;width:${width}px;height:900px;margin-bottom:1rem;border:0;outline:1px solid #ccc;`;
        const loaded = new Promise(resolve => iframe.addEventListener('load', resolve, { once: true }));
        iframe.src = path;
        document.getElementById(hostId).appendChild(iframe);
        await loaded;
        const win = iframe.contentWindow;
        const ready = await until(() => {
            const doc = iframe.contentDocument;
            if (path === '/') return heroChart(doc, win) ? true : null;
            return doc?.querySelector('.be-demo-navbar a[href="/docs"]') && doc.querySelector('h1') ? true : null;
        }, 20000);
        await frames(5);
        return { doc: iframe.contentDocument, win, ready: !!ready };
    }

    function layout({ doc, win }, width) {
        const columns = selector => {
            const element = doc.querySelector(selector);
            return element ? win.getComputedStyle(element).gridTemplateColumns.split(' ').filter(Boolean).length : 0;
        };
        const id = width === 640 ? 'S-19' : width < 768 ? 'S-05' : 'S-06';
        const phone = width < 768;
        const expected = phone ? '1, 2, 2' : '2, 3, 4';
        const cases = [
            result(`${id} ${width}px: no horizontal scroll`, true, doc.documentElement.scrollWidth <= doc.documentElement.clientWidth),
            result(`${id} ${width}px: hero, feature and chart-card columns`, expected, `${columns('.be-home-hero-grid')}, ${columns('.be-home-features')}, ${columns('.be-home-charts')}`),
        ];

        if (phone) {
            const lines = new Set([...doc.querySelectorAll('.be-home-chart-type')].map(b => Math.round(b.getBoundingClientRect().top))).size;
            const buttons = [...doc.querySelectorAll('.be-home-actions .button')];
            const fullWidth = buttons.every(b => Math.abs(b.getBoundingClientRect().width - b.parentElement.getBoundingClientRect().width) < 2);
            // The chart-type buttons need to wrap only at phone widths (AC-12); at 640px they may fit one line.
            if (width <= 375)
                cases.push(result(`${id} ${width}px: chart-type buttons wrap, and the two action buttons are full width`, 'true, true', `${lines > 1}, ${fullWidth}`));
            else
                cases.push(result(`${id} ${width}px: the two action buttons are full width`, true, fullWidth));

            // WCAG 2.2 target size (2.5.8): every visible control on the home page and in the navbar is at least 24 by 24 CSS px.
            const small = [...doc.querySelectorAll('.be-home a, .be-home button, .be-demo-navbar a, .be-demo-navbar button')]
                .filter(control => control.offsetParent !== null && control.closest('.message') === null)
                .filter(control => { const box = control.getBoundingClientRect(); return box.width < 24 || box.height < 24; })
                .map(control => `${control.textContent.trim().slice(0, 24) || control.getAttribute('aria-label')} ${Math.round(control.getBoundingClientRect().width)}x${Math.round(control.getBoundingClientRect().height)}`);
            cases.push(result(`${width === 640 ? 'S-19' : 'S-13'} ${width}px: every home page and navbar control is at least 24 by 24 CSS px`, 'none smaller', small.length ? small.join('; ') : 'none smaller'));
        }

        return cases;
    }

    function content({ doc }, expected) {
        const q = selector => doc.querySelector(selector);
        const qa = selector => [...doc.querySelectorAll(selector)];
        const home = q('.be-home');
        const badges = qa('.be-home-badges a');
        const actions = qa('.be-home-actions .button');
        const description = doc.head.querySelector('meta[name=description]')?.content ?? '';

        return [
            result('S-07 Release pill: version, text and release notes link', `New in ${expected.version} | ${expected.releaseShortDescription} | ${expected.releaseNotesUrl}`,
                `${q('.be-home-pill-tag')?.textContent.trim()} | ${q('.be-home-pill-text')?.textContent.trim()} | ${path(q('.be-home-pill'))}`),
            result('S-07 Title', 'Chart.js charts for Blazor, configured in C#', q('.be-home-title')?.textContent.trim()),
            result('S-07 The page has no "UI library" text', false, home.textContent.includes('UI library')),
            result('S-07 NuGet badges: alt text and NuGet links', 'NuGet version, NuGet downloads | 2', `${badges.map(a => a.querySelector('img')?.alt).join(', ')} | ${badges.filter(a => a.href.includes('nuget.org/packages/BlazorExpress.ChartJS')).length}`),
            result('S-07 Install command', expected.installCommand, q('.be-home-install-command')?.textContent.trim()),
            result('S-07 Action buttons: text and routes', `Get started ${expected.gettingStartedUrl} | Browse ${expected.totalSamples} samples ${expected.samplesUrl}`, actions.map(a => `${a.textContent.trim()} ${path(a)}`).join(' | ')),
            result('S-08 Six feature cards', 6, qa('.be-home-feature').length),
            result('S-08 Chart cards: sample counts and routes', expected.charts.map(c => `${c.count} samples ${c.route}`).join(' | '),
                qa('.be-home-chart').map(a => `${a.querySelector('.be-home-chart-count')?.textContent.trim()} ${path(a)}`).join(' | ')),
            result('S-08 One tag per sample category, and the Color Utils link', `${expected.categoryCount}, ${expected.colorUtilsUrl}`, `${qa('.be-home-categories a.tag').length}, ${path(q('.be-home .be-home-link'))}`),
            result('S-08 The compatibility row links to the full version table', `${expected.gettingStartedUrl}#supported-tech-stack`, path(q('.be-home-compat a'))),
            result('S-08 No full version table and no logo image (only the 2 badge images)', '0, 2', `${home.querySelectorAll('table').length}, ${home.querySelectorAll('img').length}`),
            result('S-08 Did you know and the GitHub star band are shown', true, !!q('.be-home .message.is-success') && !!q('.be-home-star .notification')),
            result('S-11 Badge links open a new tab with noopener', 2, badges.filter(a => a.target === '_blank' && a.rel.includes('noopener')).length),
            result('S-12 Page title, and a chart-specific meta description', 'Blazor Chart.js Components for .NET | BlazorExpress.ChartJS | true',
                `${doc.title} | ${description.includes('Chart.js') && !description.includes('UI library')}`),
        ];
    }

    // W3C markup and WCAG structure: page language, unique ids, one h1, no skipped heading level,
    // an accessible name on every link and button, alt text on every image and a text alternative for the chart.
    function structure({ doc }) {
        const ids = [...doc.querySelectorAll('[id]')].map(e => e.id);
        const duplicates = [...new Set(ids.filter((id, i) => ids.indexOf(id) !== i))];
        const levels = [...doc.querySelectorAll('.be-home h1, .be-home h2, .be-home h3, .be-home h4')].map(h => Number(h.tagName[1]));
        const skipped = levels.some((level, i) => i > 0 && level > levels[i - 1] + 1);
        const unnamed = [...doc.querySelectorAll('.be-home a, .be-home button')].filter(c => !(c.textContent.trim() || c.getAttribute('aria-label') || c.querySelector('img[alt]:not([alt=""])'))).length;
        const noAlt = [...doc.querySelectorAll('.be-home img')].filter(img => !img.hasAttribute('alt')).length;
        const chartLabel = doc.querySelector('.be-home-hero-chart')?.getAttribute('aria-label') ?? '';
        return [result('S-14 lang set; unique ids; 1 h1; no skipped heading; named controls; image alts; chart label',
            'true | none | 1 | false | 0 | 0 | true',
            `${!!doc.documentElement.lang} | ${duplicates.length ? duplicates.join(',') : 'none'} | ${doc.querySelectorAll('h1').length} | ${skipped} | ${unnamed} | ${noAlt} | ${chartLabel.startsWith('Sample ')}`)];
    }

    function contrast({ doc, win }, theme) {
        const low = [];
        const selectors = ['.be-home-pill-text', '.be-home-lead', '.be-home-install-command', '.be-home-chart-type:not(.is-selected)', '.be-home-chart-type.is-selected',
            '.be-home-hero-code', '.be-home-hero-samples-link', '.be-home-feature-title', '.be-home-feature-text', '.be-home-section-title', '.be-home-chart-name',
            '.be-home-chart-count', '.be-home-categories a.tag', '.be-home .be-home-link', '.be-home-compat span', '.be-home-compat a', '.be-home-btn-secondary'];
        for (const selector of selectors) {
            const element = doc.querySelector(selector);
            if (!element) { low.push(`${selector} missing`); continue; }
            const bg = background(win, element);
            const value = ratio(over(textColour(win, element), bg), bg);
            if (value < 4.5) low.push(`${selector} ${value.toFixed(2)}`);
        }
        const stops = name => (win.getComputedStyle(doc.documentElement).getPropertyValue(name).match(/#[0-9a-f]{6}/gi) ?? []).map(hex);
        const titleBg = background(win, doc.querySelector('.be-home-title'));
        stops('--be-home-text-gradient').forEach(stop => { const v = ratio(stop, titleBg); if (v < 3) low.push(`title gradient stop ${v.toFixed(2)}`); });
        stops('--be-home-button-gradient').forEach(stop => { const v = ratio({ r: 255, g: 255, b: 255, a: 1 }, stop); if (v < 4.5) low.push(`white label on button stop ${v.toFixed(2)}`); });

        // WCAG 1.4.11: the focus ring reaches 3:1 against the page.
        // The ring is read from the page's focus rules (A11Y-006): live :focus-visible needs window focus, and without
        // it outline-color is the text colour, which used to pass a ring that wasn't drawn.
        const control = doc.querySelector('.be-home-chart-type');
        const ring = focusRing(doc, win, control);
        const ringRatio = ring && ring.style !== 'none' && ring.width >= 1 && ring.colour ? ratio(ring.colour, background(win, control.parentElement)) : 0;
        if (!ring) low.push('no focus ring rule');
        else if (ringRatio < 3) low.push(`focus ring ${ringRatio.toFixed(2)}`);

        return result(`S-09 ${theme}: text 4.5:1, title gradient 3:1, button label 4.5:1, focus ring 3:1`, 'none below', low.length ? low.join('; ') : 'none below');
    }

    const white = { r: 255, g: 255, b: 255, a: 1 };
    const black22 = { r: 0, g: 0, b: 0, a: 0.22 };
    const gradientStops = ({ doc, win }) =>
        (win.getComputedStyle(doc.documentElement).getPropertyValue('--be-home-button-gradient').match(/#[0-9a-f]{6}/gi) ?? []).map(hex);

    // S-15: the gradient bar and the current-section mark on a page.
    function navbarSection({ doc, win }, path, expectedSection, expectedAria) {
        const navbar = doc.querySelector('.be-demo-navbar');
        const gradient = win.getComputedStyle(navbar).backgroundImage.includes('rgb(81, 43, 212)') && win.getComputedStyle(navbar).backgroundImage.includes('rgb(14, 116, 144)');
        const current = [...navbar.querySelectorAll('.navbar-start .navbar-item.is-current')];
        const marked = current.map(a => `${a.textContent.trim()} ${a.getAttribute('aria-current')}`).join(',') || 'none';
        const others = [...navbar.querySelectorAll('.navbar-start .navbar-item:not(.is-current)')].filter(a => a.hasAttribute('aria-current')).length;
        return result(`S-15 ${path}: gradient bar, current section and aria-current`, `true, ${expectedSection === 'none' ? 'none' : `${expectedSection} ${expectedAria}`}, 0`, `${gradient}, ${marked}, ${others}`);
    }

    // S-16: white bar text and the dark mark against both ends of the gradient; the white focus ring at least 3:1.
    function navbarBar(page, theme) {
        const { doc, win } = page;
        const low = [];
        const barItems = [doc.querySelector('.be-demo-navbar .navbar-brand .navbar-item span'), ...doc.querySelectorAll('.be-demo-navbar .navbar-start > .navbar-item, .be-demo-navbar .navbar-end > a.navbar-item, .be-demo-navbar .navbar-end .navbar-link, .be-demo-navbar .navbar-end .tag')];
        const notWhite = barItems.filter(e => win.getComputedStyle(e).color !== 'rgb(255, 255, 255)').map(e => e.textContent.trim() || e.getAttribute('aria-label'));
        if (notWhite.length) low.push(`not white: ${notWhite.join(', ')}`);
        for (const stop of gradientStops(page)) {
            if (ratio(white, stop) < 4.5) low.push(`white on stop ${ratio(white, stop).toFixed(2)}`);
            const marked = over(black22, stop);
            if (ratio(white, marked) < 4.5) low.push(`white on marked stop ${ratio(white, marked).toFixed(2)}`);
        }
        const item = doc.querySelector('.be-demo-navbar .navbar-start > .navbar-item');
        const ring = focusRing(doc, win, item);
        const drawn = ring && ring.style !== 'none' && ring.width >= 1 && ring.colour ? ring.colour : null;
        if (!drawn) low.push('no focus ring rule');
        for (const stop of drawn ? gradientStops(page) : []) {
            const value = ratio(drawn, stop);
            if (value < 3) low.push(`focus ring on stop ${value.toFixed(2)}`);
        }
        return result(`S-16 ${theme} 1280px: white bar text, text 4.5:1 on the gradient and the mark, focus ring 3:1`, 'none below', low.length ? low.join('; ') : 'none below');
    }

    // S-17: the theme-surface panels: the expanded phone menu (375px) or the theme dropdown (1280px).
    async function navbarPanel(page, theme, width) {
        const { doc, win } = page;
        const low = [];
        let panel;
        const burger = doc.querySelector('.be-demo-navbar .navbar-burger');
        if (width < 1024) {
            if (win.getComputedStyle(burger).color !== 'rgb(255, 255, 255)') low.push('burger not white');
            burger.click();
            await until(() => doc.querySelector('.be-demo-navbar .navbar-menu.is-active'), 3000);
            await frames(2);
            panel = doc.querySelector('.be-demo-navbar .navbar-menu');
        } else {
            panel = doc.querySelector('.be-demo-navbar .navbar-dropdown');
        }
        const surface = parse(win.getComputedStyle(panel).backgroundColor);
        const page_ = background(win, doc.querySelector('.be-home') ?? doc.body);
        if (!surface || surface.a < 1) low.push('panel not opaque');
        else if (Math.abs(luminance(surface) - luminance(page_)) > 0.2) low.push('panel is not the theme surface');
        const bg = surface && surface.a >= 1 ? surface : page_;
        for (const item of panel.querySelectorAll('.navbar-item')) {
            if (item.closest('.navbar-dropdown') && width < 1024) continue;
            const value = ratio(over(textColour(win, item), bg), bg);
            if (value < 4.5) low.push(`${item.textContent.trim().slice(0, 16) || item.getAttribute('aria-label')} ${value.toFixed(2)}`);
        }
        for (const icon of panel.querySelectorAll('.be-demo-navbar-icon')) {
            const value = ratio(over(textColour(win, icon), bg), bg);
            if (value < 3) low.push(`icon ${value.toFixed(2)}`);
        }
        if (width < 1024) {
            burger.click();
            await frames(2);
        }
        return result(`S-17 ${theme} ${width}px: ${width < 1024 ? 'white burger; expanded menu' : 'theme dropdown'} on the theme surface, text 4.5:1, icons 3:1`, 'none below', low.length ? low.join('; ') : 'none below');
    }

    // S-18: navbar names, safe new-tab links, tab stops for the controls the package renders without href, unique ids.
    function navbarMarkup({ doc }) {
        const navbar = doc.querySelector('.be-demo-navbar');
        const unnamed = [...navbar.querySelectorAll('a, button')].filter(c => !(c.getAttribute('aria-label') || c.innerText.trim() || c.querySelector('img[alt]:not([alt=""])'))).length;
        const unsafe = [...navbar.querySelectorAll('a[target=_blank]')].filter(a => !a.rel.includes('noopener')).length;
        const noTab = [...navbar.querySelectorAll('.navbar-burger, .navbar-link, .navbar-dropdown .navbar-item')].filter(c => c.getAttribute('tabindex') !== '0' || c.getAttribute('role') !== 'button').length;
        const ids = [...navbar.querySelectorAll('[id]')].map(e => e.id);
        const duplicates = ids.filter((id, i) => ids.indexOf(id) !== i).length;
        return result('S-18 Navbar: unnamed controls, new-tab links without noopener, controls without a tab stop and button role, duplicate ids', '0, 0, 0, 0', `${unnamed}, ${unsafe}, ${noTab}, ${duplicates}`);
    }

    function heritageLayout(page, width, theme, expected) {
        const { doc, win } = page;
        const link = doc.querySelector('.be-demo-navbar .navbar-brand a.navbar-item');
        const name = link?.querySelector('.be-navbar-brand-name');
        const navbarMark = link?.querySelector('.be-navbar-brand-mark');
        const footerMark = doc.querySelector('.be-footer-brand img');
        const footerName = doc.querySelector('.be-footer-brand-name');
        const loaded = mark => !!mark?.naturalWidth && new URL(mark.currentSrc).pathname.endsWith(expected.heritageLogo);
        const fits = element => {
            if (!element) return false;
            const rect = element.getBoundingClientRect();
            return rect.width > 0 && rect.left >= 0 && rect.right <= doc.documentElement.clientWidth
                && element.scrollWidth <= element.clientWidth;
        };
        const markBox = navbarMark?.getBoundingClientRect();
        const backing = navbarMark && parse(win.getComputedStyle(navbarMark).backgroundColor);
        const stops = gradientStops(page);
        const visibleMark = markBox?.width >= 36 && markBox?.height >= 36 && fits(navbarMark)
            && backing?.a === 1 && stops.length === 2 && stops.every(stop => ratio(backing, stop) >= 3);
        return [
            result(`Heritage ${theme} ${width}px: both marks load`, true, loaded(navbarMark) && loaded(footerMark)),
            result(`Heritage ${theme} ${width}px: navbar mark fits and backing contrasts with the gradient`, true, visibleMark),
            result(`Heritage ${theme} ${width}px: complete names fit the layout`, true,
                fits(name) && fits(footerName) && name.textContent.trim() === expected.brandName
                && footerName.textContent.trim() === expected.brandName),
            result(`Heritage ${theme} ${width}px: named home link and 24px target`, true,
                path(link) === '/' && link?.getAttribute('aria-label') === expected.brandName
                && link.getBoundingClientRect().height >= 24 && link.getBoundingClientRect().width >= 24),
        ];
    }

    function heritageCards(doc, cardPath) {
        const selectors = ['meta[property="og:image"]', 'meta[name="twitter:image"]'];
        return selectors.every(selector => {
            const tags = doc.head.querySelectorAll(selector);
            return tags.length === 1 && new URL(tags[0].content, doc.baseURI).pathname.endsWith(cardPath);
        });
    }

    async function brokenBadge({ doc }) {
        const hero = doc.querySelector('.be-home-hero');
        const before = hero.getBoundingClientRect().height;
        const image = doc.querySelector('.be-home-badges img');
        const failed = new Promise(resolve => {
            image.addEventListener('error', () => resolve(true), { once: true });
            setTimeout(() => resolve(false), 5000);
        });
        image.src = 'https://invalid.invalid/badge.svg';
        const error = await failed;
        await frames(2);
        const after = hero.getBoundingClientRect().height;
        return [result('S-10 A badge that cannot load: load error, alt text kept, hero height within 2px', 'true, NuGet version, true', `${error}, ${image.alt}, ${Math.abs(after - before) <= 2}`)];
    }

    // The site animates the page background when the theme changes, so colours are read once the background stops changing.
    async function settleTheme(page, theme) {
        page.win.setTheme(theme, false);
        let previous = '';
        for (let i = 0; i < 40; i++) {
            await demoSleep(100);
            const current = page.win.getComputedStyle(page.doc.documentElement).backgroundColor;
            if (current === previous) break;
            previous = current;
        }
        await frames(2);
    }

    async function run(expected) {
        stalls = 0;
        const errors = [];
        const original = console.error;
        console.error = (...args) => { errors.push(args.join(' ')); original.apply(console, args); };
        const onError = event => errors.push(event.message);
        window.addEventListener('error', onError);
        const cases = [];

        try {
            const host = document.querySelector('[data-self-check=hero-motion]');
            cases.push(...await chartTypes(host, expected));
            cases.push(...await quickSwitching(host, expected));
            cases.push(...await reducedMotion());
            cases.push(...await themeColours(host));
            const favicons = [...document.querySelectorAll('link[rel="icon"]')]
                .map(icon => `${icon.getAttribute('href')}:${icon.getAttribute('sizes')}`).join(', ');
            cases.push(result('Heritage favicon declarations', 'favicon-16.png:16x16, favicon.png:32x32', favicons));
            cases.push(result('Heritage default card: one runtime image per platform', true, heritageCards(document, expected.heritageCard)));

            for (const width of [320, 375, 640, 768, 1280]) {
                const page = await loadFrame(expected.frameHostId, width);
                cases.push(result(`Home page loaded in a ${width}px frame`, true, page.ready));
                if (!page.ready) continue;
                cases.push(...layout(page, width));
                if ([320, 768, 1280].includes(width)) {
                    for (const theme of ['light', 'dark']) {
                        await settleTheme(page, theme);
                        cases.push(...heritageLayout(page, width, theme, expected));
                    }
                }

                if (width === 375) {
                    for (const theme of ['light', 'dark']) {
                        await settleTheme(page, theme);
                        cases.push(await navbarPanel(page, theme, width));
                    }
                }

                if (width === 1280) {
                    cases.push(...content(page, expected));
                    cases.push(...structure(page));
                    cases.push(navbarSection(page, '/', 'none'));
                    cases.push(navbarMarkup(page));
                    for (const theme of ['light', 'dark']) {
                        await settleTheme(page, theme);
                        cases.push(contrast(page, theme));
                        cases.push(navbarBar(page, theme));
                        cases.push(await navbarPanel(page, theme, width));
                    }
                    cases.push(...await brokenBadge(page));
                }
            }

            // The navbar on the other layouts: docs, demos, blog and the 404 page.
            for (const [path, section, aria] of [
                ['/docs/getting-started/introduction', 'Docs', 'true'], ['/demos/bar-chart', 'Demos', 'true'],
                ['/blog/home', 'Releases', 'true'], ['/does-not-exist', 'none'],
            ]) {
                const page = await loadFrame(expected.frameHostId, 1280, path);
                cases.push(result(`${path} loaded in a 1280px frame`, true, page.ready));
                if (page.ready) {
                    cases.push(navbarSection(page, path, section, aria));
                    if (path === '/demos/bar-chart') {
                        cases.push(result('Heritage retains the bar chart page card override', true, heritageCards(page.doc, expected.barChartCard)));
                    }
                }
            }

            cases.push(result('Console and uncaught errors during the run', 0, errors.length));
            cases.push(result('No frame stalled during the run (otherwise the run is void)', 0, stalls));
        }
        finally {
            console.error = original;
            window.removeEventListener('error', onError);
        }

        return cases;
    }

    return { run };
})();

// Unlisted accessibility self-check page (story 26111901; rules.md "W3C and WCAG 2.x conformance" and A11Y-001 to A11Y-004).
// Loads each routed page in a frame and checks it at 320, 768 and 1280 pixels wide, in light and dark. The cases are named
// by the story's acceptance criteria. A page that does not load is recorded as failed and the walk continues.
window.accessibilitySelfCheck = (() => {
    const result = (page, name, expected, actual) => ({ page, ...selfCheckResult(name, expected, actual) });
    const { parse, canvasColour, over, ratio, background, textColour, focusRing, withFocusStyles } = demoSelfCheckColour;
    let stalls = 0;
    let drawn = 0;
    const frame = () => new Promise(resolve => {
        const timer = setTimeout(() => { stalls++; resolve(); }, 1000);
        requestAnimationFrame(() => { clearTimeout(timer); drawn++; resolve(); });
    });
    const frames = async count => { for (let i = 0; i < count; i++) await frame(); };

    const tabbableSelector = 'a[href], button:not([disabled]), input:not([disabled]):not([type="hidden"]), select:not([disabled]), textarea:not([disabled]), summary, [tabindex]:not([tabindex="-1"])';
    const controlSelector = 'a[href], button, [role="button"], input:not([type="hidden"]), select, textarea, summary';
    const isShown = (win, element) => {
        if (!element.isConnected || element.closest('[hidden], [aria-hidden="true"]')) return false;
        const style = win.getComputedStyle(element);
        if (style.visibility === 'hidden' || style.display === 'none') return false;
        const box = element.getBoundingClientRect();
        return box.width > 0 && box.height > 0;
    };
    const isScreenReaderOnly = (win, element) => !!element.closest('.is-sr-only, .be-skip-link:not(:focus)');
    const accessibleName = (doc, element) => {
        const labelledBy = element.getAttribute('aria-labelledby');
        if (labelledBy) return labelledBy.split(/\s+/).map(id => doc.getElementById(id)?.textContent ?? '').join(' ').trim();
        const label = element.getAttribute('aria-label') ?? '';
        if (label.trim()) return label.trim();
        if (element.labels?.length) return [...element.labels].map(l => l.textContent).join(' ').trim();
        const text = element.textContent.trim() || [...element.querySelectorAll('img[alt]')].map(img => img.alt).join(' ').trim();
        return text || (element.getAttribute('title') ?? '').trim() || (element.getAttribute('placeholder') ?? '').trim();
    };
    const sha = async text => [...new Uint8Array(await crypto.subtle.digest('SHA-256', new TextEncoder().encode(text)))]
        .map(b => b.toString(16).padStart(2, '0')).join('').slice(0, 16);

    async function loadFrame(hostId, path) {
        const iframe = document.createElement('iframe');
        iframe.title = `${path} under test`;
        iframe.style.cssText = 'display:block;width:1280px;height:900px;margin-bottom:1rem;border:0;outline:1px solid #ccc;';
        const loaded = new Promise(resolve => iframe.addEventListener('load', resolve, { once: true }));
        iframe.src = path;
        document.getElementById(hostId).appendChild(iframe);
        await loaded;
        const end = performance.now() + 20000;
        while (performance.now() < end && !(iframe.contentDocument?.querySelector('h1') && iframe.contentDocument.querySelector('footer'))) await demoSleep(100);
        await frames(5);
        await demoSleep(400);
        const doc = iframe.contentDocument;
        // Colours are read right after a theme switch, so transitions are switched off in the page under test:
        // without drawn frames a transition never finishes, and a mid-transition colour is not the page's colour.
        if (doc?.head) {
            const still = doc.createElement('style');
            still.setAttribute('data-self-check', '');
            still.textContent = '*, *::before, *::after { transition: none !important; animation: none !important; }';
            doc.head.appendChild(still);
        }
        return { iframe, doc, win: iframe.contentWindow, ready: !!doc?.querySelector('h1') };
    }

    async function setWidth(page, width) {
        page.iframe.style.width = `${width}px`;
        await frames(4);
        await demoSleep(250);
    }

    // The site animates the page background when the theme changes, so colours are read once the background stops changing.
    async function settleTheme(page, theme) {
        page.win.setTheme(theme, false);
        let previous = '';
        for (let i = 0; i < 40; i++) {
            await demoSleep(100);
            const current = page.win.getComputedStyle(page.doc.documentElement).backgroundColor;
            if (current === previous) break;
            previous = current;
        }
        await frames(3);
    }

    // AC-01 (A11Y-001): the first Tab stop is a skip link to the single main landmark, which holds the h1; navbar and footer are outside it.
    function landmarks({ doc, win }, path) {
        const mains = doc.querySelectorAll('main, [role="main"]');
        const main = mains[0];
        const first = [...doc.querySelectorAll(tabbableSelector)].find(e => isShown(win, e) || e.matches('.be-skip-link'));
        const isSkip = !!first?.matches('a[href="#main-content"]') && doc.getElementById('main-content') === main;
        let visibleOnFocus = false;
        if (isSkip) {
            visibleOnFocus = withFocusStyles(doc, win, first, () => {
                const box = first.getBoundingClientRect();
                return box.width >= 24 && box.height >= 24 && box.top >= 0 && box.left >= 0 && box.bottom <= win.innerHeight;
            });
        }
        const h1InMain = !!main && !!main.querySelector('h1') && doc.querySelectorAll('h1').length === main.querySelectorAll('h1').length;
        const outside = !!main && ![...doc.querySelectorAll('.be-demo-navbar, footer, .be-bulma-page-menu')].some(e => main.contains(e));
        return result(path, 'AC-01 skip link first and visible on focus; one main; h1 in main; navbar, menu and footer outside',
            'true | true | 1 | true | true', `${isSkip} | ${visibleOnFocus} | ${mains.length} | ${h1InMain} | ${outside}`);
    }

    // AC-02: one h1, no skipped level, no empty heading; and, when the T-01 baseline is present, the same ids, text and sizes.
    async function headings({ doc, win }, path, baseline) {
        const all = [...doc.querySelectorAll('h1, h2, h3, h4, h5, h6')];
        const levels = all.filter(h => isShown(win, h)).map(h => Number(h.tagName[1]));
        const skips = levels.filter((level, i) => i > 0 && level > levels[i - 1] + 1).length;
        const empty = all.filter(h => !h.textContent.trim()).length;
        const cases = [result(path, 'AC-02 one h1; skipped levels; empty headings', '1 | 0 | 0', `${doc.querySelectorAll('h1').length} | ${skips} | ${empty}`)];
        const before = baseline?.pages?.[path.split('?')[0]];
        if (before) {
            // R2-T05 made the package hero subtitle a div (it is not a heading), so the expected hash is rebuilt from the
            // T-01 list without those entries; every other heading keeps its T-01 id, text, size and weight.
            const kept = (before.list ?? []).filter(h => !/\bbe-bulma-(doc-)?subtitle\b/.test(h.cls ?? ''));
            const expectedHash = before.list ? await sha(kept.map(h => [h.id || '', h.text, h.size, h.weight].join('|')).join('\n')) : before.hash;
            const canon = all.map(h => {
                const style = win.getComputedStyle(h);
                const text = (h.querySelector('.anchor-name')?.textContent ?? h.textContent).trim().replace(/\s+/g, ' ').slice(0, 80);
                return [h.id || '', text, style.fontSize, style.fontWeight].join('|');
            }).join('\n');
            cases.push(result(path, 'AC-02, AC-10 heading ids, text and sizes match the T-01 baseline (hero subtitles excepted)', expectedHash, await sha(canon)));
        }
        return cases;
    }

    // AC-03 (A11Y-003): new-tab links have noopener and say they open in a new tab.
    function newTabLinks({ doc }, path) {
        const links = [...doc.querySelectorAll('a[target="_blank"]')];
        const unsafe = links.filter(a => !/\bnoopener\b/.test(a.rel)).length;
        const unannounced = links.filter(a => !/opens in a new tab/i.test(accessibleName(doc, a))).length;
        return result(path, 'AC-03 new-tab links without noopener; without a new-tab name', '0 | 0', `${unsafe} | ${unannounced}`);
    }

    // AC-04 (A11Y-002): every chart canvas is an image with a name, on the canvas or on a wrapper with role="img".
    function chartNames({ doc }, path) {
        const canvases = [...doc.querySelectorAll('canvas')];
        const unnamed = canvases.filter(canvas => {
            const holder = canvas.closest('[role="img"]');
            return !holder || !accessibleName(doc, holder);
        }).length;
        return result(path, 'AC-04 charts without role="img" and a name', 0, unnamed);
    }

    // AC-05 (A11Y-004): data marks (fill or a visible border) at 3:1, chart text at 4.5:1, a legend or labels when more than one series.
    function chartContrast({ doc, win }, path, theme) {
        const colour = canvasColour;
        let marks = 0, text = 0, legend = 0, measured = 0;
        for (const canvas of doc.querySelectorAll('canvas')) {
            const chart = win.Chart?.getChart(canvas);
            if (!chart || !isShown(win, canvas)) continue;
            measured++;
            const bg = background(win, canvas);
            const lines = ['line', 'radar'];
            chart.data.datasets.forEach((dataset, index) => {
                const meta = chart.getDatasetMeta(index);
                if (meta.hidden || !chart.isDatasetVisible(index)) return;
                const type = meta.type ?? chart.config.type;
                const elements = lines.includes(type) && meta.dataset ? [meta.dataset] : meta.data;
                for (const element of elements) {
                    const options = element.options ?? {};
                    const fill = colour(options.backgroundColor);
                    const border = (options.borderWidth ?? 0) >= 1 ? colour(options.borderColor) : null;
                    const best = Math.max(fill && !lines.includes(type) ? ratio(over(fill, bg), bg) : 0, border ? ratio(over(border, bg), bg) : 0);
                    if ((fill || border) && best < 3) { marks++; break; }
                }
            });
            const textColours = [chart.legend?.options?.labels?.color, ...Object.values(chart.scales ?? {}).map(s => s.options?.ticks?.color), win.Chart.defaults.color];
            if (textColours.map(colour).filter(Boolean).some(c => ratio(over(c, bg), bg) < 4.5)) text++;
            const series = ['pie', 'doughnut', 'polarArea'].includes(chart.config.type) ? (chart.data.labels?.length ?? 0) : chart.data.datasets.length;
            const labelled = chart.legend?.options?.display !== false || chart.options?.plugins?.datalabels?.display === true || !!chart.$datalabels;
            // A two-part gauge (a value and its remainder) whose value is written in its shown title does not rely on colour.
            const gauge = series === 2 && chart.options?.plugins?.title?.display === true && !!chart.options.plugins.title.text;
            if (series > 1 && !labelled && !gauge) legend++;
        }
        return result(path, `AC-05 ${theme}: charts with marks below 3:1; with text below 4.5:1; with several series and no legend (${measured} drawn)`, '0 | 0 | 0', `${marks} | ${text} | ${legend}`);
    }

    // AC-06 per theme: text contrast and the focus ring, named controls, unique ids, lang and image alternatives.
    function pageContrast({ doc, win }, path, theme) {
        const low = [];
        let unmeasured = 0;
        const walker = doc.createTreeWalker(doc.body, NodeFilter.SHOW_TEXT);
        const seen = new Set();
        while (walker.nextNode() && seen.size < 800) {
            const element = walker.currentNode.parentElement;
            if (!element || seen.has(element) || !walker.currentNode.data.trim()) continue;
            seen.add(element);
            if (!isShown(win, element) || isScreenReaderOnly(win, element) || element.closest('.be-demo-navbar, canvas, script, style, noscript, iframe')) continue;
            // Text that is fully transparent until hover or focus (such as a heading's # anchor) is not shown text.
            if (textColour(win, element).a === 0) continue;
            // WCAG 1.4.3 exempts text in an inactive control, such as a disabled button or a sample card that is not available yet.
            if (element.closest('[aria-disabled="true"], :disabled')) continue;
            let hasImage = false;
            for (let node = element; node && node !== doc.documentElement; node = node.parentElement) {
                const style = win.getComputedStyle(node);
                if (style.backgroundImage !== 'none') { hasImage = true; break; }
                if ((parse(style.backgroundColor)?.a ?? 0) >= 1) break;
            }
            if (hasImage) { unmeasured++; continue; }
            const style = win.getComputedStyle(element);
            const size = parseFloat(style.fontSize);
            const large = size >= 24 || (size >= 18.66 && Number(style.fontWeight) >= 700);
            const bg = background(win, element);
            const value = ratio(over(textColour(win, element), bg), bg);
            if (value < (large ? 3 : 4.5)) low.push(`${element.tagName.toLowerCase()}${element.className ? '.' + String(element.className).trim().split(/\s+/)[0] : ''} "${element.textContent.trim().slice(0, 24)}" ${value.toFixed(2)}`);
        }
        const rings = [];
        // The skip link is drawn over the navbar's gradient, not over its parent in the markup, so its white ring can't be
        // measured here; AC-01 checks that it shows on focus.
        for (const control of [...doc.querySelectorAll(tabbableSelector)].filter(e => isShown(win, e) && !e.closest('.be-demo-navbar, .be-skip-link')).slice(0, 12)) {
            const ring = focusRing(doc, win, control);
            if (!ring) continue; // the browser's default focus ring
            const bg = background(win, control.parentElement);
            const outline = ring.style !== 'none' && ring.width >= 1 && ring.colour ? ratio(over(ring.colour, bg), bg) : 0;
            const shadow = ring.shadow && ring.shadow !== 'none';
            if (outline < 3 && !shadow) rings.push(`${control.tagName.toLowerCase()} "${accessibleName(doc, control).slice(0, 20)}" ${outline.toFixed(2)}`);
        }
        return [
            result(path, `AC-06 ${theme}: text below 4.5:1 (3:1 large)`, 'none', low.length ? `${low.length}: ${low.slice(0, 6).join('; ')}` : 'none'),
            result(path, `AC-06 ${theme}: focus ring below 3:1`, 'none', rings.length ? rings.join('; ') : 'none'),
        ];
    }

    function markup({ doc, win }, path) {
        const ids = [...doc.querySelectorAll('[id]')].map(e => e.id);
        const duplicates = [...new Set(ids.filter((id, i) => ids.indexOf(id) !== i))];
        const noAlt = [...doc.querySelectorAll('img')].filter(img => !img.hasAttribute('alt')).length;
        return result(path, 'AC-06 lang; duplicate ids; images without alt', 'true | none | 0',
            `${!!doc.documentElement.lang} | ${duplicates.length ? duplicates.slice(0, 5).join(',') : 'none'} | ${noAlt}`);
    }

    // AC-06 per width: every shown control has a name. Some controls are shown only at some widths (the sidebar menu button
    // below 769 px), so this runs at each width.
    function controlNames({ doc, win }, path, width) {
        const unnamed = [...doc.querySelectorAll(controlSelector)].filter(e => isShown(win, e) && !accessibleName(doc, e))
            .map(e => e.outerHTML.slice(0, 60));
        return result(path, `AC-06 ${width}px: unnamed controls`, 0, `${unnamed.length}${unnamed.length ? ' ' + unnamed.slice(0, 3).join(' ') : ''}`);
    }

    // AC-06 per width: no horizontal scroll and 24 by 24 CSS px targets (WCAG 2.5.8; links inside a line of text are excepted).
    function reflow({ doc, win }, path, width) {
        const scroll = doc.documentElement.scrollWidth > doc.documentElement.clientWidth + 1;
        // WCAG 2.5.8 inline exception: a link displayed inline whose nearest block holds more text than the link (it sits in a sentence).
        const inline = control => {
            if (control.tagName !== 'A' || !win.getComputedStyle(control).display.startsWith('inline')) return false;
            let block = control.parentElement;
            while (block && win.getComputedStyle(block).display.startsWith('inline')) block = block.parentElement;
            return !!block && block.textContent.trim().length > control.textContent.trim().length + 10;
        };
        const targets = [...doc.querySelectorAll(controlSelector)].filter(c => isShown(win, c) && !c.closest('canvas'));
        const boxes = new Map(targets.map(c => [c, c.getBoundingClientRect()]));
        const undersized = c => boxes.get(c).width < 24 || boxes.get(c).height < 24;
        // WCAG 2.5.8 spacing exception: an undersized target passes when a 24 CSS px circle on its centre reaches no other
        // target, and no other undersized target's circle.
        const centre = box => ({ x: box.left + box.width / 2, y: box.top + box.height / 2 });
        const spaced = c => {
            const at = centre(boxes.get(c));
            return targets.every(other => {
                if (other === c || other.contains(c) || c.contains(other)) return true;
                const box = boxes.get(other);
                const dx = Math.max(box.left - at.x, 0, at.x - box.right), dy = Math.max(box.top - at.y, 0, at.y - box.bottom);
                if (Math.hypot(dx, dy) < 12) return false;
                if (undersized(other)) { const o = centre(box); if (Math.hypot(o.x - at.x, o.y - at.y) < 24) return false; }
                return true;
            });
        };
        const small = targets.filter(c => !inline(c) && undersized(c) && !spaced(c))
            .map(c => `${accessibleName(doc, c).slice(0, 20) || c.tagName.toLowerCase()} ${Math.round(c.getBoundingClientRect().width)}x${Math.round(c.getBoundingClientRect().height)}`);
        return result(path, `AC-06 ${width}px: horizontal scroll; targets below 24 by 24 CSS px`, 'false | none', `${scroll} | ${small.length ? `${small.length}: ${small.slice(0, 4).join('; ')}` : 'none'}`);
    }

    // AC-07 (Q-06): the opt-in W3C mode (?w3c=1, or ?w3c=only to skip the other checks). Only the rendered markup of the demo's
    // public pages and the CSS files this repository serves are sent, one request at a time with a pause, without cookies.
    // A request that fails, times out or is refused is recorded as "not run" with the reason, never as a pass.
    // The pause between requests is ?pace= seconds (3 to 120, default 3). After 3 refused or failed requests in a row the
    // service is treated as unavailable (a rate limit or its bot protection) and nothing more is sent in this run.
    const w3cMessages = [];
    let lastW3cRequest = 0;
    let w3cPaceMs = 3000;
    let w3cFailuresInARow = 0;
    const w3cFetch = async (url, init) => {
        if (w3cFailuresInARow >= 3) throw new Error('stopped after 3 failed requests in a row (rate limit or bot protection)');
        const wait = lastW3cRequest + w3cPaceMs - performance.now();
        if (wait > 0) await demoSleep(wait);
        lastW3cRequest = performance.now();
        const controller = new AbortController();
        const timer = setTimeout(() => controller.abort(), 30000);
        try {
            const response = await fetch(url, { ...init, credentials: 'omit', cache: 'no-store', referrerPolicy: 'no-referrer', signal: controller.signal });
            w3cFailuresInARow = response.ok ? 0 : w3cFailuresInARow + 1;
            return response;
        }
        catch (error) { w3cFailuresInARow++; throw error; }
        finally { clearTimeout(timer); }
    };
    const notRun = (page, name, expected, reason) => ({ page, name, expected, actual: `not run: ${reason}`, status: 'not run' });
    const reason = error => error?.name === 'AbortError' ? 'timed out after 30 s' : (error?.message || 'request failed');

    async function w3cMarkup({ doc }, path) {
        const name = 'AC-07 W3C Nu checker: errors in the rendered markup (warnings listed, not failed)';
        const clone = doc.documentElement.cloneNode(true);
        clone.querySelectorAll('[data-self-check]').forEach(element => element.remove());
        // Blazor runtime attributes exist only in the live DOM, never in served markup: _bl_N (@ref element references) and
        // b-xxxxxxxxxx (CSS isolation scopes, from the packages). They are left out of the copy sent (developer decision I-10).
        let stripped = 0;
        for (const element of clone.querySelectorAll('*')) {
            for (const attribute of [...element.attributes]) {
                if (/^_bl_\d+$/.test(attribute.name) || /^b-[a-z0-9]{10}$/.test(attribute.name)) { element.removeAttribute(attribute.name); stripped++; }
            }
        }
        const html = `<!DOCTYPE html>\n${clone.outerHTML}`;
        try {
            const response = await w3cFetch('https://validator.w3.org/nu/?out=json', { method: 'POST', body: html, headers: { 'Content-Type': 'text/html; charset=utf-8' } });
            if (!response.ok) return notRun(path, name, '0 errors', `HTTP ${response.status}`);
            const messages = (await response.json()).messages ?? [];
            w3cMessages.push({ path, messages, stripped });
            const errors = messages.filter(m => m.type === 'error' || m.type === 'non-document-error');
            const warnings = messages.filter(m => m.type === 'info' && m.subType === 'warning').length;
            const first = errors.slice(0, 3).map(m => `line ${m.lastLine ?? '?'}: ${m.message}`).join(' | ');
            return { page: path, name, expected: '0 errors', actual: `${errors.length} errors, ${warnings} warnings, ${stripped} Blazor runtime attributes left out${first ? `: ${first}` : ''}`, status: errors.length ? 'fail' : 'pass' };
        }
        catch (error) { return notRun(path, name, '0 errors', reason(error)); }
    }

    // The CSS files this repository serves: the demo stylesheet, its two Prism theme copies and the host's scoped bundle.
    // Package stylesheets (BlazorExpress.Bulma and its scoped CSS) and CDN files are outside this repository.
    async function w3cStyles() {
        const base = '_content/BlazorExpress.ChartJS.Demo.RCL/css/';
        const own = [...document.querySelectorAll('link[rel="stylesheet"]')].map(link => link.href)
            .filter(href => href.startsWith(location.origin) && !href.includes('/_content/BlazorExpress.Bulma'));
        const files = [...new Set([...own, new URL(`${base}prism-vs.min.css`, document.baseURI).href, new URL(`${base}prism-vsc-dark-plus.min.css`, document.baseURI).href])];
        const cases = [];
        for (const href of files) {
            const file = new URL(href).pathname;
            const name = `AC-07 W3C CSS validator: errors in ${file} (warnings listed, not failed)`;
            try {
                const text = await (await fetch(href, { cache: 'no-store' })).text();
                const form = new FormData();
                form.append('text', text);
                form.append('output', 'json');
                form.append('profile', 'css3svg');
                form.append('warning', '1');
                const response = await w3cFetch('https://jigsaw.w3.org/css-validator/validator', { method: 'POST', body: form });
                if (!response.ok) { cases.push(notRun('-', name, '0 errors', `HTTP ${response.status}`)); continue; }
                const report = (await response.json()).cssvalidation ?? {};
                w3cMessages.push({ path: file, css: report });
                const errors = report.errors ?? [];
                const warnings = report.result?.warningcount ?? (report.warnings ?? []).length;
                const first = errors.slice(0, 3).map(e => `line ${e.line ?? '?'}: ${(e.message ?? '').trim()}`).join(' | ');
                cases.push({ page: '-', name, expected: '0 errors', actual: `${errors.length} errors, ${warnings} warnings${first ? `: ${first}` : ''}`, status: errors.length ? 'fail' : 'pass' });
            }
            catch (error) { cases.push(notRun('-', name, '0 errors', reason(error))); }
        }
        return cases;
    }

    async function checkPage(hostId, path, baseline, w3c) {
        const cases = [];
        const page = await loadFrame(hostId, path);
        try {
            cases.push(result(path, 'Page loaded with an h1 and a footer', true, page.ready));
            if (!page.ready) return cases;
            await settleTheme(page, 'light');
            if (w3c) cases.push(await w3cMarkup(page, path));
            if (w3c === 'only') return cases;
            cases.push(landmarks(page, path));
            cases.push(...await headings(page, path, baseline));
            cases.push(newTabLinks(page, path));
            cases.push(chartNames(page, path));
            cases.push(markup(page, path));
            for (const width of [320, 768, 1280]) {
                await setWidth(page, width);
                cases.push(reflow(page, path, width));
                cases.push(controlNames(page, path, width));
            }
            for (const theme of ['light', 'dark']) {
                await settleTheme(page, theme);
                cases.push(chartContrast(page, path, theme));
                cases.push(...pageContrast(page, path, theme));
            }
        }
        catch (error) {
            cases.push(result(path, 'Checks ran without a script error', 'no error', error.message));
        }
        finally {
            page.iframe.remove();
        }
        return cases;
    }

    async function run(expected) {
        stalls = 0;
        drawn = 0;
        w3cMessages.length = 0;
        w3cFailuresInARow = 0;
        w3cPaceMs = Math.min(Math.max(Number(expected.w3cPace) || 3, 3), 120) * 1000;
        const cases = [];
        let baseline = null;
        try { baseline = JSON.parse(localStorage.getItem('wcag-26111901-baseline') ?? 'null'); } catch { baseline = null; }
        const progress = () => document.getElementById(expected.progressId);
        const undrawn = [];
        for (const [index, { path }] of expected.pages.entries()) {
            if (progress()) progress().textContent = `${index + 1} of ${expected.pages.length}: ${path}`;
            const before = drawn;
            cases.push(...await checkPage(expected.frameHostId, path, baseline, expected.w3c));
            if (drawn === before) undrawn.push(path);
        }
        // A page checked while no frame drew (a hidden pane or a background tab) is void: charts, layout and theme
        // changes don't update. A stall is a second with no frame. With frames drawing on every page, a few stalls come
        // from a frame booting the app and blocking the thread, so they are reported but don't void the run.
        if (expected.w3c) {
            if (progress()) progress().textContent = 'W3C CSS validator';
            cases.push(...await w3cStyles());
            const unvalidated = cases.filter(c => c.name.startsWith('AC-07') && c.status === 'not run').length;
            cases.push(result('-', 'AC-07 every page and CSS file has a W3C result', '0 without a result', `${unvalidated} without a result`));
        }
        cases.push(result('-', `Every page drew frames while it was checked (otherwise the run is void; ${stalls} stalled frames)`, 'none undrawn',
            undrawn.length ? `${undrawn.length} undrawn: ${undrawn.slice(0, 5).join(', ')}` : 'none undrawn'));
        return { pages: expected.pages.length, frames: drawn, stalls, cases };
    }

    // The full W3C messages of the last run, for the evidence record.
    return { run, w3cMessages: () => w3cMessages };
})();

// SiteMetaTags calls this once its tags are on the page. index.html carries site-wide defaults (marked data-site-default)
// for clients that do not run the app, such as link previews and agents; removing them leaves one set of tags per page.
function removeSiteDefaultHeadTags() {
    document.head.querySelectorAll('[data-site-default]').forEach(tag => tag.remove());
}
