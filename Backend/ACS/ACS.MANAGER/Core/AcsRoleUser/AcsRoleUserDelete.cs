using ACS.MANAGER.Core.AcsRoleUser.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleUserDelete(CommonParam param, object data)
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
                    IAcsRoleUserDelete behavior = AcsRoleUserDeleteBehaviorFactory.MakeIAcsRoleUserDelete(param, entity);
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
