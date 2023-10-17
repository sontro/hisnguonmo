using SDA.MANAGER.Core.SdaReligion.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaReligion
{
    partial class SdaReligionChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaReligionChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaReligion.Contains(entity.GetType()))
                {
                    ISdaReligionChangeLock behavior = SdaReligionChangeLockBehaviorFactory.MakeISdaReligionChangeLock(param, entity);
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
