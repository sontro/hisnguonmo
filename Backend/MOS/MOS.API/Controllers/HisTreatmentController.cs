using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTreatmentFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisTreatmentViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT>> result = new ApiResultObject<List<V_HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentFeeViewFilterQuery>), "param")]
        [ActionName("GetFeeView")]
        public ApiResult GetFeeView(ApiParam<HisTreatmentFeeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_FEE>> result = new ApiResultObject<List<V_HIS_TREATMENT_FEE>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetFeeView(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetKioskInformation")]
        public ApiResult GetKioskInformation(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<List<KioskInformationSDO>> result = new ApiResultObject<List<KioskInformationSDO>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetKioskInformation(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        //Chi can truyen vao gia tri id va patient_id
        [HttpPost]
        [ActionName("UpdatePatient")]
        public ApiResult UpdatePatient(ApiParam<HisTreatmentUpdatePatiSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.ChangePatient(param.ApiData);
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
        [ActionName("UpdateAppointmentInfo")]
        public ApiResult UpdateAppointmentInfo(ApiParam<TreatmentAppointmentInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateAppointmentInfo(param.ApiData);
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
        [ActionName("UpdateExtraEndInfo")]
        public ApiResult UpdateExtraEndInfo(ApiParam<HisTreatmentExtraEndInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateExtraEndInfo(param.ApiData);
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
        [ActionName("UpdateTranPatiInfo")]
        public ApiResult UpdateTranPatiInfo(ApiParam<HisTreatmentTranPatiSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateTranPatiInfo(param.ApiData);
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
        [ActionName("UpdateDeathInfo")]
        public ApiResult UpdateDeathInfo(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateDeathInfo(param.ApiData);
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
        public ApiResult UpdateJsonPrintId(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
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
        [ActionName("UpdateJsonForm")]
        public ApiResult UpdateJsonForm(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
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
        [ActionName("UpdateIncode")]
        public ApiResult UpdateIncode(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateIncode(param.ApiData);
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
        [ActionName("UpdateCommonInfo")]
        public ApiResult UpdateCommonInfo(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateCommonInfo(param.ApiData);
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
        [ActionName("UpdateCommonInfoSdo")]
        public ApiResult UpdateCommonInfoSdo(ApiParam<HisTreatmentCommonInfoUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HisTreatmentCommonInfoUpdateSDO> result = new ApiResultObject<HisTreatmentCommonInfoUpdateSDO>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateCommonInfo(param.ApiData);
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
        [ActionName("UpdateListDataStoreId")]
        public ApiResult UpdateListDataStoreId(ApiParam<List<HIS_TREATMENT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateListDataStoreId(param.ApiData);
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
        [ActionName("UpdateDataStoreId")]
        public ApiResult UpdateDataStoreId(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateDataStoreId(param.ApiData);
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
        [ActionName("UpdateEpidemiologyInfo")]
        public ApiResult UpdateEpidemiologyInfo(ApiParam<EpidemiologyInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateEpidemiologyInfo(param.ApiData);
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
        [ActionName("Finish")]
        public ApiResult Finish(ApiParam<HisTreatmentFinishSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.Finish(param.ApiData);
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
        [ActionName("SetEndCode")]
        public ApiResult SetEndCode(ApiParam<HisTreatmentSetEndCodeSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.SetEndCode(param.ApiData);
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
        [ActionName("Unfinish")]
        public ApiResult Unfinish(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.Unfinish(param.ApiData);
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
        [ActionName("UnlockHein")]
        public ApiResult UnlockHein(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UnlockHein(param.ApiData);
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
        [ActionName("LockHein")]
        public ApiResult LockHein(ApiParam<HisTreatmentLockHeinSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.LockHein(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
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
        [ActionName("HeinApproval")]
        public ApiResult HeinApproval(ApiParam<HisTreatmentHeinApprovalSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.HeinApproval(param.ApiData);
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
        public ApiResult Lock(ApiParam<HisTreatmentLockSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.Lock(param.ApiData);
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
        [ActionName("Unlock")]
        public ApiResult Unlock(ApiParam<HisTreatmentLockSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.Unlock(param.ApiData);
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
        [ActionName("TemporaryLock")]
        public ApiResult TemporaryLock(ApiParam<HisTreatmentTemporaryLockSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.TemporaryLock(param.ApiData);
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
        [ActionName("CancelTemporaryLock")]
        public ApiResult CancelTemporaryLock(ApiParam<HisTreatmentTemporaryLockSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.CancelTemporaryLock(param.ApiData);
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
        [ActionName("DeleteTestData")]
        public ApiResult DeleteTestData(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.DeleteTestData(param.ApiData);
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
        [ActionName("CheckDataStore")]
        public ApiResult CheckDataStore(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<HisTreatmentCheckDataStoreSDO> result = new ApiResultObject<HisTreatmentCheckDataStoreSDO>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.CheckDataStore(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentFeeView2FilterQuery>), "param")]
        [ActionName("GetFeeView2")]
        public ApiResult GetFeeView2(ApiParam<HisTreatmentFeeView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_FEE_2>> result = new ApiResultObject<List<V_HIS_TREATMENT_FEE_2>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetFeeView2(param.ApiData);
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
        [ActionName("UpdateFundPayTime")]
        public ApiResult UpdateFundPayTime(ApiParam<List<HIS_TREATMENT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateFundPayTime(param.ApiData);
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
        [ActionName("CancelFundPayTime")]
        public ApiResult CancelFundPayTime(ApiParam<List<HIS_TREATMENT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.CancelFundPayTime(param.ApiData);
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
        [ActionName("CheckExistsTreatmentOrder")]
        public ApiResult CheckExistsTreatmentOrder(ApiParam<HisTreatmentOrderSDO> param)
        {
            try
            {
                ApiResultObject<HisTreatmentOrderSDO> result = new ApiResultObject<HisTreatmentOrderSDO>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.CheckExistsTreatmentOrder(param.ApiData);
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
        [ActionName("UpdateFundInfo")]
        public ApiResult UpdateFundInfo(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateFundInfo(param.ApiData);
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
        [ActionName("UploadEmr")]
        public ApiResult UploadEmr(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UploadEmr(param.ApiData);
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
        [ActionName("OutOfMediRecord")]
        public ApiResult OutOfMediRecord(ApiParam<HIS_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.OutOfMediRecord(param.ApiData);
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
        [ActionName("Store")]
        public ApiResult Store(ApiParam<HisTreatmentStoreSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.Store(param.ApiData);
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
        [ActionName("UpdateXmlResult")]
        public ApiResult UpdateXmlResult(ApiParam<HisTreatmentXmlResultSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateXmlResult(param.ApiData);
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
        [ActionName("UpdateCollinearXmlResult")]
        public ApiResult UpdateCollinearXmlResult(ApiParam<HisTreatmentXmlResultSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateCollinearXmlResult(param.ApiData);
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
        [ActionName("RejectStore")]
        public ApiResult RejectStore(ApiParam<HisTreatmentRejectStoreSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.RejectStore(param.ApiData);
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
        [ActionName("UnrejectStore")]
        public ApiResult UnrejectStore(ApiParam<HisTreatmentRejectStoreSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UnrejectStore(param.ApiData);
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
        [ActionName("HandledRejectStore")]
        public ApiResult HandledRejectStore(ApiParam<HisTreatmentRejectStoreSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.HandledRejectStore(param.ApiData);
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
        [ActionName("ApproveFinish")]
        public ApiResult ApproveFinish(ApiParam<HisTreatmentApproveFinishSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.ApproveFinish(param.ApiData);
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
        [ActionName("UnapproveFinish")]
        public ApiResult UnapproveFinish(ApiParam<HisTreatmentApproveFinishSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UnapproveFinish(param.ApiData);
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
        [ActionName("KskApprove")]
        public ApiResult KskApprove(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.KskApprove(param.ApiData);
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
        [ActionName("KskUnapprove")]
        public ApiResult KskUnapprove(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.KskUnapprove(param.ApiData);
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
        [ActionName("UpdateExportedXml2076")]
        public ApiResult UpdateExportedXml2076(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateExportedXml2076(param.ApiData);
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
        [ActionName("ApprovalStore")]
        public ApiResult ApprovalStore(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.ApprovalStore(param.ApiData);
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
        [ActionName("UnapprovalStore")]
        public ApiResult UnapprovalStore(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UnapprovalStore(param.ApiData);
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
        [ActionName("UpdateEmrCover")]
        public ApiResult UpdateEmrCover(ApiParam<HisTreatmentEmrCoverSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateEmrCover(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentFeeView3FilterQuery>), "param")]
        [ActionName("GetFeeView3")]
        public ApiResult GetFeeView3(ApiParam<HisTreatmentFeeView3FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_FEE_3>> result = new ApiResultObject<List<V_HIS_TREATMENT_FEE_3>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetFeeView3(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentFeeView4FilterQuery>), "param")]
        [ActionName("GetFeeView4")]
        public ApiResult GetFeeView4(ApiParam<HisTreatmentFeeView4FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_FEE_4>> result = new ApiResultObject<List<V_HIS_TREATMENT_FEE_4>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetFeeView4(param.ApiData);
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
        [ActionName("OutOfMediRecordList")]
        public ApiResult OutOfMediRecordList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.OutOfMediRecordList(param.ApiData);
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
        [ActionName("RecordInspectionApprove")]
        public ApiResult RecordInspectionApprove(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.RecordInspectionApprove(param.ApiData);
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
        [ActionName("RecordInspectionUnapprove")]
        public ApiResult RecordInspectionUnapprove(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.RecordInspectionUnapprove(param.ApiData);
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
        [ActionName("RecordInspectionUnreject")]
        public ApiResult RecordInspectionUnreject(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.RecordInspectionUnreject(param.ApiData);
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
        [ActionName("RecordInspectionReject")]
        public ApiResult RecordInspectionReject(ApiParam<RecordInspectionRejectSdo> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.RecordInspectionReject(param.ApiData);
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
        [ActionName("SetTranPatiBookInfo")]
        public ApiResult SetTranPatiBookInfo(ApiParam<HisTreatmentSetTranPatiBookSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.SetTranPatiBookInfo(param.ApiData);
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
        [ActionName("ClearTranPatiBookInfo")]
        public ApiResult ClearTranPatiBookInfo(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.ClearTranPatiBookInfo(param.ApiData);
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
        [ActionName("DocumentViewCount")]
        public ApiResult DocumentViewCount(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.DocumentViewCount(param.ApiData);
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
        [ActionName("TreatmentFinishCheck")]
        public ApiResult TreatmentFinishCheck(ApiParam<HisTreatmentFinishSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.TreatmentFinishCheck(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<GetStoreBordereauCodeSDO>), "param")]
        [ActionName("GetNextStoreBordereauCode")]
        public ApiResult GetNextStoreBordereauCode(ApiParam<GetStoreBordereauCodeSDO> param)
        {
            try
            {
                ApiResultObject<string> result = new ApiResultObject<string>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetNextStoreBordereauCode(param.ApiData);
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
        [ActionName("SetStoreBordereauCode")]
        public ApiResult SetStoreBordereauCode(ApiParam<StoreBordereauCodeSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.SetStoreBordereauCode(param.ApiData);
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
        [ActionName("UpdateTuberculosisIssuedInfo")]
        public ApiResult UpdateTuberculosisIssuedInfo(ApiParam<HisTreatmentTuberculosisIssuedInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateTuberculosisIssuedInfo(param.ApiData);
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
        [ActionName("ExportXML4210")]
        public ApiResult ExportXML4210(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_1>> result = new ApiResultObject<List<V_HIS_TREATMENT_1>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.ExportXML4210(param.ApiData);
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
        [ActionName("SyncDeath")]
        public ApiResult SyncDeath(ApiParam<List<DeathSyncSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.SyncDeath(param.ApiData);
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
        [ActionName("DeleteEndInfo")]
        public ApiResult DeleteEndInfo(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.DeleteEndInfo(param.ApiData);
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
        [ActionName("UpdateXml130Info")]
        public ApiResult UpdateXml130Info(ApiParam<HisTreatmentXmlResultSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.UpdateXml130Info(param.ApiData);
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
