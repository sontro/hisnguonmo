using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaLanguage.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage.Update
{
    class SdaLanguageUpdateBehaviorEv : BeanObjectBase, ISdaLanguageUpdate
    {
        SDA_LANGUAGE current;
        SDA_LANGUAGE entity;

        internal SdaLanguageUpdateBehaviorEv(CommonParam param, SDA_LANGUAGE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaLanguageUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaLanguageDAO.Update(entity);
                if (result) { SdaLanguageEventLogUpdate.Log(current, entity); }
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
                result = result && SdaLanguageCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaLanguageCheckVerifyExistsCode.Verify(param, entity.LANGUAGE_CODE, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
