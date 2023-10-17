using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceNumOrder
{
    partial class HisServiceNumOrderUpdate : BusinessBase
    {
		private List<HIS_SERVICE_NUM_ORDER> beforeUpdateHisServiceNumOrders = new List<HIS_SERVICE_NUM_ORDER>();
		
        internal HisServiceNumOrderUpdate()
            : base()
        {

        }

        internal HisServiceNumOrderUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_NUM_ORDER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceNumOrderCheck checker = new HisServiceNumOrderCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_NUM_ORDER raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExsists(data);
                if (valid)
                {                    
					if (!DAOWorker.HisServiceNumOrderDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceNumOrder_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceNumOrder that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisServiceNumOrders.Add(raw);
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

        internal bool UpdateList(List<HIS_SERVICE_NUM_ORDER> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceNumOrderCheck checker = new HisServiceNumOrderCheck(param);
                List<HIS_SERVICE_NUM_ORDER> listRaw = new List<HIS_SERVICE_NUM_ORDER>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExsists(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceNumOrderDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceNumOrder_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceNumOrder that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisServiceNumOrders.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceNumOrders))
            {
                if (!DAOWorker.HisServiceNumOrderDAO.UpdateList(this.beforeUpdateHisServiceNumOrders))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceNumOrder that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceNumOrders", this.beforeUpdateHisServiceNumOrders));
                }
				this.beforeUpdateHisServiceNumOrders = null;
            }
        }
    }
}
