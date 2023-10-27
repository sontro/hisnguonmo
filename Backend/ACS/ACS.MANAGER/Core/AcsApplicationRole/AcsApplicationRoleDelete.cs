using ACS.MANAGER.Core.AcsApplicationRole.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole
{
    partial class AcsApplicationRoleDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationRoleDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsApplicationRole.Contains(entity.GetType()))
                {
                    IAcsApplicationRoleDelete behavior = AcsApplicationRoleDeleteBehaviorFactory.MakeIAcsApplicationRoleDelete(param, entity);
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
