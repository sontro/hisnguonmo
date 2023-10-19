using ACS.MANAGER.Core.AcsUser.CheckResetPasswordB;
using ACS.MANAGER.Core.AcsUser.Update;
using ACS.SDO;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class AcsUserCheckResetPasswordB : BeanObjectBase, IAcsUserCheckResetPasswordB
    {
        object entity;

        internal AcsUserCheckResetPasswordB(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        AcsCheckResetPasswordResultTDO IAcsUserCheckResetPasswordB.Run()
        {
            AcsCheckResetPasswordResultTDO result = null;
            try
            {
                if (TypeCollection.AcsUser.Contains(entity.GetType()))
                {
                    IAcsUserCheckResetPasswordB behavior = AcsUserCheckResetPasswordBBehaviorFactory.MakeIAcsUserCheckResetPasswordB(param, entity);
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
