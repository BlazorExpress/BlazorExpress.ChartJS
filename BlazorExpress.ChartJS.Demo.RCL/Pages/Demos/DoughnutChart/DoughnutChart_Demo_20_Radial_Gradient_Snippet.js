// In your own script file, loaded after blazorexpress.chartjs.js.
// Chart.js calls a colour function for every slice while drawing: (context) => colour.
blazorexpress.chartjs.registerFunction('demo.radialGradient', context => {
    const colors = ['0, 114, 178', '230, 159, 0', '0, 158, 115', '204, 121, 167', '86, 180, 233']; // Okabe–Ito
    const rgb = colors[context.dataIndex % colors.length];

    // The chart area is not known on the very first call; return a plain colour until it is.
    const { ctx, chartArea } = context.chart;
    if (!chartArea)
        return `rgb(${rgb})`;

    const centerX = (chartArea.left + chartArea.right) / 2;
    const centerY = (chartArea.top + chartArea.bottom) / 2;
    const radius = Math.min(chartArea.right - chartArea.left, chartArea.bottom - chartArea.top) / 2;

    // One gradient per slice: light near the centre, the full colour at the rim.
    const gradient = ctx.createRadialGradient(centerX, centerY, 0, centerX, centerY, radius);
    gradient.addColorStop(0.45, `rgba(${rgb}, 0.25)`);
    gradient.addColorStop(1, `rgb(${rgb})`);
    return gradient;
});
