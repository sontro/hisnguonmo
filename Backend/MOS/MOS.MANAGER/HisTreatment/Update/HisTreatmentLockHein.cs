using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisHeinApproval;
using MOS.MANAGER.CodeGenerator.HisTreatment;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    /// <summary>
    /// Xu ly Duyet khoa/huy duyet khoa Bao hiem
    /// </summary>
    class HisTreatmentLockHein : BusinessBase
    {
        private HIS_TREATMENT beforeUpdateHisTreatmentDTO;

        internal HisTreatmentLockHein()
            : base()
        {

        }

        internal HisTreatmentLockHein(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        public bool LockHein(HisTreatmentLockHeinSDO data, ref HIS_TREATMENT resultData)
        {
            bool valid = true;
            HIS_TREATMENT raw = null;
            HisTreatmentCheck checker = new HisTreatmentCheck(param);
            valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
            valid = valid && checker.IsLock(raw);
            valid = valid && checker.IsUnLockHein(raw);
            if (valid)
            {
                if (this.ChangeLockHein(raw, true, data, ref resultData))
                {
                    new EventLogGenerator(EventLog.Enum.HisTreatment_KhoaHoSoBhyt)
                            .TreatmentCode(resultData.TREATMENT_CODE).Run();
                    return true;
                }
            }
            return false;
        }

        public bool LockHein(HIS_TREATMENT raw, ref HIS_TREATMENT resultData)
        {
            bool valid = true;
            HisTreatmentCheck checker = new HisTreatmentCheck(param);
            valid = valid && checker.IsUnLockHein(raw);
            if (valid)
            {
                if (this.ChangeLockHein(raw, true, null, ref resultData))
                {
                    new EventLogGenerator(EventLog.Enum.HisTreatment_KhoaHoSoBhyt)
                            .TreatmentCode(resultData.TREATMENT_CODE).Run();
                    return true;
                }
            }
            return false;
        }

        public bool UnlockHein(long treatmentId, ref HIS_TREATMENT resultData)
        {
            bool valid = true;
            HIS_TREATMENT raw = null;
            HisTreatmentCheck checker = new HisTreatmentCheck(param);
            valid = valid && checker.VerifyId(treatmentId, ref raw);
            valid = valid && checker.IsLockHein(raw);
            if (valid)
            {
                if (this.ChangeLockHein(raw, false, null, ref resultData))
                {
                    new EventLogGenerator(EventLog.Enum.HisTreatment_MoKhoaHoSoBhyt)
                            .TreatmentCode(resultData.TREATMENT_CODE).Run();
                    return true;
                }
            }
            return false;
        }

        private bool ChangeLockHein(HIS_TREATMENT raw, bool isLockHein, HisTreatmentLockHeinSDO sdo, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                this.beforeUpdateHisTreatmentDTO = Mapper.Map<HIS_TREATMENT>(raw);

                // Neu la khoa bao hiem
                if (isLockHein)
                {
                    raw.IS_LOCK_HEIN = new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE);
                    raw.HEIN_LOCK_TIME = sdo != null && sdo.HeinLockTime.HasValue ? sdo.HeinLockTime : Inventec.Common.DateTime.Get.Now();
                    if (HisTreatmentCFG.IS_GENERATE_STORE_BORDEREAU_CODE_WHEN_LOCK_HEIN && sdo != null && !String.IsNullOrWhiteSpace(sdo.StoreBordereauCode))
                    {
                        HisTreatmentFilterQuery filter = new HisTreatmentFilterQuery();
                        filter.HEIN_LOCK_TIME_FROM = Convert.ToInt64(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(raw.HEIN_LOCK_TIME.Value).Value.ToString("yyyyMMdd") + "000000");
                        filter.HEIN_LOCK_TIME_TO = Convert.ToInt64(Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(raw.HEIN_LOCK_TIME.Value).Value.ToString("yyyyMMdd") + "235959");
                        if (raw.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                        {
                            filter.IS_NOI_TRU_TREATMENT_TYPE = true;
                        }
                        else
                        {
                            filter.IS_NOI_TRU_TREATMENT_TYPE = false;
                        }

                        List<HIS_TREATMENT> treats = new HisTreatmentGet().Get(filter);

                        if (IsNotNullOrEmpty(treats) && treats.Exists(o => o.STORE_BORDEREAU_CODE == sdo.StoreBordereauCode))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTreatment_MaLuuTruDaDuocSuDung, sdo.StoreBordereauCode);
                            return false;
                        }
                        raw.STORE_BORDEREAU_CODE = sdo.StoreBordereauCode;
                    }
                }
                else
                {
                    raw.IS_LOCK_HEIN = null;
                    raw.HEIN_LOCK_TIME = null;
                }

                if (!DAOWorker.HisTreatmentDAO.Update(raw))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_TamKhoaThatBai);
                    throw new Exception("Tam khoa thong tin HisTreatment that bai." + LogUtil.TraceData("raw", raw));
                }
                resultData = raw;
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            if (this.beforeUpdateHisTreatmentDTO != null)
            {
                if (!DAOWorker.HisTreatmentDAO.Update(this.beforeUpdateHisTreatmentDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisTreatment that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatmentDTO", this.beforeUpdateHisTreatmentDTO));
                }
            }
        }
    }
}
