using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Manager;
using Inventec.Backend.MANAGER;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACS.API.Controllers
{
    public class AcsSystemController : Controller
    {
        int timeout = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["ACS.AcsSystem.ResetPassword.Timeout"] ?? "86400");

        public ActionResult ResetPassword(string id)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    //Đọc chuỗi mã hóa base64 trong url của link xác nhận thông tin đổi mật khẩu
                    //Chuỗi này có dạng Base64Encode("LOGINNAME:EMAIL:DATETIME")
                    //Giải mã chuỗi về dạng chuỗi ghép các thông tin (loginname:email:datetime) trước khi mã hóa base64
                    string valueRaw = ACS.UTILITY.Util.Base64Decode(id);
                    if (String.IsNullOrEmpty(valueRaw)) throw new ArgumentNullException("valueRaw");

                    var arrRaw = valueRaw.Split(':');
                    if (arrRaw == null || arrRaw.Count() == 0)
                        throw new ArgumentNullException("arrRaw");

                    //Kiểm tra thời gian tạo lệnh reset mật khẩu (chứa trong link xác nhận) so sánh với thời gian hiện tại trên server mà lớn hơn thời gian đã cấu hình thì nhảy đến trang thông báo đã quá hạn xác nhận reset mật khẩu, yêu cầu làm lại.
                    if (arrRaw.Count() >= 2)
                    {
                        long timeCheck = Inventec.Common.TypeConvert.Parse.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss")) - Inventec.Common.TypeConvert.Parse.ToInt64(arrRaw[2]);
                        if (timeCheck > timeout)
                        {
                            Inventec.Common.Logging.LogSystem.Info("Phien reset mat khau da qua han. Du lieu dau vao: " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => arrRaw), arrRaw));
                            return View("Timeout");
                        }
                    }

                    //Thực hiện khởi tạo đối tượng đầu vào trước khi gọi hàm reset mật khẩu
                    ACS_USER user = new ACS_USER();
                    user.LOGINNAME = arrRaw[0];
                    user.EMAIL = arrRaw[1];

                    this.ViewBag.LOGINNAME = arrRaw[0];
                    this.ViewBag.EMAIL = arrRaw[1];
                    CommonParam param = new CommonParam();
                    AcsUserManager acsUserManager = new AcsUserManager(param);
                    bool success = Convert.ToBoolean(acsUserManager.ResetPassword(user));
                    //Gọi hàm reset mật khẩu, nếu trả về thành công -> nhảy đến trang thông báo đã reset mật khẩu thành công
                    if (success)
                    {
                        return View("ResetPassword");
                    }
                    //Ngược lại nếu reset mật khẩu thất bại -> nhảy đến trang thông báo reset thất bại
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    //Nếu thông tin chuỗi mã hóa trên link xác nhận reset mật khẩu không hợp lệ -> nhảy đến trang thông báo lỗi
                    Inventec.Common.Logging.LogSystem.Warn("Link xac nhan reset mat khau khong hop le. id = " + id);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return View("Error");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Timeout()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return ResetPassword();
        }
    }
}
