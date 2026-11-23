(function () {
    'use strict';

    function normalizedValue(element) {
        return (element.value || element.textContent || '')
            .trim()
            .toLocaleLowerCase('es');
    }

    function decorateButtons(root) {
        const controls = root.querySelectorAll('input[type="submit"], button[type="submit"], button[type="button"]');

        controls.forEach(function (control) {
            if (control.classList.contains('tm-logout-btn') ||
                control.classList.contains('burger-btn') ||
                control.classList.contains('sidebar-hide')) {
                return;
            }

            const value = normalizedValue(control);
            if (!value) return;

            control.classList.add('tm-btn');

            if (/(borrar|eliminar|desactivar)/.test(value)) {
                control.classList.add('tm-btn-danger');
            } else if (/(cancelar|limpiar|cerrar)/.test(value)) {
                control.classList.add('tm-btn-neutral');
            } else if (/(editar)/.test(value)) {
                control.classList.add('tm-btn-secondary');
            } else if (/(descargar|exportar)/.test(value)) {
                control.classList.add('tm-btn-success');
            } else {
                control.classList.add('tm-btn-primary');
            }

            if (!control.hasAttribute('title')) {
                control.setAttribute('title', control.value || control.textContent.trim());
            }

            control.setAttribute('data-bs-toggle', 'tooltip');
            control.setAttribute('data-bs-placement', 'top');
            control.setAttribute('data-bs-custom-class', 'tm-tooltip');
        });
    }

    function decorateSearch(root) {
        root.querySelectorAll('input[id^="fl"], input.tm-search-source').forEach(function (input) {
            const type = (input.getAttribute('type') || 'text').toLowerCase();
            if (type === 'hidden' || type === 'submit') return;
            input.classList.add('tm-search');
        });
    }

    function nextControl(label) {
        let node = label.nextElementSibling;
        let steps = 0;

        while (node && steps < 5) {
            if (node.matches('input, select, textarea')) return node;
            if (node.matches('br, img, span')) {
                node = node.nextElementSibling;
                steps += 1;
                continue;
            }
            break;
        }

        return null;
    }

    function decorateFields(root) {
        root.querySelectorAll('form label').forEach(function (label) {
            if (label.closest('.popup') || label.closest('.tm-toggle-row') || label.closest('.tm-login-card')) return;
            if (label.closest('.tm-form-field')) return;

            const control = label.htmlFor
                ? document.getElementById(label.htmlFor)
                : nextControl(label);

            if (!control || !root.contains(control)) return;

            const tag = control.tagName.toLowerCase();
            const type = (control.getAttribute('type') || '').toLowerCase();

            if (type === 'hidden' || type === 'submit' || type === 'button' || type === 'checkbox' || type === 'radio') {
                return;
            }

            if (type === 'file') {
                control.classList.add('tm-file-input');
                return;
            }

            if (!['input', 'select', 'textarea'].includes(tag)) return;
            if (control.classList.contains('tm-search')) return;

            if (control.id && !label.htmlFor) label.htmlFor = control.id;

            const wrapper = document.createElement('div');
            wrapper.className = 'tm-form-field';
            label.parentNode.insertBefore(wrapper, label);
            wrapper.appendChild(label);
            wrapper.appendChild(control);

            label.classList.add('tm-field-label');
            control.classList.add('tm-form-control');
        });

        root.querySelectorAll('input[type="file"]').forEach(function (input) {
            input.classList.add('tm-file-input');
        });
    }

    function decorateBooleanControls(root) {
        root.querySelectorAll('input[type="checkbox"]').forEach(function (input) {
            if (input.classList.contains('tm-toggle-input') || input.classList.contains('tm-checkbox-input')) return;

            const identity = ((input.id || '') + ' ' + (input.name || '')).toLowerCase();
            let label = input.id ? root.querySelector('label[for="' + CSS.escape(input.id) + '"]') : null;
            if (!label && input.nextElementSibling && input.nextElementSibling.tagName === 'LABEL') {
                label = input.nextElementSibling;
            }
            if (!label && input.previousElementSibling && input.previousElementSibling.tagName === 'LABEL') {
                label = input.previousElementSibling;
            }

            const labelText = label ? label.textContent.toLowerCase() : '';
            const isState = identity.includes('estado') || labelText.includes('activo') || labelText.includes('estado') || labelText.includes('habilitado');

            if (!isState) {
                input.classList.add('tm-checkbox-input');
                return;
            }

            const parent = input.parentElement;
            if (!parent || parent.classList.contains('tm-toggle-row')) return;

            const row = document.createElement('label');
            row.className = 'tm-toggle-row';
            if (input.id) row.htmlFor = input.id;

            parent.insertBefore(row, input);
            row.appendChild(input);
            input.classList.add('tm-toggle-input');

            const track = document.createElement('span');
            track.className = 'tm-toggle-track';
            track.setAttribute('aria-hidden', 'true');

            const handle = document.createElement('span');
            handle.className = 'tm-toggle-handle';
            track.appendChild(handle);
            row.appendChild(track);

            const text = document.createElement('span');
            text.className = 'tm-toggle-text';
            text.textContent = label ? label.textContent.trim() : 'Activo';
            row.appendChild(text);

            if (label && label !== row) label.remove();
        });
    }

    function decorateRadios(root) {
        root.querySelectorAll('input[type="radio"]').forEach(function (input) {
            const label = input.id ? root.querySelector('label[for="' + CSS.escape(input.id) + '"]') : input.closest('label');
            if (label) label.classList.add('tm-radio-option');
        });
    }

    function decorateConfirmations(root) {
        root.querySelectorAll('.popup').forEach(function (popup) {
            const card = popup.querySelector('.pp_del_inner, .pp_ms_inner');
            if (!card) return;

            const destructive = Array.from(popup.querySelectorAll('input[type="submit"], button'))
                .some(function (button) {
                    return /(borrar|eliminar|desactivar)/.test(normalizedValue(button));
                });

            card.classList.toggle('tm-message-card', !destructive);

            if (!card.querySelector('.tm-confirmation-icon')) {
                const icon = document.createElement('div');
                icon.className = 'tm-confirmation-icon';
                icon.textContent = destructive ? '!' : 'i';
                icon.setAttribute('aria-hidden', 'true');
                card.insertBefore(icon, card.firstChild);
            }
        });
    }

    function createLoader() {
        if (document.getElementById('tmLoaderOverlay')) return document.getElementById('tmLoaderOverlay');

        const overlay = document.createElement('div');
        overlay.id = 'tmLoaderOverlay';
        overlay.className = 'tm-loader-overlay';
        overlay.setAttribute('aria-hidden', 'true');

        const box = document.createElement('div');
        box.className = 'tm-loader-box';
        box.setAttribute('role', 'status');
        box.setAttribute('aria-label', 'Procesando');

        for (let i = 0; i < 3; i += 1) {
            const bar = document.createElement('span');
            bar.className = 'tm-loader-bar';
            box.appendChild(bar);
        }

        overlay.appendChild(box);
        document.body.appendChild(overlay);
        return overlay;
    }

    function installLoader(root) {
        const overlay = createLoader();

        root.querySelectorAll('form').forEach(function (form) {
            if (form.classList.contains('tm-logout-form')) return;

            form.addEventListener('submit', function (event) {
                const submitter = event.submitter;
                const action = normalizedValue(submitter || document.createElement('button'));

                if (/(cancelar|limpiar|descargar|exportar)/.test(action)) return;

                if (form.dataset.tmSubmitting === 'true') {
                    event.preventDefault();
                    return;
                }

                form.dataset.tmSubmitting = 'true';
                overlay.classList.add('is-visible');
                overlay.setAttribute('aria-hidden', 'false');
            });
        });

        window.addEventListener('pageshow', function () {
            document.querySelectorAll('form[data-tm-submitting="true"]').forEach(function (form) {
                form.dataset.tmSubmitting = 'false';
            });
            overlay.classList.remove('is-visible');
            overlay.setAttribute('aria-hidden', 'true');
        });
    }

    function initTooltips(root) {
        if (!window.bootstrap || !window.bootstrap.Tooltip) return;
        root.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (element) {
            if (!window.bootstrap.Tooltip.getInstance(element)) {
                new window.bootstrap.Tooltip(element);
            }
        });
    }

    function organizeCrudShell(root) {
        root.querySelectorAll('.tm-crud-shell').forEach(function (shell) {
            if (!shell.querySelector(':scope > .tm-crud-toolbar')) {
                const candidates = Array.from(shell.children).filter(function (element) {
                    return element.matches('input[type="submit"], input.tm-search');
                });

                if (candidates.length) {
                    const toolbar = document.createElement('div');
                    toolbar.className = 'tm-crud-toolbar';
                    shell.insertBefore(toolbar, candidates[0]);
                    candidates.forEach(function (element) {
                        toolbar.appendChild(element);
                    });
                }
            }

            Array.from(shell.children).forEach(function (element) {
                if (element.matches('.containers_vn, .popup, .tm-crud-toolbar, .tm-page-header, .tm-editor-card')) return;
                if (element.tagName === 'DIV' && element.querySelector(':scope > h3')) {
                    element.classList.add('tm-editor-card');
                }
            });
        });
    }

    function markSharedSurfaces(root) {
        root.querySelectorAll('form > .text-center').forEach(function (element) {
            element.classList.add('tm-crud-shell');
        });

        root.querySelectorAll('h1.display-4').forEach(function (heading) {
            heading.classList.add('tm-page-heading');
        });

        root.querySelectorAll('.containers_vn').forEach(function (container) {
            container.classList.add('tm-table-card');
        });

        root.querySelectorAll('.tb_data').forEach(function (table) {
            table.classList.add('tm-data-table');
        });
    }

    document.addEventListener('DOMContentLoaded', function () {
        const root = document.querySelector('.tm-main-content') || document.querySelector('.tm-public-main');
        if (!root) return;

        markSharedSurfaces(root);
        decorateSearch(root);
        organizeCrudShell(root);
        decorateFields(root);
        decorateBooleanControls(root);
        decorateRadios(root);
        decorateButtons(root);
        decorateConfirmations(root);
        installLoader(root);
        initTooltips(root);
    });
})();
