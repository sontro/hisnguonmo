using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisServiceReq.Prescription.Blood;
using MOS.MANAGER.HisServiceReq.Prescription.Blood.Update;
using MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create;
using MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update;
using MOS.MANAGER.HisServiceReq.Prescription.OutPatient;
using MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create;
using MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update;
using MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Create;
using MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig;
using MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Update;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    public partial class HisServiceReqManager : BusinessBase
    {
        [Logger]
        public ApiResultObject<OutPatientPresResultSDO> OutPatientPresCreate(List<OutPatientPresSDO> listData)
        {
            ApiResultObject<OutPatientPresResultSDO> result = new ApiResultObject<OutPatientPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                OutPatientPresResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqOutPatientPresCreateList(param).Run(listData, ref resultData);
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
        public ApiResultObject<OutPatientPresResultSDO> OutPatientPresCreate(OutPatientPresSDO data)
        {
            return this.OutPatientPresCreate(new List<OutPatientPresSDO>() { data });
        }

        [Logger]
        public ApiResultObject<OutPatientPresResultSDO> OutPatientPresUpdate(OutPatientPresSDO data)
        {
            ApiResultObject<OutPatientPresResultSDO> result = new ApiResultObject<OutPatientPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                OutPatientPresResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqOutPatientPresUpdate(param).Run(data, ref resultData);
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
        public ApiResultObject<InPatientPresResultSDO> InPatientPresCreate(InPatientPresSDO data)
        {
            ApiResultObject<InPatientPresResultSDO> result = new ApiResultObject<InPatientPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                InPatientPresResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqInPatientPresCreateList(param).Run(data, ref resultData);
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
        public ApiResultObject<SubclinicalPresResultSDO> ExpPresCreateByConfig(ExpendPresSDO data)
        {
            ApiResultObject<SubclinicalPresResultSDO> result = new ApiResultObject<SubclinicalPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                SubclinicalPresResultSDO resultData = null;
                if (valid)
                {
                    new ExpendPresCreateByConfig(param).Run(data, ref resultData);
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
        public ApiResultObject<InPatientPresResultSDO> InPatientPresUpdate(InPatientPresSDO data)
        {
            ApiResultObject<InPatientPresResultSDO> result = new ApiResultObject<InPatientPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                InPatientPresResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqInPatientPresUpdate(param).Run(data, ref resultData);
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
        public ApiResultObject<SubclinicalPresResultSDO> SubclinicalPresCreate(SubclinicalPresSDO data)
        {
            ApiResultObject<SubclinicalPresResultSDO> result = new ApiResultObject<SubclinicalPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                SubclinicalPresResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqSubclinicalPresCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<SubclinicalPresResultSDO> SubclinicalPresUpdate(SubclinicalPresSDO data)
        {
            ApiResultObject<SubclinicalPresResultSDO> result = new ApiResultObject<SubclinicalPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                SubclinicalPresResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqSubclinicalPresUpdate(param).Run(data, ref resultData);
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
        public ApiResultObject<List<PatientBloodPresResultSDO>> BloodPresCreate(PatientBloodPresSDO data)
        {
            ApiResultObject <List<PatientBloodPresResultSDO>> result = new ApiResultObject<List<PatientBloodPresResultSDO>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<PatientBloodPresResultSDO> resultData = null;
                if (valid)
                {
                    new HisServiceReqBloodPresCreateList(param).Run(data, ref resultData);
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
        public ApiResultObject<PatientBloodPresResultSDO> BloodPresUpdate(PatientBloodPresSDO data)
        {
            ApiResultObject<PatientBloodPresResultSDO> result = new ApiResultObject<PatientBloodPresResultSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                PatientBloodPresResultSDO resultData = null;
                if (valid)
                {
                    new HisServiceReqBloodPresUpdate(param).Update(data, ref resultData);
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
