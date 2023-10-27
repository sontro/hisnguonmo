using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleBase
{
    partial class TypeCollection
    {
        internal static readonly List<Type> AcsRoleBase = new List<Type>() { typeof(ACS_ROLE_BASE), typeof(List<ACS_ROLE_BASE>), typeof(List<long>) };
    }
}
