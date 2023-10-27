using ACS.MANAGER.Core.AcsControlRole.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole
{
    partial class AcsControlRoleUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsControlRoleUpdate(CommonParam param, object data)
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
                    IAcsControlRoleUpdate behavior = AcsControlRoleUpdateBehaviorFactory.MakeIAcsControlRoleUpdate(param, entity);
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
