using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cayent.Core.CQRS.Apps.Dtos;
using Cayent.Core.CQRS.Apps.Queries.Query;
using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.Core.CQRS.Roles.Queries.Query;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cayent.Web.Admin.RCL.Controllers
{
    [Route("admin-module/api/[controller]")]
    public class RolesController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromServices]IUnitOfWork unitOfWork,
            [FromServices]IQueryHandlerDispatcher queryHandlerDispatcher,
            string criteria = "", int page = 1, int pageSize = 10)
        {
            var query = new SearchRolesQuery("corrid", criteria, page, pageSize, "", true);

            var dto = queryHandlerDispatcher.Handle<SearchRolesQuery, PaginatedSearchedRoleDto>(query);

            return Ok(dto);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromServices]IUnitOfWork unitOfWork,
            [FromServices]IQueryHandlerDispatcher queryHandlerDispatcher,
            string id)
        {
            var query = new GetRoleByIdQuery("corrid", id);

            var dto = queryHandlerDispatcher.Handle<GetRoleByIdQuery, RoleDetailDto>(query);

            return Ok(dto);
        }
        
    }
}
