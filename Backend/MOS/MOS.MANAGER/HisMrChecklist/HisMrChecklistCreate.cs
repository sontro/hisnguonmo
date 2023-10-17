using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrChecklist
{
    partial class HisMrChecklistCreate : BusinessBase
    {
		private List<HIS_MR_CHECKLIST> recentHisMrChecklists = new List<HIS_MR_CHECKLIST>();
		
        internal HisMrChecklistCreate()
            : base()
        {

        }

        internal HisMrChecklistCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_MR_CHECKLIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisMrChecklistDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrChecklist_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMrChecklist that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisMrChecklists.Add(data);
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
		
		internal bool CreateList(List<HIS_MR_CHECKLIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisMrChecklistDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrChecklist_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisMrChecklist that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisMrChecklists.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisMrChecklists))
            {
                if (!DAOWorker.HisMrChecklistDAO.TruncateList(this.recentHisMrChecklists))
                {
                    LogSystem.Warn("Rollback du lieu HisMrChecklist that bai, can kiem tra lai." + LogUtil.TraceData("recentHisMrChecklists", this.recentHisMrChecklists));
                }
				this.recentHisMrChecklists = null;
            }
        }
    }
}
