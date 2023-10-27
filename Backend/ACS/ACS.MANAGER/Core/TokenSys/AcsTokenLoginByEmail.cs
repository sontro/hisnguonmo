using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Login;
using ACS.MANAGER.Core.TokenSys.LoginByEmail;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class AcsTokenLoginByEmail : BeanObjectBase, ITokenSysLoginByEmail
    {
        object entity;

        internal AcsTokenLoginByEmail(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        Inventec.Token.Core.TokenData ITokenSysLoginByEmail.Run()
        {
            Inventec.Token.Core.TokenData result = null;
            try
            {
                if (TypeCollection.AcsToken.Contains(entity.GetType()))
                {
                    ITokenSysLoginByEmail behavior = TokenSysLoginByEmailBehaviorFactory.MakeIAcsTokenLoginByEmail(param, entity);
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
