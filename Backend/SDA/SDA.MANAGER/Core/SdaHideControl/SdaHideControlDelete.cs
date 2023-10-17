using SDA.MANAGER.Core.SdaHideControl.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaHideControl
{
    partial class SdaHideControlDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaHideControlDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaHideControl.Contains(entity.GetType()))
                {
                    ISdaHideControlDelete behavior = SdaHideControlDeleteBehaviorFactory.MakeISdaHideControlDelete(param, entity);
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
