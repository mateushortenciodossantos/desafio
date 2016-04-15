$(function () {
    api.addEvents();
});

var api = {
    addEvents: function () {
        $("#buscarObj").click(function () {
            api.listarObjetos();
        });
    },
    listarObjetos: function () {
        $.ajax({
            type: "GET",
            url: "http://localhost:55642/v1/guitarras",
            dataType: "JSON",
            success: function (objetos) {
                var html = "";
                for (var i = 0; objetos.length > i; i++) {
                    html += buildTemplate(objetos[i]);
                }
                $("#json-objects").append(html);
            }
        });
    }
}

function buildTemplate(objeto) {
    var html = "";
    html += "<p>id: " + objeto.id + "<br/>";
    html += "nome: " + objeto.nome + "<br/>";
    html += "preço: " + objeto.preco + "<br/>";
    html += "descrição: " + objeto.descricao + "<br/>";
    html += "data de inclusão: " + objeto.dataInclusao + "<br/>";
    html += "url da imagem: " + objeto.urlImagem + "<br/>";
    html += "sku: " + objeto.sku + "</p><br/><br/>";   
    return html;
}