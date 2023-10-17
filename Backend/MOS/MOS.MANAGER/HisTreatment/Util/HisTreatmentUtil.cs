using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisWorkPlace;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisTreatment.Util
{
    partial class HisTreatmentUtil : BusinessBase
    {
        /// <summary>
        /// Luu du thua du lieu vao bang his_treatment
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="patient"></param>
        internal static void SetTdl(HIS_TREATMENT treatment, HIS_PATIENT patient, HIS_PATIENT_TYPE_ALTER pta)
        {
            HisTreatmentUtil.SetTdl(treatment, patient);
            if (pta != null)
                HisTreatmentUtil.SetTdl(treatment, new List<HIS_PATIENT_TYPE_ALTER>() { pta });
        }

        /// <summary>
        /// Luu du thua du lieu
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="patient"></param>
        internal static void SetTdl(HIS_TREATMENT treatment, HIS_PATIENT patient)
        {
            if (treatment != null && patient != null)
            {
                //lay du lieu danh muc
                HIS_CAREER career = HisCareerCFG.DATA != null && patient.CAREER_ID.HasValue ?
                    HisCareerCFG.DATA.Where(o => o.ID == patient.CAREER_ID.Value).FirstOrDefault() : null;
                HIS_GENDER gender = HisGenderCFG.DATA != null ?
                    HisGenderCFG.DATA.Where(o => o.ID == patient.GENDER_ID).FirstOrDefault() : null;
                HIS_MILITARY_RANK militaryRank = HisMilitaryRankCFG.DATA != null ?
                    HisMilitaryRankCFG.DATA.Where(o => patient.MILITARY_RANK_ID.HasValue && o.ID == patient.MILITARY_RANK_ID).FirstOrDefault() : null;

                HIS_WORK_PLACE workPlace = null;

                if (patient.WORK_PLACE_ID.HasValue)
                {
                    workPlace = new HisWorkPlaceGet().GetById(patient.WORK_PLACE_ID.Value);
                }

                //Luu du thua du lieu
                //Luu y: Ko lay VIR_ADDRESS, VIR_PATIENT_NAME trong patient, tranh truong hop du lieu patient 
                //truyen vao khong duoc lay tu DB ==> ko co du lieu vir_address
                treatment.TDL_PATIENT_ADDRESS = string.Format("{0}{1}{2}{3}", CommonUtil.NVL(patient.ADDRESS, ", "), CommonUtil.NVL(patient.COMMUNE_NAME, ", "), CommonUtil.NVL(patient.DISTRICT_NAME, ", "), CommonUtil.NVL(patient.PROVINCE_NAME, ", "));
                treatment.TDL_PATIENT_NAME = string.Format("{0}{1}", CommonUtil.NVL(patient.LAST_NAME, " "), CommonUtil.NVL(patient.FIRST_NAME, " "));
                treatment.TDL_PATIENT_UNSIGNED_NAME = !string.IsNullOrWhiteSpace(treatment.TDL_PATIENT_NAME) ? Inventec.Common.String.Convert.UnSignVNese2(treatment.TDL_PATIENT_NAME) : null;
                treatment.TDL_PATIENT_CAREER_NAME = career != null ? career.CAREER_NAME : null;
                treatment.TDL_PATIENT_CODE = patient.PATIENT_CODE;
                treatment.TDL_PATIENT_DISTRICT_CODE = patient.DISTRICT_CODE;
                treatment.TDL_PATIENT_DOB = patient.DOB;
                treatment.TDL_PATIENT_FIRST_NAME = patient.FIRST_NAME;
                treatment.TDL_PATIENT_GENDER_ID = patient.GENDER_ID;
                treatment.TDL_PATIENT_GENDER_NAME = gender.GENDER_NAME;
                treatment.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = patient.IS_HAS_NOT_DAY_DOB;
                treatment.TDL_PATIENT_LAST_NAME = patient.LAST_NAME;
                treatment.TDL_PATIENT_MILITARY_RANK_NAME = militaryRank != null ? militaryRank.MILITARY_RANK_NAME : null;
                treatment.TDL_PATIENT_NATIONAL_NAME = patient.NATIONAL_NAME;
                treatment.TDL_PATIENT_PROVINCE_CODE = patient.PROVINCE_CODE;
                treatment.TDL_PATIENT_RELATIVE_NAME = patient.RELATIVE_NAME;
                treatment.TDL_PATIENT_RELATIVE_TYPE = patient.RELATIVE_TYPE;
                treatment.TDL_PATIENT_WORK_PLACE = patient.WORK_PLACE;
                treatment.TDL_PATIENT_WORK_PLACE_NAME = workPlace != null ? workPlace.WORK_PLACE_NAME : null;
                treatment.TDL_PATIENT_AVATAR_URL = patient.AVATAR_URL;
                treatment.PATIENT_ID = patient.ID;
                treatment.TDL_PATIENT_COMMUNE_CODE = patient.COMMUNE_CODE;
                treatment.TDL_PATIENT_TAX_CODE = patient.TAX_CODE;
                treatment.TDL_PATIENT_ACCOUNT_NUMBER = patient.ACCOUNT_NUMBER;
                treatment.TDL_PATIENT_MOBILE = patient.MOBILE;
                treatment.TDL_PATIENT_PHONE = patient.PHONE;
                treatment.TDL_PATIENT_CLASSIFY_ID = patient.PATIENT_CLASSIFY_ID;
                treatment.TDL_SOCIAL_INSURANCE_NUMBER = patient.SOCIAL_INSURANCE_NUMBER;

                treatment.TDL_PATIENT_CMND_NUMBER = patient.CMND_NUMBER;
                treatment.TDL_PATIENT_CMND_DATE = patient.CMND_DATE;
                treatment.TDL_PATIENT_CMND_PLACE = patient.CMND_PLACE;
                treatment.TDL_PATIENT_CCCD_NUMBER = patient.CCCD_NUMBER;
                treatment.TDL_PATIENT_CCCD_DATE = patient.CCCD_DATE;
                treatment.TDL_PATIENT_CCCD_PLACE = patient.CCCD_PLACE;
                treatment.TDL_PATIENT_RELATIVE_ADDRESS = patient.RELATIVE_ADDRESS;

                treatment.TDL_PATIENT_RELATIVE_ADDRESS = patient.RELATIVE_ADDRESS;
                treatment.TDL_RELATIVE_CMND_NUMBER = patient.RELATIVE_CMND_NUMBER;
                treatment.TDL_PATIENT_RELATIVE_MOBILE = patient.RELATIVE_MOBILE;
                treatment.TDL_PATIENT_RELATIVE_PHONE = patient.RELATIVE_PHONE;
                treatment.TDL_PATIENT_MOTHER_NAME = patient.MOTHER_NAME;
                treatment.TDL_PATIENT_FATHER_NAME = patient.FATHER_NAME;
                treatment.IS_CHRONIC = patient.IS_CHRONIC;

                treatment.TDL_PATIENT_PROVINCE_NAME = patient.PROVINCE_NAME;
                treatment.TDL_PATIENT_DISTRICT_NAME = patient.DISTRICT_NAME;
                treatment.TDL_PATIENT_COMMUNE_NAME = patient.COMMUNE_NAME;
                treatment.TDL_PATIENT_NATIONAL_CODE = patient.NATIONAL_CODE;
                treatment.TDL_PATIENT_POSITION_ID = patient.POSITION_ID;

                treatment.TDL_PATIENT_PASSPORT_DATE = patient.PASSPORT_DATE;
                treatment.TDL_PATIENT_PASSPORT_NUMBER = patient.PASSPORT_NUMBER;
                treatment.TDL_PATIENT_PASSPORT_PLACE = patient.PASSPORT_PLACE;
                treatment.TDL_PATIENT_WORK_PLACE_ID = patient.WORK_PLACE_ID;
                treatment.IS_TUBERCULOSIS = patient.IS_TUBERCULOSIS;
                treatment.TDL_PATIENT_ETHNIC_NAME = patient.ETHNIC_NAME;

                treatment.TDL_PATIENT_MPS_NATIONAL_CODE = patient.MPS_NATIONAL_CODE;
                treatment.IS_HIV = patient.IS_HIV;
            }
        }

        /// <summary>
        /// Luu du thua du lieu
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="patient"></param>
        internal static void SetTdl(HIS_TREATMENT newTreat, HIS_TREATMENT oldTreat)
        {
            if (newTreat != null && oldTreat != null)
            {
                //Luu du thua du lieu
                //Luu y: Ko lay VIR_ADDRESS, VIR_PATIENT_NAME trong patient, tranh truong hop du lieu patient 
                //truyen vao khong duoc lay tu DB ==> ko co du lieu vir_address
                newTreat.TDL_PATIENT_CODE = oldTreat.TDL_PATIENT_CODE;
                newTreat.PATIENT_ID = oldTreat.PATIENT_ID;
                newTreat.TDL_PATIENT_ADDRESS = oldTreat.TDL_PATIENT_ADDRESS;
                newTreat.TDL_PATIENT_NAME = oldTreat.TDL_PATIENT_NAME;
                newTreat.TDL_PATIENT_CAREER_NAME = oldTreat.TDL_PATIENT_CAREER_NAME;
                newTreat.TDL_PATIENT_DISTRICT_CODE = oldTreat.TDL_PATIENT_DISTRICT_CODE;
                newTreat.TDL_PATIENT_DOB = oldTreat.TDL_PATIENT_DOB;
                newTreat.TDL_PATIENT_FIRST_NAME = oldTreat.TDL_PATIENT_FIRST_NAME;
                newTreat.TDL_PATIENT_GENDER_ID = oldTreat.TDL_PATIENT_GENDER_ID;
                newTreat.TDL_PATIENT_GENDER_NAME = oldTreat.TDL_PATIENT_GENDER_NAME;
                newTreat.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = oldTreat.TDL_PATIENT_IS_HAS_NOT_DAY_DOB;
                newTreat.TDL_PATIENT_LAST_NAME = oldTreat.TDL_PATIENT_LAST_NAME;
                newTreat.TDL_PATIENT_MILITARY_RANK_NAME = oldTreat.TDL_PATIENT_MILITARY_RANK_NAME;
                newTreat.TDL_PATIENT_NATIONAL_NAME = oldTreat.TDL_PATIENT_NATIONAL_NAME;
                newTreat.TDL_PATIENT_PROVINCE_CODE = oldTreat.TDL_PATIENT_PROVINCE_CODE;
                newTreat.TDL_PATIENT_RELATIVE_NAME = oldTreat.TDL_PATIENT_RELATIVE_NAME;
                newTreat.TDL_PATIENT_RELATIVE_TYPE = oldTreat.TDL_PATIENT_RELATIVE_TYPE;
                newTreat.TDL_PATIENT_WORK_PLACE = oldTreat.TDL_PATIENT_WORK_PLACE;
                newTreat.TDL_PATIENT_WORK_PLACE_NAME = oldTreat.TDL_PATIENT_WORK_PLACE_NAME;
                newTreat.TDL_PATIENT_AVATAR_URL = oldTreat.TDL_PATIENT_AVATAR_URL;
                newTreat.TDL_PATIENT_COMMUNE_CODE = oldTreat.TDL_PATIENT_COMMUNE_CODE;
                newTreat.TDL_PATIENT_TAX_CODE = oldTreat.TDL_PATIENT_TAX_CODE;
                newTreat.TDL_PATIENT_ACCOUNT_NUMBER = oldTreat.TDL_PATIENT_ACCOUNT_NUMBER;
                newTreat.TDL_PATIENT_MOBILE = oldTreat.TDL_PATIENT_MOBILE;
                newTreat.TDL_PATIENT_PHONE = oldTreat.TDL_PATIENT_PHONE;
                newTreat.TDL_PATIENT_CLASSIFY_ID = oldTreat.TDL_PATIENT_CLASSIFY_ID;
                newTreat.TDL_SOCIAL_INSURANCE_NUMBER = oldTreat.TDL_SOCIAL_INSURANCE_NUMBER;

                newTreat.TDL_PATIENT_CMND_NUMBER = oldTreat.TDL_PATIENT_CMND_NUMBER;
                newTreat.TDL_PATIENT_CMND_DATE = oldTreat.TDL_PATIENT_CMND_DATE;
                newTreat.TDL_PATIENT_CMND_PLACE = oldTreat.TDL_PATIENT_CMND_PLACE;
                newTreat.TDL_PATIENT_CCCD_NUMBER = oldTreat.TDL_PATIENT_CCCD_NUMBER;
                newTreat.TDL_PATIENT_CCCD_DATE = oldTreat.TDL_PATIENT_CCCD_DATE;
                newTreat.TDL_PATIENT_CCCD_PLACE = oldTreat.TDL_PATIENT_CCCD_PLACE;

                newTreat.TDL_PATIENT_RELATIVE_ADDRESS = oldTreat.TDL_PATIENT_RELATIVE_ADDRESS;
                newTreat.TDL_RELATIVE_CMND_NUMBER = oldTreat.TDL_RELATIVE_CMND_NUMBER;
                newTreat.TDL_PATIENT_RELATIVE_MOBILE = oldTreat.TDL_PATIENT_RELATIVE_MOBILE;
                newTreat.TDL_PATIENT_RELATIVE_PHONE = oldTreat.TDL_PATIENT_RELATIVE_PHONE;
                newTreat.TDL_PATIENT_MOTHER_NAME = oldTreat.TDL_PATIENT_MOTHER_NAME;
                newTreat.TDL_PATIENT_FATHER_NAME = oldTreat.TDL_PATIENT_FATHER_NAME;

                newTreat.TDL_PATIENT_PROVINCE_NAME = oldTreat.TDL_PATIENT_PROVINCE_NAME;
                newTreat.TDL_PATIENT_DISTRICT_NAME = oldTreat.TDL_PATIENT_DISTRICT_NAME;
                newTreat.TDL_PATIENT_COMMUNE_NAME = oldTreat.TDL_PATIENT_COMMUNE_NAME;
                newTreat.TDL_PATIENT_NATIONAL_CODE = oldTreat.TDL_PATIENT_NATIONAL_CODE;
                newTreat.TDL_PATIENT_POSITION_ID = oldTreat.TDL_PATIENT_POSITION_ID;

                newTreat.TDL_PATIENT_PASSPORT_DATE = oldTreat.TDL_PATIENT_PASSPORT_DATE;
                newTreat.TDL_PATIENT_PASSPORT_NUMBER = oldTreat.TDL_PATIENT_PASSPORT_NUMBER;
                newTreat.TDL_PATIENT_PASSPORT_PLACE = oldTreat.TDL_PATIENT_PASSPORT_PLACE;
                newTreat.TDL_PATIENT_WORK_PLACE_ID = oldTreat.TDL_PATIENT_WORK_PLACE_ID;
            }
        }

        internal static void SetTdl(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            if (treatment != null)
            {
                HIS_PATIENT_TYPE_ALTER lastPta = ptas != null ?
                    ptas.OrderByDescending(o => o.LOG_TIME)
                    .ThenByDescending(o => o.ID)
                    .FirstOrDefault() : null;

                HIS_PATIENT_TYPE_ALTER firstClinical = ptas != null ?
                    ptas.Where(o => HisTreatmentTypeCFG.TREATMENTs.Contains(o.TREATMENT_TYPE_ID))
                    .OrderBy(o => o.LOG_TIME)
                    .ThenBy(o => o.ID)
                    .FirstOrDefault() : null;

                treatment.TDL_HEIN_CARD_NUMBER = lastPta != null ? lastPta.HEIN_CARD_NUMBER : null;
                treatment.TDL_HEIN_MEDI_ORG_CODE = lastPta != null ? lastPta.HEIN_MEDI_ORG_CODE : null;
                treatment.TDL_HEIN_MEDI_ORG_NAME = lastPta != null ? lastPta.HEIN_MEDI_ORG_NAME : null;
                treatment.TDL_TREATMENT_TYPE_ID = lastPta != null ? (long?)lastPta.TREATMENT_TYPE_ID : null;
                treatment.TDL_PATIENT_TYPE_ID = lastPta != null ? (long?)lastPta.PATIENT_TYPE_ID : null;
                treatment.TDL_KSK_CONTRACT_ID = lastPta != null ? lastPta.KSK_CONTRACT_ID : null;
                treatment.TDL_HEIN_CARD_FROM_TIME = lastPta != null ? lastPta.HEIN_CARD_FROM_TIME : null;
                treatment.TDL_HEIN_CARD_TO_TIME = lastPta != null ? lastPta.HEIN_CARD_TO_TIME : null;
                treatment.CLINICAL_IN_TIME = firstClinical != null ? new Nullable<long>(firstClinical.LOG_TIME) : null;
            }
        }

        internal static void SetDepartmentInfo(HIS_TREATMENT treatment, List<HIS_DEPARTMENT_TRAN> departmentTrans)
        {
            if (treatment != null)
            {
                List<long> ids = departmentTrans != null ? departmentTrans
                    .Where(o => o.DEPARTMENT_IN_TIME.HasValue)
                    .OrderBy(o => o.DEPARTMENT_IN_TIME.Value)
                    .ThenBy(t => t.ID)
                    .Select(s => s.DEPARTMENT_ID).ToList() : null;

                HIS_DEPARTMENT_TRAN last = departmentTrans != null ? departmentTrans
                    .Where(o => o.DEPARTMENT_IN_TIME.HasValue)
                    .OrderByDescending(o => o.DEPARTMENT_IN_TIME.Value)
                    .ThenByDescending(t => t.ID)
                    .FirstOrDefault() : null;

                treatment.DEPARTMENT_IDS = (ids != null && ids.Count > 0) ? String.Join(",", ids) : null;
                treatment.LAST_DEPARTMENT_ID = last != null ? (long?)last.DEPARTMENT_ID : null;
            }
        }

        internal static void SetCoDepartmentInfo(HIS_TREATMENT treatment, List<HIS_CO_TREATMENT> coTreatments)
        {
            if (treatment != null)
            {
                List<long> ids = coTreatments != null ? coTreatments
                    .Where(o => o.START_TIME.HasValue)
                    .OrderBy(o => o.START_TIME.Value)
                    .ThenBy(t => t.ID)
                    .Select(s => s.DEPARTMENT_ID).ToList() : null;
                treatment.CO_DEPARTMENT_IDS = (ids != null && ids.Count > 0) ? String.Join(",", ids) : null;
            }
        }
    }
}
