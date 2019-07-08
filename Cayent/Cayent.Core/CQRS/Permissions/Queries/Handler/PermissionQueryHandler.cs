using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.Core.CQRS.Permissions.Queries.Query;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.CQRS.Permissions.Queries.Handler
{
    public sealed class PermissionQueryHandler : BaseQueryHandler,
        IQueryHandler<SearchPermissionsQuery, PaginatedSearchedPermissionDto>
    {
        public PermissionQueryHandler(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        PaginatedSearchedPermissionDto IQueryHandler<SearchPermissionsQuery, PaginatedSearchedPermissionDto>.Handle(SearchPermissionsQuery query)
        {
            var mainSql = @"
select  distinct item.PermissionId
from    [core_Permission] as item
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
    select  distinct item.PermissionId
    from    [core_Permission] as item
    join    ItemsFound as fnd on (fnd.PermissionId = item.PermissionId)
    where	item.PermissionId not in (select items.PermissionId from ItemsFound items order by items.rowid asc limit @Skip)
    order	by fnd.rowId limit @PageSize
;
select  count(1)
from    ItemsFound
;
select  p.PermissionId as 'Id', p.Name, p.Description
        , p.DateCreated, p.DateUpdated, p.DateEnabled, p.DateDeleted

        , app.Title as 'AppTitle'
from    [core_Permission] p
join    ItemsFiltered fnd on (fnd.PermissionId =p.PermissionId)
join    core_App app on (p.AppId = app.AppId)
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

                var items = multi.Read<SearchedPermissionDto>().ToList();

                var paginated = new PaginatedSearchedPermissionDto(items, query.Page, query.PageSize, count);

                return paginated;
            }
        }
    }
}

