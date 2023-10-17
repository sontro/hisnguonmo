using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceCondition
{
    partial class HisServiceConditionUpdate : BusinessBase
    {
		private List<HIS_SERVICE_CONDITION> beforeUpdateHisServiceConditions = new List<HIS_SERVICE_CONDITION>();
		
        internal HisServiceConditionUpdate()
            : base()
        {

        }

        internal HisServiceConditionUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_SERVICE_CONDITION data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceConditionCheck checker = new HisServiceConditionCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_SERVICE_CONDITION raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.SERVICE_CONDITION_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisServiceConditionDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceCondition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceCondition that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisServiceConditions.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_SERVICE_CONDITION> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceConditionCheck checker = new HisServiceConditionCheck(param);
                List<HIS_SERVICE_CONDITION> listRaw = new List<HIS_SERVICE_CONDITION>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.SERVICE_CONDITION_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisServiceConditionDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisServiceCondition_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisServiceCondition that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisServiceConditions.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisServiceConditions))
            {
                if (!DAOWorker.HisServiceConditionDAO.UpdateList(this.beforeUpdateHisServiceConditions))
                {
                    LogSystem.Warn("Rollback du lieu HisServiceCondition that bai, can kiem tra lai." + LogUtil.TraceData("HisServiceConditions", this.beforeUpdateHisServiceConditions));
                }
				this.beforeUpdateHisServiceConditions = null;
            }
        }
    }
}
