using ACS.EFMODEL.DataModels;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.Check
{
    class AcsRoleUserCheckVerifyValidDataForGetTree
    {
        internal static bool Verify(CommonParam param, AcsRoleUserForUpdateSDO data)
        {
            bool result = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.User == null) throw new ArgumentNullException("User");
                if (data.User.ID <= 0) throw new ArgumentNullException("USER_ID");
                //if (data.RoleUsers == null || data.RoleUsers.Count == 0) throw new ArgumentNullException("RoleUsers");
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
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
