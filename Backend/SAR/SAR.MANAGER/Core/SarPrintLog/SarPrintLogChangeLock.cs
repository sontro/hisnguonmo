using SAR.MANAGER.Core.SarPrintLog.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintLog
{
    partial class SarPrintLogChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintLogChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrintLog.Contains(entity.GetType()))
                {
                    ISarPrintLogChangeLock behavior = SarPrintLogChangeLockBehaviorFactory.MakeISarPrintLogChangeLock(param, entity);
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
