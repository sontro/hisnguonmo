using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common.Update.PacsUpdateResult
{
    class PacsServiceReqUpdateResultCheck: BusinessBase
    {
        internal PacsServiceReqUpdateResultCheck()
            : base()
        {
        }

        internal PacsServiceReqUpdateResultCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValidAccessionNumber(string accessionNumber, ref HIS_SERE_SERV sereServ, ref HIS_SERVICE_REQ serviceReq, ref HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                sereServ = new HisSereServGet().GetById(Int64.Parse(accessionNumber));
                serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value);
                treatment = new HisTreatmentGet().GetById(serviceReq.TREATMENT_ID);
                if (sereServ == null || serviceReq == null || treatment == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
            }
            return valid;
        }

        internal bool IsValidCanCel(bool isCancel, long? EndTime, HIS_TREATMENT treatment)
        {
            bool valid = true;
            try
            {
                if (isCancel)
                {
                    HisTreatmentCheck checker = new HisTreatmentCheck(param);
                    return checker.IsUnpause(treatment)
                        && checker.IsUnLock(treatment)
                        && checker.IsUnTemporaryLock(treatment)
                        && checker.IsUnLockHein(treatment);
                }
                else
                {
                    if (!EndTime.HasValue)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisServiceReq_ThieuThongTinThoiGianKetThucXuLy);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
            }
            return valid;
        }
    }
}
