using SDA.MANAGER.Core.SdaConfigAppUser.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigAppUser
{
    partial class SdaConfigAppUserCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppUserCreate(CommonParam param, object data)
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
                    ISdaConfigAppUserCreate behavior = SdaConfigAppUserCreateBehaviorFactory.MakeISdaConfigAppUserCreate(param, entity);
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
