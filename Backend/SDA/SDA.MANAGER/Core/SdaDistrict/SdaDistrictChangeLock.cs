using SDA.MANAGER.Core.SdaDistrict.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaDistrict
{
    partial class SdaDistrictChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaDistrictChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaDistrict.Contains(entity.GetType()))
                {
                    ISdaDistrictChangeLock behavior = SdaDistrictChangeLockBehaviorFactory.MakeISdaDistrictChangeLock(param, entity);
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
