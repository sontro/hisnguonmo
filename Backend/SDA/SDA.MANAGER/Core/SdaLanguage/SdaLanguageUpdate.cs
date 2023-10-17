using SDA.MANAGER.Core.SdaLanguage.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage
{
    partial class SdaLanguageUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLanguageUpdate(CommonParam param, object data)
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
                    ISdaLanguageUpdate behavior = SdaLanguageUpdateBehaviorFactory.MakeISdaLanguageUpdate(param, entity);
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
