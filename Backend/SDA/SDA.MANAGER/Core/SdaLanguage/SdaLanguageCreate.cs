using SDA.MANAGER.Core.SdaLanguage.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage
{
    partial class SdaLanguageCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLanguageCreate(CommonParam param, object data)
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
                    ISdaLanguageCreate behavior = SdaLanguageCreateBehaviorFactory.MakeISdaLanguageCreate(param, entity);
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
