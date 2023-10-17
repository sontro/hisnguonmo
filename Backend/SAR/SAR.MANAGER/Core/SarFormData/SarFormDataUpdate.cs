using SAR.MANAGER.Core.SarFormData.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData
{
    partial class SarFormDataUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormDataUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarFormData.Contains(entity.GetType()))
                {
                    ISarFormDataUpdate behavior = SarFormDataUpdateBehaviorFactory.MakeISarFormDataUpdate(param, entity);
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
