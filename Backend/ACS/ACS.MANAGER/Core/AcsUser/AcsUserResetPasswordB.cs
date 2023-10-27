using ACS.MANAGER.Core.AcsUser.ResetPasswordB;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserResetPasswordB : BeanObjectBase, IAcsUserResetPasswordB
    {
        object entity;

        internal AcsUserResetPasswordB(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        AcsCheckResetPasswordResultTDO IAcsUserResetPasswordB.Run()
        {
            AcsCheckResetPasswordResultTDO result = null;
            try
            {
                if (TypeCollection.AcsUser.Contains(entity.GetType()))
                {
                    IAcsUserResetPasswordB behavior = AcsUserResetPasswordBBehaviorFactory.MakeIAcsUserResetPasswordB(param, entity);
                    result = behavior != null ? behavior.Run() : null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
