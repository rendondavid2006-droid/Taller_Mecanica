(function () {
    'use strict';

    const sidebar = document.getElementById('sidebar');
    if (!sidebar) return;

    const desktopBreakpoint = 1200;

    function isDesktop() {
        return window.innerWidth >= desktopBreakpoint;
    }

    function removeBackdrop() {
        const backdrop = document.querySelector('.sidebar-backdrop');
        if (backdrop) backdrop.remove();
    }

    function createBackdrop() {
        if (isDesktop()) return;
        removeBackdrop();

        const backdrop = document.createElement('div');
        backdrop.className = 'sidebar-backdrop';
        backdrop.addEventListener('click', hideSidebar);
        document.body.appendChild(backdrop);
    }

    function showSidebar() {
        sidebar.classList.add('active');
        sidebar.classList.remove('inactive');
        createBackdrop();
        if (!isDesktop()) document.body.style.overflow = 'hidden';
    }

    function hideSidebar() {
        sidebar.classList.remove('active');
        sidebar.classList.add('inactive');
        removeBackdrop();
        document.body.style.overflow = '';
    }

    function toggleSidebar(event) {
        if (event) event.preventDefault();
        if (sidebar.classList.contains('active')) hideSidebar();
        else showSidebar();
    }

    document.querySelectorAll('.burger-btn').forEach(function (button) {
        button.addEventListener('click', toggleSidebar);
    });

    document.querySelectorAll('.sidebar-hide').forEach(function (button) {
        button.addEventListener('click', hideSidebar);
    });

    document.querySelectorAll('.sidebar-link').forEach(function (link) {
        link.addEventListener('click', function () {
            if (!isDesktop()) hideSidebar();
        });
    });

    window.addEventListener('resize', function () {
        removeBackdrop();
        document.body.style.overflow = '';

        if (isDesktop()) {
            sidebar.classList.add('active');
            sidebar.classList.remove('inactive');
        } else {
            sidebar.classList.remove('active');
            sidebar.classList.remove('inactive');
        }
    });

    if (isDesktop()) {
        sidebar.classList.add('active');
    }
})();
