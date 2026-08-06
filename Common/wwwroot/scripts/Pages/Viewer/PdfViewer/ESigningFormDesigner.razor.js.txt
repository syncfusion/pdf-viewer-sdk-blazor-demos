
export function getInstance(tooltipInstance) {
    currentURL = window.location.href;
    dotnetTooltipRef = tooltipInstance;
}

export function disposeDotnetTooltipRef() {
    dotnetTooltipRef = null;
}

export function documentLoaded() {
    let buttons = document.querySelectorAll(".e-pv-e-sign-form-field-property");
    buttons.forEach(button => {
        button.addEventListener("mousedown", handleMouseDown);
        button.addEventListener("touchstart", handleMouseDown, { passive: false });
    });
}