using SDA.MANAGER.Core.SdaConfigAppUser.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser
{
    partial class SdaConfigAppUserUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppUserUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaConfigAppUser.Contains(entity.GetType()))
                {
                    ISdaConfigAppUserUpdate behavior = SdaConfigAppUserUpdateBehaviorFactory.MakeISdaConfigAppUserUpdate(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
