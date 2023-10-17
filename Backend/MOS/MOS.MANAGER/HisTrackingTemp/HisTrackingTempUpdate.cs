using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTrackingTemp
{
    partial class HisTrackingTempUpdate : BusinessBase
    {
		private List<HIS_TRACKING_TEMP> beforeUpdateHisTrackingTemps = new List<HIS_TRACKING_TEMP>();
		
        internal HisTrackingTempUpdate()
            : base()
        {

        }

        internal HisTrackingTempUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_TRACKING_TEMP data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTrackingTempCheck checker = new HisTrackingTempCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_TRACKING_TEMP raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.TRACKING_TEMP_CODE, data.ID);
                if (valid)
                {                    
					if (!DAOWorker.HisTrackingTempDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTrackingTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTrackingTemp that bai." + LogUtil.TraceData("data", data));
                    }
					this.beforeUpdateHisTrackingTemps.Add(raw);
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

        internal bool UpdateList(List<HIS_TRACKING_TEMP> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTrackingTempCheck checker = new HisTrackingTempCheck(param);
                List<HIS_TRACKING_TEMP> listRaw = new List<HIS_TRACKING_TEMP>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.TRACKING_TEMP_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisTrackingTempDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTrackingTemp_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTrackingTemp that bai." + LogUtil.TraceData("listData", listData));
                    }
					
					this.beforeUpdateHisTrackingTemps.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisTrackingTemps))
            {
                if (!DAOWorker.HisTrackingTempDAO.UpdateList(this.beforeUpdateHisTrackingTemps))
                {
                    LogSystem.Warn("Rollback du lieu HisTrackingTemp that bai, can kiem tra lai." + LogUtil.TraceData("HisTrackingTemps", this.beforeUpdateHisTrackingTemps));
                }
				this.beforeUpdateHisTrackingTemps = null;
            }
        }
    }
}
