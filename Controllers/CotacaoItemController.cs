using cotacao_api.Repositories;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}")]
        public JsonResult Get(int id, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Get(id));
        }

        [HttpPost]
        public JsonResult Post([FromBody] ItemRequest request, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Save(request));
        }

        [HttpPut("{id}")]
        public JsonResult Put([FromBody] ItemRequest request, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Save(request));
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id, [FromServices] CotacaoItem cotacaoitem)
        {
            return new JsonResult(cotacaoitem.Delete(id));
        }
    }
}
