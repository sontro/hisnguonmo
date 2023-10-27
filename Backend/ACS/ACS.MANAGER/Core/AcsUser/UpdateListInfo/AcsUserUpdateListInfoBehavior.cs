using ACS.ApiConsumerManager;
using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Common.Mail;
using Inventec.Core;
using SMS.SDO;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ACS.MANAGER.Core.AcsUser.UpdateListInfo
{
    class AcsUserUpdateListInfoBehaviorEv : BeanObjectBase, IAcsUserUpdateListInfo
    {
        List<ACS.SDO.AcsUserUpdateInfoSDO> entity;

        internal AcsUserUpdateListInfoBehaviorEv(CommonParam param, List<ACS.SDO.AcsUserUpdateInfoSDO> data)
            : base(param)
        {
            entity = data;
        }

        /// <summary>
        /// - Đầu vào là 1 danh sách tài khoản đăng nhập
        ///- Lấy tài khoản theo loginname (không theo id).
        ///- Thực hiện update các thông tin Username, mobile, email (khong update password) kể cả tài khoản đang bị khóa
        ///- Những tài khoản nào không tìm được acs_user theo loginname thì bỏ quả (trả về thành công hoặc 1 mã lỗi duy nhất để bên Cos biết được là do tài khoản kia không tồn tại) và vẫn xử lý các tài khoản lấy được acs_user theo loginname.       
        ///- Ouput: true/false
        /// </summary>
        /// <returns></returns>
        bool IAcsUserUpdateListInfo.Run()
        {
            bool result = false;
            try
            {
                if (Valid())
                {
                    foreach (var data in entity)
                    {
                        ACS_USER user = (data != null && !String.IsNullOrEmpty(data.LoginName)) ? new AcsUserBO().Get<ACS_USER>(data.LoginName.ToLower()) : null;

                        if (user != null)
                        {
                            user.USERNAME = data.UserName;
                            user.MOBILE = data.Mobile;
                            user.EMAIL = data.Email;
                            if (DAOWorker.AcsUserDAO.Update(user))
                            {
                                result = true;
                            }
                            else
                            {
                                Inventec.Common.Logging.LogSystem.Warn("Yeu cau cap nhat thong tin tai khoan that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));

                                var bug = ACS.LibraryBug.DatabaseBug.Get(ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                                if (bug != null && param.BugCodes != null && !param.BugCodes.Contains(bug.code))
                                    param.BugCodes.Add(bug.code);

                                string error = String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsUser_CapNhatThongTinTaiKhoanThatBai), user.USERNAME + "(" + user.LOGINNAME + ")");
                                if (param.Messages != null && !param.Messages.Contains(error))
                                    param.Messages.Add(error);
                            }
                        }
                        else
                        {
                            var bug = ACS.LibraryBug.DatabaseBug.Get(ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                            if (bug != null && param.BugCodes != null && !param.BugCodes.Contains(bug.code))
                                param.BugCodes.Add(bug.code);

                            string error = String.Format("{0}: {1}.", MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsUser_TenDangNhapKhongChinhXac), data.LoginName);
                            if (param.Messages != null && !param.Messages.Contains(error))
                                param.Messages.Add(error);

                            Inventec.Common.Logging.LogSystem.Warn("Yeu cau cap nhat thong tin tai khoan that bai. Thong tin gui len khong hop le. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
                        }
                    }
                }
                else
                {
                    var bug = ACS.LibraryBug.DatabaseBug.Get(ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                    if (bug != null && param.BugCodes != null && !param.BugCodes.Contains(bug.code))
                        param.BugCodes.Add(bug.code);

                    string error = MessageUtil.GetMessage(LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                    if (param.Messages != null && !param.Messages.Contains(error))
                        param.Messages.Add(error);

                    Inventec.Common.Logging.LogSystem.Warn("Yeu cau cap nhat thong tin tai khoan that bai. Thong tin gui len khong hop le. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        bool Valid()
        {
            bool valid = true;
            valid = valid && entity != null;
            valid = valid && (entity.Count > 0);

            return valid;
        }
    }
}
