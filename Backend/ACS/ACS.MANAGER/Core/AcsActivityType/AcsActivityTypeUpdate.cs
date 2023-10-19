using ACS.MANAGER.Core.AcsActivityType.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsActivityType
{
    partial class AcsActivityTypeUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsActivityTypeUpdate(CommonParam param, object data)
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
                    IAcsActivityTypeUpdate behavior = AcsActivityTypeUpdateBehaviorFactory.MakeIAcsActivityTypeUpdate(param, entity);
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
