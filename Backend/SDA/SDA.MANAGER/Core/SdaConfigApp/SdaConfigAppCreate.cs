using SDA.MANAGER.Core.SdaConfigApp.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaConfigApp
{
    partial class SdaConfigAppCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaConfigAppCreate(CommonParam param, object data)
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
                    ISdaConfigAppCreate behavior = SdaConfigAppCreateBehaviorFactory.MakeISdaConfigAppCreate(param, entity);
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
