using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.AcsUser;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class AcsUserController : BaseApiController
    {
        /// <summary>
        /// Lấy danh mục người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("List")]
        [AllowAnonymous]
        public ApiResult List()
        {
            try
            {
                ApiResultObject<List<AcsUserTDO>> result = new ApiResultObject<List<AcsUserTDO>>(null);
                AcsUserManager mng = new AcsUserManager(new CommonParam());
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
