using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp.Get.V
{
    class SdaConfigAppGetVBehaviorById : BeanObjectBase, ISdaConfigAppGetV
    {
        long id;

        internal SdaConfigAppGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_CONFIG_APP ISdaConfigAppGetV.Run()
        {
            try
            {
                return DAOWorker.SdaConfigAppDAO.GetViewById(id, new SdaConfigAppViewFilterQuery().Query());
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
