using SAR.MANAGER.Core.SarFormField.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField
{
    partial class SarFormFieldUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormFieldUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarFormField.Contains(entity.GetType()))
                {
                    ISarFormFieldUpdate behavior = SarFormFieldUpdateBehaviorFactory.MakeISarFormFieldUpdate(param, entity);
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
