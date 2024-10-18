$(document).ready(function () {
    $(".btn-cadastro").click(function () {
        event.preventDefault();

        // pegando os dados do formulário
        var nomeCompleto = $("#nomeCompleto").val();
        var nomeUsuario = $("#nomeUsuario").val();
        var senha = $("#senha").val();
        var tipoUsuario = $("input[type=radio][name='Tipo']:checked").val();
        var chaveAcesso = $("#chaveAcesso").val();
        var chaveA;
        var chaveP;
        var chaveC;
        var texto = encode_utf8("usuário");

        var cursosSelecionados = [];

        // captura os valores dos checkboxes selecionados
        $("input[type=checkbox]:checked").each(function () {
            cursosSelecionados.push($(this).val());
        });

        // separando por vírgula
        var cursosFormatados = cursosSelecionados.join(", ");

        function encode_utf8(s) {
            return unescape(encodeURIComponent(s));
        }

        function decode_utf8(s) {
            return decodeURIComponent(escape(s));
        }

        if (nomeCompleto) {
            if (nomeUsuario) {
                validarNomeUsuarioServidor(nomeUsuario);
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Nome de usu&aacute;rio em branco!",
                    text: "Insira o nome de usuário.",
                    confirmButtonColor: "#00D7AA",
                }).then((result) => {
                    $("#nomeUsuario").addClass("is-invalid");
                    $("#nomeCompleto").removeClass("is-invalid");
                    $("#senha").removeClass("is-invalid");
                    $("#chaveAcesso").removeClass("is-invalid");
                })
            }
        } else {
            Swal.fire({
                icon: "error",
                title: "Nome completo em branco!",
                text: "Insira seu nome completo.",
                confirmButtonColor: "#f17272",
            }).then((result) => {
                $("#nomeCompleto").addClass("is-invalid");
                $("#nomeUsuario").removeClass("is-invalid");
                $("#senha").removeClass("is-invalid");
                $("#chaveAcesso").removeClass("is-invalid");
            });
        }

        function validarNomeUsuarioServidor(nomeUsuario) {
            $.ajax({
                url: "/Usuario/ValidarNomeUsuario", // URL que vai para o controller
                type: "GET",
                data: { nomeUsuario: nomeUsuario }, // Envia o nome de usuário para validação
                success: function (response) {
                    if (response.usuarioExiste) {
                        Swal.fire({
                            icon: "error",
                            title: "Nome de usu&aacute;rio j&aacute; existente!",
                            text: "Escolha outro nome de usu&aacute;rio.",
                            confirmButtonColor: "#00D7AA",
                        }).then((result) => {
                            $("#nomeUsuario").addClass("is-invalid");
                        });
                    } else {
                        if (senha) {
                            if (senha.length < 8) {
                                Swal.fire({
                                    icon: "error",
                                    title: "Senha inválida!",
                                    text: "A senha precisa possuir 8 caracteres.",
                                    confirmButtonColor: "#00D7AA",
                                }).then((result) => {
                                    $("#senha").addClass("is-invalid");
                                    $("#nomeUsuario").removeClass("is-invalid");
                                    $("#chaveAcesso").removeClass("is-invalid");
                                    $("#nomeCompleto").removeClass("is-invalid");
                                })
                            } else {
                                tipoUsuario ?
                                    chaveAcesso ?
                                        validarChaveAcesso(chaveAcesso)
                                        : Swal.fire({
                                            icon: "error",
                                            title: "Chave de acesso em branco!",
                                            text: "Insira a chave de acesso.",
                                            confirmButtonColor: "#00D7AA",
                                        }).then((result) => {
                                            $("#chaveAcesso").addClass("is-invalid");
                                            $("#nomeUsuario").removeClass("is-invalid");
                                            $("#senha").removeClass("is-invalid");
                                            $("#nomeCompleto").removeClass("is-invalid");
                                        })
                                    : Swal.fire({
                                        icon: "error",
                                        title: "Usu&aacute;rio não selecionado!",
                                        text: "Escolha o tipo de usu&aacute;rio.",
                                        confirmButtonColor: "#00D7AA",
                                    }).then((result) => {
                                        $("#nomeCompleto").removeClass("is-invalid");
                                        $("#nomeUsuario").removeClass("is-invalid");
                                        $("#senha").removeClass("is-invalid");
                                        $("#chaveAcesso").removeClass("is-invalid");
                                    })
                            }
                        } else {
                            Swal.fire({
                                icon: "error",
                                title: "Senha em branco!",
                                text: "Insira a senha.",
                                confirmButtonColor: "#00D7AA",
                            }).then((result) => {
                                $("#senha").addClass("is-invalid");
                                $("#nomeUsuario").removeClass("is-invalid");
                                $("#nomeCompleto").removeClass("is-invalid");
                                $("#chaveAcesso").removeClass("is-invalid");
                            })
                        }
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire({
                        icon: "error",
                        title: "Erro ao validar nome de usu&aacute;rio!",
                        text: "Tente novamente mais tarde.",
                        confirmButtonColor: "#f17272",
                    });
                }
            });
        }

        function validarChaveAcesso(chave) {
            $.ajax({
                url: "/api/ChaveAcesso/todas",
                method: "GET",
                dataType: "json",
                cache: false,
                success: function (data) {
                    console.log("A requisição foi bem-sucedida");  // Verifique se este log aparece
                    console.log(data);  // Verifique os dados retornados
                    data.forEach(function (item) {
                        chaveA = item.chaveAluno;
                        chaveP = item.chaveProfessor;
                        chaveC = item.chaveCoordenacao;
                    });

                    if ((tipoUsuario == "Aluno" && chave == chaveA) ||
                        (tipoUsuario == "Professor" && chave == chaveP) ||
                        (tipoUsuario == "Coordenacao" && chave == chaveC)) {
                        $.ajax({
                            url: "/Usuario/CadastrarProcessar",
                            type: "POST",
                            data: {
                                NomeCompleto: nomeCompleto,
                                NomeUsuario: nomeUsuario,
                                Senha: senha,
                                Cursos: cursosFormatados,
                                Tipo: tipoUsuario
                            },
                            success: function (response) {
                                Swal.fire({
                                    icon: "success",
                                    title: "Cadastro realizado!",
                                    text: "Dados enviados com sucesso.",
                                    confirmButtonColor: "#f17272",
                                });
                            },
                        });
                    } else {
                        Swal.fire({
                            icon: "error",
                            title: "Chave de acesso incorreta!",
                            text: "Consulte novamente a chave de acesso e a insira corretamente.",
                            confirmButtonColor: "#f17272",
                        });
                    }
                },
                error: function (xhr, status, error) {
                    Swal.fire({
                        icon: "error",
                        title: "Erro ao enviar os dados",
                        text: "Tente novamente mais tarde.",
                        confirmButtonColor: "#f17272",
                    });
                }
            });
        }
    });

    $(".btn-login").click(function (event) {
        event.preventDefault();

        var nomeUsuario = $("#nomeUsuarioLogin").val();
        var senha = $("#senhaLogin").val();

        // Verifica se os campos estão vazios
        if (!nomeUsuario || !senha) {
            Swal.fire({
                icon: "warning",
                title: "Campos vazios",
                text: "Por favor, preencha todos os campos.",
                confirmButtonColor: "#f17272",
            });
            return; // Se os campos estão vazios, não prosseguir com o envio
        }

        // Faz a requisição AJAX
        $.ajax({
            url: "/Usuario/LoginProcessar",
            type: "POST",
            data: {
                NomeUsuario: nomeUsuario,
                Senha: senha,
            },
            success: function (response) {
                if (response.success) {
                    // Se o login for bem-sucedido, redireciona para a página correta
                    window.location.href = response.redirectUrl;
                } else {
                    // Se o login falhou, exibe uma mensagem de erro
                    Swal.fire({
                        icon: "error",
                        title: "Erro",
                        text: "Nome de usuário ou senha incorretos.",
                        confirmButtonColor: "#f17272",
                    }).then((result) => {
                        // Adiciona classes de erro nos campos
                        $("#nomeUsuarioLogin").addClass("is-invalid");
                        $("#senhaLogin").addClass("is-invalid");
                    });
                }
            },
            error: function (xhr, status, error) {
                // Exibe uma mensagem de erro geral se houver problemas na requisição AJAX
                Swal.fire({
                    icon: "error",
                    title: "Erro ao processar o login",
                    text: "Tente novamente mais tarde.",
                    confirmButtonColor: "#f17272",
                });
            }
        });
    });



    $(".link-login").click(function () {
        $(".cadastro").addClass("d-none");
        $(".login").removeClass("d-none");
    });

    $(".link-cadastro").click(function () {
        $(".login").addClass("d-none");
        $(".cadastro").removeClass("d-none");
    });
});
