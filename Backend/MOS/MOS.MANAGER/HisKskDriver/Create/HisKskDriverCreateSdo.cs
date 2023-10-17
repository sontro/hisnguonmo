using AutoMapper;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisKskDriver.Sync;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskDriver.Create
{
    internal class HisKskDriverCreateSdo : BusinessBase
    {

        private HisKskDriverCreate kskDriverCreate;
        private HisPatientUpdate patientUpdate;

        internal HisKskDriverCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisKskDriverCreateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.kskDriverCreate = new HisKskDriverCreate(param);
            this.patientUpdate = new HisPatientUpdate(param);
        }

        private HIS_KSK_DRIVER recentKskDriver = null;

        internal bool Run(HisKskDriverSDO data, ref HIS_KSK_DRIVER resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ serviceReq = null;
                HIS_TREATMENT treatment = null;
                HIS_PATIENT patient = null;
                HIS_BRANCH branch = null;
                bool valid = true;
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                HisServiceReqCheck reqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                HisPatientCheck patientChecker = new HisPatientCheck(param);
                valid = valid && checker.VerifyRequireField(data, true);
                valid = valid && reqChecker.VerifyId(data.ServiceReqId, ref serviceReq);
                valid = valid && checker.IsExam(serviceReq);
                valid = valid && reqChecker.IsUnLock(serviceReq);
                valid = valid && patientChecker.VerifyId(serviceReq.TDL_PATIENT_ID, ref patient);
                valid = valid && checker.IsHasTHX(patient);
                valid = valid && treatChecker.VerifyId(serviceReq.TREATMENT_ID, ref treatment);
                valid = valid && checker.VerifyBranch(treatment.BRANCH_ID, ref branch);
                valid = valid && checker.IsValidLicenseClass(data, treatment);
                if (valid)
                {
                    this.ProcessKskDriver(data, serviceReq, treatment, branch);

                    this.ProcessPatient(data, patient);

                    result = true;
                    resultData = new HisKskDriverGet().GetById(this.recentKskDriver.ID);
                    this.InitThreadSync(data, this.recentKskDriver);
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
                this.Rollback();
            }
            return result;
        }

        private void ProcessKskDriver(HisKskDriverSDO data, HIS_SERVICE_REQ serviceReq, HIS_TREATMENT treatment, HIS_BRANCH branch)
        {
            HIS_KSK_DRIVER driver = new HIS_KSK_DRIVER();
            driver.CONCENTRATION = data.Concentration;
            driver.CONCENTRATION_TYPE = data.ConcentrationType;
            driver.CONCLUDER_LOGINNAME = data.ConcluderLoginname;
            driver.CONCLUDER_USERNAME = data.ConcluderUsername;
            driver.CONCLUSION = data.Conclusion;
            driver.CONCLUSION_TIME = data.ConclusionTime;
            driver.DRUG_TYPE = data.DrugType;
            driver.LICENSE_CLASS = data.LicenseClass;
            driver.REASON_BAD_HEATHLY = data.ReasonBadHealthy;
            driver.SERVICE_REQ_ID = serviceReq.ID;
            driver.SICK_CONDITION = data.SickCondition;
            driver.TDL_BRANCH_ID = treatment.BRANCH_ID;
            driver.TDL_MEDI_ORG_CODE = branch.HEIN_MEDI_ORG_CODE;
            driver.TDL_PATIENT_ID = treatment.PATIENT_ID;
            driver.TDL_TREATMENT_ID = treatment.ID;
            driver.SYNC_RESULT_TYPE = IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__NOT_SYNC;
            driver.APPOINTMENT_TIME = data.AppointmentTime;

            if (!this.kskDriverCreate.Create(driver))
            {
                throw new Exception("kskDriverCreate. Ket thuc nghiep vu");
            }

            this.recentKskDriver = driver;
        }

        private void ProcessPatient(HisKskDriverSDO data, HIS_PATIENT patient)
        {
            Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
            HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(patient);
            patient.CMND_NUMBER = null;
            patient.CMND_PLACE = null;
            patient.CMND_DATE = null;
            patient.CCCD_NUMBER = null;
            patient.CCCD_PLACE = null;
            patient.CCCD_DATE = null;
            patient.PASSPORT_NUMBER = null;
            patient.PASSPORT_PLACE = null;
            patient.PASSPORT_DATE = null;
            if (data.CmndNumber.Length == 9)
            {
                patient.CMND_NUMBER = data.CmndNumber;
                patient.CMND_PLACE = data.CmndPlace;
                patient.CMND_DATE = data.CmndDate;
            }
            else if (data.CmndNumber.Length == 12)
            {
                patient.CCCD_NUMBER = data.CmndNumber;
                patient.CCCD_PLACE = data.CmndPlace;
                patient.CCCD_DATE = data.CmndDate;
            }
            else if (data.CmndNumber.Length == 8)
            {
                patient.PASSPORT_NUMBER = data.CmndNumber;
                patient.PASSPORT_PLACE = data.CmndPlace;
                patient.PASSPORT_DATE = data.CmndDate;
            }
            else
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("CMND_NUMBER invalid length");
            }

            if (ValueChecker.IsPrimitiveDiff<HIS_PATIENT>(before, patient))
            {
                if (!this.patientUpdate.Update(patient, before))
                {
                    throw new Exception("patientUpdate. Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void InitThreadSync(HisKskDriverSDO data, HIS_KSK_DRIVER driver)
        {
            try
            {
                if (!data.IsAutoSync || driver == null) return;
                Thread thread = new Thread(new ParameterizedThreadStart(this.ProcessSync));
                thread.Priority = ThreadPriority.Lowest;
                KskDriverSyncSDO sdo = new KskDriverSyncSDO();
                sdo.KskDriveId = driver.ID;
                sdo.SyncData = data.SyncData;
                thread.Start(sdo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessSync(object data)
        {
            try
            {
                KskDriverSyncSDO sdo = (KskDriverSyncSDO)data;
                new HisKskDriverSync().Run(sdo.KskDriveId, sdo.SyncData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            try
            {
                this.patientUpdate.RollbackData();
                this.kskDriverCreate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
