// In your own script file, loaded after blazorexpress.chartjs.js.
// Chart.js calls onProgress on every animation frame and onComplete once at the end: (animation) => void.
blazorexpress.chartjs.registerFunction('demo.showAnimationProgress', animation => {
    const bar = document.getElementById('demo-animation-progress');
    if (bar)
        bar.value = animation.numSteps > 0 ? animation.currentStep / animation.numSteps : 1;
});

blazorexpress.chartjs.registerFunction('demo.completeAnimationProgress', () => {
    const bar = document.getElementById('demo-animation-progress');
    if (bar)
        bar.value = 1;
});
