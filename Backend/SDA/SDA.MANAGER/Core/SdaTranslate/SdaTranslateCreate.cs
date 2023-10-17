using SDA.MANAGER.Core.SdaTranslate.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate
{
    partial class SdaTranslateCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTranslateCreate(CommonParam param, object data)
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
                    ISdaTranslateCreate behavior = SdaTranslateCreateBehaviorFactory.MakeISdaTranslateCreate(param, entity);
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
