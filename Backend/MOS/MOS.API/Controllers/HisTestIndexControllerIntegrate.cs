using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTestIndex;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace MOS.API.Controllers
{
    public partial class HisTestIndexController : BaseApiController
    {
        /// <summary>
        /// Lấy danh mục chỉ số xét nghiệm
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("List")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<List<HisTestIndexTDO>>))]
        public ApiResult List()
        {
            try
            {
                ApiResultObject<List<HisTestIndexTDO>> result = new ApiResultObject<List<HisTestIndexTDO>>(null);
                HisTestIndexManager mng = new HisTestIndexManager(new CommonParam());
                result = mng.GetTDO();
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
