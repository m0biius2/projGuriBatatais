$(document).ready(function(){
            $("#show-scale").click(function(){
                var notes = ["D", "E", "F#", "G", "A", "B", "C#", "D"];
                $("#scale-container").empty(); // Limpa o container antes de mostrar as notas
                $.each(notes, function(index, note){
                    $("#scale-container").append("<div class='note'>" + note + "</div>");
                });
            });
        });