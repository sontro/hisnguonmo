using ACS.MANAGER.Core.AcsUser.ActivationRequired;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserActivationRequired : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserActivationRequired(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                IAcsUserActivationRequired behavior = AcsUserActivationRequiredBehaviorFactory.MakeIAcsUserActivationRequired(param, entity);
                result = behavior != null ? behavior.Run() : false;
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
