using SAR.MANAGER.Core.SarFormType.Create;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.SarFormType
{
    partial class SarFormTypeCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal SarFormTypeCreate(CommonParam param, object data)
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
                    ISarFormTypeCreate behavior = SarFormTypeCreateBehaviorFactory.MakeISarFormTypeCreate(param, entity);
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
