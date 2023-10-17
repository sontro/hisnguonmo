using SDA.MANAGER.Core.SdaLanguage.Lock;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage
{
    partial class SdaLanguageChangeLock : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLanguageChangeLock(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaLanguage.Contains(entity.GetType()))
                {
                    ISdaLanguageChangeLock behavior = SdaLanguageChangeLockBehaviorFactory.MakeISdaLanguageChangeLock(param, entity);
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
