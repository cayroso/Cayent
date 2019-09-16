using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.Core.CQRS.Users.Dtos;
using Cayent.Core.CQRS.Users.Queries.Query;
using Cayent.CQRS.Queries;
using Cayent.Infrastructure.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.CQRS.Users.Queries.Handler
{
    public sealed class UserQueryHandler : BaseQueryHandler,
        IQueryHandler<SearchUsersQuery, PaginatedSearchedUserDto>,
        IQueryHandler<GetUserByIdQuery, UserDetailDto>
    {
        public UserQueryHandler(IUnitOfWorkFactory unitOfWork)
            : base(unitOfWork)
        {

        }

        PaginatedSearchedUserDto IQueryHandler<SearchUsersQuery, PaginatedSearchedUserDto>.Handle(SearchUsersQuery query)
        {
            var mainSql = @"
select  distinct item.UserId
from    [core_User] as item
join    core_Membership as m on (m.MembershipId = item.UserId)
where   (item.FirstName like @Criteria 
        or item.MiddleName like @Criteria
        or item.LastName like @Criteria
        or item.Email like @Criteria
        or item.Phone like @Criteria
        or item.Mobile like @Criteria
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
    select  distinct item.UserId
    from    [core_User] as item
    join    ItemsFound as fnd on (fnd.UserId = item.UserId)
    where	item.UserId not in (select items.UserId from ItemsFound items order by items.rowid asc limit @Skip)
    order	by fnd.rowId limit @PageSize
;
select  count(1)
from    ItemsFound
;
select  u.UserId as 'Id', u.FirstName, u.MiddleName, u.LastName, u.Email, u.Phone, u.Mobile
        , u.DateCreated, u.DateUpdated, u.DateEnabled, u.DateDeleted
from    [core_User] u
join    ItemsFiltered fnd on (fnd.UserId = u.UserId)
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

                var items = multi.Read<SearchedUserDto>().ToList();

                var paginated = new PaginatedSearchedUserDto(items, query.Page, query.PageSize, count);

                return paginated;
            }
        }

        UserDetailDto IQueryHandler<GetUserByIdQuery, UserDetailDto>.Handle(GetUserByIdQuery query)
        {
            const string sql = @"
select  u.UserId as 'Id', u.FirstName, u.MiddleName, u.LastName, u.Email, u.Phone, u.Mobile
        , u.DateCreated, u.DateUpdated, u.DateEnabled, u.DateDeleted
from    core_vwUser u
where   u.UserId = @Id
;
select  r.RoleId as 'Id', r.Name, r.Description
        , r.DateCreated, r.DateUpdated, r.DateEnabled, r.DateDeleted
from    core_Role r
join    core_MembershipRole mr on (mr.RoleId = r.RoleId)
where   mr.MembershipId = @Id
;
";
            using (var multi = DbConnection.QueryMultiple(sql, new
            {
                Id = query.UserId
            }, DbTransaction))
            {
                var item = multi.Read<UserDetailDto>().SingleOrDefault();

                if (item != null)
                {
                    var roles = multi.Read<RoleDto>().ToList();

                    item.Roles = roles;
                }

                return item;
            }
        }
    }
}
