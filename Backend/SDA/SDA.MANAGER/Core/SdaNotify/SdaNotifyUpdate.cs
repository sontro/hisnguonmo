using SDA.MANAGER.Core.SdaNotify.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify
{
    partial class SdaNotifyUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNotifyUpdate(CommonParam param, object data)
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
                    ISdaNotifyUpdate behavior = SdaNotifyUpdateBehaviorFactory.MakeISdaNotifyUpdate(param, entity);
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
