using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql.Create
{
    class SdaSqlCreateBehaviorEv : BeanObjectBase, ISdaSqlCreate
    {
        SDA_SQL entity;

        internal SdaSqlCreateBehaviorEv(CommonParam param, SDA_SQL data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaSqlCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaSqlDAO.Create(entity);
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
