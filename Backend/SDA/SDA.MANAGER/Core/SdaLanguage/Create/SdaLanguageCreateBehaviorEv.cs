using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaLanguage.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage.Create
{
    class SdaLanguageCreateBehaviorEv : BeanObjectBase, ISdaLanguageCreate
    {
        SDA_LANGUAGE entity;

        internal SdaLanguageCreateBehaviorEv(CommonParam param, SDA_LANGUAGE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLanguageCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaLanguageDAO.Create(entity);
                if (result) { SdaLanguageEventLogCreate.Log(entity); }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && SdaLanguageCheckVerifyValidData.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
