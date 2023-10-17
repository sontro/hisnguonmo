using SDA.MANAGER.Core.SdaConfig.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig
{
    partial class SdaConfigUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigUpdate(CommonParam param, object data)
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
                    ISdaConfigUpdate behavior = SdaConfigUpdateBehaviorFactory.MakeISdaConfigUpdate(param, entity);
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
