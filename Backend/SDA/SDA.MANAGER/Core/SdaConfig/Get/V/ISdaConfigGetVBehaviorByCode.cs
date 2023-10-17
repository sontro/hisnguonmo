using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig.Get.V
{
    class SdaConfigGetVBehaviorByCode : BeanObjectBase, ISdaConfigGetV
    {
        string code;

        internal SdaConfigGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_CONFIG ISdaConfigGetV.Run()
        {
            try
            {
                return DAOWorker.SdaConfigDAO.GetViewByCode(code, new SdaConfigViewFilterQuery().Query());
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
