using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRegisterGate
{
    class HisRegisterGateCallOrReCallCheck : BusinessBase
    {
        internal HisRegisterGateCallOrReCallCheck()
            : base()
        {
        }

        internal HisRegisterGateCallOrReCallCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {
        }

        internal bool VerifyRequireField(RegisterGateCallSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.RegisterGateId)) throw new ArgumentNullException("data.RegisterGateId");
                if (string.IsNullOrWhiteSpace(data.CallPlace)) throw new ArgumentNullException("data.CallPlace");
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
