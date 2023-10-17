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
    class HisTreatmentSetTranPatiBookInfo : BusinessBase
    {
        HIS_TREATMENT oldTreatment = null;
        internal HisTreatmentSetTranPatiBookInfo()
            : base()
        {
        }

        internal HisTreatmentSetTranPatiBookInfo(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(HisTreatmentSetTranPatiBookSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT raw = null;
                HisTreatmentCheckTranPatiBookInfo tranPatiChecker = new HisTreatmentCheckTranPatiBookInfo(param);
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                bool valid = true;
                valid = valid && tranPatiChecker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.TreatmentId, ref raw);
                valid = valid && tranPatiChecker.HasOutTime(raw);
                valid = valid && tranPatiChecker.IsEndTransType(raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                    this.oldTreatment = Mapper.Map<HIS_TREATMENT>(raw);
                    raw.TRAN_PATI_BOOK_TIME = data.TranPatiBookTime;
                    raw.TRAN_PATI_DOCTOR_LOGINNAME = data.TranPatiDoctorLoginname;
                    raw.TRAN_PATI_DOCTOR_USERNAME = data.TranPatiDoctorUsername;
                    raw.TRAN_PATI_DEPARTMENT_LOGINNAME = data.TranPatiDepartmentLoginname;
                    raw.TRAN_PATI_DEPARTMENT_USERNAME = data.TranPatiDepartmentUsername;
                    raw.TRAN_PATI_HOSPITAL_LOGINNAME = data.TranPatiHospitalLoginname;
                    raw.TRAN_PATI_HOSPITAL_USERNAME = data.TranPatiHospitalUsername;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransfusionSum_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin quan ly chuyen vien that bai." + LogUtil.TraceData("data", raw));
                    }

                    resultData = new HisTreatmentGet().GetById(raw.ID);

                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisTreatment_VaoSoLuuTruThongTinGiayChuyenVien)
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
