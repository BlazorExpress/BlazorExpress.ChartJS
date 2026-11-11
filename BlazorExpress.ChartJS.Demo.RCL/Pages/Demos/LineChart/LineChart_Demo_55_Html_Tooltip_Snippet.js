// In your own script file, loaded after blazorexpress.chartjs.js.
// Chart.js calls an external tooltip function whenever the tooltip changes: (context) => void.
blazorexpress.chartjs.registerFunction('demo.htmlTooltip', context => {
    const { chart, tooltip } = context;
    const parent = chart.canvas.parentNode;

    // Create the tooltip element once per chart.
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

    // Chart.js sets opacity to 0 when the pointer leaves the chart.
    if (tooltip.opacity === 0) {
        box.style.opacity = '0';
        return;
    }

    // Build the content from elements and textContent, never from an HTML string,
    // so a label that contains markup is shown as text.
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

    // Place it at the point Chart.js would have used.
    box.style.opacity = '1';
    box.style.left = `${chart.canvas.offsetLeft + tooltip.caretX}px`;
    box.style.top = `${chart.canvas.offsetTop + tooltip.caretY}px`;
});
