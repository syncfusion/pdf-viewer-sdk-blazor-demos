
export function created() {
    document.getElementById("pdfviewer_open").addEventListener("click", function () {
        document.querySelector(".e-upload-browse-btn").click()
    })
}

export function getInstance(tooltipInstance) {
    currentURL = window.location.href;
    dotnetTooltipRef = tooltipInstance;
}

export function closeTooltipPopup(methodName) {
    const scrollableDiv = document.getElementById("right-pane");
    function scrollHandler() {
        if (currentURL !== window.location.href) {
            scrollableDiv.removeEventListener("scroll", scrollHandler);
            dotnetTooltipRef = null;
        }
        if (dotnetTooltipRef) {
            dotnetTooltipRef.invokeMethodAsync(methodName);
        }
    };
    scrollableDiv.addEventListener("scroll", scrollHandler);
}

export function disposeDotnetTooltipRef() {
    dotnetTooltipRef = null;
}

export function changeFocus() {
    const activeElement = document.activeElement;
    if (activeElement) {
        activeElement.blur();
    }
    var pdfViewerContainer = document.getElementById('pdfviewer_section');
    if (pdfViewerContainer) {
        pdfViewerContainer.focus();
    }
}

export function mapSearchValue(viewerId) {
    var customSearchInput = document.getElementById('textbox');
    var pdfViewerSearchInput = document.getElementById(viewerId + '_search_input');

    if (customSearchInput !== null && pdfViewerSearchInput !== null) {
        customSearchInput.addEventListener('input', () => {
            pdfViewerSearchInput.value = customSearchInput.value;
        });
    }
}