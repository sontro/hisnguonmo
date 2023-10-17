using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using SDA.MANAGER.Core.SdaSql.EventLog;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Update
{
    class SdaSqlUpdateBehaviorEv : BeanObjectBase, ISdaSqlUpdate
    {
        SDA_SQL current;
        SDA_SQL entity;

        internal SdaSqlUpdateBehaviorEv(CommonParam param, SDA_SQL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaSqlDAO.Update(entity);
                if (result) { SdaSqlEventLogUpdate.Log(current, entity); }
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
                result = result && SdaSqlCheckVerifyValidData.Verify(param, entity);
                result = result && SdaSqlCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && SdaSqlCheckVerifyExistsCode.Verify(param, entity.SQL_CODE, entity.ID);
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
