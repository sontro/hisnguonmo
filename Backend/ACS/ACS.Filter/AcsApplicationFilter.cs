using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsApplicationFilter : FilterBase
    {
        public short? IS_CHECK_VERSION { get; set; }
        public string APPLICATION_CODE { get; set; }

        public AcsApplicationFilter()
            : base()
        {
        }
    }
}
