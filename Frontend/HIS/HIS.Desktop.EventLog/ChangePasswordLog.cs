using HIS.Desktop.LocalStorage.LocalData;
using System;

namespace HIS.Desktop.EventLog
{
    public class ChangePasswordLog
    {
        public static void ChangePasswordSuccessLog()
        {
            try
            {
                string message = String.Format(EventLogUtil.SetLog(His.EventLog.Message.Enum.DoiMatKhau), Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginAddress());                
                His.EventLog.Logger.Log(GlobalVariables.APPLICATION_CODE, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginName(), message, Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetLoginAddress(), Inventec.UC.Login.Base.ClientTokenManagerStore.ClientTokenManager.GetGroupCode());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
