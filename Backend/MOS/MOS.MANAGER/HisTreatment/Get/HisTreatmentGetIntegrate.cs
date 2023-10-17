using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.Filter;
using MOS.MANAGER.HisDepartmentTran;
using MOS.SDO;
using AutoMapper;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisHeinApproval;
using MOS.TDO;
using MOS.MANAGER.HisSereServ;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentGet : GetBase
    {
        internal HisTreatmentInvoiceInfoTDO GetInvoiceInfo(string treatmentCode)
        {
            try
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetByCode(treatmentCode);

                if (treatment != null)
                {
                    HisTreatmentInvoiceInfoTDO result = new HisTreatmentInvoiceInfoTDO();
                    result.Dob = treatment.TDL_PATIENT_DOB;
                    result.PatientCode = treatment.TDL_PATIENT_CODE;
                    result.PatientName = treatment.TDL_PATIENT_NAME;
                    result.Address = treatment.TDL_PATIENT_ADDRESS;
                    result.TreatmentCode = treatment.TREATMENT_CODE;

                    List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                    if (IsNotNullOrEmpty(sereServs))
                    {
                        result.PatientPrice = sereServs.Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value);
                        result.PatientPriceByBhyt = sereServs.Sum(o => o.VIR_TOTAL_PATIENT_PRICE_BHYT.Value);
                        result.PatientPriceByDifference = result.PatientPrice - result.PatientPriceByBhyt;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            return null;
        }
    }
}
