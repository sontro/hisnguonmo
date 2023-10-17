using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.TDO;
using System.Web.Http.Description;

namespace MOS.API.Controllers
{
    public partial class HisPatientTypeController : BaseApiController
    {
        /// <summary>
        /// Lấy danh sách đối tượng bệnh nhân
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("List")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<List<HisPatientTypeTDO>>))]
        public ApiResult List()
        {
            try
            {
                ApiResultObject<List<HisPatientTypeTDO>> result = new ApiResultObject<List<HisPatientTypeTDO>>(null);
                HisPatientTypeManager mng = new HisPatientTypeManager(new CommonParam());
                result = mng.GetTdo();
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
