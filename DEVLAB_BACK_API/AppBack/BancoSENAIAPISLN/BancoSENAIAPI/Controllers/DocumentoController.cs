using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class DocumentoController : ControllerBase
    {
        private readonly string _caminhoRaiz = Path.Combine(
            Directory.GetCurrentDirectory(),
            "ClienteArquivos"
            );
        private static List<Models.DocumentoMetadado> _documentosMetadados = new List<Models.DocumentoMetadado>();

        private static int _nextId = 1;

        [HttpPost("upload/{codigoCliente}")]
        public async Task<IActionResult> AnexarArquivo(int codigoCliente, IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado.");
            }

            long limiteTamanho = 2 * 1024 * 1024;

            if (arquivo.Length > limiteTamanho)
            {
                return BadRequest("O arquivo excede o limite máximo de 2 MB.");
            }

            string extensao = Path.GetExtension(arquivo.FileName).ToLower();

            string[] extensoesPermitidas = { ".pdf", ".jpg", ".png" };

            if (!extensoesPermitidas.Contains(extensao))
            {
                return BadRequest("Extensão de arquivo não permitida. Apenas .pdf, .jpg e .png são aceitos.");
            }

            string pastaCliente = Path.Combine(_caminhoRaiz, codigoCliente.ToString());

            if (!Directory.Exists(pastaCliente))
            {
                Directory.CreateDirectory(pastaCliente);
            }

            string nomeOriginal = Path.GetFileNameWithoutExtension(arquivo.FileName);
            string novoNome = $"{codigoCliente}_{nomeOriginal}_{Guid.NewGuid()}{extensao}";
            string caminhoFinal = Path.Combine(pastaCliente, novoNome);

            using (var stream = new FileStream(caminhoFinal, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var documentoMetadados = new Models.DocumentoMetadado
            {
                Id = _nextId++,
                Name = nomeOriginal,
                Extensao = extensao,
                Caminho = caminhoFinal,
                CodigoCliente = codigoCliente
            };

            _documentosMetadados.Add(documentoMetadados);

            return Ok(new { mensagem = "Documento anexado com sucesso", arquivoSalvo = novoNome });
        }

        [HttpGet("listar/{codigoCliente}")]
        public IActionResult ListarDocumentos(int codigoCliente)
        {
            var documentos = _documentosMetadados
                .Where(d => d.CodigoCliente == codigoCliente)
                .ToList();

            if (!documentos.Any())
            {
                return NotFound("Nenhum documento encontrado para este cliente.");
            }

            return Ok(documentos);
        }

        [HttpGet("download/{id}")]
        public IActionResult DownloadDocumento(int id)
        {
            var documento = _documentosMetadados
                .FirstOrDefault(d => d.Id == id);

            if (documento == null)
            {
                return NotFound("Documento não encontrado.");
            }

            if (!System.IO.File.Exists(documento.Caminho))
            {
                return NotFound("Arquivo não encontrado no servidor.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(documento.Caminho);

            string nomeArquivo = documento.Name + documento.Extensao;

            return File(fileBytes, "application/octet-stream", nomeArquivo);
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult ExcluirDocumento(int id)
        {
            var documento = _documentosMetadados
                .FirstOrDefault(d => d.Id == id);

            if (documento == null)
            {
                return NotFound("Documento não encontrado.");
            }

            System.IO.File.Delete(documento.Caminho);

            _documentosMetadados.Remove(documento);

            return Ok("Documento excluído com sucesso.");
        }
    }
}