using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Get.V
{
    class SdaModuleFieldGetVBehaviorById : BeanObjectBase, ISdaModuleFieldGetV
    {
        long id;

        internal SdaModuleFieldGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_MODULE_FIELD ISdaModuleFieldGetV.Run()
        {
            try
            {
                return DAOWorker.SdaModuleFieldDAO.GetViewById(id, new SdaModuleFieldViewFilterQuery().Query());
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
