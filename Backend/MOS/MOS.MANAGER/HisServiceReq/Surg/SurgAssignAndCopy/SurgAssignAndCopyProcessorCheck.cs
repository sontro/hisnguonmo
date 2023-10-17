using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.SurgAssignAndCopy
{
    class SurgAssignAndCopyProcessorCheck: BusinessBase
	{
		internal SurgAssignAndCopyProcessorCheck()
			: base()
		{
		}

        internal SurgAssignAndCopyProcessorCheck(CommonParam param)
			: base(param)
		{
		}

        internal bool VerifyRequireField(SurgAssignAndCopySDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ServiceReqId)) throw new ArgumentNullException("data.ServiceReqId");
                if (!IsNotNullOrEmpty(data.InstructionTimes)) throw new ArgumentNullException("data.InstructionTimes");
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
