using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Core.SdaGroupType;
using Inventec.Core;
using System;

namespace SDA.MANAGER.Core.Check
{
    class SdaGroupTypeCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SDA_GROUP_TYPE raw = new SdaGroupTypeBO().Get<SDA_GROUP_TYPE>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SDA_GROUP_TYPE raw)
        {
            bool result = true;
            try
            {
                raw = new SdaGroupTypeBO().Get<SDA_GROUP_TYPE>(id);
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
