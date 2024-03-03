using Accounting.Application.Queries;
using Accounting.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]

    public class ItemController : Controller
    {

        [HttpGet]
        public IActionResult Get([FromServices] ItemQueries query)
        {

            return Ok(query.Get());
        }

        [HttpPost]
        public IActionResult Add([FromServices] ItemServices service, [FromForm] ItemServices.ItemParams @params)
        {

            return Ok(service.New(@params));
        }

        [HttpPut("changeOrder")]
        public IActionResult ChangeOrder([FromServices] ItemServices service, [FromBody] ItemServices.OrderParams @orderParams) {
        
            return Ok(service.ChangeOrder(@orderParams));
        }


    }


}
