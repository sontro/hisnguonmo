using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.Check
{
    class SdaConfigAppUserCheckVerifyValidData
    {
        internal static bool Verify(CommonParam param, SDA_CONFIG_APP_USER data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
            }
            catch (ArgumentNullException ex)
            {
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
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

        internal static bool Verify(CommonParam param, List<SDA_CONFIG_APP_USER> datas)
        {
            bool result = true;
            try
            {
                if (datas == null) throw new ArgumentNullException("datas");
                foreach (var data in datas)
                {
                    if (data.CONFIG_APP_ID == 0) throw new ArgumentNullException("CONFIG_APP_ID");
                    if (String.IsNullOrEmpty(data.LOGINNAME)) throw new ArgumentNullException("LOGINNAME");
                    if (String.IsNullOrEmpty(data.VALUE)) throw new ArgumentNullException("VALUE");
                }
            }
            catch (ArgumentNullException ex)
            {
                SDA.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
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
