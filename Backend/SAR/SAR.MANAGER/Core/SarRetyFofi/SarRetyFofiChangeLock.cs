using SAR.MANAGER.Core.SarRetyFofi.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarRetyFofi
{
    partial class SarRetyFofiChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarRetyFofiChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarRetyFofi.Contains(entity.GetType()))
                {
                    ISarRetyFofiChangeLock behavior = SarRetyFofiChangeLockBehaviorFactory.MakeISarRetyFofiChangeLock(param, entity);
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
