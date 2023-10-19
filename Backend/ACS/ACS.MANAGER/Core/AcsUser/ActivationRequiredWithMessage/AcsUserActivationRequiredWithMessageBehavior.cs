using ACS.ApiConsumerManager;
using ACS.EFMODEL.DataModels;
using ACS.LibraryConfig;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsApplication;
using ACS.MANAGER.Core.AcsAppOtpType;
using ACS.MANAGER.Core.AcsAppOtpType.Get;
using ACS.MANAGER.Core.AcsOtpType;
using ACS.MANAGER.Core.AcsOtpType.Get;
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

namespace ACS.MANAGER.Core.AcsUser.ActivationRequiredWithMessage
{
    class AcsUserActivationRequiredWithMessageBehaviorEv : BeanObjectBase, IAcsUserActivationRequiredWithMessage
    {
        AcsUserActivationRequiredWithMessageSDO entity;
        ACS_APPLICATION application;
        ACS_APP_OTP_TYPE currentAppOtpType;
        ACS_OTP_TYPE currentOtpType;

        internal AcsUserActivationRequiredWithMessageBehaviorEv(CommonParam param, AcsUserActivationRequiredWithMessageSDO data)
            : base(param)
        {
            entity = data;
        }

        /// <summary>
        /// + Kiểm tra xem SĐT và tên đăng nhập có hợp lệ ko. Nếu hợp lệ thì xử lý tiếp các bước sau:
        ///+ Sinh ra mã kích hoạt (OTP_CODE), insert dữ liệu OTP_CODE, EXPIRED_TIME (lấy thời gian hiện tại + số phút, số phút này cho phép người dùng cấu hình) vào bảng ACS_OTP
        ///+ Gọi đến tổng đài SMS, thực hiện nhắn tin mã kích hoạt theo SĐT của người dùng
        ///- Ouput: true/false
        /// </summary>
        /// <returns></returns>
        bool IAcsUserActivationRequiredWithMessage.Run()
        {
            bool result = false;
            try
            {
                ACS_USER user = Valid() ? new AcsUserBO().Get<ACS_USER>(entity.LOGINNAME.ToLower()) : null;
                if (user != null)
                {
                    if (user != null)
                    {
                        if (GetPrefixMobileNumber(user.MOBILE).Equals(GetPrefixMobileNumber(entity.MOBILE)))
                        {
                            double minuteAdd = GetActivationMinutePlus();
                            ACS_OTP otpCreate = new ACS_OTP()
                            {
                                LOGINAME = user.LOGINNAME,
                                USERNAME = user.USERNAME,                             
                                OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__ACTIVATE,
                                OTP_CODE = GenerateActivateCode(),
                                EXPIRE_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now.AddMinutes(minuteAdd)),
                                EMAIL = user.EMAIL,
                                MOBILE = GetPrefixMobileNumber(user.MOBILE)
                            };

                            if (DAOWorker.AcsOtpDAO.Create(otpCreate))
                            {
                                result = true;
                            }
                            else
                            {
                                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__CapNhatThatBai);
                            }

                            if (result && !this.SendActivateCode(user, otpCreate))
                            {
                                Inventec.Common.Logging.LogSystem.Warn("Yeu cau kich hoat tai khoan. Cap nhat thong tin ma kich hoat va thoi han kich hoat vao bang acs_user that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                                //
                                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsUser_YeuCaukichHoatTaiKhoanThatBai);
                            }
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Warn("Yeu cau kich hoat tai khoan. Thong tin so dien thoai (mobile) khong hop le. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => user), user) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                            ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__DuLieuTruyenLenKhongHopLe);
                        }
                    }
                    else
                    {
                        //ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__ThieuThongTinBatBuoc);
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Common__ThieuThongTinBatBuoc);
                        Inventec.Common.Logging.LogSystem.Warn("Kiem tra du lieu user khong hop le. Doi mat khau that bai. Can kiem tra lai. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
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
            bool valid = true;
            try
            {
                Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity));
                valid = entity != null && !String.IsNullOrEmpty(entity.LOGINNAME) && !String.IsNullOrEmpty(entity.MOBILE);
                if (!String.IsNullOrEmpty(entity.APPLICATIONCODE))
                {
                    this.application = GetCurrentApplication(entity.APPLICATIONCODE);
                    if (application == null || application.ID == 0)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsOtp_OtpReqiure__MaUngDungKhongHopLe);
                        valid = false;
                        throw new Exception("Khong tim thay ung dung. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.APPLICATIONCODE), entity.APPLICATIONCODE));
                    }


                    AcsAppOtpTypeFilterQuery appOtpTypeFilterQuery = new AcsAppOtpTypeFilterQuery();
                    appOtpTypeFilterQuery.APPLICSTION_ID = this.application.ID;
                    appOtpTypeFilterQuery.OTP_TYPE_ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__ACTIVATE;
                    appOtpTypeFilterQuery.ORDER_DIRECTION = "DESC";
                    appOtpTypeFilterQuery.ORDER_FIELD = "MODIFY_TIME";
                    appOtpTypeFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                    var appOtpTypes = new AcsAppOtpTypeBO().Get<List<ACS_APP_OTP_TYPE>>(appOtpTypeFilterQuery);
                    if (appOtpTypes == null || appOtpTypes.Count == 0)
                    {
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsOtp_OtpReqiure__ungDungChuaKhaiBaoMauTinNhanKhac);
                        valid = false;
                        throw new Exception("Ung dung chua duoc khai bao mau tin nhan khac. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity.APPLICATIONCODE), entity.APPLICATIONCODE));
                    }
                    else
                        currentAppOtpType = appOtpTypes.First();
                }

                AcsOtpTypeFilterQuery otpTypeFilterQuery = new AcsOtpTypeFilterQuery();
                otpTypeFilterQuery.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                otpTypeFilterQuery.ID = IMSys.DbConfig.ACS_RS.ACS_OTP_TYPE.ID__ACTIVATE;
                var otpTypes = new AcsOtpTypeBO().Get<List<ACS_OTP_TYPE>>(otpTypeFilterQuery);
                if (otpTypes == null || otpTypes.Count == 0)
                {
                    valid = false;
                    throw new Exception("Loai tin nhan khong hop le. " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => otpTypes), otpTypes));
                }
                currentOtpType = otpTypes.First();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

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

