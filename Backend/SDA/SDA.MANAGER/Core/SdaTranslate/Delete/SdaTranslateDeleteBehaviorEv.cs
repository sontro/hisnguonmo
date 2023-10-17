using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaTranslate.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate.Delete
{
    class SdaTranslateDeleteBehaviorEv : BeanObjectBase, ISdaTranslateDelete
    {
        SDA_TRANSLATE entity;

        internal SdaTranslateDeleteBehaviorEv(CommonParam param, SDA_TRANSLATE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaTranslateDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaTranslateDAO.Truncate(entity);
                if (result) { SdaTranslateEventLogDelete.Log(entity); }
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
                result = result && SdaTranslateCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
