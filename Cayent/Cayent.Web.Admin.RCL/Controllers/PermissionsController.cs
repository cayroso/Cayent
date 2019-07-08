using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cayent.Core.CQRS.Apps.Dtos;
using Cayent.Core.CQRS.Apps.Queries.Query;
using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.Core.CQRS.Permissions.Queries.Query;
using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.Core.CQRS.Roles.Queries.Query;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cayent.Web.Admin.RCL.Controllers
{
    [Route("admin-module/api/[controller]")]
    public class PermissionsController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromServices]IUnitOfWork unitOfWork,
            [FromServices]IQueryHandlerDispatcher queryHandlerDispatcher,
            string criteria = "", int page = 1, int pageSize = 10)
        {
            var query = new SearchPermissionsQuery("corrid", criteria, page, pageSize, "", true);

            var dto = queryHandlerDispatcher.Handle<SearchPermissionsQuery, PaginatedSearchedPermissionDto>(query);

            return Ok(dto);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
