using HIS.Desktop.Plugins.TreatmentFinish.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentFinish
{
    public class Compare : IEqualityComparer<IcdADO>
    {
        public bool Equals(IcdADO x, IcdADO y)
        {
            return
                x.ICD_CODE == y.ICD_CODE &&
                x.ICD_NAME == y.ICD_NAME &&
                x.IS_ACTIVE == y.IS_ACTIVE;
        }

        public int GetHashCode(IcdADO x)
        {
            return (!string.IsNullOrEmpty(x.ICD_CODE) ? x.ICD_CODE.GetHashCode() : 0) +
                (!string.IsNullOrEmpty(x.ICD_NAME) ? x.ICD_NAME.GetHashCode() : 0) +
                (x.IS_ACTIVE != null ? x.IS_ACTIVE.GetHashCode() : 0);
        }
    }
}
