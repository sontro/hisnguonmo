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
    public class DuLieuDonThuocController : BaseApiController
    {
        [HttpGet]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get([FromUri]string id = null, [FromUri]string date = null)
        {
            try
            {
                CommonParam CommonParam = new CommonParam();
                HisTreatmentManager mng = new HisTreatmentManager(CommonParam);
                var apiResult = mng.GetDuLieuDonThuocQlpk(id, date);

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