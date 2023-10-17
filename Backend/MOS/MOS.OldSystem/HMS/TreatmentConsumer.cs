using Inventec.Common.Logging;
using MOS.OldSystem.HmsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class TreatmentConsumer
    {
        private const string OK = "OK";

        private IeMRMainServiceClient client;

        public TreatmentConsumer (string baseUri)
        {
            this.client = new IeMRMainServiceClient("WSHttpBinding_IeMRMainService", baseUri);
        }

        /// <summary>
        /// Gui y/c Tao ho so cua BN cu sang he thong HMS
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Create(OldPatientTreatmentData data)
        {
            bool result = false;
            string input = "";
            if (data != null)
            {
                try
                {
                    int isLine = data.IsRightRoute ? 1 : 0;
                    int hospital = !string.IsNullOrWhiteSpace(data.TransferHeinOrgCode) ? int.Parse(data.TransferHeinOrgCode) : 0;
                    string cardNumber = !string.IsNullOrWhiteSpace(data.CardNumber) ? data.CardNumber + data.HeinOrgCode : "0"; //do thiet ke cua HMS, luu them duoi la noi KCBBD
                    string cardFromDate = !string.IsNullOrWhiteSpace(data.CardFromDate) ? data.CardFromDate : "1/1/2001";
                    string cardToDate = !string.IsNullOrWhiteSpace(data.CardToDate) ? data.CardToDate : "1/1/2001";

                    input = LogParamUtil.LogContent("PatID", data.PatientCode, "RoomExamID", data.ExamRoomCode, "ServiceID", data.ExamStyleId.ToString(), "isLine", isLine, "isDoubleExam", 0, "CardCode", cardNumber, "InsFrom", cardFromDate, "InsTo", cardToDate, "Hospital", hospital, "ICD", data.TransferIcdCode, "ListExam", "", "Login_name", data.Creator, "MedID", data.TreatmentCode);

                    string rs = this.client.srv_hms_AddNewReception(
                        data.PatientCode,
                        data.ExamRoomCode,
                        data.ExamStyleId.ToString(),
                        isLine,
                        0,
                        cardNumber,
                        cardFromDate,
                        cardToDate,
                        hospital,
                        data.TransferIcdCode,
                        "",
                        data.Creator,
                        data.TreatmentCode);

                    result = rs == OK;

                    if (!result)
                    {
                        LogSystem.Warn("Tao ho so cua BN cu sang he thong HMS that bai. Input: " + input + " Output: " + rs);
                    }
                    else
                    {
                        LogSystem.Info("Tao ho so cua BN cu sang he thong HMS thanh cong. Input: " + input + " Output: " + rs);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error("Tao ho so cua BN cu sang he thong HMS that bai. Input: " + input);
                    LogSystem.Error(ex);
                    result = false;
                }
            }
            return result;
        }

        public bool Create(NewBhytPatientTreatmentData data)
        {
            bool result = false;
            string input = "";
            if (data != null)
            {
                try
                {
                    int isLine = data.IsRightRoute ? 1 : 0;
                    int hospital = int.Parse(data.HeinOrgCode);
                    string cardNumber = data.CardNumber + data.HeinOrgCode; //do thiet ke cua HMS, luu them duoi la noi KCBBD
                    int hospital1 = !string.IsNullOrWhiteSpace(data.TransferHeinOrgCode) ? int.Parse(data.TransferHeinOrgCode) : 0;
                    int sex = data.IsMale ? 1 : 0;
                    int isMergency = data.IsEmergency ? 1 : 0;

                    input = LogParamUtil.LogContent("PatID", data.PatientCode, "MedID", data.TreatmentCode, "Name", data.PatientName, "YBrith", data.DateOfBirth, "Sex", sex, "CardCode", cardNumber, "InsFrom", data.CardFromDate, "InsTo", data.CardToDate, "Hospital", hospital, "CardAdd", data.CardAddress, "HomeNumber", "", "Address", data.Address, "ProvinceID", data.ProvinceCode, "Districtid", data.DistrictCode, "PrecintID", data.CommuneCode, "Relatedinfo", data.RelativeInfo, "Diag", "", "Doctor", data.Creator, "ExamRoom", data.ExamRoomCode, "ExamStyle", data.ExamStyleId.ToString(), "Emergency", isMergency, "WorkID", data.CareerCode, "Chanel", isLine, "Hospital1", hospital, "ICD", data.TransferIcdCode, "DoubleExam", 0, "ListExam", "", "Military", 0, "login_name", data.Creator, "EthnicID", data.EthnicCode);

                    string rs = this.client.srv_hms_insReceivePatient_Insurance(
                        data.PatientCode, 
                        data.TreatmentCode, 
                        data.PatientName, 
                        data.DateOfBirth,
                        sex,
                        cardNumber, 
                        data.CardFromDate, 
                        data.CardToDate, 
                        hospital, 
                        data.CardAddress, 
                        "",
                        data.Address, 
                        data.ProvinceCode,
                        data.DistrictCode,
                        data.CommuneCode, 
                        data.RelativeInfo, 
                        "", 
                        data.Creator, 
                        data.ExamRoomCode,
                        data.ExamStyleId.ToString(), 
                        isMergency,
                        data.CareerCode,
                        isLine,
                        hospital1, 
                        data.TransferIcdCode, 
                        0, 
                        "", 
                        0, 
                        data.Creator,
                        data.EthnicCode);

                    result = rs == OK;

                    if (!result)
                    {
                        LogSystem.Warn("Tao ho so cua BN moi BHYT sang he thong HMS that bai. Input: " + input + " Output: " + rs);
                    }
                    else
                    {
                        LogSystem.Info("Tao ho so cua BN moi BHYT sang he thong HMS thanh cong. Input: " + input + " Output: " + rs);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error("Tao ho so cua BN moi BHYT sang he thong HMS that bai. Input: " + input);
                    LogSystem.Error(ex);
                    result = false;
                }
            }
            return result;
        }

        public bool Create(NewPatientTreatmentData data)
        {
            bool result = false;
            string input = "";
            if (data != null)
            {
                try
                {
                    int isLine = data.IsRightRoute ? 1 : 0;
                    int hospital = !string.IsNullOrWhiteSpace(data.TransferHeinOrgCode) ? int.Parse(data.TransferHeinOrgCode) : 0;
                    int sex = data.IsMale ? 1 : 0;
                    int isMergency = data.IsEmergency ? 1 : 0;
                    input = LogParamUtil.LogContent("PatID", data.PatientCode, "MedID", data.TreatmentCode, "Name", data.PatientName, "YBrith", data.DateOfBirth, "Sex", sex, "HomeNumber", "", "Address", data.Address, "ProvinceID", data.ProvinceCode, "Districtid", data.DistrictCode, "PrecintID", data.CommuneCode, "Relatedinfo", data.RelativeInfo, "Diag", "", "Doctor", data.Creator, "ExamRoom", data.ExamRoomCode, "ExamStyle", data.ExamStyleId.ToString(), "Emergency", isMergency, "WorkID", data.CareerCode, "Chanel", isLine, "Hospital", hospital, "ICD", data.TransferIcd, "DoubleExam", 0, "ListExam", "", "Military", 0, "login_name", data.Creator, "EthnicID", data.EthnicCode);

                    string rs = this.client.insReceivePatient_NonInsurance(
                        data.PatientCode,
                        data.TreatmentCode,
                        data.PatientName,
                        data.DateOfBirth,
                        sex,
                        "",
                        data.Address,
                        data.ProvinceCode,
                        data.DistrictCode,
                        data.CommuneCode,
                        data.RelativeInfo,
                        "",
                        data.Creator,
                        data.ExamRoomCode,
                        data.ExamStyleId.ToString(),
                        isMergency,
                        data.CareerCode,
                        isLine,
                        hospital,
                        data.TransferIcd,
                        0,
                        "",
                        0,
                        data.Creator,
                        data.EthnicCode);
                    
                    result = rs == OK;

                    if (!result)
                    {
                        LogSystem.Warn("Tao ho so cua BN moi (ko phai BHYT) sang he thong HMS that bai. Input: " + input + " Output: " + rs);
                    }
                    else
                    {
                        LogSystem.Info("Tao ho so cua BN moi (ko phai BHYT) sang he thong HMS thanh cong. Input: " + input + " Output: " + rs);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error("Tao ho so cua BN moi (ko phai BHYT) sang he thong HMS that bai. Input: " + input);
                    LogSystem.Error(ex);
                    result = false;
                }
            }
            return result;
        }

        public bool EndExam(EndExamData data)
        {
            bool result = false;
            string input = "";
            if (data != null)
            {
                try
                {
                    int roomId = !string.IsNullOrWhiteSpace(data.NextRoomCode) ? int.Parse(data.NextRoomCode) : 0;
                    int hospitalId = !string.IsNullOrWhiteSpace(data.TransferOrgCode) ? int.Parse(data.TransferOrgCode) : 0;
                    int treatmentdivisionId = !string.IsNullOrWhiteSpace(data.TreatmentDepartmentCode) ? int.Parse(data.TreatmentDepartmentCode) : 0;
                    input = LogParamUtil.LogContent("ReceptionID", 0, "MedID", data.TreatmentCode, "ExamDate", data.OutTime, "SuggestID", (int)data.Suggestion, "ConclusionID", (int)data.Conclusion, "TreatmentdivisionID", treatmentdivisionId, "Day", 0, "RoomID", roomId, "ICD", data.IcdCode, "HospitalID", hospitalId, "login_name", data.LoginName);

                    string rs = this.client.srv_hms_ExamResult(
                        0,
                        data.TreatmentCode,
                        data.OutTime,
                        (int)data.Suggestion,
                        (int)data.Conclusion,
                        treatmentdivisionId,
                        0,
                        roomId,
                        data.IcdCode,
                        hospitalId,
                        data.LoginName
                        );

                    result = rs == OK;

                    if (!result)
                    {
                        LogSystem.Warn("Ket thuc kham sang he thong HMS that bai. Input: " + input + " Output: " + rs);
                    }
                    else
                    {
                        LogSystem.Info("Ket thuc kham sang he thong HMS thanh cong. Input: " + input + " Output: " + rs);
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error("Ket thuc kham sang he thong HMS that bai. Input: " + input);
                    LogSystem.Error(ex);
                    result = false;
                }
            }
            return result;
        }
    }
}
