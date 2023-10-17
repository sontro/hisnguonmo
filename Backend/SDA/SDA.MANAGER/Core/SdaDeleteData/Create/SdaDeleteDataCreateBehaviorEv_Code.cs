using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaDeleteData.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Create
{
    class SdaDeleteDataCreateBehaviorEv : BeanObjectBase, ISdaDeleteDataCreate
    {
        SDA_DELETE_DATA entity;

        internal SdaDeleteDataCreateBehaviorEv(CommonParam param, SDA_DELETE_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDeleteDataCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDeleteDataDAO.Create(entity);
                if (result) { SdaDeleteDataEventLogCreate.Log(entity); }
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
                result = result && SdaDeleteDataCheckVerifyValidData.Verify(param, entity);
                result = result && SdaDeleteDataCheckVerifyExistsCode.Verify(param, entity.DELETE_DATA_CODE, null);
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
