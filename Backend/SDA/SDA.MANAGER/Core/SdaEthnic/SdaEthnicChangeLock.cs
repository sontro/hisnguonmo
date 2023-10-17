using SDA.MANAGER.Core.SdaEthnic.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaEthnic
{
    partial class SdaEthnicChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaEthnicChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaEthnic.Contains(entity.GetType()))
                {
                    ISdaEthnicChangeLock behavior = SdaEthnicChangeLockBehaviorFactory.MakeISdaEthnicChangeLock(param, entity);
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
