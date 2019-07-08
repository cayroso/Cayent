using Cayent.Core.CQRS.Roles.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Roles.Queries.Query
{
    public sealed class GetRoleByIdQuery : Query<RoleDetailDto>
    {
        public GetRoleByIdQuery(string correlationId, string userId)
            : base(correlationId)
        {
            UserId = userId;
        }

        public string UserId { get; }

    }
}
