using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup.Create
{
    class SdaGroupCreateBehaviorEv : BeanObjectBase, ISdaGroupCreate
    {
        SDA_GROUP entity;

        internal SdaGroupCreateBehaviorEv(CommonParam param, SDA_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupDAO.Create(entity);
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
                result = result && SdaGroupCheckVerifyValidData.Verify(param, entity);
                result = result && SdaGroupCheckVerifyExistsCode.Verify(param, entity.GROUP_CODE, null);
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
