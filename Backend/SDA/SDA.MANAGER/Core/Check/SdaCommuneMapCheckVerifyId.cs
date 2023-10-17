using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaCommuneMap;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaCommuneMapCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_COMMUNE_MAP raw = new SdaCommuneMapBO().Get<SDA_COMMUNE_MAP>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_COMMUNE_MAP raw)
        {
            bool result = true;
            try
            {
                raw = new SdaCommuneMapBO().Get<SDA_COMMUNE_MAP>(id);
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
