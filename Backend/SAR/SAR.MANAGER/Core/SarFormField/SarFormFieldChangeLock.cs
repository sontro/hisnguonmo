using SAR.MANAGER.Core.SarFormField.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField
{
    partial class SarFormFieldChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormFieldChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarFormField.Contains(entity.GetType()))
                {
                    ISarFormFieldChangeLock behavior = SarFormFieldChangeLockBehaviorFactory.MakeISarFormFieldChangeLock(param, entity);
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
