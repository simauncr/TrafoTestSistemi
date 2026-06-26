/**
 * Global SweetAlert2 Theme Configuration
 * Soft Lila Kurumsal Tasarım
 */

const styleId = 'swal2-theme-styles';
if (!document.getElementById(styleId)) {
    const style = document.createElement('style');
    style.id = styleId;
    style.textContent = `
        @keyframes swal2-slideInDown {
            0% {
                opacity: 0;
            }
            100% {
                opacity: 1;
            }
        }

        @keyframes swal2-slideOutUp {
            0% {
                opacity: 1;
            }
            100% {
                opacity: 0;
            }
        }

        /* Container & Backdrop */
        .swal2-theme-container,
        .swal2-container {
            z-index: 1055;
            position: absolute !important;
            inset: 0 !important;
            display: flex !important;
            align-items: center !important;
            justify-content: center !important;
            padding: 1rem !important;
        }

        .swal2-container.swal2-backdrop-show,
        .swal2-container.swal2-noanimation.swal2-backdrop-show {
            background: rgba(0, 0, 0, 0.18);
            backdrop-filter: blur(2px);
            pointer-events: auto !important;
        }

        .swal2-container.swal2-backdrop-hide {
            pointer-events: none !important;
            display: none !important;
        }

        /* Modal Popup */
        .swal2-theme-popup,
        .swal2-popup {
            border-radius: 18px !important;
            background: #ffffff !important;
            box-shadow: 0 1.2rem 2.4rem rgba(115, 103, 240, 0.15),
                        0 0.4rem 0.8rem rgba(115, 103, 240, 0.08) !important;
            border: 1px solid rgba(115, 103, 240, 0.1) !important;
            padding: 2rem !important;
            max-width: 440px !important;
            animation: swal2-slideInDown 0.35s ease-out !important;
            margin: 0 !important;
        }

        /* Header & Title */
        .swal2-theme-header,
        .swal2-header {
            margin: 0;
            padding: 0;
        }

        .swal2-theme-title,
        .swal2-title {
            color: #2c2e38;
            font-family: "Public Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
            font-size: 1.25rem;
            font-weight: 700;
            letter-spacing: -0.015em;
            line-height: 1.4;
            margin-bottom: 0.5rem;
            text-align: center;
        }

        /* Content & Text */
        .swal2-theme-content,
        .swal2-html-container {
            color: #6b7280;
            font-family: "Public Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
            font-size: 0.95rem;
            font-weight: 500;
            line-height: 1.6;
            margin-top: 1rem;
            text-align: center;
        }

        .swal2-theme-footer,
        .swal2-footer {
            color: #9ca3af;
            font-size: 0.85rem;
            margin-top: 1rem;
            border-top: 1px solid rgba(115, 103, 240, 0.08);
            padding-top: 1rem;
        }

        /* Icon Styling */
        .swal2-icon {
            margin: 0 auto 1rem;
            width: 5em;
            height: 5em;
            box-sizing: content-box;
        }

        .swal2-icon.swal2-success {
            border-color: #10b981;
            color: #10b981;
        }

        .swal2-icon.swal2-error {
            border-color: #ef4444;
            color: #ef4444;
        }

        .swal2-icon.swal2-warning {
            border-color: #f59e0b;
            color: #f59e0b;
        }

        .swal2-icon.swal2-info {
            border-color: #7367f0;
            color: #7367f0;
        }

        .swal2-icon.swal2-question {
            border-color: #7367f0;
            color: #7367f0;
        }

        .swal2-icon .swal2-icon-content {
            line-height: 1;
        }

        .swal2-icon.swal2-success [class^='swal2-success-circular-line'],
        .swal2-icon.swal2-success .swal2-success-fix,
        .swal2-icon.swal2-success [class^='swal2-success-line'] {
            box-sizing: content-box;
        }

        /* Actions Container */
        .swal2-theme-actions,
        .swal2-actions {
            gap: 0.75rem;
            flex-wrap: wrap;
            justify-content: center;
            margin-top: 1.5rem;
        }

        /* Confirm Button (Success/Save) */
        .swal2-theme-confirm,
        .swal2-confirm {
            background: linear-gradient(135deg, #10b981 0%, #059669 100%) !important;
            color: #ffffff !important;
            border: none !important;
            border-radius: 12px !important;
            padding: 0.65rem 1.5rem !important;
            font-family: "Public Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif !important;
            font-size: 0.95rem !important;
            font-weight: 700 !important;
            cursor: pointer !important;
            box-shadow: 0 0.4rem 0.8rem rgba(16, 185, 129, 0.24) !important;
            transition: all 0.24s cubic-bezier(0.4, 0, 0.2, 1) !important;
            line-height: 1.4 !important;
        }

        .swal2-theme-confirm:hover:not(:disabled),
        .swal2-confirm:hover:not(:disabled) {
            background: linear-gradient(135deg, #059669 0%, #047857 100%) !important;
            box-shadow: 0 0.6rem 1.2rem rgba(16, 185, 129, 0.32) !important;
            transform: translateY(-1px) !important;
        }

        .swal2-theme-confirm:active:not(:disabled),
        .swal2-confirm:active:not(:disabled) {
            transform: translateY(0) !important;
            box-shadow: 0 0.2rem 0.4rem rgba(16, 185, 129, 0.2) !important;
        }

        .swal2-theme-confirm:disabled,
        .swal2-confirm:disabled {
            opacity: 0.6 !important;
            cursor: not-allowed !important;
        }

        /* Cancel/Deny Button (Decline) */
        .swal2-theme-cancel,
        .swal2-theme-deny,
        .swal2-cancel,
        .swal2-deny {
            background: #ffffff !important;
            color: #5a607b !important;
            border: 1.5px solid rgba(115, 103, 240, 0.4) !important;
            border-radius: 12px !important;
            padding: 0.65rem 1.5rem !important;
            font-family: "Public Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif !important;
            font-size: 0.95rem !important;
            font-weight: 600 !important;
            cursor: pointer !important;
            transition: all 0.24s cubic-bezier(0.4, 0, 0.2, 1) !important;
            line-height: 1.4 !important;
            box-shadow: none !important;
        }

        .swal2-theme-cancel:hover:not(:disabled),
        .swal2-theme-deny:hover:not(:disabled),
        .swal2-cancel:hover:not(:disabled),
        .swal2-deny:hover:not(:disabled) {
            background: rgba(115, 103, 240, 0.08) !important;
            border-color: rgba(115, 103, 240, 0.5) !important;
            color: #2c2e38 !important;
        }

        .swal2-theme-cancel:active:not(:disabled),
        .swal2-theme-deny:active:not(:disabled),
        .swal2-cancel:active:not(:disabled),
        .swal2-deny:active:not(:disabled) {
            background: rgba(115, 103, 240, 0.12) !important;
            border-color: rgba(115, 103, 240, 0.6) !important;
        }

        .swal2-theme-cancel:disabled,
        .swal2-theme-deny:disabled,
        .swal2-cancel:disabled,
        .swal2-deny:disabled {
            opacity: 0.5 !important;
            cursor: not-allowed !important;
        }

        /* Alternative Danger/Delete Button */
        .btn-swal-danger {
            background: linear-gradient(135deg, #f87171 0%, #ef4444 100%) !important;
            color: #ffffff !important;
            border: none !important;
            border-radius: 12px !important;
            padding: 0.65rem 1.5rem !important;
            font-family: "Public Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif !important;
            font-size: 0.95rem !important;
            font-weight: 700 !important;
            cursor: pointer !important;
            box-shadow: 0 0.4rem 0.8rem rgba(244, 63, 94, 0.24) !important;
            transition: all 0.24s cubic-bezier(0.4, 0, 0.2, 1) !important;
            line-height: 1.4 !important;
        }

        .btn-swal-danger:hover:not(:disabled) {
            background: linear-gradient(135deg, #ef4444 0%, #dc2626 100%) !important;
            box-shadow: 0 0.6rem 1.2rem rgba(239, 68, 68, 0.32) !important;
            transform: translateY(-1px) !important;
        }

        .btn-swal-danger:active:not(:disabled) {
            transform: translateY(0) !important;
            box-shadow: 0 0.2rem 0.4rem rgba(239, 68, 68, 0.2) !important;
        }

        /* Close Button */
        .swal2-theme-close,
        .swal2-close {
            color: #9ca3af;
            transition: color 0.24s ease-out;
        }

        .swal2-theme-close:hover,
        .swal2-close:hover {
            color: #5a607b;
        }

        /* Animations for hide */
        .swal2.swal2-hide {
            animation: swal2-slideOutUp 0.3s ease-in forwards;
        }

        /* Loading Spinner */
        .swal2-loading {
            border-color: rgba(115, 103, 240, 0.2);
        }

        .swal2-loading::after {
            border-color: #7367f0 transparent #7367f0 transparent;
        }

        /* Input Fields */
        .swal2-input {
            background: rgba(115, 103, 240, 0.04);
            border: 1px solid rgba(115, 103, 240, 0.22);
            border-radius: 10px;
            color: #2c2e38;
            font-family: "Public Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
            font-size: 0.95rem;
            padding: 0.65rem 0.95rem;
            transition: all 0.24s ease-out;
        }

        .swal2-input:focus {
            border-color: rgba(115, 103, 240, 0.5);
            background: rgba(115, 103, 240, 0.06);
            box-shadow: 0 0 0 0.2rem rgba(115, 103, 240, 0.14);
        }

        /* Select & Textarea */
        .swal2-select,
        .swal2-textarea {
            border-radius: 10px;
            border: 1px solid rgba(115, 103, 240, 0.22);
            background: rgba(115, 103, 240, 0.04);
            color: #2c2e38;
            font-family: "Public Sans", -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
        }

        .swal2-select:focus,
        .swal2-textarea:focus {
            border-color: rgba(115, 103, 240, 0.5);
            background: rgba(115, 103, 240, 0.06);
            box-shadow: 0 0 0 0.2rem rgba(115, 103, 240, 0.14);
        }

        /* Checkbox & Radio */
        .swal2-checkbox,
        .swal2-radio {
            accent-color: #7367f0;
        }

        /* Progress Bar */
        .swal2-progress-steps .swal2-progress-step.swal2-active {
            background: #7367f0;
        }

        .swal2-progress-steps .swal2-progress-step {
            background: rgba(115, 103, 240, 0.2);
        }

        .swal2-progress-steps .swal2-progress-step.swal2-success {
            background: #10b981;
        }

        .swal2-progress-steps .swal2-progress-line {
            background: rgba(115, 103, 240, 0.1);
        }

        /* Responsive */
        @media (max-width: 576px) {
            .swal2-theme-popup {
                padding: 1.5rem;
                margin: 1rem;
                width: calc(100% - 2rem);
            }

            .swal2-theme-title {
                font-size: 1.1rem;
            }

            .swal2-theme-content {
                font-size: 0.9rem;
            }

            .swal2-theme-actions {
                flex-direction: column-reverse;
            }

            .swal2-theme-confirm,
            .swal2-theme-cancel,
            .swal2-theme-deny {
                width: 100%;
            }
        }
    `;
    document.head.appendChild(style);
}

if (window.Swal && typeof window.Swal.mixin === 'function') {
    const swalTarget = document.querySelector('.app-main') || document.body;
    window.Swal = window.Swal.mixin({
        target: swalTarget
    });
}
