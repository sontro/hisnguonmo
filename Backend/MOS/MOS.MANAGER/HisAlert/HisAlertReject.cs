using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisRoom;
using MOS.PubSub;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisAlert
{
    class HisAlertReject: BusinessBase
    {
        private List<HIS_REJECT_ALERT> recentHisRejectAlerts = new List<HIS_REJECT_ALERT>();
        internal HisAlertReject()
            : base()
        {
            this.Init();
        }

        internal HisAlertReject(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
        }

        /// <summary>
        /// từ chối báo động
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAlertCheck checker = new HisAlertCheck(param);
                valid = valid && checker.VerifyRequireField2(id);

                if (valid)
                {
                    HIS_REJECT_ALERT rejectAlert = new HIS_REJECT_ALERT();
                    rejectAlert.ALERT_ID = id;
                    rejectAlert.REJECTER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    rejectAlert.REJECTER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    HIS_EMPLOYEE employee = HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == rejectAlert.REJECTER_LOGINNAME);
                    rejectAlert.DEPARTMENT_ID = employee.DEPARTMENT_ID;

                    if (!DAOWorker.HisRejectAlertDAO.Create(rejectAlert))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAlert_CapNhatThatBai);
                        throw new Exception("Tao thong tin HisRejectAlert that bai." + LogUtil.TraceData("data", rejectAlert));
                    }
                    this.recentHisRejectAlerts.Add(rejectAlert);
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

    }
}
