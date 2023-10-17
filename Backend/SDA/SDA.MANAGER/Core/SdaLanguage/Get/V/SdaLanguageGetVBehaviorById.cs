using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage.Get.V
{
    class SdaLanguageGetVBehaviorById : BeanObjectBase, ISdaLanguageGetV
    {
        long id;

        internal SdaLanguageGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_SDA_LANGUAGE ISdaLanguageGetV.Run()
        {
            try
            {
                return DAOWorker.SdaLanguageDAO.GetViewById(id, new SdaLanguageViewFilterQuery().Query());
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
