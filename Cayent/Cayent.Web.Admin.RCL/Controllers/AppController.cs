using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cayent.Core.CQRS.Apps.Dtos;
using Cayent.Core.CQRS.Apps.Queries.Query;
using Cayent.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cayent.Web.Admin.RCL.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        [HttpGet("gets")]
        public IActionResult Get([FromServices]IQueryHandlerDispatcher queryHandlerDispatcher)
        {
            var query = new GetAppByIdQuery("AppDisabled", "system.security");
            var dto = queryHandlerDispatcher.Handle<GetAppByIdQuery, AppDto>(query);

            return Ok(dto);
        }
    }
}
