using SDA.MANAGER.Core.SdaNotify.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaNotify
{
    partial class SdaNotifyDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaNotifyDelete(CommonParam param, object data)
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
                    ISdaNotifyDelete behavior = SdaNotifyDeleteBehaviorFactory.MakeISdaNotifyDelete(param, entity);
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
