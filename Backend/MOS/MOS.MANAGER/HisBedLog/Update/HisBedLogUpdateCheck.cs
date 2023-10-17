using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisBedLog.Update
{
    class HisBedLogUpdateCheck : BusinessBase
    {
        internal HisBedLogUpdateCheck()
            : base()
        {
        }

        internal HisBedLogUpdateCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsAllow(HIS_BED_LOG raw)
        {
            bool valid = true;
            try
            {
                if (raw != null && raw.SERVICE_REQ_ID.HasValue)
                {
                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(raw.SERVICE_REQ_ID.Value);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBedLog_DaCoYLenhTuongUng, serviceReq.SERVICE_REQ_CODE);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidSSBill(long bedLogId, ref List<HIS_SERVICE_REQ> serviceReqs)
        {
            bool valid = true;
            try
            {
                serviceReqs = new HisServiceReqGet().GetByBedLogId(bedLogId);
                var SSBills = IsNotNullOrEmpty(serviceReqs) ? new HisSereServBillGet().GetByServiceReqIds(serviceReqs.Select(o => o.ID).Distinct().ToList()) : null;
                if (IsNotNullOrEmpty(SSBills) && SSBills.Exists(o => !o.IS_CANCEL.HasValue || o.IS_CANCEL != Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBedLog_ChiPhiGiuongDaDuocThanhToan);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidIfHasNoEndTime(SDO.HisBedLogSDO data, List<HIS_SERVICE_REQ> serviceReqs, HIS_BED_LOG raw)
        {
            bool valid = true;
            try
            {
                if (!data.FinishTime.HasValue && IsNotNullOrEmpty(serviceReqs) && serviceReqs.All(o => o.INTRUCTION_TIME <= data.StartTime) && data.StartTime != raw.START_TIME)
                {
                    throw new Exception("Khong co du lieu endtime va thoi gian end time lon hon tat ca thoi gian y lenh cu ton tai");
                }
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
