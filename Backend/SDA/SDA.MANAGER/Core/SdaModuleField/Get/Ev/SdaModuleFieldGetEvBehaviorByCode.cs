using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Get.Ev
{
    class SdaModuleFieldGetEvBehaviorByCode : BeanObjectBase, ISdaModuleFieldGetEv
    {
        string code;

        internal SdaModuleFieldGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        SDA_MODULE_FIELD ISdaModuleFieldGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaModuleFieldDAO.GetByCode(code, new SdaModuleFieldFilterQuery().Query());
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
