using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
//using SDA.MANAGER.Core.SdaModuleField.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Delete
{
    class SdaModuleFieldDeleteBehaviorEv : BeanObjectBase, ISdaModuleFieldDelete
    {
        SDA_MODULE_FIELD entity;

        internal SdaModuleFieldDeleteBehaviorEv(CommonParam param, SDA_MODULE_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaModuleFieldDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaModuleFieldDAO.Truncate(entity);
                if (result)
                {
                    //SdaModuleFieldEventLogDelete.Log(entity);
                }
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
                result = result && SdaModuleFieldCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
