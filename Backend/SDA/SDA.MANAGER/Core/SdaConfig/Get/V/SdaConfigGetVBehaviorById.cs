using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig.Get.V
{
    class SdaConfigGetVBehaviorById : BeanObjectBase, ISdaConfigGetV
    {
        long id;

        internal SdaConfigGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_CONFIG ISdaConfigGetV.Run()
        {
            try
            {
                return DAOWorker.SdaConfigDAO.GetViewById(id, new SdaConfigViewFilterQuery().Query());
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
