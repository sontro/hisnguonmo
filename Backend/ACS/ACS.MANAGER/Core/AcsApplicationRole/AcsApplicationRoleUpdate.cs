using ACS.MANAGER.Core.AcsApplicationRole.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole
{
    partial class AcsApplicationRoleUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsApplicationRoleUpdate(CommonParam param, object data)
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
                    IAcsApplicationRoleUpdate behavior = AcsApplicationRoleUpdateBehaviorFactory.MakeIAcsApplicationRoleUpdate(param, entity);
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
