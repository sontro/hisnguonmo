using SAR.MANAGER.Core.SarForm.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm
{
    partial class SarFormUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormUpdate(CommonParam param, object data)
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
                    ISarFormUpdate behavior = SarFormUpdateBehaviorFactory.MakeISarFormUpdate(param, entity);
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
