using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsUser;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.Check
{
    class AcsUserCheckVerifyValidDataForLogin
    {
        internal static bool Verify(CommonParam param, ref ACS_USER user, string loginName, string password)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(loginName) && !String.IsNullOrEmpty(password))
                {
                    user = new AcsUserBO().Get<ACS_USER>(loginName);
                    if (user != null && user.ID > 0 && !String.IsNullOrEmpty(password))
                    {
                        if (!String.IsNullOrEmpty(user.PASSWORD))
                        {
                            string encryptPassword = new MOS.EncryptPassword.Cryptor().EncryptPassword(password, user.LOGINNAME);
                            result = (encryptPassword.Equals(user.PASSWORD));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }

            if (!result)
            {
                ACS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TenDangNhapHoacMatKhauKhongChinhXac);
            }

            return result;
        }
    }
}
