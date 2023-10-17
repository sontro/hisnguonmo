using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaTranslate.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate.Create
{
    class SdaTranslateCreateBehaviorEv : BeanObjectBase, ISdaTranslateCreate
    {
        SDA_TRANSLATE entity;

        internal SdaTranslateCreateBehaviorEv(CommonParam param, SDA_TRANSLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTranslateCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaTranslateDAO.Create(entity);
                if (result) { SdaTranslateEventLogCreate.Log(entity); }
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
                result = result && SdaTranslateCheckVerifyValidData.Verify(param, entity);
                result = result && SdaTranslateCheckVerifyExistsCode.Verify(param, entity.TRANSLATE_CODE, null);
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
