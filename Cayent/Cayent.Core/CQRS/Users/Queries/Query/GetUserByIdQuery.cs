using Cayent.Core.CQRS.Users.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Users.Queries.Query
{
    public sealed class GetUserByIdQuery : Query<UserDetailDto>
    {
        public GetUserByIdQuery(string correlationId, string userId)
            : base(correlationId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
