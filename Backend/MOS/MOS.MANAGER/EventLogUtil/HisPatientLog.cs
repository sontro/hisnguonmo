using Inventec.Common.ObjectChecker;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisMilitaryRank;
using MOS.MANAGER.HisPatientClassify;
using MOS.MANAGER.HisPosition;
using MOS.MANAGER.HisWorkPlace;
using MOS.SDO;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MOS.MANAGER.EventLogUtil
{
    class HisPatientLog
    {
        private static string FORMAT_EDIT = "{0}: {1} => {2}";

        internal static void Run(HIS_PATIENT editData, HIS_PATIENT oldData, EventLog.Enum logEnum)
        {
            try
            {
                List<string> editFields = new List<string>();
                string co = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khong);

                if (oldData != null && editData != null)
                {
                    if (IsDiffString(oldData.VIR_PATIENT_NAME, editData.VIR_PATIENT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HoTen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.VIR_PATIENT_NAME, editData.VIR_PATIENT_NAME));
                    }
                    if (IsDiffLong(oldData.GENDER_ID, editData.GENDER_ID))
                    {
                        HIS_GENDER genderOld = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == oldData.GENDER_ID);
                        HIS_GENDER genderEdit = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == editData.GENDER_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaGioiTinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, genderOld != null ? genderOld.GENDER_CODE :"", genderEdit != null ? genderEdit.GENDER_CODE : ""));
                    }
                    if (IsDiffLong(oldData.GENDER_ID, editData.GENDER_ID))
                    {
                        HIS_GENDER genderOld = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == oldData.GENDER_ID);
                        HIS_GENDER genderEdit = HisGenderCFG.DATA.FirstOrDefault(o => o.ID == editData.GENDER_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenGioiTinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, genderOld != null ? genderOld.GENDER_NAME : "", genderEdit != null ? genderEdit.GENDER_NAME : ""));
                    }
                    if (IsDiffLong(oldData.DOB, editData.DOB))
                    {
                        string dobOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldData.DOB));
                        string dobEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(editData.DOB));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgaySinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, dobOld, dobEdit));
                    }
                    if (IsDiffString(oldData.PROVINCE_CODE, editData.PROVINCE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaTinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PROVINCE_CODE, editData.PROVINCE_CODE));
                    }
                    if (IsDiffString(oldData.PROVINCE_NAME, editData.PROVINCE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenTinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PROVINCE_NAME, editData.PROVINCE_NAME));
                    }
                    if (IsDiffString(oldData.HT_PROVINCE_NAME, editData.HT_PROVINCE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenTinhHienTai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HT_PROVINCE_NAME, editData.HT_PROVINCE_NAME));
                    }
                    if (IsDiffString(oldData.DISTRICT_CODE, editData.DISTRICT_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaHuyen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DISTRICT_CODE, editData.DISTRICT_CODE));
                    }
                    if (IsDiffString(oldData.DISTRICT_NAME, editData.DISTRICT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenHuyen);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.DISTRICT_NAME, editData.DISTRICT_NAME));
                    }
                    if (IsDiffString(oldData.HT_DISTRICT_NAME, editData.HT_DISTRICT_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenHuyenHienTai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HT_DISTRICT_NAME, editData.HT_DISTRICT_NAME));
                    }
                    if (IsDiffString(oldData.COMMUNE_CODE, editData.COMMUNE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaXa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.COMMUNE_CODE, editData.COMMUNE_CODE));
                    }

                    if (IsDiffString(oldData.COMMUNE_NAME, editData.COMMUNE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenXa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.COMMUNE_NAME, editData.COMMUNE_NAME));
                    }
                    if (IsDiffString(oldData.HT_COMMUNE_NAME, editData.HT_COMMUNE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenXaHienTai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HT_COMMUNE_NAME, editData.HT_COMMUNE_NAME));
                    }
                    if (IsDiffString(oldData.ADDRESS, editData.ADDRESS))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DiaChi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ADDRESS, editData.ADDRESS));
                    }
                    if (IsDiffString(oldData.HT_ADDRESS, editData.HT_ADDRESS))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DiaChiHienTai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.HT_ADDRESS, editData.HT_ADDRESS));
                    }
                    if (IsDiffString(oldData.CCCD_NUMBER, editData.CCCD_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CMND);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CCCD_NUMBER, editData.CCCD_NUMBER));
                    }

                    if (IsDiffString(oldData.CCCD_PLACE, editData.CCCD_PLACE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiCap);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CCCD_PLACE, editData.CCCD_PLACE));
                    }
                    if (IsDiffLong(oldData.CCCD_DATE, editData.CCCD_DATE))
                    {
                        string dobOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldData.CCCD_DATE ?? 0));
                        string dobEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(editData.CCCD_DATE ?? 0));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayCap);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, dobOld, dobEdit));
                    }
                    if (IsDiffString(oldData.CMND_NUMBER, editData.CMND_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CMND);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CMND_NUMBER, editData.CMND_NUMBER));
                    }

                    if (IsDiffString(oldData.CMND_PLACE, editData.CMND_PLACE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiCap);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CMND_PLACE, editData.CMND_PLACE));
                    }
                    if (IsDiffLong(oldData.CMND_DATE, editData.CMND_DATE))
                    {
                        string dobOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldData.CMND_DATE ?? 0));
                        string dobEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(editData.CMND_DATE ?? 0));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayCap);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, dobOld, dobEdit));
                    }
                    if (IsDiffString(oldData.PASSPORT_NUMBER, editData.PASSPORT_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CMND);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PASSPORT_NUMBER, editData.PASSPORT_NUMBER));
                    }

                    if (IsDiffString(oldData.PASSPORT_PLACE, editData.PASSPORT_PLACE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiCap);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PASSPORT_PLACE, editData.PASSPORT_PLACE));
                    }
                    if (IsDiffLong(oldData.PASSPORT_DATE, editData.PASSPORT_DATE))
                    {
                        string dobOld = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(oldData.PASSPORT_DATE ?? 0));
                        string dobEdit = string.Format("{0}", Inventec.Common.DateTime.Convert.TimeNumberToDateString(editData.PASSPORT_DATE ?? 0));
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgayCap);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, dobOld, dobEdit));
                    }
                   
                    if (IsDiffString(oldData.PHONE, editData.PHONE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DienThoai);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PHONE, editData.PHONE));
                    }
                    if (IsDiffString(oldData.EMAIL, editData.EMAIL))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Email);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.EMAIL, editData.EMAIL));
                    }
                    if (IsDiffLong(oldData.WORK_PLACE_ID, editData.WORK_PLACE_ID))
                    {
                        HIS_WORK_PLACE workPlaceOld = new HisWorkPlaceGet().GetById(oldData.WORK_PLACE_ID ?? 0);
                        HIS_WORK_PLACE workPlaceEdit = new HisWorkPlaceGet().GetById(editData.WORK_PLACE_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiLamViec);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, workPlaceOld != null ? workPlaceOld.WORK_PLACE_NAME : "", workPlaceEdit != null ? workPlaceEdit.WORK_PLACE_NAME :""));
                    }
                    if (IsDiffString(oldData.WORK_PLACE, editData.WORK_PLACE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NoiLamViecKhac);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.WORK_PLACE, editData.WORK_PLACE));
                    }
                    if (IsDiffString(oldData.NATIONAL_CODE, editData.NATIONAL_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaQuocTich);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NATIONAL_CODE, editData.NATIONAL_CODE));
                    }
                    if (IsDiffString(oldData.NATIONAL_NAME, editData.NATIONAL_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenQuocTich);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.NATIONAL_NAME, editData.NATIONAL_NAME));
                    }
                    if (IsDiffString(oldData.UUID_BHYT_NUMBER, editData.UUID_BHYT_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TheBHYT);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.UUID_BHYT_NUMBER, editData.UUID_BHYT_NUMBER));
                    }
                    if (IsDiffString(oldData.SOCIAL_INSURANCE_NUMBER, editData.SOCIAL_INSURANCE_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaBHXH);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.SOCIAL_INSURANCE_NUMBER, editData.SOCIAL_INSURANCE_NUMBER));
                    }
                    if (IsDiffString(oldData.CAREER_CODE, editData.CAREER_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaNgheNghiep);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.CAREER_CODE, editData.CAREER_CODE));
                    }
                    if (IsDiffLong(oldData.CAREER_ID, editData.CAREER_ID))
                    {
                        HIS_CAREER careerOld = HisCareerCFG.DATA.FirstOrDefault(o => o.ID == oldData.CAREER_ID);
                        HIS_CAREER careerEdit = HisCareerCFG.DATA.FirstOrDefault(o => o.ID == editData.CAREER_ID);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenNgheNghiep);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, careerOld != null ? careerOld.CAREER_NAME : "", careerEdit != null ? careerEdit.CAREER_NAME : ""));
                    }
                    if (IsDiffString(oldData.ETHNIC_CODE, editData.ETHNIC_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaDanToc);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ETHNIC_CODE, editData.ETHNIC_CODE));
                    }
                    if (IsDiffString(oldData.ETHNIC_NAME, editData.ETHNIC_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenDanToc);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ETHNIC_NAME, editData.ETHNIC_NAME));
                    }
                    if (IsDiffString(oldData.RELIGION_NAME, editData.RELIGION_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TonGiao);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RELIGION_NAME, editData.RELIGION_NAME));
                    }
                    if (IsDiffString(oldData.ACCOUNT_NUMBER, editData.ACCOUNT_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.STK);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.ACCOUNT_NUMBER, editData.ACCOUNT_NUMBER));
                    }

                    if (IsDiffString(oldData.TAX_CODE, editData.TAX_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaSoThue);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.TAX_CODE, editData.TAX_CODE));
                    }
                    if (IsDiffLong(oldData.MILITARY_RANK_ID, editData.MILITARY_RANK_ID))
                    {
                        HIS_MILITARY_RANK militaryRankOld = new HisMilitaryRankGet().GetById(oldData.MILITARY_RANK_ID ?? 0);
                        HIS_MILITARY_RANK militaryRankEdit = new HisMilitaryRankGet().GetById(editData.MILITARY_RANK_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuanHam);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, militaryRankOld != null ? militaryRankOld.MILITARY_RANK_NAME : "", militaryRankEdit != null ? militaryRankEdit.MILITARY_RANK_NAME : ""));
                    }
                    if (IsDiffLong(oldData.POSITION_ID, editData.POSITION_ID))
                    {
                        HIS_POSITION potionOld = new HisPositionGet().GetById(oldData.POSITION_ID ?? 0);
                        HIS_POSITION potionEdit = new HisPositionGet().GetById(editData.POSITION_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ChucVu);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, potionOld != null ? potionOld.POSITION_NAME : "", potionEdit != null ? potionEdit.POSITION_NAME : ""));
                    }
                    if (IsDiffString(oldData.BLOOD_ABO_CODE, editData.BLOOD_ABO_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NhomMau);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.BLOOD_ABO_CODE, editData.BLOOD_ABO_CODE));
                    }
                    if (IsDiffLong(oldData.PATIENT_CLASSIFY_ID, editData.PATIENT_CLASSIFY_ID))
                    {
                        HIS_PATIENT_CLASSIFY patientClassifyOld = new HisPatientClassifyGet().GetById(oldData.PATIENT_CLASSIFY_ID ?? 0);
                        HIS_PATIENT_CLASSIFY patientClassifyEdit = new HisPatientClassifyGet().GetById(editData.PATIENT_CLASSIFY_ID ?? 0);
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DoiTuongChiTiet);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, patientClassifyOld != null ? patientClassifyOld.PATIENT_CLASSIFY_NAME : "", patientClassifyEdit != null ? patientClassifyEdit.PATIENT_CLASSIFY_NAME : ""));
                    }
                    if (IsDiffString(oldData.BLOOD_RH_CODE, editData.BLOOD_RH_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.RH);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.BLOOD_RH_CODE, editData.BLOOD_RH_CODE));
                    }
                    if (IsDiffString(oldData.PATIENT_STORE_CODE, editData.PATIENT_STORE_CODE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MaLuuTru);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PATIENT_STORE_CODE, editData.PATIENT_STORE_CODE));
                    }
                    if (IsDiffString(oldData.FATHER_NAME, editData.FATHER_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HoTenBo);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FATHER_NAME, editData.FATHER_NAME));
                    }
                    if (IsDiffString(oldData.FATHER_EDUCATIIONAL_LEVEL, editData.FATHER_EDUCATIIONAL_LEVEL))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TrinhDoVanHoa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FATHER_EDUCATIIONAL_LEVEL, editData.FATHER_EDUCATIIONAL_LEVEL));
                    }
                    if (IsDiffString(oldData.FATHER_CAREER, editData.FATHER_CAREER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgheNghiep);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.FATHER_CAREER, editData.FATHER_CAREER));
                    }
                    if (IsDiffString(oldData.MOTHER_NAME, editData.MOTHER_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.HoTenMe);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MOTHER_NAME, editData.MOTHER_NAME));
                    }
                    if (IsDiffString(oldData.MOTHER_EDUCATIIONAL_LEVEL, editData.MOTHER_EDUCATIIONAL_LEVEL))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TrinhDoVanHoa);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MOTHER_EDUCATIIONAL_LEVEL, editData.MOTHER_EDUCATIIONAL_LEVEL));
                    }
                    if (IsDiffString(oldData.MOTHER_CAREER, editData.MOTHER_CAREER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NgheNghiep);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.MOTHER_CAREER, editData.MOTHER_CAREER));
                    }
                    if (IsDiffString(oldData.RELATIVE_NAME, editData.RELATIVE_NAME))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.NguoiNha);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RELATIVE_NAME, editData.RELATIVE_NAME));
                    }
                    if (IsDiffString(oldData.RELATIVE_TYPE, editData.RELATIVE_TYPE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.QuanHe);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RELATIVE_TYPE, editData.RELATIVE_TYPE));
                    }
                    if (IsDiffString(oldData.RELATIVE_CMND_NUMBER, editData.RELATIVE_CMND_NUMBER))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.CMND);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RELATIVE_CMND_NUMBER, editData.RELATIVE_CMND_NUMBER));
                    }
                    if (IsDiffString(oldData.RELATIVE_ADDRESS, editData.RELATIVE_ADDRESS))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DiaChi);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RELATIVE_ADDRESS, editData.RELATIVE_ADDRESS));
                    }
                    if (IsDiffString(oldData.RELATIVE_PHONE, editData.RELATIVE_PHONE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Sdt);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RELATIVE_PHONE, editData.RELATIVE_PHONE));
                    }
                    if (IsDiffString(oldData.RELATIVE_MOBILE, editData.RELATIVE_MOBILE))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DiDong);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.RELATIVE_MOBILE, editData.RELATIVE_MOBILE));
                    }
                    if (IsDiffShortIsField(oldData.IS_CHRONIC, editData.IS_CHRONIC))
                    {
                        string newValue = editData.IS_CHRONIC == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_CHRONIC == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BenhNhanManTinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffShortIsField(oldData.IS_TUBERCULOSIS, editData.IS_TUBERCULOSIS))
                    {
                        string newValue = editData.IS_TUBERCULOSIS == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_TUBERCULOSIS == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BenhNhanLao);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                    if (IsDiffString(oldData.PT_PATHOLOGICAL_HISTORY, editData.PT_PATHOLOGICAL_HISTORY))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TienXuBenhCuaBenhNhan);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PT_PATHOLOGICAL_HISTORY, editData.PT_PATHOLOGICAL_HISTORY));
                    }
                    if (IsDiffString(oldData.PT_PATHOLOGICAL_HISTORY_FAMILY, editData.PT_PATHOLOGICAL_HISTORY_FAMILY))
                    {
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TienXuBenhGiaDinh);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldData.PT_PATHOLOGICAL_HISTORY_FAMILY, editData.PT_PATHOLOGICAL_HISTORY_FAMILY));
                    }

                    if (IsDiffShortIsField(oldData.IS_HIV, editData.IS_HIV))
                    {
                        string newValue = editData.IS_HIV == Constant.IS_TRUE ? co : khong;
                        string oldValue = oldData.IS_HIV == Constant.IS_TRUE ? co : khong;
                        string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.BenhNhanHiv);
                        editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                    }
                   
                    new EventLogGenerator(logEnum, String.Join(". ", editFields))
                     .PatientCode(oldData.PATIENT_CODE)
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
