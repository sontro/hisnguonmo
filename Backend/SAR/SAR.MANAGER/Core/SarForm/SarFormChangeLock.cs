using SAR.MANAGER.Core.SarForm.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm
{
    partial class SarFormChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarForm.Contains(entity.GetType()))
                {
                    ISarFormChangeLock behavior = SarFormChangeLockBehaviorFactory.MakeISarFormChangeLock(param, entity);
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
