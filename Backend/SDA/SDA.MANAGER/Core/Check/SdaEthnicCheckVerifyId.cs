using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaEthnic;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaEthnicCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_ETHNIC raw = new SdaEthnicBO().Get<SDA_ETHNIC>(id);
                if (raw == null)
                {
                    result = false;
                    SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_ETHNIC raw)
        {
            bool result = true;
            try
            {
                raw = new SdaEthnicBO().Get<SDA_ETHNIC>(id);
                if (raw == null)
                {
                    result = false;
                    SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
