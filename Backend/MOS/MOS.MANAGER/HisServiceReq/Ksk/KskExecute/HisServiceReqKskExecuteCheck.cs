using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ksk.KskExecute
{
    public class HisServiceReqKskExecuteCheck : BusinessBase
    {
        internal HisServiceReqKskExecuteCheck()
            : base()
        {
        }

        internal HisServiceReqKskExecuteCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(HisServiceReqKskExecuteSDO data)
        {
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ServiceReqId)) throw new ArgumentNullException("data.ServiceReqId");

                return true;
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool VerifyRequireField(HisServiceReqKskExecuteV2SDO data)
        {
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.ServiceReqId)) throw new ArgumentNullException("data.ServiceReqId");

                return true;
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        internal bool VerifyServiceReq(EFMODEL.DataModels.HIS_SERVICE_REQ data)
        {
            try
            {
                if (data.SERVICE_REQ_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_KhongPhaiYeuCauKham);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            return false;
        }
    }
}
