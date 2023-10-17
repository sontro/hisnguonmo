using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMest.Base.BaseAddition.Update;
using MOS.MANAGER.HisExpMest.Base.BaseCompensation.Create;
using MOS.MANAGER.HisExpMest.Base.BaseCompensation.Delete;
using MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase.Create;
using MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase.Delete;
using MOS.MANAGER.HisExpMest.Base.BaseHandler.Approve;
using MOS.MANAGER.HisExpMest.Base.BaseHandler.Export;
using MOS.MANAGER.HisExpMest.Base.BaseHandler.Unapprove;
using MOS.MANAGER.HisExpMest.Base.BaseHandler.Unexport;
using MOS.MANAGER.HisExpMest.Base.BaseReduction.Update;
using MOS.MANAGER.HisExpMest.BaseAddition.Create;
using MOS.MANAGER.HisExpMest.BaseDelete;
using MOS.MANAGER.HisExpMest.BaseReduction.Create;
using MOS.MANAGER.HisExpMest.Common.Approve;
using MOS.MANAGER.HisExpMest.Common.ApproveNotTaken;
using MOS.MANAGER.HisExpMest.Common.Decline;
using MOS.MANAGER.HisExpMest.Common.Delete;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisExpMest.Common.Finish;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.RecoverNotTaken;
using MOS.MANAGER.HisExpMest.Common.Unapprove;
using MOS.MANAGER.HisExpMest.Common.Undecline;
using MOS.MANAGER.HisExpMest.Common.Unexport;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMest.Common.Call;
using MOS.MANAGER.HisExpMest.Common.Absent;
using MOS.MANAGER.HisExpMest.Common.ConfirmAndGetDetail;
using MOS.MANAGER.HisExpMest.Confirm.PresBloodConfirm;
using MOS.MANAGER.HisExpMest.Confirm.PresBloodUnconfirm;
using MOS.MANAGER.HisExpMest.TestInfo;
using MOS.Filter;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMest
{
    public partial class HisExpMestManager : BusinessBase
    {
        public HisExpMestManager()
            : base()
        {

        }

        public HisExpMestManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> Get(HisExpMestFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_EXP_MEST>> GetView(HisExpMestViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_EXP_MEST>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_EXP_MEST> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestSummarySDO> GetSummary(HisExpMestView2FilterQuery filter)
        {
            ApiResultObject<HisExpMestSummarySDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisExpMestSummarySDO resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestGet(param).GetSummary(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> Export(HisExpMestExportSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestExport(param).Export(data, false, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestResultSDO> Approve(HisExpMestApproveSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestApprove(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> Decline(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestDecline(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> Unapprove(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestUnapprove(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> Unexport(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestUnexport(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> Undecline(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestUndecline(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> Finish(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestFinish(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HisExpMestSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestTruncate(param).Truncate(data, false);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestResultSDO> UpdateTestInfo(HisExpMestTestInfoSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestTestInfoUpdate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> UpdateNationalCode(List<HIS_EXP_MEST> data)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_EXP_MEST> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestUpdateNationalCode(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> CancelNationalCode(List<long> listData)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_EXP_MEST> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestCancelNationalCode(param).Run(listData, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> ApproveNotTaken(ApproveNotTakenPresSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestApproveNotTaken(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestResultSDO> BaseAdditionCreate(CabinetBaseAdditionSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseAdditionCreate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestResultSDO> BaseReductionCreate(CabinetBaseReductionSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseReductionCreate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestResultSDO> BaseApprove(HisExpMestApproveSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseApprove(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> BaseUnapprove(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseUnapprove(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> BaseDelete(HisExpMestSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestBaseDelete(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisExpMestResultSDO> BaseAdditionUpdate(CabinetBaseAdditionSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseAdditionUpdate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }


        [Logger]
        public ApiResultObject<HisExpMestResultSDO> BaseReductionUpdate(CabinetBaseReductionSDO data)
        {
            ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisExpMestResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseReductionUpdate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> BaseCompensationCreate(CabinetBaseCompensationSDO data)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_EXP_MEST> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseCompensationCreate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> BaseCompensationDelete(HisExpMestSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestBaseCompensationDelete(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> CompensationByBaseCreate(CabinetBaseCompensationSDO data)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_EXP_MEST> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestCompensationByBaseCreate(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> CompensationByBaseDelete(HisExpMestSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestCompensationByBaseDelete(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> RecoverNotTaken(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestRecoverNotTaken(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<CabinetBaseResultSDO> BaseExport(HisExpMestExportSDO data)
        {
            ApiResultObject<CabinetBaseResultSDO> result = new ApiResultObject<CabinetBaseResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                CabinetBaseResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseExport(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> BaseUnexport(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestBaseUnexport(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST>> UpdateSentErx(List<HIS_EXP_MEST> data)
        {
            ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HIS_EXP_MEST> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestUpdateSentErx(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> UpdateReason(ExpMestUpdateReasonSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestUpdateReason(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> PresBloodConfirm(HisExpMestConfirmSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestPresBloodConfirm(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_EXP_MEST> PresBloodUnconfirm(HisExpMestSDO data)
        {
            ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXP_MEST resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpMestPresBloodUnconfirm(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<ExpMestDetailResultSDO> ConfirmAndGetDetail(long id)
        {
            ApiResultObject<ExpMestDetailResultSDO> result = new ApiResultObject<ExpMestDetailResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                ExpMestDetailResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new ExpMestConfirmAndGetDetail(param).Run(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Absent(HisExpMestSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestAbsent(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Call(ExpMestCallSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisExpMestCall(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
