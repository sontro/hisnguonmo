using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.HisEmployee
{
    partial class HisEmployeeUtil : BusinessBase
    {
        public static string GetTitle(string loginName)
        {
            HIS_EMPLOYEE employee = HisEmployeeUtil.GetEmployee(loginName);
            return employee != null ? employee.TITLE : null;
        }

        public static HIS_EMPLOYEE GetEmployee(string loginName)
        {
            return HisEmployeeCFG.DATA != null && !string.IsNullOrWhiteSpace(loginName) ?
                HisEmployeeCFG.DATA.Where(o => o.LOGINNAME == loginName).FirstOrDefault() : null;
        }

        public static HIS_EMPLOYEE GetEmployee()
        {
            string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            return GetEmployee(loginName);
        }

        public static bool IsAdmin()
        {
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                return IsAdmin(loginName);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public static bool IsAdmin(string loginname)
        {
            try
            {
                return HisEmployeeCFG.DATA != null
                    && !string.IsNullOrWhiteSpace(loginname)
                    && HisEmployeeCFG.DATA.Exists(o => o.LOGINNAME == loginname && o.IS_ADMIN == Constant.IS_TRUE);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Kiem tra co phai tai khoan quan tri ko, co tra ve thong bao
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool CheckAdmin(CommonParam param)
        {
            bool result = true;
            try
            {
                if (!HisEmployeeUtil.IsAdmin())
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanKhongPhaiLaQuanTri);
                    return false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static bool IsDoctor()
        {
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                return IsDoctor(loginName);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public static bool IsDoctor(string loginname)
        {
            try
            {
                return HisEmployeeCFG.DATA != null
                    && !string.IsNullOrWhiteSpace(loginname)
                    && HisEmployeeCFG.DATA.Exists(o => o.LOGINNAME == loginname && o.IS_DOCTOR == Constant.IS_TRUE);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public static bool HasDiploma()
        {
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                return HasDiploma(loginName);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public static bool HasDiploma(string loginname)
        {
            try
            {
                return HisEmployeeCFG.DATA != null
                    && !string.IsNullOrWhiteSpace(loginname)
                    && HisEmployeeCFG.DATA.Exists(o => o.LOGINNAME == loginname && !string.IsNullOrWhiteSpace(o.DIPLOMA));
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public static bool IsSampleDepartment(string creator)
        {
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                return IsSampleDepartment(creator, loginName);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public static bool IsSampleDepartment(string creator, string loginname)
        {
            try
            {
                bool valid = true;

                valid = valid && HisEmployeeCFG.DATA != null;
                valid = valid && !string.IsNullOrWhiteSpace(creator);
                valid = valid && !string.IsNullOrWhiteSpace(loginname);

                if (valid)
                {
                    HIS_EMPLOYEE crt = HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == creator && o.DEPARTMENT_ID.HasValue);
                    HIS_EMPLOYEE ln = HisEmployeeCFG.DATA.FirstOrDefault(o => o.LOGINNAME == loginname && o.DEPARTMENT_ID.HasValue);

                    valid = valid && crt != null && ln != null && crt.DEPARTMENT_ID == ln.DEPARTMENT_ID;
                }

                return valid;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
