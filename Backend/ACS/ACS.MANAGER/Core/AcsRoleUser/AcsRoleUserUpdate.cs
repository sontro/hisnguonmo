using ACS.MANAGER.Core.AcsRoleUser.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleUserUpdate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsRoleUser.Contains(entity.GetType()))
                {
                    IAcsRoleUserUpdate behavior = AcsRoleUserUpdateBehaviorFactory.MakeIAcsRoleUserUpdate(param, entity);
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
