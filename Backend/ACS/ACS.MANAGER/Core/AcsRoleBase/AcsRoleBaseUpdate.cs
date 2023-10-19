using ACS.MANAGER.Core.AcsRoleBase.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase
{
    partial class AcsRoleBaseUpdate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsRoleBaseUpdate(CommonParam param, object data)
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
                    IAcsRoleBaseUpdate behavior = AcsRoleBaseUpdateBehaviorFactory.MakeIAcsRoleBaseUpdate(param, entity);
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
