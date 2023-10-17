using Inventec.Desktop.CustomControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PharmacyCashier.Util
{
    public class GridLookupEditCustom : CustomGridLookUpEditWithFilterMultiColumn
    {
        public GridLookupEditCustom() : base() { }

        protected override bool IsAutoComplete
        {
            get
            {
                return false;
            }
        }
    }
}
