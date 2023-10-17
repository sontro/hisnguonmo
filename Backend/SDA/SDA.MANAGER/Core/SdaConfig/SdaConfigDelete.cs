using SDA.MANAGER.Core.SdaConfig.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig
{
    partial class SdaConfigDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaConfig.Contains(entity.GetType()))
                {
                    ISdaConfigDelete behavior = SdaConfigDeleteBehaviorFactory.MakeISdaConfigDelete(param, entity);
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
