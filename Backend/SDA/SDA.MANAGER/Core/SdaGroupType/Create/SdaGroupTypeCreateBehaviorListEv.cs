using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaGroupType.Create
{
    class SdaGroupTypeCreateBehaviorListEv : BeanObjectBase, ISdaGroupTypeCreate
    {
        List<SDA_GROUP_TYPE> entities;

        internal SdaGroupTypeCreateBehaviorListEv(CommonParam param, List<SDA_GROUP_TYPE> datas)
            : base(param)
        {
            entities = datas;
        }

        bool ISdaGroupTypeCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupTypeDAO.CreateList(entities);
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
                result = result && SdaGroupTypeCheckVerifyValidData.Verify(param, entities);
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
