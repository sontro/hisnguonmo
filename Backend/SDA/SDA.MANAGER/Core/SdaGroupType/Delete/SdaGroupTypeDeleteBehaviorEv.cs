using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType.Delete
{
    class SdaGroupTypeDeleteBehaviorEv : BeanObjectBase, ISdaGroupTypeDelete
    {
        SDA_GROUP_TYPE entity;

        internal SdaGroupTypeDeleteBehaviorEv(CommonParam param, SDA_GROUP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupTypeDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupTypeDAO.Truncate(entity);
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
