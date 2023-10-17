using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaModuleField.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaModuleField.Create
{
    class SdaModuleFieldCreateBehaviorEv : BeanObjectBase, ISdaModuleFieldCreate
    {
        SDA_MODULE_FIELD entity;

        internal SdaModuleFieldCreateBehaviorEv(CommonParam param, SDA_MODULE_FIELD data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaModuleFieldCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaModuleFieldDAO.Create(entity);
                if (result) { SdaModuleFieldEventLogCreate.Log(entity); }
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
                result = result && SdaModuleFieldCheckVerifyExistsCode.Verify(param, entity.MODULE_FIELD_CODE, null);
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
