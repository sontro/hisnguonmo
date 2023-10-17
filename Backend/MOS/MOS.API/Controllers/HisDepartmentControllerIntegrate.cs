using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDepartment;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace MOS.API.Controllers
{
    public partial class HisDepartmentController : BaseApiController
    {
        /// <summary>
        /// Lấy danh mục khoa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("List")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<List<HisDeparmentTDO>>))]
        public ApiResult List()
        {
            try
            {
                ApiResultObject<List<HisDeparmentTDO>> result = new ApiResultObject<List<HisDeparmentTDO>>(null);
                HisDepartmentManager mng = new HisDepartmentManager(new CommonParam());
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
