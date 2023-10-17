using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class HisServiceReqExamLog
    {
        internal static void LogProcess(HisServiceReqExamUpdateSDO data, HIS_SERVICE_REQ currentServiceReq, V_HIS_SERVICE_REQ addExamReq)
        {
            try
            {
                if (!(data.ExamAdditionSDO != null || data.HospitalizeSDO != null || data.TreatmentFinishSDO != null || data.IsFinish))
                {
                    return;
                }
                string message = "";
                if (data.ExamAdditionSDO != null)
                {
                    string kham_them = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.XuTriKhamThem);
                    string addReqCode = addExamReq != null ? addExamReq.SERVICE_REQ_CODE : "";
                    string khong_huong_bhyt = (data.ExamAdditionSDO.IsNotUseBhyt ? LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongHuongBhyt) + ". " : "");
                    V_HIS_SERVICE addService = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == data.ExamAdditionSDO.AdditionServiceId);
                    string service_name = addService != null ? addService.SERVICE_NAME : "";

                    message = String.Format("{0} ({1}{2}: {3} ({4}))", kham_them, khong_huong_bhyt, SimpleEventKey.SERVICE_REQ_CODE, addReqCode, service_name);
                }
                else if (data.HospitalizeSDO != null)
                {
                    string nhap_vien = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.XuTriNhapVien);
                    string vao_khoa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VaoKhoa);
                    string thoi_gian = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianYeuCau);
                    string dien_dieu_tri = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DienDieuTri);
                    HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == data.HospitalizeSDO.DepartmentId);
                    HIS_TREATMENT_TYPE treatType = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == data.HospitalizeSDO.TreatmentTypeId);

                    string depart_name = department != null ? department.DEPARTMENT_NAME : "";
                    string treat_type_name = treatType != null ? treatType.TREATMENT_TYPE_NAME : "";
                    string time_str = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.HospitalizeSDO.Time);
                    message = String.Format("{0} ({1}: {2}. {3}: {4}. {5}: {6})", nhap_vien, vao_khoa, depart_name, thoi_gian, time_str, dien_dieu_tri, treat_type_name);
                }
                else if (data.TreatmentFinishSDO != null)
                {
                    string ket_thuc_dieu_Tri = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.XutriKetThucDieuTri);
                    string thoi_gian = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Treatment_ThoiGianKetThuc);
                    string loai_ra_vien = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiRaVien);
                    string thong_tin_bo_sung = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThongTinBoSung);

                    string finish_time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.TreatmentFinishSDO.TreatmentFinishTime);
                    HIS_TREATMENT_END_TYPE treatEndType = HisTreatmentEndTypeCFG.DATA.FirstOrDefault(o => o.ID == data.TreatmentFinishSDO.TreatmentEndTypeId);
                    string treat_end_type_name = treatEndType != null ? treatEndType.TREATMENT_END_TYPE_NAME : "";
                    var treatmentEndTypeExt = HisTreatmentEndTypeExtCFG.DATA.FirstOrDefault(o => o.ID == data.TreatmentFinishSDO.TreatmentEndTypeExtId);
                    string treat_end_type_ext_name = treatmentEndTypeExt != null ? treatmentEndTypeExt.TREATMENT_END_TYPE_EXT_NAME : "";

                    message = String.Format("{0} ({1}: {2}. {3}: {4}. {5}: {6})", ket_thuc_dieu_Tri, thoi_gian, finish_time, loai_ra_vien, treat_end_type_name, thong_tin_bo_sung, treat_end_type_ext_name);
                }
                else if (data.IsFinish)
                {
                    string ket_thuc_kham = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.XuTriKetThucKham);
                    string thoi_gian = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ServiceReq_ThoiGianKetThuc);
                    string finish_time = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(currentServiceReq.FINISH_TIME ?? 0);
                    message = String.Format("{0} ({1}: {2})", ket_thuc_kham, thoi_gian, finish_time);
                }

                new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceReq_XuTriKham, message).ServiceReqCode(currentServiceReq.SERVICE_REQ_CODE).TreatmentCode(currentServiceReq.TDL_TREATMENT_CODE).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
