using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrCheckItem
{
    partial class HisMrCheckItemUpdate : BusinessBase
    {
		private List<HIS_MR_CHECK_ITEM> beforeUpdateHisMrCheckItems = new List<HIS_MR_CHECK_ITEM>();
		
        internal HisMrCheckItemUpdate()
            : base()
        {

        }

        internal HisMrCheckItemUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MR_CHECK_ITEM data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckItemCheck checker = new HisMrCheckItemCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MR_CHECK_ITEM raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMrCheckItemDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItem_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrCheckItem that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMrCheckItems.Add(raw);
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

        internal bool UpdateList(List<HIS_MR_CHECK_ITEM> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrCheckItemCheck checker = new HisMrCheckItemCheck(param);
                List<HIS_MR_CHECK_ITEM> listRaw = new List<HIS_MR_CHECK_ITEM>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMrCheckItemDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItem_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrCheckItem that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMrCheckItems.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMrCheckItems))
            {
                if (!DAOWorker.HisMrCheckItemDAO.UpdateList(this.beforeUpdateHisMrCheckItems))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckItem that bai, can kiem tra lai." + LogUtil.TraceData("HisMrCheckItems", this.beforeUpdateHisMrCheckItems));
                }
				this.beforeUpdateHisMrCheckItems = null;
            }
        }
    }
}
