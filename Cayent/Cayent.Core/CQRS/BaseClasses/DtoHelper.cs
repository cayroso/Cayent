using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cayent.Core.CQRS.BaseClasses
{
    public static class DtoHelper
    {
        public static DateTime GetMinimum(params DateTime[] dates)
        {
            return dates.Min();
        }

        public static DateTime GetMax(params DateTime[] dates)
        {
            return dates.Max();
        }
    }
}
