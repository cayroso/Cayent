using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public static class DtoHelper
    {
        public static DateTimeOffset GetMinimum(params DateTimeOffset[] dates)
        {
            return dates.Min();
        }

        public static DateTimeOffset GetMax(params DateTimeOffset[] dates)
        {
            return dates.Max();
        }
    }
}
