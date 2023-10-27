using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.Token
{
    public sealed partial class TokenManager  : Inventec.Backend.MANAGER.BusinessBase
    {
        private const string LANGUAGE = "LANGUAGE";

        public ApiResultObject<bool> SetLanguage(string language)
        {
            try
            {
                return new ApiResultObject<bool>(Inventec.Token.ResourceSystem.ResourceTokenManager.SetCredentialData(LANGUAGE, language));
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return new ApiResultObject<bool>(false);
            }
        }
    }
}
