using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.Token;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisConfig
{
    partial class HisConfigUpdate : BusinessBase
    {
		private List<HIS_CONFIG> beforeUpdateHisConfigs = new List<HIS_CONFIG>();
		
        internal HisConfigUpdate()
            : base()
        {

        }

        internal HisConfigUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisConfigCheck checker = new HisConfigCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CONFIG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.KEY, data.BRANCH_ID, data.ID);
                if (valid)
                {
                    this.beforeUpdateHisConfigs.Add(raw);
					if (!DAOWorker.HisConfigDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisConfig_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisConfig that bai." + LogUtil.TraceData("data", data));
                    }
                    
                    result = true;

                    HisConfigLog.Run(data, raw, LibraryEventLog.EventLog.Enum.HisConfig_Sua);
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

        internal bool UpdateList(List<HIS_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisConfigCheck checker = new HisConfigCheck(param);
                List<HIS_CONFIG> listRaw = new List<HIS_CONFIG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.KEY, data.BRANCH_ID, data.ID);
                }
                if (valid)
                {
					this.beforeUpdateHisConfigs.AddRange(listRaw);
					if (!DAOWorker.HisConfigDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisConfig_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisConfig that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisConfigs))
            {
                if (!DAOWorker.HisConfigDAO.UpdateList(this.beforeUpdateHisConfigs))
                {
                    LogSystem.Warn("Rollback du lieu HisConfig that bai, can kiem tra lai." + LogUtil.TraceData("HisConfigs", this.beforeUpdateHisConfigs));
                }
            }
        }
    }
}
