using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class TreatmentProcessor : BusinessBase
    {
        private HisTreatmentCreate hisTreatmentCreate;

        internal TreatmentProcessor()
            : base()
        {
            this.hisTreatmentCreate = new HisTreatmentCreate(param);
        }

        internal TreatmentProcessor(CommonParam param)
            : base(param)
        {
            this.hisTreatmentCreate = new HisTreatmentCreate(param);
        }

        internal bool Run(PrepareData prepareData, WorkPlaceSDO wp, ref string desc)
        {
            bool result = false;
            try
            {
                prepareData.Treatment.LAST_DEPARTMENT_ID = wp.DepartmentId;
                prepareData.Treatment.DEPARTMENT_IDS = wp.DepartmentId.ToString();
                if (!string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdCode) && !string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdName))
                {
                    prepareData.Treatment.ICD_CODE = prepareData.HisKskPatientSDO.IcdCode;
                    prepareData.Treatment.ICD_NAME = prepareData.HisKskPatientSDO.IcdName;
                }
                HIS_ICD hisIcdCode = new HisIcdGet().GetByCode(prepareData.HisKskPatientSDO.IcdCode);
                List<HIS_ICD> hisIcdSubCodes = new List<HIS_ICD>();
                if (!string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdSubCode))
                {
                    List<string> subCodes = new List<string>();
                    subCodes = prepareData.HisKskPatientSDO.IcdSubCode.Split(';').ToList();
                    hisIcdSubCodes = new HisIcdGet().GetByCodes(subCodes);
                }

                if (!string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdCode) && string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdName))
                {
                    prepareData.Treatment.ICD_CODE = prepareData.HisKskPatientSDO.IcdCode;
                    if (hisIcdCode != null && hisIcdCode.ICD_NAME != null)
                    {
                        prepareData.Treatment.ICD_NAME = hisIcdCode.ICD_NAME;
                    }
                    else
                    {
                        prepareData.Treatment.ICD_NAME = "";
                    }
                }
                if (!string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdSubCode) && !string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdText))
                {
                    prepareData.Treatment.ICD_SUB_CODE = prepareData.HisKskPatientSDO.IcdSubCode;
                    prepareData.Treatment.ICD_TEXT = prepareData.HisKskPatientSDO.IcdText;
                }
                if (!string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdSubCode) && string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdText))
                {
                    if (hisIcdCode != null)
                    {
                        List<string> icdSubCodes = hisIcdSubCodes.Select(o => o.ICD_CODE).ToList();
                        List<string> icdNames = hisIcdSubCodes.Select(o => o.ICD_NAME).ToList();

                        prepareData.Treatment.ICD_SUB_CODE = string.Join(";", icdSubCodes);
                        prepareData.Treatment.ICD_TEXT = string.Join(";", icdNames);
                    }
                    else
                    {
                        prepareData.Treatment.ICD_SUB_CODE = "";
                        prepareData.Treatment.ICD_TEXT = "";
                    }
                }
                if (string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdSubCode) && string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdText))
                {
                    prepareData.Treatment.ICD_SUB_CODE = "";
                    prepareData.Treatment.ICD_TEXT = "";
                }
                if (string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdCode) && string.IsNullOrWhiteSpace(prepareData.HisKskPatientSDO.IcdName))
                {
                    prepareData.Treatment.ICD_CODE = "";
                    prepareData.Treatment.ICD_NAME = "";
                }
                if (!this.hisTreatmentCreate.CreateWithoutValidate(prepareData.Treatment, prepareData.Patient, prepareData.PatientTypeAlter))
                {
                    desc = !String.IsNullOrWhiteSpace(param.GetMessage()) ? param.GetMessage() : param.GetBugCode();
                    throw new Exception("Tao Treatment that bai");
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;                
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisTreatmentCreate.Rollback();
        }
    }
}
