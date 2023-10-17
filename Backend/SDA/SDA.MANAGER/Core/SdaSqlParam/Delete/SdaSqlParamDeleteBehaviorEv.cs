using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Delete
{
    class SdaSqlParamDeleteBehaviorEv : BeanObjectBase, ISdaSqlParamDelete
    {
        SDA_SQL_PARAM entity;

        internal SdaSqlParamDeleteBehaviorEv(CommonParam param, SDA_SQL_PARAM data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlParamDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaSqlParamDAO.Truncate(entity);
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
                result = result && SdaSqlParamCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
