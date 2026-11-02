if (!window.blazorexpress) {
    window.blazorexpress = {};
}

if (!window.blazorexpress.chartjs) {
    window.blazorexpress.chartjs = {};
}

(() => {
    const minimumChartJsVersion = '4.6.0';
    let isChartJsVersionChecked = false;

    const applyDataLabelsBackgroundColor = (options) => {
        options.plugins ??= {};
        options.plugins.datalabels ??= {};
        options.plugins.datalabels.backgroundColor = function (context) {
            return context.dataset.backgroundColor;
        };
    };

    const createTooltipLinePlugin = () => ({
        id: 'tooltipLine',
        beforeDraw: chart => {
            if (chart.tooltip?._active && chart.tooltip?._active.length) {
                const ctx = chart.ctx;
                ctx.save();
                const activePoint = chart.tooltip._active[0];

                ctx.beginPath();
                ctx.setLineDash([5, 5]);
                ctx.moveTo(activePoint.element.x, chart.chartArea.top);
                ctx.lineTo(activePoint.element.x, activePoint.element.y);
                ctx.lineWidth = 2;
                ctx.strokeStyle = 'grey';
                ctx.stroke();

                ctx.beginPath();
                ctx.moveTo(activePoint.element.x, activePoint.element.y);
                ctx.lineTo(activePoint.element.x, chart.chartArea.bottom);
                ctx.lineWidth = 2;
                ctx.strokeStyle = 'grey';
                ctx.stroke();

                ctx.restore();
            }
        },
    });

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

    // ResizeAsync sends CSS declarations such as "width:300px"; a style property needs only the value ("300px").
    const toCssValue = (declaration) => {
        const text = String(declaration ?? '');
        const separator = text.indexOf(':');

        return (separator > -1 ? text.slice(separator + 1) : text).replace(/;\s*$/, '').trim();
    };

    const warnIfChartJsIsOutdated = () => {
        if (isChartJsVersionChecked)
            return;

        isChartJsVersionChecked = true;

        if (typeof Chart !== 'undefined' && isVersionBelow(Chart.version, minimumChartJsVersion))
            console.warn(`BlazorExpress.ChartJS: Chart.js ${Chart.version} is loaded; ${minimumChartJsVersion} or later is required.`);
    };

    // Shared implementation for every chart type. A per-type module passes hooks for the behavior that differs.
    const core = {
        addClickHandler: (chart, dotNetObjectReference) => {
            if (!chart || !chart.canvas || !dotNetObjectReference || chart.blazorExpressClickHandler) return;

            chart.blazorExpressClickHandler = true;
            chart.canvas.addEventListener('click', (event) => {
                const activeElements = chart.getElementsAtEventForMode(event, 'nearest', { intersect: true }, true);
                if (!activeElements || activeElements.length === 0) return;

                const activeElement = activeElements[0];
                const dataset = chart.data.datasets?.[activeElement.datasetIndex];

                dotNetObjectReference.invokeMethodAsync('HandleClickAsync', {
                    datasetIndex: activeElement.datasetIndex,
                    datasetLabel: dataset?.label ?? null,
                    index: activeElement.index,
                    label: chart.data.labels?.[activeElement.index] ?? null,
                    value: dataset?.data?.[activeElement.index] ?? null
                }).catch(() => { });
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
        initialize: (elementId, type, data, options, plugins, dotNetObjectReference, hooks = {}) => {
            warnIfChartJsIsOutdated();

            let chart = core.get(elementId);
            if (!chart) {
                core.create(elementId, type, data, options, plugins, hooks);
                chart = core.get(elementId);
            }

            core.addClickHandler(chart, dotNetObjectReference);
        },
        resize: (elementId, width, height) => {
            let chart = core.get(elementId);
            if (chart) {
                chart.canvas.parentNode.style.height = toCssValue(height);
                chart.canvas.parentNode.style.width = toCssValue(width);
                chart.resize();
            }
        },
        update: (elementId, type, data, options) => {
            let chart = core.get(elementId);
            if (chart) {
                if (chart.config.plugins && chart.config.plugins.findIndex(x => x.id == 'datalabels') > -1) {
                    // set datalabel background color
                    applyDataLabelsBackgroundColor(options);
                }

                chart.data = data;
                chart.options = options;
                chart.update();
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
        initialize: (elementId, type, data, options, plugins, dotNetObjectReference) => core.initialize(elementId, type, data, options, plugins, dotNetObjectReference, hooks),
    });

    const arcChartHooks = {
        afterAddDatasetData: (dataset, chartDatasetData) => dataset.backgroundColor.push(chartDatasetData.backgroundColor),
        applyDataLabelsBackgroundColorOnCreate: true,
    };

    Object.assign(window.blazorexpress.chartjs, core);

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
