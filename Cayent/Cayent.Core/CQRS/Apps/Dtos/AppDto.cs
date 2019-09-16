using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Permissions.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Dtos
{
    public class AppDto : BaseDto, IResponse
    {
        public AppDto()
        {
            Title = string.Empty;
            Description = string.Empty;
            IconClass = string.Empty;
            Url = string.Empty;
            Modules = new List<ModuleDto>();
            Permissions = new List<PermissionDto>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }
        public List<ModuleDto> Modules { get; set; }
        public List<PermissionDto> Permissions { get; set; }

    }

    public class SearchedAppDto : BaseDto, IResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }
        public string Url { get; set; }

        //public int Tenants { get; set; }
        public int Modules { get; set; }
    }

    public class PaginatedSearchedAppDto : Paginated<SearchedAppDto>, IResponse
    {
        public PaginatedSearchedAppDto(List<SearchedAppDto> list, int page, int pageSize, int itemCount)
            : base(list, page, pageSize, itemCount)
        {
        }
    }
}
