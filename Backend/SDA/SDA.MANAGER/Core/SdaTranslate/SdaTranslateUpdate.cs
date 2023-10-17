using SDA.MANAGER.Core.SdaTranslate.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaTranslate
{
    partial class SdaTranslateUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaTranslateUpdate(CommonParam param, object data)
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
                    ISdaTranslateUpdate behavior = SdaTranslateUpdateBehaviorFactory.MakeISdaTranslateUpdate(param, entity);
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
