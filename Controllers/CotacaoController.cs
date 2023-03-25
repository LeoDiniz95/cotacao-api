using cotacao_api.Models;
using cotacao_api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cotacao_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaoController : ControllerBase
    {

        [HttpGet]
        public JsonResult GetAll([FromServices] Cotacao cotacao)
        {
            return new JsonResult(cotacao.GetAll());
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id, [FromServices] Cotacao cotacao)
        {
            return new JsonResult(cotacao.Get(id));
        }

        [HttpPost]
        public JsonResult Post([FromBody] CotacaoRequest request, [FromServices] Cotacao cotacao)
        {
            return new JsonResult(cotacao.Save(request));
        }

        [HttpPut("{id}")]
        public JsonResult Put([FromBody] CotacaoRequest request, [FromServices] Cotacao cotacao)
        {
            return new JsonResult(cotacao.Save(request));
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id,[FromServices] Cotacao cotacao)
        {
            return new JsonResult(cotacao.Delete(id));
        }
    }
}
