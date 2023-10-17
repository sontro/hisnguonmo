using SDA.MANAGER.Core.SdaEventLog.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog
{
    partial class SdaEventLogUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEventLogUpdate(CommonParam param, object data)
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
                    ISdaEventLogUpdate behavior = SdaEventLogUpdateBehaviorFactory.MakeISdaEventLogUpdate(param, entity);
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
