using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAlert
{
    partial class HisAlertUpdate : BusinessBase
    {
		private List<HIS_ALERT> beforeUpdateHisAlerts = new List<HIS_ALERT>();
		
        internal HisAlertUpdate()
            : base()
        {

        }

        internal HisAlertUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ALERT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAlertCheck checker = new HisAlertCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_ALERT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ALERT_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.HisAlertDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAlert_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAlert that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateHisAlerts.Add(raw);
                    
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

        internal bool UpdateList(List<HIS_ALERT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAlertCheck checker = new HisAlertCheck(param);
                List<HIS_ALERT> listRaw = new List<HIS_ALERT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.ALERT_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.HisAlertDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAlert_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAlert that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateHisAlerts.AddRange(listRaw);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisAlerts))
            {
                if (!DAOWorker.HisAlertDAO.UpdateList(this.beforeUpdateHisAlerts))
                {
                    LogSystem.Warn("Rollback du lieu HisAlert that bai, can kiem tra lai." + LogUtil.TraceData("HisAlerts", this.beforeUpdateHisAlerts));
                }
				this.beforeUpdateHisAlerts = null;
            }
        }
    }
}
