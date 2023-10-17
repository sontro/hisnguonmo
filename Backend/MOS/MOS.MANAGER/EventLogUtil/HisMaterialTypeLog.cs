using Inventec.Common.ObjectChecker;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisActiveIngredient;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisFilmSize;
using MOS.MANAGER.HisManufacturer;
using MOS.MANAGER.HisMaterialTypeMap;
using MOS.MANAGER.HisMedicineGroup;
using MOS.MANAGER.HisMedicineLine;
using MOS.MANAGER.HisMedicineUseForm;
using MOS.MANAGER.HisMestMatyDepa;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisMaterialTypeLog
    {
        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal static void Run(HIS_MATERIAL_TYPE editData, HIS_MATERIAL_TYPE oldData, HIS_SERVICE editService, HIS_SERVICE oldService, HisMaterialTypeSDO data, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (IsDiffString(oldData.MATERIAL_TYPE_CODE, editData.MATERIAL_TYPE_CODE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ma);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MATERIAL_TYPE_CODE, editData.MATERIAL_TYPE_CODE));
                }
                if (IsDiffString(oldData.MATERIAL_TYPE_NAME, editData.MATERIAL_TYPE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ten);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MATERIAL_TYPE_NAME, editData.MATERIAL_TYPE_NAME));
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
                if (IsDiffString(oldData.MATERIAL_GROUP_BHYT, editData.MATERIAL_GROUP_BHYT))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomVTBHYT);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MATERIAL_GROUP_BHYT, editData.MATERIAL_GROUP_BHYT));
                }
                if (IsDiffString(oldData.NATIONAL_NAME, editData.NATIONAL_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuocGia);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NATIONAL_NAME, editData.NATIONAL_NAME));
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
                if (IsDiffDecimal(oldData.LAST_EXP_VAT_RATIO, editData.LAST_EXP_VAT_RATIO))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VATBan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.LAST_EXP_VAT_RATIO, editData.LAST_EXP_VAT_RATIO));
                }
                if (IsDiffString(oldData.REGISTER_NUMBER, editData.REGISTER_NUMBER))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoDangKy);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.REGISTER_NUMBER, editData.REGISTER_NUMBER));
                }
                if (IsDiffShortIsField(oldData.IS_STOP_IMP, editData.IS_STOP_IMP))
                {
                    string newValue = editData.IS_STOP_IMP == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_STOP_IMP == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DungNhap);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_STENT, editData.IS_STENT))
                {
                    string newValue = editData.IS_STENT == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_STENT == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LaStent);
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
                if (IsDiffShortIsField(oldData.IS_FILM, editData.IS_FILM))
                {
                    string newValue = editData.IS_FILM == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_FILM == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhimChup);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_AUTO_EXPEND, editData.IS_AUTO_EXPEND))
                {
                    string newValue = editData.IS_AUTO_EXPEND == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_AUTO_EXPEND == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuDongHaoPhi);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_REUSABLE, editData.IS_REUSABLE))
                {
                    string newValue = editData.IS_REUSABLE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_REUSABLE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatTuTaiSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffLong(oldData.MAX_REUSE_COUNT, editData.MAX_REUSE_COUNT))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TaiSuDungToiDa);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MAX_REUSE_COUNT, editData.MAX_REUSE_COUNT));
                }
                if (IsDiffShortIsField(oldData.IS_MUST_PREPARE, editData.IS_MUST_PREPARE))
                {
                    string newValue = editData.IS_MUST_PREPARE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_MUST_PREPARE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhaiDuTru);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffString(oldData.DESCRIPTION, editData.DESCRIPTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DESCRIPTION, editData.DESCRIPTION));
                }
                if (IsDiffString(oldData.PACKING_TYPE_NAME, editData.PACKING_TYPE_NAME))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuyCachDongGoi);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PACKING_TYPE_NAME, editData.PACKING_TYPE_NAME));
                }

                //if (IsDiffLong(oldData.MATERIAL_TYPE_MAP_ID, editData.MATERIAL_TYPE_MAP_ID))
                //{
                //    HIS_MATERIAL_TYPE_MAP materialTypeMapOld = new HisMaterialTypeMapGet().GetById(oldData.MATERIAL_TYPE_MAP_ID.Value);
                //    HIS_MATERIAL_TYPE_MAP materialTypeMapEdit = new HisMaterialTypeMapGet().GetById(editData.MATERIAL_TYPE_MAP_ID.Value);
                //    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatTuTuongDuong);
                //    string newValue = string.Join(",", materialTypeMapEdit.MATERIAL_TYPE_MAP_NAME);
                //    string oldValue = string.Join(",", materialTypeMapOld.MATERIAL_TYPE_MAP_NAME);
                //    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue != null ? oldValue : "", newValue != null ? newValue : ""));
                    
                //}
                //if (oldData.HIS_MEST_MATY_DEPA != null && editData.HIS_MEST_MATY_DEPA != null)
                //{
                //    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChanXuatChoKhoa);
                //    editFields.Add(String.Format(FORMAT_EDIT, fieldName, string.Join(",", oldData.HIS_MEST_MATY_DEPA), string.Join(",", editData.HIS_MEST_MATY_DEPA)));
                //}

                if (IsDiffShortIsField(oldData.IS_CHEMICAL_SUBSTANCE, editData.IS_CHEMICAL_SUBSTANCE))
                {
                    string newValue = editData.IS_CHEMICAL_SUBSTANCE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_CHEMICAL_SUBSTANCE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LaHoaChat1);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_CONSUMABLE, editData.IS_CONSUMABLE))
                {
                    string newValue = editData.IS_CONSUMABLE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_CONSUMABLE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LaVatTuTieuHaoCanThiepTimManh);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_OUT_HOSPITAL, editData.IS_OUT_HOSPITAL))
                {
                    string newValue = editData.IS_OUT_HOSPITAL == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_OUT_HOSPITAL == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatTuNgoaiVien);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_DRUG_STORE, editData.IS_DRUG_STORE))
                {
                    string newValue = editData.IS_DRUG_STORE == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_DRUG_STORE == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatTuQuayThuoc);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_RAW_MATERIAL, editData.IS_RAW_MATERIAL))
                {
                    string newValue = editData.IS_RAW_MATERIAL == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_RAW_MATERIAL == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LaNguyenLieuBaoChe);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffShortIsField(oldData.IS_NOT_SHOW_TRACKING, editData.IS_NOT_SHOW_TRACKING))
                {
                    string newValue = editData.IS_NOT_SHOW_TRACKING == Constant.IS_TRUE ? co : khong;
                    string oldValue = oldData.IS_NOT_SHOW_TRACKING == Constant.IS_TRUE ? co : khong;
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongHienThiTrenToDieuTri);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }
                if (IsDiffDecimal(oldData.LAST_EXP_PRICE, editData.LAST_EXP_PRICE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaBan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.LAST_EXP_PRICE, editData.LAST_EXP_PRICE));
                }
                if (IsDiffLong(oldData.PARENT_ID, editData.PARENT_ID))
                {
                    string cha = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Cha);
                    HIS_MATERIAL_TYPE newP = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.PARENT_ID);
                    HIS_MATERIAL_TYPE oldP = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.PARENT_ID);
                    string newCha = newP != null ? newP.MATERIAL_TYPE_NAME : "";
                    string oldCha = oldP != null ? oldP.MATERIAL_TYPE_NAME : "";
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
                if (IsDiffLong(oldData.MATERIAL_TYPE_MAP_ID, editData.MATERIAL_TYPE_MAP_ID))
                {
                    string duongdung = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.VatTuAnhXa);
                    HIS_MATERIAL_TYPE_MAP newM = null;
                    HIS_MATERIAL_TYPE_MAP oldM = null;
                    if (oldData.MATERIAL_TYPE_MAP_ID.HasValue)
                    {
                        oldM = new HisMaterialTypeMapGet().GetById(oldData.MATERIAL_TYPE_MAP_ID.Value);
                    }
                    if (editData.MATERIAL_TYPE_MAP_ID.HasValue)
                    {
                        newM = new HisMaterialTypeMapGet().GetById(editData.MATERIAL_TYPE_MAP_ID.Value);
                    }
                    string newValue = newM != null ? newM.MATERIAL_TYPE_MAP_NAME : "";
                    string oldValue = oldM != null ? oldM.MATERIAL_TYPE_MAP_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, duongdung ?? "", oldValue ?? "", newValue ?? ""));
                }
                if (IsDiffLong(oldData.FILM_SIZE_ID, editData.FILM_SIZE_ID))
                {
                    string dongthuoc = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KichThuocPhim);
                    HIS_FILM_SIZE newO = null;
                    HIS_FILM_SIZE oldO = null;
                    if (oldData.FILM_SIZE_ID.HasValue)
                    {
                        oldO = new HisFilmSizeGet().GetById(oldData.FILM_SIZE_ID.Value);
                    }
                    if (editData.FILM_SIZE_ID.HasValue)
                    {
                        newO = new HisFilmSizeGet().GetById(editData.FILM_SIZE_ID.Value);
                    }
                    string newValue = newO != null ? newO.FILM_SIZE_NAME : "";
                    string oldValue = oldO != null ? oldO.FILM_SIZE_NAME : "";
                    editFields.Add(String.Format(FORMAT_EDIT, dongthuoc, oldValue, newValue));
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
                if (IsDiffLong(oldData.LAST_EXPIRED_DATE, editData.LAST_EXPIRED_DATE))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HanSuDung);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.LAST_EXPIRED_DATE, editData.LAST_EXPIRED_DATE));
                }
                if (IsDiffString(oldData.RECORDING_TRANSACTION, editData.RECORDING_TRANSACTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DinhKhoanKeToan);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RECORDING_TRANSACTION, editData.RECORDING_TRANSACTION));
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
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayApDungTyLeBHYTMoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue != null ? oldValue : "", newValue != null ? newValue : ""));
                    }
                    if (IsDiffShortIsField(oldService.IS_OUT_PARENT_FEE, editService.IS_OUT_PARENT_FEE))
                    {
                        string newValue = editService.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChiPhiNgoaiGoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
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
                    if (IsDiffShortIsField(oldService.IS_OUT_OF_DRG, editService.IS_OUT_OF_DRG))
                    {
                        string newValue = editService.IS_OUT_OF_DRG == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldService.IS_OUT_OF_DRG == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgoaiDanhMucDRG);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffLong(oldService.OTHER_PAY_SOURCE_ID, editService.OTHER_PAY_SOURCE_ID))
                    {
                        HIS_OTHER_PAY_SOURCE oPaySoureOld = HisOtherPaySourceCFG.DATA.FirstOrDefault(o => o.ID == oldService.OTHER_PAY_SOURCE_ID);
                        HIS_OTHER_PAY_SOURCE oPaySoureEdit = HisOtherPaySourceCFG.DATA.FirstOrDefault(o => o.ID == editService.OTHER_PAY_SOURCE_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguonChiTraKhac);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oPaySoureOld != null ? oPaySoureOld.OTHER_PAY_SOURCE_NAME : "", oPaySoureEdit != null ? oPaySoureEdit.OTHER_PAY_SOURCE_NAME : ""));
                    }
                    if (IsDiffString(oldService.OTHER_PAY_SOURCE_ICDS, editService.OTHER_PAY_SOURCE_ICDS))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BenhDuocHoTro);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldService.OTHER_PAY_SOURCE_ICDS, editService.OTHER_PAY_SOURCE_ICDS));
                    }
                }
                string updateLockTreatment;
                string updateLockTreat;
                if (data.UpdateSereServ == true)
                {
                    string capNhatHoSo = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatHoSoChuaKhoaVienPhi);
                    string capNhatTatCa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatTatCa);
                    editFields.Add(String.Format("{0}: {1}, {2}: {3}", capNhatHoSo, "Không check", capNhatTatCa, "Có check"));
                }
                else if (data.UpdateSereServ == false)
                {
                    string capNhatHoSo = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatHoSoChuaKhoaVienPhi);
                    string capNhatTatCa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatTatCa);
                    editFields.Add(String.Format("{0}: {1}, {2}: {3}", capNhatHoSo, "Có check", capNhatTatCa, "Không check"));
                }

                new EventLogGenerator(logEnum, String.Join(". ", editFields))
                    .MaterialTypeId(oldData.ID.ToString())
                    .MaterialTypeCode(oldData.MATERIAL_TYPE_CODE)
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
