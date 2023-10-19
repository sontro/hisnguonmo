using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.TokenSys.Authentication;
using ACS.SDO;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsUser
{
    partial class TypeCollection
    {
        internal static readonly List<Type> AcsUser = new List<Type>() { typeof(ACS_USER), typeof(List<ACS_USER>), typeof(CreateAndGrantUserSDO), typeof(AcsUserChangePasswordSDO), typeof(UserRoleCopySDO), typeof(AcsUserResetPasswordTDO), typeof(AcsUserUpdateLoginNameTDO), typeof(AcsUserCheckResetPasswordTDO), typeof(List<ACS.SDO.CreateAndGrantUserSDO>), typeof(AcsUserChangePasswordWithOtpSDO), typeof(OtpRequiredSDO) };
    }
}
