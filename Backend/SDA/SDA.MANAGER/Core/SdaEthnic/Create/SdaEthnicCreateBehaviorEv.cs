using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic.Create
{
    class SdaEthnicCreateBehaviorEv : BeanObjectBase, ISdaEthnicCreate
    {
        SDA_ETHNIC entity;

        internal SdaEthnicCreateBehaviorEv(CommonParam param, SDA_ETHNIC data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaEthnicCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaEthnicDAO.Create(entity);
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
