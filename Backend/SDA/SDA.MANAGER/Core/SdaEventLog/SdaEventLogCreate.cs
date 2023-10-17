using SDA.MANAGER.Core.SdaEventLog.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEventLog
{
    partial class SdaEventLogCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEventLogCreate(CommonParam param, object data)
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
                    ISdaEventLogCreate behavior = SdaEventLogCreateBehaviorFactory.MakeISdaEventLogCreate(param, entity);
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
