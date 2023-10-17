using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaDeleteData;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaDeleteDataCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_DELETE_DATA raw = new SdaDeleteDataBO().Get<SDA_DELETE_DATA>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_DELETE_DATA raw)
        {
            bool result = true;
            try
            {
                raw = new SdaDeleteDataBO().Get<SDA_DELETE_DATA>(id);
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
