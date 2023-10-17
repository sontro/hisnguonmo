using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBirthCertBook;
using MOS.MANAGER.HisBornPosition;
using MOS.MANAGER.HisBornType;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisBabyLog
    {
        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal static void Run(HIS_TREATMENT oldData, HIS_TREATMENT editData, HIS_BABY baby, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (oldData != null && editData != null)
                {
                    if (IsDiffString(oldData.TDL_PATIENT_PROVINCE_CODE, editData.TDL_PATIENT_PROVINCE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaTinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_PROVINCE_CODE, editData.TDL_PATIENT_PROVINCE_CODE));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_PROVINCE_NAME, editData.TDL_PATIENT_PROVINCE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenTinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_PROVINCE_NAME, editData.TDL_PATIENT_PROVINCE_NAME));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_DISTRICT_CODE, editData.TDL_PATIENT_DISTRICT_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaHuyen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_DISTRICT_CODE, editData.TDL_PATIENT_DISTRICT_CODE));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_DISTRICT_NAME, editData.TDL_PATIENT_DISTRICT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenHuyen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_DISTRICT_NAME, editData.TDL_PATIENT_DISTRICT_NAME));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_COMMUNE_CODE, editData.TDL_PATIENT_COMMUNE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaXa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_COMMUNE_CODE, editData.TDL_PATIENT_COMMUNE_CODE));
                    }

                    if (IsDiffString(oldData.TDL_PATIENT_COMMUNE_NAME, editData.TDL_PATIENT_COMMUNE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenXa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_COMMUNE_NAME, editData.TDL_PATIENT_COMMUNE_NAME));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_ADDRESS, editData.TDL_PATIENT_ADDRESS))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DiaChi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_ADDRESS, editData.TDL_PATIENT_ADDRESS));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_CCCD_NUMBER, editData.TDL_PATIENT_CCCD_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CCCD);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_CCCD_NUMBER, editData.TDL_PATIENT_CCCD_NUMBER));
                    }

                    if (IsDiffString(oldData.TDL_PATIENT_CCCD_PLACE, editData.TDL_PATIENT_CCCD_PLACE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiCapCCCD);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_CCCD_PLACE, editData.TDL_PATIENT_CCCD_PLACE));
                    }
                    if (IsDiffLong(oldData.TDL_PATIENT_CCCD_DATE, editData.TDL_PATIENT_CCCD_DATE))
                    {
                        string dobOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldData.TDL_PATIENT_CCCD_DATE ?? 0));
                        string dobEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(editData.TDL_PATIENT_CCCD_DATE ?? 0));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayCapCCCD);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, dobOld, dobEdit));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_CMND_NUMBER, editData.TDL_PATIENT_CMND_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Cmnd1);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_CMND_NUMBER, editData.TDL_PATIENT_CMND_NUMBER));
                    }

                    if (IsDiffString(oldData.TDL_PATIENT_CMND_PLACE, editData.TDL_PATIENT_CMND_PLACE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiCapCMND);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_CMND_PLACE, editData.TDL_PATIENT_CMND_PLACE));
                    }
                    if (IsDiffLong(oldData.TDL_PATIENT_CMND_DATE, editData.TDL_PATIENT_CMND_DATE))
                    {
                        string dobOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldData.TDL_PATIENT_CMND_DATE ?? 0));
                        string dobEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(editData.TDL_PATIENT_CMND_DATE ?? 0));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayCapCMND);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, dobOld, dobEdit));
                    }
                    if (IsDiffString(oldData.TDL_PATIENT_PASSPORT_NUMBER, editData.TDL_PATIENT_PASSPORT_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HoChieu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_PASSPORT_NUMBER, editData.TDL_PATIENT_PASSPORT_NUMBER));
                    }

                    if (IsDiffString(oldData.TDL_PATIENT_PASSPORT_PLACE, editData.TDL_PATIENT_PASSPORT_PLACE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiCapHoChieu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_PATIENT_PASSPORT_PLACE, editData.TDL_PATIENT_PASSPORT_PLACE));
                    }
                    if (IsDiffLong(oldData.TDL_PATIENT_PASSPORT_DATE, editData.TDL_PATIENT_PASSPORT_DATE))
                    {
                        string dobOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldData.TDL_PATIENT_PASSPORT_DATE ?? 0));
                        string dobEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(editData.TDL_PATIENT_PASSPORT_DATE ?? 0));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayCapHoChieu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, dobOld, dobEdit));
                    }

                    new EventLogGenerator(logEnum, baby.BIRTH_CERT_NUM, String.Join(". ", editFields))
                     .TreatmentCode(oldData.TREATMENT_CODE)
                     .PatientCode(oldData.TDL_PATIENT_CODE)
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
