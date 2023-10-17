using SDA.MANAGER.Core.SdaDistrictMap.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrictMap
{
    partial class SdaDistrictMapChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictMapChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaDistrictMap.Contains(entity.GetType()))
                {
                    ISdaDistrictMapChangeLock behavior = SdaDistrictMapChangeLockBehaviorFactory.MakeISdaDistrictMapChangeLock(param, entity);
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
