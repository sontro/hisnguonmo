using AutoMapper;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;
using MOS.MANAGER.Token;
using MOS.MANAGER.AosAccount;
using AOS.SDO;

namespace MOS.MANAGER.HisPatient
{
	partial class HisPatientGet : GetBase
	{
        /// <summary>
        /// Lay thong tin benh nhan theo ma the
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        internal HisCardPatientSDO GetSdoByCardServiceCode(string serviceCode)
        {
            try
            {
                HisCardPatientSDO result = null;
                V_HIS_CARD card = new HisCardGet().GetViewByServiceCode(serviceCode);

                if (card != null)
                {
                    result = new HisCardPatientSDO();
                    this.AddPatientInfo(result, card);
                    this.AddCardInfo(result, card);
                    this.AddHeinCardInfo(result, card.PATIENT_ID);
                }
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        private void AddPatientInfo(HisCardPatientSDO data, V_HIS_CARD card)
        {
            if (data != null && card != null)
            {
                data.ADDRESS = card.ADDRESS;
                data.COMMUNE_NAME = card.COMMUNE_NAME;
                data.DISTRICT_NAME = card.DISTRICT_NAME;
                data.ETHNIC_NAME = card.ETHNIC_NAME;
                data.FIRST_NAME = card.FIRST_NAME;
                data.GENDER_NAME = card.GENDER_NAME;
                data.LAST_NAME = card.LAST_NAME;
                data.NATIONAL_NAME = card.NATIONAL_NAME;
                data.PATIENT_CODE = card.PATIENT_CODE;
                data.PHONE = card.PHONE;
                data.PROVINCE_NAME = card.PROVINCE_NAME;
                data.RELIGION_NAME = card.RELIGION_NAME;
                data.CAREER_ID = card.CAREER_ID;
                data.DOB = card.DOB.HasValue ? card.DOB.Value : 0;
                data.ID = card.PATIENT_ID.HasValue ? card.PATIENT_ID.Value : 0;
            }
        }

        private void AddHeinCardInfo(HisCardPatientSDO data, long? patientId)
        {
            if (patientId.HasValue)
            {
                List<V_HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().GetViewByPatientId(patientId.Value);
                if (IsNotNullOrEmpty(patientTypeAlters))
                {
                    V_HIS_PATIENT_TYPE_ALTER pta = patientTypeAlters.OrderByDescending(t => t.LOG_TIME).FirstOrDefault();
                    data.PatientTypeAlter = pta;
                    data.PatientTypeId = pta.PATIENT_TYPE_ID;
                    data.PatientTypeName = pta.PATIENT_TYPE_NAME;
                    data.TreatmentTypeId = pta.TREATMENT_TYPE_ID;
                    data.TreatmentTypeName = pta.TREATMENT_TYPE_NAME;
                }
            }
        }

        private void AddCardInfo(HisCardPatientSDO data, V_HIS_CARD card)
        {
            if (card != null)
            {
                data.CardCode = card.CARD_CODE;
                data.ServiceCode = card.SERVICE_CODE;
            }
        }
    }
}
