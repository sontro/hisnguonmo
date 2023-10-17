using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCacheMonitor
{
    partial class HisCacheMonitorUpdate : BusinessBase
    {
		private List<HIS_CACHE_MONITOR> beforeUpdateHisCacheMonitors = new List<HIS_CACHE_MONITOR>();
		
        internal HisCacheMonitorUpdate()
            : base()
        {

        }

        internal HisCacheMonitorUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_CACHE_MONITOR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCacheMonitorCheck checker = new HisCacheMonitorCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_CACHE_MONITOR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisCacheMonitors.Add(raw);
					if (!DAOWorker.HisCacheMonitorDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCacheMonitor_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCacheMonitor that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_CACHE_MONITOR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCacheMonitorCheck checker = new HisCacheMonitorCheck(param);
                List<HIS_CACHE_MONITOR> listRaw = new List<HIS_CACHE_MONITOR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisCacheMonitors.AddRange(listRaw);
					if (!DAOWorker.HisCacheMonitorDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCacheMonitor_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisCacheMonitor that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisCacheMonitors))
            {
                if (!DAOWorker.HisCacheMonitorDAO.UpdateList(this.beforeUpdateHisCacheMonitors))
                {
                    LogSystem.Warn("Rollback du lieu HisCacheMonitor that bai, can kiem tra lai." + LogUtil.TraceData("HisCacheMonitors", this.beforeUpdateHisCacheMonitors));
                }
            }
        }
    }
}
