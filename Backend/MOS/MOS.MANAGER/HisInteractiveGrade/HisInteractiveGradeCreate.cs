using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInteractiveGrade
{
    partial class HisInteractiveGradeCreate : BusinessBase
    {
		private List<HIS_INTERACTIVE_GRADE> recentHisInteractiveGrades = new List<HIS_INTERACTIVE_GRADE>();
		
        internal HisInteractiveGradeCreate()
            : base()
        {

        }

        internal HisInteractiveGradeCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_INTERACTIVE_GRADE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInteractiveGradeCheck checker = new HisInteractiveGradeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsValidInteractiveGrade(data.INTERACTIVE_GRADE);
                if (valid)
                {
					if (!DAOWorker.HisInteractiveGradeDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInteractiveGrade_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInteractiveGrade that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisInteractiveGrades.Add(data);
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
		
		internal bool CreateList(List<HIS_INTERACTIVE_GRADE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInteractiveGradeCheck checker = new HisInteractiveGradeCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsValidInteractiveGrade(data.INTERACTIVE_GRADE);
                }
                if (valid)
                {
                    if (!DAOWorker.HisInteractiveGradeDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInteractiveGrade_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInteractiveGrade that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisInteractiveGrades.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisInteractiveGrades))
            {
                if (!DAOWorker.HisInteractiveGradeDAO.TruncateList(this.recentHisInteractiveGrades))
                {
                    LogSystem.Warn("Rollback du lieu HisInteractiveGrade that bai, can kiem tra lai." + LogUtil.TraceData("recentHisInteractiveGrades", this.recentHisInteractiveGrades));
                }
				this.recentHisInteractiveGrades = null;
            }
        }
    }
}
