using SDA.MANAGER.Core.SdaSql.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSql
{
    partial class SdaSqlChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaSql.Contains(entity.GetType()))
                {
                    ISdaSqlChangeLock behavior = SdaSqlChangeLockBehaviorFactory.MakeISdaSqlChangeLock(param, entity);
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
