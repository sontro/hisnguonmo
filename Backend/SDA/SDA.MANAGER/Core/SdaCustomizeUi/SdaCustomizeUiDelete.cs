using SDA.MANAGER.Core.SdaCustomizeUi.Delete;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.SdaCustomizeUi
{
    partial class SdaCustomizeUiDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SdaCustomizeUiDelete(CommonParam param, object data)
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
                    ISdaCustomizeUiDelete behavior = SdaCustomizeUiDeleteBehaviorFactory.MakeISdaCustomizeUiDelete(param, entity);
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
