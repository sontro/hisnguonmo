using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrChecklist
{
    partial class HisMrChecklistUpdate : BusinessBase
    {
		private List<HIS_MR_CHECKLIST> beforeUpdateHisMrChecklists = new List<HIS_MR_CHECKLIST>();
		
        internal HisMrChecklistUpdate()
            : base()
        {

        }

        internal HisMrChecklistUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MR_CHECKLIST data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MR_CHECKLIST raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.MR_CHECKLIST_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisMrChecklistDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrChecklist_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrChecklist that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisMrChecklists.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_MR_CHECKLIST> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrChecklistCheck checker = new HisMrChecklistCheck(param);
                List<HIS_MR_CHECKLIST> listRaw = new List<HIS_MR_CHECKLIST>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.MR_CHECKLIST_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisMrChecklistDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrChecklist_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrChecklist that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisMrChecklists.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMrChecklists))
            {
                if (!DAOWorker.HisMrChecklistDAO.UpdateList(this.beforeUpdateHisMrChecklists))
                {
                    LogSystem.Warn("Rollback du lieu HisMrChecklist that bai, can kiem tra lai." + LogUtil.TraceData("HisMrChecklists", this.beforeUpdateHisMrChecklists));
                }
				this.beforeUpdateHisMrChecklists = null;
            }
        }
    }
}
