using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaConfig.Create
{
    class SdaConfigCreateBehaviorListEv : BeanObjectBase, ISdaConfigCreate
    {
        List<SDA_CONFIG> entities;

        internal SdaConfigCreateBehaviorListEv(CommonParam param, List<SDA_CONFIG> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaConfigCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaConfigDAO.CreateList(entities);
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
                result = result && SdaConfigCheckVerifyValidData.Verify(param, entities);
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
