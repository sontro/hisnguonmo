using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate.Get.V
{
    class SdaTranslateGetVBehaviorById : BeanObjectBase, ISdaTranslateGetV
    {
        long id;

        internal SdaTranslateGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_TRANSLATE ISdaTranslateGetV.Run()
        {
            try
            {
                return DAOWorker.SdaTranslateDAO.GetViewById(id, new SdaTranslateViewFilterQuery().Query());
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
