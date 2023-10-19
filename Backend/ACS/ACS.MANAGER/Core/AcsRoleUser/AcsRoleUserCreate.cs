using ACS.MANAGER.Core.AcsRoleUser.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class AcsRoleUserCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleUserCreate(CommonParam param, object data)
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
                    IAcsRoleUserCreate behavior = AcsRoleUserCreateBehaviorFactory.MakeIAcsRoleUserCreate(param, entity);
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
