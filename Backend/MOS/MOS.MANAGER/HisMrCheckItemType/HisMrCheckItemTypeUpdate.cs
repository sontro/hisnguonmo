using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeUpdate : BusinessBase
    {
		private List<HIS_MR_CHECK_ITEM_TYPE> beforeUpdateHisMrCheckItemTypes = new List<HIS_MR_CHECK_ITEM_TYPE>();
		
        internal HisMrCheckItemTypeUpdate()
            : base()
        {

        }

        internal HisMrCheckItemTypeUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MR_CHECK_ITEM_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMrCheckItemTypeCheck checker = new HisMrCheckItemTypeCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MR_CHECK_ITEM_TYPE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {                    
					if (!DAOWorker.HisMrCheckItemTypeDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItemType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrCheckItemType that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisMrCheckItemTypes.Add(raw);
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

        internal bool UpdateList(List<HIS_MR_CHECK_ITEM_TYPE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMrCheckItemTypeCheck checker = new HisMrCheckItemTypeCheck(param);
                List<HIS_MR_CHECK_ITEM_TYPE> listRaw = new List<HIS_MR_CHECK_ITEM_TYPE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisMrCheckItemTypeDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMrCheckItemType_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMrCheckItemType that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisMrCheckItemTypes.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMrCheckItemTypes))
            {
                if (!DAOWorker.HisMrCheckItemTypeDAO.UpdateList(this.beforeUpdateHisMrCheckItemTypes))
                {
                    LogSystem.Warn("Rollback du lieu HisMrCheckItemType that bai, can kiem tra lai." + LogUtil.TraceData("HisMrCheckItemTypes", this.beforeUpdateHisMrCheckItemTypes));
                }
				this.beforeUpdateHisMrCheckItemTypes = null;
            }
        }
    }
}
