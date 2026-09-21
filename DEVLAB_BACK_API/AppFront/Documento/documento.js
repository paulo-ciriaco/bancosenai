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

        document.getElementById("codigoClienteBusca").value = codigoCliente;
        listarDocumentos();

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
                    <td>
                        <button onclick="baixarDocumento(${documento.id})">Baixar</button>
                        <button onclick="excluirDocumento(${documento.id})">Excluir</button>
                    </td>
                </tr>
            `;
        });
    } else {
        alert("Erro ao buscar documentos");
    }
}

async function baixarDocumento(id) {
    const response = await fetch(`${URL_API}/download/${id}`);

    if (response.ok) {
        const blob = await response.blob();

        const url = window.URL.createObjectURL(blob);
        const link = document.createElement("a");

        link.href = url;
        link.download = "documento";
        link.click();

        window.URL.revokeObjectURL(url);
    } else {
        alert("Erro ao baixar o documento");
    }
}

async function excluirDocumento(id) {
    const confirmar = confirm("Deseja excluir este documento?");

    if (!confirmar) {
        return;
    }

    const response = await fetch(`${URL_API}/excluir/${id}`, {
        method: "DELETE"
    });

    if (response.ok) {
        alert("Documento excluido com sucesso");
        listarDocumentos();
    } else {
        alert("Erro ao excluir o documento");
    }
}