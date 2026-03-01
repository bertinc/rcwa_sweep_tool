// Detect system dark mode preference and apply it
function applyDarkMode(isDarkMode) {
    if (isDarkMode) {
        document.documentElement.setAttribute('data-theme', 'dark');
        document.documentElement.style.colorScheme = 'dark';
    } else {
        document.documentElement.setAttribute('data-theme', 'light');
        document.documentElement.style.colorScheme = 'light';
    }
}

// Apply dark mode on page load
function initializeDarkMode() {
    const isDarkMode = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    applyDarkMode(isDarkMode);
}

// Apply dark mode on page load
initializeDarkMode();

// Listen for changes in system theme preference
if (window.matchMedia) {
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
        applyDarkMode(e.matches);
    });
}
