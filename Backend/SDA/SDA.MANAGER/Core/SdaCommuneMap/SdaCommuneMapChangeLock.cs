using SDA.MANAGER.Core.SdaCommuneMap.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCommuneMap
{
    partial class SdaCommuneMapChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCommuneMapChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCommuneMap.Contains(entity.GetType()))
                {
                    ISdaCommuneMapChangeLock behavior = SdaCommuneMapChangeLockBehaviorFactory.MakeISdaCommuneMapChangeLock(param, entity);
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
