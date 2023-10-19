using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Authentication;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class AcsTokenAuthenticationResource : BeanObjectBase, ITokenDelegacy
    {
        object entity;

        internal AcsTokenAuthenticationResource(CommonParam param, object data)
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
                    ITokenSysAuthenticationResource behavior = TokenSysAuthenticationResourceBehaviorFactory.MakeIAcsTokenAuthentication(param, entity);
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
