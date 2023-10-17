using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using SDA.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType.Create
{
    class SdaGroupTypeCreateBehaviorEv : BeanObjectBase, ISdaGroupTypeCreate
    {
        SDA_GROUP_TYPE entity;

        internal SdaGroupTypeCreateBehaviorEv(CommonParam param, SDA_GROUP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool ISdaGroupTypeCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.SdaGroupTypeDAO.Create(entity);
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
