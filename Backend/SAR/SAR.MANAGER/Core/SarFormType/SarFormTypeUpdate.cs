using SAR.MANAGER.Core.SarFormType.Update;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType
{
    partial class SarFormTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormTypeUpdate(CommonParam param, object data)
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
                    ISarFormTypeUpdate behavior = SarFormTypeUpdateBehaviorFactory.MakeISarFormTypeUpdate(param, entity);
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
