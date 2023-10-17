using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEthnic.Create
{
    class SdaEthnicCreateBehaviorListEv : BeanObjectBase, ISdaEthnicCreate
    {
        List<SDA_ETHNIC> entities;

        internal SdaEthnicCreateBehaviorListEv(CommonParam param, List<SDA_ETHNIC> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaEthnicCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaEthnicDAO.CreateList(entities);
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
                result = result && SdaEthnicCheckVerifyValidData.Verify(param, entities);
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
