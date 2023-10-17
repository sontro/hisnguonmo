using SAR.MANAGER.Core.SarFormType.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType
{
    partial class SarFormTypeChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormTypeChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarFormType.Contains(entity.GetType()))
                {
                    ISarFormTypeChangeLock behavior = SarFormTypeChangeLockBehaviorFactory.MakeISarFormTypeChangeLock(param, entity);
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
