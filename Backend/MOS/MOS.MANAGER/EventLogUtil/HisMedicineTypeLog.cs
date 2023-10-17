using Inventec.Common.ObjectChecker;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisActiveIngredient;
using MOS.MANAGER.HisContraindication;
using MOS.MANAGER.HisManufacturer;
using MOS.MANAGER.HisMedicineGroup;
using MOS.MANAGER.HisMedicineLine;
using MOS.MANAGER.HisMedicineUseForm;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisMedicineTypeLog
    {
        private static string FORMAT_EDIT = "{0}: {1} => {2}";
        private static string FORMAT_FIELD = "{0}({1})";

        internal static void Run(HIS_MEDICINE_TYPE editData, HIS_MEDICINE_TYPE oldData, HIS_SERVICE editService, HIS_SERVICE oldService, List<HIS_MEDICINE_TYPE_ACIN> inserts, List<HIS_MEDICINE_TYPE_ACIN> deletes, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (IsDiffString(oldData.MEDICINE_TYPE_CODE, editData.MEDICINE_TYPE_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ma);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MEDICINE_TYPE_CODE, editData.MEDICINE_TYPE_CODE));
                }
                if (IsDiffString(oldData.MEDICINE_TYPE_NAME, editData.MEDICINE_TYPE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ten);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MEDICINE_TYPE_NAME, editData.MEDICINE_TYPE_NAME));
                }
                if (IsDiffString(oldData.SCIENTIFIC_NAME, editData.SCIENTIFIC_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenKhoaHoc);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SCIENTIFIC_NAME, editData.SCIENTIFIC_NAME));
                }
                if (IsDiffLong(oldData.NUM_ORDER, editData.NUM_ORDER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTHien);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NUM_ORDER, editData.NUM_ORDER));
                }
                if (IsDiffString(oldData.CONCENTRA, editData.CONCENTRA))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HamLuong);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CONCENTRA, editData.CONCENTRA));
                }
                if (IsDiffString(oldData.ACTIVE_INGR_BHYT_CODE, editData.ACTIVE_INGR_BHYT_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaHoatChatBhyt);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ACTIVE_INGR_BHYT_CODE, editData.ACTIVE_INGR_BHYT_CODE));
                }
                if (IsDiffString(oldData.ACTIVE_INGR_BHYT_NAME, editData.ACTIVE_INGR_BHYT_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenHoatChatBhyt);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ACTIVE_INGR_BHYT_NAME, editData.ACTIVE_INGR_BHYT_NAME));
                }
                if (IsDiffString(oldData.REGISTER_NUMBER, editData.REGISTER_NUMBER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoDangKy);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.REGISTER_NUMBER, editData.REGISTER_NUMBER));
                }
                if (IsDiffString(oldData.NATIONAL_NAME, editData.NATIONAL_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuocGia);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NATIONAL_NAME, editData.NATIONAL_NAME));
                }
                if (IsDiffString(oldData.TUTORIAL, editData.TUTORIAL))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HuongDanSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TUTORIAL, editData.TUTORIAL));
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
                if (IsDiffDecimal(oldData.LAST_EXP_PRICE, editData.LAST_EXP_PRICE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaBan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.LAST_EXP_PRICE, editData.LAST_EXP_PRICE));
                }

                if (IsDiffDecimal(oldData.ALERT_MAX_IN_TREATMENT, editData.ALERT_MAX_IN_TREATMENT))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CanhBaoToiDaDotDieuTri);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ALERT_MAX_IN_TREATMENT, editData.ALERT_MAX_IN_TREATMENT));
                }
                if (IsDiffLong(oldData.ALERT_EXPIRED_DATE, editData.ALERT_EXPIRED_DATE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CanhBaoHanSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ALERT_EXPIRED_DATE, editData.ALERT_EXPIRED_DATE));
                }
                if (IsDiffDecimal(oldData.ALERT_MIN_IN_STOCK, editData.ALERT_MIN_IN_STOCK))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CanhBaoTonKho);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ALERT_MIN_IN_STOCK, editData.ALERT_MIN_IN_STOCK));
                }
                if (IsDiffDecimal(oldData.ALERT_MAX_IN_PRESCRIPTION, editData.ALERT_MAX_IN_PRESCRIPTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CanhBaoToiDaTrenMotDon);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ALERT_MAX_IN_PRESCRIPTION, editData.ALERT_MAX_IN_PRESCRIPTION));
                }

                if (IsDiffShortIsField(oldData.SOURCE_MEDICINE, editData.SOURCE_MEDICINE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguonGoc);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SOURCE_MEDICINE, editData.SOURCE_MEDICINE));
                }

                if (IsDiffString(oldData.QUALITY_STANDARDS, editData.QUALITY_STANDARDS))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TieuChuanChatLuong);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.QUALITY_STANDARDS, editData.QUALITY_STANDARDS));
                }

                if (IsDiffString(oldData.CONTRAINDICATION_IDS, editData.CONTRAINDICATION_IDS))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TruongHopChongChiDinh);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, string.Join(",", oldData.CONTRAINDICATION_IDS), string.Join(",", editData.CONTRAINDICATION_IDS)));
                }
                if (IsDiffLong(oldData.STORAGE_CONDITION_ID, editData.STORAGE_CONDITION_ID))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DieuKienBaoQuan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.STORAGE_CONDITION_ID, editData.STORAGE_CONDITION_ID));
                }
                if (IsDiffLong(oldData.LAST_EXPIRED_DATE, editData.LAST_EXPIRED_DATE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.LAST_EXPIRED_DATE, editData.LAST_EXPIRED_DATE));
                }
                if (IsDiffDecimal(oldData.IMP_UNIT_CONVERT_RATIO, editData.IMP_UNIT_CONVERT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DonViNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IMP_UNIT_CONVERT_RATIO, editData.IMP_UNIT_CONVERT_RATIO));
                }

                if (IsDiffShortIsField(oldData.IS_STOP_IMP, editData.IS_STOP_IMP))
                {
                    string newValue = editData.IS_STOP_IMP == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_STOP_IMP == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DungNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_STAR_MARK, editData.IS_STAR_MARK))
                {
                    string newValue = editData.IS_STAR_MARK == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_STAR_MARK == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThuocDauSao);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_ALLOW_ODD, editData.IS_ALLOW_ODD))
                {
                    string newValue = editData.IS_ALLOW_ODD == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_ALLOW_ODD == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChoKeLe);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_ALLOW_EXPORT_ODD, editData.IS_ALLOW_EXPORT_ODD))
                {
                    string newValue = editData.IS_ALLOW_EXPORT_ODD == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_ALLOW_EXPORT_ODD == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChoXuatLe);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_FUNCTIONAL_FOOD, editData.IS_FUNCTIONAL_FOOD))
                {
                    string newValue = editData.IS_FUNCTIONAL_FOOD == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_FUNCTIONAL_FOOD == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThucPhamChucNang);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_REQUIRE_HSD, editData.IS_REQUIRE_HSD))
                {
                    string newValue = editData.IS_REQUIRE_HSD == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_REQUIRE_HSD == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhaiNhapHanSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_SALE_EQUAL_IMP_PRICE, editData.IS_SALE_EQUAL_IMP_PRICE))
                {
                    string newValue = editData.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_SALE_EQUAL_IMP_PRICE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BanBangGiaNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_BUSINESS, editData.IS_BUSINESS))
                {
                    string newValue = editData.IS_BUSINESS == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_BUSINESS == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KinhDoanh);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_RAW_MEDICINE, editData.IS_RAW_MEDICINE))
                {
                    string newValue = editData.IS_RAW_MEDICINE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_RAW_MEDICINE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguyenLieuBaoChe);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_AUTO_EXPEND, editData.IS_AUTO_EXPEND))
                {
                    string newValue = editData.IS_AUTO_EXPEND == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_AUTO_EXPEND == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuDongHaoPhi);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_VITAMIN_A, editData.IS_VITAMIN_A))
                {
                    string newValue = editData.IS_VITAMIN_A == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_VITAMIN_A == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VitaminA);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_VACCINE, editData.IS_VACCINE))
                {
                    string newValue = editData.IS_VACCINE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_VACCINE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Vaccin);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_TCMR, editData.IS_TCMR))
                {
                    string newValue = editData.IS_TCMR == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_TCMR == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TiemChungMoRong);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_MUST_PREPARE, editData.IS_MUST_PREPARE))
                {
                    string newValue = editData.IS_MUST_PREPARE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_MUST_PREPARE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhaiDuTru);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.ALLOW_MISSING_PKG_INFO, editData.ALLOW_MISSING_PKG_INFO))
                {
                    string newValue = editData.ALLOW_MISSING_PKG_INFO == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.ALLOW_MISSING_PKG_INFO == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongBatBuocNhapSoLoHSD);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_BLOCK_MAX_IN_PRESCRIPTION, editData.IS_BLOCK_MAX_IN_PRESCRIPTION))
                {
                    string newValue = editData.IS_BLOCK_MAX_IN_PRESCRIPTION == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_BLOCK_MAX_IN_PRESCRIPTION == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Chan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_BLOCK_MAX_IN_PRESCRIPTION, editData.IS_BLOCK_MAX_IN_PRESCRIPTION))
                {
                    string newValue = editData.IS_BLOCK_MAX_IN_PRESCRIPTION == Constant.IS_TRUE ? khong : co;
                    string oldValue = oldData.IS_BLOCK_MAX_IN_PRESCRIPTION == Constant.IS_TRUE ? khong : co;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CanhBao);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_TREATMENT_DAY_COUNT, editData.IS_TREATMENT_DAY_COUNT))
                {
                    string newValue = editData.IS_TREATMENT_DAY_COUNT == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_TREATMENT_DAY_COUNT == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DemSoNgayDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_NOT_TREATMENT_DAY_COUNT, editData.IS_NOT_TREATMENT_DAY_COUNT))
                {
                    string newValue = editData.IS_NOT_TREATMENT_DAY_COUNT == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_NOT_TREATMENT_DAY_COUNT == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongDemSoNgayDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_OUT_HOSPITAL, editData.IS_OUT_HOSPITAL))
                {
                    string newValue = editData.IS_OUT_HOSPITAL == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_OUT_HOSPITAL == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThuocNgoaiVien);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffString(oldData.DESCRIPTION, editData.DESCRIPTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DESCRIPTION, editData.DESCRIPTION));
                }
                if (IsDiffString(oldData.BYT_NUM_ORDER, editData.BYT_NUM_ORDER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTTT40);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.BYT_NUM_ORDER, editData.BYT_NUM_ORDER));
                }
                if (IsDiffString(oldData.TCY_NUM_ORDER, editData.TCY_NUM_ORDER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTTT30);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TCY_NUM_ORDER, editData.TCY_NUM_ORDER));
                }
                if (IsDiffString(oldData.PACKING_TYPE_NAME, editData.PACKING_TYPE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuyCachDongGoi);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PACKING_TYPE_NAME, editData.PACKING_TYPE_NAME));
                }
                if (IsDiffLong(oldData.RANK, editData.RANK))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapDo);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RANK, editData.RANK));
                }
                if (IsDiffString(oldData.MEDICINE_NATIONAL_CODE, editData.MEDICINE_NATIONAL_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaDuocQuocGia);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MEDICINE_NATIONAL_CODE, editData.MEDICINE_NATIONAL_CODE));
                }
                if (IsDiffString(oldData.CONTRAINDICATION, editData.CONTRAINDICATION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChongChiDinh);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CONTRAINDICATION, editData.CONTRAINDICATION));
                }
                if (IsDiffString(oldData.ATC_CODES, editData.ATC_CODES))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaATC);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ATC_CODES, editData.ATC_CODES));
                }
                if (IsDiffString(oldData.PREPROCESSING, editData.PREPROCESSING))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoChe);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PREPROCESSING, editData.PREPROCESSING));
                }
                if (IsDiffString(oldData.PROCESSING, editData.PROCESSING))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhucChe);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PROCESSING, editData.PROCESSING));
                }
                if (IsDiffString(oldData.USED_PART, editData.USED_PART))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BoPhanDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.USED_PART, editData.USED_PART));
                }
                if (IsDiffString(oldData.DOSAGE_FORM, editData.DOSAGE_FORM))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DangBaoChe);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DOSAGE_FORM, editData.DOSAGE_FORM));
                }
                if (IsDiffString(oldData.DISTRIBUTED_AMOUNT, editData.DISTRIBUTED_AMOUNT))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuongPhanBo);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DISTRIBUTED_AMOUNT, editData.DISTRIBUTED_AMOUNT));
                }
                if (IsDiffString(oldData.RECORDING_TRANSACTION, editData.RECORDING_TRANSACTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DinhKhoanKeToan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RECORDING_TRANSACTION, editData.RECORDING_TRANSACTION));
                }
                if (IsDiffDecimal(oldData.IMP_UNIT_CONVERT_RATIO, editData.IMP_UNIT_CONVERT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TyLeQuyDoiDonViNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.IMP_UNIT_CONVERT_RATIO, editData.IMP_UNIT_CONVERT_RATIO));
                }
                if (IsDiffShortIsField(oldData.IS_KIDNEY, editData.IS_KIDNEY))
                {
                    string newValue = editData.IS_KIDNEY == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_KIDNEY == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChayThan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_CHEMICAL_SUBSTANCE, editData.IS_CHEMICAL_SUBSTANCE))
                {
                    string newValue = editData.IS_CHEMICAL_SUBSTANCE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_CHEMICAL_SUBSTANCE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LaHoaChat);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_SPLIT_COMPENSATION, editData.IS_SPLIT_COMPENSATION))
                {
                    string newValue = editData.IS_SPLIT_COMPENSATION == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_SPLIT_COMPENSATION == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TachPhanBuLamTron);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }

                if (IsDiffLong(oldData.PARENT_ID, editData.PARENT_ID))
                {
                    string cha = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Cha);
                    HIS_MEDICINE_TYPE newP = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.PARENT_ID);
                    HIS_MEDICINE_TYPE oldP = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.PARENT_ID);
                    string newCha = newP != null ? newP.MEDICINE_TYPE_NAME : "";
                    string oldCha = oldP != null ? oldP.MEDICINE_TYPE_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, cha ?? "", oldCha ?? "", newCha ?? ""));
                }
                if (IsDiffLong(oldData.MANUFACTURER_ID, editData.MANUFACTURER_ID))
                {
                    string hangsanxuat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HangSanXuat);
                    HIS_MANUFACTURER newM = null;
                    HIS_MANUFACTURER oldM = null;
                    if (oldData.MANUFACTURER_ID.HasValue)
                    {
                        oldM = new HisManufacturerGet().GetById(oldData.MANUFACTURER_ID.Value);
                    }
                    if (editData.MANUFACTURER_ID.HasValue)
                    {
                        newM = new HisManufacturerGet().GetById(editData.MANUFACTURER_ID.Value);
                    }
                    string newHang = newM != null ? newM.MANUFACTURER_NAME : "";
                    string oldHang = oldM != null ? oldM.MANUFACTURER_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, hangsanxuat ?? "", oldHang ?? "", newHang ?? ""));
                }
                if (IsDiffLong(oldData.MEDICINE_USE_FORM_ID, editData.MEDICINE_USE_FORM_ID))
                {
                    string duongdung = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DuongDung);
                    HIS_MEDICINE_USE_FORM newM = null;
                    HIS_MEDICINE_USE_FORM oldM = null;
                    if (oldData.MEDICINE_USE_FORM_ID.HasValue)
                    {
                        oldM = new HisMedicineUseFormGet().GetById(oldData.MEDICINE_USE_FORM_ID.Value);
                    }
                    if (editData.MEDICINE_USE_FORM_ID.HasValue)
                    {
                        newM = new HisMedicineUseFormGet().GetById(editData.MEDICINE_USE_FORM_ID.Value);
                    }
                    string newDung = newM != null ? newM.MEDICINE_USE_FORM_NAME : "";
                    string oldDung = oldM != null ? oldM.MEDICINE_USE_FORM_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, duongdung ?? "", oldDung ?? "", newDung ?? ""));
                }
                if (IsDiffLong(oldData.MEDICINE_LINE_ID, editData.MEDICINE_LINE_ID))
                {
                    string dongthuoc = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DongThuoc);
                    HIS_MEDICINE_LINE newL = null;
                    HIS_MEDICINE_LINE oldL = null;
                    if (oldData.MEDICINE_LINE_ID.HasValue)
                    {
                        oldL = new HisMedicineLineGet().GetById(oldData.MEDICINE_LINE_ID.Value);
                    }
                    if (editData.MEDICINE_LINE_ID.HasValue)
                    {
                        newL = new HisMedicineLineGet().GetById(editData.MEDICINE_LINE_ID.Value);
                    }
                    string newDong = newL != null ? newL.MEDICINE_LINE_NAME : "";
                    string oldDong = oldL != null ? oldL.MEDICINE_LINE_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, dongthuoc ?? "", oldDong ?? "", newDong ?? ""));
                }
                if (IsDiffLong(oldData.MEDICINE_GROUP_ID, editData.MEDICINE_GROUP_ID))
                {
                    string nhomthuoc = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomThuoc);
                    HIS_MEDICINE_GROUP newG = null;
                    HIS_MEDICINE_GROUP oldG = null;
                    if (oldData.MEDICINE_GROUP_ID.HasValue)
                    {
                        oldG = new HisMedicineGroupGet().GetById(oldData.MEDICINE_GROUP_ID.Value);
                    }
                    if (editData.MEDICINE_GROUP_ID.HasValue)
                    {
                        newG = new HisMedicineGroupGet().GetById(editData.MEDICINE_GROUP_ID.Value);
                    }
                    string newNhom = newG != null ? newG.MEDICINE_GROUP_NAME : "";
                    string oldNhom = oldG != null ? oldG.MEDICINE_GROUP_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, nhomthuoc ?? "", oldNhom ?? "", newNhom ?? ""));
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
                if (IsDiffLong(oldData.TDL_GENDER_ID, editData.TDL_GENDER_ID))
                {
                    string gioitinh = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GioiTinh);
                    HIS_GENDER newG = null;
                    HIS_GENDER oldG = null;
                    if (oldData.TDL_GENDER_ID.HasValue)
                    {
                        oldG = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == oldData.TDL_GENDER_ID.Value); ;
                    }
                    if (editData.TDL_GENDER_ID.HasValue)
                    {
                        newG = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == editData.TDL_GENDER_ID.Value);
                    }
                    string newGen = newG != null ? newG.GENDER_NAME : "";
                    string oldGen = oldG != null ? oldG.GENDER_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, gioitinh ?? "", oldGen ?? "", newGen ?? ""));
                }
                if (IsDiffDecimal(oldData.LAST_EXP_VAT_RATIO, editData.LAST_EXP_VAT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VATBan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.LAST_EXP_VAT_RATIO, editData.LAST_EXP_VAT_RATIO));
                }
                if (IsDiffShortIsField(oldData.IS_DRUG_STORE, editData.IS_DRUG_STORE))
                {
                    string newValue = editData.IS_DRUG_STORE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_DRUG_STORE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThuocQuayThuoc);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (editService != null && oldService != null)
                {
                    if (IsDiffString(oldService.HEIN_SERVICE_BHYT_NAME, editService.HEIN_SERVICE_BHYT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.HEIN_SERVICE_BHYT_NAME, editService.HEIN_SERVICE_BHYT_NAME));
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
                    if (IsDiffShortIsField(oldService.IS_USE_SERVICE_HEIN, editService.IS_USE_SERVICE_HEIN))
                    {
                        string newValue = editService.IS_USE_SERVICE_HEIN == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_USE_SERVICE_HEIN == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ApDungTranTT30);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldService.IS_OTHER_SOURCE_PAID, editService.IS_OTHER_SOURCE_PAID))
                    {
                        string newValue = editService.IS_OTHER_SOURCE_PAID == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_OTHER_SOURCE_PAID == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CoNguonKhacChiTra);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldService.IS_OUT_PARENT_FEE, editService.IS_OUT_PARENT_FEE))
                    {
                        string newValue = editService.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChiPhiNgoaiGoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffLong(oldService.AGE_FROM, editService.AGE_FROM))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuoiTu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.AGE_FROM, editService.AGE_FROM));
                    }
                    if (IsDiffLong(oldService.AGE_TO, editService.AGE_TO))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuoiDen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.AGE_TO, editService.AGE_TO));
                    }
                    if (IsDiffShortIsField(oldService.IS_NO_HEIN_LIMIT_FOR_SPECIAL, editService.IS_NO_HEIN_LIMIT_FOR_SPECIAL))
                    {
                        string newValue = editService.IS_NO_HEIN_LIMIT_FOR_SPECIAL != Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_NO_HEIN_LIMIT_FOR_SPECIAL != Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ApTranVoiTheDacBiet);
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
                    if (IsDiffDecimal(oldService.HEIN_LIMIT_PRICE_INTR_TIME, editService.HEIN_LIMIT_PRICE_INTR_TIME))
                    {
                        string newValue = Inventec.Common.DateTime.Convert.TimeNumberToDateString(editService.HEIN_LIMIT_PRICE_INTR_TIME ?? 0);
                        string oldValue = Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldService.HEIN_LIMIT_PRICE_INTR_TIME ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ApDungTyLeBHYTMoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue != null ? oldValue : "", newValue != null ? newValue : ""));
                        
                    }
                    if (IsDiffShortIsField(oldService.IS_OUT_OF_DRG, editService.IS_OUT_OF_DRG))
                    {
                        string newValue = editService.IS_OUT_OF_DRG == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_OUT_OF_DRG == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgoaiDanhMucDRG);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffString(oldService.OTHER_PAY_SOURCE_ICDS, editService.OTHER_PAY_SOURCE_ICDS))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BenhDuocHoTro);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.OTHER_PAY_SOURCE_ICDS, editService.OTHER_PAY_SOURCE_ICDS));
                    }
                    if (IsDiffLong(oldService.OTHER_PAY_SOURCE_ID, editService.OTHER_PAY_SOURCE_ID))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguonChiTraKhac);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.OTHER_PAY_SOURCE_ID, editService.OTHER_PAY_SOURCE_ID));
                    }
                }

                if (deletes != null && deletes.Count > 0)
                {
                    string xoahoatchat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.XoaHoatChat);
                    List<HIS_ACTIVE_INGREDIENT> actives = new HisActiveIngredientGet().GetByIds(deletes.Select(s => s.ACTIVE_INGREDIENT_ID).ToList());
                    string name = "";
                    if (actives != null && actives.Count > 0)
                    {
                        name = String.Join(", ", actives.Select(s => s.ACTIVE_INGREDIENT_NAME).ToList());
                    }
                    editFields.Add(String.Format(FORMAT_FIELD, xoahoatchat, name));
                }

                if (inserts != null && inserts.Count > 0)
                {
                    string themhoatchat = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThemHoatChat);
                    List<HIS_ACTIVE_INGREDIENT> actives = new HisActiveIngredientGet().GetByIds(inserts.Select(s => s.ACTIVE_INGREDIENT_ID).ToList());
                    string name = "";
                    if (actives != null && actives.Count > 0)
                    {
                        name = String.Join(", ", actives.Select(s => s.ACTIVE_INGREDIENT_NAME).ToList());
                    }
                    editFields.Add(String.Format(FORMAT_FIELD, themhoatchat, name));
                }

                new EventLogGenerator(logEnum, String.Join(". ", editFields))
                    .MedicineTypeId(oldData.ID.ToString())
                    .MedicineTypeCode(oldData.MEDICINE_TYPE_CODE)
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
