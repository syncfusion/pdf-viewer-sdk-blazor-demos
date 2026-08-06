

let closeClickHandler = null;
let searchClickHandler = null;
let textboxInputHandler = null;

export function created() {
    document.getElementById("pdfviewer_open").addEventListener("click", function () {
        document.querySelector(".e-upload-browse-btn").click()
    })
}

export function bindEvents(dotNetRef) {
    const textbox = document.getElementById("textbox");
    if (!textbox) {
        return;
    }
    const wrapper = textbox.closest(".e-input-group");
    if (!wrapper) {
        return;
    }
    const searchIcon = wrapper.querySelector(".e-search");
    const closeIcon = wrapper.querySelector(".e-close");

    if (closeIcon) {
        // Remove previous binding if any
        if (closeClickHandler) {
            closeIcon.removeEventListener("click", closeClickHandler);
        }
        closeClickHandler = () => {
            dotNetRef.invokeMethodAsync("OnSearchCloseIconClick");
        };

        closeIcon.addEventListener("click", closeClickHandler);
    }

    if (searchIcon) {
        // Remove previous binding if any
        if (searchClickHandler) {
            searchIcon.removeEventListener("click", searchClickHandler);
        }
        searchClickHandler = () => {
            dotNetRef.invokeMethodAsync("OnSearchIconClick");
        };
        searchIcon.addEventListener("click", searchClickHandler);
    }
}
    
export function unbindEvents() {

    const textbox = document.getElementById("textbox");
    if (!textbox) {
        return;
    }
    const wrapper = textbox.closest(".e-input-group");
    if (!wrapper) {
        return;
    }
    const searchIcon = wrapper.querySelector(".e-search");
    const closeIcon = wrapper.querySelector(".e-close");
    if (closeIcon && closeClickHandler) {
        closeIcon.removeEventListener("click", closeClickHandler);
        closeClickHandler = null;
    }
    if (searchIcon && searchClickHandler) {
        searchIcon.removeEventListener("click", searchClickHandler);
        searchClickHandler = null;
    }
}

export function toggleSearchIcons(showClose) {
    const textbox = document.getElementById("textbox");
    if (!textbox) {
        return;
    }
    const wrapper = textbox.closest(".e-input-group");
    if (!wrapper) {
        return;
    }
    const searchIcon = wrapper.querySelector(".e-search");
    const closeIcon = wrapper.querySelector(".e-close");
    if (searchIcon) {
        searchIcon.style.display = showClose ? "none" : "flex";
    }
    if (closeIcon) {
        closeIcon.style.display = showClose ? "flex" : "none";
    }
};

export function clearInputBox() {
    const textbox = document.getElementById("textbox");
    if (!textbox) {
        return;
    }
    else {
        textbox.value = '';
        textbox.blur();
    }
}

export function wireSearchTextbox(textboxId) {
    const textbox = document.getElementById(textboxId);
    if (textboxInputHandler) {
        textbox.removeEventListener("input", textboxInputHandler);
    }
    textboxInputHandler = function () {
        const searchIcon = textbox.parentElement.querySelector('.e-search');
        const closeIcon = textbox.parentElement.querySelector('.e-close');

        closeIcon.style.display = 'none';
        searchIcon.style.display = 'flex';
    };
    textbox.addEventListener("input", textboxInputHandler);
}


export function unwireSearchTextbox(textboxId) {
    const textbox = document.getElementById(textboxId);
    if (textbox && textboxInputHandler) {
        textbox.removeEventListener("input", textboxInputHandler);
        textboxInputHandler = null;
    }
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