using Cayent.Core.CQRS.Apps.Dtos;
using Cayent.Core.CQRS.Apps.Queries.Query;
using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Queries.Handler
{
    public sealed class AppQueryHandler : BaseQueryHandler,
        IQueryHandler<GetAppByIdQuery, AppDto>//,
        //IQueryHandler<GetAppByTenantQuery, AppDto>,
        //IQueryHandler<GetTenantAppsQuery, List<AppDto>>,
        //IQueryHandler<SearchAppsQuery, PaginatedSearchedAppDto>,
        //IQueryHandler<GetTenantUnassignedAppsQuery, List<AppDto>>,
        //IQueryHandler<GetTenantAppsByUserQuery, List<AppDto>>,
        //IQueryHandler<GetAppByUserQuery, AppDto>

    {
        public AppQueryHandler(IUnitOfWorkFactory  unitOfWork)
            : base(unitOfWork)
        {

        }

        AppDto IQueryHandler<GetAppByIdQuery, AppDto>.Handle(GetAppByIdQuery query)
        {
            const string sql = @"
select  a.AppId, a.Title, a.Description, a.IconClass, a.Url, a.Sequence
        , a.DateCreated, a.DateUpdated, a.DateEnabled, a.DateDeleted
from    core_App a
where   a.AppId = @Id
order by a.Sequence asc
;

select  m.AppId, m.ModuleId, m.Title, m.Description, m.IconClass, m.Url, m.Sequence
        , m.DateCreated, m.DateUpdated, m.DateEnabled, m.DateDeleted
from    core_vwModule m
where   m.AppId = @Id
order by m.Sequence asc

;
select  p.AppId, p.PermissionId, p.Name, p.Description, p.DateCreated, p.DateUpdated, p.DateEnabled, p.DateDeleted
from    core_vwPermission p
where   p.AppId = @Id
;
";
            using (var multi = DbConnection.QueryMultiple(sql, new { Id = query.AppId }, DbTransaction))
            {
                var data = multi.Read<AppDto>().SingleOrDefault();

                if (data != null)
                {

                    var mods = multi.Read<ModuleDto>().ToList();

                    var perms = multi.Read<PermissionDto>().ToList();

                    data.Modules = mods;
                    data.Permissions = perms;

                }
                return data;
            }
        }

//        AppDto IQueryHandler<GetAppByTenantQuery, AppDto>.Handle(GetAppByTenantQuery query)
//        {
//            const string sql = @"
//select  a.Id, a.Title, a.Description, a.IconClass, a.Url
//        , a.DateCreated, a.DateUpdated, a.DateEnabled, a.DateDeleted
//from    core_vwTenantApp a
//where   a.TenantId = @TenantId
//and     a.Id = @AppId
//order by a.Sequence asc
//;

//select  m.Id, m.AppId, m.Title, m.Description, m.IconClass, m.Url, m.Sequence
//        , m.DateCreated, m.DateUpdated, m.DateEnabled, m.DateDeleted
//from    core_vwTenantModule m
//where   m.TenantId = @TenantId
//and     m.AppId = @AppId
//order by m.Sequence asc
//;

//select  p.Id, p.AppId, p.Name, p.Description, p.DateCreated, p.DateUpdated, p.DateEnabled, p.DateDeleted
//from    core_vwTenantPermission p
//where   p.TenantId = @TenantId
//and     p.AppId = @AppId
//;

//";
//            using (var multi = DbConnection.QueryMultiple(sql, new
//            {
//                TenantId = query.TenantId,
//                AppId = query.AppId
//            }, DbTransaction))
//            {

//                var app = multi.Read<AppDto>().SingleOrDefault();

//                if (app != null)
//                {
//                    var mods = multi.Read<ModuleDto>().ToList();

//                    var perms = multi.Read<PermissionDto>().ToList();

//                    app.Modules = mods;
//                    app.Permissions = perms;

//                }
//                return app;
//            }
//        }

//        List<AppDto> IQueryHandler<GetTenantAppsQuery, List<AppDto>>.Handle(GetTenantAppsQuery query)
//        {
//            const string sql = @"
//select  ta.Id, ta.Title, ta.Description, ta.IconClass, ta.Url, ta.Sequence
//        , ta.DateCreated, ta.DateUpdated, ta.DateEnabled, ta.DateDeleted
//from    core_vwTenantApp ta
//where   ta.TenantId = @Id
//order by ta.Sequence asc
//;
//";
//            using (var multi = DbConnection.QueryMultiple(sql, new { Id = query.TenantId }, DbTransaction))
//            {
//                var apps = multi.Read<AppDto>().ToList();

//                return apps;
//            }
//        }

//        PaginatedSearchedAppDto IQueryHandler<SearchAppsQuery, PaginatedSearchedAppDto>.Handle(SearchAppsQuery query)
//        {
//            var mainSql = @"
//select  distinct item.Id        
//from    core_App as item
//where   (item.Title like @Criteria or item.Description like @Criteria)
//";
//            if (!string.IsNullOrEmpty(query.SortBy))
//            {
//                mainSql += string.Format("order by item.{0} {1}", query.SortBy, query.SortOrderAsc ? "asc" : "desc");
//            }

//            var sql = @"
//drop table if exists ItemsFound
//;
//drop table if exists ItemsFiltered
//;
//create temporary table if not exists ItemsFound as"
//    + mainSql +
//@";

//create temporary table if not exists ItemsFiltered as
//    select  distinct item.Id
//    from    core_App as item
//    join    ItemsFound as fnd on (fnd.Id = item.Id)
//    where	item.Id not in (select items.Id from ItemsFound items order by items.rowid asc limit @Skip)
//    order	by fnd.rowId limit @PageSize
//;
//select  count(1)
//from    ItemsFound
//;
//select  a.*
//		, mod.Modules
//		, ta.Tenants
//from    core_App a
//left join (select ta.AppId, count(1) as 'Tenants'
//            from core_TenantApp ta group by ta.AppId) ta
//		on (ta.AppId = a.Id)
//left join (select m.AppId, count(1) as 'Modules'
//            from core_Module m group by m.AppId) mod
//		on (mod.AppId = a.Id)
//join    ItemsFiltered fnd on (fnd.Id = a.Id)
//order by a.Sequence asc
//;
//";
//            using (var multi = DbConnection.QueryMultiple(sql, new
//            {
//                Criteria = "%" + query.Criteria + "%",
//                Skip = (query.Page - 1) * query.PageSize,
//                PageSize = query.PageSize,
//                DateDeleted = DateTime.MaxValue
//            }, DbTransaction))
//            {
//                var count = multi.Read<int>().Single();

//                var items = multi.Read<SearchedAppDto>().ToList();

//                var paginated = new PaginatedSearchedAppDto(items, query.Page, query.PageSize, count);


//                return paginated;
//            }
//        }

//        List<AppDto> IQueryHandler<GetTenantUnassignedAppsQuery, List<AppDto>>.Handle(GetTenantUnassignedAppsQuery query)
//        {
//            const string sql = @"
//select  a.*
//from    core_App a
//where   a.Id not in (select AppId from core_TenantApplication where TenantId = @Id)
//order by a.Sequence asc
//;
//";
//            using (var multi = DbConnection.QueryMultiple(sql, new { Id = query.TenantId }, DbTransaction))
//            {
//                var apps = multi.Read<AppDto>().ToList();

//                return apps;
//            }
//        }

//        List<AppDto> IQueryHandler<GetTenantAppsByUserQuery, List<AppDto>>.Handle(GetTenantAppsByUserQuery query)
//        {
//            const string sql = @"
//select  ta.Id, ta.Title, ta.Description, ta.IconClass, ta.Url, ta.Sequence
//        , ta.DateCreated, ta.DateUpdated, ta.DateEnabled, ta.DateDeleted
//from    core_vwTenantApp ta
//join    core_MembershipApp ma on (ma.AppId = ta.Id) 
//where   ta.TenantId = @Id
//and     ma.MembershipId = @MembershipId
//order by ta.Sequence asc
//;
//";
//            using (var multi = DbConnection.QueryMultiple(sql, new
//            {
//                Id = query.TenantId,
//                MembershipId = query.UserId
//            }, DbTransaction))
//            {
//                var apps = multi.Read<AppDto>().ToList();

//                return apps;
//            }
//        }

//        AppDto IQueryHandler<GetAppByUserQuery, AppDto>.Handle(GetAppByUserQuery query)
//        {
//            const string sql = @"
//select  a.Id, a.Title, a.Description, a.IconClass, a.Url
//        , a.DateCreated, a.DateUpdated, a.DateEnabled, a.DateDeleted
//from    core_MembershipApp ma
//join    core_App a on (ma.AppId = a.Id)
//where   ma.MembershipId = @MembershipId
//and     a.Id = @AppId
//;

//select  m.Id, m.AppId, m.Title, m.Description, m.IconClass, m.Url, m.Sequence
//        , m.DateCreated, m.DateUpdated, m.DateEnabled, m.DateDeleted
//from    core_MembershipModule ma
//join    core_Module m on (ma.ModuleId = m.Id)
//where   ma.MembershipId = @MembershipId
//and     m.AppId = @AppId
//order by m.Sequence asc
//;

//select  p.Id, p.AppId, p.Name, p.Description, p.DateCreated, p.DateUpdated, p.DateEnabled, p.DateDeleted
//from    core_vwTenantPermission p
//where   p.TenantId = @TenantId
//and     p.AppId = @AppId
//;

//";
//            using (var multi = DbConnection.QueryMultiple(sql, new
//            {
//                TenantId = query.TenantId,
//                MembershipId = query.UserId,
//                AppId = query.AppId
//            }, DbTransaction))
//            {

//                var app = multi.Read<AppDto>().SingleOrDefault();

//                if (app != null)
//                {
//                    var mods = multi.Read<ModuleDto>().ToList();

//                    var perms = multi.Read<PermissionDto>().ToList();

//                    app.Modules = mods;
//                    app.Permissions = perms;

//                }
//                return app;
//            }
//        }
    }
}
