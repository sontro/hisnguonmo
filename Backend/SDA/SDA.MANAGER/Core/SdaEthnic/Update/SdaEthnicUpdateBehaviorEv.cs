using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic.Update
{
    class SdaEthnicUpdateBehaviorEv : BeanObjectBase, ISdaEthnicUpdate
    {
        SDA_ETHNIC entity;

        internal SdaEthnicUpdateBehaviorEv(CommonParam param, SDA_ETHNIC data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEthnicUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaEthnicDAO.Update(entity);
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
                result = result && SdaEthnicCheckVerifyValidData.Verify(param, entity);
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
