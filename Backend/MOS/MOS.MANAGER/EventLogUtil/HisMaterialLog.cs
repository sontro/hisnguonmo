using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMedicalContract;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisMaterialLog
    {
        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal static void Run(HIS_MATERIAL editData, HIS_MATERIAL oldData, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);
               

                if (IsDiffDecimal(oldData.IMP_PRICE, editData.IMP_PRICE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IMP_PRICE, editData.IMP_PRICE));
                }
                if (IsDiffDecimal(oldData.PROFIT_RATIO, editData.PROFIT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoiNhuan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PROFIT_RATIO, editData.PROFIT_RATIO));
                }
                if (IsDiffString(oldData.TDL_BID_NUM_ORDER, editData.TDL_BID_NUM_ORDER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_BID_NUM_ORDER, editData.TDL_BID_NUM_ORDER));
                }
                if (IsDiffString(oldData.TDL_BID_GROUP_CODE, editData.TDL_BID_GROUP_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_BID_GROUP_CODE, editData.TDL_BID_GROUP_CODE));
                }

                if (IsDiffString(oldData.TDL_BID_YEAR, editData.TDL_BID_YEAR))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NamThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_BID_YEAR, editData.TDL_BID_YEAR));
                }
                if (IsDiffString(oldData.TDL_BID_EXTRA_CODE, editData.TDL_BID_EXTRA_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaQuyetDinhThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_BID_EXTRA_CODE, editData.TDL_BID_EXTRA_CODE));
                }
                if (IsDiffString(oldData.BID_MATERIAL_TYPE_CODE, editData.BID_MATERIAL_TYPE_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaTrungThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.BID_MATERIAL_TYPE_CODE, editData.BID_MATERIAL_TYPE_CODE));
                }
                if (IsDiffString(oldData.BID_MATERIAL_TYPE_NAME, editData.BID_MATERIAL_TYPE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenTrungThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.BID_MATERIAL_TYPE_NAME, editData.BID_MATERIAL_TYPE_NAME));
                }

                if (IsDiffLong(oldData.MEDICAL_CONTRACT_ID, editData.MEDICAL_CONTRACT_ID))
                {
                    HIS_MEDICAL_CONTRACT medicalContractOld = new HisMedicalContractGet().GetById(oldData.MEDICAL_CONTRACT_ID ?? 0);
                    HIS_MEDICAL_CONTRACT medicalContractEdit = new HisMedicalContractGet().GetById(editData.MEDICAL_CONTRACT_ID ?? 0);
                    if (IsDiffString(medicalContractOld.MEDICAL_CONTRACT_CODE, medicalContractEdit.MEDICAL_CONTRACT_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaHopDong);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, medicalContractOld.MEDICAL_CONTRACT_CODE, medicalContractEdit.MEDICAL_CONTRACT_CODE));
                    }
                    if (IsDiffString(medicalContractOld.MEDICAL_CONTRACT_NAME, editData.HIS_MEDICAL_CONTRACT.MEDICAL_CONTRACT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenHopDong);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, medicalContractOld.MEDICAL_CONTRACT_NAME, medicalContractEdit.MEDICAL_CONTRACT_NAME));
                    }
                }

                if (IsDiffDecimal(oldData.IMP_VAT_RATIO, editData.IMP_VAT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VAT);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IMP_VAT_RATIO, editData.IMP_VAT_RATIO));
                }

                if (IsDiffString(oldData.PACKAGE_NUMBER, editData.PACKAGE_NUMBER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PACKAGE_NUMBER, editData.PACKAGE_NUMBER));
                }
                if (IsDiffLong(oldData.EXPIRED_DATE, editData.EXPIRED_DATE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CanhBaoHanSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.EXPIRED_DATE, editData.EXPIRED_DATE));
                }
                if (IsDiffString(oldData.TDL_BID_PACKAGE_CODE, editData.TDL_BID_PACKAGE_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GoiThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_BID_PACKAGE_CODE, editData.TDL_BID_PACKAGE_CODE));
                }

                if (IsDiffString(oldData.TDL_BID_NUMBER, editData.TDL_BID_NUMBER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuyetDinhThau);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TDL_BID_NUMBER, editData.TDL_BID_NUMBER));
                }
                if (IsDiffShortIsField(oldData.IS_SALE_EQUAL_IMP_PRICE, editData.IS_SALE_EQUAL_IMP_PRICE))
                {
                    string newValue = editData.IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE ? khong : co;
                    string oldValue = oldData.IS_SALE_EQUAL_IMP_PRICE != Constant.IS_TRUE ? khong : co;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BBGN);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }

                new EventLogGenerator(logEnum, String.Join(". ", editFields))
                    .MaterialId(oldData.ID.ToString())
                    .MaterialTypeCode(oldData.MATERIAL_TYPE_ID.ToString())
                    .Run();
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
        private static bool IsDiffShortIsField(short? oldValue, short? newValue)
        {
            return (((oldValue == Constant.IS_TRUE) && (newValue != Constant.IS_TRUE)) || ((oldValue != Constant.IS_TRUE) && (newValue == Constant.IS_TRUE)));
        }
    }
}
