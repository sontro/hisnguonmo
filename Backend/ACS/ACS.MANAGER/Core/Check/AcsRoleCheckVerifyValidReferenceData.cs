using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace ACS.MANAGER.Core.Check
{
    class AcsRoleCheckVerifyValidReferenceData
    {
        internal bool Verify(CommonParam param, AcsRoleSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");

                List<long> acsRoleBaseIds = new List<long>();
                acsRoleBaseIds = GetEvAcsRoleBaseDTOs(param, data).Select(o => o.ROLE_BASE_ID).ToList();
                if (acsRoleBaseIds != null && acsRoleBaseIds.Count > 0)
                {
                    var roleBases = new ACS.MANAGER.Core.AcsRole.AcsRoleBO().GetRoleBase<List<ACS_ROLE_BASE>>(acsRoleBaseIds);
                    if (roleBases != null && roleBases.Count > 0)
                    {
                        var roleCheck = roleBases.Where(o => o.ROLE_BASE_ID == data.ID).ToList();
                        if ((roleCheck != null && roleCheck.Count > 0))
                        {
                            valid = false;
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsRole_VaiTroKeThuaDaDuocKeThuaTuVaiTroDangChon);
                        }
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private List<ACS_ROLE_BASE> GetEvAcsRoleBaseDTOs(CommonParam param, AcsRoleSDO data)
        {
            List<ACS_ROLE_BASE> result = new List<ACS_ROLE_BASE>();
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!String.IsNullOrEmpty(data.ROLE_BASE_ID.Trim()))
                {
                    string[] lines = Regex.Split(data.ROLE_BASE_ID, ",");
                    if (lines != null && lines.Length > 0)
                    {
                        foreach (var item in lines)
                        {
                            long roleId = Inventec.Common.TypeConvert.Parse.ToInt64(item);
                            if (roleId > 0)
                            {
                                ACS_ROLE_BASE rb = new ACS_ROLE_BASE();
                                rb.ROLE_ID = data.ID;
                                rb.ROLE_BASE_ID = roleId;
                                result.Add(rb);
                            }
                        }
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
                param.HasException = true;
            }

            return result;
        }
    }
}
