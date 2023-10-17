using SDA.MANAGER.Core.SdaConfig.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfig
{
    partial class SdaConfigCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigCreate(CommonParam param, object data)
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
                    ISdaConfigCreate behavior = SdaConfigCreateBehaviorFactory.MakeISdaConfigCreate(param, entity);
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
