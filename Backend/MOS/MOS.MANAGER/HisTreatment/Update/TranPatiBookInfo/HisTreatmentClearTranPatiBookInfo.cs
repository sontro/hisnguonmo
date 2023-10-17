using AutoMapper;
using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.TranPatiBookInfo
{
    class HisTreatmentClearTranPatiBookInfo : BusinessBase
    {
        HIS_TREATMENT oldTreatment = null;

        internal HisTreatmentClearTranPatiBookInfo()
            : base()
        {
        }

        internal HisTreatmentClearTranPatiBookInfo(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(long treatmentId)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                bool valid = true;
                valid = valid && IsGreaterThanZero(treatmentId);
                valid = valid && checker.VerifyId(treatmentId, ref raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    this.oldTreatment = Mapper.Map<HIS_TREATMENT>(raw);

                    raw.TRAN_PATI_BOOK_TIME = null;
                    raw.TRAN_PATI_DOCTOR_LOGINNAME = null;
                    raw.TRAN_PATI_DOCTOR_USERNAME = null;
                    raw.TRAN_PATI_DEPARTMENT_LOGINNAME = null;
                    raw.TRAN_PATI_DEPARTMENT_USERNAME = null;
                    raw.TRAN_PATI_HOSPITAL_LOGINNAME = null;
                    raw.TRAN_PATI_HOSPITAL_USERNAME = null;
                    raw.TRAN_PATI_BOOK_NUMBER = null;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin quan ly chuyen vien that bai." + LogUtil.TraceData("data", raw));
                    }

                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisTreatment_XoaSoLuuTruThongTinGiayChuyenVien)
                        .TreatmentCode(raw.TREATMENT_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Rollback()
        {
            if (IsNotNull(this.oldTreatment))
            {
                if (!DAOWorker.HisTreatmentDAO.Update(this.oldTreatment))
                {
                    LogSystem.Warn("Rollback cap nhat thong tin quan ly chuyen vien that bai." + LogUtil.TraceData("data", this.oldTreatment));
                }
                this.oldTreatment = null;
            }
        }
    }
}
