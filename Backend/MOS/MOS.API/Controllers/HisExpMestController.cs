using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.Filter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisExpMestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExpMestFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisExpMestViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST>> result = new ApiResultObject<List<V_HIS_EXP_MEST>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestView2FilterQuery>), "param")]
        [ActionName("GetSummary")]
        public ApiResult GetSummary(ApiParam<HisExpMestView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<HisExpMestSummarySDO> result = new ApiResultObject<HisExpMestSummarySDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetSummary(param.ApiData);
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
        [ActionName("Export")]
        public ApiResult Export(ApiParam<HisExpMestExportSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Export(param.ApiData);
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
        [ActionName("Approve")]
        public ApiResult Approve(ApiParam<HisExpMestApproveSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Approve(param.ApiData);
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
        [ActionName("Decline")]
        public ApiResult Decline(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Decline(param.ApiData);
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
        [ActionName("Unapprove")]
        public ApiResult Unapprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Unapprove(param.ApiData);
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
        [ActionName("Unexport")]
        public ApiResult Unexport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Unexport(param.ApiData);
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
        [ActionName("Undecline")]
        public ApiResult Undecline(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Undecline(param.ApiData);
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
        public ApiResult Finish(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
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
        [ActionName("UpdateTestInfo")]
        public ApiResult UpdateTestInfo(ApiParam<HisExpMestTestInfoSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.UpdateTestInfo(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        //[HttpPost]
        //[ActionName("ExportBlood")]
        //public ApiResult ExportBlood(ApiParam<HisExportBloodSDO> param)
        //{
        //    try
        //    {
        //        ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
        //        if (param != null)
        //        {
        //            HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
        //            result = mng.ExportBlood(param.ApiData);
        //        }
        //        return new ApiResult(result, this.ActionContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //        return null;
        //    }
        //}

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HisExpMestSDO> param)
        {
            try
            {

                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
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
        [ActionName("UpdateNationalCode")]
        public ApiResult UpdateNationalCode(ApiParam<List<HIS_EXP_MEST>> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.UpdateNationalCode(param.ApiData);
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
        [ActionName("CancelNationalCode")]
        public ApiResult CancelNationalCode(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.CancelNationalCode(param.ApiData);
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
        [ActionName("ApproveNotTaken")]
        public ApiResult ApproveNotTaken(ApiParam<ApproveNotTakenPresSDO> param)
        {
            try
            {

                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.ApproveNotTaken(param.ApiData);
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
        [ActionName("BaseAdditionCreate")]
        public ApiResult BaseAdditionCreate(ApiParam<CabinetBaseAdditionSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseAdditionCreate(param.ApiData);
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
        [ActionName("BaseReductionCreate")]
        public ApiResult BaseReductionCreate(ApiParam<CabinetBaseReductionSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseReductionCreate(param.ApiData);
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
        [ActionName("BaseApprove")]
        public ApiResult BaseApprove(ApiParam<HisExpMestApproveSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseApprove(param.ApiData);
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
        [ActionName("BaseUnapprove")]
        public ApiResult BaseUnapprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseUnapprove(param.ApiData);
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
        [ActionName("BaseDelete")]
        public ApiResult BaseDelete(ApiParam<HisExpMestSDO> param)
        {
            try
            {

                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseDelete(param.ApiData);
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
        [ActionName("BaseAdditionUpdate")]
        public ApiResult BaseAdditionUpdate(ApiParam<CabinetBaseAdditionSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseAdditionUpdate(param.ApiData);
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
        [ActionName("BaseReductionUpdate")]
        public ApiResult BaseReductionUpdate(ApiParam<CabinetBaseReductionSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseReductionUpdate(param.ApiData);
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
        [ActionName("BaseCompensationCreate")]
        public ApiResult BaseCompensationCreate(ApiParam<CabinetBaseCompensationSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseCompensationCreate(param.ApiData);
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
        [ActionName("BaseCompensationDelete")]
        public ApiResult BaseCompensationDelete(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseCompensationDelete(param.ApiData);
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
        [ActionName("CompensationByBaseCreate")]
        public ApiResult CompensationByBaseCreate(ApiParam<CabinetBaseCompensationSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.CompensationByBaseCreate(param.ApiData);
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
        [ActionName("CompensationByBaseDelete")]
        public ApiResult CompensationByBaseDelete(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.CompensationByBaseDelete(param.ApiData);
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
        [ActionName("RecoverNotTaken")]
        public ApiResult RecoverNotTaken(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.RecoverNotTaken(param.ApiData);
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
        [ActionName("BaseExport")]
        public ApiResult BaseExport(ApiParam<HisExpMestExportSDO> param)
        {
            try
            {
                ApiResultObject<CabinetBaseResultSDO> result = new ApiResultObject<CabinetBaseResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseExport(param.ApiData);
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
        [ActionName("BaseUnexport")]
        public ApiResult BaseUnexport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.BaseUnexport(param.ApiData);
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
        [ActionName("UpdateSentErx")]
        public ApiResult UpdateSentErx(ApiParam<List<HIS_EXP_MEST>> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.UpdateSentErx(param.ApiData);
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
        [ActionName("UpdateReason")]
        public ApiResult UpdateReason(ApiParam<ExpMestUpdateReasonSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.UpdateReason(param.ApiData);
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
        [ActionName("PresBloodConfirm")]
        public ApiResult PresBloodConfirm(ApiParam<HisExpMestConfirmSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.PresBloodConfirm(param.ApiData);
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
        [ActionName("PresBloodUnconfirm")]
        public ApiResult PresBloodUnconfirm(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.PresBloodUnconfirm(param.ApiData);
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
        [ActionName("ConfirmAndGetDetail")]
        public ApiResult ConfirmAndGetDetail(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<ExpMestDetailResultSDO> result = new ApiResultObject<ExpMestDetailResultSDO>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.ConfirmAndGetDetail(param.ApiData);
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
        [ActionName("Absent")]
        public ApiResult Absent(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Absent(param.ApiData);
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
        [ActionName("Call")]
        public ApiResult Call(ApiParam<ExpMestCallSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.Call(param.ApiData);
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
