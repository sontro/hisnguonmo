using SDA.MANAGER.Core.SdaConfigApp.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp
{
    partial class SdaConfigAppUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppUpdate(CommonParam param, object data)
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
                    ISdaConfigAppUpdate behavior = SdaConfigAppUpdateBehaviorFactory.MakeISdaConfigAppUpdate(param, entity);
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
