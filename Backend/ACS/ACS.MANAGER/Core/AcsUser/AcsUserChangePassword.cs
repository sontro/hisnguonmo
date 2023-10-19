using ACS.MANAGER.Core.AcsUser.ChangePassword;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserChangePassword : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserChangePassword(CommonParam param, object data)
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
                    IAcsUserChangePassword behavior = AcsUserChangePasswordBehaviorFactory.MakeIAcsUserChangePassword(param, entity);
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
