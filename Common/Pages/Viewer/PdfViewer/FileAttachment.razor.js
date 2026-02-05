
export function fileOpen(uploaderClass) {
    const fileUploadButton = document.querySelector(`.${uploaderClass} .e-upload-browse-btn`);
    if (fileUploadButton) {
        fileUploadButton.click();
    }
}

export function downloadPdfFile(filename, base64Data) {
    const link = document.createElement('a');
    link.download = filename;
    link.href = 'data:application/pdf;base64,' + base64Data;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

export function addFileToolbarClass() {
    let elements = ['file_attachment_btn', 'download_btn', 'load_btn'];
    let container = document.querySelector('.control-section.e-pv-file-attachment');
    if (!container) return;
    elements.forEach(id => {
        let toolbarItem = container.querySelector(`#${id}`);
        if (toolbarItem && toolbarItem.firstElementChild) {
            let buttonElement = toolbarItem.firstElementChild;

            buttonElement.classList.add('e-pv-tbar-btn');

            if (buttonElement.firstElementChild) {
                let iconElement = buttonElement.firstElementChild;
                iconElement.classList.remove('e-icons', 'e-btn-icon');
            }
        }
    });

}