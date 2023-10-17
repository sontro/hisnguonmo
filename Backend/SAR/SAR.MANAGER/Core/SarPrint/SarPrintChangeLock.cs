using SAR.MANAGER.Core.SarPrint.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrint
{
    partial class SarPrintChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrint.Contains(entity.GetType()))
                {
                    ISarPrintChangeLock behavior = SarPrintChangeLockBehaviorFactory.MakeISarPrintChangeLock(param, entity);
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
