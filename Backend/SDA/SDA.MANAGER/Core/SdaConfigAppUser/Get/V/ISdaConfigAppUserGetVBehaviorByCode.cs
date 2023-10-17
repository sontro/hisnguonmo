using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser.Get.V
{
    class SdaConfigAppUserGetVBehaviorByCode : BeanObjectBase, ISdaConfigAppUserGetV
    {
        string code;

        internal SdaConfigAppUserGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_CONFIG_APP_USER ISdaConfigAppUserGetV.Run()
        {
            try
            {
                return DAOWorker.SdaConfigAppUserDAO.GetViewByCode(code, new SdaConfigAppUserViewFilterQuery().Query());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
