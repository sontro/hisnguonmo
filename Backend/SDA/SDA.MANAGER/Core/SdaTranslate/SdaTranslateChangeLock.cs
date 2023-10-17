using SDA.MANAGER.Core.SdaTranslate.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate
{
    partial class SdaTranslateChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTranslateChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaTranslate.Contains(entity.GetType()))
                {
                    ISdaTranslateChangeLock behavior = SdaTranslateChangeLockBehaviorFactory.MakeISdaTranslateChangeLock(param, entity);
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
