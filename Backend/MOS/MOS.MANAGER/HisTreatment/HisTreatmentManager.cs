using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment.ApproveFinish;
using MOS.MANAGER.HisTreatment.ChangePatient;
using MOS.MANAGER.HisTreatment.Get;
using MOS.MANAGER.HisTreatment.Ksk.Approve;
using MOS.MANAGER.HisTreatment.Ksk.Unapprove;
using MOS.MANAGER.HisTreatment.Lock;
using MOS.MANAGER.HisTreatment.MediRecord;
using MOS.MANAGER.HisTreatment.MediRecord.OutOfMediRecord;
using MOS.MANAGER.HisTreatment.MediRecord.RecordInspection;
using MOS.MANAGER.HisTreatment.Update;
using MOS.MANAGER.HisTreatment.Update.Finish;
using MOS.MANAGER.HisTreatment.FinishCheck;
using MOS.MANAGER.HisTreatment.Update.Fund;
using MOS.MANAGER.HisTreatment.Update.Unfinish;
using MOS.MANAGER.HisTreatment.Update.UpdateIncode;
using MOS.MANAGER.HisTreatment.UpdatePatientInfo;
using MOS.MANAGER.HisTreatment.Update.TranPatiBookInfo;
using MOS.MANAGER.HisTreatment.Update.UpdateEpidemiologyInfo;
using MOS.MANAGER.HisTreatment.MediRecord.DocumentViewCount;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using MOS.MANAGER.HisTreatment.SetStoreBordereauCode;
using MOS.MANAGER.HisTreatment.Update.UpdateTuberculosisIssuedInfo;
using MOS.MANAGER.HisTreatment.Xml;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {
        public HisTreatmentManager()
            : base()
        {

        }

        public HisTreatmentManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> Get(HisTreatmentFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisTreatmentGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT>> GetView(HisTreatmentViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisTreatmentGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_FEE>> GetFeeView(HisTreatmentFeeViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_FEE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_FEE> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisTreatmentGet(param).GetFeeView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_FEE_2>> GetFeeView2(HisTreatmentFeeView2FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_FEE_2>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_FEE_2> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisTreatmentGet(param).GetFeeView2(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_FEE_3>> GetFeeView3(HisTreatmentFeeView3FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_FEE_3>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_FEE_3> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisTreatmentGet(param).GetFeeView3(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_FEE_4>> GetFeeView4(HisTreatmentFeeView4FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TREATMENT_FEE_4>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TREATMENT_FEE_4> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisTreatmentGet(param).GetFeeView4(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateEpidemiologyInfo(EpidemiologyInfoSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateEpidemiologyInfo(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> Finish(HisTreatmentFinishSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentFinish(param).Finish(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Unfinish(long treamentId)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool resultData = new HisTreatmentUnfinish(param).Unfinish(treamentId);
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateJsonPrintId(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdate(param).UpdateJsonPrintId(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateIncode(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateIncode(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateJsonForm(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateJsonForm(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> UpdateListDataStoreId(List<HIS_TREATMENT> data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdate(param).UpdateListDataStoreId(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateDataStoreId(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdate(param).UpdateDataStoreId(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateCommonInfo(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateCommonInfo(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisTreatmentCommonInfoUpdateSDO> UpdateCommonInfo(HisTreatmentCommonInfoUpdateSDO data)
        {
            ApiResultObject<HisTreatmentCommonInfoUpdateSDO> result = new ApiResultObject<HisTreatmentCommonInfoUpdateSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisTreatmentCommonInfoUpdateSDO resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateCommonInfo(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> ChangePatient(HisTreatmentUpdatePatiSDO data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisTreatment);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentChangePatient(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateAppointmentInfo(TreatmentAppointmentInfoSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateAppointmentInfo(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateTranPatiInfo(HisTreatmentTranPatiSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdate(param).UpdateTranPatiInfo(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateDeathInfo(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdate(param).UpdateDeathInfo(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateExtraEndInfo(HisTreatmentExtraEndInfoSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateExtraEndInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT> Lock(HisTreatmentLockSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentLock(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> Unlock(HisTreatmentLockSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUnlock(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> TemporaryLock(HisTreatmentTemporaryLockSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentTemporaryLock(param).Lock(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> CancelTemporaryLock(HisTreatmentTemporaryLockSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentTemporaryLock(param).Unlock(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_TREATMENT data)
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
                    resultData = new HisTreatmentTruncate(param).Truncate(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UnlockHein(long hisTreatmentId)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentLockHein(param).UnlockHein(hisTreatmentId, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> LockHein(HisTreatmentLockHeinSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentLockHein(param).LockHein(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> SetEndCode(HisTreatmentSetEndCodeSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentSetEndcode(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> HeinApproval(HisTreatmentHeinApprovalSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisTreatmentHeinApproval(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> DeleteTestData(HIS_TREATMENT data)
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
                    resultData = new HisTreatmentTruncate(param).TruncateTestData(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> UpdateFundPayTime(List<HIS_TREATMENT> listData)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdateFundPayTime(param).Run(listData, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> CancelFundPayTime(List<HIS_TREATMENT> listData)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentCancelFundPayTime(param).Run(listData, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateFundInfo(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUpdate(param).UpdateFundInfo(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> OutOfMediRecord(HIS_TREATMENT data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentOutOfMediRecord(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> OutOfMediRecordList(List<long> data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentOutOfMediRecord(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> RecordInspectionApprove(List<long> data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentRecordInspectionApprove(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> RecordInspectionUnapprove(long data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentRecordInspectionUnapprove(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> RecordInspectionUnreject(long data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentRecordInspectionUnreject(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> RecordInspectionReject(RecordInspectionRejectSdo data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentRecordInspectionReject(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> Store(HisTreatmentStoreSDO data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentStore(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateXmlResult(HisTreatmentXmlResultSDO data)
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
                    resultData = new HisTreatmentUpdateXmlResult(param).UpdateXmlResult(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateCollinearXmlResult(HisTreatmentXmlResultSDO data)
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
                    resultData = new HisTreatmentUpdateXmlResult(param).UpdateCollinearXmlResult(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> RejectStore(HisTreatmentRejectStoreSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentRejectStore(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UnrejectStore(HisTreatmentRejectStoreSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUnrejectStore(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> HandledRejectStore(HisTreatmentRejectStoreSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentHandledRejectStore(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> ApproveFinish(HisTreatmentApproveFinishSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentApproveFinish(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> UnapproveFinish(HisTreatmentApproveFinishSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new HisTreatmentUnapproveFinish(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> KskApprove(List<long> data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentKskApprove(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> KskUnapprove(List<long> data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentKskUnapprove(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateExportedXml2076(List<long> data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisTreatmentUpdateExportedXml2076(param).Run(data);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> ApprovalStore(List<long> data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentApprovalStore(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<HIS_TREATMENT>> UnapprovalStore(List<long> data)
        {
            ApiResultObject<List<HIS_TREATMENT>> result = new ApiResultObject<List<HIS_TREATMENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_TREATMENT> resultData = null;
                if (valid)
                {
                    new HisTreatmentUnapprovalStore(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<KioskInformationSDO>> GetKioskInformation(string input)
        {
            ApiResultObject<List<KioskInformationSDO>> result = new ApiResultObject<List<KioskInformationSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(input);
                List<KioskInformationSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentGetKioskInformation(param).Run(input);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateEmrCover(HisTreatmentEmrCoverSDO data)
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
                    resultData = new HisTreatmentUpdateEmrCover(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> SetTranPatiBookInfo(HisTreatmentSetTranPatiBookSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentSetTranPatiBookInfo(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> ClearTranPatiBookInfo(long data)
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
                    resultData = new HisTreatmentClearTranPatiBookInfo(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> DocumentViewCount(long id)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentDocumentViewCount(param).Run(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> TreatmentFinishCheck(HisTreatmentFinishSDO data)
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
                    resultData = new HisTreatmentFinishCheckProcessor(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> SetStoreBordereauCode(StoreBordereauCodeSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentSetStoreBordereauCode(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }


        [Logger]
        public ApiResultObject<HIS_TREATMENT> UpdateTuberculosisIssuedInfo(HisTreatmentTuberculosisIssuedInfoSDO data)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT resultData = null;
                if (valid)
                {
                    new UpdateTuberculosisIssuedInfo(param).Run(data, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_TREATMENT_1>> ExportXML4210(List<long> data)
        {
            ApiResultObject<List<V_HIS_TREATMENT_1>> result = new ApiResultObject<List<V_HIS_TREATMENT_1>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<V_HIS_TREATMENT_1> resultData = null;
                if (valid)
                {
                    new HisTreatmentExportXML4210Create(param).Run(data, ref resultData);
                }

                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TREATMENT> DeleteEndInfo(long id)
        {
            ApiResultObject<HIS_TREATMENT> result = new ApiResultObject<HIS_TREATMENT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentDeleteEndInfo(param).Run(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateXml130Info(HisTreatmentXmlResultSDO data)
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
                    resultData = new HisTreatmentUpdateXml130Info(param).UpdateXml130Info(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
