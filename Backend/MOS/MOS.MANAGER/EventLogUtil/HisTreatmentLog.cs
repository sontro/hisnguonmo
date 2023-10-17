using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.LibraryHein.Bhyt.HeinRightRoute;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using MOS.LogManager;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisFund;
using MOS.MANAGER.HisOtherPaySource;
using MOS.MANAGER.HisOweType;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisTreatmentLog
    {
        internal static void Run(HIS_PATIENT patient, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER patientTypeAlter, List<V_HIS_SERVICE_REQ> serviceReqs, List<V_HIS_SERE_SERV> sereServs, EventLog.Enum logEnum)
        {
            try
            {
                List<ServiceReqData> serviceReqData = HisServiceReqLog.GetServiceReqData(serviceReqs, sereServs);
                new EventLogGenerator(logEnum, GetPatientTypeAlterData(patientTypeAlter))
                    .TreatmentCode(treatment.TREATMENT_CODE)
                    .ServiceReqList(serviceReqData)
                    .PatientData(new PatientData(patient.PATIENT_CODE, patient.VIR_PATIENT_NAME, patient.DOB, patient.GENDER_ID, patient.IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE))
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal static void Run(string treatmentCode, HIS_PATIENT_TYPE_ALTER patientTypeAlter, EventLog.Enum logEnum)
        {
            try
            {
                new EventLogGenerator(logEnum, GetPatientTypeAlterData(patientTypeAlter)).TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal static void Run(string treatmentCode, HIS_PATIENT_TYPE_ALTER oldPta, HIS_PATIENT_TYPE_ALTER newPta, EventLog.Enum logEnum)
        {
            try
            {
                new EventLogGenerator(logEnum, GetPatientTypeAlterData(oldPta), GetPatientTypeAlterData(newPta))
                    .TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal static void Run(HIS_PATIENT patient, HIS_TREATMENT treatment, HIS_PATIENT_TYPE_ALTER patientTypeAlter, EventLog.Enum logEnum)
        {
            try
            {
                new EventLogGenerator(logEnum, GetPatientTypeAlterData(patientTypeAlter))
                    .TreatmentCode(treatment.TREATMENT_CODE)
                    .PatientData(new PatientData(patient.PATIENT_CODE, patient.VIR_PATIENT_NAME, patient.DOB, patient.GENDER_ID, patient.IS_HAS_NOT_DAY_DOB == Constant.IS_TRUE))
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private static string GetPatientTypeAlterData(HIS_PATIENT_TYPE_ALTER patientTypeAlter)
        {
            try
            {
                if (patientTypeAlter != null)
                {
                    HIS_PATIENT_TYPE patientType = HisPatientTypeCFG.DATA.Where(o => o.ID == patientTypeAlter.PATIENT_TYPE_ID).FirstOrDefault();
                    HIS_TREATMENT_TYPE treatmentType = HisTreatmentTypeCFG.DATA.Where(o => o.ID == patientTypeAlter.TREATMENT_TYPE_ID).FirstOrDefault();

                    string patientTypeName = patientType != null ? patientType.PATIENT_TYPE_NAME : "";
                    string treatmentTypeName = treatmentType != null ? treatmentType.TREATMENT_TYPE_NAME : "";
                    string heinInfo = "";
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.HEIN_CARD_NUMBER))
                    {
                        heinInfo += string.Format("Số thẻ BHYT: {0}; ", patientTypeAlter.HEIN_CARD_NUMBER);
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.HEIN_MEDI_ORG_CODE))
                    {
                        heinInfo += string.Format("Mã nơi ĐK KCB ban đầu: {0}; ", patientTypeAlter.HEIN_MEDI_ORG_CODE);
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.HEIN_MEDI_ORG_NAME))
                    {
                        heinInfo += string.Format("Nơi ĐK KCB ban đầu: {0}; ", patientTypeAlter.HEIN_MEDI_ORG_NAME);
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.HAS_BIRTH_CERTIFICATE))
                    {
                        heinInfo += string.Format("Chứng sinh: {0}; ", patientTypeAlter.HAS_BIRTH_CERTIFICATE.Equals(LibraryHein.Bhyt.HeinHasBirthCertificate.HeinHasBirthCertificateCode.TRUE) ? "Có" : "Không");
                    }
                    if (patientTypeAlter.HEIN_CARD_FROM_TIME.HasValue)
                    {
                        string time = Inventec.Common.DateTime.Convert.TimeNumberToDateString(patientTypeAlter.HEIN_CARD_FROM_TIME.Value);
                        heinInfo += string.Format("Hiệu lực từ: {0}; ", time);
                    }
                    if (patientTypeAlter.HEIN_CARD_TO_TIME.HasValue)
                    {
                        string time = Inventec.Common.DateTime.Convert.TimeNumberToDateString(patientTypeAlter.HEIN_CARD_TO_TIME.Value);
                        heinInfo += string.Format("Hiệu lực đến: {0}; ", time);
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.LIVE_AREA_CODE))
                    {
                        heinInfo += string.Format("Khu vực: {0}; ", patientTypeAlter.LIVE_AREA_CODE);
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.JOIN_5_YEAR))
                    {
                        heinInfo += string.Format("Tham gia BH 5 năm: {0}; ", patientTypeAlter.JOIN_5_YEAR.Equals(LibraryHein.Bhyt.HeinJoin5Year.HeinJoin5YearCode.TRUE) ? "Có" : "Không");
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.PAID_6_MONTH))
                    {
                        heinInfo += string.Format("Đồng chi trả đủ 6 tháng: {0}; ", patientTypeAlter.PAID_6_MONTH.Equals(LibraryHein.Bhyt.HeinPaid6Month.HeinPaid6MonthCode.TRUE) ? "Đủ" : "Chưa");
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.RIGHT_ROUTE_CODE))
                    {
                        var t = HeinRightRouteStore.GetByCode(patientTypeAlter.RIGHT_ROUTE_CODE);
                        heinInfo += string.Format("{0}; ", t != null ? t.HeinRightRouteName : "");
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.RIGHT_ROUTE_TYPE_CODE))
                    {
                        var t = HeinRightRouteTypeStore.GetByCode(patientTypeAlter.RIGHT_ROUTE_TYPE_CODE);
                        heinInfo += string.Format("{0}; ", t != null ? t.HeinRightRouteTypeName : "");
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.LIVE_AREA_CODE))
                    {
                        heinInfo += string.Format("Khu vực: {0} ", patientTypeAlter.LIVE_AREA_CODE);
                    }
                    if (!string.IsNullOrWhiteSpace(patientTypeAlter.ADDRESS))
                    {
                        heinInfo += string.Format("Địa chỉ: {0} ", patientTypeAlter.ADDRESS);
                    }

                    return string.Format("Đối tượng: {0}. {1}. {2}", patientTypeName, treatmentTypeName, heinInfo);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }

        internal static void Run(HIS_TREATMENT treatment, EventLog.Enum logEnum, params object[] extras)
        {
            try
            {
                new EventLogGenerator(logEnum, extras)
                    .TreatmentCode(treatment.TREATMENT_CODE)
                    .Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }


        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal static void Run(HIS_TREATMENT editData, HIS_TREATMENT oldData, HisTreatmentCommonInfoUpdateSDO sdo, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (oldData != null && editData != null)
                {
                    if (IsDiffLong(oldData.IN_TIME, editData.IN_TIME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianVaoVien);
                        string inTimeOld = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.IN_TIME);
                        string inTimeEdit = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.IN_TIME);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, inTimeOld, inTimeEdit));
                    }
                    if (IsDiffLong(oldData.CLINICAL_IN_TIME, editData.CLINICAL_IN_TIME))
                    {
                        string clinicalInTimeOld = oldData.CLINICAL_IN_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.CLINICAL_IN_TIME.Value) : "";
                        string clinicalInTimeEdit = editData.CLINICAL_IN_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.CLINICAL_IN_TIME.Value) : "";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianNhapVien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, clinicalInTimeOld, clinicalInTimeEdit));
                    }
                    if (IsDiffLong(oldData.OUT_TIME, editData.OUT_TIME))
                    {
                        string outTimeOld = oldData.OUT_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.OUT_TIME.Value) : "";
                        string outTimeEdit = editData.OUT_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.OUT_TIME.Value) : "";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianRaVien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, outTimeOld, outTimeEdit));
                    }
                    if (IsDiffString(oldData.IN_CODE, editData.IN_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoVaoVien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IN_CODE, editData.IN_CODE));
                    }
                    if (IsDiffString(oldData.END_CODE, editData.END_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoRaVien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.END_CODE, editData.END_CODE));
                    }
                    if (IsDiffLong(oldData.TREATMENT_ORDER, editData.TREATMENT_ORDER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SttHoSo);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TREATMENT_ORDER, editData.TREATMENT_ORDER));
                    }
                    if (IsDiffString(oldData.ICD_CODE, editData.ICD_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CDChinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ICD_CODE, editData.ICD_CODE));
                    }
                    if (IsDiffString(oldData.ICD_NAME, editData.ICD_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenCDChinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ICD_NAME, editData.ICD_NAME));
                    }
                    if (IsDiffString(oldData.ICD_SUB_CODE, editData.ICD_SUB_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CDPhu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ICD_SUB_CODE, editData.ICD_SUB_CODE));
                    }
                    if (IsDiffString(oldData.ICD_TEXT, editData.ICD_TEXT))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenCDPhu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ICD_TEXT, editData.ICD_TEXT));
                    }
                    if (IsDiffString(oldData.TRADITIONAL_ICD_CODE, editData.TRADITIONAL_ICD_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CDYHCT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TRADITIONAL_ICD_CODE, editData.TRADITIONAL_ICD_CODE));
                    }

                    if (IsDiffString(oldData.TRADITIONAL_ICD_NAME, editData.TRADITIONAL_ICD_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenCDYHCT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TRADITIONAL_ICD_NAME, editData.TRADITIONAL_ICD_NAME));
                    }
                    if (IsDiffString(oldData.TRADITIONAL_ICD_SUB_CODE, editData.TRADITIONAL_ICD_SUB_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CDYHCTPhu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TRADITIONAL_ICD_SUB_CODE, editData.TRADITIONAL_ICD_SUB_CODE));
                    }
                    if (IsDiffString(oldData.TRADITIONAL_ICD_TEXT, editData.TRADITIONAL_ICD_TEXT))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenCDYHCTPhu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TRADITIONAL_ICD_TEXT, editData.TRADITIONAL_ICD_TEXT));
                    }
                    if (IsDiffString(oldData.ICD_CAUSE_CODE, editData.ICD_CAUSE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NNNgoai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ICD_CAUSE_CODE, editData.ICD_CAUSE_CODE));
                    }
                    if (IsDiffString(oldData.ICD_CAUSE_NAME, editData.ICD_CAUSE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenNNNgoai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ICD_CAUSE_NAME, editData.ICD_CAUSE_NAME));
                    }

                    if (IsDiffString(oldData.DOCTOR_LOGINNAME, editData.DOCTOR_LOGINNAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BacSyDieuTri);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DOCTOR_LOGINNAME, editData.DOCTOR_LOGINNAME));
                    }
                    if (IsDiffString(oldData.DOCTOR_USERNAME, editData.DOCTOR_USERNAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenBacSiDieuTri);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DOCTOR_USERNAME, editData.DOCTOR_USERNAME));
                    }
                    if (IsDiffString(oldData.IN_ICD_CODE, editData.IN_ICD_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChanDoanNhapVien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IN_ICD_CODE, editData.IN_ICD_CODE));
                    }

                    if (IsDiffString(oldData.IN_ICD_NAME, editData.IN_ICD_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenCDNhapVien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IN_ICD_NAME, editData.IN_ICD_NAME));
                    }
                    if (IsDiffString(oldData.IN_ICD_SUB_CODE, editData.IN_ICD_SUB_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChanDoanPhuNhapVien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IN_ICD_SUB_CODE, editData.IN_ICD_SUB_CODE));
                    }
                    if (IsDiffString(oldData.IN_ICD_TEXT, editData.IN_ICD_TEXT))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenCDNhapVienPhu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IN_ICD_TEXT, editData.IN_ICD_TEXT));
                    }

                    if (IsDiffShortIsField(oldData.IS_EMERGENCY, editData.IS_EMERGENCY))
                    {
                        string newValue = editData.IS_EMERGENCY == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_EMERGENCY == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapCuu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }

                    if (IsDiffLong(oldData.FUND_ID, editData.FUND_ID))
                    {
                        HIS_FUND fundOld = new HisFundGet().GetById(oldData.FUND_ID ?? 0);
                        HIS_FUND fundEdit = new HisFundGet().GetById(editData.FUND_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DonViCungChiTra);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, fundOld != null ? fundOld.FUND_NAME : "", fundEdit != null ? fundEdit.FUND_NAME : ""));
                    }
                    if (IsDiffString(oldData.FUND_NUMBER, editData.FUND_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoThe);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FUND_NUMBER, editData.FUND_NUMBER));
                    }

                    if (IsDiffString(oldData.FUND_TYPE_NAME, editData.FUND_TYPE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SanPham);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FUND_TYPE_NAME, editData.FUND_TYPE_NAME));
                    }
                    if (IsDiffLong(oldData.FUND_FROM_TIME, editData.FUND_FROM_TIME))
                    {
                        string fundFromTimeOld = oldData.FUND_FROM_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.FUND_FROM_TIME.Value) : "";
                        string fundFromTimeeEdit = editData.FUND_FROM_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.FUND_FROM_TIME.Value) : "";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiHanTu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, fundFromTimeOld, fundFromTimeeEdit));
                    }

                    if (IsDiffLong(oldData.FUND_TO_TIME, editData.FUND_TO_TIME))
                    {
                        string fundFromTimeOld = oldData.FUND_TO_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.FUND_TO_TIME.Value) : "";
                        string fundFromTimeeEdit = editData.FUND_TO_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.FUND_TO_TIME.Value) : "";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiHanDen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, fundFromTimeOld, fundFromTimeeEdit));
                    }

                    if (IsDiffLong(oldData.FUND_ISSUE_TIME, editData.FUND_ISSUE_TIME))
                    {
                        string fundFromTimeOld = oldData.FUND_ISSUE_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.FUND_ISSUE_TIME.Value) : "";
                        string fundFromTimeeEdit = editData.FUND_ISSUE_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.FUND_ISSUE_TIME.Value) : "";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayCap);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, fundFromTimeOld, fundFromTimeeEdit));
                    }
                    if (IsDiffString(oldData.FUND_CUSTOMER_NAME, editData.FUND_CUSTOMER_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenKhachHang);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FUND_CUSTOMER_NAME, editData.FUND_CUSTOMER_NAME));
                    }
                    if (IsDiffString(oldData.FUND_COMPANY_NAME, editData.FUND_COMPANY_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Congty);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FUND_COMPANY_NAME, editData.FUND_COMPANY_NAME));
                    }
                    if (IsDiffDecimal(oldData.FUND_BUDGET, editData.FUND_BUDGET))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanMuc);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FUND_BUDGET, editData.FUND_BUDGET));
                    }
                    if (IsDiffLong(oldData.OTHER_PAY_SOURCE_ID, editData.OTHER_PAY_SOURCE_ID))
                    {
                        HIS_OTHER_PAY_SOURCE payOld = new HisOtherPaySourceGet().GetById(oldData.OTHER_PAY_SOURCE_ID ?? 0);
                        HIS_OTHER_PAY_SOURCE payEdit = new HisOtherPaySourceGet().GetById(editData.OTHER_PAY_SOURCE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguonChiTra);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, payOld != null ? payOld.OTHER_PAY_SOURCE_NAME : "", payEdit != null ? payEdit.OTHER_PAY_SOURCE_NAME : ""));
                    }
                    
                    if (sdo.UpdateOtherPaySourceIdForSereServ == true)
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatThongTinNguonChiTraChoCacDichVuThuocDaKe);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, "Không check", "Có check"));
                    }
                    else if (sdo.UpdateOtherPaySourceIdForSereServ == null)
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatThongTinNguonChiTraChoCacDichVuThuocDaKe);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, "Có check", "Không check"));
                    }

                    if (IsDiffLong(oldData.OWE_TYPE_ID, editData.OWE_TYPE_ID))
                    {
                        HIS_OWE_TYPE oweOld = new HisOweTypeGet().GetById(oldData.OWE_TYPE_ID ?? 0);
                        HIS_OWE_TYPE oweEdit = new HisOweTypeGet().GetById(editData.OWE_TYPE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoVienPhi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oweOld != null ? oweOld.OWE_TYPE_NAME : "", oweEdit != null ? oweEdit.OWE_TYPE_NAME : ""));
                    }

                    if (IsDiffShortIsField(oldData.IS_NOT_CHECK_LHMP, editData.IS_NOT_CHECK_LHMP))
                    {
                        string newValue = editData.IS_NOT_CHECK_LHMP == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_NOT_CHECK_LHMP == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongGioiHanTienThuocBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.NEED_SICK_LEAVE_CERT, editData.NEED_SICK_LEAVE_CERT))
                    {
                        string newValue = editData.NEED_SICK_LEAVE_CERT == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.NEED_SICK_LEAVE_CERT == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.YeuCauCapGiayNghiOm);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }

                    new EventLogGenerator(logEnum, String.Join(". ", editFields))
                     .TreatmentCode(oldData.TREATMENT_CODE)
                     .Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool IsDiffString(string oldValue, string newValue)
        {
            return (oldValue ?? "") != (newValue ?? "");
        }
        private static bool IsDiffLong(long? oldValue, long? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
        private static bool IsDiffDecimal(decimal? oldValue, decimal? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
        private static bool IsDiffShort(short? oldValue, short? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }
        private static bool IsDiffShortIsField(short? oldValue, short? newValue)
        {
            return (((oldValue == Constant.IS_TRUE) && (newValue != Constant.IS_TRUE)) || ((oldValue != Constant.IS_TRUE) && (newValue == Constant.IS_TRUE)));
        }
    }
}
