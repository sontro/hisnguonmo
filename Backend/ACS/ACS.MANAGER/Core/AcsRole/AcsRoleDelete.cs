using ACS.MANAGER.Core.AcsRole.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole
{
    partial class AcsRoleDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsRole.Contains(entity.GetType()))
                {
                    IAcsRoleDelete behavior = AcsRoleDeleteBehaviorFactory.MakeIAcsRoleDelete(param, entity);
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
