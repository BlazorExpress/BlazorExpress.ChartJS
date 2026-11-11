// In your own script file, loaded after blazorexpress.chartjs.js.
// chartjs-plugin-datalabels calls the formatter for every label: (value, context) => text.
blazorexpress.chartjs.registerFunction('demo.percentOfTotal', (value, context) => {
    const total = context.dataset.data.reduce((sum, item) => sum + (item ?? 0), 0);
    return total > 0 ? `${Math.round(value / total * 100)}%` : '';
});
