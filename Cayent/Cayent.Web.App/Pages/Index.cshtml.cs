using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cayent.Core.CQRS.Apps.Commands.Command;
using Cayent.CQRS.Commands;
using Cayent.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cayent.Web.App.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet([FromServices]IUnitOfWork unitOfWork, [FromServices]ICommandHandlerDispatcher commandHandlerDispatcher)
        {
            var cmd1 = new CreateAppCommand(string.Empty, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), string.Empty, 1);

            //var cmd2 = new CreateAppCommand(string.Empty, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), string.Empty, 1);
            //commandHandlerDispatcher.Handle(cmd2);

            //var cmd2 = new CreateModuleCommand(string.Empty, cmd1.AppId, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "1x", "x", "x", 1);
            //var cmd3 = new CreateModuleCommand(string.Empty, cmd1.AppId, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "2x", "x", "x", 1);


            commandHandlerDispatcher.Handle(cmd1);
            //commandHandlerDispatcher.Handle(cmd2);
            //commandHandlerDispatcher.Handle(cmd3); 



            unitOfWork.Commit();
            //unitOfWork.Rollback();
        }
    }
}
