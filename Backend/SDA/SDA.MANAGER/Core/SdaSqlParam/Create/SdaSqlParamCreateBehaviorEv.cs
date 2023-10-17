using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam.Create
{
    class SdaSqlParamCreateBehaviorEv : BeanObjectBase, ISdaSqlParamCreate
    {
        SDA_SQL_PARAM entity;

        internal SdaSqlParamCreateBehaviorEv(CommonParam param, SDA_SQL_PARAM data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlParamCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaSqlParamDAO.Create(entity);
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
