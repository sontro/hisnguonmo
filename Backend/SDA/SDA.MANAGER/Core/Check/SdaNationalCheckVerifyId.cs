using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaNational;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaNationalCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_NATIONAL raw = new SdaNationalBO().Get<SDA_NATIONAL>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_NATIONAL raw)
        {
            bool result = true;
            try
            {
                raw = new SdaNationalBO().Get<SDA_NATIONAL>(id);
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
