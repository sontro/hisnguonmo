using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Login;
using ACS.MANAGER.Core.TokenSys.LoginBySecretKey;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class AcsTokenLoginBySecretKey : BeanObjectBase, ITokenSysLoginBySecretKey
    {
        object entity;

        internal AcsTokenLoginBySecretKey(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        Inventec.Token.Core.TokenData ITokenSysLoginBySecretKey.Run()
        {
            Inventec.Token.Core.TokenData result = null;
            try
            {
                if (TypeCollection.AcsToken.Contains(entity.GetType()))
                {
                    ITokenSysLoginBySecretKey behavior = TokenSysLoginBySecretKeyBehaviorFactory.MakeIAcsTokenLoginBySecretKey(param, entity);
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
