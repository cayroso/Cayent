using Cayent.Core.CQRS.BaseClasses;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Permissions.Dtos
{
    public class PermissionDto : EntityBaseDto
    {
        public PermissionDto()
        {
            AppId = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
        }

        public string AppId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public string AppTitle { get; set; }
    }
    
    public class SearchedPermissionDto : PermissionDto
    {

    }

    public class PaginatedSearchedPermissionDto : Paginated<SearchedPermissionDto>, IResponse
    {
        public PaginatedSearchedPermissionDto(List<SearchedPermissionDto> list, int page, int pageSize, int itemCount)
            : base(list, page, pageSize, itemCount)
        {
        }
    }
}
