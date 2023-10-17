using Inventec.Core;
using MOS.API.Base;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class DanhSachDangKyKhamController : BaseApiController
    {
        [HttpGet]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get([FromUri]string fromdate = null, [FromUri]string todate = null, [FromUri]string trangthai = null, [FromUri]string tukhoa = null)
        {
            try
            {
                CommonParam CommonParam = new CommonParam();
                HisTreatmentManager mng = new HisTreatmentManager(CommonParam);
                var apiResult = mng.GetDanhSachDangKyKhamQlpk(fromdate, todate, trangthai, tukhoa);

                return new ApiResult(apiResult.Data, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}