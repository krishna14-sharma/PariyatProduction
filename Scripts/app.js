/* ==========================================================================
   Phoenix Poultry - shared page behaviour (loaded by every master page)
     App.toast(message, kind)   kind: "info" | "success" | "error"
     busy indicator for AJAX (UpdatePanel) and normal postbacks
     highlights the current page in the menu
   ========================================================================== */
(function (window, document) {
    'use strict';

    var App = window.App = window.App || {};
    var busyTimer = null;

    function byId(id) { return document.getElementById(id); }

    function toastHost() {
        var host = byId('appToasts');
        if (!host) {
            host = document.createElement('div');
            host.id = 'appToasts';
            host.className = 'app-toasts';
            host.setAttribute('aria-live', 'polite');
            document.body.appendChild(host);
        }
        return host;
    }

    App.toast = function (message, kind) {
        if (!message) { return; }
        kind = kind || 'info';
        var run = function () {
            var el = document.createElement('div');
            el.className = 'app-toast is-' + kind;
            el.setAttribute('role', kind === 'error' ? 'alert' : 'status');
            var text = document.createElement('div');
            text.className = 'app-toast-text';
            text.textContent = String(message);
            var close = document.createElement('button');
            close.type = 'button';
            close.setAttribute('aria-label', 'Close');
            close.innerHTML = '&times;';
            close.onclick = function () { el.parentNode && el.parentNode.removeChild(el); };
            el.appendChild(text);
            el.appendChild(close);
            toastHost().appendChild(el);
            if (kind !== 'error') {
                window.setTimeout(function () { el.parentNode && el.parentNode.removeChild(el); }, 6000);
            }
        };
        if (document.body) { run(); } else { document.addEventListener('DOMContentLoaded', run); }
    };

    /* ---------------------------------------------------------------- busy indicator */
    App.showBusy = function (blocking) {
        var bar = byId('appProgress');
        if (bar) { bar.classList.add('is-active'); }
        if (blocking) {
            window.clearTimeout(busyTimer);
            busyTimer = window.setTimeout(function () {
                var overlay = byId('appBusy');
                if (overlay) { overlay.classList.add('is-active'); }
            }, 350);
        }
    };

    App.hideBusy = function () {
        window.clearTimeout(busyTimer);
        var bar = byId('appProgress'), overlay = byId('appBusy');
        if (bar) { bar.classList.remove('is-active'); }
        if (overlay) { overlay.classList.remove('is-active'); }
    };

    function hookFullPostbacks() {
        var form = document.forms[0];
        if (!form) { return; }
        form.addEventListener('submit', function () {
            // Report export/print posts into a hidden frame or a new window: no indicator then.
            if (form.target) { return; }
            App.showBusy(false);
            // Safety net for file downloads, where the page itself never reloads.
            window.setTimeout(App.hideBusy, 15000);
        });
        window.addEventListener('pageshow', App.hideBusy);
    }

    function hookAjax() {
        if (!window.Sys || !Sys.WebForms || !Sys.WebForms.PageRequestManager) { return; }
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () { App.showBusy(true); });
        prm.add_endRequest(function (sender, args) {
            App.hideBusy();
            var error = args.get_error();
            if (error) {
                args.set_errorHandled(true);
                var msg = (error.message || '').replace(/^Sys\.WebForms\.PageRequestManager\w*Exception:\s*/, '');
                if (error.httpStatusCode === 0 || /timeout/i.test(error.name || '')) {
                    msg = 'The server did not respond. Please check your connection and try again.';
                }
                App.toast('Something went wrong: ' + msg, 'error');
            }
        });
    }

    /* ---------------------------------------------------------------- menu highlight */
    function highlightMenu() {
        var here = window.location.pathname.toLowerCase();
        var links = document.querySelectorAll('.app-nav a.dropdown-item, .app-nav a.nav-link:not(.dropdown-toggle)');
        for (var i = 0; i < links.length; i++) {
            var a = links[i];
            var path = (a.pathname || '').toLowerCase();
            if (path && path === here) {
                a.classList.add('active');
                var menu = a.closest ? a.closest('.dropdown') : null;
                var toggle = menu ? menu.querySelector('.dropdown-toggle') : null;
                if (toggle) { toggle.classList.add('active'); }
            }
        }
    }

    document.addEventListener('DOMContentLoaded', function () {
        highlightMenu();
        hookFullPostbacks();
    });
    // AutoPostBack controls call __doPostBack(), which does not raise the form "submit" event.
    function hookDoPostBack() {
        var original = window.__doPostBack;
        if (typeof original !== 'function' || original.__appWrapped) { return; }
        var wrapped = function (target, argument) {
            var form = document.forms[0];
            if (form && !form.target) { App.showBusy(false); }
            return original.apply(this, arguments);
        };
        wrapped.__appWrapped = true;
        window.__doPostBack = wrapped;
    }

    // Sys is available once ScriptManager's scripts have loaded (before window.load).
    window.addEventListener('load', function () {
        hookAjax();
        hookDoPostBack();
    });
})(window, document);
