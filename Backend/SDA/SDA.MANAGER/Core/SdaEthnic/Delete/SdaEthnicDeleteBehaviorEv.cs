using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic.Delete
{
    class SdaEthnicDeleteBehaviorEv : BeanObjectBase, ISdaEthnicDelete
    {
        SDA_ETHNIC entity;

        internal SdaEthnicDeleteBehaviorEv(CommonParam param, SDA_ETHNIC data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEthnicDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaEthnicDAO.Truncate(entity);
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
                result = result && SdaEthnicCheckVerifyIsUnlock.Verify(param, entity.ID);
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
