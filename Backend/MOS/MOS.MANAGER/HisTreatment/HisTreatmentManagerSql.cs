using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment.Get;
using MOS.MANAGER.HisTreatment.GetNextStoreBordereauCode;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatment
{
    public partial class HisTreatmentManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<List<HisTreatmentWithPatientTypeInfoSDO>> GetTreatmentWithPatientTypeInfoSdo(HisTreatmentWithPatientTypeInfoFilter filter)
        {
            ApiResultObject<List<HisTreatmentWithPatientTypeInfoSDO>> result = null;

            try
            {
                List<HisTreatmentWithPatientTypeInfoSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetTreatmentWithPatientTypeInfoSdo(filter);
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
        public ApiResultObject<HisTreatmentCounterAndPriceSDO> GetTreatmentCounterAndPriceByTime(HisTreatmentCounterAndPriceFilter filter)
        {
            ApiResultObject<HisTreatmentCounterAndPriceSDO> result = null;
            try
            {
                HisTreatmentCounterAndPriceSDO resultData = null;
                resultData = new HisTreatmentGet(param).GetTreatmentCounterAndPriceByTime(filter);
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
        public ApiResultObject<List<MissingInvoiceInfoMaterialSDO>> GetMissingInvoiceInfoMaterialByTreatmentId(long treatmentId)
        {
            ApiResultObject<List<MissingInvoiceInfoMaterialSDO>> result = null;

            try
            {
                List<MissingInvoiceInfoMaterialSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetMissingInvoiceInfoMaterialByTreatmentId(treatmentId);
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
        public ApiResultObject<List<HisTreatmentCounterSDO>> GetTreatmentCounter(List<List<string>> filter)
        {
            ApiResultObject<List<HisTreatmentCounterSDO>> result = null;
            try
            {
                List<HisTreatmentCounterSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetTreatmentCounter(filter);
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
        public ApiResultObject<List<HipoFinanceReportSDO>> GetFinanceReport(List<List<string>> filter)
        {
            ApiResultObject<List<HipoFinanceReportSDO>> result = null;
            try
            {
                List<HipoFinanceReportSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetFinanceReport(filter);
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
        public ApiResultObject<HisTreatmentForEmrSDO> GetForEmr(HisTreatmentForEmrSDO filter)
        {
            ApiResultObject<HisTreatmentForEmrSDO> result = null;
            try
            {
                HisTreatmentForEmrSDO resultData = null;
                resultData = new HisTreatmentGetForEmr(param).Run(filter);
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
        public ApiResultObject<List<string>> GetCardServiceCodeByDepartment(string departmentCode)
        {
            ApiResultObject<List<string>> result = null;
            try
            {
                List<string> resultData = null;
                resultData = new HisTreatmentGetByDepartment(param).GetCardServiceCode(departmentCode);
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
        public ApiResultObject<HisTreatmentForRecordCheckingSDO> GetInfoForRecordChecking(HisTreatmentForRecordCheckingFilter filter)
        {
            ApiResultObject<HisTreatmentForRecordCheckingSDO> result = null;
            try
            {
                HisTreatmentForRecordCheckingSDO resultData = null;
                resultData = new HisTreatmentGetInfoForRecordChecking(param).Run(filter);
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
        public ApiResultObject<List<HisTreatmentRationNotApproveSDO>> GetRationNotApprove(HisTreatmentRationNotApproveFilter filter)
        {
            ApiResultObject<List<HisTreatmentRationNotApproveSDO>> result = null;
            try
            {
                List<HisTreatmentRationNotApproveSDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetRationNotApprove(filter);
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
        public ApiResultObject<List<HisTreatmentMedicineTDO>> GetMedicineForEmr(HisTreatmentMedicineForEmrFilter filter)
        {
            ApiResultObject<List<HisTreatmentMedicineTDO>> result = null;
            try
            {
                List<HisTreatmentMedicineTDO> resultData = null;
                resultData = new HisTreatmentGet(param).GetMedicineForEmr(filter);
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
        public ApiResultObject<HisTreatmentClinicalDetailForEmrTDO> GetClinicalDetailForEmr(string treatmentCode)
        {
            ApiResultObject<HisTreatmentClinicalDetailForEmrTDO> result = null;
            try
            {
                HisTreatmentClinicalDetailForEmrTDO resultData = null;
                resultData = new HisTreatmentGet(param).GetClinicalDetailForEmr(treatmentCode);
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
        public ApiResultObject<string> GetNextStoreBordereauCode(GetStoreBordereauCodeSDO data)
        {
            ApiResultObject<string> result = new ApiResultObject<string>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool isSuccess = false;
                string resultData = null;
                if (valid)
                {
                    isSuccess = new GetNextStoreBordereauCodeProcessor(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
