using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType.Update
{
    class SdaGroupTypeUpdateBehaviorEv : BeanObjectBase, ISdaGroupTypeUpdate
    {
        SDA_GROUP_TYPE entity;

        internal SdaGroupTypeUpdateBehaviorEv(CommonParam param, SDA_GROUP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupTypeUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupTypeDAO.Update(entity);
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
                result = result && SdaGroupTypeCheckVerifyValidData.Verify(param, entity);
                result = result && SdaGroupTypeCheckVerifyIsUnlock.Verify(param, entity.ID);
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
