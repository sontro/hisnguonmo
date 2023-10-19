using ACS.MANAGER.Core.AcsRoleBase.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase
{
    partial class AcsRoleBaseCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleBaseCreate(CommonParam param, object data)
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
                    IAcsRoleBaseCreate behavior = AcsRoleBaseCreateBehaviorFactory.MakeIAcsRoleBaseCreate(param, entity);
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
