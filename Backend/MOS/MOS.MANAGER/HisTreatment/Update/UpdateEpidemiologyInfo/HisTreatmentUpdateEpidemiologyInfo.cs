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

namespace MOS.MANAGER.HisTreatment.Update.UpdateEpidemiologyInfo
{
    class HisTreatmentUpdateEpidemiologyInfo : BusinessBase
    {

        private List<HIS_TREATMENT> recentTreatments = null;

        internal HisTreatmentUpdateEpidemiologyInfo()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentUpdateEpidemiologyInfo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.recentTreatments = new List<HIS_TREATMENT>();
        }

        internal bool Run(EpidemiologyInfoSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisTreatmentUpdateEpidemiologyInfoCheck checker = new HisTreatmentUpdateEpidemiologyInfoCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsValid(data);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                if (valid)
                {
                    this.recentTreatments.Add(treatment);

                    // Update treatment
                    treatment.VACCINE_ID = data.VaccineId;
                    treatment.VACINATION_ORDER = data.VaccinationOrder;
                    treatment.EPIDEMILOGY_CONTACT_TYPE = data.EpidemiologyContactType;
                    treatment.EPIDEMILOGY_SYMPTOM = data.EpidemiologySympton;
                    treatment.COVID_PATIENT_CODE = data.CovidPatientCode;

                    if (!DAOWorker.HisTreatmentDAO.Update(treatment))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    result = true;
                    resultData = treatment;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.recentTreatments))
                {
                    if (!DAOWorker.HisTreatmentDAO.UpdateList(this.recentTreatments))
                    {
                        LogSystem.Warn("Rollback du lieu HisTreatment that bai, can kiem tra lai." + LogUtil.TraceData("HisTreatment", this.recentTreatments));
                    }
                    this.recentTreatments = null;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
