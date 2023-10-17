using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisNumOrderBlock
{
    partial class HisNumOrderBlockUpdate : BusinessBase
    {
		private List<HIS_NUM_ORDER_BLOCK> beforeUpdateHisNumOrderBlocks = new List<HIS_NUM_ORDER_BLOCK>();
		
        internal HisNumOrderBlockUpdate()
            : base()
        {

        }

        internal HisNumOrderBlockUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_NUM_ORDER_BLOCK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_NUM_ORDER_BLOCK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {                    
					if (!DAOWorker.HisNumOrderBlockDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderBlock_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNumOrderBlock that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisNumOrderBlocks.Add(raw);
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

        internal bool UpdateList(List<HIS_NUM_ORDER_BLOCK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisNumOrderBlockCheck checker = new HisNumOrderBlockCheck(param);
                List<HIS_NUM_ORDER_BLOCK> listRaw = new List<HIS_NUM_ORDER_BLOCK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisNumOrderBlockDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisNumOrderBlock_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisNumOrderBlock that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisNumOrderBlocks.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisNumOrderBlocks))
            {
                if (!DAOWorker.HisNumOrderBlockDAO.UpdateList(this.beforeUpdateHisNumOrderBlocks))
                {
                    LogSystem.Warn("Rollback du lieu HisNumOrderBlock that bai, can kiem tra lai." + LogUtil.TraceData("HisNumOrderBlocks", this.beforeUpdateHisNumOrderBlocks));
                }
				this.beforeUpdateHisNumOrderBlocks = null;
            }
        }
    }
}
