using SDA.MANAGER.Core.SdaLanguage.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaLanguage
{
    partial class SdaLanguageDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaLanguageDelete(CommonParam param, object data)
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
                    ISdaLanguageDelete behavior = SdaLanguageDeleteBehaviorFactory.MakeISdaLanguageDelete(param, entity);
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
