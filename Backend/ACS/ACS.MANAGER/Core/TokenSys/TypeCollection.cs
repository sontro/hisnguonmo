using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Authentication;
using ACS.SDO;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.TokenSys
{
    partial class TypeCollection
    {
        internal static readonly List<Type> AcsToken = new List<Type>() { typeof(AcsTokenAuthenticationSDO), typeof(AcsTokenLoginSDO), typeof(LoginByAuthenRequestTDO), typeof(TokenSysAuthenticationResourceSDO), typeof(LoginBySecretKeySDO), typeof(AcsTokenSyncInsertSDO), typeof(AcsTokenSyncDeleteSDO), typeof(LoginByEmailTDO), typeof(List<ACS.SDO.AcsAuthorizeSDO>), typeof(List<ACS.SDO.AcsCredentialTrackingSDO>) };
    }
}
