using Microsoft.AspNetCore.Mvc;
using projGuriBatatais.DataAccess;
using System.Data;

namespace projGuriBatatais.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChaveAcessoController : ControllerBase
    {
        private ChaveAcesso _chaveAcesso;

        public ChaveAcessoController()
        {
            _chaveAcesso = new ChaveAcesso();
        }

        // GET: api/ChaveAcesso
        [HttpGet("todas")]
        public IActionResult GetTodasChaves()
        {
            try
            {
                var chaves = _chaveAcesso.SelecionarTodos();
                return Ok(chaves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar dados: {ex.Message}");
            }
        }
    }
}
