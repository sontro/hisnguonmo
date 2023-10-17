using SAR.MANAGER.Core.SarFormField.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormField
{
    partial class SarFormFieldCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormFieldCreate(CommonParam param, object data)
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
                    ISarFormFieldCreate behavior = SarFormFieldCreateBehaviorFactory.MakeISarFormFieldCreate(param, entity);
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
