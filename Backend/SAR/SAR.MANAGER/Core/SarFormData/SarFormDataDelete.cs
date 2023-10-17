using SAR.MANAGER.Core.SarFormData.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormData
{
    partial class SarFormDataDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormDataDelete(CommonParam param, object data)
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
                    ISarFormDataDelete behavior = SarFormDataDeleteBehaviorFactory.MakeISarFormDataDelete(param, entity);
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
