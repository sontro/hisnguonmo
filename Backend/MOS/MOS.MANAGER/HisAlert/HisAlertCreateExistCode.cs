using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAlert
{
    partial class HisAlertCreate : BusinessBase
    {
		private List<HIS_ALERT> recentHisAlerts = new List<HIS_ALERT>();
		
        internal HisAlertCreate()
            : base()
        {

        }

        internal HisAlertCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_ALERT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAlertCheck checker = new HisAlertCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ExistsCode(data.ALERT_CODE, null);
                if (valid)
                {
					if (!DAOWorker.HisAlertDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAlert_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAlert that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisAlerts.Add(data);
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
            if (IsNotNullOrEmpty(this.recentHisAlerts))
            {
                if (!DAOWorker.HisAlertDAO.TruncateList(this.recentHisAlerts))
                {
                    LogSystem.Warn("Rollback du lieu HisAlert that bai, can kiem tra lai." + LogUtil.TraceData("recentHisAlerts", this.recentHisAlerts));
                }
				this.recentHisAlerts = null;
            }
        }
    }
}
