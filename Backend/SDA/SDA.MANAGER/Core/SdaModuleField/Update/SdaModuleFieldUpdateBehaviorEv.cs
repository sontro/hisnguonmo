using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
//using SDA.MANAGER.Core.SdaModuleField.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Update
{
    class SdaModuleFieldUpdateBehaviorEv : BeanObjectBase, ISdaModuleFieldUpdate
    {
        SDA_MODULE_FIELD current;
        SDA_MODULE_FIELD entity;

        internal SdaModuleFieldUpdateBehaviorEv(CommonParam param, SDA_MODULE_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaModuleFieldUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaModuleFieldDAO.Update(entity);
                if (result) 
                {
                    //SdaModuleFieldEventLogUpdate.Log(current, entity);
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
                result = result && SdaModuleFieldCheckVerifyValidData.Verify(param, entity);
                result = result && SdaModuleFieldCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
