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
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisVaccinationExam
{
    partial class HisVaccinationExamUtil : BusinessBase
    {
        /// <summary>
        /// Luu du thua du lieu
        /// </summary>
        /// <param name="vaccination"></param>
        /// <param name="patient"></param>
        internal static void SetTdl(HIS_VACCINATION_EXAM vaccination, HIS_PATIENT patient)
        {
            if (vaccination != null && patient != null)
            {
                //lay du lieu danh muc
                HIS_CAREER career = HisCareerCFG.DATA != null && patient.CAREER_ID.HasValue ?
                    HisCareerCFG.DATA.Where(o => o.ID == patient.CAREER_ID.Value).FirstOrDefault() : null;
                HIS_GENDER gender = HisGenderCFG.DATA != null ?
                    HisGenderCFG.DATA.Where(o => o.ID == patient.GENDER_ID).FirstOrDefault() : null;
                HIS_WORK_PLACE workPlace = null;

                if (patient.WORK_PLACE_ID.HasValue)
                {
                    workPlace = new HisWorkPlaceGet().GetById(patient.WORK_PLACE_ID.Value);
                }

                //Luu du thua du lieu
                //Luu y: Ko lay VIR_ADDRESS, VIR_PATIENT_NAME trong patient, tranh truong hop du lieu patient 
                //truyen vao khong duoc lay tu DB ==> ko co du lieu vir_address
                vaccination.TDL_PATIENT_ADDRESS = string.Format("{0}{1}{2}{3}", CommonUtil.NVL(patient.ADDRESS, ", "), CommonUtil.NVL(patient.COMMUNE_NAME, ", "), CommonUtil.NVL(patient.DISTRICT_NAME, ", "), CommonUtil.NVL(patient.PROVINCE_NAME, ", "));
                vaccination.TDL_PATIENT_NAME = string.Format("{0}{1}", CommonUtil.NVL(patient.LAST_NAME, " "), CommonUtil.NVL(patient.FIRST_NAME, " "));
                vaccination.TDL_PATIENT_CAREER_NAME = career != null ? career.CAREER_NAME : null;
                vaccination.TDL_PATIENT_CODE = patient.PATIENT_CODE;
                vaccination.TDL_PATIENT_DOB = patient.DOB;
                vaccination.TDL_PATIENT_FIRST_NAME = patient.FIRST_NAME;
                vaccination.TDL_PATIENT_GENDER_ID = patient.GENDER_ID;
                vaccination.TDL_PATIENT_GENDER_NAME = gender.GENDER_NAME;
                vaccination.TDL_PATIENT_IS_HAS_NOT_DAY_DOB = patient.IS_HAS_NOT_DAY_DOB;
                vaccination.TDL_PATIENT_LAST_NAME = patient.LAST_NAME;
                vaccination.TDL_PATIENT_WORK_PLACE = patient.WORK_PLACE;
                vaccination.TDL_PATIENT_WORK_PLACE_NAME = workPlace != null ? workPlace.WORK_PLACE_NAME : null;
                vaccination.PATIENT_ID = patient.ID;

            }
        }

        internal enum VaccinationExamStt
        {
            /// <summary>
            /// Chua xu ly
            /// </summary>
            CHUA_XU_LY = 1,
            /// <summary>
            /// Dang xu ly
            /// </summary>
            DANG_XU_LY = 2,
            /// <summary>
            /// Ket thuc
            /// </summary>
            KET_THUC = 3
        }
    }
}
