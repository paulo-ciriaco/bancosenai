const URL_API = 'https://localhost:7081/api/v1/Documento';

async function enviarDocumento() {
    const codigoCliente = document.getElementById("codigoCliente").value;
    const inputArquivo = document.getElementById("arquivo");
    const arquivo = inputArquivo.files[0];

    if (!codigoCliente || !arquivo) {
        alert("Informe o codigo do cliente e selecione um arquivo");
        return;
    }

    const dadosArquivo = new FormData();
    dadosArquivo.append("arquivo", arquivo);

    const response = await fetch(`${URL_API}/upload/${codigoCliente}`, {
        method: "POST",
        body: dadosArquivo
    });

    if (response.ok) {
        alert("documento enviado com sucesso.");
        document.getElementById("codigoCliente").value = "";
        document.getElementById("arquivo").value = "";
    } else {
        const erro = await response.json();
        alert("Erro: " + (erro.message || "Falha ao enviar o documento"));
    }
}

async function listarDocumentos() {
    const codigoCliente = document.getElementById("codigoClienteBusca").value;

    if (!codigoCliente) {
        alert("Informe o codigo do cliente");
        return;
    }

    const response = await fetch(`${URL_API}/listar/${codigoCliente}`);

    if (response.ok) {
        const documentos = await response.json();
        const tabela = document.getElementById("tabelaDocumentos");

        tabela.innerHTML = "";

        documentos.forEach(documento => {
            tabela.innerHTML += `
                <tr>
                    <td>${documento.id}</td>
                    <td>${documento.nome}</td>
                    <td>${documento.extensao}</td>
                    <td></td>
                </tr>
            `;
        });
    } else {
        alert("Erro ao buscar documentos");
    }
}