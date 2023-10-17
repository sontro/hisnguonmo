using SDA.MANAGER.Core.SdaCustomizeUi.Update;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi
{
    partial class SdaCustomizeUiUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCustomizeUiUpdate(CommonParam param, object data)
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
                    ISdaCustomizeUiUpdate behavior = SdaCustomizeUiUpdateBehaviorFactory.MakeISdaCustomizeUiUpdate(param, entity);
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
