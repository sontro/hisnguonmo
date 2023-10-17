using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatient.UpdateInfo;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBaby
{
    partial class HisBabyUpdateSdo : BusinessBase
    {
        private const int CCCD_LENGTH = 12;
        private const int CMND_LENGTH = 9;

        private HisBabyUpdate hisBabyUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisPatientUpdate hisPatientUpdate;

        internal HisBabyUpdateSdo()
            : base()
        {
            this.Init();
        }

        internal HisBabyUpdateSdo(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisBabyUpdate = new HisBabyUpdate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        internal bool Run(HisBabySDO data, ref HIS_BABY resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = null;
                HIS_TREATMENT treatmentEdit = null;
                HIS_BABY baby = null;
                HIS_PATIENT hisPatient = null;

                if (this.IsValidData(data, ref baby, ref treatment))
                {
                    this.ProcessBaby(data, baby);
                    this.ProcessPatient(data, treatment, ref hisPatient);
                    this.ProcessTreatment(data.BabyInfoForTreatment, baby, data, ref treatmentEdit, hisPatient);
                    resultData = baby;
                    result = true;

                    HisBabyLog.Run(treatment, treatmentEdit, baby, LibraryEventLog.EventLog.Enum.HisBaby_SuaThongTinTreSoSinh);
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessTreatment(BabyInfoForTreatmentSDO babyInfoForTreatmentSDO, HIS_BABY baby, HisBabySDO data, ref HIS_TREATMENT treatmentEdit, HIS_PATIENT hisPatient)
        {
            HIS_TREATMENT treatment = new HisTreatmentGet().GetById(baby.TREATMENT_ID);
            if (babyInfoForTreatmentSDO != null && treatment != null)
            {
                if (!String.IsNullOrWhiteSpace(data.IdentityNumber))
                {
                    if (data.IdentityNumber.Length <= CMND_LENGTH && CommonUtil.HasChar(data.IdentityNumber))
                    {
                        treatment.TDL_PATIENT_PASSPORT_NUMBER = data.IdentityNumber;
                        treatment.TDL_PATIENT_PASSPORT_PLACE = data.IssuePlace;
                        treatment.TDL_PATIENT_PASSPORT_DATE = data.IssueDate;

                        treatment.TDL_PATIENT_CMND_NUMBER = null;
                        treatment.TDL_PATIENT_CMND_PLACE = null;
                        treatment.TDL_PATIENT_CMND_DATE = null;

                        treatment.TDL_PATIENT_CCCD_NUMBER = null;
                        treatment.TDL_PATIENT_CCCD_PLACE = null;
                        treatment.TDL_PATIENT_CCCD_DATE = null;
                    }
                    else if (data.IdentityNumber.Length == CCCD_LENGTH)
                    {
                        treatment.TDL_PATIENT_CCCD_NUMBER = data.IdentityNumber;
                        treatment.TDL_PATIENT_CCCD_PLACE = data.IssuePlace;
                        treatment.TDL_PATIENT_CCCD_DATE = data.IssueDate;

                        treatment.TDL_PATIENT_PASSPORT_NUMBER = null;
                        treatment.TDL_PATIENT_PASSPORT_PLACE = null;
                        treatment.TDL_PATIENT_PASSPORT_DATE = null;

                        treatment.TDL_PATIENT_CMND_NUMBER = null;
                        treatment.TDL_PATIENT_CMND_PLACE = null;
                        treatment.TDL_PATIENT_CMND_DATE = null;
                    }
                    else if (data.IdentityNumber.Length == CMND_LENGTH)
                    {
                        treatment.TDL_PATIENT_CMND_NUMBER = data.IdentityNumber;
                        treatment.TDL_PATIENT_CMND_PLACE = data.IssuePlace;
                        treatment.TDL_PATIENT_CMND_DATE = data.IssueDate;

                        treatment.TDL_PATIENT_PASSPORT_NUMBER = null;
                        treatment.TDL_PATIENT_PASSPORT_PLACE = null;
                        treatment.TDL_PATIENT_PASSPORT_DATE = null;

                        treatment.TDL_PATIENT_CCCD_NUMBER = null;
                        treatment.TDL_PATIENT_CCCD_PLACE = null;
                        treatment.TDL_PATIENT_CCCD_DATE = null;
                    }
                }
                else
                {
                    treatment.TDL_PATIENT_CMND_NUMBER = null;
                    treatment.TDL_PATIENT_CMND_PLACE = null;
                    treatment.TDL_PATIENT_CMND_DATE = null;

                    treatment.TDL_PATIENT_PASSPORT_NUMBER = null;
                    treatment.TDL_PATIENT_PASSPORT_PLACE = null;
                    treatment.TDL_PATIENT_PASSPORT_DATE = null;

                    treatment.TDL_PATIENT_CCCD_NUMBER = null;
                    treatment.TDL_PATIENT_CCCD_PLACE = null;
                    treatment.TDL_PATIENT_CCCD_DATE = null;
                }

                treatment.TDL_PATIENT_PROVINCE_CODE = data.MotherProvinceCode;
                treatment.TDL_PATIENT_PROVINCE_NAME = data.MotherProvinceName;
                treatment.TDL_PATIENT_DISTRICT_CODE = data.MotherDistrictCode;
                treatment.TDL_PATIENT_DISTRICT_NAME = data.MotherDistrictName;
                treatment.TDL_PATIENT_COMMUNE_CODE = data.MotherCommuneCode;
                treatment.TDL_PATIENT_COMMUNE_NAME = data.MotherCommuneName;
                if (hisPatient != null)
                {
                    treatment.TDL_PATIENT_ADDRESS = hisPatient.VIR_ADDRESS;
                }
                else
                {
                    treatment.TDL_PATIENT_ADDRESS = null;
                }
                

                treatment.NUMBER_OF_FULL_TERM_BIRTH = babyInfoForTreatmentSDO.NumberOfFullTermBirth;
                treatment.NUMBER_OF_PREMATURE_BIRTH = babyInfoForTreatmentSDO.NumberOfPrematureBirth;
                treatment.NUMBER_OF_MISCARRIAGE = babyInfoForTreatmentSDO.NumberOfMiscarriage;
                treatment.NUMBER_OF_TESTS = babyInfoForTreatmentSDO.NumberOfTests;
                treatment.TEST_HIV = babyInfoForTreatmentSDO.TestHiv;
                treatment.TEST_SYPHILIS = babyInfoForTreatmentSDO.TestSyphilis;
                treatment.TEST_HEPATITIS_B = babyInfoForTreatmentSDO.TestHepatitisB;
                treatment.IS_TEST_BLOOD_SUGAR = babyInfoForTreatmentSDO.IsTestBloodSugar;
                treatment.IS_EARLY_NEWBORN_CARE = babyInfoForTreatmentSDO.IsEarlyNewbornCare;
                treatment.NEWBORN_CARE_AT_HOME = babyInfoForTreatmentSDO.NewbornCareAtHome;
                treatment.NUMBER_OF_BIRTH = babyInfoForTreatmentSDO.NumberOfBirth;

                if (!this.hisTreatmentUpdate.Update(treatment))
                {
                    throw new Exception("Cap nhat thong tin ho so that bai");
                }

                treatmentEdit = treatment;
            }
        }

        private void ProcessBaby(HisBabySDO data, HIS_BABY baby)
        {
            Mapper.CreateMap<HIS_BABY, HIS_BABY>();
            HIS_BABY old = Mapper.Map<HIS_BABY>(baby);

            baby.TREATMENT_ID = data.TreatmentId;
            baby.BABY_NAME = data.BabyName;
            baby.BABY_ORDER = data.BabyOrder;
            baby.BORN_POSITION_ID = data.BornPositionId;
            baby.BORN_RESULT_ID = data.BornResultId;
            baby.BORN_TIME = data.BornTime;
            baby.BORN_TYPE_ID = data.BornTypeId;
            baby.FATHER_NAME = data.FatherName;
            baby.GENDER_ID = data.GenderId;
            baby.HEAD = data.Head;
            baby.HEIGHT = data.Height;
            baby.MIDWIFE = data.Midwife;
            baby.MONTH_COUNT = data.MonthCount;
            baby.WEEK_COUNT = data.WeekCount;
            baby.WEIGHT = data.Weight;
            baby.ETHNIC_CODE = data.EthnicCode;
            baby.ETHNIC_NAME = data.EthnicName;
            baby.BIRTH_CERT_BOOK_ID = data.BirthCertBookID;
            baby.ISSUER_LOGINNAME = (String.IsNullOrWhiteSpace(data.IssuerLoginname) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName() : data.IssuerLoginname);
            baby.ISSUER_USERNAME = (String.IsNullOrWhiteSpace(data.IssuerLoginname) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName() : data.IssuerUsername);
            baby.CURRENT_ALIVE = data.CurrentAlive;
            baby.NUMBER_OF_PREGNANCIES = data.NumberOfPregnancies;
            baby.POSTPARTUM_CARE = data.PostpartumCare;
            baby.IS_DIFFICULT_BIRTH = data.IsDifficultBirth.HasValue && data.IsDifficultBirth.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_HAEMORRHAGE = data.IsHaemorrhage.HasValue && data.IsHaemorrhage.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_UTERINE_RUPTURE = data.IsUterineRupture.HasValue && data.IsUterineRupture.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_PUERPERAL = data.IsPuerperal.HasValue && data.IsPuerperal.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_BACTERIAL_CONTAMINATION = data.IsBacterialContamination.HasValue && data.IsBacterialContamination.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_TETANUS = data.IsTetanus.HasValue && data.IsTetanus.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_MOTHER_DEATH = data.IsMotherDeath.HasValue && data.IsMotherDeath.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_FETAL_DEATH_22_WEEKS = data.IsFetalDeath22Weeks.HasValue && data.IsFetalDeath22Weeks.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_INJECT_K1 = data.IsInjectK1.HasValue && data.IsInjectK1.Value ? (short?)Constant.IS_TRUE : null;
            baby.IS_INJECT_B = data.IsInjectB.HasValue && data.IsInjectB.Value ? (short?)Constant.IS_TRUE : null;
            baby.BIRTHPLACE = data.Birthplace;
            baby.METHOD_STYLE = data.MethodStyle;
            baby.NOTE = data.Note;
            baby.HEIN_CARD_NUMBER_TMP = data.HeinCardNumberTmp;
            baby.DEPARTMENT_ID = data.DepartmentId;
            baby.IS_SURGERY = data.IsSurgery;
            baby.NUMBER_CHILDREN_BIRTH = data.NumberChildrenBirth;
            baby.DEATH_DATE = data.DeathDate;
            baby.ISSUED_DATE = data.IssueDateBaby;
            baby.BIRTHPLACE_TYPE = data.BirthplaceType;
            baby.BIRTH_PROVINCE_CODE = data.BirthProvinceCode;
            baby.BIRTH_PROVINCE_NAME = data.BirthProvinceName;
            baby.BIRTH_COMMUNE_CODE = data.BirthCommuneCode;
            baby.BIRTH_COMMUNE_NAME = data.BirthCommuneName;
            baby.BIRTH_DISTRICT_CODE = data.BirthDistrictCode;
            baby.BIRTH_DISTRICT_NAME = data.BirthDistrictName;
            baby.BIRTH_HOSPITAL_CODE = data.BirthHospitalCode;
            baby.BIRTH_HOSPITAL_NAME = data.BirthHospitalName;

            if (ValueChecker.IsPrimitiveDiff<HIS_BABY>(old, baby)
                && !this.hisBabyUpdate.Update(baby))
            {
                throw new Exception("Cap nhat thong tin baby that bai");
            }
        }

        private void ProcessPatient(HisBabySDO data, HIS_TREATMENT treatment, ref HIS_PATIENT hisPatient)
        {
            HIS_PATIENT patient = new HisPatientGet().GetById(treatment.PATIENT_ID);
            //Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
            //HIS_PATIENT old = Mapper.Map<HIS_PATIENT>(patient);

            if (!String.IsNullOrWhiteSpace(data.IdentityNumber))
            {
                if (data.IdentityNumber.Length <= CMND_LENGTH && CommonUtil.HasChar(data.IdentityNumber))
                {
                    patient.PASSPORT_NUMBER = data.IdentityNumber;
                    patient.PASSPORT_PLACE = data.IssuePlace;
                    patient.PASSPORT_DATE = data.IssueDate;

                    patient.CMND_NUMBER = null;
                    patient.CMND_PLACE = null;
                    patient.CMND_DATE = null;

                    patient.CCCD_NUMBER = null;
                    patient.CCCD_PLACE = null;
                    patient.CCCD_DATE = null;
                }
                else if (data.IdentityNumber.Length == CCCD_LENGTH)
                {
                    patient.CCCD_NUMBER = data.IdentityNumber;
                    patient.CCCD_PLACE = data.IssuePlace;
                    patient.CCCD_DATE = data.IssueDate;

                    patient.PASSPORT_NUMBER = null;
                    patient.PASSPORT_PLACE = null;
                    patient.PASSPORT_DATE = null;

                    patient.CMND_NUMBER = null;
                    patient.CMND_PLACE = null;
                    patient.CMND_DATE = null;
                }
                else if (data.IdentityNumber.Length == CMND_LENGTH)
                {
                    patient.CMND_NUMBER = data.IdentityNumber;
                    patient.CMND_PLACE = data.IssuePlace;
                    patient.CMND_DATE = data.IssueDate;

                    patient.PASSPORT_NUMBER = null;
                    patient.PASSPORT_PLACE = null;
                    patient.PASSPORT_DATE = null;

                    patient.CCCD_NUMBER = null;
                    patient.CCCD_PLACE = null;
                    patient.CCCD_DATE = null;
                }
            }
            else
            {
                patient.CMND_NUMBER = null;
                patient.CMND_PLACE = null;
                patient.CMND_DATE = null;

                patient.PASSPORT_NUMBER = null;
                patient.PASSPORT_PLACE = null;
                patient.PASSPORT_DATE = null;

                patient.CCCD_NUMBER = null;
                patient.CCCD_PLACE = null;
                patient.CCCD_DATE = null;
            }

            patient.PROVINCE_CODE = data.MotherProvinceCode;
            patient.PROVINCE_NAME = data.MotherProvinceName;
            patient.DISTRICT_CODE = data.MotherDistrictCode;
            patient.DISTRICT_NAME = data.MotherDistrictName;
            patient.COMMUNE_CODE = data.MotherCommuneCode;
            patient.COMMUNE_NAME = data.MotherCommuneName;
            patient.ADDRESS = data.MotherAddress;

            patient.HT_PROVINCE_NAME = data.HtProvinceName;
            patient.HT_DISTRICT_NAME = data.HtDistrictName;
            patient.HT_COMMUNE_NAME = data.HtCommuneName;
            patient.HT_ADDRESS = data.HtAddress;

            if (!this.hisPatientUpdate.Update(patient))
            {
                throw new Exception("Cap nhat thong tin his_patient that bai. Rollback");
            }
            if (patient != null)
            {
                hisPatient = patient;
            }
        }

        private bool IsValidData(HisBabySDO data, ref HIS_BABY baby, ref HIS_TREATMENT treatment)
        {
            try
            {
                baby = data.Id.HasValue ? new HisBabyGet().GetById(data.Id.Value) : null;
                if (baby == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("id ko ton tai");
                    return false;
                }

                treatment = new HisTreatmentGet().GetById(data.TreatmentId);
                if (treatment == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("treatment_id ko ton tai");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void Rollback()
        {
            this.hisTreatmentUpdate.RollbackData();
            this.hisPatientUpdate.RollbackData();
            this.hisBabyUpdate.RollbackData();
        }
    }
}
