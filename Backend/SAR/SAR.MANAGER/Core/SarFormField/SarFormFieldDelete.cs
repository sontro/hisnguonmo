using SAR.MANAGER.Core.SarFormField.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField
{
    partial class SarFormFieldDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormFieldDelete(CommonParam param, object data)
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
                    ISarFormFieldDelete behavior = SarFormFieldDeleteBehaviorFactory.MakeISarFormFieldDelete(param, entity);
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
