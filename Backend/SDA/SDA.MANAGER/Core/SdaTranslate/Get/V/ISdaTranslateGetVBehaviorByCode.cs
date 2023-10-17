using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate.Get.V
{
    class SdaTranslateGetVBehaviorByCode : BeanObjectBase, ISdaTranslateGetV
    {
        string code;

        internal SdaTranslateGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_TRANSLATE ISdaTranslateGetV.Run()
        {
            try
            {
                return DAOWorker.SdaTranslateDAO.GetViewByCode(code, new SdaTranslateViewFilterQuery().Query());
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
