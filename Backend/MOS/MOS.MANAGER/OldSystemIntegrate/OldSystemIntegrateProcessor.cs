using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt.HeinRightRoute;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.OldSystem.HMS;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.OldSystemIntegrate
{
    class SubclinicalService
    {
        public string ServiceCode { get; set; }
        public bool IsUseBhyt { get; set; }
        public bool IsUseService { get; set; }
        public string ParentServiceCode { get; set; }
    }

    /// <summary>
    /// Xu ly nghiep vu tich hop he thong phan mem cu de dong bo du lieu
    /// </summary>
    public class OldSystemIntegrateProcessor
    {
        private static string UPDATE_TREATMENT = "UPDATE HIS_TREATMENT T SET T.IS_INTEGRATE_HIS_SENT = 1 WHERE ID = :param1";
        private static string UPDATE_SERVICE_REQ = "UPDATE HIS_SERVICE_REQ T SET T.IS_INTEGRATE_HIS_SENT = 1 WHERE ID = :param1";
        
        /// <summary>
        /// Tao ho so dieu tri
        /// </summary>
        /// <param name="isOldPatient"></param>
        /// <param name="patient"></param>
        /// <param name="treatment"></param>
        /// <param name="serviceReq"></param>
        /// <param name="pta"></param>
        public static bool CreateTreatment(bool isOldPatient, HIS_PATIENT_TYPE_ALTER pta, HIS_PATIENT patient, HIS_TREATMENT treatment, HIS_SERE_SERV sereServ)
        {
            try
            {
                V_HIS_ROOM room = sereServ != null ? HisRoomCFG.DATA.Where(o => o.ID == sereServ.TDL_EXECUTE_ROOM_ID).FirstOrDefault() : null;
                //Neu du lieu ko du thi bo qua
                if (sereServ == null || treatment == null || room == null || patient == null || pta == null)
                {
                    return false;
                }

                //Neu co cau hinh ket noi den he thong pm HIS PMS
                if (OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS)
                {
                    bool rs = false;

                    MOS.MANAGER.Config.OldSystemCFG.HmsExamStyleMapping mapping = OldSystemCFG.HMS_EXAM_STYLE_MAPPING
                        .Where(o => o.PatientTypeId == sereServ.PATIENT_TYPE_ID && o.ServiceId == sereServ.SERVICE_ID)
                        .FirstOrDefault();
                    if (mapping == null)
                    {
                        LogSystem.Warn("Ko tim thay exam_style tuong ung voi service_id: " + sereServ.SERVICE_ID + " va patient_Type_id: " + sereServ.PATIENT_TYPE_ID);
                        return false;
                    }

                    TreatmentConsumer consumer = new TreatmentConsumer(OldSystemCFG.ADDRESS);

                    //Neu la benh nhan cu
                    if (isOldPatient)
                    {
                        OldPatientTreatmentData data = new OldPatientTreatmentData();

                        data.CardFromDate = pta.HEIN_CARD_FROM_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(pta.HEIN_CARD_FROM_TIME.Value) : "";
                        data.CardNumber = pta.HEIN_CARD_NUMBER;
                        data.CardToDate = pta.HEIN_CARD_TO_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(pta.HEIN_CARD_TO_TIME.Value) : "";
                        data.HeinOrgCode = pta.HEIN_MEDI_ORG_CODE;
                        data.Creator = treatment.CREATOR;
                        data.ExamRoomCode = room.ROOM_CODE;
                        data.ExamStyleId = mapping.ExamStyleId;
                        data.IsEmergency = treatment.IS_EMERGENCY == Constant.IS_TRUE;
                        data.IsRightRoute = pta.RIGHT_ROUTE_CODE == HeinRightRouteCode.TRUE;
                        data.PatientCode = PatientCode(patient.PATIENT_CODE);
                        data.TransferHeinOrgCode = treatment.TRANSFER_IN_MEDI_ORG_CODE;
                        data.TransferIcdCode = treatment.TRANSFER_IN_ICD_CODE;
                        data.TreatmentCode = TreatmentCode(treatment.TREATMENT_CODE);

                        rs = consumer.Create(data);
                    }
                    //Neu la BN moi va doi tuong la BHYT
                    else if (pta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        NewBhytPatientTreatmentData data = new NewBhytPatientTreatmentData();
                        data.Address = patient.ADDRESS;
                        data.CommuneCode = string.IsNullOrWhiteSpace(patient.COMMUNE_CODE) ? 0 : int.Parse(patient.COMMUNE_CODE);
                        data.DistrictCode = string.IsNullOrWhiteSpace(patient.DISTRICT_CODE) ? 0 : int.Parse(patient.DISTRICT_CODE);
                        data.ProvinceCode = string.IsNullOrWhiteSpace(patient.PROVINCE_CODE) ? 0 : int.Parse(patient.PROVINCE_CODE);
                        data.CardAddress = pta.ADDRESS;
                        data.CareerCode = string.IsNullOrWhiteSpace(patient.CAREER_CODE) ? 0 : int.Parse(patient.CAREER_CODE);
                        data.DateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(patient.DOB);
                        data.IsMale = patient.GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE;
                        //ko lay truong vir_patient_name tranh truong hop doi tuong patient ko phai lay tu DB
                        data.PatientName = CommonUtil.NVL(patient.LAST_NAME, " ") + CommonUtil.NVL(patient.FIRST_NAME);
                        data.RelativeInfo = patient.RELATIVE_NAME;
                        data.CardFromDate = pta.HEIN_CARD_FROM_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(pta.HEIN_CARD_FROM_TIME.Value) : "";
                        data.CardNumber = pta.HEIN_CARD_NUMBER;
                        data.CardToDate = pta.HEIN_CARD_TO_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(pta.HEIN_CARD_TO_TIME.Value) : "";
                        data.HeinOrgCode = pta.HEIN_MEDI_ORG_CODE;
                        data.Creator = treatment.CREATOR; //
                        data.ExamRoomCode = room.ROOM_CODE;
                        data.ExamStyleId = mapping.ExamStyleId;
                        data.IsEmergency = treatment.IS_EMERGENCY == Constant.IS_TRUE;
                        data.IsRightRoute = pta.RIGHT_ROUTE_CODE == HeinRightRouteCode.TRUE;
                        data.PatientCode = PatientCode(patient.PATIENT_CODE);
                        data.TransferHeinOrgCode = treatment.TRANSFER_IN_MEDI_ORG_CODE;
                        data.TransferIcdCode = treatment.TRANSFER_IN_ICD_CODE;
                        data.TreatmentCode = TreatmentCode(treatment.TREATMENT_CODE);
                        data.EthnicCode = string.IsNullOrWhiteSpace(patient.ETHNIC_CODE) ? 0 : int.Parse(patient.ETHNIC_CODE);

                        rs = consumer.Create(data);
                    }
                    //Neu la BN moi va doi tuong ko phai la BHYT
                    else
                    {
                        NewPatientTreatmentData data = new NewPatientTreatmentData();
                        data.Address = patient.ADDRESS;
                        data.CommuneCode = string.IsNullOrWhiteSpace(patient.COMMUNE_CODE) ? 0 : int.Parse(patient.COMMUNE_CODE);
                        data.DistrictCode = string.IsNullOrWhiteSpace(patient.DISTRICT_CODE) ? 0 : int.Parse(patient.DISTRICT_CODE);
                        data.ProvinceCode = string.IsNullOrWhiteSpace(patient.PROVINCE_CODE) ? 0 : int.Parse(patient.PROVINCE_CODE);
                        data.CareerCode = string.IsNullOrWhiteSpace(patient.CAREER_CODE) ? 0 : int.Parse(patient.CAREER_CODE);
                        data.DateOfBirth = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(patient.DOB);
                        data.IsMale = patient.GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE;
                        //ko lay truong vir_patient_name tranh truong hop doi tuong patient ko phai lay tu DB
                        data.PatientName = CommonUtil.NVL(patient.LAST_NAME, " ") + CommonUtil.NVL(patient.FIRST_NAME);
                        data.RelativeInfo = patient.RELATIVE_NAME;
                        data.Creator = treatment.CREATOR; //
                        data.ExamRoomCode = room.ROOM_CODE;
                        data.ExamStyleId = mapping.ExamStyleId;
                        data.IsEmergency = treatment.IS_EMERGENCY == Constant.IS_TRUE;
                        data.IsRightRoute = pta.RIGHT_ROUTE_CODE == HeinRightRouteCode.TRUE;
                        data.PatientCode = PatientCode(patient.PATIENT_CODE);
                        data.TransferHeinOrgCode = treatment.TRANSFER_IN_MEDI_ORG_CODE;
                        data.TreatmentCode = TreatmentCode(treatment.TREATMENT_CODE);
                        data.TransferIcd = treatment.TRANSFER_IN_ICD_CODE;
                        data.EthnicCode = string.IsNullOrWhiteSpace(patient.ETHNIC_CODE) ? 0 : int.Parse(patient.ETHNIC_CODE);
                        rs = consumer.Create(data);
                    }

                    //Neu gui sang thanh cong thi thuc hien cap nhat du lieu la da gui
                    if (rs && !DAOWorker.SqlDAO.Execute(UPDATE_TREATMENT, treatment.ID))
                    {
                        LogSystem.Warn("Cap nhat ho so da gui sang he thong PMS that bai");
                    }
                    return rs;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        public static void FinishTreatment(HIS_TREATMENT treatment)
        {
            try
            {
                //Neu co cau hinh ket noi den he thong pm HIS PMS
                if (OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS)
                {
                    bool rs = false;

                    V_HIS_ROOM room = treatment != null ? HisRoomCFG.DATA.Where(o => o.ID == treatment.END_ROOM_ID).FirstOrDefault() : null;

                    TreatmentConsumer consumer = new TreatmentConsumer(OldSystemCFG.ADDRESS);

                    EndExamData data = new EndExamData();
                    data.IcdCode = treatment.ICD_CODE;
                    data.LoginName = treatment.END_LOGINNAME;
                    data.NextRoomCode = room.ROOM_CODE;
                    data.OutTime = treatment.OUT_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(treatment.OUT_TIME.Value) : "";
                    data.TreatmentCode = TreatmentCode(treatment.TREATMENT_CODE);

                    if (treatment.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__TRON)
                    {
                        data.Conclusion = ConclusionEnum.TRON_VIEN;
                    }
                    else if (treatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__KTD)
                    {
                        data.Conclusion = ConclusionEnum.KHONG_DOI;
                    }
                    else if (treatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__DO)
                    {
                        data.Conclusion = ConclusionEnum.GIAM;
                    }
                    else if (treatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__KHOI)
                    {
                        data.Conclusion = ConclusionEnum.KHOI;
                    }
                    else if (treatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__NANG)
                    {
                        data.Conclusion = ConclusionEnum.NANG_HON;
                    }
                    else if (treatment.TREATMENT_RESULT_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_RESULT.ID__CHET)
                    {
                        DateTime inTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.IN_TIME).Value;
                        DateTime outTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.OUT_TIME.Value).Value;
                        double diff = outTime.Subtract(inTime).TotalHours;

                        if (diff < 24)
                        {
                            data.Conclusion = ConclusionEnum.TU_VONG_TRUOC_24H;
                        }
                        else
                        {
                            data.Conclusion = ConclusionEnum.TU_VONG_SAU_24H;
                        }
                    }
                    else
                    {
                        data.Conclusion = ConclusionEnum.KHAC;
                    }

                    if (treatment.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN)
                    {
                        data.Suggestion = SuggestionEnum.CHUYEN_TUYEN_TREN;
                        data.TransferOrgCode = treatment.MEDI_ORG_CODE;
                    }
                    else
                    {
                        data.Suggestion = SuggestionEnum.RA_VIEN;
                    }

                    rs = consumer.EndExam(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public static void AddExam(HIS_TREATMENT treatment, HIS_SERVICE_REQ newServiceReq)
        {
            try
            {
                //Neu co cau hinh ket noi den he thong pm HIS PMS
                if (OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS)
                {
                    bool rs = false;
                    V_HIS_ROOM room = newServiceReq != null ? HisRoomCFG.DATA.Where(o => o.ID == newServiceReq.EXECUTE_ROOM_ID).FirstOrDefault() : null;
                    TreatmentConsumer consumer = new TreatmentConsumer(OldSystemCFG.ADDRESS);

                    EndExamData data = new EndExamData();
                    data.IcdCode = treatment.ICD_CODE;
                    data.LoginName = newServiceReq.REQUEST_LOGINNAME;
                    data.OutTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(newServiceReq.INTRUCTION_TIME);
                    data.TreatmentCode = TreatmentCode(treatment.TREATMENT_CODE);
                    data.Conclusion = ConclusionEnum.KHONG_DOI;
                    data.Suggestion = SuggestionEnum.CHUYEN_KHAM;
                    data.NextRoomCode = room.ROOM_CODE;
                    rs = consumer.EndExam(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public static void Hospitalize(HIS_TREATMENT treatment, long time, long departmentId)
        {
            try
            {
                //Neu co cau hinh ket noi den he thong pm HIS PMS
                if (OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS)
                {
                    V_HIS_ROOM room = treatment != null ? HisRoomCFG.DATA.Where(o => o.ID == treatment.IN_ROOM_ID).FirstOrDefault() : null;

                    bool rs = false;
                    TreatmentConsumer consumer = new TreatmentConsumer(OldSystemCFG.ADDRESS);
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.Where(o => o.ID == departmentId).FirstOrDefault();
                    if (department == null)
                    {
                        return;
                    }

                    EndExamData data = new EndExamData();
                    data.IcdCode = treatment.ICD_CODE;
                    data.LoginName = treatment.IN_LOGINNAME;
                    data.OutTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(time);
                    data.TreatmentCode = TreatmentCode(treatment.TREATMENT_CODE);
                    data.Conclusion = ConclusionEnum.KHONG_DOI;
                    data.NextRoomCode = room.ROOM_CODE;
                    if (treatment.IN_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                    {
                        data.Suggestion = SuggestionEnum.DIEU_TRI_NOI_TRU;
                    }
                    else
                    {
                        data.Suggestion = SuggestionEnum.DIEU_TRI_NGOAI_TRU;
                    }
                    data.TreatmentDepartmentCode = department.DEPARTMENT_CODE;
                    
                    rs = consumer.EndExam(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        public static bool CreateServiceReq(HIS_SERVICE_REQ serviceReq, List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                //Neu co cau hinh ket noi den he thong pm HIS PMS
                if (OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS)
                {
                    V_HIS_ROOM room = serviceReq != null ? HisRoomCFG.DATA.Where(o => o.ID == serviceReq.REQUEST_ROOM_ID).FirstOrDefault() : null;

                    if (room == null || serviceReq == null || sereServs == null || sereServs.Count == 0)
                    {
                        return false;
                    }
                    List<SubclinicalService> subclinicalServices = new List<SubclinicalService>();
                    foreach (HIS_SERE_SERV ss in sereServs)
                    {
                        SubclinicalService s = new SubclinicalService();
                        s.ServiceCode = ss.TDL_SERVICE_CODE;
                        s.IsUseBhyt = ss.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT;
                        s.IsUseService = ss.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__SERVICE || ss.PRIMARY_PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__SERVICE;
                        V_HIS_SERVICE service = HisServiceCFG.DATA_VIEW.Where(o => o.ID == ss.SERVICE_ID).FirstOrDefault();
                        if (service.PARENT_ID.HasValue)
                        {
                            V_HIS_SERVICE pr = HisServiceCFG.DATA_VIEW.Where(o => o.ID == service.PARENT_ID.Value).FirstOrDefault();
                            s.ParentServiceCode = pr.SERVICE_CODE;
                        }
                        else
                        {
                            //Xu ly de gan "ma nhom" (cua HIS cu) tuong ung voi loai DV vao ParentServiceCode
                        }
                        subclinicalServices.Add(s);
                    }

                    ServiceReqConsumer serviceReqConsumer = new ServiceReqConsumer(OldSystemCFG.ADDRESS);

                    var groups = subclinicalServices.GroupBy(o => new { o.IsUseBhyt, o.IsUseService, o.ParentServiceCode }).ToList();

                    bool rs = false;
                    foreach (var g in groups)
                    {
                        ServiceReqData data = new ServiceReqData();
                        data.CreateTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(serviceReq.INTRUCTION_TIME);
                        data.Creator = serviceReq.CREATOR;
                        data.GroupCode = g.Key.ParentServiceCode;
                        data.IcdName = serviceReq.ICD_NAME;
                        data.IsEmergency = serviceReq.IS_EMERGENCY == Constant.IS_TRUE;
                        data.IsUseBhyt = g.Key.IsUseBhyt;
                        data.IsUseService = g.Key.IsUseService;
                        data.PatientCode = PatientCode(serviceReq.TDL_PATIENT_CODE);
                        data.ReqRoomCode = room.ROOM_CODE;
                        data.RequestLoginname = serviceReq.REQUEST_LOGINNAME;
                        data.ServiceReqCode = serviceReq.SERVICE_REQ_CODE;
                        data.SubclinicalCodes = g.Select(o => o.ServiceCode).ToList();
                        data.TreatmentCode = TreatmentCode(serviceReq.TDL_TREATMENT_CODE);
                        data.Description = serviceReq.DESCRIPTION;

                        rs = serviceReqConsumer.Create(data);
                    }
                    //Neu gui sang thanh cong thi thuc hien cap nhat du lieu la da gui
                    if (rs && !DAOWorker.SqlDAO.Execute(UPDATE_SERVICE_REQ, serviceReq.ID))
                    {
                        LogSystem.Warn("Cap nhat y lenh da gui sang he thong PMS that bai");
                    }
                    return rs;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        public static void CreateServiceReq(List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                if (serviceReqs != null && serviceReqs.Count > 0 && sereServs != null && sereServs.Count > 0)
                {
                    foreach (HIS_SERVICE_REQ serviceReq in serviceReqs)
                    {
                        List<HIS_SERE_SERV> ss = sereServs.Where(o => o.SERVICE_REQ_ID == serviceReq.ID).ToList();
                        if (ss != null && ss.Count > 0)
                        {
                            CreateServiceReq(serviceReq, ss);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private static string TreatmentCode(string input)
        {
            return input != null && input.Length == 12 ? input.Substring(4, 8) : input;
        }

        private static string PatientCode(string input)
        {
            return input != null && input.Length == 10 ? input.Substring(2, 8) : input;
        }
    }
}