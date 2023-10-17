using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaDeleteData.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDeleteData.Delete
{
    class SdaDeleteDataDeleteBehaviorEv : BeanObjectBase, ISdaDeleteDataDelete
    {
        SDA_DELETE_DATA entity;

        internal SdaDeleteDataDeleteBehaviorEv(CommonParam param, SDA_DELETE_DATA data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaDeleteDataDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaDeleteDataDAO.Truncate(entity);
                if (result) { SdaDeleteDataEventLogDelete.Log(entity); }
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
                result = result && SdaDeleteDataCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
