using ACS.DAO.StagingObject;
using ACS.EFMODEL.DataModels;
using ACS.Filter;
using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.TokenSys.Authentication
{
    internal class TokenSysAuthenticationResourceSDO
    {
        internal string ApplicationCode { get; set; }
        internal string LoginName { get; set; }
    }
}
