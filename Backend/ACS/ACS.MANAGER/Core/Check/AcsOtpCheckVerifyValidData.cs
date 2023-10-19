using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsOtp;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.Check
{
    class AcsOtpCheckVerifyValidData
    {
        internal static bool Verify(CommonParam param, AcsOtpFilterQuery data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MOBILE__EXACT == null) throw new ArgumentNullException("MOBILE__EXACT");
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                param.Messages.Add(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc));
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = false;
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
