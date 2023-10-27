using ACS.MANAGER.Core.AcsControlRole.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole
{
    partial class AcsControlRoleDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlRoleDelete(CommonParam param, object data)
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
                    IAcsControlRoleDelete behavior = AcsControlRoleDeleteBehaviorFactory.MakeIAcsControlRoleDelete(param, entity);
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
