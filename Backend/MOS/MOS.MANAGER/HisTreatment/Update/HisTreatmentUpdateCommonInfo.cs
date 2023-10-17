using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisPatientProgram;
using MOS.MANAGER.HisProgram;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.PayslipInfo;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisTracking;

namespace MOS.MANAGER.HisTreatment
{
    partial class HisTreatmentUpdateCommonInfo : BusinessBase
    {
        private HisMediRecordCreate hisMediRecordCreate;
        private HisPatientProgramCreate hisPatientProgramCreate;
        private HisSereServUpdatePayslipInfo hisSereServUpdatePayslipInfo;
        private HIS_TREATMENT beforeUpdate;

        internal HisTreatmentUpdateCommonInfo()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentUpdateCommonInfo(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisMediRecordCreate = new HisMediRecordCreate(param);
            this.hisPatientProgramCreate = new HisPatientProgramCreate(param);
            this.hisSereServUpdatePayslipInfo = new HisSereServUpdatePayslipInfo(param);
        }

        internal bool Run(HIS_TREATMENT data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLockHein(raw);
                valid = valid && checker.IsValidInOutTime(data.IN_TIME, data.OUT_TIME, data.CLINICAL_IN_TIME);
                valid = valid && checker.VerifyIntructionTime(data.OUT_TIME, data.ID);
                valid = valid && this.IsValidFundTime(data.FUND_FROM_TIME, data.FUND_TO_TIME);
                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();

                    HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(raw);
                    this.beforeUpdate = before;

                    raw.ICD_CODE = data.ICD_CODE;
                    raw.ICD_NAME = data.ICD_NAME;
                    raw.ICD_CAUSE_CODE = data.ICD_CAUSE_CODE;
                    raw.ICD_CAUSE_NAME = data.ICD_CAUSE_NAME;
                    raw.ICD_SUB_CODE = data.ICD_SUB_CODE;
                    raw.ICD_TEXT = data.ICD_TEXT;
                    raw.IN_TIME = data.IN_TIME;
                    raw.OUT_TIME = data.OUT_TIME;
                    raw.CLINICAL_IN_TIME = data.CLINICAL_IN_TIME;
                    raw.IS_NOT_CHECK_LHMP = data.IS_NOT_CHECK_LHMP;
                    raw.IS_NOT_CHECK_LHSP = data.IS_NOT_CHECK_LHSP;
                    raw.TREATMENT_DAY_COUNT = data.TREATMENT_DAY_COUNT;
                    raw.TREATMENT_ORDER = data.TREATMENT_ORDER;
                    raw.NEED_SICK_LEAVE_CERT = data.NEED_SICK_LEAVE_CERT;
                    raw.FUND_ID = data.FUND_ID;
                    raw.FUND_BUDGET = data.FUND_BUDGET;
                    raw.FUND_COMPANY_NAME = data.FUND_COMPANY_NAME;
                    raw.FUND_CUSTOMER_NAME = data.FUND_CUSTOMER_NAME;
                    raw.FUND_FROM_TIME = data.FUND_FROM_TIME;
                    raw.FUND_ISSUE_TIME = data.FUND_ISSUE_TIME;
                    raw.FUND_NUMBER = data.FUND_NUMBER;
                    raw.FUND_PAY_TIME = data.FUND_PAY_TIME;
                    raw.FUND_TO_TIME = data.FUND_TO_TIME;
                    raw.FUND_TYPE_NAME = data.FUND_TYPE_NAME;
                    raw.OWE_TYPE_ID = data.OWE_TYPE_ID;
                    raw.IS_EMERGENCY = data.IS_EMERGENCY;
                    //raw.IS_IN_CODE_REQUEST = data.CLINICAL_IN_TIME.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }
                    resultData = raw;
                    result = true;

                    new EventLogGenerator(EventLog.Enum.HisTreatment_SuaThongTinChung, this.LogData(before), this.LogData(resultData))
                        .TreatmentCode(resultData.TREATMENT_CODE)
                        .Run();
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool Run(HisTreatmentCommonInfoUpdateSDO data, ref HisTreatmentCommonInfoUpdateSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT raw = null;
                HisTreatmentCheck checker = new HisTreatmentCheck(param);
                valid = valid && checker.VerifyId(data.Id, ref raw);
                valid = valid && checker.IsUnLockHein(raw);
                valid = valid && checker.IsValidInOutTime(data.InTime, data.OutTime, data.ClinicalInTime);
                valid = valid && checker.VerifyIntructionTime(data.OutTime, data.Id);
                valid = valid && this.IsValidFundTime(data.FundFromTime, data.FundToTime);
                valid = valid && (!data.UpdateOtherPaySourceIdForSereServ || checker.IsUnLock(raw));
                valid = valid && (raw.IS_PAUSE != Constant.IS_TRUE || HisTreatmentCFG.UPDATE_INFO_OPTION);
                valid = valid && checker.IsValidDepartmentInTime(raw.ID, data.InTime);
                valid = valid && this.VerifyTreatmentFinishCheckTracking(data,raw);

                if (valid)
                {
                    Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();

                    HIS_TREATMENT before = Mapper.Map<HIS_TREATMENT>(raw);
                    this.beforeUpdate = before;
                    raw.ICD_CODE = data.IcdCode;
                    raw.ICD_NAME = data.IcdName;
                    raw.TRADITIONAL_ICD_CODE = data.TraditionalIcdCode;
                    raw.TRADITIONAL_ICD_NAME = data.TraditionalIcdName;
                    raw.TRADITIONAL_ICD_SUB_CODE = data.TraditionalIcdSubCode;
                    raw.TRADITIONAL_ICD_TEXT = data.TraditionalIcdText;
                    raw.ICD_CAUSE_CODE = data.IcdCauseCode;
                    raw.ICD_CAUSE_NAME = data.IcdCauseName;
                    raw.ICD_SUB_CODE = data.IcdSubCode;
                    raw.ICD_TEXT = data.IcdText;
                    raw.IN_TIME = data.InTime;
                    raw.OUT_TIME = data.OutTime;
                    raw.CLINICAL_IN_TIME = data.ClinicalInTime;
                    raw.IS_NOT_CHECK_LHMP = data.IsNotCheckLhmp ? (short?)Constant.IS_TRUE : null;
                    raw.IS_NOT_CHECK_LHSP = data.IsNotCheckLhsp ? (short?)Constant.IS_TRUE : null;
                    raw.TREATMENT_DAY_COUNT = data.TreatmentDayCount;
                    raw.TREATMENT_ORDER = data.TreatmentOrder;
                    raw.NEED_SICK_LEAVE_CERT = data.NeedSickLeaveCert;
                    raw.FUND_ID = data.FundId;
                    raw.FUND_BUDGET = data.FundBudget;
                    raw.FUND_COMPANY_NAME = data.FundCompanyName;
                    raw.FUND_CUSTOMER_NAME = data.FundCustomerName;
                    raw.FUND_FROM_TIME = data.FundFromTime;
                    raw.FUND_ISSUE_TIME = data.FundIssueTime;
                    raw.FUND_NUMBER = data.FundNumber;
                    raw.FUND_PAY_TIME = data.FundPayTime;
                    raw.FUND_TO_TIME = data.FundToTime;
                    raw.FUND_TYPE_NAME = data.FundTypeName;
                    raw.OWE_TYPE_ID = data.OweTypeId;
                    raw.OTHER_PAY_SOURCE_ID = data.OtherPaySourceId;
                    raw.DOCTOR_LOGINNAME = data.DoctorLoginName;
                    raw.DOCTOR_USERNAME = data.DoctorUserName;
                    raw.IS_EMERGENCY = data.IsEmergency ? (short?)Constant.IS_TRUE : null;
                    raw.IN_ICD_CODE = data.InIcdCode;
                    raw.IN_ICD_NAME = data.InIcdName;
                    raw.IN_ICD_SUB_CODE = data.InIcdSubCode;
                    raw.IN_ICD_TEXT = data.InIcdText;

                    //raw.IS_IN_CODE_REQUEST = data.CLINICAL_IN_TIME.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;

                    if (!DAOWorker.HisTreatmentDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatment that bai." + LogUtil.TraceData("data", data));
                    }

                    this.ProcessSereServ(data, raw);

                    result = true;

                    resultData = data;

                    resultData.InCode = raw.IN_CODE;
                    resultData.EndCode = raw.END_CODE;

                    HisTreatmentLog.Run(raw, before, data, LibraryEventLog.EventLog.Enum.HisTreatment_SuaThongTinChung1);
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private bool IsValidFundTime(long? fundFromTime, long? fundToTime)
        {
            bool result = true;
            try
            {
                if (fundFromTime.HasValue && fundToTime.HasValue && fundToTime.Value < fundFromTime.Value)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_ThoigianHieuLucTuLonHonThoiGianHieuLucDen);
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                LogSystem.Error(ex);
            }
            return result;
        }

        private string LogData(HIS_TREATMENT data)
        {
            if (data != null)
            {
                string thoiGianVaoVien = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianVaoVien);
                string thoiGianNhapVien = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianNhapVien);
                string thoiGianRaVien = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianRaVien);
                string moGioiHanTienThuocBhyt = LogCommonUtil.GetEventLogContent(EventLog.Enum.MoGioiHanTienThuocBhyt);
                string moGioiHanTienBhytTheoChuyenKhoa = LogCommonUtil.GetEventLogContent(EventLog.Enum.MoGioiHanTienBhytTheoChuyenKhoa);
                string nguonChiTraKhac = LogCommonUtil.GetEventLogContent(EventLog.Enum.NguonChiTraKhac);
                string bsDieuTri = LogCommonUtil.GetEventLogContent(EventLog.Enum.BacSyDieuTri);
                string capCuu = LogCommonUtil.GetEventLogContent(EventLog.Enum.CapCuu);

                string otherPaySourceName = data.OTHER_PAY_SOURCE_ID.HasValue ? HisOtherPaySourceCFG.DATA.Where(o => o.ID == data.OTHER_PAY_SOURCE_ID.Value).Select(o => o.OTHER_PAY_SOURCE_NAME).FirstOrDefault() : "";

                string co = LogCommonUtil.GetEventLogContent(EventLog.Enum.Co);
                string khong = LogCommonUtil.GetEventLogContent(EventLog.Enum.Khong);

                string icd1 = data.ICD_CODE != null || data.ICD_NAME != null ? string.Format("({0}) {1}", CommonUtil.NVL(data.ICD_CODE), CommonUtil.NVL(data.ICD_NAME)) : "";
                string icd2 = data.ICD_SUB_CODE != null || data.ICD_TEXT != null ? string.Format("({0}) {1}", CommonUtil.NVL(data.ICD_SUB_CODE), CommonUtil.NVL(data.ICD_TEXT)) : "";

                string icdYhct1 = data.TRADITIONAL_ICD_CODE != null || data.TRADITIONAL_ICD_NAME != null ? string.Format("({0}) {1}", CommonUtil.NVL(data.TRADITIONAL_ICD_CODE), CommonUtil.NVL(data.TRADITIONAL_ICD_NAME)) : "";
                string icdYhct2 = data.TRADITIONAL_ICD_SUB_CODE != null || data.TRADITIONAL_ICD_TEXT != null ? string.Format("({0}) {1}", CommonUtil.NVL(data.TRADITIONAL_ICD_SUB_CODE), CommonUtil.NVL(data.TRADITIONAL_ICD_TEXT)) : "";

                string inTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.IN_TIME);
                string clinicalInTime = data.CLINICAL_IN_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.CLINICAL_IN_TIME.Value) : "";
                string outTime = data.OUT_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.OUT_TIME.Value) : "";
                string isNotCheckLhmp = data.IS_NOT_CHECK_LHMP == Constant.IS_TRUE ? co : khong;
                string isNotCheckLhsp = data.IS_NOT_CHECK_LHSP == Constant.IS_TRUE ? co : khong;
                string tenBsDieuTRi = string.Format("{0} - {1}", CommonUtil.NVL(data.DOCTOR_LOGINNAME), CommonUtil.NVL(data.DOCTOR_USERNAME));
                string isEmergency = data.IS_EMERGENCY == Constant.IS_TRUE ? co : khong;
                return string.Format("ICD: {0} - {1}. ICD YHCT: {2} - {3}. {4}: {5}. {6}: {7}. {8}: {9}. {10}: {11}. {12}: {13}. {14}: {15}. {16}: {17}. {18}: {19}", icd1, icd2, icdYhct1, icdYhct2, thoiGianVaoVien, inTime, thoiGianNhapVien, clinicalInTime, thoiGianRaVien, outTime, moGioiHanTienThuocBhyt, isNotCheckLhmp, moGioiHanTienBhytTheoChuyenKhoa, isNotCheckLhsp, nguonChiTraKhac, otherPaySourceName, bsDieuTri, tenBsDieuTRi, capCuu, isEmergency);
            }
            return "";
        }

        private void ProcessSereServ(HisTreatmentCommonInfoUpdateSDO sdo, HIS_TREATMENT treatment)
        {
            if (sdo.UpdateOtherPaySourceIdForSereServ)
            {
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByTreatmentId(sdo.Id);
                hisSereServs.ForEach(o => o.OTHER_PAY_SOURCE_ID = sdo.OtherPaySourceId);

                HisSereServPayslipSDO data = new HisSereServPayslipSDO();
                data.SereServs = hisSereServs;
                data.TreatmentId = treatment.ID;
                data.Field = UpdateField.OTHER_PAY_SOURCE_ID;

                List<HIS_SERE_SERV> resultData = null;
                if (!this.hisSereServUpdatePayslipInfo.Run(data, treatment, false, ref resultData))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Ket thuc dieu tri kiem tra xem co ton tai to dieu tri nao co
        /// thoi gian dieu tri lon hon thoi gian ket thuc dieu tri
        ///các tờ điều trị có thời gian tờ điều trị lớn hơn thời gian ra viện
        /// </summary>
        /// <param name="treatmentId"></param>
        /// <param name="outTime"></param>
        /// <returns></returns>
        private bool VerifyTreatmentFinishCheckTracking(HisTreatmentCommonInfoUpdateSDO sdo, HIS_TREATMENT treatment)
        {
            bool result = true;
            try
            {
                if (treatment != null && sdo.OutTime.HasValue)
                {
                    HisTrackingFilterQuery filter = new HisTrackingFilterQuery();
                    filter.TREATMENT_ID = treatment.ID;
                    filter.TRACKING_TIME_FROM = sdo.OutTime + 1;// TRACKING_TIME > outTime : default  TRACKING_TIME >= outTime
                    List<HIS_TRACKING> trackings = new HisTrackingGet().Get(filter);
                    if (trackings != null && trackings.Count > 0)
                    {
                        MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_TonTaiToDieuTriCoThoiGianLonHonThoiGianRaVien);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisMediRecordCreate.RollbackData();
                if (this.beforeUpdate != null && !DAOWorker.HisTreatmentDAO.Update(this.beforeUpdate))
                {
                    LogSystem.Warn("Rollback treatment that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

        }
    }
}
