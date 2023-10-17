using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaDeleteData.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Update
{
    class SdaDeleteDataUpdateBehaviorEv : BeanObjectBase, ISdaDeleteDataUpdate
    {
        SDA_DELETE_DATA current;
        SDA_DELETE_DATA entity;

        internal SdaDeleteDataUpdateBehaviorEv(CommonParam param, SDA_DELETE_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDeleteDataUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDeleteDataDAO.Update(entity);
                if (result) { SdaDeleteDataEventLogUpdate.Log(current, entity); }
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
                result = result && SdaDeleteDataCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
