using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.MANAGER.HisDrugIntervention;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDrugInterventionController : BaseApiController
    {
        [HttpPost]
        [ActionName("CreateInfo")]
        [AllowAnonymous]
        public ApiResult CreateInfo(DrugInterventionInfoTDO param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                CommonParam commonParam = new CommonParam();
                if (param != null)
                {
                    HisDrugInterventionManager mng = new HisDrugInterventionManager(commonParam);
                    result = mng.CreateInfo(param);
                }

                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}