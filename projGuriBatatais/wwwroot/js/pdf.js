// URL do PDF que você quer carregar
var url = '@Url.Content("~/Arquivos/61-Game of Thrones.pdf")';

// Inicializando o PDF.js
var pdfjsLib = window['pdfjs-dist/build/pdf'];

// Definindo o caminho do PDF.js worker
pdfjsLib.GlobalWorkerOptions.workerSrc = 'https://cdnjs.cloudflare.com/ajax/libs/pdf.js/2.10.377/pdf.worker.min.js';

// Carregar o PDF
pdfjsLib.getDocument(url).promise.then(function (pdf) {
    console.log('PDF carregado.');

    // Carregar a primeira página
    pdf.getPage(1).then(function (page) {
        console.log('Página carregada.');

        var scale = 1.5;  // Definir a escala da página
        var viewport = page.getViewport({ scale: scale });

        // Preparar o canvas usando as dimensões da página
        var canvas = document.getElementById('pdf-canvas');
        var context = canvas.getContext('2d');
        canvas.height = viewport.height;
        canvas.width = viewport.width;

        // Renderizar a página no canvas
        var renderContext = {
            canvasContext: context,
            viewport: viewport
        };
        page.render(renderContext);
    });
});
