using SDA.MANAGER.Core.SdaProvinceMap.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvinceMap
{
    partial class SdaProvinceMapChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceMapChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaProvinceMap.Contains(entity.GetType()))
                {
                    ISdaProvinceMapChangeLock behavior = SdaProvinceMapChangeLockBehaviorFactory.MakeISdaProvinceMapChangeLock(param, entity);
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
