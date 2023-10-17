using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaccExamResult
{
    partial class HisVaccExamResultCreate : BusinessBase
    {
		private List<HIS_VACC_EXAM_RESULT> recentHisVaccExamResults = new List<HIS_VACC_EXAM_RESULT>();
		
        internal HisVaccExamResultCreate()
            : base()
        {

        }

        internal HisVaccExamResultCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_VACC_EXAM_RESULT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccExamResultCheck checker = new HisVaccExamResultCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisVaccExamResultDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccExamResult_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccExamResult that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisVaccExamResults.Add(data);
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
		
		internal bool CreateList(List<HIS_VACC_EXAM_RESULT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisVaccExamResultCheck checker = new HisVaccExamResultCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisVaccExamResultDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisVaccExamResult_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisVaccExamResult that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisVaccExamResults.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisVaccExamResults))
            {
                if (!DAOWorker.HisVaccExamResultDAO.TruncateList(this.recentHisVaccExamResults))
                {
                    LogSystem.Warn("Rollback du lieu HisVaccExamResult that bai, can kiem tra lai." + LogUtil.TraceData("recentHisVaccExamResults", this.recentHisVaccExamResults));
                }
				this.recentHisVaccExamResults = null;
            }
        }
    }
}
