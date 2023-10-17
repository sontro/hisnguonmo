using Inventec.Common.ObjectChecker;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisActiveIngredient;
using MOS.MANAGER.HisBloodGroup;
using MOS.MANAGER.HisBloodVolume;
using MOS.MANAGER.HisManufacturer;
using MOS.MANAGER.HisMedicineGroup;
using MOS.MANAGER.HisMedicineLine;
using MOS.MANAGER.HisMedicineUseForm;
using MOS.MANAGER.HisPackingType;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisBloodTypeLog
    {
        private static string FORMAT_EDIT = "{0}({1}=>{2})";
        private static string FORMAT_FIELD = "{0}({1})";

        internal static void Run(HIS_BLOOD_TYPE editData, HIS_BLOOD_TYPE oldData, HIS_SERVICE editService, HIS_SERVICE oldService, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (IsDiffString(oldData.BLOOD_TYPE_CODE, editData.BLOOD_TYPE_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ma);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.BLOOD_TYPE_CODE, editData.BLOOD_TYPE_CODE));
                }
                if (IsDiffString(oldData.BLOOD_TYPE_NAME, editData.BLOOD_TYPE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ten);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.BLOOD_TYPE_NAME, editData.BLOOD_TYPE_NAME));
                }
                if (IsDiffLong(oldData.NUM_ORDER, editData.NUM_ORDER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTHien);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NUM_ORDER, editData.NUM_ORDER));
                }
                if (IsDiffDecimal(oldData.IMP_PRICE, editData.IMP_PRICE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IMP_PRICE, editData.IMP_PRICE));
                }
                if (IsDiffDecimal(oldData.IMP_VAT_RATIO, editData.IMP_VAT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IMP_VAT_RATIO, editData.IMP_VAT_RATIO));
                }
                if (IsDiffDecimal(oldData.INTERNAL_PRICE, editData.INTERNAL_PRICE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaNoiBo);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.INTERNAL_PRICE, editData.INTERNAL_PRICE));
                }
                if (IsDiffLong(oldData.ALERT_EXPIRED_DATE, editData.ALERT_EXPIRED_DATE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CanhBaoHanSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ALERT_EXPIRED_DATE, editData.ALERT_EXPIRED_DATE));
                }
                if (IsDiffString(oldData.ELEMENT, editData.ELEMENT))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThanhPhan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ELEMENT, editData.ELEMENT));
                }

                if (IsDiffLong(oldData.PARENT_ID, editData.PARENT_ID))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Cha);
                    HIS_BLOOD_TYPE newP = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.PARENT_ID);
                    HIS_BLOOD_TYPE oldP = HisBloodTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.PARENT_ID);
                    string newCha = newP != null ? newP.BLOOD_TYPE_NAME : "";
                    string oldCha = oldP != null ? oldP.BLOOD_TYPE_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName ?? "", oldCha ?? "", newCha ?? ""));
                }
                if (IsDiffLong(oldData.BLOOD_VOLUME_ID, editData.BLOOD_VOLUME_ID))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DungTich);
                    HIS_BLOOD_VOLUME oldM = new HisBloodVolumeGet().GetById(oldData.BLOOD_VOLUME_ID);
                    HIS_BLOOD_VOLUME newM = new HisBloodVolumeGet().GetById(editData.BLOOD_VOLUME_ID);
                    decimal newValue = newM != null ? newM.VOLUME : 0;
                    decimal oldValue = oldM != null ? oldM.VOLUME : 0;
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffLong(oldData.BLOOD_GROUP_ID, editData.BLOOD_GROUP_ID))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Nhom);
                    HIS_BLOOD_GROUP newM = null;
                    HIS_BLOOD_GROUP oldM = null;
                    if (oldData.BLOOD_GROUP_ID.HasValue)
                    {
                        oldM = new HisBloodGroupGet().GetById(oldData.BLOOD_GROUP_ID.Value);
                    }
                    if (editData.BLOOD_GROUP_ID.HasValue)
                    {
                        newM = new HisBloodGroupGet().GetById(editData.BLOOD_GROUP_ID.Value);
                    }
                    string newValue = newM != null ? newM.BLOOD_GROUP_NAME : "";
                    string oldValue = oldM != null ? oldM.BLOOD_GROUP_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffLong(oldData.PACKING_TYPE_ID, editData.PACKING_TYPE_ID))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiDongGoi);
                    HIS_PACKING_TYPE newL = null;
                    HIS_PACKING_TYPE oldL = null;
                    if (oldData.PACKING_TYPE_ID.HasValue)
                    {
                        oldL = new HisPackingTypeGet().GetById(oldData.PACKING_TYPE_ID.Value);
                    }
                    if (editData.PACKING_TYPE_ID.HasValue)
                    {
                        newL = new HisPackingTypeGet().GetById(editData.PACKING_TYPE_ID.Value);
                    }
                    string newValue = newL != null ? newL.PACKING_TYPE_NAME : "";
                    string oldValue = oldL != null ? oldL.PACKING_TYPE_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffLong(oldData.TDL_SERVICE_UNIT_ID, editData.TDL_SERVICE_UNIT_ID))
                {
                    string donvitinh = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DonViTinh);
                    HIS_SERVICE_UNIT oldUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == oldData.TDL_SERVICE_UNIT_ID);
                    HIS_SERVICE_UNIT newUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == editData.TDL_SERVICE_UNIT_ID);
                    string newDonVi = newUnit != null ? newUnit.SERVICE_UNIT_NAME : "";
                    string oldDonVi = oldUnit != null ? oldUnit.SERVICE_UNIT_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, donvitinh ?? "", oldDonVi ?? "", newDonVi ?? ""));
                }

                if (editService != null && oldService != null)
                {
                    if (IsDiffString(oldService.HEIN_SERVICE_BHYT_CODE, editService.HEIN_SERVICE_BHYT_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_SERVICE_BHYT_CODE, editService.HEIN_SERVICE_BHYT_CODE));
                    }
                    if (IsDiffString(oldService.HEIN_SERVICE_BHYT_NAME, editService.HEIN_SERVICE_BHYT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_SERVICE_BHYT_NAME, editService.HEIN_SERVICE_BHYT_NAME));
                    }
                    if (IsDiffString(oldService.HEIN_ORDER, editService.HEIN_ORDER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_ORDER, editService.HEIN_ORDER));
                    }
                    if (IsDiffDecimal(oldService.HEIN_LIMIT_RATIO, editService.HEIN_LIMIT_RATIO))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TyLeBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_LIMIT_RATIO, editService.HEIN_LIMIT_RATIO));
                    }
                    if (IsDiffDecimal(oldService.HEIN_LIMIT_RATIO_OLD, editService.HEIN_LIMIT_RATIO_OLD))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TyLeBHYTCu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_LIMIT_RATIO_OLD, editService.HEIN_LIMIT_RATIO_OLD));
                    }
                    if (IsDiffDecimal(oldService.HEIN_LIMIT_PRICE, editService.HEIN_LIMIT_PRICE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaTranBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_LIMIT_PRICE, editService.HEIN_LIMIT_PRICE));
                    }
                    if (IsDiffDecimal(oldService.HEIN_LIMIT_PRICE_OLD, editService.HEIN_LIMIT_PRICE_OLD))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaTranBHYTCu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_LIMIT_PRICE_OLD, editService.HEIN_LIMIT_PRICE_OLD));
                    }
                    if (IsDiffLong(oldService.HEIN_LIMIT_PRICE_IN_TIME, editService.HEIN_LIMIT_PRICE_IN_TIME))
                    {
                        string newValue = Inventec.Common.DateTime.Convert.TimeNumberToDateString(editService.HEIN_LIMIT_PRICE_IN_TIME ?? 0);
                        string oldValue = Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldService.HEIN_LIMIT_PRICE_IN_TIME ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayVaoVienApDungTranBHYTMoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffLong(oldService.HEIN_LIMIT_PRICE_IN_TIME, editService.HEIN_LIMIT_PRICE_IN_TIME))
                    {
                        string newValue = Inventec.Common.DateTime.Convert.TimeNumberToDateString(editService.HEIN_LIMIT_PRICE_IN_TIME ?? 0);
                        string oldValue = Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldService.HEIN_LIMIT_PRICE_IN_TIME ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayChiDinhApDungTranBHYTMoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldService.IS_OUT_PARENT_FEE, editService.IS_OUT_PARENT_FEE))
                    {
                        string newValue = editService.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChiPhiNgoaiGoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffLong(oldService.HEIN_SERVICE_TYPE_ID, editService.HEIN_SERVICE_TYPE_ID))
                    {
                        string gioitinh = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomBhyt);
                        HIS_HEIN_SERVICE_TYPE newO = null;
                        HIS_HEIN_SERVICE_TYPE oldO = null;
                        if (editService.HEIN_SERVICE_TYPE_ID.HasValue)
                        {
                            newO = HisHeinServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == editService.HEIN_SERVICE_TYPE_ID.Value);
                        }
                        if (oldService.HEIN_SERVICE_TYPE_ID.HasValue)
                        {
                            oldO = HisHeinServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == oldService.HEIN_SERVICE_TYPE_ID.Value); ;
                        }
                        string newValue = newO != null ? newO.HEIN_SERVICE_TYPE_NAME : "";
                        string oldValue = oldO != null ? oldO.HEIN_SERVICE_TYPE_NAME : "";
                        editFields.Add(String.Format(FORMAT_EDIT, gioitinh ?? "", oldValue ?? "", newValue ?? ""));
                    }
                }

                new EventLogGenerator(logEnum, String.Join(". ", editFields))
                    .BloodTypeId(oldData.ID.ToString())
                    .BloodTypeCode(oldData.BLOOD_TYPE_CODE)
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
