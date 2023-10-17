using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace MOS.API.Controllers
{
    //Ko thay doi ten class nay de ko phai thay doi URI cua cac API tich hop voi Labconn
    public partial class HisTestServiceReqController : BaseApiController
    {
        /// <summary>
        /// Lấy danh sách yêu cầu xét nghiệm
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="isSpecimen"></param>
        /// <param name="roomTypeCode"></param>
        /// <param name="kskContractCode"></param>
        /// <param name="executeDepartmentCode"></param>
        /// <param name="hasContract"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("List")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<List<HisTestServiceReqTDO>>))]
        public ApiResult List(long fromTime, long toTime, bool? isSpecimen, string roomTypeCode = null, string kskContractCode = null, string executeDepartmentCode = null, bool? hasContract = null)
        {
            try
            {
                ApiResultObject<List<HisTestServiceReqTDO>> result = new ApiResultObject<List<HisTestServiceReqTDO>>(null);
                HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                result = mng.GetTdo(fromTime, toTime, isSpecimen, roomTypeCode, kskContractCode, executeDepartmentCode, hasContract);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Gửi lại yêu cầu xét nghiệm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RequestOrder")]
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult RequestOrder(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.RequestOrder(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy yêu cầu xét nghiệm theo mã phiếu yêu cầu
        /// </summary>
        /// <param name="serviceReqCode"></param>
        /// <param name="isSpecimen"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetByCode")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<HisTestServiceReqTDO>))]
        public ApiResult GetByCode(string serviceReqCode, bool? isSpecimen)
        {
            try
            {
                ApiResultObject<HisTestServiceReqTDO> result = new ApiResultObject<HisTestServiceReqTDO>(null);
                HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                result = mng.GetTdo(serviceReqCode, isSpecimen);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách xét nghiệm theo mã lượt chỉ định
        /// </summary>
        /// <param name="turnCode"></param>
        /// <param name="isSpecimen"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetByTurnCode")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<List<HisTestServiceReqTDO>>))]
        public ApiResult GetByTurnCode(string turnCode, bool? isSpecimen)
        {
            try
            {
                ApiResultObject<List<HisTestServiceReqTDO>> result = new ApiResultObject<List<HisTestServiceReqTDO>>(null);
                HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                result = mng.GetTdoByTurnCode(turnCode, isSpecimen);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Cập nhật kết quả xét nghiệm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateResult")]
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult UpdateResult(ApiParam<HisTestResultTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
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

        /// <summary>
        /// Cập nhật kết quả xét nghiệm
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateResultAnonymous")]
        [AllowAnonymous] //tich hop voi he thong ko y/c xac thuc 
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult UpdateResultAnonymous(ApiParam<HisTestResultTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    if (param.ApiData != null && string.IsNullOrWhiteSpace(param.ApiData.ExecuteLoginname))
                    {
                        param.ApiData.ExecuteLoginname = "HMS"; //fix de tich hop tam thoi voi pm HMS cua ĐK TW Can Tho
                    }
                    if (param.ApiData != null && string.IsNullOrWhiteSpace(param.ApiData.ExecuteUsername))
                    {
                        param.ApiData.ExecuteUsername = "HMS"; //fix de tich hop tam thoi voi pm HMS cua ĐK TW Can Tho
                    }

                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
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

        /// <summary>
        /// Cập nhật dịch vụ khi LIS đã lấy dữ liệu
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateSpecimen")]
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult UpdateSpecimen(ApiParam<HisTestServiceReqTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.UpdateSpecimen(param.ApiData);
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
        [ActionName("CultureResult")]
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult CultureResult(ApiParam<CultureResultTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    LogSystem.Info("CultureResult" + LogUtil.TraceData("InputData", param));
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.CultureResult(param.ApiData);
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
        [ActionName("AntibioticMapResult")]
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult AntibioticMapResult(ApiParam<AntibioticMapResultTDO> param)
        {
            try
            {
                LogSystem.Info("AntibioticMapResult" + LogUtil.TraceData("InputData", param));
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.AntibioticMapResult(param.ApiData);
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
        [ActionName("UpdateBarcode")]
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult UpdateBarcode(ApiParam<HisTestUpdateBarcodeTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                    result = mng.UpdateBarcode(param.ApiData);
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
        [ActionName("GetDetailBySearchCode")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<HisTestDetailTDO>))]
        public ApiResult GetDetailBySearchCode(string searchCode)
        {
            try
            {
                ApiResultObject<HisTestDetailTDO> result = new ApiResultObject<HisTestDetailTDO>(null);
                HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                result = mng.GetDetailBySearchCode(searchCode);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy yêu cầu xét nghiệm theo mã điều trị
        /// </summary>
        /// <param name="treatmentCode"></param>
        /// <param name="isSpecimen"></param>
        /// <returns></returns>
        [HttpGet]
        [ActionName("GetByTreatmentCode")]
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<List<HisTestServiceReqTDO>>))]
        public ApiResult GetByTreatmentCode(string treatmentCode, bool? isSpecimen)
        {
            try
            {
                ApiResultObject<List<HisTestServiceReqTDO>> result = new ApiResultObject<List<HisTestServiceReqTDO>>(null);
                HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
                result = mng.GetTdoByTreatmentCode(treatmentCode, isSpecimen);
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
        [ResponseType(typeof(ApiResultObject<bool>))]
        public ApiResult ConfirmNoExcute(ApiParam<HisTestConfirmNoExcuteTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(new CommonParam());
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
    }
}
