using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarFormData;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.Check
{
    class SarFormDataCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SAR_FORM_DATA raw = new SarFormDataBO().Get<SAR_FORM_DATA>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SAR_FORM_DATA raw)
        {
            bool result = true;
            try
            {
                raw = new SarFormDataBO().Get<SAR_FORM_DATA>(id);
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
