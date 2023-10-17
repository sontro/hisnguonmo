using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
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
