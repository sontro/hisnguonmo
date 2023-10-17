using SDA.MANAGER.Core.SdaConfigApp.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp
{
    partial class SdaConfigAppDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaConfigApp.Contains(entity.GetType()))
                {
                    ISdaConfigAppDelete behavior = SdaConfigAppDeleteBehaviorFactory.MakeISdaConfigAppDelete(param, entity);
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
