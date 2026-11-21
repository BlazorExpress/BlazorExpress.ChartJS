// In your own script file. observe(id, dotNetRef) returns whether the page is dark now, then calls the component's
// OnThemeChanged(isDark) each time the theme changes, until disconnect(id) is called.
window.demoThemeObserver = (() => {
    const media = window.matchMedia('(prefers-color-scheme: dark)');
    const stops = new Map(); // id -> stops that observer

    // The data-theme attribute on <html> wins; with no attribute, the system setting decides.
    const isDark = () => {
        const theme = document.documentElement.getAttribute('data-theme');
        return theme ? theme === 'dark' : media.matches;
    };

    const disconnect = id => {
        stops.get(id)?.();
        stops.delete(id);
    };

    const observe = (id, dotNetRef) => {
        disconnect(id);
        let last = isDark();

        const notify = () => {
            const dark = isDark();
            if (dark === last)
                return;

            last = dark;
            dotNetRef.invokeMethodAsync('OnThemeChanged', dark);
        };

        const observer = new MutationObserver(notify);
        observer.observe(document.documentElement, { attributes: true, attributeFilter: ['data-theme'] });
        media.addEventListener('change', notify);
        stops.set(id, () => {
            observer.disconnect();
            media.removeEventListener('change', notify);
        });

        return last;
    };

    return { observe, disconnect };
})();
