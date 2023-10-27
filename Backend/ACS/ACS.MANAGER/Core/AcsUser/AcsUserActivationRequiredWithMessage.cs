using ACS.MANAGER.Core.AcsUser.ActivationRequiredWithMessage;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserActivationRequiredWithMessage : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserActivationRequiredWithMessage(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                IAcsUserActivationRequiredWithMessage behavior = AcsUserActivationRequiredWithMessageBehaviorFactory.MakeIAcsUserActivationRequiredWithMessage(param, entity);
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
