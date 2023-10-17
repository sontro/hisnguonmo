using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Get.Ev
{
    class SdaModuleFieldGetEvBehaviorById : BeanObjectBase, ISdaModuleFieldGetEv
    {
        long id;

        internal SdaModuleFieldGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        SDA_MODULE_FIELD ISdaModuleFieldGetEv.Run()
        {
            try
            {
                return DAOWorker.SdaModuleFieldDAO.GetById(id, new SdaModuleFieldFilterQuery().Query());
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
