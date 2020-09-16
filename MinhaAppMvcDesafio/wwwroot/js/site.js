function AjaxModal()
{
    $(document).ready(function ()
    {
        $(function ()
        {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e)
                {
                    $('#myModalContent').load(this.href,
                        function ()
                        {
                            $('#myModal').modal({ keyboard: true }, 'show');
                            bindForm(this);
                        });
                    return false;
                });
        });

        function bindForm(dialog)
        {
            $('form', dialog).submit(function ()
            {
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result)
                    {
                        if (result.success)
                        {
                            $('#myModal').modal('hide');
                            $('#EnderecoTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                        }
                        else
                        {
                            $('#myModalContent').html(result);
                            bindForm(dialog);
                        }
                    }
                });
                return false;
            });
        }
    });
}

function BuscaCep()
{
    $(document).ready(function ()
    {
        function limpa_formulário_cep()
        {
            // Limpa valores do formulário de cep.
            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Endereco_Cep").blur(function ()
        {
            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "")
            {
                //Expressão regular para validar o CEP.
                var validacep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (validacep.test(cep))
                {
                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados)
                        {
                            if (!("erro" in dados))
                            {
                                //Atualiza os campos com os valores da consulta.
                                $("#Endereco_Logradouro").val(dados.logradouro.toUpperCase());
                                $("#Endereco_Bairro").val(dados.bairro.toUpperCase());
                                $("#Endereco_Cidade").val(dados.localidade.toUpperCase());
                                $("#Endereco_Estado").val(dados.uf.toUpperCase());
                            } //end if.
                            else
                            {
                                //CEP pesquisado não foi encontrado.
                                limpa_formulário_cep();
                                alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else
                {
                    //cep é inválido.
                    limpa_formulário_cep();
                    alert("Formato de CEP inválido.");
                }
            } //end if.
            else
            {
                //cep sem valor, limpa formulário.
                limpa_formulário_cep();
            }
        });
    });
}

function RedefinirMenu()
{

}

function MontarPaginacao(pagina)
{
    var paginas       = parseInt($("#QtdPaginas").val());
    var pagina        = parseInt(pagina);
    var pagina_atual  = parseInt((pagina / 100).toFixed(0));
    var view_index    = $("#ViewIndex").val();
    var link_anterior = (pagina_atual == 1) ? "#" : "../lista-" + view_index + "/" + (pagina_atual - 1) + $("#OrdCampo").val() + $("#OrdSentido").val();
    var link_proximo  = (pagina_atual == paginas) ? "#" : "../lista-" + view_index + "/" + (pagina_atual + 1) + $("#OrdCampo").val() + $("#OrdSentido").val();
    var paginacao     = "";
    paginacao += "<div class='divPaginacao'>";
    paginacao += "<div class='pagination'>";
    paginacao += "<a href='" + link_anterior + "'>&laquo;</a>";
    for (var i = 1; i <= paginas; i++)
    {
        if (i == pagina_atual)
            paginacao += "<a href='#' class='active'>" + i + "</a>";
        else
            paginacao += "<a href='../lista-" + view_index + "/" + (i + $("#OrdCampo").val() + $("#OrdSentido").val()) + "' class=''>" + i + "</a>";
    }
    paginacao += "<a href='" + link_proximo + "'>&raquo;</a>";
    paginacao += "</div>";
    paginacao += "</div>";
    $("#div_paginacao").html(paginacao);
}

function MontarOrdenacao()
{

}

function MascaraMoeda(objTextBox, e)
{
    var SeparadorMilesimo = '.';                    // Caracter separador de milésimos
    var SeparadorDecimal = ',';                     // Caracter separador de decimais   
    var sep = 0;
    var key = '';
    var i = j = 0;
    var len = len2 = 0;
    var strCheck = '0123456789';
    var aux = aux2 = '';
    var whichCode = (window.Event) ? e.which : e.keyCode;
    if (whichCode == 13) return true;
    key = String.fromCharCode(whichCode);           // Valor para o código da Chave
    if (strCheck.indexOf(key) == -1) return false;  // Chave inválida
    len = objTextBox.value.length;
    for (i = 0; i < len; i++)
        if ((objTextBox.value.charAt(i) != '0') && (objTextBox.value.charAt(i) != SeparadorDecimal)) break;
    aux = '';
    for (; i < len; i++)
        if (strCheck.indexOf(objTextBox.value.charAt(i)) != -1) aux += objTextBox.value.charAt(i);
    aux += key;
    len = aux.length;
    if (len == 0) objTextBox.value = '';
    if (len == 1) objTextBox.value = '0' + SeparadorDecimal + '0' + aux;
    if (len == 2) objTextBox.value = '0' + SeparadorDecimal + aux;
    if (len > 2) {
        aux2 = '';
        for (j = 0, i = len - 3; i >= 0; i--) {
            if (j == 3) {
                aux2 += SeparadorMilesimo;
                j = 0;
            }
            aux2 += aux.charAt(i);
            j++;
        }
        objTextBox.value = '';
        len2 = aux2.length;
        for (i = len2 - 1; i >= 0; i--)
            objTextBox.value += aux2.charAt(i);
        objTextBox.value += SeparadorDecimal + aux.substr(len - 2, len);
    }
    return false;
}

$(document).ready(function ()
{
    RedefinirMenu();
    if ($("#Pagina").val() !== undefined && $("#Pagina").val() != "")
        MontarPaginacao($("#Pagina").val());
});