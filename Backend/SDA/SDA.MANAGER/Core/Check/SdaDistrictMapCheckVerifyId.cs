using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaDistrictMap;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaDistrictMapCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_DISTRICT_MAP raw = new SdaDistrictMapBO().Get<SDA_DISTRICT_MAP>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_DISTRICT_MAP raw)
        {
            bool result = true;
            try
            {
                raw = new SdaDistrictMapBO().Get<SDA_DISTRICT_MAP>(id);
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
