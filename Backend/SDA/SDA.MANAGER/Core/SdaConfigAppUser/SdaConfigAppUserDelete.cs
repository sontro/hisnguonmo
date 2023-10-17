using SDA.MANAGER.Core.SdaConfigAppUser.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser
{
    partial class SdaConfigAppUserDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppUserDelete(CommonParam param, object data)
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
                    ISdaConfigAppUserDelete behavior = SdaConfigAppUserDeleteBehaviorFactory.MakeISdaConfigAppUserDelete(param, entity);
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
