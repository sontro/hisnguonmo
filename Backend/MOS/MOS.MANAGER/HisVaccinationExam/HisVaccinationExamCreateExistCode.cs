using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamCreate : BusinessBase
    {
		private List<HIS_VACCINATION_EXAM> recentHisVaccinationExams = new List<HIS_VACCINATION_EXAM>();
		
        internal HisVaccinationExamCreate()
            : base()
        {

        }

        internal HisVaccinationExamCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACCINATION_EXAM data, HIS_PATIENT patient)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.CheckPatientType(data);
                valid = valid && checker.ExistsCode(data.VACCINATION_EXAM_CODE, null);
                if (valid)
                {
                    HisVaccinationExamUtil.SetTdl(data, patient);
					if (!DAOWorker.HisVaccinationExamDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationExam_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccinationExam that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccinationExams.Add(data);
                    result = true;
                }
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
            if (IsNotNullOrEmpty(this.recentHisVaccinationExams))
            {
                if (!DAOWorker.HisVaccinationExamDAO.TruncateList(this.recentHisVaccinationExams))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationExam that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccinationExams", this.recentHisVaccinationExams));
                }
				this.recentHisVaccinationExams = null;
            }
        }
    }
}
