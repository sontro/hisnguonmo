using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Authorize;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class AcsTokenAuthorize : BeanObjectBase, IAuthorizeDelegacy
    {
        object entity;

        internal AcsTokenAuthorize(CommonParam param, object data)
            : base(param)
        {
            entity = data;
        }

        ACS.SDO.AcsAuthorizeSDO IAuthorizeDelegacy.Execute()
        {
            ACS.SDO.AcsAuthorizeSDO result = null;
            try
            {
                if (TypeCollection.AcsToken.Contains(entity.GetType()))
                {
                    IAcsTokenAuthorize behavior = AcsTokenAuthorizeBehaviorFactory.MakeIAcsTokenLogin(param, entity);
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
