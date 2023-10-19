using ACS.MANAGER.Core.AcsActivityType.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType
{
    partial class AcsActivityTypeDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsActivityTypeDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsActivityType.Contains(entity.GetType()))
                {
                    IAcsActivityTypeDelete behavior = AcsActivityTypeDeleteBehaviorFactory.MakeIAcsActivityTypeDelete(param, entity);
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
