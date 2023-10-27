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
using System.Configuration;

namespace ACS.MANAGER.Core.AcsUser.InActive
{
    class AcsUserInActiveBehaviorEv : BeanObjectBase, IAcsUserInActive
    {
        AcsUserActivationRequiredSDO entity;

        internal AcsUserInActiveBehaviorEv(CommonParam param, AcsUserActivationRequiredSDO data)
            : base(param)
        {
            entity = data;
        }

        /// <summary>
        /// + Kiểm tra xem SĐT và tên đăng nhập có hợp lệ ko. Nếu hợp lệ thì xử lý tiếp các bước sau:
        ///+ Sinh ra mã kích hoạt (ACTIVATION_CODE), cập nhật dữ liệu ACTIVATION_CODE, ACTIVATION_CODE_EXPIRED_TIME (lấy thời gian hiện tại + số phút, số phút này cho phép người dùng cấu hình) vào bảng ACS_USER
        ///+ Gọi đến tổng đài SMS, thực hiện nhắn tin mã kích hoạt theo SĐT của người dùng
        ///- Ouput: true/false
        /// </summary>
        /// <returns></returns>
        bool IAcsUserInActive.Run()
        {
            bool result = false;
            try
            {
                ACS_USER user = Valid() ? new AcsUserBO().Get<ACS_USER>(entity.LOGINNAME.ToLower()) : null;
                if (user != null)
                {
                    if (user != null)
                    {                       
                        user.IS_ACTIVE = 0;
                        if (DAOWorker.AcsUserDAO.Update(user))
                        {
                            result = true;
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Warn("Yeu cau huy kich hoat tai khoan. Cap nhat reset thong tin ma kich hoat va thoi han kich hoat vao bang acs_user that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                            ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_YeuCaukichHoatTaiKhoanThatBai);
                        }
                    }
                    else
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                        Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Huy kich hoat tai khoan that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    }
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
            //Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
            return entity != null && !String.IsNullOrEmpty(entity.LOGINNAME);
        }
    }
}
