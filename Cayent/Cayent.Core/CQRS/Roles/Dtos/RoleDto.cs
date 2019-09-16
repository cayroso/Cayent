using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Roles.Dtos
{

    public class RoleDto : BaseDto, IResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SearchedRoleDto : RoleDto, IResponse
    {

    }

    public class RoleDetailDto : RoleDto, IResponse
    {

        public List<PermissionDto> Permissions { get; set; }
    }


    public class PaginatedSearchedRoleDto : Paginated<SearchedRoleDto>, IResponse
    {
        public PaginatedSearchedRoleDto(List<SearchedRoleDto> list, int page, int pageSize, int itemCount)
            : base(list, page, pageSize, itemCount)
        {
        }
    }
}
