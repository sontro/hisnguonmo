using ACS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token.ResourceSystem;
using MOS.ApiConsumerManager;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.AcsAuthorize
{
    class AcsAuthorizeGet : BusinessBase
    {
        internal AcsAuthorizeGet()
            : base()
        {

        }

        internal AcsAuthorizeGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<ACS_CONTROL> GetControlInRoles()
        {
            try
            {
                var ro = this.GetAuthorizeSdo();
                if (ro != null)
                {
                    return ro.ControlInRoles;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal ACS.SDO.AcsAuthorizeSDO GetAuthorizeSdo()
        {
            try
            {
                ACS.SDO.AcsTokenLoginSDO tokenLoginSDOForAuthorize = new ACS.SDO.AcsTokenLoginSDO();
                tokenLoginSDOForAuthorize.LOGIN_NAME = ResourceTokenManager.GetLoginName();
                tokenLoginSDOForAuthorize.APPLICATION_CODE = MOS.UTILITY.Constant.CLIENT_APPLICATION_CODE;
                var ro = ApiConsumerStore.AcsConsumer.Get<Inventec.Core.ApiResultObject<ACS.SDO.AcsAuthorizeSDO>>("/api/AcsToken/Authorize", param, tokenLoginSDOForAuthorize);
                if (ro != null)
                {
                    param = ro.Param != null ? ro.Param : param;
                    return ro.Data;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
