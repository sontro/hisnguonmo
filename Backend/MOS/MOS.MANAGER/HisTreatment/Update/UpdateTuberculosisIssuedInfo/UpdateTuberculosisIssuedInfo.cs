using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatment.Update.UpdateTuberculosisIssuedInfo
{
    class UpdateTuberculosisIssuedInfo: BusinessBase
    {
        private List<HIS_TREATMENT> recentTreatments = null;

        internal UpdateTuberculosisIssuedInfo()
            : base()
        {
            this.Init();
        }

        internal UpdateTuberculosisIssuedInfo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.recentTreatments = new List<HIS_TREATMENT>();
        }

        internal bool Run(HisTreatmentTuberculosisIssuedInfoSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HIS_MEDI_ORG mediOrg = null;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                UpdateTuberculosisIssuedInfoCheck checker = new UpdateTuberculosisIssuedInfoCheck(param);
                valid = valid && checker.IsNotDate(data);
                valid = valid && checker.IsNotId(data, ref mediOrg);
                valid = valid && checker.IsNotTreatmentId(data, ref treatment);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                if (valid)
                {
                    this.recentTreatments.Add(treatment);

                    if (!string.IsNullOrEmpty(data.TuberculosisIssuedOrgCode))
                    {
                        treatment.TUBERCULOSIS_ISSUED_ORG_CODE = data.TuberculosisIssuedOrgCode;
                        treatment.TUBERCULOSIS_ISSUED_ORG_NAME = mediOrg.MEDI_ORG_NAME;
                        treatment.TUBERCULOSIS_ISSUED_DATE = data.TuberculosisIssuedDate;
                    }
                    else
                    {
                        treatment.TUBERCULOSIS_ISSUED_ORG_CODE = null;
                        treatment.TUBERCULOSIS_ISSUED_ORG_NAME = null;
                        treatment.TUBERCULOSIS_ISSUED_DATE = null;
                    }
                    string tciDate = Inventec.Common.DateTime.Convert.TimeNumberToDateString(treatment.TUBERCULOSIS_ISSUED_DATE.Value);
                    HisTreatmentLog.Run(treatment, EventLog.Enum.HisTreatment_CapNhatThongTinGiayXacNhanBenhLao, treatment.TUBERCULOSIS_ISSUED_ORG_CODE,treatment.TUBERCULOSIS_ISSUED_ORG_NAME,tciDate);
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
