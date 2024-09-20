// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Set the width of the side navigation to 250px */
function openNav() {
    document.getElementById("mySidenav").style.width = "250px"; // Abre a sidebar
    $(".navMobile").addClass("d-none");
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0"; // Fecha a sidebar
    $(".navMobile").removeClass("d-none");
}

$(document).ready(function () {
    // navbar transparente/preta
    var lastScrollTop = 0;
    $(window).on("scroll", function () {
        var scrollTop = $(this).scrollTop();

        if (scrollTop > lastScrollTop) {
            // rolagem para baixo
            $(".navbar").removeClass("bg-transparent").addClass("bg-black");
        } else {
            // rolagem para cima
            if (scrollTop <= 0) {
                $(".navbar").removeClass("bg-black").addClass("bg-transparent");
            }
        }
        lastScrollTop = scrollTop;
    });

    $(".btn-cadastro").click(function () {

        var cursosSelecionados = [];

        // Captura os valores dos checkboxes selecionados
        $("input[type=checkbox]:checked").each(function () {
            cursosSelecionados.push($(this).val());
        });

        // Converte o array em uma string, separando por vírgula
        var cursosFormatados = cursosSelecionados.join(", ");

        // Agora você pode enviar essa string formatada para o controller
        $.ajax({
            url: "/Usuario/CadastrarProcessar",
            type: "POST",
            data: {
                Cursos: cursosFormatados,
                // Adicione outros dados do formulário aqui
            },
            success: function (response) {
                console.log("Dados enviados com sucesso!");
            }
        });
    });
})

