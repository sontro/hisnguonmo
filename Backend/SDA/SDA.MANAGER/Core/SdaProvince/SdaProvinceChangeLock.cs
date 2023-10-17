using SDA.MANAGER.Core.SdaProvince.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaProvince
{
    partial class SdaProvinceChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaProvinceChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaProvince.Contains(entity.GetType()))
                {
                    ISdaProvinceChangeLock behavior = SdaProvinceChangeLockBehaviorFactory.MakeISdaProvinceChangeLock(param, entity);
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
