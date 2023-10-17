using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisSereServ;
using MOS.SDO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisSereServController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSereServFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV>> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSereServViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV>> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServBhytOutpatientExamFilter>), "param")]
        [ActionName("GetSereServBhytOutpatientExam")]
        public ApiResult GetSereServBhytOutpatientExam(ApiParam<HisSereServBhytOutpatientExamFilter> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV>> result = null;
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.GetSereServBhytOutpatientExam(param.ApiData);
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
        [ActionName("UpdateDiscount")]
        public ApiResult UpdateDiscount(ApiParam<HIS_SERE_SERV> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateDiscount(param.ApiData);
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
        [ActionName("UpdateDiscountList")]
        public ApiResult UpdateDiscountList(ApiParam<HisSereServDiscountSDO> param)
        {
            try
            {
                ApiResultObject<HisSereServDiscountSDO> result = new ApiResultObject<HisSereServDiscountSDO>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateDiscountList(param.ApiData);
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
        [ActionName("SwapService")]
        public ApiResult SwapService(ApiParam<SwapServiceSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.SwapService(param.ApiData);
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
        [ActionName("UpdateNoExecute")]
        public ApiResult UpdateNoExecute(ApiParam<HisSereServNoExecuteSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV>> result = new ApiResultObject<List<HIS_SERE_SERV>>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateNoExecute(param.ApiData);
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
        [ActionName("AcceptNoExecute")]
        public ApiResult AcceptNoExecute(ApiParam<HisSereServAcceptNoExecuteSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.AcceptNoExecute(param.ApiData);
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
        [ActionName("UnacceptNoExecute")]
        public ApiResult UnacceptNoExecute(ApiParam<HisSereServAcceptNoExecuteSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UnacceptNoExecute(param.ApiData);
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
        [ActionName("AcceptNoExecuteByServiceReq")]
        public ApiResult AcceptNoExecuteByServiceReq(ApiParam<HisServiceReqAcceptNoExecuteSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.AcceptNoExecuteByServiceReq(param.ApiData);
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
        [ActionName("UnacceptNoExecuteByServiceReq")]
        public ApiResult UnacceptNoExecuteByServiceReq(ApiParam<HisServiceReqAcceptNoExecuteSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UnacceptNoExecuteByServiceReq(param.ApiData);
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
        [ActionName("UpdateResult")]
        public ApiResult UpdateResult(ApiParam<HIS_SERE_SERV> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateResult(param.ApiData);
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
        [ActionName("UpdatePayslipInfo")]
        public ApiResult UpdatePayslipInfo(ApiParam<HisSereServPayslipSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV>> result = new ApiResultObject<List<HIS_SERE_SERV>>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdatePayslipInfo(param.ApiData);
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
        [ActionName("UpdateEveryPayslipInfo")]
        public ApiResult UpdateEveryPayslipInfo(ApiParam<HisSereServPayslipSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV>> result = new ApiResultObject<List<HIS_SERE_SERV>>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateEveryPayslipInfo(param.ApiData);
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
        [ActionName("UpdatePriceAndHeinInfoForAll")]
        public ApiResult UpdatePriceAndHeinInfoForAll(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV>> result = new ApiResultObject<List<V_HIS_SERE_SERV>>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdatePriceAndHeinInfoForAll(param.ApiData);
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
        [ActionName("UpdateJsonPrintId")]
        public ApiResult UpdateJsonPrintId(ApiParam<HIS_SERE_SERV> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateJsonPrintId(param.ApiData);
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
        [ActionName("UpdateWithFile")]
        public async Task<ApiResult> UpdateWithFile()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
                }

                List<FileHolder> files = new List<FileHolder>();
                var streamProvider = new MultipartMemoryStreamProvider();
                return await Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<ApiResult>((task) =>
                {
                    string data = null;
                    MultipartMemoryStreamProvider provider = task.Result;
                    foreach (HttpContent content in provider.Contents)
                    {
                        if ("File".Equals(content.Headers.ContentDisposition.Name))
                        {
                            byte[] bytes = content.ReadAsByteArrayAsync().Result;
                            string fileName = content.Headers.ContentDisposition.FileName;
                            files.Add(new FileHolder(new MemoryStream(bytes), fileName));
                        }
                        else if ("Data".Equals(content.Headers.ContentDisposition.Name))
                        {
                            data = Uri.UnescapeDataString(content.ReadAsStringAsync().Result);
                        }
                    }
                    ApiParam<HIS_SERE_SERV> param = JsonConvert.DeserializeObject<ApiParam<HIS_SERE_SERV>>(data);
                    ApiResultObject<HisSereServWithFileSDO> r = new ApiResultObject<HisSereServWithFileSDO>(null);
                    if (param != null)
                    {
                        HisSereServManager mng = new HisSereServManager(param.CommonParam);
                        r = mng.Update(param.ApiData, files);
                    }
                    return new ApiResult(r, this.ActionContext);
                });
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ExamDelete")]
        public ApiResult ExamDelete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.ExamDelete(param.ApiData);
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
        [ActionName("UpdateBedAmount")]
        public ApiResult UpdateBedAmount(ApiParam<UpdateBedAmountSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateBedAmount(param.ApiData);
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
        [ActionName("UpdateBedTempAmount")]
        public ApiResult UpdateBedTempAmount(ApiParam<UpdateBedAmountSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateBedTempAmount(param.ApiData);
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
        [ActionName("ConfirmNoExcute")]
        public ApiResult ConfirmNoExcute(ApiParam<HisSereServConfirmNoExcuteSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.ConfirmNoExcute(param.ApiData);
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
        [ActionName("DeleteConfirmNoExcute")]
        public ApiResult DeleteConfirmNoExcute(ApiParam<HisSereServDeleteConfirmNoExcuteSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV> result = new ApiResultObject<HIS_SERE_SERV>(null);
                if (param != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.DeleteConfirmNoExcute(param.ApiData);
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
        [ActionName("UpdateMachine")]
        public ApiResult UpdateMachine(ApiParam<List<HisSereServUpdateMachineSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null && param.ApiData != null)
                {
                    HisSereServManager mng = new HisSereServManager(param.CommonParam);
                    result = mng.UpdateMachine(param.ApiData);
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
