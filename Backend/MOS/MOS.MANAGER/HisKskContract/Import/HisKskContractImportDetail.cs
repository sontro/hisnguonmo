using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisKsk;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class HisKskContractImportDetail : BusinessBase
    {
        private PatientProcessor patientProcessor;
        private TreatmentProcessor treatmentProcessor;
        private DepartmentTranProcessor departmentTranProcessor;
        private PatientTypeAlterProcessor patientTypeAlterProcessor;
        private ServiceReqProcessor serviceReqProcessor;

        internal HisKskContractImportDetail()
            : base()
        {
            this.Init();
        }

        internal HisKskContractImportDetail(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.patientProcessor = new PatientProcessor(param);
            this.treatmentProcessor = new TreatmentProcessor(param);
            this.departmentTranProcessor = new DepartmentTranProcessor(param);
            this.patientTypeAlterProcessor = new PatientTypeAlterProcessor(param);
            this.serviceReqProcessor = new ServiceReqProcessor(param);
        }

        internal bool Run(PrepareData prepareData, WorkPlaceSDO workPlace, string loginname, string username, ref string desc)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_DEPARTMENT_TRAN departmentTran = null;
                HIS_PATIENT_TYPE_ALTER patientTypeAlter = null;

                if (valid)
                {
                    bool isNewPatient = prepareData.Patient.ID <= 0;
                    if (!this.patientProcessor.Run(prepareData, ref desc))
                    {
                        throw new Exception("patientProcessor. Ket thuc nghiep vu, Rollback du lieu");
                    }
                    if (!this.treatmentProcessor.Run(prepareData, workPlace, ref desc))
                    {
                        throw new Exception("treatmentProcessor. Ket thuc nghiep vu, Rollback du lieu");
                    }

                    if (!this.departmentTranProcessor.Run(prepareData, workPlace, ref departmentTran, ref desc))
                    {
                        throw new Exception("departmentTranProcessor. Ket thuc nghiep vu, Rollback du lieu");
                    }

                    if (!this.patientTypeAlterProcessor.Run(prepareData, departmentTran, workPlace, ref patientTypeAlter, ref desc))
                    {
                        throw new Exception("patientTypeAlterProcessor. Ket thuc nghiep vu, Rollback du lieu");
                    }

                    if (!this.serviceReqProcessor.Run(prepareData, patientTypeAlter, workPlace, loginname, username, ref desc))
                    {
                        throw new Exception("serviceReqProcessor. Ket thuc nghiep vu, Rollback du lieu");
                    }

                    result = true;
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(desc)) desc = MessageUtil.GetMessage(LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe, param.LanguageCode);
                    prepareData.HisKskPatientSDO.Descriptions.Add(desc);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.Rollback();
                prepareData.HisKskPatientSDO.Descriptions.Add(desc);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            this.serviceReqProcessor.Rollback();
            this.patientTypeAlterProcessor.Rollback();
            this.departmentTranProcessor.Rollback();
            this.treatmentProcessor.Rollback();
            this.patientProcessor.Rollback();
        }
    }
}
