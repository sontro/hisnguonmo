using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Get.V
{
    class SdaModuleFieldGetVBehaviorByCode : BeanObjectBase, ISdaModuleFieldGetV
    {
        string code;

        internal SdaModuleFieldGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_MODULE_FIELD ISdaModuleFieldGetV.Run()
        {
            try
            {
                return DAOWorker.SdaModuleFieldDAO.GetViewByCode(code, new SdaModuleFieldViewFilterQuery().Query());
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
