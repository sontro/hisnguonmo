using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Login;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class AcsTokenLogin : BeanObjectBase, ITokenDelegacy
    {
        object entity;

        internal AcsTokenLogin(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        ACS_USER ITokenDelegacy.Execute()
        {
            ACS_USER result = null;
            try
            {
                if (TypeCollection.AcsToken.Contains(entity.GetType()))
                {
                    ITokenSysLogin behavior = TokenSysLoginBehaviorFactory.MakeIAcsTokenLogin(param, entity);
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
