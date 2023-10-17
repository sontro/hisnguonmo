using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInteractiveGrade
{
    partial class HisInteractiveGradeUpdate : BusinessBase
    {
		private List<HIS_INTERACTIVE_GRADE> beforeUpdateHisInteractiveGrades = new List<HIS_INTERACTIVE_GRADE>();
		
        internal HisInteractiveGradeUpdate()
            : base()
        {

        }

        internal HisInteractiveGradeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_INTERACTIVE_GRADE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInteractiveGradeCheck checker = new HisInteractiveGradeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_INTERACTIVE_GRADE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.INTERACTIVE_GRADE_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisInteractiveGradeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInteractiveGrade_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInteractiveGrade that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisInteractiveGrades.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_INTERACTIVE_GRADE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInteractiveGradeCheck checker = new HisInteractiveGradeCheck(param);
                List<HIS_INTERACTIVE_GRADE> listRaw = new List<HIS_INTERACTIVE_GRADE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.INTERACTIVE_GRADE_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisInteractiveGradeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInteractiveGrade_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInteractiveGrade that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisInteractiveGrades.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisInteractiveGrades))
            {
                if (!DAOWorker.HisInteractiveGradeDAO.UpdateList(this.beforeUpdateHisInteractiveGrades))
                {
                    LogSystem.Warn("Rollback du lieu HisInteractiveGrade that bai, can kiem tra lai." + LogUtil.TraceData("HisInteractiveGrades", this.beforeUpdateHisInteractiveGrades));
                }
				this.beforeUpdateHisInteractiveGrades = null;
            }
        }
    }
}
