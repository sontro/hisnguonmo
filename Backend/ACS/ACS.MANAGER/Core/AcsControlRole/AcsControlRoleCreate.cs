using ACS.MANAGER.Core.AcsControlRole.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole
{
    partial class AcsControlRoleCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlRoleCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsControlRole.Contains(entity.GetType()))
                {
                    IAcsControlRoleCreate behavior = AcsControlRoleCreateBehaviorFactory.MakeIAcsControlRoleCreate(param, entity);
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
