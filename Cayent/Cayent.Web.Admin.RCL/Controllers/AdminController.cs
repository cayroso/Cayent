using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cayent.Core.CQRS.Apps.Dtos;
using Cayent.Core.CQRS.Apps.Queries.Query;
using Cayent.Core.CQRS.Notifications.Commands.Command;
using Cayent.Core.CQRS.Notifications.Dtos;
using Cayent.Core.CQRS.Notifications.Queries.Query;
using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.Core.CQRS.Permissions.Queries.Query;
using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.Core.CQRS.Roles.Queries.Query;
using Cayent.CQRS.Commands;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cayent.Web.Admin.RCL.Controllers
{
    [Route("admin-module/api/[controller]")]
    public class AdminController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get([FromServices]IUnitOfWorkFactory unitOfWorkFactory,
            [FromServices]IQueryHandlerDispatcher queryHandlerDispatcher,
            string criteria = "", int page = 1, int pageSize = 10)
        {
            var query = new SearchPermissionsQuery("corrid", criteria, page, pageSize, "", true);

            var dto = queryHandlerDispatcher.Handle<SearchPermissionsQuery, PaginatedSearchedPermissionDto>(query);

            return Ok(dto);
        }

        [HttpGet("navbar")]
        public async Task<IActionResult> GetNavbarAsync([FromServices]IUnitOfWorkFactory  unitOfWorkFactory,
            [FromServices]ICommandHandlerDispatcher commandHandlerDispatcher,
            [FromServices]IQueryHandlerDispatcher queryHandlerDispatcher)
        {
            var unitOfWork = unitOfWorkFactory.Create();

            var cmd = new CreateNotificationCommand("xact", "notif-" + Guid.NewGuid().ToString(), 1, "subject1", "content1", "reference1", DateTime.UtcNow);
            commandHandlerDispatcher.Handle(cmd);
            
            var query = new GetUnreadNotificationReceiversByUserIdQuery("xact", "system-administrator", "", 1, 10, "", true);
            var dto = queryHandlerDispatcher.Handle<GetUnreadNotificationReceiversByUserIdQuery, PaginatedNotificationReceiverDto>(query);


            //var items = await notificationService.GetUnreadNotificationsAsync(UserId);
            //var notifications = items.OrderByDescending(p => p.Notification.DateSent).Take(15).ToList();

            ////var items2 = await messageService.GetUnreadMessagesAsync(UserId);
            ////var msgHeaders = items2.Take(15).ToList();

            //var chats = await appDbContext
            //    .ChatReceiverMessages
            //    .Include(p => p.ChatMessage)
            //        .ThenInclude(p => p.Sender)
            //            .ThenInclude(p => p.User)
            //    //.Include(p => p.ChatReceiver)
            //    .Where(p => p.ChatReceiver.ReceiverId == UserId && p.IsRead == false)
            //    .Take(15)
            //    .ToListAsync();

            ////  trigger populate
            ////var chatReceivers = await appDbContext
            ////    .ChatReceivers
            ////    .Where(p => p.ReceiverId == UserId)
            ////    .ToListAsync();


            //var model = new NavbarInfo
            //{
            //    Username = User.Identity.Name,
            //    NotificationReceivers = notifications,
            //    MessageReceiverMessages = chats
            //};


            //return Ok(model);
            unitOfWork.Commit();

            return Ok(dto);
        }

        [HttpGet("sidebar")]
        public async Task<IActionResult> GetSidebarAsync()
        {
            return Ok();
        }
    }
}
