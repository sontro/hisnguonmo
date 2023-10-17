using HIS.Desktop.LocalStorage.LocalData;
using Inventec.Core;
using System;

namespace HIS.Desktop.EventLog
{
    public class LoginLog
    {
        public static void LoginSuccessLog(string mesage)
        {
            try
            {
                CommonParam param = new CommonParam();
                string message = String.Format(mesage, GlobalVariables.APPLICATION_CODE);
                His.EventLog.Logger.Log(GlobalVariables.APPLICATION_CODE, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName(), message, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginAddress(), Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetGroupCode());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public static void LoginSuccessLog()
        {
            try
            {
                CommonParam param = new CommonParam();
                string message = EventLogUtil.SetLog(His.EventLog.Message.Enum.DangNhap);
                His.EventLog.Logger.Log(GlobalVariables.APPLICATION_CODE, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName(), message, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginAddress(), Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetGroupCode());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
