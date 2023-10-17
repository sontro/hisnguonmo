using Inventec.Common.ObjectChecker;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDiimType;
using MOS.MANAGER.HisExeServiceModule;
using MOS.MANAGER.HisFilmSize;
using MOS.MANAGER.HisFuexType;
using MOS.MANAGER.HisIcdCm;
using MOS.MANAGER.HisOtherPaySource;
using MOS.MANAGER.HisPackage;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPtttGroup;
using MOS.MANAGER.HisPtttMethod;
using MOS.MANAGER.HisRationGroup;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServiceUnit;
using MOS.MANAGER.HisSuimIndex;
using MOS.MANAGER.HisTestType;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class HisServiceLog
    {
        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal static void Run(HIS_SERVICE editData, HIS_SERVICE oldData, HisServiceSDO sdo, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (oldData != null && editData != null)
                {
                    if (IsDiffLong(oldData.SERVICE_TYPE_ID, editData.SERVICE_TYPE_ID)) 
                    {
                        HIS_SERVICE_TYPE hisServiceTypeOld = HisServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.SERVICE_TYPE_ID);
                        HIS_SERVICE_TYPE hisServiceTypeEdit = HisServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.SERVICE_TYPE_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenLoaiDichVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, hisServiceTypeOld != null ? hisServiceTypeOld.SERVICE_TYPE_NAME : "", hisServiceTypeEdit != null ? hisServiceTypeEdit.SERVICE_TYPE_NAME : ""));
                    }
                    if (IsDiffString(oldData.SERVICE_CODE, editData.SERVICE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaDichVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SERVICE_CODE, editData.SERVICE_CODE));
                    }
                    if (IsDiffString(oldData.SERVICE_NAME, editData.SERVICE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenDichVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SERVICE_NAME, editData.SERVICE_NAME));
                    }
                    if (IsDiffLong(oldData.SERVICE_UNIT_ID, editData.SERVICE_UNIT_ID))
                    {
                        string donvitinh = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DonViTinh);
                        HIS_SERVICE_UNIT oldUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == oldData.SERVICE_UNIT_ID);
                        HIS_SERVICE_UNIT newUnit = HisServiceUnitCFG.DATA.FirstOrDefault(o => o.ID == editData.SERVICE_UNIT_ID);
                        string newDonVi = newUnit != null ? newUnit.SERVICE_UNIT_NAME : "";
                        string oldDonVi = oldUnit != null ? oldUnit.SERVICE_UNIT_NAME : "";
                        editFields.Add(String.Format(FORMAT_EDIT, donvitinh ?? "", oldDonVi ?? "", newDonVi ?? ""));
                    }
                    if (IsDiffLong(oldData.GENDER_ID, editData.GENDER_ID))
                    {
                        string gioitinh = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GioiTinh);
                        HIS_GENDER newG = null;
                        HIS_GENDER oldG = null;
                        if (oldData.GENDER_ID.HasValue)
                        {
                            oldG = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == oldData.GENDER_ID.Value); ;
                        }
                        if (editData.GENDER_ID.HasValue)
                        {
                            newG = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == editData.GENDER_ID.Value);
                        }
                        string newGen = newG != null ? newG.GENDER_NAME : "";
                        string oldGen = oldG != null ? oldG.GENDER_NAME : "";
                        editFields.Add(String.Format(FORMAT_EDIT, gioitinh ?? "", oldGen ?? "", newGen ?? ""));
                    }
                    if (IsDiffString(oldData.SPECIALITY_CODE, editData.SPECIALITY_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaChuyenKhoa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SPECIALITY_CODE, editData.SPECIALITY_CODE));
                    }

                    if (IsDiffLong(oldData.HEIN_SERVICE_TYPE_ID, editData.HEIN_SERVICE_TYPE_ID))
                    {
                        HIS_HEIN_SERVICE_TYPE heinServiceTypeOld = HisHeinServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.HEIN_SERVICE_TYPE_ID);
                        HIS_HEIN_SERVICE_TYPE heinServiceTypeEdit = HisHeinServiceTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.HEIN_SERVICE_TYPE_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiDichVuBaoHiem);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, heinServiceTypeOld != null ? heinServiceTypeOld.HEIN_SERVICE_TYPE_NAME : "", heinServiceTypeEdit != null ? heinServiceTypeEdit.HEIN_SERVICE_TYPE_NAME : ""));
                    }
                    if (IsDiffString(oldData.HEIN_SERVICE_BHYT_CODE, editData.HEIN_SERVICE_BHYT_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaDichVuBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HEIN_SERVICE_BHYT_CODE, editData.HEIN_SERVICE_BHYT_CODE));
                    }

                    if (IsDiffString(oldData.HEIN_SERVICE_BHYT_NAME, editData.HEIN_SERVICE_BHYT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenDichVuBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HEIN_SERVICE_BHYT_NAME, editData.HEIN_SERVICE_BHYT_NAME));
                    }
                    if (IsDiffString(oldData.HEIN_ORDER, editData.HEIN_ORDER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STTBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HEIN_ORDER, editData.HEIN_ORDER));
                    }
                    if (IsDiffLong(oldData.PARENT_ID, editData.PARENT_ID))
                    {
                        HIS_SERVICE serviceOld = new HisServiceGet().GetById(oldData.PARENT_ID ?? 0);
                        HIS_SERVICE serviceEdit = new HisServiceGet().GetById(editData.PARENT_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DichVuCha);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, serviceOld != null ? serviceOld.SERVICE_NAME : "", serviceEdit != null ? serviceEdit.SERVICE_NAME : ""));
                    }

                    if (IsDiffLong(oldData.PACKAGE_ID, editData.PACKAGE_ID))
                    {
                        HIS_PACKAGE packageEdit = new HisPackageGet().GetById(editData.PACKAGE_ID ?? 0);
                        HIS_PACKAGE packageOld = new HisPackageGet().GetById(oldData.PACKAGE_ID ?? 0);

                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GoiDichVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, packageOld != null ? packageOld.PACKAGE_NAME : "", packageEdit != null ? packageEdit.PACKAGE_NAME : ""));
                    }

                    if (IsDiffDecimal(oldData.PACKAGE_PRICE, editData.PACKAGE_PRICE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThongTinGiaGoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PACKAGE_PRICE, editData.PACKAGE_PRICE));
                    }
                    if (IsDiffShort(oldData.BILL_OPTION, editData.BILL_OPTION))
                    {
                        string billOptionOld = oldData.BILL_OPTION == null ? "" : oldData.BILL_OPTION == Constant.IS_TRUE ? "Hóa đơn thường" : oldData.BILL_OPTION == 2 ? "Tách chênh lệch vào hóa đơn dịch vụ" : "Hóa đơn dịch vụ";
                        string billOptionEdit = editData.BILL_OPTION == null ? "" : editData.BILL_OPTION == Constant.IS_TRUE ? "Hóa đơn thường" : editData.BILL_OPTION == 2 ? "Tách chênh lệch vào hóa đơn dịch vụ" : "Hóa đơn dịch vụ";
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiHinhHoaDon);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, billOptionOld, billOptionEdit));
                    }
                    if (IsDiffLong(oldData.BILL_PATIENT_TYPE_ID, editData.BILL_PATIENT_TYPE_ID))
                    {
                        HIS_PATIENT_TYPE patientTypeOld = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.BILL_PATIENT_TYPE_ID);
                        HIS_PATIENT_TYPE patientTypeEdit = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.BILL_PATIENT_TYPE_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongPhuThuMacDinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, patientTypeOld != null ? patientTypeOld.PATIENT_TYPE_NAME : "", patientTypeEdit != null ? patientTypeEdit.PATIENT_TYPE_NAME : ""));
                    }
                    if (IsDiffString(oldData.APPLIED_PATIENT_TYPE_IDS, editData.APPLIED_PATIENT_TYPE_IDS))
                    {
                        string old = string.Join(",", oldData.APPLIED_PATIENT_TYPE_IDS);
                        string edit = string.Join(",", editData.APPLIED_PATIENT_TYPE_IDS);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongThanhToanApDung);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, old, edit));
                    }
                    if (IsDiffLong(oldData.PTTT_METHOD_ID, editData.PTTT_METHOD_ID))
                    {
                        HIS_PTTT_METHOD pttMethodOld = new HisPtttMethodGet().GetById(oldData.PTTT_METHOD_ID ?? 0);
                        HIS_PTTT_METHOD pttMethodEdit = new HisPtttMethodGet().GetById(editData.PTTT_METHOD_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhuongPhapPhauThuat);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, pttMethodOld != null ? pttMethodOld.PTTT_METHOD_NAME : "", pttMethodEdit != null ? pttMethodEdit.PTTT_METHOD_NAME : ""));
                    }
                    if (IsDiffLong(oldData.DEFAULT_PATIENT_TYPE_ID, editData.DEFAULT_PATIENT_TYPE_ID))
                    {
                        HIS_PATIENT_TYPE patientTypeOld = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == oldData.DEFAULT_PATIENT_TYPE_ID);
                        HIS_PATIENT_TYPE patientTypeEdit = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == editData.DEFAULT_PATIENT_TYPE_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DTTTMacDinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, patientTypeOld != null ? patientTypeOld.PATIENT_TYPE_NAME : "", patientTypeEdit != null ? patientTypeEdit.PATIENT_TYPE_NAME : ""));
                    }
                    if (IsDiffString(oldData.TESTING_TECHNIQUE, editData.TESTING_TECHNIQUE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KyThuatXetNghiem);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TESTING_TECHNIQUE, editData.TESTING_TECHNIQUE));
                    }
                    if (IsDiffLong(oldData.PTTT_GROUP_ID, editData.PTTT_GROUP_ID))
                    {
                        HIS_PTTT_GROUP pttGroupOld = new HisPtttGroupGet().GetById(oldData.PTTT_GROUP_ID ?? 0);
                        HIS_PTTT_GROUP pttGroupEdit = new HisPtttGroupGet().GetById(editData.PTTT_GROUP_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomPTTT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, pttGroupOld != null ? pttGroupOld.PTTT_GROUP_NAME : "", pttGroupEdit != null ? pttGroupEdit.PTTT_GROUP_NAME : ""));
                    }
                    if (IsDiffDecimal(oldData.HEIN_LIMIT_PRICE_OLD, editData.HEIN_LIMIT_PRICE_OLD))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaTranCu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HEIN_LIMIT_PRICE_OLD, editData.HEIN_LIMIT_PRICE_OLD));
                    }
                    if (IsDiffDecimal(oldData.HEIN_LIMIT_PRICE, editData.HEIN_LIMIT_PRICE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaTranMoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HEIN_LIMIT_PRICE, editData.HEIN_LIMIT_PRICE));
                    }
                    if (IsDiffLong(oldData.ICD_CM_ID, editData.ICD_CM_ID))
                    {
                        HIS_ICD_CM icdCmOld = new HisIcdCmGet().GetById(oldData.ICD_CM_ID ?? 0);
                        HIS_ICD_CM icdCmEdit = new HisIcdCmGet().GetById(editData.ICD_CM_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaICDCM);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, icdCmOld != null ? icdCmOld.ICD_CM_CODE : "", icdCmEdit != null ? icdCmEdit.ICD_CM_CODE : ""));
                    }
                    if (IsDiffLong(oldData.ICD_CM_ID, editData.ICD_CM_ID))
                    {
                        HIS_ICD_CM icdCmOld = new HisIcdCmGet().GetById(oldData.ICD_CM_ID ?? 0);
                        HIS_ICD_CM icdCmEdit = new HisIcdCmGet().GetById(editData.ICD_CM_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ICDCM);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, icdCmOld != null ? icdCmOld.ICD_CM_NAME : "", icdCmEdit != null ? icdCmEdit.ICD_CM_NAME : ""));
                    }
                    if (IsDiffLong(oldData.HEIN_LIMIT_PRICE_IN_TIME, editData.HEIN_LIMIT_PRICE_IN_TIME))
                    {
                        string heinLitmitPriceInTimeOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(oldData.HEIN_LIMIT_PRICE_IN_TIME ?? 0));
                        string heinLitmitPriceInTimeEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(editData.HEIN_LIMIT_PRICE_IN_TIME ?? 0));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianApDung);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, heinLitmitPriceInTimeOld != null ? heinLitmitPriceInTimeOld : "", heinLitmitPriceInTimeEdit != null ? heinLitmitPriceInTimeEdit : ""));
                    }
                    if (IsDiffDecimal(oldData.COGS, editData.COGS))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GiaVon);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.COGS, editData.COGS));
                    }

                    if (IsDiffString(oldData.RATION_SYMBOL, editData.RATION_SYMBOL))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KyHieuXuatAn);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RATION_SYMBOL, editData.RATION_SYMBOL));
                    }
                    if (IsDiffLong(oldData.RATION_GROUP_ID, editData.RATION_GROUP_ID))
                    {
                        HIS_RATION_GROUP rationGroupOld = new HisRationGroupGet().GetById(oldData.RATION_GROUP_ID ?? 0);
                        HIS_RATION_GROUP rationGroupEdit = new HisRationGroupGet().GetById(editData.RATION_GROUP_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomXuatAn);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, rationGroupOld != null ? rationGroupOld.RATION_GROUP_NAME : "", rationGroupEdit != null ? rationGroupEdit.RATION_GROUP_NAME : ""));
                    }
                    if (IsDiffLong(oldData.NUM_ORDER, editData.NUM_ORDER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThuTu1);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NUM_ORDER, editData.NUM_ORDER));
                    }
                    if (IsDiffString(oldData.PACS_TYPE_CODE, editData.PACS_TYPE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaNhomDichVuPacs);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PACS_TYPE_CODE, editData.PACS_TYPE_CODE));
                    }
                    if (IsDiffLong(oldData.DIIM_TYPE_ID, editData.DIIM_TYPE_ID))
                    {
                        HIS_DIIM_TYPE diimTypeOld = new HisDiimTypeGet().GetById(oldData.DIIM_TYPE_ID ?? 0);
                        HIS_DIIM_TYPE diimTypeEdit = new HisDiimTypeGet().GetById(editData.DIIM_TYPE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiChuanDoanHinhAnh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, diimTypeOld != null ? diimTypeOld.DIIM_TYPE_NAME : "", diimTypeEdit != null ? diimTypeEdit.DIIM_TYPE_NAME : ""));
                    }
                    if (IsDiffLong(oldData.FUEX_TYPE_ID, editData.FUEX_TYPE_ID))
                    {
                        HIS_FUEX_TYPE fuexTypeOld = new HisFuexTypeGet().GetById(oldData.FUEX_TYPE_ID ?? 0);
                        HIS_FUEX_TYPE fuexypeEdit = new HisFuexTypeGet().GetById(editData.FUEX_TYPE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiTDCN);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, fuexTypeOld != null ? fuexTypeOld.FUEX_TYPE_NAME : "", fuexypeEdit != null ? fuexypeEdit.FUEX_TYPE_NAME : ""));
                    }

                    if (IsDiffLong(oldData.TEST_TYPE_ID, editData.TEST_TYPE_ID))
                    {
                        HIS_TEST_TYPE testTypeOld = new HisTestTypeGet().GetById(oldData.TEST_TYPE_ID ?? 0);
                        HIS_TEST_TYPE testTypeEdit = new HisTestTypeGet().GetById(editData.TEST_TYPE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiXetNghiem);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, testTypeOld != null ? testTypeOld.TEST_TYPE_NAME : "", testTypeEdit != null ? testTypeEdit.TEST_TYPE_NAME : ""));
                    }
                    if (IsDiffString(oldData.SAMPLE_TYPE_CODE, editData.SAMPLE_TYPE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiMauXetNghiem);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SAMPLE_TYPE_CODE, editData.SAMPLE_TYPE_CODE));
                    }
                    if (IsDiffDecimal(oldData.MAX_EXPEND, editData.MAX_EXPEND))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GioiHanSoTienHaoPhi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MAX_EXPEND, editData.MAX_EXPEND));
                    }
                    if (IsDiffLong(oldData.NUMBER_OF_FILM, editData.NUMBER_OF_FILM))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoPhim);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NUMBER_OF_FILM, editData.NUMBER_OF_FILM));
                    }
                    if (IsDiffLong(oldData.FILM_SIZE_ID, editData.FILM_SIZE_ID))
                    {
                        HIS_FILM_SIZE filmSizeOld = new HisFilmSizeGet().GetById(oldData.FILM_SIZE_ID ?? 0);
                        HIS_FILM_SIZE filmSizeEdit = new HisFilmSizeGet().GetById(editData.FILM_SIZE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CoPhim);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, filmSizeOld != null ? filmSizeOld.FILM_SIZE_NAME : "", filmSizeEdit != null ? filmSizeEdit.FILM_SIZE_NAME : ""));
                    }
                    if (IsDiffDecimal(oldData.ESTIMATE_DURATION, editData.ESTIMATE_DURATION))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianDuKien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ESTIMATE_DURATION, editData.ESTIMATE_DURATION));
                    }
                    if (IsDiffDecimal(oldData.MIN_PROCESS_TIME, editData.MIN_PROCESS_TIME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianXuLyToiThieu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MIN_PROCESS_TIME, editData.MIN_PROCESS_TIME));
                    }
                    if (IsDiffString(oldData.MIN_PROC_TIME_EXCEPT_PATY_IDS, editData.MIN_PROC_TIME_EXCEPT_PATY_IDS))
                    {
                        string old = string.Join(",", oldData.MIN_PROC_TIME_EXCEPT_PATY_IDS);
                        string edit = string.Join(",", editData.MIN_PROC_TIME_EXCEPT_PATY_IDS);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongThanhToanKhongApDung);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, old, edit));
                    }
                    if (IsDiffString(oldData.MAX_PROC_TIME_EXCEPT_PATY_IDS, editData.MAX_PROC_TIME_EXCEPT_PATY_IDS))
                    {
                        string old = string.Join(",", oldData.MAX_PROC_TIME_EXCEPT_PATY_IDS);
                        string edit = string.Join(",", editData.MAX_PROC_TIME_EXCEPT_PATY_IDS);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongThanhToanKhongApDung);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, old, edit));
                    }
                    if (IsDiffLong(oldData.AGE_FROM, editData.AGE_FROM))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuoiTu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.AGE_FROM, editData.AGE_FROM));
                    }
                    if (IsDiffLong(oldData.AGE_TO, editData.AGE_TO))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuoiDen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.AGE_TO, editData.AGE_TO));
                    }
                    if (IsDiffLong(oldData.MAX_PROCESS_TIME, editData.MAX_PROCESS_TIME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianXuLyToiDa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MAX_PROCESS_TIME, editData.MAX_PROCESS_TIME));
                    }
                    if (IsDiffLong(oldData.MAX_TOTAL_PROCESS_TIME, editData.MAX_TOTAL_PROCESS_TIME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TongXuLyToiDa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MAX_TOTAL_PROCESS_TIME, editData.MAX_TOTAL_PROCESS_TIME));
                    }
                    if (IsDiffLong(oldData.MAX_AMOUNT, editData.MAX_AMOUNT))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuongToiDa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MAX_AMOUNT, editData.MAX_AMOUNT));
                    }
                    if (IsDiffLong(oldData.MAX_AMOUNT, editData.MAX_AMOUNT))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuongToiDa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MAX_AMOUNT, editData.MAX_AMOUNT));
                    }
                    if (IsDiffLong(oldData.MIN_DURATION, editData.MIN_DURATION))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianToiThieu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MIN_DURATION, editData.MIN_DURATION));
                    }
                    if (IsDiffLong(oldData.WARNING_SAMPLING_TIME, editData.WARNING_SAMPLING_TIME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGianLayMau);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.WARNING_SAMPLING_TIME, editData.WARNING_SAMPLING_TIME));
                    }
                    if (IsDiffString(oldData.BODY_PART_IDS, editData.BODY_PART_IDS))
                    {
                        string old = string.Join(",", oldData.BODY_PART_IDS);
                        string edit = string.Join(",", editData.BODY_PART_IDS);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BoPhan);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, old, edit));
                    }
                    if (IsDiffLong(oldData.CAPACITY, editData.CAPACITY))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DungTich);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CAPACITY, editData.CAPACITY));
                    }
                    if (IsDiffLong(oldData.EXE_SERVICE_MODULE_ID, editData.EXE_SERVICE_MODULE_ID))
                    {
                        HIS_EXE_SERVICE_MODULE exeServiceModuleOld = new HisExeServiceModuleGet().GetById(oldData.EXE_SERVICE_MODULE_ID ?? 0);
                        HIS_EXE_SERVICE_MODULE exeServiceModuleEdit = new HisExeServiceModuleGet().GetById(editData.EXE_SERVICE_MODULE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ModuleXuLy);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, exeServiceModuleOld != null ? exeServiceModuleOld.EXE_SERVICE_MODULE_NAME : "", exeServiceModuleEdit != null ? exeServiceModuleEdit.EXE_SERVICE_MODULE_NAME : ""));
                    }
                    if (IsDiffLong(oldData.SUIM_INDEX_ID, editData.SUIM_INDEX_ID))
                    {
                        HIS_SUIM_INDEX suimIndexOld = new HisSuimIndexGet().GetById(oldData.SUIM_INDEX_ID ?? 0);
                        HIS_SUIM_INDEX suimIndexEdit = new HisSuimIndexGet().GetById(editData.SUIM_INDEX_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ModuleXuLy);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, suimIndexOld != null ? suimIndexOld.SUIM_INDEX_NAME : "", suimIndexEdit != null ? suimIndexEdit.SUIM_INDEX_NAME : ""));
                    }
                    if (IsDiffShortIsField(oldData.IS_KIDNEY, editData.IS_KIDNEY))
                    {
                        string newValue = editData.IS_KIDNEY == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_KIDNEY == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChayThan);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_ANTIBIOTIC_RESISTANCE, editData.IS_ANTIBIOTIC_RESISTANCE))
                    {
                        string newValue = editData.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_ANTIBIOTIC_RESISTANCE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhangSinhDo);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_DISALLOWANCE_NO_EXECUTE, editData.IS_DISALLOWANCE_NO_EXECUTE))
                    {
                        string newValue = editData.IS_DISALLOWANCE_NO_EXECUTE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_DISALLOWANCE_NO_EXECUTE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChanKhongThucHien);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_MULTI_REQUEST, editData.IS_MULTI_REQUEST))
                    {
                        string newValue = editData.IS_MULTI_REQUEST == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_MULTI_REQUEST == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChiDinhKhac1);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_SPLIT_SERVICE_REQ, editData.IS_SPLIT_SERVICE_REQ))
                    {
                        string newValue = editData.IS_SPLIT_SERVICE_REQ == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_SPLIT_SERVICE_REQ == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TachYLenh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_SPLIT_SERVICE, editData.IS_SPLIT_SERVICE))
                    {
                        string newValue = editData.IS_SPLIT_SERVICE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_SPLIT_SERVICE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TachDichVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_OUT_PARENT_FEE, editData.IS_OUT_PARENT_FEE))
                    {
                        string newValue = editData.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_OUT_PARENT_FEE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChiPhiNgoaiGoi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_ALLOW_EXPEND, editData.IS_ALLOW_EXPEND))
                    {
                        string newValue = editData.IS_ALLOW_EXPEND == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_ALLOW_EXPEND == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChoPhepHaoPhi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_AUTO_EXPEND, editData.IS_AUTO_EXPEND))
                    {
                        string newValue = editData.IS_AUTO_EXPEND == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_AUTO_EXPEND == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TuDongTichHaoPhi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_OUT_OF_DRG, editData.IS_OUT_OF_DRG))
                    {
                        string newValue = editData.IS_OUT_OF_DRG == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_OUT_OF_DRG == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgoaiDinhSuatDRG);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_OUT_OF_MANAGEMENT, editData.IS_OUT_OF_MANAGEMENT))
                    {
                        string newValue = editData.IS_OUT_OF_MANAGEMENT == Constant.IS_TRUE ? khong : co;
                        string oldValue = oldData.IS_OUT_OF_MANAGEMENT == Constant.IS_TRUE ? khong : co;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LaDichVuQuanLyNgoai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_OTHER_SOURCE_PAID, editData.IS_OTHER_SOURCE_PAID))
                    {
                        string newValue = editData.IS_OTHER_SOURCE_PAID == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_OTHER_SOURCE_PAID == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CoNguonKhacChiTra);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_ENABLE_ASSIGN_PRICE, editData.IS_ENABLE_ASSIGN_PRICE))
                    {
                        string newValue = editData.IS_ENABLE_ASSIGN_PRICE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_ENABLE_ASSIGN_PRICE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChoChiDinhGia);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_NOT_SHOW_TRACKING, editData.IS_NOT_SHOW_TRACKING))
                    {
                        string newValue = editData.IS_NOT_SHOW_TRACKING == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_NOT_SHOW_TRACKING == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongHienThiTrenToDieuTri);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.MUST_BE_CONSULTED, editData.MUST_BE_CONSULTED))
                    {
                        string newValue = editData.MUST_BE_CONSULTED == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.MUST_BE_CONSULTED == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BatBuocCoBBHC);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_BLOCK_DEPARTMENT_TRAN, editData.IS_BLOCK_DEPARTMENT_TRAN))
                    {
                        string newValue = oldData.IS_BLOCK_DEPARTMENT_TRAN == Constant.IS_TRUE ? co : khong;
                        string oldValue = editData.IS_BLOCK_DEPARTMENT_TRAN == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChanChuyenKhoa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.ALLOW_SIMULTANEITY, editData.ALLOW_SIMULTANEITY))
                    {
                        string newValue = editData.ALLOW_SIMULTANEITY == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.ALLOW_SIMULTANEITY == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongChanThucHienCungLuc);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_NOT_REQUIRED_COMPLETE, editData.IS_NOT_REQUIRED_COMPLETE))
                    {
                        string newValue = editData.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_NOT_REQUIRED_COMPLETE == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.KhongBatBuocHoanThanh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffLong(oldData.OTHER_PAY_SOURCE_ID, editData.OTHER_PAY_SOURCE_ID))
                    {
                        HIS_OTHER_PAY_SOURCE otherPaySourceOld = new HisOtherPaySourceGet().GetById(oldData.OTHER_PAY_SOURCE_ID ?? 0);
                        HIS_OTHER_PAY_SOURCE otherPaySourceEdit = new HisOtherPaySourceGet().GetById(editData.OTHER_PAY_SOURCE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguonChiTraKhac);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, otherPaySourceOld != null ? otherPaySourceOld.OTHER_PAY_SOURCE_NAME : "", otherPaySourceEdit != null ? otherPaySourceEdit.OTHER_PAY_SOURCE_NAME : ""));
                    }
                    if (IsDiffString(oldData.OTHER_PAY_SOURCE_ICDS, editData.OTHER_PAY_SOURCE_ICDS))
                    {
                        string old = string.Join(",", oldData.OTHER_PAY_SOURCE_ICDS);
                        string edit = string.Join(",", editData.OTHER_PAY_SOURCE_ICDS);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BenhDuocHoTro);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, old, edit));
                    }
                    if (IsDiffString(oldData.ATTACH_ASSIGN_PRINT_TYPE_CODE, editData.ATTACH_ASSIGN_PRINT_TYPE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MauInKem);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ATTACH_ASSIGN_PRINT_TYPE_CODE, editData.ATTACH_ASSIGN_PRINT_TYPE_CODE));
                    }
                    if (IsDiffString(oldData.DESCRIPTION, editData.DESCRIPTION))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DienGiai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DESCRIPTION, editData.DESCRIPTION));
                    }
                    if (IsDiffString(oldData.NOTICE, editData.NOTICE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.GhiChu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NOTICE, editData.NOTICE));
                    }
                    if (IsDiffLong(oldData.TAX_RATE_TYPE, editData.TAX_RATE_TYPE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LoaiThueSuat);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TAX_RATE_TYPE, editData.TAX_RATE_TYPE));
                    }
                    if (IsDiffString(oldData.PROCESS_CODE, editData.PROCESS_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaQuyTrinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PROCESS_CODE, editData.PROCESS_CODE));
                    }
                    string updateLockTreatment;
                    string updateLockTreat;
                    if (sdo.UpdateSereServ == true)
                    {
                        string capNhatHoSo = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatHoSoChuaKhoaVienPhi);
                        string capNhatTatCa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatTatCa);
                        editFields.Add(String.Format("{0}: {1}, {2}: {3}", capNhatHoSo, "Không check", capNhatTatCa, "Có check"));
                    }
                    else if (sdo.UpdateSereServ == false)
                    {
                        string capNhatHoSo = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatHoSoChuaKhoaVienPhi);
                        string capNhatTatCa = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CapNhatTatCa);
                        editFields.Add(String.Format("{0}: {1}, {2}: {3}", capNhatHoSo, "Có check", capNhatTatCa, "Không check"));
                    }
                    new EventLogGenerator(logEnum, String.Join(". ", editFields))
                     .ServiceCode(oldData.SERVICE_CODE)
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
