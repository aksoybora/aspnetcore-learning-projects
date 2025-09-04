// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Theme toggle: persists in localStorage and toggles [data-theme="dark"] on <html>
(function () {
  const root = document.documentElement;
  const storageKey = 'deviceformapp-theme';
  const saved = localStorage.getItem(storageKey);
  if (saved === 'dark') {
    root.setAttribute('data-theme', 'dark');
  }
  document.addEventListener('DOMContentLoaded', function () {
    const btn = document.getElementById('themeToggle');
    if (!btn) return;
    const isDark = () => root.getAttribute('data-theme') === 'dark';
    const applyIcon = () => {
      btn.textContent = isDark() ? '☀️' : '🌙';
    };
    applyIcon();
    btn.addEventListener('click', function () {
      if (isDark()) {
        root.removeAttribute('data-theme');
        localStorage.setItem(storageKey, 'light');
      } else {
        root.setAttribute('data-theme', 'dark');
        localStorage.setItem(storageKey, 'dark');
      }
      applyIcon();
    });
  });
})();
