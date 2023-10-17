using COS.SDO;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.CosCard;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using COS.EFMODEL.DataModels;
using MOS.MANAGER.Config;

namespace MOS.MANAGER.HisCard
{
    partial class HisCardGet : GetBase
    {
        /// <summary>
        /// Truy van tren he thong cua BV neu ko co thi goi sang he thong the
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public V_HIS_CARD GetCardByCode(string code)
        {
            V_HIS_CARD result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(code);
                if (valid)
                {
                    result = new HisCardGet(param).GetViewByCode(code);

                    //Goi sang he thong COS de lay thong tin
                    if (result == null)
                    {
                        CardOwnerSDO cardOwnerSDO = new CosCardGet().GetByServiceCode(code);

                        if (cardOwnerSDO != null)
                        {
                            //Neu trong DB chua co thi tu dong insert du lieu vao bang HIS_CARD
                            HIS_CARD card = new HIS_CARD();
                            card.CARD_NUMBER = cardOwnerSDO.CardNumber;
                            card.CARD_CODE = cardOwnerSDO.CardCode;
                            card.SERVICE_CODE = cardOwnerSDO.ServiceCode;
                            card.BANK_CARD_CODE = cardOwnerSDO.BankCardCode;

                            if (!new HisCardCreate().Create(card))
                            {
                                LogSystem.Warn("Tu dong insert thong tin the (lay tu COS) that bai");
                            }

                            //tao du lieu
                            result = new V_HIS_CARD();
                            this.AddCardInfo(result, cardOwnerSDO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Truy van tren he thong cua BV neu ko co thi goi sang he thong the
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public HisCardSDO GetCardSdoByCode(string code)
        {
            HisCardSDO result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(code);
                if (valid)
                {
                    V_HIS_CARD hisCard = new HisCardGet(param).GetViewByCode(code);
                    if (hisCard == null || !hisCard.PATIENT_ID.HasValue)
                    {
                        //Goi sang he thong COS de lay thong tin
                        CardOwnerSDO cardOwnerSDO = new CosCardGet().GetByServiceCode(code);

                        if (cardOwnerSDO != null)
                        {
                            //Neu trong DB chua co thi tu dong insert du lieu vao bang HIS_CARD
                            if (hisCard == null)
                            {
                                HIS_CARD card = new HIS_CARD();
                                card.CARD_NUMBER = cardOwnerSDO.CardNumber;
                                card.CARD_CODE = cardOwnerSDO.CardCode;
                                card.SERVICE_CODE = cardOwnerSDO.ServiceCode;
                                card.BANK_CARD_CODE = cardOwnerSDO.BankCardCode;

                                if (!new HisCardCreate().Create(card))
                                {
                                    LogSystem.Warn("Tu dong insert thong tin the (lay tu COS) that bai");
                                }
                            }

                            //tao du lieu
                            result = new HisCardSDO();
                            this.AddCardInfo(result, cardOwnerSDO);
                        }
                    }
                    else
                    {
                        result = new HisCardSDO();
                        this.AddInfo(result, hisCard);
                        this.AddHeinCardInfo(result, hisCard.PATIENT_ID);
                        this.AddAppointmentInfo(result, hisCard.PATIENT_ID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        private void AddCardInfo(HisCardSDO data, CardOwnerSDO cardOwnerSDO)
        {
            if (data != null && cardOwnerSDO != null)
            {
                data.CardCode = cardOwnerSDO.CardCode;
                data.ServiceCode = cardOwnerSDO.ServiceCode;
                data.BankCardCode = cardOwnerSDO.BankCardCode;
                if (cardOwnerSDO.People != null)
                {
                    data.Address = cardOwnerSDO.People.ADDRESS;
                    data.CmndDate = cardOwnerSDO.People.CMND_DATE;
                    data.CmndNumber = cardOwnerSDO.People.CMND_NUMBER;
                    data.CmndPlace = cardOwnerSDO.People.CMND_PLACE;
                    data.CommuneName = cardOwnerSDO.People.COMMUNE_NAME;
                    data.DistrictName = cardOwnerSDO.People.DISTRICT_NAME;
                    data.GenderCode = cardOwnerSDO.People.GENDER_CODE;
                    data.GenderName = cardOwnerSDO.People.GENDER_NAME;
                    data.GenderId = cardOwnerSDO.People.GENDER_ID.HasValue ? cardOwnerSDO.People.GENDER_ID.Value : IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE;
                    data.Dob = cardOwnerSDO.People.DOB.HasValue ? cardOwnerSDO.People.DOB.Value : 0;
                    data.Email = cardOwnerSDO.People.EMAIL;
                    data.EthnicName = cardOwnerSDO.People.ETHNIC_NAME;
                    data.FirstName = cardOwnerSDO.People.FIRST_NAME;
                    data.LastName = cardOwnerSDO.People.LAST_NAME;
                    data.NationalName = cardOwnerSDO.People.NATIONAL_NAME;
                    data.Phone = cardOwnerSDO.People.PHONE;
                    data.ProvinceName = cardOwnerSDO.People.PROVINCE_NAME;
                    data.ReligionName = cardOwnerSDO.People.RELIGION_NAME;
                    data.VirAddress = cardOwnerSDO.People.VIR_ADDRESS;
                    data.WorkPlace = cardOwnerSDO.People.WORK_PLACE;
                    data.HtAddress = cardOwnerSDO.People.HT_ADDRESS;
                    data.HtCommuneName = cardOwnerSDO.People.HT_COMMUNE_NAME;
                    data.HtProvinceName = cardOwnerSDO.People.HT_PROVINCE_NAME;
                    data.HtDistrictName = cardOwnerSDO.People.HT_DISTRICT_NAME;

                    if (IsNotNullOrEmpty(cardOwnerSDO.Bhyts))
                    {
                        COS_BHYT bhyt = cardOwnerSDO.Bhyts.OrderByDescending(o => o.ID).FirstOrDefault();
                        data.HeinCardNumber = bhyt.BHYT_NUMBER;
                        data.HeinCardToTime = bhyt.TO_TIME;
                        data.HeinCardFromTime = bhyt.FROM_TIME;
                        data.HeinOrgCode = bhyt.MEDI_ORG_CODE;
                        data.HeinOrgName = bhyt.MEDI_ORG_NAME;
                    }
                }
            }
        }

        private void AddCardInfo(V_HIS_CARD data, CardOwnerSDO cardOwnerSDO)
        {
            if (data != null && cardOwnerSDO != null)
            {
                data.CARD_CODE = cardOwnerSDO.CardCode;
                data.SERVICE_CODE = cardOwnerSDO.ServiceCode;
                data.BANK_CARD_CODE = cardOwnerSDO.BankCardCode;

                if (cardOwnerSDO.People != null)
                {
                    data.ADDRESS = cardOwnerSDO.People.ADDRESS;
                    data.COMMUNE_NAME = cardOwnerSDO.People.COMMUNE_NAME;
                    data.DISTRICT_NAME = cardOwnerSDO.People.DISTRICT_NAME;
                    data.GENDER_CODE = cardOwnerSDO.People.GENDER_CODE;
                    data.GENDER_NAME = cardOwnerSDO.People.GENDER_NAME;
                    data.DOB = cardOwnerSDO.People.DOB.HasValue ? cardOwnerSDO.People.DOB.Value : 0;
                    data.EMAIL = cardOwnerSDO.People.EMAIL;
                    data.ETHNIC_NAME = cardOwnerSDO.People.ETHNIC_NAME;
                    data.FIRST_NAME = cardOwnerSDO.People.FIRST_NAME;
                    data.LAST_NAME = cardOwnerSDO.People.LAST_NAME;
                    data.NATIONAL_NAME = cardOwnerSDO.People.NATIONAL_NAME;
                    data.PHONE = cardOwnerSDO.People.PHONE;
                    data.PROVINCE_NAME = cardOwnerSDO.People.PROVINCE_NAME;
                    data.RELIGION_NAME = cardOwnerSDO.People.RELIGION_NAME;
                    data.VIR_ADDRESS = cardOwnerSDO.People.VIR_ADDRESS;

                    data.HT_ADDRESS = cardOwnerSDO.People.HT_ADDRESS;
                    data.HT_COMMUNE_NAME = cardOwnerSDO.People.HT_COMMUNE_NAME;
                    data.HT_PROVINCE_NAME = cardOwnerSDO.People.HT_PROVINCE_NAME;
                    data.HT_DISTRICT_NAME = cardOwnerSDO.People.HT_DISTRICT_NAME;
                }
            }
        }

        private void AddInfo(HisCardSDO data, V_HIS_CARD card)
        {
            if (data != null && card != null)
            {
                HIS_GENDER gender = HisGenderCFG.DATA.Where(o => o.ID == card.GENDER_ID).FirstOrDefault();
                data.CardCode = card.CARD_CODE;
                data.ServiceCode = card.SERVICE_CODE;
                data.BankCardCode = card.BANK_CARD_CODE;
                data.Address = card.ADDRESS;
                data.CommuneName = card.COMMUNE_NAME;
                data.DistrictName = card.DISTRICT_NAME;
                data.EthnicName = card.ETHNIC_NAME;
                data.FirstName = card.FIRST_NAME;
                data.GenderName = gender != null ? gender.GENDER_NAME : null;
                data.GenderId = card.GENDER_ID.HasValue ? card.GENDER_ID.Value : IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE;
                data.GenderCode = gender != null ? gender.GENDER_CODE : null;
                data.LastName = card.LAST_NAME;
                data.NationalName = card.NATIONAL_NAME;
                data.PatientCode = card.PATIENT_CODE;
                data.Phone = card.PHONE;
                data.ProvinceName = card.PROVINCE_NAME;
                data.ReligionName = card.RELIGION_NAME;
                data.CareerId = card.CAREER_ID;
                data.Dob = card.DOB.HasValue ? card.DOB.Value : 0;
                data.PatientId = card.PATIENT_ID;
                data.WorkPlace = card.WORK_PLACE;
                data.IsHasNotDayDob = card.IS_HAS_NOT_DAY_DOB;
                data.HtAddress = card.HT_ADDRESS;
                data.HtCommuneName = card.HT_COMMUNE_NAME;
                data.HtDistrictName = card.HT_DISTRICT_NAME;
                data.HtProvinceName = card.HT_PROVINCE_NAME;
            }
        }

        private void AddHeinCardInfo(HisCardSDO data, long? patientId)
        {
            if (patientId.HasValue)
            {
                HisPatientTypeAlterFilterQuery filter = new HisPatientTypeAlterFilterQuery();
                filter.TDL_PATIENT_ID = patientId;
                filter.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters = new HisPatientTypeAlterGet().Get(filter);
                if (IsNotNullOrEmpty(patientTypeAlters))
                {
                    HIS_PATIENT_TYPE_ALTER last = patientTypeAlters.OrderByDescending(t => t.LOG_TIME).ThenByDescending(o => o.ID).FirstOrDefault();
                    if (last != null)
                    {
                        data.HeinCardFromTime = last.HEIN_CARD_FROM_TIME;
                        data.HeinCardToTime = last.HEIN_CARD_TO_TIME;
                        data.HeinCardNumber = last.HEIN_CARD_NUMBER;
                        data.HeinOrgCode = last.HEIN_MEDI_ORG_CODE;
                        data.HeinOrgName = last.HEIN_MEDI_ORG_NAME;
                        data.HeinAddress = last.ADDRESS;
                        data.Join5Year = last.JOIN_5_YEAR;
                        data.Paid6Month = last.PAID_6_MONTH;
                        data.Join5YearTime = last.JOIN_5_YEAR_TIME;
                        data.FreeCoPaidTime = last.FREE_CO_PAID_TIME;
                        data.RightRouteCode = last.RIGHT_ROUTE_CODE;
                        data.LiveAreaCode = last.LIVE_AREA_CODE;
                        data.LevelCode = last.LEVEL_CODE;
                    }
                }
            }
        }

        private void AddAppointmentInfo(HisCardSDO data, long? patientId)
        {
            //Neu dang ky kiosk co su dung thong tin "dich vu hen kham" (dich vu duoc chi dinh khi bac sy ket thuc dieu tri voi loai la hen kham) thi thuc hien truy van lay thong tin
            if (HisTreatmentCFG.IS_USING_APPOINTMENT_SERVICE_IN_KIOSK_RECEPTION)
            {
                //Lay khoang ngay cho phep de truy van dua vao cau hinh so ngay cho phep den muon hoac den som so voi ngay hen kham
                DateTime toTime = DateTime.Now.AddDays(HisTreatmentCFG.ALLOWED_APPOINTMENT_DAY_BEFORE * 1);
                DateTime fromTime = DateTime.Now.AddDays(HisTreatmentCFG.ALLOWED_APPOINTMENT_DAY_AFTER * -1);

                long from = Inventec.Common.DateTime.Get.StartDay(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(fromTime).Value).Value;
                long to = Inventec.Common.DateTime.Get.EndDay(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(toTime).Value).Value;

                string sql = " SELECT TREA.ID AS TREATMENT_ID,TREA.APPOINTMENT_TIME,TREA.ICD_CODE,TREA.ICD_NAME,TREA.ICD_SUB_CODE,TREA.ICD_TEXT,ASV.AMOUNT,ASV.SERVICE_ID,ASV.PATIENT_TYPE_ID "
                            + " FROM HIS_TREATMENT TREA"
                            + " LEFT JOIN HIS_APPOINTMENT_SERV ASV ON TREA.ID = ASV.TREATMENT_ID "
                            + " WHERE TREA.PATIENT_ID = :param1 "
                            + " AND TREA.APPOINTMENT_TIME >= :param2 "
                            + " AND TREA.APPOINTMENT_TIME <= :param3 ";

                List<AppointmentServiceInfo> rs = DAOWorker.SqlDAO.GetSql<AppointmentServiceInfo>(sql, patientId, from, to);
                if (IsNotNullOrEmpty(rs))
                {
                    //Lay ra cac thong tin dich vu hen kham cua ho so co ngay hen kham gan voi ngay hien tai nhat
                    long now = Inventec.Common.DateTime.Get.Now().Value;
                    AppointmentServiceInfo f = rs.OrderBy(o => Math.Abs(now - o.APPOINTMENT_TIME)).ThenBy(o => o.TREATMENT_ID).FirstOrDefault();
                    List<AppointmentServiceInfo> appointmentServices = rs.Where(o => o.TREATMENT_ID == f.TREATMENT_ID).ToList();
                    data.PreviousAppointmentTime = appointmentServices[0].APPOINTMENT_TIME;
                    data.PreviousIcdCode = appointmentServices[0].ICD_CODE;
                    data.PreviousIcdSubCode = appointmentServices[0].ICD_SUB_CODE;
                    data.PreviousIcdName = appointmentServices[0].ICD_NAME;
                    data.PreviousIcdText = appointmentServices[0].ICD_TEXT;
                    data.PreviousTreatmentId = appointmentServices[0].TREATMENT_ID;
                    data.AppointmentServices = appointmentServices.Where(o => o.AMOUNT.HasValue && o.SERVICE_ID.HasValue)
                        .Select(o => new AppointmentServiceSDO
                        {
                            Amount = o.AMOUNT.Value,
                            ServiceId = o.SERVICE_ID.Value,
                            PatientTypeId = o.PATIENT_TYPE_ID
                        }).ToList();
                }
            }
        }

        /// <summary>
        /// Truy van tren he thong cua BV neu ko co thi goi sang he thong the
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<HisCardSDO> GetCardSdoByPhone(string phoneNumber)
        {
            List<HisCardSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(phoneNumber);
                if (valid)
                {
                    List<CardOwnerSDO> cardOwnerSDO = new CosCardGet().GetByMobile(phoneNumber);
                    if (IsNotNullOrEmpty(cardOwnerSDO))
                    {
                        result = new List<HisCardSDO>();
                        foreach (var cardOwner in cardOwnerSDO)
                        {
                            HisCardSDO sdo = new HisCardSDO();
                            this.AddCardInfo(sdo, cardOwner);
                            result.Add(sdo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }
    }
}
