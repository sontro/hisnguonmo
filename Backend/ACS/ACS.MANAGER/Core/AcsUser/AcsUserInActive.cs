using ACS.MANAGER.Core.AcsUser.InActive;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserInActive : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserInActive(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                IAcsUserInActive behavior = AcsUserInActiveBehaviorFactory.MakeIAcsUserInActive(param, entity);
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
