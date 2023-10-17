using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisRejectAlert
{
    partial class HisRejectAlertUpdate : BusinessBase
    {
		private List<HIS_REJECT_ALERT> beforeUpdateHisRejectAlerts = new List<HIS_REJECT_ALERT>();
		
        internal HisRejectAlertUpdate()
            : base()
        {

        }

        internal HisRejectAlertUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_REJECT_ALERT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRejectAlertCheck checker = new HisRejectAlertCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_REJECT_ALERT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.REJECT_ALERT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisRejectAlertDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRejectAlert_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRejectAlert that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisRejectAlerts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_REJECT_ALERT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisRejectAlertCheck checker = new HisRejectAlertCheck(param);
                List<HIS_REJECT_ALERT> listRaw = new List<HIS_REJECT_ALERT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.REJECT_ALERT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisRejectAlertDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRejectAlert_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisRejectAlert that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisRejectAlerts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisRejectAlerts))
            {
                if (!DAOWorker.HisRejectAlertDAO.UpdateList(this.beforeUpdateHisRejectAlerts))
                {
                    LogSystem.Warn("Rollback du lieu HisRejectAlert that bai, can kiem tra lai." + LogUtil.TraceData("HisRejectAlerts", this.beforeUpdateHisRejectAlerts));
                }
				this.beforeUpdateHisRejectAlerts = null;
            }
        }
    }
}
