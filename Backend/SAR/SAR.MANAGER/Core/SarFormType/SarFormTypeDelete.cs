using SAR.MANAGER.Core.SarFormType.Delete;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType
{
    partial class SarFormTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormTypeDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.SarFormType.Contains(entity.GetType()))
                {
                    ISarFormTypeDelete behavior = SarFormTypeDeleteBehaviorFactory.MakeISarFormTypeDelete(param, entity);
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
