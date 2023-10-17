using SAR.MANAGER.Core.SarForm.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm
{
    partial class SarFormDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarForm.Contains(entity.GetType()))
                {
                    ISarFormDelete behavior = SarFormDeleteBehaviorFactory.MakeISarFormDelete(param, entity);
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
