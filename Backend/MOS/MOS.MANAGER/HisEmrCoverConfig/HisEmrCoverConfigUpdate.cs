using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisEmrCoverConfig
{
    partial class HisEmrCoverConfigUpdate : BusinessBase
    {
		private List<HIS_EMR_COVER_CONFIG> beforeUpdateHisEmrCoverConfigs = new List<HIS_EMR_COVER_CONFIG>();
		
        internal HisEmrCoverConfigUpdate()
            : base()
        {

        }

        internal HisEmrCoverConfigUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_EMR_COVER_CONFIG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisEmrCoverConfigCheck checker = new HisEmrCoverConfigCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_EMR_COVER_CONFIG raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotExists(data);
                if (valid)
                {                    
					if (!DAOWorker.HisEmrCoverConfigDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrCoverConfig_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmrCoverConfig that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisEmrCoverConfigs.Add(raw);
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

        internal bool UpdateList(List<HIS_EMR_COVER_CONFIG> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisEmrCoverConfigCheck checker = new HisEmrCoverConfigCheck(param);
                List<HIS_EMR_COVER_CONFIG> listRaw = new List<HIS_EMR_COVER_CONFIG>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.IsNotExists(data);
                }
                if (valid)
                {
					if (!DAOWorker.HisEmrCoverConfigDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisEmrCoverConfig_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisEmrCoverConfig that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisEmrCoverConfigs.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisEmrCoverConfigs))
            {
                if (!DAOWorker.HisEmrCoverConfigDAO.UpdateList(this.beforeUpdateHisEmrCoverConfigs))
                {
                    LogSystem.Warn("Rollback du lieu HisEmrCoverConfig that bai, can kiem tra lai." + LogUtil.TraceData("HisEmrCoverConfigs", this.beforeUpdateHisEmrCoverConfigs));
                }
				this.beforeUpdateHisEmrCoverConfigs = null;
            }
        }
    }
}
