using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public class RelationDto
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateEnabled { get; set; }
        public bool IsEnabled { get { return DateEnabled > DateTime.UtcNow; } }

        public RelationDto()
        {
            DateCreated = DateTime.Now;
            DateEnabled = DateTime.MaxValue;
        }
    }
}
