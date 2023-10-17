using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaGroup.Create
{
    class SdaGroupCreateBehaviorListEv : BeanObjectBase, ISdaGroupCreate
    {
        List<SDA_GROUP> entities;

        internal SdaGroupCreateBehaviorListEv(CommonParam param, List<SDA_GROUP> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaGroupCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupDAO.CreateList(entities);
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
                result = result && SdaGroupCheckVerifyValidData.Verify(param, entities);
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
