using SAR.MANAGER.Core.SarPrintType.Lock;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarPrintType
{
    partial class SarPrintTypeChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarPrintTypeChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarPrintType.Contains(entity.GetType()))
                {
                    ISarPrintTypeChangeLock behavior = SarPrintTypeChangeLockBehaviorFactory.MakeISarPrintTypeChangeLock(param, entity);
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
