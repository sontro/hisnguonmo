using SDA.MANAGER.Core.SdaSqlParam.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaSqlParam
{
    partial class SdaSqlParamChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaSqlParamChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaSqlParam.Contains(entity.GetType()))
                {
                    ISdaSqlParamChangeLock behavior = SdaSqlParamChangeLockBehaviorFactory.MakeISdaSqlParamChangeLock(param, entity);
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