        bool SendActivateCode(ACS_USER user, ACS_OTP acsOtp)
        {
            bool success = false;
            //Gọi đến tổng đài SMS, thực hiện nhắn tin mã kích hoạt theo SĐT của người dùng
            try
            {
                if (!String.IsNullOrEmpty(user.MOBILE))
                {
                    SmsMtSendSDO smsMtSDO = new SmsMtSendSDO();
                    smsMtSDO.Mobile = GetPrefixMobileNumber(user.MOBILE);
                    smsMtSDO.MerchantCode = WebConfig.INTEGRATED_SMS__MERCHANT_CODE;
                    smsMtSDO.SecurityCode = WebConfig.INTEGRATED_SMS__SECURITY_CODE;
                    smsMtSDO.ContentParams = new List<SmsContentParamSDO>();

                    if (currentAppOtpType != null)
                    {
                        smsMtSDO.TemplateCode = currentAppOtpType.SMS_TEMPLATE_CODE;
                    }
                    else if (!String.IsNullOrEmpty(entity.MESSAGE_FORMAT))
                    {
                        smsMtSDO.TemplateCode = entity.MESSAGE_FORMAT;
                    }
                    else
                        smsMtSDO.TemplateCode = "Ma kich hoat: {OTP_CODE}. Hieu luc den: {EXPIRE_TIME}.";

                    smsMtSDO.ContentParams.Add(new SmsContentParamSDO() { Key = "OTP_CODE", Value = acsOtp.OTP_CODE });
                    smsMtSDO.ContentParams.Add(new SmsContentParamSDO() { Key = "EXPIRE_TIME", Value = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(acsOtp.EXPIRE_TIME ?? 0) });
                    smsMtSDO.ContentParams.Add(new SmsContentParamSDO() { Key = "APPLICATION_NAME", Value = this.application != null ? this.application.APPLICATION_NAME : "" });

                    CommonParam commonParamCreate = new Inventec.Core.CommonParam();

                    var result = ApiConsumerStore.SmsConsumer.Post<ApiResultObject<SmsMtSendResponseSDO>>("/api/SmsMt/Send", commonParamCreate, smsMtSDO);
                    success = (result != null && result.Data != null && result.Data.ResponseCode == SmsResponseCode.SUCCESS);
                    if (!success)
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiSmsXacNhanThongTinTaiKhoanThatBai);
                        param.Messages.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.Core_AcsUser_GuiSmsMaKichHoatTaiKhoanDenSDTThatBai), user.MOBILE));
                        Inventec.Common.Logging.LogSystem.Warn("Tao ma kich hoat va cap nhat vao bang acs_user thanh cong, tuy nhien khong gui duoc tin nhan sms thong tin ma kich hoat den nguoi dung . " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => entity), entity) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result) + "____" + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => commonParamCreate), commonParamCreate));
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                Inventec.Common.Logging.LogSystem.Warn(String.Format("Gui Sms den so dien thoai {0} that bai", user.MOBILE), ex);
                ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsUser_GuiSmsXacNhanThongTinTaiKhoanThatBai);
            }
            return success;
        }

        double GetActivationMinutePlus()
        {
            double minutePlus = 2;
            try
            {
                if (currentOtpType != null && currentOtpType.EXPIRE_TIME_NUMBER.HasValue)
                {
                    minutePlus = (double)(currentOtpType.EXPIRE_TIME_NUMBER.Value / 60000);
                }
                else
                    minutePlus = double.Parse(ConfigurationManager.AppSettings["ACS.Activation.ExpireMinutePlus"]);
            }
            catch (Exception ex)
            {
                minutePlus = 5;
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
            return "abc123";
        }
    }
}
