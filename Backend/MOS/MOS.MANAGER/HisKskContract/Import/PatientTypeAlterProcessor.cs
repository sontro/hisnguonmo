using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class PatientTypeAlterProcessor : BusinessBase
    {
        private HisPatientTypeAlterCreate hisPatientTypeAlterCreate;

        internal PatientTypeAlterProcessor()
            : base()
        {
            this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
        }

        internal PatientTypeAlterProcessor(CommonParam param)
            : base(param)
        {
            this.hisPatientTypeAlterCreate = new HisPatientTypeAlterCreate(param);
        }

        internal bool Run(PrepareData prepareData, HIS_DEPARTMENT_TRAN departmentTran, WorkPlaceSDO workPlace, ref HIS_PATIENT_TYPE_ALTER patientTypeAlter, ref string desc)
        {
            bool result = false;
            try
            {
                prepareData.PatientTypeAlter.TREATMENT_ID = prepareData.Treatment.ID;
                prepareData.PatientTypeAlter.DEPARTMENT_TRAN_ID = departmentTran.ID;
                prepareData.PatientTypeAlter.TDL_PATIENT_ID = prepareData.Patient.ID;

                HIS_PATIENT_TYPE_ALTER resultData = new HIS_PATIENT_TYPE_ALTER();
                if (!this.hisPatientTypeAlterCreate.Create(prepareData.PatientTypeAlter, prepareData.Treatment, prepareData.Patient, ref resultData))
                {
                    desc = !String.IsNullOrWhiteSpace(param.GetMessage()) ? param.GetMessage() : param.GetBugCode();
                    throw new Exception("Tao PatientTypeAlter that bai");
                }
                result = true;
                patientTypeAlter = resultData;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.hisPatientTypeAlterCreate.RollbackData();
        }
    }
}
