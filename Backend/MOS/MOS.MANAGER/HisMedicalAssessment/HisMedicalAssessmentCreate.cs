using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicalAssessment
{
    partial class HisMedicalAssessmentCreate : BusinessBase
    {
		private List<HIS_MEDICAL_ASSESSMENT> recentHisMedicalAssessments = new List<HIS_MEDICAL_ASSESSMENT>();
		
        internal HisMedicalAssessmentCreate()
            : base()
        {

        }

        internal HisMedicalAssessmentCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MEDICAL_ASSESSMENT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMedicalAssessmentDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalAssessment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicalAssessment that bai." + LogUtil.TraceData("data", data));
                    }

                    this.recentHisMedicalAssessments.Add(data);
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
		
		internal bool CreateList(List<HIS_MEDICAL_ASSESSMENT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMedicalAssessmentCheck checker = new HisMedicalAssessmentCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMedicalAssessmentDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicalAssessment_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMedicalAssessment that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMedicalAssessments.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMedicalAssessments))
            {
                if (!DAOWorker.HisMedicalAssessmentDAO.TruncateList(this.recentHisMedicalAssessments))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicalAssessment that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMedicalAssessments", this.recentHisMedicalAssessments));
                }
				this.recentHisMedicalAssessments = null;
            }
        }
    }
}
