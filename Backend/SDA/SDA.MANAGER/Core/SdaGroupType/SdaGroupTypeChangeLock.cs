using SDA.MANAGER.Core.SdaGroupType.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaGroupType
{
    partial class SdaGroupTypeChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaGroupTypeChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaGroupType.Contains(entity.GetType()))
                {
                    ISdaGroupTypeChangeLock behavior = SdaGroupTypeChangeLockBehaviorFactory.MakeISdaGroupTypeChangeLock(param, entity);
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
