using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using MOS.MANAGER.HisTreatment;

namespace MOS.MANAGER.HisImpMest.UpdateStatus
{
    public class HisImpMestUpdateStatusCheck : BusinessBase
    {
        internal HisImpMestUpdateStatusCheck()
            : base()
        {

        }

        internal HisImpMestUpdateStatusCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool CheckTreatmentFinished(HIS_IMP_MEST raw, long statusIdUpdate)
        {
            bool valid = true;
            try
            {
                if (!HisImpMestCFG.MUST_APPROVE_BEFORE_TREATMENT_FINISHED) return true;
                if (!(raw.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DNTTL
                    || raw.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DTTTL))
                    return true;

                if (!raw.TDL_TREATMENT_ID.HasValue)
                {
                    LogSystem.Warn("Phieu Thu hoi don thuoc khong co TDL_TREATMENT_ID. ImpMestCode: " + raw.IMP_MEST_CODE);
                    return true;
                }
                if (raw.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL
                    && statusIdUpdate == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST)
                {
                    HIS_TREATMENT treatment = new HisTreatmentGet().GetById(raw.TDL_TREATMENT_ID.Value);
                    if (treatment.IS_PAUSE.HasValue && treatment.IS_PAUSE.Value == Constant.IS_TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_BenhNhanDaKetThucDieuTri);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidChildren(HIS_IMP_MEST raw, ref List<HIS_IMP_MEST> children)
        {
            bool valid = true;
            try
            {
                if (raw.IMP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__THT)
                {
                    List<HIS_IMP_MEST> list = new HisImpMestGet().GetByAggrImpMestId(raw.ID);

                    if (!IsNotNullOrEmpty(list))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhongCoDuLieu, raw.IMP_MEST_CODE);
                        return false;
                    }
                    HisImpMestCheck childChecker = new HisImpMestCheck(param);
                    valid = valid && childChecker.IsUnLock(list);
                    foreach (var rawItem in list)
                    {
                        valid = valid && childChecker.HasNotMediStockPeriod(rawItem);
                        valid = valid && childChecker.IsAllowChangeStatus(rawItem.IMP_MEST_STT_ID, raw.IMP_MEST_STT_ID);
                    }
                    children = list;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
        
    }
}
