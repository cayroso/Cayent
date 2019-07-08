using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.Core.CQRS.Roles.Queries.Query;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.CQRS.Roles.Queries.Handler
{
    public sealed class RoleQueryHandler : BaseQueryHandler,
        IQueryHandler<SearchRolesQuery, PaginatedSearchedRoleDto>,
        IQueryHandler<GetRoleByIdQuery, RoleDetailDto>
    {
        public RoleQueryHandler(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        PaginatedSearchedRoleDto IQueryHandler<SearchRolesQuery, PaginatedSearchedRoleDto>.Handle(SearchRolesQuery query)
        {
            var mainSql = @"
select  distinct item.RoleId
from    [core_Role] as item
where   (item.Name like @Criteria 
        or item.Description like @Criteria
        )
";
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                mainSql += string.Format("order by item.{0} {1}", query.SortBy, query.SortOrderAsc ? "asc" : "desc");
            }

            var sql = @"
drop table if exists ItemsFound
;
drop table if exists ItemsFiltered
;
create temporary table if not exists ItemsFound as"
    + mainSql +
@";

create temporary table if not exists ItemsFiltered as
    select  distinct item.RoleId
    from    [core_Role] as item
    join    ItemsFound as fnd on (fnd.RoleId = item.RoleId)
    where	item.RoleId not in (select items.RoleId from ItemsFound items order by items.rowid asc limit @Skip)
    order	by fnd.rowId limit @PageSize
;
select  count(1)
from    ItemsFound
;
select  r.RoleId as 'Id', r.Name, r.Description
        , r.DateCreated, r.DateUpdated, r.DateEnabled, r.DateDeleted
from    [core_Role] r
join    ItemsFiltered fnd on (fnd.RoleId = r.RoleId)
order	by fnd.rowId limit @PageSize

;
";

            using (var multi = DbConnection.QueryMultiple(sql, new
            {
                Criteria = "%" + query.Criteria + "%",
                Skip = (query.Page - 1) * query.PageSize,
                PageSize = query.PageSize,
                DateDeleted = DateTime.MaxValue
            }, DbTransaction))
            {
                var count = multi.Read<int>().Single();

                var items = multi.Read<SearchedRoleDto>().ToList();

                var paginated = new PaginatedSearchedRoleDto(items, query.Page, query.PageSize, count);

                return paginated;
            }
        }

        RoleDetailDto IQueryHandler<GetRoleByIdQuery, RoleDetailDto>.Handle(GetRoleByIdQuery query)
        {
            const string sql = @"
select  r.RoleId as 'Id', r.Name, r.Description
        , r.DateCreated, r.DateUpdated, r.DateEnabled, r.DateDeleted
from    core_vwRole r
where   r.RoleId = @Id
;
select  rp.PermissionId as 'Id', rp.Name, rp.Description
        , rp.DateCreated, rp.DateUpdated, rp.DateEnabled, rp.DateDeleted

        , app.AppId, app.Title as 'AppTitle'
from    core_vwRolePermission rp
join    core_vwPermission p on (rp.PermissionId = p.PermissionId)
join    core_App app on (p.AppId = app.AppId)
where   rp.RoleId = @Id
;
";
            using (var multi = DbConnection.QueryMultiple(sql, new
            {
                Id = query.UserId
            }, DbTransaction))
            {
                var item = multi.Read<RoleDetailDto>().SingleOrDefault();

                if (item != null)
                {
                    var items = multi.Read<PermissionDto>().ToList();

                    item.Permissions = items;
                }

                return item;
            }
        }
    }
}

