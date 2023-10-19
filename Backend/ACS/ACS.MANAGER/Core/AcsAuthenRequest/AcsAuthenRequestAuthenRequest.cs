using Inventec.Common.Logging;
using Inventec.Core;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using ACS.SDO;
using System.Web.Http.Controllers;
using ACS.MANAGER.AcsAuthorSystem;

namespace ACS.MANAGER.AcsAuthenRequest
{
    partial class AcsAuthenRequestAuthenRequest : BusinessBase
    {
        const string KeyHeader__AuthorSystemCode = "AuthorSystemCode";
        const string KeyHeader__SercureKey = "SercureKey";

        string authorSystemCode;
        string sercureKey;
        ACS_AUTHOR_SYSTEM currentAuthorSystem;
        const short AUTHOR_SYSTEM_TYPE_ID__HIS = 1;
        AuthenRequestTDO entity;
        internal AcsAuthenRequestAuthenRequest()
            : base()
        {

        }

        internal AcsAuthenRequestAuthenRequest(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        /// <summary>
        /// a. Bổ sung api yêu cầu xác thực:
        ///- Api này sẽ do hệ thống ủy quyền gọi.
        ///- Không cần xác thực api mà sẽ bổ sung biến Khóa bảo mật vào Header gửi lên.
        ///- Input header:
        ///+ Mã hệ thống ủy quyền.
        ///+ Key bảo mật của hệ thống ủy quyền.
        ///- Input body:
        ///+ Tên người yêu cầu.
        ///+ Email, sđt người yêu cầu.
        ///+ Mã xác thực.
        ///+ Thông tin bổ sung.

        ///- Xử lý:
        ///+ Nếu không có mã hoặc mã hệ thống ủy quyền không đúng -> Chặn báo lỗi.
        ///+ Nếu không có khóa bảo mật hoặc khóa bảo mật không đúng -> Chặn báo lỗi.
        ///+ Nếu không có thông tin người yêu cầu, mã xác thực -> Chặn báo lỗi.
        ///+ Trường hợp hệ thống ủy quyền loại là HIS và không có thông tin bổ sung (Mã CCHN) -> Chặn báo lỗi.
        ///+ Nếu các thông tin trên đều hợp lệ thì tạo yêu cầu (ACS_AUTHEN_REQUEST). Tg yêu cầu là tg hiện tại, tg hết hạn là tg hiện tại + 5 phút.

        ///- Output: Trả về true hoặc false cho hệ thống ủy quyền.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool AuthenRequest(AuthenRequestTDO data, HttpActionContext httpActionContext)
        {
            bool result = false;
            try
            {
                bool valid = true;
                AcsAuthenRequestCheck checker = new AcsAuthenRequestCheck(param);
                this.entity = data;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && ValidRequestHeaderParam(httpActionContext);
                if (valid)
                {
                    LogSystem.Debug("authorSystemCode:" + authorSystemCode + "____" + "sercureKey:" + sercureKey);

                    ACS_AUTHEN_REQUEST authenRequestCreate = new ACS_AUTHEN_REQUEST();
                    authenRequestCreate.ADDITIONAL_INFO = this.entity.AdditionalInfo;
                    authenRequestCreate.AUTHENTICATION_CODE = this.entity.AuthenticationCode;
                    authenRequestCreate.AUTHOR_SYSTEM_ID = this.currentAuthorSystem.ID;
                    authenRequestCreate.TDL_AUTHOR_SYSTEM_CODE = this.currentAuthorSystem.AUTHOR_SYSTEM_CODE;
                    authenRequestCreate.EMAIL = this.entity.Email;
                    authenRequestCreate.MOBILE = this.entity.Mobile;
                    authenRequestCreate.REQUEST_USERNAME = this.entity.UserName;
                    authenRequestCreate.REQUEST_TIME = Inventec.Common.DateTime.Get.Now() ?? 0;

                    if (!DAOWorker.AcsAuthenRequestDAO.Create(authenRequestCreate))
                    {
                        ACS.MANAGER.Base.BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.AcsAuthenRequest_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin AcsAuthenRequest that bai." + LogUtil.TraceData("data", data));
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        bool ValidRequestHeaderParam(HttpActionContext httpActionContext)
        {
            bool valid = true;
            try
            {
                var headers = httpActionContext.ControllerContext.Request.Headers;
                if (headers.Contains(KeyHeader__AuthorSystemCode) && headers.Contains(KeyHeader__SercureKey))
                {
                    authorSystemCode = headers.GetValues(KeyHeader__AuthorSystemCode).First();
                    sercureKey = headers.GetValues(KeyHeader__SercureKey).First();
                    LogSystem.Debug("authorSystemCode:" + authorSystemCode + "____" + "sercureKey:" + sercureKey);

                    if (string.IsNullOrWhiteSpace(authorSystemCode))
                    {
                        valid = false;
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuMaHeThongUyQuyen);
                    }
                    if (string.IsNullOrWhiteSpace(sercureKey))
                    {
                        valid = false;
                        MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuKhoaBaoMat);
                    }
                    if (!string.IsNullOrWhiteSpace(authorSystemCode))
                    {
                        AcsAuthorSystemFilterQuery authorSystemFilterQuery = new AcsAuthorSystemFilterQuery();

                        this.currentAuthorSystem = DAOWorker.AcsAuthorSystemDAO.GetByCode(authorSystemCode, authorSystemFilterQuery.Query());
                        if (this.currentAuthorSystem == null)
                        {
                            valid = false;
                            MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__MaHeThongUyQuyenKhongHopLe);
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(sercureKey) && this.currentAuthorSystem.SERCURE_KEY != sercureKey)
                            {
                                valid = false;
                                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__KhoaBaoMatKhongHopLe);
                            }
                            if (this.currentAuthorSystem.AUTHOR_SYSTEM_TYPE_ID == AUTHOR_SYSTEM_TYPE_ID__HIS && String.IsNullOrEmpty(entity.AdditionalInfo))
                            {
                                valid = false;
                                MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.Core_AcsAuthenRequest_AuthenRequest__ThieuThongTinBoSung);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
