using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCard;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisTreatment;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MOS.SDO;
using AutoMapper;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.Token;

namespace MOS.MANAGER.HisPatient
{
    partial class HisPatientGet : GetBase
    {
        internal HisPatientForKioskSDO GetInformationForKiosk(HisPatientAdvanceFilter filter)
        {
            try
            {
                HisPatientForKioskSDO result = null;

                List<HisPatientSDO> datas = this.GetSdoAdvance(filter);
                if (IsNotNullOrEmpty(datas))
                {
                    Mapper.CreateMap<HisPatientSDO, HisPatientForKioskSDO>();
                    result = Mapper.Map<HisPatientForKioskSDO>(datas[0]);
                    var treatmentId = result.TreatmentId;
                    if (treatmentId.HasValue)
                    {
                        var inDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(result.InDate ?? 0);
                        if (inDate.HasValue && inDate.Value.Date >= DateTime.Today && result.IsPause != 1)
                        {

                            result.PatientTypeAlters = new HisPatientTypeAlterGet().GetViewByTreatmentId(treatmentId.Value);
                            result.ServiceReqs = new HisServiceReqGet().GetViewByTreatmentId(treatmentId.Value);
                            result.SereServs = new HisSereServGet().GetViewByTreatmentId(treatmentId.Value);
                        }

                        HisSereServDepositFilterQuery filterHisSereServDeposit = new HisSereServDepositFilterQuery() { TDL_TREATMENT_ID = treatmentId, IS_CANCEL = false };
                        result.SereServDeposits = new HisSereServDepositGet().Get(filterHisSereServDeposit);

                        HisSereServBillFilterQuery filterHisSereServBill = new HisSereServBillFilterQuery() { TDL_TREATMENT_ID = treatmentId, IS_NOT_CANCEL = true };
                        result.SereServBills = new HisSereServBillGet().Get(filterHisSereServBill);

                        //Chi ho tro thanh toan o kiosk neu benh nhan quet the
                        if (EpaymentCFG.KIOSK_PAYMENT_OPTION == EpaymentCFG.KioskPaymentOption.AUTO_PAY && IsNotNull(filter.SERVICE_CODE__EXACT))
                        {
                            HIS_BRANCH branch = new TokenManager().GetBranch();
                            //Lay so du tuong ung voi the duoc quet
                            result.Balance = new HisPatientBalance().GetAndUpdateCardBalance(filter.SERVICE_CODE__EXACT, branch.THE_BRANCH_CODE);
                        }
                    }
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

        internal List<HisPatientSDO> GetSdoAdvance(HisPatientAdvanceFilter filter)
        {
            try
            {
                List<HisPatientSDO> result = null;
                if (filter != null)
                {
                    //Neu tim theo ma dieu tri
                    if (IsNotNull(filter.TREATMENT_CODE__EXACT))
                    {
                        result = this.GetByTreatmentCode(filter.TREATMENT_CODE__EXACT);
                    }
                    //Neu tim theo ma benh nhan
                    else if (IsNotNull(filter.PATIENT_CODE__EXACT))
                    {
                        result = this.GetByPatientCode(filter.PATIENT_CODE__EXACT);
                    }
                    else if (IsNotNull(filter.HEIN_CARD_NUMBER_OR_CCCD_NUMBER))
                    {
                        if (IsNotNull(filter.HEIN_CARD_NUMBER_OR_CCCD_NUMBER.HEIN_CARD_NUMBER__EXACT) || IsNotNull(filter.HEIN_CARD_NUMBER_OR_CCCD_NUMBER.CCCD_NUMBER__EXACT))
                        {
                            result = this.GetByHeinCardNumberOrCccdNumber(filter.HEIN_CARD_NUMBER_OR_CCCD_NUMBER.HEIN_CARD_NUMBER__EXACT, filter.HEIN_CARD_NUMBER_OR_CCCD_NUMBER.CCCD_NUMBER__EXACT);
                        }
                    }
                    //Neu tim theo cac thong tin dinh danh khac (CMND/CCCD)
                    else if (IsNotNull(filter.CMND_NUMBER__EXACT) || IsNotNull(filter.CCCD_NUMBER__EXACT))
                    {
                        result = this.GetByPatientIdentifiedNumber(filter.CMND_NUMBER__EXACT, filter.CCCD_NUMBER__EXACT);
                    }
                    //Neu tim theo so the BHYT
                    else if (IsNotNull(filter.HEIN_CARD_NUMBER__EXACT))
                    {
                        result = this.GetByHeinCardNumber(filter.HEIN_CARD_NUMBER__EXACT);
                    }
                    //Neu tim theo ma the KCB
                    else if (IsNotNull(filter.CARD_CODE__EXACT))
                    {
                        result = this.GetByCardCode(filter.CARD_CODE__EXACT);
                    }
                    else if (IsNotNull(filter.APPOINTMENT_CODE__EXACT))
                    {
                        result = this.GetByAppointmentCode(filter.APPOINTMENT_CODE__EXACT);
                    }
                    else if (IsNotNull(filter.PATIENT_PROGRAM_CODE__EXACT))
                    {
                        result = this.GetByPatientProgramCode(filter.PATIENT_PROGRAM_CODE__EXACT);
                    }
                    else if (IsNotNull(filter.HRM_EMPLOYEE_CODE__EXACT))
                    {
                        result = this.GetByPatientHrmEmployeeCode(filter.HRM_EMPLOYEE_CODE__EXACT);
                    }
                    else if (IsNotNull(filter.PHONE__EXACT))
                    {
                        result = this.GetByPatientPhoneNumber(filter.PHONE__EXACT);
                    }
                    else if (IsNotNull(filter.SERVICE_CODE__EXACT))
                    {
                        result = this.GetByServiceCode(filter.SERVICE_CODE__EXACT);
                    }
                    else
                    {
                        result = this.GetByPatientInfo(filter.VIR_PATIENT_NAME__EXACT, filter.GENDER_ID, filter.DOB);
                    }
                    if (result != null && result.Count == 1)
                    {
                        V_HIS_TREATMENT_FEE_4 lastTreatmentFee = null;
                        List<PreviousDebtTreatmentSDO> previousDebts = this.GetPreviousDebtTreatment(result[0].ID, ref lastTreatmentFee);
                        result[0].PreviousDebtTreatments = IsNotNullOrEmpty(previousDebts) ? previousDebts.Select(o => o.TDL_TREATMENT_CODE).ToList() : null;
                        result[0].PreviousDebtTreatmentDetails = previousDebts;
                        result[0].PreviousPrescriptions = this.GetPreviousPrescription(result[0].ID);
                        result[0].TodayFinishTreatments = this.GetTodayFinishTreatment(result[0].ID);
                        result[0].LastTreatmentFee = lastTreatmentFee;
                    }
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

        internal HisPatientWarningSDO GetPreviousWarning(long patientId)
        {
            try
            {
                HisPatientWarningSDO result = new HisPatientWarningSDO();
                V_HIS_TREATMENT_FEE_4 lastTreatmentFee = null;
                List<PreviousDebtTreatmentSDO> previousDebts = this.GetPreviousDebtTreatment(patientId, ref lastTreatmentFee);
                result.PreviousDebtTreatments = IsNotNullOrEmpty(previousDebts) ? previousDebts.Select(o => o.TDL_TREATMENT_CODE).ToList() : null;
                result.PreviousPrescriptions = this.GetPreviousPrescription(patientId);
                result.TodayFinishTreatments = this.GetTodayFinishTreatment(patientId);
                result.LastTreatmentFee = lastTreatmentFee;
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        private List<string> GetTodayFinishTreatment(long patientId)
        {
            try
            {
                if (HisTreatmentCFG.IS_CHECK_TODAY_FINISH_TREATMENT)
                {
                    long today = Inventec.Common.DateTime.Get.StartDay().Value;

                    string query = "SELECT TREATMENT_CODE FROM HIS_TREATMENT WHERE PATIENT_ID = :param1 AND IS_PAUSE = :param2 AND OUT_DATE = :param3 ";
                    return DAOWorker.SqlDAO.GetSql<string>(query, patientId, Constant.IS_TRUE, today);
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisPreviousPrescriptionSDO> GetPreviousPrescription(long patientId)
        {
            try
            {
                if (HisTreatmentCFG.IS_CHECK_PREVIOUS_PRESCRIPTION)
                {
                    long today = Inventec.Common.DateTime.Get.StartDay().Value;
                    //review
                    string query = "SELECT USE_TIME_TO, REQUEST_ROOM_ID, TDL_TREATMENT_CODE AS TREATMENT_CODE, SERVICE_REQ_CODE, INTRUCTION_TIME FROM HIS_SERVICE_REQ WHERE TDL_PATIENT_ID = :param1 AND SERVICE_REQ_STT_ID = :param2 AND USE_TIME_TO >= :param3 ";
                    List<HisPreviousPrescriptionSDO> data = DAOWorker.SqlDAO.GetSql<HisPreviousPrescriptionSDO>(query, patientId, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT, today);
                    if (IsNotNullOrEmpty(data))
                    {
                        foreach (HisPreviousPrescriptionSDO sdo in data)
                        {
                            if (sdo.REQUEST_ROOM_ID > 0)
                            {
                                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == sdo.REQUEST_ROOM_ID).FirstOrDefault();
                                sdo.REQUEST_ROOM_NAME = room != null ? room.ROOM_NAME : null;
                            }
                        }
                    }
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HisPreviousPrescriptionDetailSDO> GetPreviousPrescriptionDetail(long patientId)
        {
            try
            {
                if (HisTreatmentCFG.IS_CHECK_PREVIOUS_PRESCRIPTION_EXAM)
                {
                    long today = Inventec.Common.DateTime.Get.StartDay().Value;
                    string query = "SELECT HSR.REQUEST_ROOM_ID, HSR.SERVICE_REQ_CODE, HSR.TDL_TREATMENT_CODE AS TREATMENT_CODE, HSR.INTRUCTION_TIME, HEMM.USE_TIME_TO, HEMM.MEDICINE_TYPE_CODE, HEMM.MEDICINE_TYPE_NAME FROM HIS_SERVICE_REQ HSR JOIN V_HIS_EXP_MEST_MEDICINE HEMM ON HSR.ID = HEMM.TDL_SERVICE_REQ_ID WHERE HSR.TDL_PATIENT_ID = :param1 AND HSR.SERVICE_REQ_STT_ID = :param2 AND HEMM.USE_TIME_TO >= :param3";
                    List<PreviousPrescriptionDetailResultSDO> data = DAOWorker.SqlDAO.GetSql<PreviousPrescriptionDetailResultSDO>(query, patientId, IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT, today);
                    List<HisPreviousPrescriptionDetailSDO> result = new List<HisPreviousPrescriptionDetailSDO>();
                    if (IsNotNullOrEmpty(data))
                    {
                        var group = data.GroupBy(o => new { o.REQUEST_ROOM_ID, o.SERVICE_REQ_CODE, o.TREATMENT_CODE, o.INTRUCTION_TIME }).ToList();
                        foreach (var g in group)
                        {
                            HisPreviousPrescriptionDetailSDO sdo = new HisPreviousPrescriptionDetailSDO();
                            sdo.REQUEST_ROOM_ID = g.First().REQUEST_ROOM_ID;
                            if (sdo.REQUEST_ROOM_ID > 0)
                            {
                                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == sdo.REQUEST_ROOM_ID).FirstOrDefault();
                                sdo.REQUEST_ROOM_NAME = room != null ? room.ROOM_NAME : null;
                            }
                            sdo.SERVICE_REQ_CODE = g.First().SERVICE_REQ_CODE;
                            sdo.TREATMENT_CODE = g.First().TREATMENT_CODE;
                            sdo.INTRUCTION_TIME = g.First().INTRUCTION_TIME;
                            List<PreviousPrescriptionMedicineSDO> listMedicines = new List<PreviousPrescriptionMedicineSDO>();
                            foreach (var child in g)
                            {
                                if (!string.IsNullOrWhiteSpace(child.MEDICINE_TYPE_CODE))
                                {
                                    PreviousPrescriptionMedicineSDO medicine = new PreviousPrescriptionMedicineSDO();
                                    medicine.MEDICINE_TYPE_CODE = child.MEDICINE_TYPE_CODE;
                                    medicine.MEDICINE_TYPE_NAME = child.MEDICINE_TYPE_NAME;
                                    medicine.USE_TIME_TO = child.USE_TIME_TO;
                                    listMedicines.Add(medicine);
                                }
                            }
                            sdo.ExpMedicines = listMedicines;
                            result.Add(sdo);
                        }
                    }
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        private List<HisPatientSDO> GetByPatientInfo(string patientName, long? genderId, long? dob)
        {
            //Chi truy van neu truyen du ho ten, gioi tinh, ngay sinh
            //de tranh cao tai he thong
            List<HisPatientSDO> result = new List<HisPatientSDO>();
            if (!string.IsNullOrWhiteSpace(patientName) && genderId.HasValue && dob.HasValue)
            {
                string query = "SELECT * FROM D_HIS_PATIENT WHERE VIR_PATIENT_NAME = :param1 AND GENDER_ID = :param2 AND (DOB = :param3 OR VIR_DOB_YEAR = :param4) ORDER BY BHYT_CREATE_TIME DESC ";

                List<D_HIS_PATIENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT>(query, patientName.ToUpper(), genderId.Value, dob.Value, dob.Value.ToString().Substring(0, 4));
                if (IsNotNullOrEmpty(data))
                {
                    foreach (D_HIS_PATIENT tmp in data)
                    {
                        if (!result.Exists(o => o.ID == tmp.ID))
                        {
                            Mapper.CreateMap<D_HIS_PATIENT, HisPatientSDO>();
                            HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);

                            sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                            sdo.HeinAddress = tmp.BHYT_ADDRESS;
                            sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                            sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                            sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                            sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                            sdo.Join5Year = tmp.JOIN_5_YEAR;
                            sdo.Paid6Month = tmp.PAID_6_MONTH;
                            sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                            sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                            sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                            sdo.PHONE = tmp.PHONE;
                            sdo.MOBILE = tmp.MOBILE;
                            sdo.WORK_PLACE = tmp.WORK_PLACE;
                            sdo.WORK_PLACE_ID = tmp.WORK_PLACE_ID;
                            sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                            sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                            sdo.TreatmentId = tmp.TREATMENT_ID;
                            sdo.InDate = tmp.IN_DATE;
                            sdo.IsPause = tmp.IS_PAUSE;
                            result.Add(sdo);
                        }
                    }
                }
            }
            return result;
        }

        private List<HisPatientSDO> GetByPatientPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                List<HisPatientSDO> result = new List<HisPatientSDO>();
                string query = "SELECT * FROM D_HIS_PATIENT WHERE PHONE = :param1 OR MOBILE = :param2 ORDER BY LOG_TIME DESC";

                List<D_HIS_PATIENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT>(query, phoneNumber, phoneNumber);
                if (IsNotNullOrEmpty(data))
                {
                    foreach (D_HIS_PATIENT tmp in data)
                    {
                        if (!result.Exists(o => o.ID == tmp.ID))
                        {
                            Mapper.CreateMap<D_HIS_PATIENT, HisPatientSDO>();
                            HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);

                            sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                            sdo.HeinAddress = tmp.BHYT_ADDRESS;
                            sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                            sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                            sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                            sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                            sdo.Join5Year = tmp.JOIN_5_YEAR;
                            sdo.Paid6Month = tmp.PAID_6_MONTH;
                            sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                            sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                            sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                            sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                            sdo.PHONE = tmp.PHONE;
                            sdo.MOBILE = tmp.MOBILE;
                            sdo.WORK_PLACE = tmp.WORK_PLACE;
                            sdo.WORK_PLACE_ID = tmp.WORK_PLACE_ID;
                            sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                            sdo.TreatmentId = tmp.TREATMENT_ID;
                            sdo.InDate = tmp.IN_DATE;
                            sdo.IsPause = tmp.IS_PAUSE;
                            result.Add(sdo);
                        }
                    }
                }
                return result;
            }
            return null;
        }

        private List<HisPatientSDO> GetByPatientIdentifiedNumber(string cmndNumber, string cccdNumber)
        {
            if (!string.IsNullOrWhiteSpace(cmndNumber) || !string.IsNullOrWhiteSpace(cccdNumber))
            {
                List<HisPatientSDO> result = new List<HisPatientSDO>();

                StringBuilder str = new StringBuilder("SELECT * FROM D_HIS_PATIENT WHERE 1 = 1 ");
                List<string> pars = new List<string>();

                if (IsNotNullOrEmpty(cmndNumber))
                {
                    str.Append(" AND CMND_NUMBER = :param1 ");
                    pars.Add(cmndNumber);
                }
                if (IsNotNullOrEmpty(cccdNumber))
                {
                    str.Append(" AND CCCD_NUMBER = :param2 ");
                    pars.Add(cccdNumber);
                }
                str.Append(" ORDER BY LOG_TIME DESC ");


                List<D_HIS_PATIENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT>(str.ToString(), pars.ToArray());
                if (IsNotNullOrEmpty(data))
                {
                    foreach (D_HIS_PATIENT tmp in data)
                    {
                        if (!result.Exists(o => o.ID == tmp.ID))
                        {
                            Mapper.CreateMap<D_HIS_PATIENT, HisPatientSDO>();
                            HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);

                            sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                            sdo.HeinAddress = tmp.BHYT_ADDRESS;
                            sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                            sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                            sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                            sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                            sdo.Join5Year = tmp.JOIN_5_YEAR;
                            sdo.Paid6Month = tmp.PAID_6_MONTH;
                            sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                            sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                            sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                            sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                            sdo.PHONE = tmp.PHONE;
                            sdo.MOBILE = tmp.MOBILE;
                            sdo.WORK_PLACE = tmp.WORK_PLACE;
                            sdo.WORK_PLACE_ID = tmp.WORK_PLACE_ID;
                            sdo.TransferInCmkt = tmp.TRANSFER_IN_CMKT;
                            sdo.TransferInCode = tmp.TRANSFER_IN_CODE;
                            sdo.TransferInFormId = tmp.TRANSFER_IN_FORM_ID;
                            sdo.TransferInIcdCode = tmp.TRANSFER_IN_ICD_CODE;
                            sdo.TransferInIcdName = tmp.TRANSFER_IN_ICD_NAME;
                            sdo.TransferInMediOrgCode = tmp.TRANSFER_IN_MEDI_ORG_CODE;
                            sdo.TransferInMediOrgName = tmp.TRANSFER_IN_MEDI_ORG_NAME;
                            sdo.TransferInReasonId = tmp.TRANSFER_IN_REASON_ID;
                            sdo.TransferInTimeFrom = tmp.TRANSFER_IN_TIME_FROM;
                            sdo.TransferInTimeTo = tmp.TRANSFER_IN_TIME_TO;
                            sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                            sdo.TreatmentId = tmp.TREATMENT_ID;
                            sdo.InDate = tmp.IN_DATE;
                            sdo.IsPause = tmp.IS_PAUSE;
                            result.Add(sdo);
                        }
                    }
                }
                return result;
            }
            return null;
        }

        private List<HisPatientSDO> GetByPatientCode(string patientCode)
        {
            if (!string.IsNullOrWhiteSpace(patientCode))
            {
                List<HisPatientSDO> result = new List<HisPatientSDO>();
                string query = "SELECT * FROM D_HIS_PATIENT WHERE PATIENT_CODE = :param1 ORDER BY LOG_TIME DESC ";

                List<D_HIS_PATIENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT>(query, patientCode);
                if (IsNotNullOrEmpty(data))
                {
                    D_HIS_PATIENT tmp = data[0];
                    Mapper.CreateMap<D_HIS_PATIENT, HisPatientSDO>();
                    HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
                    sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                    sdo.HeinAddress = tmp.BHYT_ADDRESS;
                    sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                    sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                    sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                    sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                    sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                    sdo.Join5Year = tmp.JOIN_5_YEAR;
                    sdo.Paid6Month = tmp.PAID_6_MONTH;
                    sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                    sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                    sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                    sdo.PHONE = tmp.PHONE;
                    sdo.MOBILE = tmp.MOBILE;
                    sdo.WORK_PLACE = tmp.WORK_PLACE;
                    sdo.WORK_PLACE_ID = tmp.WORK_PLACE_ID;
                    sdo.IS_CHRONIC = tmp.IS_CHRONIC;
                    sdo.IS_TUBERCULOSIS = tmp.IS_TUBERCULOSIS;
                    sdo.TransferInCmkt = tmp.TRANSFER_IN_CMKT;
                    sdo.TransferInCode = tmp.TRANSFER_IN_CODE;
                    sdo.TransferInFormId = tmp.TRANSFER_IN_FORM_ID;
                    sdo.TransferInIcdCode = tmp.TRANSFER_IN_ICD_CODE;
                    sdo.TransferInIcdName = tmp.TRANSFER_IN_ICD_NAME;
                    sdo.TransferInMediOrgCode = tmp.TRANSFER_IN_MEDI_ORG_CODE;
                    sdo.TransferInMediOrgName = tmp.TRANSFER_IN_MEDI_ORG_NAME;
                    sdo.TransferInReasonId = tmp.TRANSFER_IN_REASON_ID;
                    sdo.TransferInTimeFrom = tmp.TRANSFER_IN_TIME_FROM;
                    sdo.TransferInTimeTo = tmp.TRANSFER_IN_TIME_TO;
                    sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                    sdo.TreatmentId = tmp.TREATMENT_ID;
                    sdo.InDate = tmp.IN_DATE;
                    sdo.IsPause = tmp.IS_PAUSE;
                    result.Add(sdo);
                }
                return result;
            }
            return null;
        }

        private List<HisPatientSDO> GetByPatientHrmEmployeeCode(string hrmEmployeeCode)
        {
            if (!string.IsNullOrWhiteSpace(hrmEmployeeCode))
            {
                List<HisPatientSDO> result = new List<HisPatientSDO>();
                string query = "SELECT * FROM D_HIS_PATIENT WHERE HRM_EMPLOYEE_CODE = :param1 ORDER BY LOG_TIME DESC ";

                List<D_HIS_PATIENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT>(query, hrmEmployeeCode);
                if (IsNotNullOrEmpty(data))
                {
                    D_HIS_PATIENT tmp = data[0];
                    Mapper.CreateMap<D_HIS_PATIENT, HisPatientSDO>();
                    HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
                    sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                    sdo.HeinAddress = tmp.BHYT_ADDRESS;
                    sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                    sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                    sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                    sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                    sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                    sdo.Join5Year = tmp.JOIN_5_YEAR;
                    sdo.Paid6Month = tmp.PAID_6_MONTH;
                    sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                    sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                    sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                    sdo.PHONE = tmp.PHONE;
                    sdo.MOBILE = tmp.MOBILE;
                    sdo.WORK_PLACE = tmp.WORK_PLACE;
                    sdo.WORK_PLACE_ID = tmp.WORK_PLACE_ID;
                    sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                    sdo.TreatmentId = tmp.TREATMENT_ID;
                    sdo.InDate = tmp.IN_DATE;
                    sdo.IsPause = tmp.IS_PAUSE;
                    result.Add(sdo);
                }
                return result;
            }
            return null;
        }

        private List<HisPatientSDO> GetByPatientProgramCode(string patientProgramCode)
        {
            List<HisPatientSDO> result = new List<HisPatientSDO>();
            string query = "SELECT * FROM D_HIS_PATIENT_PROGRAM WHERE PATIENT_PROGRAM_CODE = :param1 ORDER BY LOG_TIME DESC ";

            List<D_HIS_PATIENT_PROGRAM> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT_PROGRAM>(query, patientProgramCode);
            if (IsNotNullOrEmpty(data))
            {
                D_HIS_PATIENT_PROGRAM tmp = data[0];
                Mapper.CreateMap<D_HIS_PATIENT_PROGRAM, HisPatientSDO>();
                HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
                sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                sdo.HeinAddress = tmp.BHYT_ADDRESS;
                sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                sdo.Join5Year = tmp.JOIN_5_YEAR;
                sdo.Paid6Month = tmp.PAID_6_MONTH;
                sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                sdo.PatientProgramCode = tmp.PATIENT_PROGRAM_CODE;
                sdo.ProgramId = tmp.PROGRAM_ID;
                result.Add(sdo);
            }
            return result;
        }

        private List<HisPatientSDO> GetByCardCode(string cardCode)
        {
            List<HisPatientSDO> result = new List<HisPatientSDO>();
            string query = "SELECT * FROM D_HIS_PATIENT_CARD WHERE CARD_CODE = :param1 ORDER BY LOG_TIME DESC ";

            List<D_HIS_PATIENT_CARD> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT_CARD>(query, cardCode);
            if (IsNotNullOrEmpty(data))
            {
                D_HIS_PATIENT_CARD tmp = data[0];
                Mapper.CreateMap<D_HIS_PATIENT_CARD, HisPatientSDO>();
                HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
                sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                sdo.HeinAddress = tmp.BHYT_ADDRESS;
                sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                sdo.Join5Year = tmp.JOIN_5_YEAR;
                sdo.Paid6Month = tmp.PAID_6_MONTH;
                sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                sdo.CardCode = tmp.CARD_CODE;
                sdo.CardId = tmp.CARD_ID;
                sdo.TransferInCmkt = tmp.TRANSFER_IN_CMKT;
                sdo.TransferInCode = tmp.TRANSFER_IN_CODE;
                sdo.TransferInFormId = tmp.TRANSFER_IN_FORM_ID;
                sdo.TransferInIcdCode = tmp.TRANSFER_IN_ICD_CODE;
                sdo.TransferInIcdName = tmp.TRANSFER_IN_ICD_NAME;
                sdo.TransferInMediOrgCode = tmp.TRANSFER_IN_MEDI_ORG_CODE;
                sdo.TransferInMediOrgName = tmp.TRANSFER_IN_MEDI_ORG_NAME;
                sdo.TransferInReasonId = tmp.TRANSFER_IN_REASON_ID;
                sdo.TransferInTimeFrom = tmp.TRANSFER_IN_TIME_FROM;
                sdo.TransferInTimeTo = tmp.TRANSFER_IN_TIME_TO;
                sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                sdo.TreatmentId = tmp.TREATMENT_ID;
                sdo.InDate = tmp.IN_DATE;
                sdo.IsPause = tmp.IS_PAUSE;
                result.Add(sdo);
            }
            return result;
        }

        private List<HisPatientSDO> GetByAppointmentCode(string appointmentCode)
        {
            //ma hen kham duoc lay bang ma cua Ho so dieu tri cu~
            string query = "SELECT * FROM D_HIS_PATIENT_TREATMENT WHERE TREATMENT_CODE = :param1 AND TREATMENT_END_TYPE_ID IN (:param2, :param3, :param4) AND APPOINTMENT_TIME IS NOT NULL ORDER BY LOG_TIME DESC ";

            List<D_HIS_PATIENT_TREATMENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT_TREATMENT>(query, appointmentCode, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__RAVIEN, IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__XINRAVIEN);

            return this.QueryByTreatmentCode(data);
        }

        private List<HisPatientSDO> GetByTreatmentCode(string treatmentCode)
        {
            List<HisPatientSDO> result = new List<HisPatientSDO>();

            //ma hen kham duoc lay bang ma cua Ho so dieu tri cu~
            string query = string.Format("SELECT * FROM D_HIS_PATIENT_TREATMENT WHERE TREATMENT_CODE = :param1 ORDER BY LOG_TIME DESC ", treatmentCode);

            List<D_HIS_PATIENT_TREATMENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT_TREATMENT>(query, treatmentCode);

            return this.QueryByTreatmentCode(data);
        }

        private List<HisPatientSDO> QueryByTreatmentCode(List<D_HIS_PATIENT_TREATMENT> data)
        {
            List<HisPatientSDO> result = new List<HisPatientSDO>();

            if (IsNotNullOrEmpty(data))
            {
                D_HIS_PATIENT_TREATMENT tmp = data[0];
                Mapper.CreateMap<D_HIS_PATIENT_TREATMENT, HisPatientSDO>();
                HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
                sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                sdo.HeinAddress = tmp.BHYT_ADDRESS;
                sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                sdo.Join5Year = tmp.JOIN_5_YEAR;
                sdo.Paid6Month = tmp.PAID_6_MONTH;
                sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                sdo.AppointmentCode = tmp.TREATMENT_CODE;
                sdo.IcdCode = tmp.ICD_CODE;
                sdo.IcdName = tmp.ICD_NAME;
                sdo.IcdText = tmp.ICD_TEXT;
                sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;

                sdo.TransferInCmkt = tmp.TRANSFER_IN_CMKT;
                sdo.TransferInCode = tmp.TRANSFER_IN_CODE;
                sdo.TransferInFormId = tmp.TRANSFER_IN_FORM_ID;
                sdo.TransferInIcdCode = tmp.TRANSFER_IN_ICD_CODE;
                sdo.TransferInIcdName = tmp.TRANSFER_IN_ICD_NAME;
                sdo.TransferInMediOrgCode = tmp.TRANSFER_IN_MEDI_ORG_CODE;
                sdo.TransferInMediOrgName = tmp.TRANSFER_IN_MEDI_ORG_NAME;
                sdo.TransferInReasonId = tmp.TRANSFER_IN_REASON_ID;
                sdo.TransferInTimeFrom = tmp.TRANSFER_IN_TIME_FROM;
                sdo.TransferInTimeTo = tmp.TRANSFER_IN_TIME_TO;
                sdo.AppointmentTime = tmp.APPOINTMENT_TIME;
                sdo.AppointmentExamServiceId = tmp.APPOINTMENT_EXAM_SERVICE_ID;
                sdo.NextExamNumOrder = tmp.NEXT_EXAM_NUM_ORDER;
                sdo.NumOrderIssueId = tmp.NUM_ORDER_ISSUE_ID;
                sdo.NextExamFromTime = tmp.NEXT_EXAM_FROM_TIME;
                sdo.NextExamToTime = tmp.NEXT_EXAM_TO_TIME;
                sdo.PHONE = tmp.PHONE;
                sdo.MOBILE = tmp.MOBILE;
                sdo.WORK_PLACE = tmp.WORK_PLACE;
                sdo.WORK_PLACE_ID = tmp.WORK_PLACE_ID;
                sdo.TreatmentId = tmp.TREATMENT_ID;
                sdo.InDate = tmp.IN_DATE;
                sdo.IsPause = tmp.IS_PAUSE;
                sdo.IS_TUBERCULOSIS = tmp.IS_TUBERCULOSIS;
                sdo.IS_CHRONIC = tmp.IS_CHRONIC;

                if (!string.IsNullOrWhiteSpace(tmp.APPOINTMENT_EXAM_ROOM_IDS))
                {
                    string[] idStr = tmp.APPOINTMENT_EXAM_ROOM_IDS.Split(',');

                    if (idStr != null && idStr.Length > 0)
                    {
                        List<long> ids = new List<long>();
                        foreach (string i in idStr)
                        {
                            try
                            {
                                long id = long.Parse(i);
                                ids.Add(id);
                            }
                            catch (Exception ex)
                            {
                                LogSystem.Warn(ex);
                            }
                        }
                        sdo.AppointmentExamRoomIds = ids;
                    }
                }

                result.Add(sdo);
            }
            return result;
        }

        private List<HisPatientSDO> GetByHeinCardNumber(string heinCardNumber)
        {
            List<HisPatientSDO> result = new List<HisPatientSDO>();
            string query = "SELECT * FROM D_HIS_PATIENT WHERE HEIN_CARD_NUMBER = :param1 ORDER BY LOG_TIME DESC";

            List<D_HIS_PATIENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT>(query, heinCardNumber);
            if (IsNotNullOrEmpty(data))
            {
                D_HIS_PATIENT tmp = data[0];
                Mapper.CreateMap<D_HIS_PATIENT, HisPatientSDO>();
                HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
                sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                sdo.HeinAddress = tmp.BHYT_ADDRESS;
                sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                sdo.Join5Year = tmp.JOIN_5_YEAR;
                sdo.Paid6Month = tmp.PAID_6_MONTH;
                sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                sdo.TransferInCmkt = tmp.TRANSFER_IN_CMKT;
                sdo.TransferInCode = tmp.TRANSFER_IN_CODE;
                sdo.TransferInFormId = tmp.TRANSFER_IN_FORM_ID;
                sdo.TransferInIcdCode = tmp.TRANSFER_IN_ICD_CODE;
                sdo.TransferInIcdName = tmp.TRANSFER_IN_ICD_NAME;
                sdo.TransferInMediOrgCode = tmp.TRANSFER_IN_MEDI_ORG_CODE;
                sdo.TransferInMediOrgName = tmp.TRANSFER_IN_MEDI_ORG_NAME;
                sdo.TransferInReasonId = tmp.TRANSFER_IN_REASON_ID;
                sdo.TransferInTimeFrom = tmp.TRANSFER_IN_TIME_FROM;
                sdo.TransferInTimeTo = tmp.TRANSFER_IN_TIME_TO;
                sdo.PHONE = tmp.PHONE;
                sdo.MOBILE = tmp.MOBILE;
                sdo.WORK_PLACE = tmp.WORK_PLACE;
                sdo.WORK_PLACE_ID = tmp.WORK_PLACE_ID;
                sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                sdo.TreatmentId = tmp.TREATMENT_ID;
                sdo.InDate = tmp.IN_DATE;
                sdo.IsPause = tmp.IS_PAUSE;

                result.Add(sdo);
            }
            return result;
        }

        private List<PreviousDebtTreatmentSDO> GetPreviousDebtTreatment(long patientId, ref V_HIS_TREATMENT_FEE_4 lastTreatmentFee)
        {
            if (HisTreatmentCFG.CHECK_PREVIOUS_DEBT_OPTION == HisTreatmentCFG.CheckPreviousDebtOption.WARNING
                || HisTreatmentCFG.CHECK_PREVIOUS_DEBT_OPTION == HisTreatmentCFG.CheckPreviousDebtOption.WARNING_WITH_BHYT)
            {
                string query = "SELECT DISTINCT TDL_TREATMENT_CODE, PATIENT_TYPE_ID FROM HIS_SERE_SERV S WHERE NOT EXISTS(SELECT 1 FROM HIS_SERE_SERV_BILL B WHERE B.SERE_SERV_ID = S.ID AND B.IS_CANCEL IS NULL) AND S.IS_DELETE = 0 AND S.SERVICE_REQ_ID IS NOT NULL AND S.VIR_TOTAL_PATIENT_PRICE > 0 AND TDL_PATIENT_ID = :param1 ";
                List<PreviousDebtTreatmentSDO> data = DAOWorker.SqlDAO.GetSql<PreviousDebtTreatmentSDO>(query, patientId);

                if (HisTreatmentCFG.CHECK_PREVIOUS_DEBT_OPTION == HisTreatmentCFG.CheckPreviousDebtOption.WARNING_WITH_BHYT)
                {
                    string query2 = "SELECT DISTINCT TREAT.TREATMENT_CODE TDL_TREATMENT_CODE, TREAT.TDL_PATIENT_TYPE_ID PATIENT_TYPE_ID FROM HIS_TREATMENT TREAT WHERE TREAT.IS_ACTIVE = 1 AND TREAT.PATIENT_ID = :param1";
                    List<PreviousDebtTreatmentSDO> dataTreatmentIsActive = DAOWorker.SqlDAO.GetSql<PreviousDebtTreatmentSDO>(query2, patientId);
                    if (IsNotNullOrEmpty(dataTreatmentIsActive))
                    {
                        data.AddRange(dataTreatmentIsActive);
                        data = data.GroupBy(g => new { g.TDL_TREATMENT_CODE, g.PATIENT_TYPE_ID }).Select(s => s.First()).ToList();
                    }
                }

                return data;
            }
            else if (HisTreatmentCFG.CHECK_PREVIOUS_DEBT_OPTION == HisTreatmentCFG.CheckPreviousDebtOption.WARNING_AS_FIRST)
            {
                string query3 = "SELECT * FROM V_HIS_TREATMENT_FEE_4 WHERE PATIENT_ID = :param1 ORDER BY IN_TIME DESC FETCH FIRST ROWS ONLY";
                lastTreatmentFee = DAOWorker.SqlDAO.GetSqlSingle<V_HIS_TREATMENT_FEE_4>(query3, patientId);
            }
            else if (HisTreatmentCFG.CHECK_PREVIOUS_DEBT_OPTION == HisTreatmentCFG.CheckPreviousDebtOption.WARNING_HAVE_TO_PAY_MORE)
            {
                string query4 = "SELECT DISTINCT TFEE.TREATMENT_CODE TDL_TREATMENT_CODE, TFEE.TDL_PATIENT_TYPE_ID PATIENT_TYPE_ID FROM V_HIS_TREATMENT_FEE TFEE WHERE (TFEE.IS_ACTIVE = 1 OR (TFEE.TOTAL_PATIENT_PRICE - TFEE.TOTAL_DEPOSIT_AMOUNT - TFEE.TOTAL_BILL_AMOUNT + TFEE.TOTAL_BILL_TRANSFER_AMOUNT + TFEE.TOTAL_REPAY_AMOUNT) > 0) AND TFEE.PATIENT_ID = :param1";
                List<PreviousDebtTreatmentSDO> data = DAOWorker.SqlDAO.GetSql<PreviousDebtTreatmentSDO>(query4, patientId);

                return data;
            }
            return null;
        }

        private List<HisPatientSDO> GetByServiceCode(string serviceCode)
        {
            List<HisPatientSDO> result = new List<HisPatientSDO>();
            string query = "SELECT * FROM D_HIS_PATIENT_CARD WHERE SERVICE_CODE = :param1 ORDER BY LOG_TIME DESC ";

            List<D_HIS_PATIENT_CARD> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT_CARD>(query, serviceCode);
            if (IsNotNullOrEmpty(data))
            {
                D_HIS_PATIENT_CARD tmp = data[0];
                Mapper.CreateMap<D_HIS_PATIENT_CARD, HisPatientSDO>();
                HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(tmp);
                sdo.HeinCardNumber = tmp.HEIN_CARD_NUMBER;
                sdo.HeinAddress = tmp.BHYT_ADDRESS;
                sdo.HeinCardFromTime = tmp.HEIN_CARD_FROM_TIME;
                sdo.HeinCardToTime = tmp.HEIN_CARD_TO_TIME;
                sdo.HeinMediOrgCode = tmp.HEIN_MEDI_ORG_CODE;
                sdo.HeinMediOrgName = tmp.HEIN_MEDI_ORG_NAME;
                sdo.HasBirthCertificate = tmp.HAS_BIRTH_CERTIFICATE;
                sdo.Join5Year = tmp.JOIN_5_YEAR;
                sdo.Paid6Month = tmp.PAID_6_MONTH;
                sdo.RightRouteCode = tmp.RIGHT_ROUTE_CODE;
                sdo.RightRouteTypeCode = tmp.RIGHT_ROUTE_TYPE_CODE;
                sdo.LiveAreaCode = tmp.LIVE_AREA_CODE;
                sdo.CardCode = tmp.CARD_CODE;
                sdo.CardId = tmp.CARD_ID;
                sdo.TransferInCmkt = tmp.TRANSFER_IN_CMKT;
                sdo.TransferInCode = tmp.TRANSFER_IN_CODE;
                sdo.TransferInFormId = tmp.TRANSFER_IN_FORM_ID;
                sdo.TransferInIcdCode = tmp.TRANSFER_IN_ICD_CODE;
                sdo.TransferInIcdName = tmp.TRANSFER_IN_ICD_NAME;
                sdo.TransferInMediOrgCode = tmp.TRANSFER_IN_MEDI_ORG_CODE;
                sdo.TransferInMediOrgName = tmp.TRANSFER_IN_MEDI_ORG_NAME;
                sdo.TransferInReasonId = tmp.TRANSFER_IN_REASON_ID;
                sdo.TransferInTimeFrom = tmp.TRANSFER_IN_TIME_FROM;
                sdo.TransferInTimeTo = tmp.TRANSFER_IN_TIME_TO;
                sdo.TreatmentTypeId = tmp.TDL_TREATMENT_TYPE_ID;
                sdo.TreatmentId = tmp.TREATMENT_ID;
                sdo.InDate = tmp.IN_DATE;
                sdo.IsPause = tmp.IS_PAUSE;
                result.Add(sdo);
            }
            return result;
        }

        private List<HisPatientSDO> GetByHeinCardNumberOrCccdNumber(string heinCardNumber, string cccdNumber)
        {
            if (!string.IsNullOrWhiteSpace(heinCardNumber) || !string.IsNullOrWhiteSpace(cccdNumber))
            {
                List<HisPatientSDO> result = new List<HisPatientSDO>();

                
                List<string> pars = new List<string>();

                StringBuilder str = new StringBuilder("SELECT * FROM (");
                if (IsNotNullOrEmpty(heinCardNumber))
                {
                    str.Append("SELECT * FROM D_HIS_PATIENT WHERE HEIN_CARD_NUMBER = :param1 ");
                    pars.Add(heinCardNumber);
                }
                if (IsNotNullOrEmpty(heinCardNumber) && IsNotNullOrEmpty(cccdNumber))
                {
                    str.Append(" UNION ALL ");
                }
                if (IsNotNullOrEmpty(cccdNumber))
                {
                    str.Append("SELECT * FROM D_HIS_PATIENT WHERE CCCD_NUMBER = :param2 ");
                    pars.Add(cccdNumber);
                }
                str.Append(") ORDER BY LOG_TIME DESC");


                List<D_HIS_PATIENT> data = DAOWorker.SqlDAO.GetSql<D_HIS_PATIENT>(str.ToString(), pars.ToArray());

                if (IsNotNullOrEmpty(data))
                {
                    D_HIS_PATIENT patient = new D_HIS_PATIENT();
                    patient = data.Where(o => o.HEIN_CARD_NUMBER != null).OrderByDescending(P => P.PATIENT_CODE).FirstOrDefault();
                    if (patient == null)
                    {
                        patient = data.OrderByDescending(P => P.PATIENT_CODE).FirstOrDefault();
                    }
                    if (patient != null)
                    {
                        Mapper.CreateMap<D_HIS_PATIENT, HisPatientSDO>();
                        HisPatientSDO sdo = Mapper.Map<HisPatientSDO>(patient);

                        sdo.HeinCardNumber = patient.HEIN_CARD_NUMBER;
                        sdo.HeinAddress = patient.BHYT_ADDRESS;
                        sdo.HeinCardFromTime = patient.HEIN_CARD_FROM_TIME;
                        sdo.HeinCardToTime = patient.HEIN_CARD_TO_TIME;
                        sdo.HeinMediOrgCode = patient.HEIN_MEDI_ORG_CODE;
                        sdo.HeinMediOrgName = patient.HEIN_MEDI_ORG_NAME;
                        sdo.Join5Year = patient.JOIN_5_YEAR;
                        sdo.Paid6Month = patient.PAID_6_MONTH;
                        sdo.RightRouteCode = patient.RIGHT_ROUTE_CODE;
                        sdo.RightRouteTypeCode = patient.RIGHT_ROUTE_TYPE_CODE;
                        sdo.LiveAreaCode = patient.LIVE_AREA_CODE;
                        sdo.HasBirthCertificate = patient.HAS_BIRTH_CERTIFICATE;
                        sdo.PHONE = patient.PHONE;
                        sdo.MOBILE = patient.MOBILE;
                        sdo.WORK_PLACE = patient.WORK_PLACE;
                        sdo.WORK_PLACE_ID = patient.WORK_PLACE_ID;
                        sdo.TransferInCmkt = patient.TRANSFER_IN_CMKT;
                        sdo.TransferInCode = patient.TRANSFER_IN_CODE;
                        sdo.TransferInFormId = patient.TRANSFER_IN_FORM_ID;
                        sdo.TransferInIcdCode = patient.TRANSFER_IN_ICD_CODE;
                        sdo.TransferInIcdName = patient.TRANSFER_IN_ICD_NAME;
                        sdo.TransferInMediOrgCode = patient.TRANSFER_IN_MEDI_ORG_CODE;
                        sdo.TransferInMediOrgName = patient.TRANSFER_IN_MEDI_ORG_NAME;
                        sdo.TransferInReasonId = patient.TRANSFER_IN_REASON_ID;
                        sdo.TransferInTimeFrom = patient.TRANSFER_IN_TIME_FROM;
                        sdo.TransferInTimeTo = patient.TRANSFER_IN_TIME_TO;
                        sdo.TreatmentTypeId = patient.TDL_TREATMENT_TYPE_ID;
                        sdo.TreatmentId = patient.TREATMENT_ID;
                        sdo.InDate = patient.IN_DATE;
                        sdo.IsPause = patient.IS_PAUSE;
                        result.Add(sdo);
                    }
                }
                return result;
            }
            return null;
        }

    }
}
