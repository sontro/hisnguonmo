using ACS.EFMODEL.DataModels;
using ACS.SDO;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser
{
    partial class TypeCollection
    {
        internal static readonly List<Type> AcsRoleUser = new List<Type>() { typeof(ACS_ROLE_USER), typeof(List<ACS_ROLE_USER>), typeof(List<long>), typeof(AcsRoleUserForUpdateSDO) };
    }
}
