using SDA.MANAGER.Core.SdaTranslate.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate
{
    partial class SdaTranslateDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTranslateDelete(CommonParam param, object data)
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
                    ISdaTranslateDelete behavior = SdaTranslateDeleteBehaviorFactory.MakeISdaTranslateDelete(param, entity);
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
