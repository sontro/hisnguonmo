using SDA.MANAGER.Core.SdaTrouble.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTrouble
{
    partial class SdaTroubleChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTroubleChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaTrouble.Contains(entity.GetType()))
                {
                    ISdaTroubleChangeLock behavior = SdaTroubleChangeLockBehaviorFactory.MakeISdaTroubleChangeLock(param, entity);
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
