using SDA.MANAGER.Core.SdaNotify.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify
{
    partial class SdaNotifyCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNotifyCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaNotify.Contains(entity.GetType()))
                {
                    ISdaNotifyCreate behavior = SdaNotifyCreateBehaviorFactory.MakeISdaNotifyCreate(param, entity);
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
