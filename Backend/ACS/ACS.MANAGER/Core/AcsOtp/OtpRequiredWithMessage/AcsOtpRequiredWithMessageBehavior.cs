using ACS.ApiConsumerManager;
using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsAppOtpType;
using ACS.MANAGER.Core.AcsAppOtpType.Get;
using ACS.MANAGER.Core.AcsOtpType;
using ACS.MANAGER.Core.AcsOtpType.Get;
using ACS.MANAGER.Core.AcsUser;
using ACS.MANAGER.Core.Check;
using ACS.SDO;
using Inventec.Common.Mail;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using SMS.SDO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ACS.MANAGER.Core.AcsOtp.OtpRequiredWithMessage
{
    class AcsOtpOtpRequiredWithMessageBehaviorEv : BeanObjectBase, IAcsOtpOtpRequiredWithMessage
    {
        OtpRequiredWithMessageSDO entity;
        ACS_APPLICATION application;
        ACS_APP_OTP_TYPE currentAppOtpType;
        ACS_OTP_TYPE currentOtpType;

        internal AcsOtpOtpRequiredWithMessageBehaviorEv(CommonParam param, OtpRequiredWithMessageSDO data)
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
        bool IAcsOtpOtpRequiredWithMessage.Run()
        {
            bool result = false;
            try
            {
                if (Valid())
                {
                    string mobile = GetPrefixMobileNumber(entity.Mobile);
                    string loginname = entity.LoginName;
                    ACS_USER user = !String.IsNullOrEmpty(entity.LoginName) ? new AcsUserBO().Get<ACS_USER>(entity.LoginName.ToLower()) : null;
                    if (user == null)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_TenDangNhapKhongChinhXac);
                        Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu truyen vao khong hop le. Gui yeu cau nhan tin ma otp doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                        return false;
                    }

                    if (user != null && GetPrefixMobileNumber(user.MOBILE) != mobile)
                    {
                        //ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_GuiYeuCauOtpDoiMatKhauSoDienThoaiGuiLenKhongKhopVoiSoDTDaDangKy);
                        Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu truyen vao khong hop le. Gui yeu cau nhan tin ma otp doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(mobile))
                        {
                            double minuteAdd = GetActivationMinitePlus();

                            ACS_OTP acsOtp = new ACS_OTP();
                            acsOtp.OTP_CODE = GenerateActivateCode();
                            acsOtp.EXPIRE_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now.AddMinutes(minuteAdd));
                            //acsOtp.OTP_TYPE = StandardConstant.OTP_TYPE_CHANGEPASS;//1:kích hoạt, 2:đổi mật khẩu, ...
                            acsOtp.OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__CHANGE_PASS;
                            acsOtp.LOGINAME = user.LOGINNAME;
                            acsOtp.USERNAME = user.USERNAME;
                            acsOtp.EMAIL = user.EMAIL;
                            acsOtp.MOBILE = GetPrefixMobileNumber(user.MOBILE);
                            if (DAOWorker.AcsOtpDAO.Create(acsOtp))
                            {
                                result = true;
                            }
                            else
                            {
                                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                            }

                            if (result && !this.SendActivateCode(mobile, acsOtp))
                            {
                                Inventec.Common.Logging.LogSystem.Warn("Yeu cau doi mat khau tai khoan. Cap nhat thong tin ma kich hoat va thoi han kich hoat vao bang acs_otp that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                                //param.Messages.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsUser_GuiSmsMaKichHoatTaiKhoanDenSDTThatBai), user.MOBILE));
                            }
                        }
                        else
                        {
                            //ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                            Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu truyen vao khong hop le. Gui yeu cau nhan tin ma otp doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                        }
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

        string GetPrefixMobileNumber(string mobile)
        {
            string prefixMobile = mobile;
            if (!String.IsNullOrEmpty(prefixMobile))
            {
                if (prefixMobile.StartsWith("0"))
                {
                    prefixMobile = String.Format("{0}{1}", "84", mobile.Substring(1));
                }
            }

            return prefixMobile;
        }

        bool Valid()
        {
            bool valid = entity != null && !String.IsNullOrEmpty(entity.Mobile) && !String.IsNullOrEmpty(entity.LoginName);
            if (!valid)
            {
                //ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu truyen vao khong hop le. Gui yeu cau nhan tin ma otp doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
            }
            else if (!String.IsNullOrEmpty(entity.ApplicationCode))
            {
                this.application = GetCurrentApplication(entity.ApplicationCode);
                if (application == null || application.ID == 0)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsOtp_OtpReqiure__MaUngDungKhongHopLe);
                    valid = false;
                    throw new Exception("Khong tim thay ung dung. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.ApplicationCode), entity.ApplicationCode));
                }

                AcsAppOtpTypeFilterQuery appOtpTypeFilterQuery = new AcsAppOtpTypeFilterQuery();
                appOtpTypeFilterQuery.APPLICSTION_ID = this.application.ID;
                appOtpTypeFilterQuery.OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__CHANGE_PASS;
                appOtpTypeFilterQuery.ORDER_DIRECTION = "DESC";
                appOtpTypeFilterQuery.ORDER_FIELD = "MODIFY_TIME";
                appOtpTypeFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                var appOtpTypes = new AcsAppOtpTypeBO().Get<List<ACS_APP_OTP_TYPE>>(appOtpTypeFilterQuery);
                if (appOtpTypes == null || appOtpTypes.Count == 0)
                {
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanKhac);
                    valid = false;
                    throw new Exception("Ung dung chua duoc khai bao mau tin nhan khac. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.ApplicationCode), entity.ApplicationCode));
                }
                else
                    currentAppOtpType = appOtpTypes.First();
            }

            AcsOtpTypeFilterQuery otpTypeFilterQuery = new AcsOtpTypeFilterQuery();
            otpTypeFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
            otpTypeFilterQuery.ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__CHANGE_PASS;
            var otpTypes = new AcsOtpTypeBO().Get<List<ACS_OTP_TYPE>>(otpTypeFilterQuery);
            if (otpTypes == null || otpTypes.Count == 0)
            {
                valid = false;
                throw new Exception("Loai tin nhan khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => otpTypes), otpTypes));
            }
            currentOtpType = otpTypes.First();

            return valid;
        }

        private ACS_APPLICATION GetCurrentApplication(string applicationCode)
        {
            try
            {
                return new AcsApplicationBO().Get<ACS_APPLICATION>(applicationCode);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return null;
        }

        bool SendActivateCode(string mobile, ACS_OTP acsOtp)
        {
            bool success = false;
            //Gọi đến tổng đài SMS, thực hiện nhắn tin mã kích hoạt theo SĐT của người dùng
            try
            {
                if (!String.IsNullOrEmpty(mobile))
                {
                    SmsMtSendSDO smsMtSDO = new SmsMtSendSDO();
                    smsMtSDO.Mobile = mobile;
                    smsMtSDO.MerchantCode = WebConfig.INTEGRATED_SMS__MERCHANT_CODE;
                    smsMtSDO.SecurityCode = WebConfig.INTEGRATED_SMS__SECURITY_CODE;
                    smsMtSDO.ContentParams = new List<SmsContentParamSDO>();

                    if (currentAppOtpType != null)
                    {
                        smsMtSDO.TemplateCode = currentAppOtpType.SMS_TEMPLATE_CODE;
                    }
                    else if (!String.IsNullOrEmpty(entity.Message_Format))
                    {
                        smsMtSDO.TemplateCode = entity.Message_Format;
                    }
                    else
                        smsMtSDO.TemplateCode = "Otp doi mat khau: {OTP_CODE}. Hieu luc den: {EXPIRE_TIME}.";

                    smsMtSDO.ContentParams.Add(new SmsContentParamSDO() { Key = "OTP_CODE", Value = acsOtp.OTP_CODE });
                    smsMtSDO.ContentParams.Add(new SmsContentParamSDO() { Key = "EXPIRE_TIME", Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(acsOtp.EXPIRE_TIME ?? 0) });
                    smsMtSDO.ContentParams.Add(new SmsContentParamSDO() { Key = "APPLICATION_NAME", Value = this.application != null ? this.application.APPLICATION_NAME : "" });

                    CommonParam commonParamCreate = new Inventec.Core.CommonParam();
                    var result = ApiConsumerStore.SmsConsumer.Post<ApiResultObject<SmsMtSendResponseSDO>>("/api/SmsMt/Send", commonParamCreate, smsMtSDO);
                    success = (result != null && result.Data != null && result.Data.ResponseCode == SmsResponseCode.SUCCESS);
                    if (!success)
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiSmsXacNhanThongTinTaiKhoanThatBai);
                        param.Messages.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsOtp_GuiSmsMaDoiMatKhauDenSDTThatBai), mobile));
                        Inventec.Common.Logging.LogSystem.Warn("Tao ma doi mat khau va cap nhat vao bang acs_user thanh cong, tuy nhien khong gui duoc tin nhan sms thong tin ma doi mat khau den nguoi dung . " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParamCreate), commonParamCreate));
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                Inventec.Common.Logging.LogSystem.Warn(String.Format("Gui Sms den so dien thoai {0} that bai", mobile), ex);
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiSmsXacNhanThongTinTaiKhoanThatBai);
            }
            return success;
        }

        double GetActivationMinitePlus()
        {
            double minutePlus = 60;
            try
            {
                if (currentOtpType != null && currentOtpType.EXPIRE_TIME_NUMBER.HasValue)
                {
                    minutePlus = (double)(currentOtpType.EXPIRE_TIME_NUMBER.Value / 60000);
                }
                else
                    minutePlus = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["ACS.AcsSystem.ResetPassword.Timeout"] ?? "60");
            }
            catch (Exception ex)
            {
                minutePlus = 60;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return minutePlus;
        }

        string GenerateActivateCode()
        {
            try
            {
                return ACS.UTILITY.Password.RandomString(4);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return "az19";
        }
    }
}
