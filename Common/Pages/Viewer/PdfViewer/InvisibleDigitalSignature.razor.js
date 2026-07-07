
export function created() {
    document.getElementById('pdfviewer_open').addEventListener('click', function () {
        document.querySelector('.e-upload-browse-btn').click();
    });
}