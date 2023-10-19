using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Core.AcsCredentialData;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.Check
{
    class AcsCredentialDataCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                ACS_CREDENTIAL_DATA raw = new AcsCredentialDataBO().Get<ACS_CREDENTIAL_DATA>(id);
                if (raw == null)
                {
                    result = false;
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal static bool Verify(CommonParam param, long id, ref ACS_CREDENTIAL_DATA raw)
        {
            bool result = true;
            try
            {
                raw = new AcsCredentialDataBO().Get<ACS_CREDENTIAL_DATA>(id);
                if (raw == null)
                {
                    result = false;
                    ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }
    }
}
