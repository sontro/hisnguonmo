using ACS.MANAGER.Core.AcsUser.Create;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserCreate : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserCreate(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        bool IDelegacy.Execute()
        {
            bool result = false;
            try
            {
                if (TypeCollection.AcsUser.Contains(entity.GetType()))
                {
                    IAcsUserCreate behavior = AcsUserCreateBehaviorFactory.MakeIAcsUserCreate(param, entity);
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
