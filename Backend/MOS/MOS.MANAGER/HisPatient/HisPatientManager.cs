using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient.Recognition;
using MOS.MANAGER.HisPatient.Register;
using MOS.MANAGER.HisPatient.UpdateCard;
using MOS.MANAGER.HisPatient.UpdateInfo;
using MOS.MANAGER.HisPatient.UpdateClassify;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPatient
{
    public class HisPatientManager : BusinessBase
    {
        public HisPatientManager()
            : base()
        {

        }

        public HisPatientManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_PATIENT>> Get(HisPatientFilterQuery filter)
        {
            ApiResultObject<List<HIS_PATIENT>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PATIENT> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisPatientGet(param).Get(filter);
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
        public ApiResultObject<decimal?> GetCardBalance(long patientId)
        {
            ApiResultObject<decimal?> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                decimal? resultData = null;
                if (valid)
                {
                    resultData = new HisPatientBalance(param).GetAndUpdateCardBalance(patientId, null);
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
        public ApiResultObject<decimal?> GetCardBalance(CardBalanceFilter filter)
        {
            ApiResultObject<decimal?> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                decimal? resultData = null;
                if (valid)
                {
                    resultData = new HisPatientBalance(param).GetAndUpdateCardBalance(filter.PATIENT_ID, filter.LAST_DIGITS_OF_BANK_CARD_CODE);
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
        public ApiResultObject<List<HisPatientSDO>> GetSdoAdvance(HisPatientAdvanceFilter filter)
        {
            ApiResultObject<List<HisPatientSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HisPatientSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientGet(param).GetSdoAdvance(filter);
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
        public ApiResultObject<HisPatientWarningSDO> GetPreviousWarning(long patientId)
        {
            ApiResultObject<HisPatientWarningSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisPatientWarningSDO resultData = null;
                if (valid)
                {
                    resultData = new HisPatientGet(param).GetPreviousWarning(patientId);
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
        public ApiResultObject<List<HisPreviousPrescriptionSDO>> GetPreviousPrescription(long patientId)
        {
            ApiResultObject<List<HisPreviousPrescriptionSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisPreviousPrescriptionSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientGet(param).GetPreviousPrescription(patientId);
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
        public ApiResultObject<List<HisPreviousPrescriptionDetailSDO>> GetPreviousPrescriptionDetail(long patientId)
        {
            ApiResultObject<List<HisPreviousPrescriptionDetailSDO>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                List<HisPreviousPrescriptionDetailSDO> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientGet(param).GetPreviousPrescriptionDetail(patientId);
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
        public ApiResultObject<List<V_HIS_PATIENT>> GetView(HisPatientViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT> resultData = null;
                if (valid)
                {
                    HIS_BRANCH branch = new TokenManager().GetBranch();
                    if (HisBranchCFG.IS_ISOLATION && branch != null)
                    {
                        filter.BRANCH_ID = branch.ID;
                    }

                    resultData = new HisPatientGet(param).GetView(filter);
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
        public ApiResultObject<List<V_HIS_PATIENT_1>> GetView1(HisPatientView1FilterQuery filter)
        {
            ApiResultObject<List<V_HIS_PATIENT_1>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_PATIENT_1> resultData = null;
                if (valid)
                {
                    resultData = new HisPatientGet(param).GetView1(filter);
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
        public ApiResultObject<HIS_PATIENT> Create(HIS_PATIENT data)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT resultData = null;
                if (valid && new HisPatientCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<List<HIS_PATIENT>> CreateList(List<HIS_PATIENT> data)
        {
            ApiResultObject<List<HIS_PATIENT>> result = new ApiResultObject<List<HIS_PATIENT>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_PATIENT> resultData = null;
                if (valid && new HisPatientCreate(param).CreateList(data))
                {
                    resultData = data;
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
        public ApiResultObject<HisCardPatientSDO> GetSdoByCardServiceCode(string cardServiceCode)
        {
            ApiResultObject<HisCardPatientSDO> result = new ApiResultObject<HisCardPatientSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HisCardPatientSDO resultData = null;
                if (valid)
                {
                    resultData = new HisPatientGet(param).GetSdoByCardServiceCode(cardServiceCode);
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
        public ApiResultObject<HisPatientProfileSDO> RegisterProfile(HisPatientProfileSDO data)
        {
            ApiResultObject<HisPatientProfileSDO> result = new ApiResultObject<HisPatientProfileSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPatientProfileSDO resultData = null;
                HIS_CARD card = null;
                if (valid && new HisPatientRegister(param).RegisterProfile(data, card, false))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_PATIENT> Update(HIS_PATIENT data)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT resultData = null;
                if (valid && new HisPatientUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_PATIENT> UpdateSdo(HisPatientUpdateSDO data)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.HisPatient);
                HIS_PATIENT resultData = null;
                if (valid)
                {
                    new HisPatientUpdateInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> UpdateClassify(HisPatientUpdateClassifySDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.PatientId);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisPatientUpdateClassify(param).Run(data);
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
        public ApiResultObject<HIS_PATIENT> ChangeLock(HIS_PATIENT data)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                //valid = valid && new HisPatientVerifyLock(param).VerifyLock(param.ModuleCode, id);
                HIS_PATIENT resultData = null;
                if (valid && new HisPatientLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<bool> Delete(HIS_PATIENT data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisPatientTruncate deleteConcrete = new HisPatientTruncate(param);
                    result = deleteConcrete.PackSingleResult(deleteConcrete.Truncate(data));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateImageByCard()
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisPatientUpdateImageByHisCard(param).Run();
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
        public ApiResultObject<HisPatientVitaminASDO> RegisterVitaminA(HisPatientVitaminASDO data)
        {
            ApiResultObject<HisPatientVitaminASDO> result = new ApiResultObject<HisPatientVitaminASDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisPatientVitaminASDO resultData = null;
                if (valid && new HisPatientRegisterVitaminA(param).Run(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_PATIENT> Follow(HIS_PATIENT data)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT resultData = null;
                if (valid)
                {
                    new HisPatientUpdateOwnBranch(param).Follow(data, ref resultData);
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
        public ApiResultObject<HIS_PATIENT> Unfollow(HIS_PATIENT data)
        {
            ApiResultObject<HIS_PATIENT> result = new ApiResultObject<HIS_PATIENT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PATIENT resultData = null;
                if (valid)
                {
                    new HisPatientUpdateOwnBranch(param).Unfollow(data, ref resultData);
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
        public ApiResultObject<RecognitionResultSDO> Recognition(RecognitionSDO data)
        {
            ApiResultObject<RecognitionResultSDO> result = new ApiResultObject<RecognitionResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                RecognitionResultSDO resultData = null;
                if (valid)
                {
                    new HisPatientRecognition(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> UpdateCard(HisPatientUpdateCardSDO data)
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
                    resultData = new HisPatientUpdateCard(param).Run(data);
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
        public ApiResultObject<HisPatientForKioskSDO> GetInformationForKiosk(HisPatientAdvanceFilter filter)
        {
            ApiResultObject<HisPatientForKioskSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisPatientForKioskSDO resultData = null;
                if (valid)
                {
                    resultData = new HisPatientGet(param).GetInformationForKiosk(filter);
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
    }
}
