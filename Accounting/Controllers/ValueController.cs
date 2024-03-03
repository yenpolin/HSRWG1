using Accounting.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Accounting.Application.Services.ValueServices;

namespace Accounting.Controllers
{
    [Route("api/[controller]")]
    public class ValueController : Controller
    {
        [HttpGet("getAllValues")]
       public IActionResult Get([FromServices] ValueServices service )
       {
           return Ok(service.Get());
       }

       [HttpGet("getByYearMonth/{yearmonth}")]
       public IActionResult GetByYearMonth([FromServices] ValueServices service, DateTime yearMonth)
       {   
           //Console.WriteLine(yearMonth);
           //var year = int.Parse(yearMonth.Substring(0,4));
           //var month = int.Parse(yearMonth.Substring(4,2));
           //DateTime YearMonthToGet = new DateTime(year, month, 1);
           
           return Ok(service.GetByYearMonth(yearMonth));
       }

       [HttpPost("add")]
       public IActionResult Add([FromServices] ValueServices service, [FromForm] ValueServices.ValueParams @params)
       {

           return Ok(service.New(@params));
       }

        [HttpPut("addmany")]
        public IActionResult AddMany([FromServices] ValueServices service,[FromQuery] DateTime yearMonth, [FromBody] List<ItemValue> @params)
        {
            service.NewMany(yearMonth, @params);
            return Ok();
        }

    }
}
