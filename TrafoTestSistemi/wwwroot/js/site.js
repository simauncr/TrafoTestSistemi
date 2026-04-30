(function () {
	const root = document.documentElement;
	const appShell = document.querySelector('.app-shell');
	const themeToggle = document.querySelector('[data-theme-toggle]');
	const themeIcon = document.querySelector('[data-theme-icon]');
	const mobileOpen = document.querySelector('[data-sidebar-mobile-open]');
	const mobileClose = document.querySelector('[data-sidebar-mobile-close]');
	const backdrop = document.querySelector('[data-sidebar-backdrop]');
	const sidebarToggles = document.querySelectorAll('[data-sidebar-toggle]');

	const themeStorageKey = 'app-theme';
	const sidebarStorageKey = 'app-sidebar-collapsed';

	function applyTheme(theme) {
		root.setAttribute('data-bs-theme', theme);
		if (!themeIcon) {
			return;
		}

		themeIcon.classList.toggle('fa-moon', theme !== 'dark');
		themeIcon.classList.toggle('fa-sun', theme === 'dark');
	}

	function getInitialTheme() {
		const saved = localStorage.getItem(themeStorageKey);
		if (saved === 'light' || saved === 'dark') {
			return saved;
		}

		return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
	}

	function setCollapsedState(isCollapsed) {
		if (!appShell) {
			return;
		}

		appShell.classList.toggle('sidebar-collapsed', isCollapsed);
		localStorage.setItem(sidebarStorageKey, isCollapsed ? '1' : '0');
	}

	function setSidebarOpen(isOpen) {
		if (!appShell) {
			return;
		}

		appShell.classList.toggle('sidebar-open', isOpen);
	}

	applyTheme(getInitialTheme());

	if (appShell) {
		setCollapsedState(localStorage.getItem(sidebarStorageKey) === '1');
	}

	if (themeToggle) {
		themeToggle.addEventListener('click', function () {
			const next = root.getAttribute('data-bs-theme') === 'dark' ? 'light' : 'dark';
			localStorage.setItem(themeStorageKey, next);
			applyTheme(next);
		});
	}

	window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', function (event) {
		if (!localStorage.getItem(themeStorageKey)) {
			applyTheme(event.matches ? 'dark' : 'light');
		}
	});

	sidebarToggles.forEach(function (button) {
		button.addEventListener('click', function () {
			setCollapsedState(!appShell.classList.contains('sidebar-collapsed'));
		});
	});

	if (mobileOpen) {
		mobileOpen.addEventListener('click', function () {
			setSidebarOpen(true);
		});
	}

	if (mobileClose) {
		mobileClose.addEventListener('click', function () {
			setSidebarOpen(false);
		});
	}

	if (backdrop) {
		backdrop.addEventListener('click', function () {
			setSidebarOpen(false);
		});
	}
})();
