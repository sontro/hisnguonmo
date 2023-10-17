using SDA.MANAGER.Core.SdaCustomizeUi.Create;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi
{
    partial class SdaCustomizeUiCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCustomizeUiCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SdaCustomizeUi.Contains(entity.GetType()))
                {
                    ISdaCustomizeUiCreate behavior = SdaCustomizeUiCreateBehaviorFactory.MakeISdaCustomizeUiCreate(param, entity);
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
