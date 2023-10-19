using ACS.MANAGER.Core.AcsUser.ResetPassword;
using ACS.MANAGER.Core.AcsUser.Update;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserResetPassword : BeanObjectBase, IDelegacy
    {
        object entity;

        internal AcsUserResetPassword(CommonParam param, object data)
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
                    IAcsUserResetPassword behavior = AcsUserResetPasswordBehaviorFactory.MakeIAcsUserResetPassword(param, entity);
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
