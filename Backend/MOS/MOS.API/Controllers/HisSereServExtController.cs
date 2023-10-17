using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServExt;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
	public partial class HisSereServExtController : BaseApiController
	{
		[HttpGet]
		[ApiParamFilter(typeof(ApiParam<HisSereServExtFilterQuery>), "param")]
		[ActionName("Get")]
		public ApiResult Get(ApiParam<HisSereServExtFilterQuery> param)
		{
			try
			{
				ApiResultObject<List<HIS_SERE_SERV_EXT>> result = new ApiResultObject<List<HIS_SERE_SERV_EXT>>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.Get(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
		
		[HttpPost]
		[ActionName("Create")]
		public ApiResult Create(ApiParam<HIS_SERE_SERV_EXT> param)
		{
			try
			{
				ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.Create(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
		
		[HttpPost]
		[ActionName("SetInstructionNote")]
		public ApiResult SetInstructionNote(ApiParam<HIS_SERE_SERV_EXT> param)
		{
			try
			{
				ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.SetInstructionNote(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

		[HttpPost]
		[ActionName("UpdateJsonForm")]
		public ApiResult UpdateJsonForm(ApiParam<HIS_SERE_SERV_EXT> param)
		{
			try
			{
				ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.UpdateJsonForm(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

		[HttpPost]
		[ActionName("SetIsFee")]
		public ApiResult SetIsFee(ApiParam<HisSereServExtIsFeeSDO> param)
		{
			try
			{
				ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.SetIsFee(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

		[HttpPost]
		[ActionName("SetIsGatherData")]
		public ApiResult SetIsGatherData(ApiParam<HisSereServExtIsGatherDataSDO> param)
		{
			try
			{
				ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.SetIsGatherData(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

        [HttpPost]
        [ActionName("CreateSdo")]
        public ApiResult CreateSdo(ApiParam<HisSereServExtSDO> param)
        {
            try
            {
                ApiResultObject<HisSereServExtWithFileSDO> r = new ApiResultObject<HisSereServExtWithFileSDO>(null);
                if (param != null)
                {
                    HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
                    r = mng.Create(param.ApiData);
                }
                return new ApiResult(r, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

		[HttpPost]
		[ActionName("Update")]
		public ApiResult Update(ApiParam<HIS_SERE_SERV_EXT> param)
		{
			try
			{
				ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.Update(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}

        [HttpPost]
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisSereServExtSDO> param)
        {
            try
            {
                ApiResultObject<HisSereServExtWithFileSDO> r = new ApiResultObject<HisSereServExtWithFileSDO>(null);
                if (param != null)
                {
                    HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
                    r = mng.Update(param.ApiData);
                }
                return new ApiResult(r, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateForEmr")]
        public ApiResult UpdateForEmr(ApiParam<UpdateForEmrSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
                    result = mng.UpdateForEmr(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

		[HttpPost]
		[ActionName("Delete")]
        public ApiResult Delete(ApiParam<HisSereServDeleteConfirmNoExcuteSDO> param)
		{
			try
			{
                ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.Delete(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
		
		[HttpPost]
		[ActionName("ChangeLock")]
		public ApiResult ChangeLock(ApiParam<long> param)
		{
			try
			{
				ApiResultObject<HIS_SERE_SERV_EXT> result = new ApiResultObject<HIS_SERE_SERV_EXT>(null);
				if (param != null)
				{
					HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
					result = mng.ChangeLock(param.ApiData);
				}
				return new ApiResult(result, this.ActionContext);
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				return null;
			}
		}
		
		[HttpPost]
		[ActionName("Lock")]
		public ApiResult Lock(ApiParam<long> param)
		{
			ApiResultObject<HIS_SERE_SERV_EXT> result = null;
			if (param != null)
			{
				HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
				result = mng.Lock(param.ApiData);
			}
			return new ApiResult(result, this.ActionContext);
		}

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetLinkResult")]
        public ApiResult GetLinkResult(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<string> result = new ApiResultObject<string>(null);
                HisSereServExtManager mng = new HisSereServExtManager(param.CommonParam);
                result = mng.GetLinkResult(param.ApiData);
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
