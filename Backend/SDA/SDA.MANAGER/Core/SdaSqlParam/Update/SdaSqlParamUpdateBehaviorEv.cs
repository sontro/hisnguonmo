using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Update
{
    class SdaSqlParamUpdateBehaviorEv : BeanObjectBase, ISdaSqlParamUpdate
    {
        SDA_SQL_PARAM current;
        SDA_SQL_PARAM entity;

        internal SdaSqlParamUpdateBehaviorEv(CommonParam param, SDA_SQL_PARAM data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlParamUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaSqlParamDAO.Update(entity);
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
                result = result && SdaSqlParamCheckVerifyValidData.Verify(param, entity);
                result = result && SdaSqlParamCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
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
