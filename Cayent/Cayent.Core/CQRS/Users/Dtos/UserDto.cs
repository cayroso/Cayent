using Cayent.Core.CQRS.BaseClasses;
using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Users.Dtos
{
    
    public class UserDto: BaseDto, IResponse
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
    }

    public class SearchedUserDto : UserDto, IResponse
    {
        
    }

    public class UserDetailDto : UserDto, IResponse
    {
        public List<RoleDto> Roles { get; set; }
    }

    public class PaginatedSearchedUserDto : Paginated<SearchedUserDto>, IResponse
    {
        public PaginatedSearchedUserDto(List<SearchedUserDto> list, int page, int pageSize, int itemCount)
            : base(list, page, pageSize, itemCount)
        {
        }
    }
}
