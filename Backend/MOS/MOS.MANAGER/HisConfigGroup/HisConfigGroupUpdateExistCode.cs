using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisConfigGroup
{
    partial class HisConfigGroupUpdate : BusinessBase
    {
		private List<HIS_CONFIG_GROUP> beforeUpdateHisConfigGroups = new List<HIS_CONFIG_GROUP>();
		
        internal HisConfigGroupUpdate()
            : base()
        {

        }

        internal HisConfigGroupUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CONFIG_GROUP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisConfigGroupCheck checker = new HisConfigGroupCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CONFIG_GROUP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CONFIG_GROUP_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisConfigGroupDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisConfigGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisConfigGroup that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisConfigGroups.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_CONFIG_GROUP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisConfigGroupCheck checker = new HisConfigGroupCheck(param);
                List<HIS_CONFIG_GROUP> listRaw = new List<HIS_CONFIG_GROUP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CONFIG_GROUP_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisConfigGroupDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisConfigGroup_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisConfigGroup that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisConfigGroups.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisConfigGroups))
            {
                if (!DAOWorker.HisConfigGroupDAO.UpdateList(this.beforeUpdateHisConfigGroups))
                {
                    LogSystem.Warn("Rollback du lieu HisConfigGroup that bai, can kiem tra lai." + LogUtil.TraceData("HisConfigGroups", this.beforeUpdateHisConfigGroups));
                }
				this.beforeUpdateHisConfigGroups = null;
            }
        }
    }
}
