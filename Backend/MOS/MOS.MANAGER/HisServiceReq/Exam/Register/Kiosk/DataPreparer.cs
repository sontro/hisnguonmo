using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;
using MOS.LibraryHein.Bhyt.HeinJoin5Year;
using MOS.LibraryHein.Bhyt.HeinPaid6Month;
using MOS.LibraryHein.Bhyt.HeinRightRoute;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisServiceFollow;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.ServicePaty;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Kiosk
{
    class DataPreparer : BusinessBase
    {
        internal DataPreparer(CommonParam param)
            : base(param)
        {
        }

        internal HisServiceReqExamRegisterSDO ToRegisterKioskSdo(HisExamRegisterKioskSDO sdo)
        {
            HisServiceReqExamRegisterSDO result = null;
            WorkPlaceSDO workPlace = null;
            if (sdo != null && sdo.CardSDO != null && this.HasWorkPlaceInfo(sdo.RequestRoomId, ref workPlace))
            {
                long treatmentTime = Inventec.Common.DateTime.Get.Now().Value;

                HIS_BRANCH hisBranch = new TokenManager(param).GetBranch();
                HIS_TREATMENT treatment = new HIS_TREATMENT();
                V_HIS_SERVICE vservice = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == sdo.ServiceId);



                HIS_PATIENT_TYPE_ALTER hisPatientTypeAlter = new HIS_PATIENT_TYPE_ALTER();
                hisPatientTypeAlter.PATIENT_TYPE_ID = sdo.PatientTypeId;
                hisPatientTypeAlter.TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM;//mac dinh la kham
                hisPatientTypeAlter.LOG_TIME = treatmentTime;
                hisPatientTypeAlter.EXECUTE_ROOM_ID = sdo.RequestRoomId;

                if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.AUTO)
                {
                    hisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID = sdo.PrimaryPatientTypeId;
                }
                else if (HisSereServCFG.SET_PRIMARY_PATIENT_TYPE == HisSereServCFG.SetPrimaryPatientType.YES)
                {
                    if (vservice.BILL_PATIENT_TYPE_ID.HasValue && IsValidAppliedPatientTypeIds(vservice, sdo.PatientTypeId))
                    {
                        hisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID = vservice.BILL_PATIENT_TYPE_ID;
                    }
                }
                if (sdo.PatientTypeId == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    hisPatientTypeAlter.ADDRESS = sdo.CardSDO.HeinAddress;
                    hisPatientTypeAlter.FREE_CO_PAID_TIME = sdo.CardSDO.FreeCoPaidTime;
                    hisPatientTypeAlter.HEIN_CARD_FROM_TIME = sdo.CardSDO.HeinCardFromTime;
                    hisPatientTypeAlter.HEIN_CARD_NUMBER = sdo.CardSDO.HeinCardNumber;
                    hisPatientTypeAlter.HEIN_CARD_TO_TIME = sdo.CardSDO.HeinCardToTime;
                    hisPatientTypeAlter.HEIN_MEDI_ORG_CODE = sdo.CardSDO.HeinOrgCode;
                    hisPatientTypeAlter.HEIN_MEDI_ORG_NAME = sdo.CardSDO.HeinOrgName;
                    hisPatientTypeAlter.JOIN_5_YEAR = sdo.CardSDO.Join5Year;
                    hisPatientTypeAlter.LEVEL_CODE = hisBranch.HEIN_LEVEL_CODE;
                    hisPatientTypeAlter.LIVE_AREA_CODE = sdo.CardSDO.LiveAreaCode;
                    hisPatientTypeAlter.RIGHT_ROUTE_CODE = sdo.RightRouteCode;

                    //Neu dung noi KCB ban dau hoac BV la tuyen huyen hoac tuyen xa thi la dung tuyen, 
                    //con lai la trai tuyen (vi thuc te se ko co cac hinh thuc cap cuu, gioi thieu neu dang ky qua kiosk)
                    //hisPatientTypeAlter.RIGHT_ROUTE_CODE = hisBranch.HEIN_MEDI_ORG_CODE == sdo.CardSDO.HeinOrgCode || hisBranch.HEIN_LEVEL_CODE == HeinLevelCode.COMMUNE || hisBranch.HEIN_LEVEL_CODE == HeinLevelCode.DISTRICT ? HeinRightRouteCode.TRUE : HeinRightRouteCode.FALSE;
                    hisPatientTypeAlter.JOIN_5_YEAR = sdo.CardSDO.Join5Year == HeinJoin5YearCode.TRUE ? HeinJoin5YearCode.TRUE : HeinJoin5YearCode.FALSE;
                    hisPatientTypeAlter.PAID_6_MONTH = sdo.CardSDO.Paid6Month == HeinPaid6MonthCode.TRUE ? HeinPaid6MonthCode.TRUE : HeinPaid6MonthCode.FALSE;
                    hisPatientTypeAlter.RIGHT_ROUTE_TYPE_CODE = sdo.RightRouteTypeCode;
                    hisPatientTypeAlter.HAS_BIRTH_CERTIFICATE = HeinHasBirthCertificateCode.FALSE; //BN dang ky kiosk se ko co giay chung sinh

                    treatment.TRANSFER_IN_CODE = sdo.TransferInCode;
                    treatment.TRANSFER_IN_ICD_CODE = sdo.TransferInIcdCode;
                    treatment.TRANSFER_IN_ICD_NAME = sdo.TransferInIcdName;
                    treatment.TRANSFER_IN_MEDI_ORG_CODE = sdo.TransferInMediOrgCode;
                    treatment.TRANSFER_IN_MEDI_ORG_NAME = sdo.TransferInMediOrgName;
                    treatment.IS_TRANSFER_IN = (sdo.IsTransferIn.HasValue && sdo.IsTransferIn.Value) ? new Nullable<short>(Constant.IS_TRUE) : null;
                    treatment.TRANSFER_IN_CMKT = sdo.TransferInCmkt;
                    treatment.TRANSFER_IN_FORM_ID = sdo.TransferInFormId;
                    treatment.TRANSFER_IN_REASON_ID = sdo.TransferInReasonId;
                    treatment.TRANSFER_IN_TIME_FROM = sdo.TransferInTimeFrom;
                    treatment.TRANSFER_IN_TIME_TO = sdo.TransferInTimeTo;
                }

                HIS_PATIENT hisPatient = null;

                if (sdo.CardSDO.PatientId.HasValue)
                {
                    hisPatient = new HisPatientGet().GetById(sdo.CardSDO.PatientId.Value);
                    if (sdo.CardSDO.CccdDate.HasValue)
                        hisPatient.CCCD_DATE = sdo.CardSDO.CccdDate;
                    if (!string.IsNullOrWhiteSpace(sdo.CardSDO.CccdNumber))
                        hisPatient.CCCD_NUMBER = sdo.CardSDO.CccdNumber;
                    if (!string.IsNullOrWhiteSpace(sdo.CardSDO.CccdPlace))
                        hisPatient.CCCD_PLACE = sdo.CardSDO.CccdPlace;
                }
                else
                {
                    hisPatient = new HIS_PATIENT();
                    hisPatient.ADDRESS = sdo.CardSDO.Address;
                    hisPatient.CAREER_ID = sdo.CardSDO.CareerId;
                    HIS_CAREER hisCareer = HisCareerCFG.DATA.FirstOrDefault(o => o.ID == sdo.CardSDO.CareerId);
                    hisPatient.CAREER_CODE = hisCareer != null ? hisCareer.CAREER_CODE : null;
                    hisPatient.CAREER_NAME = hisCareer != null ? hisCareer.CAREER_NAME : null;
                    hisPatient.COMMUNE_NAME = sdo.CardSDO.CommuneName;
                    hisPatient.DISTRICT_NAME = sdo.CardSDO.DistrictName;
                    hisPatient.DOB = sdo.CardSDO.Dob;
                    hisPatient.EMAIL = sdo.CardSDO.Email;
                    hisPatient.ETHNIC_NAME = sdo.CardSDO.EthnicName;
                    hisPatient.FIRST_NAME = sdo.CardSDO.FirstName;
                    hisPatient.GENDER_ID = sdo.CardSDO.GenderId;
                    hisPatient.LAST_NAME = sdo.CardSDO.LastName;
                    hisPatient.NATIONAL_NAME = sdo.CardSDO.NationalName;
                    hisPatient.PHONE = sdo.CardSDO.Phone;
                    hisPatient.PROVINCE_NAME = sdo.CardSDO.ProvinceName;
                    hisPatient.RELIGION_NAME = sdo.CardSDO.ReligionName;
                    hisPatient.WORK_PLACE = sdo.CardSDO.WorkPlace;
                    hisPatient.CCCD_DATE = sdo.CardSDO.CccdDate;
                    hisPatient.CCCD_NUMBER = sdo.CardSDO.CccdNumber;
                    hisPatient.CCCD_PLACE = sdo.CardSDO.CccdPlace;
                    hisPatient.PROVINCE_CODE = sdo.CardSDO.ProvinceCode;
                    hisPatient.DISTRICT_CODE = sdo.CardSDO.DistrictCode;
                   
                }
              
                if (sdo.IsChronic.HasValue && sdo.IsChronic.Value)
                {
                    hisPatient.IS_CHRONIC = Constant.IS_TRUE;
                    treatment.IS_CHRONIC = Constant.IS_TRUE;
                }
                else
                {
                    hisPatient.IS_CHRONIC = null;
                    treatment.IS_CHRONIC = null;
                }
                HisPatientProfileSDO patientProfile = new HisPatientProfileSDO();
                patientProfile.HisPatient = hisPatient;
                patientProfile.HisPatientTypeAlter = hisPatientTypeAlter;
                patientProfile.TreatmentTime = treatmentTime;
                patientProfile.HisTreatment = treatment;
                patientProfile.ProvinceCode = hisPatient.PROVINCE_CODE;
                patientProfile.DistrictCode = hisPatient.DISTRICT_CODE;

                patientProfile.RequestRoomId = sdo.RequestRoomId;
                patientProfile.CardCode = sdo.CardSDO != null ? sdo.CardSDO.CardCode : null;

                if (!String.IsNullOrWhiteSpace(patientProfile.CardCode))
                {
                    hisPatient.HAS_CARD = Constant.IS_TRUE;
                    treatment.HAS_CARD = Constant.IS_TRUE;
                }
              
                V_HIS_ROOM room = HisRoomCFG.DATA != null ? HisRoomCFG.DATA.Where(o => o.ID == sdo.RoomId).FirstOrDefault() : null;
                if (room != null)
                {
                    patientProfile.DepartmentId = room.DEPARTMENT_ID;
                }

                result = new HisServiceReqExamRegisterSDO();
                result.InstructionTimes = new List<long>() { treatmentTime };
                result.InstructionTime = treatmentTime;
                result.IsNotRequireFee = sdo.IsNotRequireFee ? (short?)Constant.IS_TRUE : null;
               
                
                ServiceReqDetailSDO service = new ServiceReqDetailSDO();
                service.Amount = 1;
                service.ServiceId = sdo.ServiceId;
                service.PatientTypeId = sdo.PatientTypeId;
                service.RoomId = sdo.RoomId;
                service.PrimaryPatientTypeId = hisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID;
                result.ServiceReqDetails = new List<ServiceReqDetailSDO>() { service };
                if (IsNotNullOrEmpty(sdo.AdditionalServices))
                {
                    result.ServiceReqDetails.AddRange(sdo.AdditionalServices);
                }

                result.HisPatientProfile = patientProfile;
                result.RequestRoomId = sdo.RequestRoomId;
                result.Priority = sdo.IsPriority ? (long?)Constant.IS_TRUE : null;
               
            }
            return result;
        }

        internal HisServiceReqExamRegisterSDO ToRegisterKioskSdo(HisRegisterKioskSDO sdo)
        {
            HisServiceReqExamRegisterSDO result = null;
            if (sdo != null)
            {
                long treatmentTime = Inventec.Common.DateTime.Get.Now().Value;
                HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.PATIENT_TYPE_CODE == sdo.PatientTypeCode).FirstOrDefault();
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ROOM_CODE == sdo.RoomCode).FirstOrDefault();
                V_HIS_ROOM requestRoom = HisRoomCFG.DATA.Where(o => o.ROOM_CODE == sdo.RequestRoomCode && o.ROOM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_ROOM_TYPE.ID__TD).FirstOrDefault();
                V_HIS_SERVICE hisService = HisServiceCFG.DATA_VIEW.Where(o => o.SERVICE_CODE == sdo.ServiceCode).FirstOrDefault();
                HisPatientFilterQuery filter = new HisPatientFilterQuery();
                filter.PATIENT_CODE__EXACT = String.Format("{0:0000000000}", int.Parse(sdo.PatientCode));
                List<HIS_PATIENT> patients = new HisPatientGet().Get(filter);
                HIS_PATIENT hisPatient = IsNotNullOrEmpty(patients) ? patients[0] : null;

                if (hisPatient == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Benh nhan ko ton tai");
                }
                if (room == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phong kham ko ton tai");
                }
                if (requestRoom == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Benh nhan ko ton tai");
                }
                if (hisService == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Dich vu kham ko ton tai");
                }
                if (patientType == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Doi tuong benh nhan ko ton tai");
                }

                HIS_BRANCH hisBranch = new TokenManager(param).GetBranch();
                HIS_TREATMENT treatment = new HIS_TREATMENT();

                HIS_PATIENT_TYPE_ALTER hisPatientTypeAlter = new HIS_PATIENT_TYPE_ALTER();
                hisPatientTypeAlter.PATIENT_TYPE_ID = patientType.ID;
                hisPatientTypeAlter.TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM;//mac dinh la kham
                hisPatientTypeAlter.LOG_TIME = treatmentTime;
                hisPatientTypeAlter.EXECUTE_ROOM_ID = requestRoom.ID;

                if (hisPatientTypeAlter.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                {
                    hisPatientTypeAlter.ADDRESS = sdo.HeinAddress;
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (!string.IsNullOrWhiteSpace(sdo.FreeCoPaidTime))
                    {
                        DateTime freeCoPaidTime = DateTime.ParseExact(sdo.FreeCoPaidTime, "dd/MM/yyyy HH:mm:ss", provider);
                        hisPatientTypeAlter.FREE_CO_PAID_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(freeCoPaidTime);
                    }
                    if (!string.IsNullOrWhiteSpace(sdo.HeinCardFromTime))
                    {
                        DateTime heinCardFromTime = DateTime.ParseExact(sdo.HeinCardFromTime, "dd/MM/yyyy HH:mm:ss", provider);
                        hisPatientTypeAlter.HEIN_CARD_FROM_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(heinCardFromTime);
                    }
                    if (!string.IsNullOrWhiteSpace(sdo.HeinCardToTime))
                    {
                        DateTime heinCardToTime = DateTime.ParseExact(sdo.HeinCardToTime, "dd/MM/yyyy HH:mm:ss", provider);
                        hisPatientTypeAlter.HEIN_CARD_TO_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(heinCardToTime);
                    }
                    hisPatientTypeAlter.HEIN_CARD_NUMBER = sdo.HeinCardNumber;
                    hisPatientTypeAlter.HEIN_MEDI_ORG_CODE = sdo.HeinOrgCode;
                    hisPatientTypeAlter.HEIN_MEDI_ORG_NAME = sdo.HeinOrgName;
                    hisPatientTypeAlter.JOIN_5_YEAR = sdo.IsJoin5Year ? HeinJoin5YearCode.TRUE : HeinJoin5YearCode.FALSE;
                    hisPatientTypeAlter.LEVEL_CODE = hisBranch.HEIN_LEVEL_CODE;
                    hisPatientTypeAlter.LIVE_AREA_CODE = sdo.LiveAreaCode;
                    hisPatientTypeAlter.PAID_6_MONTH = sdo.IsPaid6Month ? HeinPaid6MonthCode.TRUE : HeinPaid6MonthCode.FALSE;
                    hisPatientTypeAlter.RIGHT_ROUTE_CODE = HeinRightRouteCode.TRUE; //luon la dung tuyen

                }

                HisPatientProfileSDO patientProfile = new HisPatientProfileSDO();
                patientProfile.HisPatient = hisPatient;
                patientProfile.HisPatientTypeAlter = hisPatientTypeAlter;
                patientProfile.TreatmentTime = treatmentTime;
                patientProfile.HisTreatment = treatment;
                patientProfile.ProvinceCode = hisPatient.PROVINCE_CODE;
                patientProfile.DistrictCode = hisPatient.DISTRICT_CODE;


                patientProfile.RequestRoomId = requestRoom.ID;
                patientProfile.DepartmentId = room.DEPARTMENT_ID;

                result = new HisServiceReqExamRegisterSDO();
                result.InstructionTimes = new List<long>() { treatmentTime };
                result.InstructionTime = treatmentTime;

                ServiceReqDetailSDO service = new ServiceReqDetailSDO();
                service.Amount = 1;
                service.ServiceId = hisService.ID;
                service.PatientTypeId = patientType.ID;
                service.RoomId = room.ID;
                service.PrimaryPatientTypeId = hisPatientTypeAlter.PRIMARY_PATIENT_TYPE_ID;
                result.ServiceReqDetails = new List<ServiceReqDetailSDO>() { service };
                result.HisPatientProfile = patientProfile;
                result.RequestRoomId = requestRoom.ID;
                result.Priority = sdo.IsPriority ? (long?)Constant.IS_TRUE : null;
            }
            return result;
        }

        private bool IsValidAppliedPatientTypeIds(V_HIS_SERVICE vservice, long? patientTypeId)
        {
            bool valid = false;
            try
            {
                if (vservice == null)
                    return false;
                if (String.IsNullOrWhiteSpace(vservice.APPLIED_PATIENT_TYPE_IDS))
                {
                    valid = true;
                }
                else if (patientTypeId > 0)
                {
                    if (("," + vservice.APPLIED_PATIENT_TYPE_IDS + ",").Contains("," + patientTypeId.ToString() + ","))
                        valid = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
