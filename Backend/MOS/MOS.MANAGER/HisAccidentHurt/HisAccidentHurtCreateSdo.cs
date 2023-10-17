using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccidentHurt
{
    partial class HisAccidentHurtCreateSdo : BusinessBase
    {
        private HIS_ACCIDENT_HURT recentHisAccidentHurtDTO = new HIS_ACCIDENT_HURT();
        private HisPatientUpdate hisPatientUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisAccidentHurtCreateSdo()
            : base()
        {
            this.Init();
        }

        internal HisAccidentHurtCreateSdo(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Create(HisAccidentHurtSDO data, ref HIS_ACCIDENT_HURT resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlace = null;
                HIS_TREATMENT treatment = null;
                HIS_PATIENT patient = null;
                bool valid = true;
                HisAccidentHurtCheck checker = new HisAccidentHurtCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisPatientCheck patientChecker = new HisPatientCheck(param);

                valid = valid && this.HasWorkPlaceInfo(data.ExecuteRoomId, ref workPlace);
                valid = valid && treatmentChecker.VerifyId(data.TreatmentId, ref treatment);
                valid = valid && treatment != null && patientChecker.VerifyId(treatment.PATIENT_ID, ref patient);

                if (valid)
                {
                    string year = DateTime.Now.Year.ToString().Substring(2);

                    HIS_ACCIDENT_HURT accidentHurt = new HIS_ACCIDENT_HURT();
                    accidentHurt.ACCIDENT_BODY_PART_ID = data.AccidentBodyPartId;
                    accidentHurt.ACCIDENT_CARE_ID = data.AccidentCareId;
                    accidentHurt.ACCIDENT_HELMET_ID = data.AccidentHelmetId;
                    accidentHurt.ACCIDENT_HURT_TYPE_ID = data.AccidentHurtTypeId;
                    accidentHurt.ACCIDENT_LOCATION_ID = data.AccidentLocationId;
                    accidentHurt.ACCIDENT_POISON_ID = data.AccidentPoisonId;
                    accidentHurt.ACCIDENT_RESULT_ID = data.AccidentResultId;
                    accidentHurt.ACCIDENT_TIME = data.AccidentTime;
                    accidentHurt.ACCIDENT_VEHICLE_ID = data.AccidentVehicleId;
                    accidentHurt.ALCOHOL_TEST_RESULT = data.AlcoholTestResult ? (short?) Constant.IS_TRUE : null;
                    accidentHurt.CONTENT = data.Content;
                    accidentHurt.IS_USE_ALCOHOL = data.IsUseAlcohol ? (short?)Constant.IS_TRUE : null;
                    accidentHurt.NARCOTICS_TEST_RESULT = data.NarcoticsTestResult ? (short?)Constant.IS_TRUE : null;
                    accidentHurt.STATUS_IN = data.StatusIn;
                    accidentHurt.STATUS_OUT = data.StatusOut;
                    accidentHurt.TREATMENT_ID = data.TreatmentId;
                    accidentHurt.TREATMENT_INFO = data.TreatmentInfo;
                    accidentHurt.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    accidentHurt.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    accidentHurt.EXECUTE_ROOM_ID = workPlace.RoomId;
                    accidentHurt.EXECUTE_DEPARTMENT_ID = workPlace.DepartmentId;
                    accidentHurt.SEEDING_ISSUED_CODE = year;
                    accidentHurt.ACCIDENT_HURT_ICD_CODE = data.AccidentHurtIcdCode;
                    accidentHurt.ACCIDENT_HURT_ICD_NAME = data.AccidentHurtIcdName;
                    accidentHurt.ACCIDENT_HURT_ICD_SUB_CODE = data.AccidentHurtIcdSubCode;
                    accidentHurt.ACCIDENT_HURT_ICD_TEXT = data.AccidentHurtIcdText;

                    if (!DAOWorker.HisAccidentHurtDAO.Create(accidentHurt))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisAccidentHurt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisAccidentHurt that bai." + LogUtil.TraceData("data", data));
                    }

                    this.recentHisAccidentHurtDTO = accidentHurt;

                    this.ProcessUpdatePatientInfo(patient, data);

                    this.ProcessUpdateTreatmentInfo(treatment, data);

                    resultData = accidentHurt;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void ProcessUpdatePatientInfo(HIS_PATIENT patient, HisAccidentHurtSDO data)
        {
            Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
            HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(patient);

            patient.CCCD_DATE = data.CccdDate;
            patient.CCCD_NUMBER = data.CccdNumber;
            patient.CCCD_PLACE = data.CccdPlace;
            patient.CMND_NUMBER = data.CmndNumber;
            patient.CMND_PLACE = data.CmndPlace;
            patient.CMND_DATE = data.CmndDate;

            if (!this.hisPatientUpdate.UpdateWithoutChecking(patient, before))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisPatient_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin HisPatient that bai." + LogUtil.TraceData("data", data));
            }
        }

        internal void ProcessUpdateTreatmentInfo(HIS_TREATMENT treatment, HisAccidentHurtSDO data)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(treatment);

            treatment.HOSPITALIZATION_REASON = data.HospitalizationReason;

            if (!this.hisTreatmentUpdate.Update(treatment, before, true))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
            }
        }
		
		internal void RollbackData()
        {
            if (this.recentHisAccidentHurtDTO != null)
            {
                if (!DAOWorker.HisAccidentHurtDAO.Truncate(this.recentHisAccidentHurtDTO))
                {
                    LogSystem.Warn("Rollback du lieu HisAccidentHurt that bai, can kiem tra lai." + LogUtil.TraceData("HisAccidentHurt", this.recentHisAccidentHurtDTO));
                }
                this.recentHisAccidentHurtDTO = null;
            }
            this.hisPatientUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
