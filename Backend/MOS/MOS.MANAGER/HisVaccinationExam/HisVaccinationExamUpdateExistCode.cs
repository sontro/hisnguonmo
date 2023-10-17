using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamUpdate : BusinessBase
    {
		private List<HIS_VACCINATION_EXAM> beforeUpdateHisVaccinationExams = new List<HIS_VACCINATION_EXAM>();
		
        internal HisVaccinationExamUpdate()
            : base()
        {

        }

        internal HisVaccinationExamUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_VACCINATION_EXAM data, HIS_VACCINATION_EXAM before)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationExamCheck checker = new HisVaccinationExamCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsUnLock(before);
                if (valid)
                {
					if (!DAOWorker.HisVaccinationExamDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccinationExam_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisVaccinationExam that bai." + LogUtil.TraceData("data", data));
                    }

                    this.beforeUpdateHisVaccinationExams.Add(before);
                    
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisVaccinationExams))
            {
                if (!DAOWorker.HisVaccinationExamDAO.UpdateList(this.beforeUpdateHisVaccinationExams))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccinationExam that bai, can kiem tra lai." + LogUtil.TraceData("HisVaccinationExams", this.beforeUpdateHisVaccinationExams));
                }
				this.beforeUpdateHisVaccinationExams = null;
            }
        }
    }
}
