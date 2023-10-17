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
    class HisAlertReceiver: BusinessBase
    {
        private List<HIS_ALERT> beforeUpdateHisAlerts = new List<HIS_ALERT>();
        internal HisAlertReceiver()
            : base()
        {
            this.Init();
        }

        internal HisAlertReceiver(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
        }

        /// <summary>
        /// tiếp nhận báo động
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
                HIS_ALERT raw = null;
                valid = valid && checker.VerifyId(id, ref raw);

                if (valid)
                {
                    raw.RECEIVER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.RECEIVER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    raw.RECEIVE_TIME = (Inventec.Common.DateTime.Get.Now() ?? 0);

                    if (!DAOWorker.HisAlertDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAlert_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisAlert that bai." + LogUtil.TraceData("data", raw));
                    }
                    this.beforeUpdateHisAlerts.Add(raw);
                    PubSubProcessor.SendMessage(raw);
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
