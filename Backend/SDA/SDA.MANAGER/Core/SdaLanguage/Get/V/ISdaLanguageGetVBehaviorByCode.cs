using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage.Get.V
{
    class SdaLanguageGetVBehaviorByCode : BeanObjectBase, ISdaLanguageGetV
    {
        string code;

        internal SdaLanguageGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_SDA_LANGUAGE ISdaLanguageGetV.Run()
        {
            try
            {
                return DAOWorker.SdaLanguageDAO.GetViewByCode(code, new SdaLanguageViewFilterQuery().Query());
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
