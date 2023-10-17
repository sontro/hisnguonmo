using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaTranslate.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate.Update
{
    class SdaTranslateUpdateBehaviorEv : BeanObjectBase, ISdaTranslateUpdate
    {
        SDA_TRANSLATE current;
        SDA_TRANSLATE entity;

        internal SdaTranslateUpdateBehaviorEv(CommonParam param, SDA_TRANSLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTranslateUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaTranslateDAO.Update(entity);
                if (result) { SdaTranslateEventLogUpdate.Log(current, entity); }
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
                result = result && SdaTranslateCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaTranslateCheckVerifyExistsCode.Verify(param, entity.TRANSLATE_CODE, entity.ID);
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
