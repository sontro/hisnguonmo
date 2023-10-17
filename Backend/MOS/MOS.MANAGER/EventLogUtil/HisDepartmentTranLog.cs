using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBed;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisDepartmentTranLog
    {
        internal static void LogCreate(HIS_TREATMENT treatment, HIS_DEPARTMENT department, HIS_DEPARTMENT_TRAN dt, WorkPlaceSDO wp)
        {
            try
            {
                List<string> logs = new List<string>();

                string treatmentCode = treatment != null ? treatment.TREATMENT_CODE : "";

                string khoaDen = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_KhoaDen);
                logs.Add(String.Format("{0}: {1}", khoaDen, department.DEPARTMENT_NAME));

                string phongYeuCau = LogCommonUtil.GetEventLogContent(EventLog.Enum.PhongYeuCau);
                logs.Add(String.Format("{0}: {1}", phongYeuCau, wp.RoomName));

                if (dt.DEPARTMENT_IN_TIME.HasValue)
                {
                    string tudong = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_TuDongTiepNhan);
                    string co = LogCommonUtil.GetEventLogContent(EventLog.Enum.Co);
                    logs.Add(String.Format("{0}: {1}", tudong, co));

                    string thoigian = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_ThoiGianTiepNhan);
                    string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(dt.DEPARTMENT_IN_TIME.Value);
                    logs.Add(String.Format("{0}: {1}", thoigian, inTime));
                }
                else
                {
                    string tudong = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_TuDongTiepNhan);
                    string khong = LogCommonUtil.GetEventLogContent(EventLog.Enum.Khong);
                    logs.Add(String.Format("{0}: {1}", tudong, khong));
                }

                new EventLogGenerator(EventLog.Enum.HisDepartmentTran_ChuyenKhoa, String.Join(", ", logs)).TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static void LogReceive(HisDepartmentTranReceiveSDO inData, HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN dt)
        {
            try
            {
                List<string> logs = new List<string>();

                string treatmentCode = treatment != null ? treatment.TREATMENT_CODE : "";

                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == dt.DEPARTMENT_ID);
                string depaName = department != null ? department.DEPARTMENT_NAME : "";
                string khoaDen = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_KhoaDen);
                logs.Add(String.Format("{0}: {1}", khoaDen, depaName));

                V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == inData.RequestRoomId);
                string roomName = room != null ? room.ROOM_NAME : "";
                string phongTiepNhan = LogCommonUtil.GetEventLogContent(EventLog.Enum.PhongTiepNhan);
                logs.Add(String.Format("{0}: {1}", phongTiepNhan, roomName));

                string thoigian = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_ThoiGianTiepNhan);
                string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(dt.DEPARTMENT_IN_TIME ?? 0);
                logs.Add(String.Format("{0}: {1}", thoigian, inTime));

                if (inData.TreatmentTypeId.HasValue)
                {
                    HIS_TREATMENT_TYPE type = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == inData.TreatmentTypeId.Value);
                    string name = type != null ? type.TREATMENT_TYPE_NAME : "";
                    string dienDT = LogCommonUtil.GetEventLogContent(EventLog.Enum.DienDieuTri);
                    logs.Add(String.Format("{0}: {1}", dienDT, name));
                }

                if (inData.BedRoomId.HasValue)
                {
                    V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.FirstOrDefault(o => o.ID == inData.BedRoomId.Value);
                    string name = bedRoom != null ? bedRoom.BED_ROOM_NAME : "";
                    string buong = LogCommonUtil.GetEventLogContent(EventLog.Enum.BuongBenh);
                    logs.Add(String.Format("{0}: {1}", buong, name));
                }

                if (inData.BedId.HasValue)
                {
                    HIS_BED bed = new HisBedGet().GetById(inData.BedId.Value);
                    string name = bed != null ? bed.BED_NAME : "";
                    string giuong = LogCommonUtil.GetEventLogContent(EventLog.Enum.GiuongBenh);
                    logs.Add(String.Format("{0}: {1}", giuong, name));
                }

                new EventLogGenerator(EventLog.Enum.HisDepartmentTran_TiepNhanChuyenKhoa, String.Join(", ", logs)).TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static void LogHospitalize(HisDepartmentTranHospitalizeSDO inData, HIS_TREATMENT treatment, WorkPlaceSDO wp)
        {
            try
            {
                List<string> logs = new List<string>();

                string treatmentCode = treatment != null ? treatment.TREATMENT_CODE : "";

                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == inData.DepartmentId);
                string depaName = department != null ? department.DEPARTMENT_NAME : "";
                string khoaDen = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_KhoaDen);
                logs.Add(String.Format("{0}: {1}", khoaDen, depaName));

                string phongYeuCau = LogCommonUtil.GetEventLogContent(EventLog.Enum.PhongYeuCau);
                logs.Add(String.Format("{0}: {1}", phongYeuCau, wp.RoomName));

                string thoigian = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianYeuCau);
                string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(inData.Time);
                logs.Add(String.Format("{0}: {1}", thoigian, inTime));

                HIS_TREATMENT_TYPE type = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == inData.TreatmentTypeId);
                string typeName = type != null ? type.TREATMENT_TYPE_NAME : "";
                string dienDT = LogCommonUtil.GetEventLogContent(EventLog.Enum.DienDieuTri);
                logs.Add(String.Format("{0}: {1}", dienDT, typeName));

                if (inData.BedRoomId.HasValue)
                {
                    V_HIS_BED_ROOM bedRoom = HisBedRoomCFG.DATA.FirstOrDefault(o => o.ID == inData.BedRoomId.Value);
                    string name = bedRoom != null ? bedRoom.BED_ROOM_NAME : "";
                    string buong = LogCommonUtil.GetEventLogContent(EventLog.Enum.BuongBenh);
                    logs.Add(String.Format("{0}: {1}", buong, name));
                }

                new EventLogGenerator(EventLog.Enum.HisDepartmentTran_NhapVien, String.Join(", ", logs)).TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static void LogDelete(HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN dt)
        {
            try
            {
                List<string> logs = new List<string>();

                string treatmentCode = treatment != null ? treatment.TREATMENT_CODE : "";

                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == dt.DEPARTMENT_ID);
                string depaName = department != null ? department.DEPARTMENT_NAME : "";
                string khoaDen = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_KhoaDen);
                logs.Add(String.Format("{0}: {1}", khoaDen, depaName));

                if (dt.DEPARTMENT_IN_TIME.HasValue)
                {
                    string thoigian = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianVaoKhoa);
                    string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(dt.DEPARTMENT_IN_TIME.Value);
                    logs.Add(String.Format("{0}: {1}", thoigian, inTime));
                }

                new EventLogGenerator(EventLog.Enum.HisDepartmentTran_Xoa, String.Join(", ", logs)).TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal static void LogUpdate(HIS_DEPARTMENT_TRAN data, HIS_DEPARTMENT_TRAN before, HIS_TREATMENT treatment)
        {
            try
            {
                List<string> logs = new List<string>();

                string treatmentCode = treatment != null ? treatment.TREATMENT_CODE : "";

                HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == data.DEPARTMENT_ID);
                string depaName = department != null ? department.DEPARTMENT_NAME : "";
                string khoaDen = LogCommonUtil.GetEventLogContent(EventLog.Enum.HisDepartmentTran_KhoaDen);
                logs.Add(String.Format("{0}: {1}", khoaDen, depaName));

                if ((data.DEPARTMENT_IN_TIME ?? 0) != (before.DEPARTMENT_IN_TIME ?? 0))
                {
                    string thoigian = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianYeuCau);
                    string beforeTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(before.DEPARTMENT_IN_TIME ?? 0);
                    string afterTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(data.DEPARTMENT_IN_TIME ?? 0);
                    logs.Add(String.Format("{0}: {1} => {2}", thoigian, beforeTime, afterTime));
                }

                new EventLogGenerator(EventLog.Enum.HisDepartmentTran_SuaThongTinChuyenKhoa, String.Join(", ", logs)).TreatmentCode(treatmentCode).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
