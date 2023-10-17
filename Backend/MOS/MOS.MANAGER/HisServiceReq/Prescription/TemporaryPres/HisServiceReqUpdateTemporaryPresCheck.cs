using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.TemporaryPres
{
    class HisServiceReqUpdateTemporaryPresCheck: BusinessBase
    {
        internal HisServiceReqUpdateTemporaryPresCheck()
            : base()
        {
        }

        internal HisServiceReqUpdateTemporaryPresCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(TemporaryServiceReqSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ServiceReqId)) throw new ArgumentNullException("data.ServiceReqId");
                if (!IsGreaterThanZero(data.InstructionTime)) throw new ArgumentNullException("data.InstructionTime");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool IsValidTemporaryPres(HIS_SERVICE_REQ serviceReq)
        {
            try
            {
                if (serviceReq == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.LoiDuLieuKhongHopLe);
                    return false;
                }

                if (serviceReq != null && serviceReq.IS_TEMPORARY_PRES != Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_YLenhKhongPhaiDonTam, serviceReq.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}
