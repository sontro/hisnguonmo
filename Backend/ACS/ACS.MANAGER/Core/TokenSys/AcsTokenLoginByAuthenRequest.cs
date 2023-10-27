using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Login;
using ACS.MANAGER.Core.TokenSys.LoginByAuthenRequest;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class AcsTokenLoginByAuthenRequest : BeanObjectBase, ILoginByAuthenRequest
    {
        object entity;

        internal AcsTokenLoginByAuthenRequest(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        Inventec.Token.Core.TokenData ILoginByAuthenRequest.Run()
        {
            Inventec.Token.Core.TokenData result = null;
            try
            {
                if (TypeCollection.AcsToken.Contains(entity.GetType()))
                {
                    ILoginByAuthenRequest behavior = LoginByAuthenRequestBehaviorFactory.MakeIAcsTokenLoginByAuthenRequest(param, entity);
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
