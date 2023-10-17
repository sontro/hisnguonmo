using HIS.Desktop.Plugins.AssignPrescriptionPK.ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionPK
{
    public class Compare : IEqualityComparer<IcdCheckADO>
    {
        public bool Equals(IcdCheckADO x, IcdCheckADO y)
        {
            return
                x.ICD_CODE == y.ICD_CODE &&
                x.ICD_NAME == y.ICD_NAME &&
                x.IS_ACTIVE == y.IS_ACTIVE;
        }

        public int GetHashCode(IcdCheckADO x)
        {
            return (!string.IsNullOrEmpty(x.ICD_CODE) ? x.ICD_CODE.GetHashCode() : 0) +
                (!string.IsNullOrEmpty(x.ICD_NAME) ? x.ICD_NAME.GetHashCode() : 0) +
                (x.IS_ACTIVE != null ? x.IS_ACTIVE.GetHashCode() : 0);
        }
    }
}
