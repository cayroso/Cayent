using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Infrastructure.Data.Edges
{
    public class EdgeData
    {
        public EdgeData()
        {
            Id = 0;
            EntryEdgeId = 0;
            DirectEdgeId = 0;
            ExitEdgeId = 0;

            StartVertex = string.Empty;
            EndVertex = string.Empty;
            Hops = 0;
            Source = string.Empty;
        }

        public int Id { get; set; }

        public int EntryEdgeId { get; set; }
        public int DirectEdgeId { get; set; }
        public int ExitEdgeId { get; set; }

        public string StartVertex { get; set; }
        public string EndVertex { get; set; }
        public int Hops { get; set; }

        public string Source { get; set; }

    }
}
