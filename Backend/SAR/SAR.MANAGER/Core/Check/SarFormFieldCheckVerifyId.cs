using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Core.SarFormField;
using Inventec.Core;
using System;

namespace SAR.MANAGER.Core.Check
{
    class SarFormFieldCheckVerifyId
    {
        internal static bool Verify(CommonParam param, long id)
        {
            bool result = true;
            try
            {
                SAR_FORM_FIELD raw = new SarFormFieldBO().Get<SAR_FORM_FIELD>(id);
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

        internal static bool Verify(CommonParam param, long id, ref SAR_FORM_FIELD raw)
        {
            bool result = true;
            try
            {
                raw = new SarFormFieldBO().Get<SAR_FORM_FIELD>(id);
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
