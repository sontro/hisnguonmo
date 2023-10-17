using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroup.Update
{
    class SdaGroupUpdateBehaviorEv : BeanObjectBase, ISdaGroupUpdate
    {
        SDA_GROUP entity;

        internal SdaGroupUpdateBehaviorEv(CommonParam param, SDA_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupDAO.Update(entity);
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
                result = result && SdaGroupCheckVerifyIsUnlock.Verify(param, entity.ID);
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
