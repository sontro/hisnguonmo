using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsRoleUser;
using ACS.MANAGER.Core.AcsRoleUser.Get;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACS.MANAGER.Core.AcsUser.CopyRole
{
    class AcsUserCopyRoleBehavior : BeanObjectBase, IAcsUserCopyRole
    {
        UserRoleCopySDO entity;
        ACS_USER userCopy;
        ACS_USER userPaste;

        internal AcsUserCopyRoleBehavior(CommonParam param, UserRoleCopySDO data)
            : base(param)
        {
            entity = data;
        }

        List<ACS_ROLE_USER> IAcsUserCopyRole.Run()
        {
            List<ACS_ROLE_USER> result = null;
            try
            {
                if (Check())
                {
                    List<ACS_ROLE_USER> listCreate = new List<ACS_ROLE_USER>();
                    var roleUserCopys = GetRoleUserByUserId(userCopy.ID);
                    var roleUserPastes = GetRoleUserByUserId(userPaste.ID);
                    if (roleUserCopys == null || roleUserCopys.Count == 0)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanChuaDuocThietLapQuyenTruyCapUngDung);
                        throw new NullReferenceException("user " + userCopy.LOGINNAME + " empty role");
                    }

                    if (roleUserPastes != null && roleUserPastes.Count > 0)
                    {
                        var oldRUIds = (roleUserPastes != null ? roleUserPastes.Select(o => o.ID).ToList() : null);
                        listCreate = (oldRUIds != null ? roleUserCopys.Where(o => !oldRUIds.Contains(o.ID)).ToList() : roleUserCopys);
                    }
                    else
                    {
                        listCreate.AddRange(roleUserCopys);
                    }
                    if (listCreate != null && listCreate.Count > 0)
                    {
                        foreach (var item in listCreate)
                        {
                            item.USER_ID = userPaste.ID;
                        }
                        if (DAOWorker.AcsRoleUserDAO.CreateList(listCreate))
                            result = listCreate;
                        else
                            result = null;
                    }
                    else
                        result = new List<ACS_ROLE_USER>();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        bool Check()
        {
            bool result = true;
            try
            {
                bool valid = (AcsUserCheckVerifyValidDataForAuthorize.Verify(param, ref userCopy, entity.CopyLoginname));
                valid = valid && (AcsUserCheckVerifyValidDataForAuthorize.Verify(param, ref userPaste, entity.PasteLoginname));

                if (!valid) throw new ArgumentNullException("CopyLoginname hoac PasteLoginname khong dung. CopyLoginname  = " + entity.CopyLoginname + " | PasteLoginname = " + entity.PasteLoginname);

                //valid = valid && CheckUserIsActive(userCopy);
                //valid = valid && CheckUserIsActive(userPaste);
            }
            catch (Exception ex)
            {
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private List<ACS_ROLE_USER> GetRoleUserByUserId(long userId)
        {
            try
            {
                AcsRoleUserFilterQuery filter = new AcsRoleUserFilterQuery();
                filter.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                filter.USER_ID = userId;
                return new AcsRoleUserBO().Get<List<ACS_ROLE_USER>>(filter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return null;
        }

        /// <summary>
        /// Ham kiem tra tai khoan co bi tam khoa hay khong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckUserIsActive(ACS_USER data)
        {
            bool valid = false;
            try
            {
                valid = (data != null && data.ID > 0 && data.IS_ACTIVE == IMSys.DbConfig.SDA_RS.COMMON.IS_ACTIVE__TRUE);
                if (!valid && data != null)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TaiKhoanDangBiTamKhoa);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
