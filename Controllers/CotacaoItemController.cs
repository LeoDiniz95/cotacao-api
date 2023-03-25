using cotacao_api.General;
using cotacao_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace cotacao_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotacaoItemController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetAll([FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.GetAll());
        }

        [HttpGet("{idCotacao}")]
        public JsonResult Get(int id, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Get(id));
        }

        [HttpGet("GetByCotacao/{idCotacao}")]
        public JsonResult GetByIdCotacao(int idCotacao, [FromServices] CotacaoItem cotacaoitem)
        {
            var result = new GeneralResult();
            result.data = cotacaoitem.GetByCotacao(idCotacao);
            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult Post([FromBody] ItemRequest request, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Save(request));
        }

        [HttpPut("{idCotacao}")]
        public JsonResult Put([FromBody] ItemRequest request, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Save(request));
        }

        [HttpDelete("{idCotacao}")]
        public JsonResult Delete(int id, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Delete(id));
        }
    }
}
