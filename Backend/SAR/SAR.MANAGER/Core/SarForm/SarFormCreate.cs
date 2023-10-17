using SAR.MANAGER.Core.SarForm.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarForm
{
    partial class SarFormCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormCreate(CommonParam param, object data)
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
                    ISarFormCreate behavior = SarFormCreateBehaviorFactory.MakeISarFormCreate(param, entity);
                    result = behavior != null ? behavior.Run() : false;
                }
                else if (entity.GetType() == typeof(SAR.SDO.SarFormCreateOrUpdateSDO))
                {
                    ISarFormCreate behavior = SarFormCreateBehaviorFactory.MakeISarFormCreate(param, entity);
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
