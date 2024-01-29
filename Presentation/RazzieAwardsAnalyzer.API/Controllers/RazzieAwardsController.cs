using Microsoft.AspNetCore.Mvc;
using RazzieAwardsAnalyzer.Application.DTOs.Response;
using RazzieAwardsAnalyzer.Application.Interfaces;

namespace RazzieAwardsAnalyzer.API.Controllers
{
    /// <summary>
    /// RazzieAwards
    /// </summary>
    /// <param name="awardIntervalService"></param>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class RazzieAwardsController(IAwardIntervalService awardIntervalService) : ControllerBase
    {
        private readonly IAwardIntervalService _awardIntervalService = awardIntervalService;

        /// <summary>
        /// Retorna Intervalo de tempo de premiação por produtores
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna o resultado solicitado</response>
        /// <response code="204">Sem conteúdo</response>
        /// <response code="500">Erro interno do Servidor</response>
        [HttpGet]
        public async Task<ActionResult<AwardIntervalResponseDTO>> Get()
        {
            try
            {
                var result = await _awardIntervalService.GetAwardInterval();
                if(result == null || (result.Min.Count == 0 && result.Max.Count == 0))
                {
                    return NoContent();
                }
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }            
        }
    }
}