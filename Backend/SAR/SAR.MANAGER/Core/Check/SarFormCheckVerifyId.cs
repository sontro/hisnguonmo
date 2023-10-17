using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarForm;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.Check
{
    class SarFormCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SAR_FORM raw = new SarFormBO().Get<SAR_FORM>(id);
                if (raw == null)
                {
                    result = false;
                    SAR.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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

        internal static bool Verify(CommonParam param, long id, ref SAR_FORM raw)
        {
            bool result = true;
            try
            {
                raw = new SarFormBO().Get<SAR_FORM>(id);
                if (raw == null)
                {
                    result = false;
                    SAR.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
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
