using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisPackage;
using MOS.MANAGER.HisPatientClassify;
using MOS.MANAGER.HisRationTime;
using MOS.MANAGER.HisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisServicePatyLog
    {
        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal static void Run(HIS_SERVICE_PATY editData, HIS_SERVICE_PATY oldData, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                V_HIS_SERVICE hisServiceOld = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == oldData.SERVICE_ID);

                if (oldData != null && editData != null)
                {
                    if (IsDiffLong(oldData.PATIENT_TYPE_ID, editData.PATIENT_TYPE_ID))
                    {
                        HIS_PATIENT_TYPE patientTypeOld = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.PATIENT_TYPE_ID);
                        HIS_PATIENT_TYPE patientTypeEdit = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.PATIENT_TYPE_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThongTinDoiTuongThanhToan);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, patientTypeOld.PATIENT_TYPE_NAME, patientTypeEdit.PATIENT_TYPE_NAME));
                    }
                    if (IsDiffLong(oldData.BRANCH_ID, editData.BRANCH_ID))
                    {
                        HIS_BRANCH branchOld = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == oldData.BRANCH_ID);
                        HIS_BRANCH branchEdit = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == editData.BRANCH_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChiNhanh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, branchOld.BRANCH_NAME, branchEdit.BRANCH_NAME));
                    }

                    if (IsDiffLong(oldData.PATIENT_CLASSIFY_ID, editData.PATIENT_CLASSIFY_ID))
                    {
                        HIS_PATIENT_CLASSIFY patientClassifyOld = new HisPatientClassifyGet().GetById(oldData.PATIENT_CLASSIFY_ID ?? 0);
                        HIS_PATIENT_CLASSIFY patientClassifyEdit = new HisPatientClassifyGet().GetById(editData.PATIENT_CLASSIFY_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongChiTiet);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, patientClassifyOld != null ? patientClassifyOld.PATIENT_CLASSIFY_NAME : "", patientClassifyEdit != null ? patientClassifyEdit.PATIENT_CLASSIFY_NAME : ""));
                    }
                    if (IsDiffDecimal(oldData.PRICE, editData.PRICE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Gia);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PRICE, editData.PRICE));
                    }
                    if (IsDiffDecimal(oldData.OVERTIME_PRICE, editData.OVERTIME_PRICE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaChenhLech);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.OVERTIME_PRICE, editData.OVERTIME_PRICE));
                    }
                    if (IsDiffLong(oldData.PRIORITY, editData.PRIORITY))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoUuTien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PRIORITY, editData.PRIORITY));
                    }

                    if (IsDiffLong(oldData.PACKAGE_ID, editData.PACKAGE_ID))
                    {
                        HIS_PACKAGE packageEdit = new HisPackageGet().GetById(editData.PACKAGE_ID ?? 0);
                        HIS_PACKAGE packageOld = new HisPackageGet().GetById(oldData.PACKAGE_ID ?? 0);

                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GoiDichVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, packageOld != null ? packageOld.PACKAGE_NAME : "", packageEdit != null ? packageEdit.PACKAGE_NAME : ""));
                    }
                    if (IsDiffLong(oldData.INTRUCTION_NUMBER_FROM, editData.INTRUCTION_NUMBER_FROM))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuLanChiDinhThu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.INTRUCTION_NUMBER_FROM, editData.INTRUCTION_NUMBER_FROM));
                    }
                    if (IsDiffLong(oldData.INTRUCTION_NUMBER_TO, editData.INTRUCTION_NUMBER_TO))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DenLanChiDinhThu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.INTRUCTION_NUMBER_TO, editData.INTRUCTION_NUMBER_TO));
                    }
                    if (IsDiffLong(oldData.INSTR_NUM_BY_TYPE_FROM, editData.INSTR_NUM_BY_TYPE_FROM))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuLanchiDinhTheoDichVuThu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.INSTR_NUM_BY_TYPE_FROM, editData.INSTR_NUM_BY_TYPE_FROM));
                    }
                    if (IsDiffLong(oldData.INSTR_NUM_BY_TYPE_TO, editData.INSTR_NUM_BY_TYPE_TO))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DenLanChiDinhThu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.INSTR_NUM_BY_TYPE_TO, editData.INSTR_NUM_BY_TYPE_TO));
                    }
                    if (IsDiffLong(oldData.FROM_TIME, editData.FROM_TIME))
                    {
                        string oldFromTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.FROM_TIME ?? 0);
                        string editFromTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.FROM_TIME ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ApDungTu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldFromTime, editFromTime));
                    }
                    if (IsDiffLong(oldData.TO_TIME, editData.TO_TIME))
                    {
                        string oldToTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.TO_TIME ?? 0);
                        string editToTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.TO_TIME ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ApDungDen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldToTime, editToTime));
                    }
                    if (IsDiffLong(oldData.TREATMENT_FROM_TIME, editData.TREATMENT_FROM_TIME))
                    {
                        string oldFromTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.TREATMENT_FROM_TIME ?? 0);
                        string editFromTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.TREATMENT_FROM_TIME ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DieuTriTu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldFromTime, editFromTime));
                    }
                    if (IsDiffLong(oldData.TREATMENT_TO_TIME, editData.TREATMENT_TO_TIME))
                    {
                        string oldToTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(oldData.TREATMENT_TO_TIME ?? 0);
                        string editToTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(editData.TREATMENT_TO_TIME ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DieuTriDen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldToTime, editToTime));
                    }
                    if (IsDiffShort(oldData.DAY_FROM, editData.DAY_FROM))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Thutu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DAY_FROM, editData.DAY_FROM));
                    }
                    if (IsDiffShort(oldData.DAY_TO, editData.DAY_TO))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThuDen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DAY_TO, editData.DAY_TO));
                    }
                    if (IsDiffString(oldData.HOUR_FROM, editData.HOUR_FROM))
                    {
                        string hourFromOld = oldData.HOUR_FROM != null ? string.Format("{0}: {1}", oldData.HOUR_FROM.Substring(0, 2), oldData.HOUR_FROM.Substring(2, 2)) : "";
                        string hourFromEdit = editData.HOUR_FROM != null ? string.Format("{0}: {1}", editData.HOUR_FROM.Substring(0, 2), editData.HOUR_FROM.Substring(2, 2)) : "";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GioTu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, hourFromOld, hourFromEdit));
                    }
                    if (IsDiffString(oldData.HOUR_TO, editData.HOUR_TO))
                    {
                        string hourToOld = oldData.HOUR_TO != null ? string.Format("{0}: {1}", oldData.HOUR_TO.Substring(0, 2), oldData.HOUR_TO.Substring(2, 2)) : "";
                        string hourToEdit = editData.HOUR_TO != null ? string.Format("{0}: {1}", editData.HOUR_TO.Substring(0, 2), editData.HOUR_TO.Substring(2, 2)) : "";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GioDen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, hourToOld, hourToEdit));
                    }
                    if (IsDiffDecimal(oldData.VAT_RATIO, editData.VAT_RATIO))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VAT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.VAT_RATIO, editData.VAT_RATIO));
                    }
                    if (IsDiffDecimal(oldData.ACTUAL_PRICE, editData.ACTUAL_PRICE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaThucTe);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ACTUAL_PRICE, editData.ACTUAL_PRICE));
                    }

                    if (IsDiffLong(oldData.RATION_TIME_ID, editData.RATION_TIME_ID))
                    {
                        HIS_RATION_TIME rationTimeOld = new HisRationTimeGet().GetById(oldData.RATION_TIME_ID ?? 0);
                        HIS_RATION_TIME rationTimeEdit = new HisRationTimeGet().GetById(editData.RATION_TIME_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BuaAn);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, rationTimeOld != null ? rationTimeOld.RATION_TIME_NAME : "", rationTimeEdit != null ? rationTimeEdit.RATION_TIME_NAME : ""));
                    }
                    if (IsDiffLong(oldData.SERVICE_CONDITION_ID, editData.SERVICE_CONDITION_ID))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DieuKienDichVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SERVICE_CONDITION_ID, editData.SERVICE_CONDITION_ID));
                    }
                    new EventLogGenerator(logEnum, String.Join(". ", editFields))
                     .ServiceCode(hisServiceOld.SERVICE_CODE)
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
    }
}
