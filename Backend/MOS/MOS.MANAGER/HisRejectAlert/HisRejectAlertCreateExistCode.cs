using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRejectAlert
{
    partial class HisRejectAlertCreate : BusinessBase
    {
		private List<HIS_REJECT_ALERT> recentHisRejectAlerts = new List<HIS_REJECT_ALERT>();
		
        internal HisRejectAlertCreate()
            : base()
        {

        }

        internal HisRejectAlertCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_REJECT_ALERT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRejectAlertCheck checker = new HisRejectAlertCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.REJECT_ALERT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisRejectAlertDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisRejectAlert_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisRejectAlert that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisRejectAlerts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisRejectAlerts))
            {
                if (!DAOWorker.HisRejectAlertDAO.TruncateList(this.recentHisRejectAlerts))
                {
                    LogSystem.Warn("Rollback du lieu HisRejectAlert that bai, can kiem tra lai." + LogUtil.TraceData("recentHisRejectAlerts", this.recentHisRejectAlerts));
                }
				this.recentHisRejectAlerts = null;
            }
        }
    }
}
