using Cayent.Core.CQRS.Apps.Dtos;
using Cayent.CQRS.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.Apps.Queries.Query
{

    public sealed class GetAppByIdQuery : Query<AppDto>
    {
        public GetAppByIdQuery(string correlationId, string appId)
            : base(correlationId)
        {
            AppId = appId;
        }

        public string AppId { get; private set; }
    }
}
