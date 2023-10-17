using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMestInventory
{
    partial class HisMestInventoryUpdate : BusinessBase
    {
		private List<HIS_MEST_INVENTORY> beforeUpdateHisMestInventorys = new List<HIS_MEST_INVENTORY>();
		
        internal HisMestInventoryUpdate()
            : base()
        {

        }

        internal HisMestInventoryUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_MEST_INVENTORY data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisMestInventoryCheck checker = new HisMestInventoryCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_MEST_INVENTORY raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisMestInventorys.Add(raw);
					if (!DAOWorker.HisMestInventoryDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInventory_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestInventory that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        internal bool UpdateList(List<HIS_MEST_INVENTORY> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisMestInventoryCheck checker = new HisMestInventoryCheck(param);
                List<HIS_MEST_INVENTORY> listRaw = new List<HIS_MEST_INVENTORY>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisMestInventorys.AddRange(listRaw);
					if (!DAOWorker.HisMestInventoryDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMestInventory_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMestInventory that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMestInventorys))
            {
                if (!new HisMestInventoryUpdate(param).UpdateList(this.beforeUpdateHisMestInventorys))
                {
                    LogSystem.Warn("Rollback du lieu HisMestInventory that bai, can kiem tra lai." + LogUtil.TraceData("HisMestInventorys", this.beforeUpdateHisMestInventorys));
                }
            }
        }
    }
}
