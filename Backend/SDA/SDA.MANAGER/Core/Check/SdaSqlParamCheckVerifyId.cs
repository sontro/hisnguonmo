using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaSqlParam;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaSqlParamCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_SQL_PARAM raw = new SdaSqlParamBO().Get<SDA_SQL_PARAM>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_SQL_PARAM raw)
        {
            bool result = true;
            try
            {
                raw = new SdaSqlParamBO().Get<SDA_SQL_PARAM>(id);
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
