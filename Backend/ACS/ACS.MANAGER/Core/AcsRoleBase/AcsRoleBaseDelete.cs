using ACS.MANAGER.Core.AcsRoleBase.Delete;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase
{
    partial class AcsRoleBaseDelete : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleBaseDelete(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsRoleBase.Contains(entity.GetType()))
                {
                    IAcsRoleBaseDelete behavior = AcsRoleBaseDeleteBehaviorFactory.MakeIAcsRoleBaseDelete(param, entity);
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
