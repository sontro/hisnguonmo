using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.GetNextStoreBordereauCode
{
    class GetNextStoreBordereauCodeCheck : BusinessBase
    {
        internal GetNextStoreBordereauCodeCheck()
            : base()
        {

        }

        internal GetNextStoreBordereauCodeCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(GetStoreBordereauCodeSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.HeinLockTime)) throw new ArgumentNullException("data.HeinLockTime");
                if (!IsGreaterThanZero(data.TreatmentTypeId)) throw new ArgumentNullException("data.TreatmentTypeId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
