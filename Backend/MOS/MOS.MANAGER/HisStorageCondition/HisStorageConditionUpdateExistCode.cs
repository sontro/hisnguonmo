using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisStorageCondition
{
    partial class HisStorageConditionUpdate : BusinessBase
    {
		private List<HIS_STORAGE_CONDITION> beforeUpdateHisStorageConditions = new List<HIS_STORAGE_CONDITION>();
		
        internal HisStorageConditionUpdate()
            : base()
        {

        }

        internal HisStorageConditionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_STORAGE_CONDITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisStorageConditionCheck checker = new HisStorageConditionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_STORAGE_CONDITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.STORAGE_CONDITION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisStorageConditionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStorageCondition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisStorageCondition that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisStorageConditions.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_STORAGE_CONDITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisStorageConditionCheck checker = new HisStorageConditionCheck(param);
                List<HIS_STORAGE_CONDITION> listRaw = new List<HIS_STORAGE_CONDITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.STORAGE_CONDITION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisStorageConditionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisStorageCondition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisStorageCondition that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisStorageConditions.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisStorageConditions))
            {
                if (!DAOWorker.HisStorageConditionDAO.UpdateList(this.beforeUpdateHisStorageConditions))
                {
                    LogSystem.Warn("Rollback du lieu HisStorageCondition that bai, can kiem tra lai." + LogUtil.TraceData("HisStorageConditions", this.beforeUpdateHisStorageConditions));
                }
				this.beforeUpdateHisStorageConditions = null;
            }
        }
    }
}
