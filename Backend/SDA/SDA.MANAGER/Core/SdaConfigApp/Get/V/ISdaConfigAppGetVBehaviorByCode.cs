using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp.Get.V
{
    class SdaConfigAppGetVBehaviorByCode : BeanObjectBase, ISdaConfigAppGetV
    {
        string code;

        internal SdaConfigAppGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_CONFIG_APP ISdaConfigAppGetV.Run()
        {
            try
            {
                return DAOWorker.SdaConfigAppDAO.GetViewByCode(code, new SdaConfigAppViewFilterQuery().Query());
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
