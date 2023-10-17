using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.GrantPermission
{
    public class GrantPermissionCheck: BusinessBase
    {
        internal GrantPermissionCheck()
            : base()
        {

        }

        internal GrantPermissionCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValid(GrantPermissionSDO sdo)
        {
            bool valid = true;
            try
            {
                if (sdo.DataId <= 0)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sdo.DataId <= 0");
                    return false;
                }
                if (sdo.Table == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sdo.Table null");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
