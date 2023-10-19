using ACS.MANAGER.Core.AcsUser.Activate;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserActivate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserActivate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                IAcsUserActivate behavior = AcsUserActivateBehaviorFactory.MakeIAcsUserActivate(param, entity);
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
