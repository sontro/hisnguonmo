using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Delete
{
    class SdaSqlDeleteBehaviorEv : BeanObjectBase, ISdaSqlDelete
    {
        SDA_SQL entity;

        internal SdaSqlDeleteBehaviorEv(CommonParam param, SDA_SQL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaSqlDAO.Truncate(entity);
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
                result = result && SdaSqlCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
