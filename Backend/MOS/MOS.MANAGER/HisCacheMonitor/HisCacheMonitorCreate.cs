using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCacheMonitor
{
    partial class HisCacheMonitorCreate : BusinessBase
    {
		private List<HIS_CACHE_MONITOR> recentHisCacheMonitors = new List<HIS_CACHE_MONITOR>();
		
        internal HisCacheMonitorCreate()
            : base()
        {

        }

        internal HisCacheMonitorCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_CACHE_MONITOR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCacheMonitorCheck checker = new HisCacheMonitorCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
					if (!DAOWorker.HisCacheMonitorDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCacheMonitor_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCacheMonitor that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisCacheMonitors.Add(data);
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
		
		internal bool CreateList(List<HIS_CACHE_MONITOR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCacheMonitorCheck checker = new HisCacheMonitorCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisCacheMonitorDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisCacheMonitor_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisCacheMonitor that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisCacheMonitors.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisCacheMonitors))
            {
                if (!new HisCacheMonitorTruncate(param).TruncateList(this.recentHisCacheMonitors))
                {
                    LogSystem.Warn("Rollback du lieu HisCacheMonitor that bai, can kiem tra lai." + LogUtil.TraceData("recentHisCacheMonitors", this.recentHisCacheMonitors));
                }
            }
        }
    }
}
