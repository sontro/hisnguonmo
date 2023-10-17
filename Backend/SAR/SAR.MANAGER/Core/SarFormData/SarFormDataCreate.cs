using SAR.MANAGER.Core.SarFormData.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData
{
    partial class SarFormDataCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormDataCreate(CommonParam param, object data)
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
                    ISarFormDataCreate behavior = SarFormDataCreateBehaviorFactory.MakeISarFormDataCreate(param, entity);
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
