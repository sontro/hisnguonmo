using SAR.API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SAR.SDO;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;

namespace SAR.API.Controllers
{
    public partial class SarFormController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarForm.Get.SarFormViewFilterQuery>), "param")]
        [ActionName("GetView")]
        [AllowAnonymous]
        public ApiResult GetView(ApiParam<SAR.MANAGER.Core.SarForm.Get.SarFormViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SAR_FORM>> result = new ApiResultObject<List<V_SAR_FORM>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarFormManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SAR_FORM> resultData = managerContainer.Run<List<V_SAR_FORM>>();
                    result = PackResult<List<V_SAR_FORM>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}