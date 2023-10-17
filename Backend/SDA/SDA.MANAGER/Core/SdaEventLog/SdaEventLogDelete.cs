using SDA.MANAGER.Core.SdaEventLog.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog
{
    partial class SdaEventLogDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEventLogDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaEventLog.Contains(entity.GetType()))
                {
                    ISdaEventLogDelete behavior = SdaEventLogDeleteBehaviorFactory.MakeISdaEventLogDelete(param, entity);
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
