
export function downloadEsignFillingPdf(dataUrl, filename) {
    const blob = new Blob([dataUrl], { type: 'application/pdf' });
    const blobUrl = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = blobUrl;
    a.download = filename || 'file.pdf';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
    URL.revokeObjectURL(blobUrl);
};