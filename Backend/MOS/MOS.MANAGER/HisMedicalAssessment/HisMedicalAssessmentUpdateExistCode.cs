using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicalAssessment
{
    partial class HisMedicalAssessmentUpdate : BusinessBase
    {
		private List<HIS_MEDICAL_ASSESSMENT> beforeUpdateHisMedicalAssessments = new List<HIS_MEDICAL_ASSESSMENT>();
		
        internal HisMedicalAssessmentUpdate()
            : base()
        {

        }

        internal HisMedicalAssessmentUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEDICAL_ASSESSMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEDICAL_ASSESSMENT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MEDICAL_ASSESSMENT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMedicalAssessmentDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalAssessment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicalAssessment that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMedicalAssessments.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MEDICAL_ASSESSMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);
                List<HIS_MEDICAL_ASSESSMENT> listRaw = new List<HIS_MEDICAL_ASSESSMENT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MEDICAL_ASSESSMENT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMedicalAssessmentDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalAssessment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicalAssessment that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMedicalAssessments.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicalAssessments))
            {
                if (!DAOWorker.HisMedicalAssessmentDAO.UpdateList(this.beforeUpdateHisMedicalAssessments))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicalAssessment that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicalAssessments", this.beforeUpdateHisMedicalAssessments));
                }
				this.beforeUpdateHisMedicalAssessments = null;
            }
        }
    }
}
