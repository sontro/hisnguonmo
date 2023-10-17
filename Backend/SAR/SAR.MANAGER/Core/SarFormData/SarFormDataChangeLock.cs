using SAR.MANAGER.Core.SarFormData.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData
{
    partial class SarFormDataChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormDataChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarFormData.Contains(entity.GetType()))
                {
                    ISarFormDataChangeLock behavior = SarFormDataChangeLockBehaviorFactory.MakeISarFormDataChangeLock(param, entity);
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
