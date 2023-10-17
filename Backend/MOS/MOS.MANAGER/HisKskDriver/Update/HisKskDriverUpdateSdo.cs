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

namespace MOS.MANAGER.HisKskDriver.Update
{
    class HisKskDriverUpdateSdo : BusinessBase
    {

        private HisKskDriverUpdate kskDriverUpdate;
        private HisPatientUpdate patientUpdate;

        internal HisKskDriverUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisKskDriverUpdateSdo(CommonParam param)
            : base(param)
        {
            this.Init();
        }
        private void Init()
        {
            this.kskDriverUpdate = new HisKskDriverUpdate(param);
            this.patientUpdate = new HisPatientUpdate(param);
        }

        internal bool Run(HisKskDriverSDO data, ref HIS_KSK_DRIVER resultData)
        {
            bool result = false;
            try
            {
                HIS_KSK_DRIVER raw = null;
                HIS_PATIENT patient = null;
                HIS_TREATMENT treatment = null;
                bool valid = true;
                HisKskDriverCheck checker = new HisKskDriverCheck(param);
                HisPatientCheck patientChecker = new HisPatientCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyRequireField(data, false);
                valid = valid && checker.VerifyId(data.Id.Value, ref raw);
                valid = valid && patientChecker.VerifyId(raw.TDL_PATIENT_ID, ref patient);
                valid = valid && checker.IsHasTHX(patient);
                valid = valid && treatChecker.VerifyId(raw.TDL_TREATMENT_ID, ref treatment);
                valid = valid && checker.IsValidLicenseClass(data, treatment);

                if (valid)
                {
                    this.ProcessKskDriver(data, raw);

                    this.ProcessPatient(data, patient);

                    result = true;
                    resultData = raw;
                    this.InitThreadSync(data, raw);
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

        private void ProcessKskDriver(HisKskDriverSDO data, HIS_KSK_DRIVER raw)
        {
            Mapper.CreateMap<HIS_KSK_DRIVER, HIS_KSK_DRIVER>();
            HIS_KSK_DRIVER before = Mapper.Map<HIS_KSK_DRIVER>(raw);

            raw.CONCENTRATION = data.Concentration;
            raw.CONCENTRATION_TYPE = data.ConcentrationType;
            raw.CONCLUDER_LOGINNAME = data.ConcluderLoginname;
            raw.CONCLUDER_USERNAME = data.ConcluderUsername;
            raw.CONCLUSION = data.Conclusion;
            raw.CONCLUSION_TIME = data.ConclusionTime;
            raw.DRUG_TYPE = data.DrugType;
            raw.LICENSE_CLASS = data.LicenseClass;
            raw.REASON_BAD_HEATHLY = data.ReasonBadHealthy;
            raw.SICK_CONDITION = data.SickCondition;
            if (raw.SYNC_RESULT_TYPE == IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__SYNC_SUCCESSFUL || raw.SYNC_RESULT_TYPE == IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__EDIT_INFO)
            {
                raw.SYNC_RESULT_TYPE = IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__EDIT_INFO;
            }
            else
            {
                raw.SYNC_RESULT_TYPE = IMSys.DbConfig.HIS_RS.HIS_KSK_DRIVER.SYNC_TYPE__NOT_SYNC;
            }
            raw.SYNC_TIME = null;
            raw.SYNC_FAILD_REASON = null;
            raw.APPOINTMENT_TIME = data.AppointmentTime;

            if (!this.kskDriverUpdate.Update(raw, before))
            {
                throw new Exception("kskDriverUpdate. Ket thuc nghiep vu");
            }

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
                this.kskDriverUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
