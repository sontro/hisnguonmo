using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsUser;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.Check
{
    class AcsUserCheckVerifyValidDataForAuthorize
    {
        internal static bool Verify(CommonParam param, ref ACS_USER user, string loginName)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(loginName))
                {
                    user = new AcsUserBO().Get<ACS_USER>(loginName);
                    if (user != null && user.ID > 0)
                    {
                        result = true;
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
