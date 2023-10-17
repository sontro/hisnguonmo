using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisExpMestMedicine;

namespace MOS.MANAGER.HisServiceReq.UpdateCommonInfo
{
    partial class HisServiceReqUpdateCommonInfo : BusinessBase
    {
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServExtUpdate hisSereServExtUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisServiceReqMetyUpdate hisServiceReqMetyUpdate;
        private HisServiceReqRequestOrder hisServiceReqRequestOrder;

        internal HisServiceReqUpdateCommonInfo()
            : base()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
        }

        internal HisServiceReqUpdateCommonInfo(CommonParam param)
            : base(param)
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisServiceReqMetyUpdate = new HisServiceReqMetyUpdate(param);
            this.hisServiceReqRequestOrder = new HisServiceReqRequestOrder(param);
        }

        internal bool UpdateCommonInfo(HIS_SERVICE_REQ data)
        {
            bool result = false;

            try
            {
                bool valid = true;
                HIS_SERVICE_REQ raw = null;
                HIS_TREATMENT treatmentRaw = null;
                List<HIS_SERE_SERV_EXT> ssExts = null;
                List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                List<HIS_SERVICE_REQ_METY> serviceReqMetys = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisServiceCheck svChecker = new HisServiceCheck(param);
                HisServiceReqUpdateCommonInfoCheck commonInfoChecker = new HisServiceReqUpdateCommonInfoCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && commonInfoChecker.IsValidData(data, raw);
                valid = valid && treatmentChecker.VerifyId(raw.TREATMENT_ID, ref treatmentRaw);
                valid = valid && (HisEmployeeUtil.IsAdmin(loginName) || treatmentChecker.IsUnLockHein(treatmentRaw));
                valid = valid && (HisEmployeeUtil.IsAdmin(loginName) || treatmentChecker.IsUnpause(treatmentRaw));
                valid = valid && checker.IsValidInstructionTime(data.INTRUCTION_TIME, treatmentRaw);
                valid = valid && checker.IsNotAprovedSurgeryRemuneration(raw);//da duyet cong PTTT thi ko cho phep sua thoi gian y lenh
                valid = valid && checker.VerifyAssignedExecuteUser(data.ASSIGNED_EXECUTE_LOGINNAME, raw.EXECUTE_ROOM_ID);
                valid = valid && checker.IsValidStartOrFinishTimeForSereServExt(data.ID, data.START_TIME, data.FINISH_TIME, ref ssExts);
                valid = valid && svChecker.IsValidMinMaxServiceTime(ssExts, null);
                valid = valid && commonInfoChecker.IsValidPresBidDate(data, raw);
                if (valid)
                {

                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(raw);

                    List<long> listUseTimeTo = new List<long>();
                    if (!this.ProcessExpMestMedicine(data, before, ref expMestMedicines, ref listUseTimeTo))
                    {
                        throw new Exception("ProcessExpMestMedicine Failed!");
                    }
                    if (!this.ProcessServiceReqMety(data, before, ref serviceReqMetys, ref listUseTimeTo))
                    {
                        throw new Exception("ProcessServiceReqMety Failed!");
                    }
                    long? maxUseTimeTo = listUseTimeTo.Count > 0 ? listUseTimeTo.Max() : 0;
                    raw.USE_TIME_TO = maxUseTimeTo > 0 ? maxUseTimeTo : null;

                    raw.ICD_TEXT = data.ICD_TEXT;
                    raw.ICD_SUB_CODE = data.ICD_SUB_CODE;
                    raw.ICD_CODE = data.ICD_CODE;
                    raw.ICD_NAME = data.ICD_NAME;
                    raw.ICD_CAUSE_CODE = data.ICD_CAUSE_CODE;
                    raw.ICD_CAUSE_NAME = data.ICD_CAUSE_NAME;
                    raw.INTRUCTION_TIME = data.INTRUCTION_TIME;
                    raw.INTRUCTION_DATE = data.INTRUCTION_TIME - data.INTRUCTION_TIME % 1000000;
                    raw.START_TIME = data.START_TIME;
                    raw.FINISH_TIME = data.FINISH_TIME;
                    raw.USE_TIME = data.USE_TIME;
                    raw.EXECUTE_LOGINNAME = data.EXECUTE_LOGINNAME;
                    raw.EXECUTE_USERNAME = data.EXECUTE_USERNAME;
                    raw.PRIORITY = data.PRIORITY;
                    raw.IS_EMERGENCY = data.IS_EMERGENCY;
                    raw.IS_NOT_REQUIRE_FEE = data.IS_NOT_REQUIRE_FEE;
                    raw.RESULT_APPROVER_LOGINNAME = data.RESULT_APPROVER_LOGINNAME;
                    raw.RESULT_APPROVER_USERNAME = data.RESULT_APPROVER_USERNAME;
                    raw.CONSULTANT_LOGINNAME = data.CONSULTANT_LOGINNAME;
                    raw.CONSULTANT_USERNAME = data.CONSULTANT_USERNAME;
                    raw.ASSIGNED_EXECUTE_LOGINNAME = data.ASSIGNED_EXECUTE_LOGINNAME;
                    raw.ASSIGNED_EXECUTE_USERNAME = data.ASSIGNED_EXECUTE_USERNAME;
                    raw.IS_NOT_USE_BHYT = data.IS_NOT_USE_BHYT;
                    raw.APPOINTMENT_DESC = data.APPOINTMENT_DESC;
                    raw.APPOINTMENT_TIME = data.APPOINTMENT_TIME;
                    raw.TDL_APPOINTMENT_DATE = data.APPOINTMENT_TIME - (data.APPOINTMENT_TIME % 1000000);
                    if (raw.IS_SENT_EXT == Constant.IS_TRUE)
                    {
                        raw.IS_UPDATED_EXT = Constant.IS_TRUE;
                        if (raw.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN)
                        {
                            raw.LIS_STT_ID = MOS.MANAGER.HisServiceReq.Test.LisSenderV1.LisUtil.LIS_STT_ID__UPDATE;
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(data.REQUEST_LOGINNAME))
                    {
                        raw.REQUEST_LOGINNAME = data.REQUEST_LOGINNAME;
                        raw.REQUEST_USERNAME = data.REQUEST_USERNAME;
                        raw.REQUEST_USER_TITLE = HisEmployeeUtil.GetTitle(data.REQUEST_LOGINNAME);
                    }

                    if (data.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH)
                    {
                        raw.NOTE = data.NOTE;
                    }

                    //sua thong tin chung thi gui lai sang he thong tich hop LIS/PACS
                    if (PacsCFG.SEND_WHEN_CHANGE_STATUS
                        && raw.IS_SENT_EXT == Constant.IS_TRUE
                        && raw.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT
                        && (HisServiceReqTypeCFG.PACS_TYPE_IDs.Contains(data.SERVICE_REQ_TYPE_ID) || raw.ALLOW_SEND_PACS == Constant.IS_TRUE))
                    {
                        this.hisServiceReqRequestOrder.Run(data.ID);
                    }

                    // Cap nhat thong tin vao y lenh
                    if (!this.hisServiceReqUpdate.Update(raw, before, false))
                    {
                        throw new Exception("Cap nhat thong tin vao y lenh that bai. Rollback");
                    }

                    if (IsNotNullOrEmpty(ssExts))
                    {
                        if (!this.hisSereServExtUpdate.UpdateList(ssExts))
                        {
                            throw new Exception("Cap nhat thong tin thoi gian bat dau ket thuc cua dich vu that bai");
                        }
                    }

                    this.UpdateExpMest(raw);

                    //sua thong tin chung thi gui lai sang he thong tich hop LIS/PACS
                    if (raw.IS_UPDATED_EXT == Constant.IS_TRUE)
                    {
                        this.UpdateSereServIsSentExt(raw);
                    }

                    if (IsNotNullOrEmpty(expMestMedicines) && !this.hisExpMestMedicineUpdate.UpdateList(expMestMedicines))
                    {
                        throw new Exception("Cap nhat thong tin HIS_EXP_MEST_MEDICINE that bai");
                    }
                    if (IsNotNullOrEmpty(serviceReqMetys) && !this.hisServiceReqMetyUpdate.UpdateList(serviceReqMetys))
                    {
                        throw new Exception("Cap nhat thong tin HIS_SERVICE_REQ_METY that bai");
                    }

                    new EventLogGenerator(EventLog.Enum.HisServiceReq_SuaThongTinChung, this.LogData(before), this.LogData(raw))
                        .TreatmentCode(raw.TDL_TREATMENT_CODE)
                        .ServiceReqCode(raw.SERVICE_REQ_CODE)
                        .Run();

                    //Trong truong hop thoi gian y lenh thay doi co the co kha nang thay doi lai thong tin
                    //dien doi tuong tuong ung voi y lenh --> can thuc hien cap nhat lai
                    if (raw.INTRUCTION_TIME != before.INTRUCTION_TIME)
                    {
                        this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatmentRaw, false);
                        if (!this.hisSereServUpdateHein.UpdateDb())
                        {
                            throw new Exception("Cap nhat thong tin bang ke (his_sere_serv) that bai. Rollback du lieu");
                        }
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        private bool ProcessExpMestMedicine(HIS_SERVICE_REQ data, HIS_SERVICE_REQ before, ref List<HIS_EXP_MEST_MEDICINE> expMestMedicinesChanged, ref List<long> listUseTimeTo)
        {
            bool result = true;
            try
            {
                var expMestMedicines = new HisExpMestMedicineGet().Get(new HisExpMestMedicineFilterQuery() { TDL_SERVICE_REQ_ID = data.ID });
                expMestMedicinesChanged = new List<HIS_EXP_MEST_MEDICINE>();
                if (IsNotNullOrEmpty(expMestMedicines))
                {
                    foreach (var item in expMestMedicines)
                    {
                        var oldUseTimeTo = item.USE_TIME_TO;

                        if (item.USE_TIME_TO > 0)
                        {
                            var diffTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item.USE_TIME_TO.Value) - Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(before.USE_TIME > 0 ? before.USE_TIME.Value : before.INTRUCTION_TIME);
                            long dayCount = (diffTime.HasValue && diffTime.Value.Days > 0) ? diffTime.Value.Days : 0;
                            DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.USE_TIME > 0 ? data.USE_TIME.Value : data.INTRUCTION_TIME).Value;
                            DateTime useTimeTo = time.AddDays(dayCount);
                            item.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                        }
                        if (item.USE_TIME_TO != oldUseTimeTo)
                            expMestMedicinesChanged.Add(item);
                    }
                    if (IsNotNullOrEmpty(expMestMedicines))
                        listUseTimeTo.AddRange(expMestMedicines.Select(o => o.USE_TIME_TO ?? 0));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool ProcessServiceReqMety(HIS_SERVICE_REQ data, HIS_SERVICE_REQ before, ref List<HIS_SERVICE_REQ_METY> serviceReqMetysChanged, ref List<long> listUseTimeTo)
        {
            bool result = true;
            try
            {
                var serviceReqMetys = new HisServiceReqMetyGet().GetByServiceReqId(data.ID);
                serviceReqMetysChanged = new List<HIS_SERVICE_REQ_METY>();
                if (IsNotNullOrEmpty(serviceReqMetys))
                {
                    foreach (var item in serviceReqMetys)
                    {
                        var oldUseTimeTo = item.USE_TIME_TO;

                        if (item.USE_TIME_TO > 0)
                        {
                            var diffTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item.USE_TIME_TO.Value) - Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(before.USE_TIME > 0 ? before.USE_TIME.Value : before.INTRUCTION_TIME);
                            long dayCount = (diffTime.HasValue && diffTime.Value.Days > 0) ? diffTime.Value.Days : 0;
                            DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.USE_TIME > 0 ? data.USE_TIME.Value : data.INTRUCTION_TIME).Value;
                            DateTime useTimeTo = time.AddDays(dayCount);
                            item.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                        }
                        if (item.USE_TIME_TO != oldUseTimeTo)
                            serviceReqMetysChanged.Add(item);
                    }
                    if (IsNotNullOrEmpty(serviceReqMetys))
                        listUseTimeTo.AddRange(serviceReqMetys.Select(o => o.USE_TIME_TO ?? 0));
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private void UpdateExpMest(HIS_SERVICE_REQ serviceReq)
        {
            if (serviceReq != null && HisServiceReqTypeCFG.PRESCRIPTION_TYPE_IDs.Contains(serviceReq.SERVICE_REQ_TYPE_ID))
            {
                string sql = "UPDATE HIS_EXP_MEST SET REQ_LOGINNAME = :param1, REQ_USERNAME = :param2, REQ_USER_TITLE = :param3 WHERE SERVICE_REQ_ID = :param4";

                if (!DAOWorker.SqlDAO.Execute(sql, serviceReq.REQUEST_LOGINNAME, serviceReq.REQUEST_USERNAME, serviceReq.REQUEST_USER_TITLE, serviceReq.ID))
                {
                    throw new Exception("Cap nhat thong tin ExpMest that bai. Rollback");
                }
            }

        }

        private void UpdateSereServIsSentExt(HIS_SERVICE_REQ serviceReq)
        {
            if (serviceReq != null && serviceReq.IS_SENT_EXT == Constant.IS_TRUE && serviceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT)
            {
                string sql = "UPDATE HIS_SERE_SERV SET IS_SENT_EXT = null WHERE SERVICE_REQ_ID = :param1";

                if (!DAOWorker.SqlDAO.Execute(sql, serviceReq.ID))
                {
                    throw new Exception("Cap nhat thong tin SereServ that bai. Rollback");
                }
            }
        }

        private string LogData(HIS_SERVICE_REQ data)
        {
            if (data != null)
            {
                string thoiGianYLenh = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianYLenh);
                string thoiGianKetThuc = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianKetThuc);
                string thoiGianBatDau = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianBatDau);
                string nguoiThucHien = LogCommonUtil.GetEventLogContent(EventLog.Enum.NguoiThucHien);
                string nguoiTuVan = LogCommonUtil.GetEventLogContent(EventLog.Enum.NguoiTuVan);
                string capCuu = LogCommonUtil.GetEventLogContent(EventLog.Enum.CapCuu);
                string thuSau = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThuSau);
                string uuTien = LogCommonUtil.GetEventLogContent(EventLog.Enum.UuTien);
                string nguoiDuocChiDinhXL = LogCommonUtil.GetEventLogContent(EventLog.Enum.NguoiDuocChiDinhXuLy);
                string loiDan = LogCommonUtil.GetEventLogContent(EventLog.Enum.LoiDan);
                string thoiGianHenKham = LogCommonUtil.GetEventLogContent(EventLog.Enum.ThoiGianHenKham);

                string cc = data.IS_EMERGENCY == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.Co) : LogCommonUtil.GetEventLogContent(EventLog.Enum.Khong);
                string ts = data.IS_NOT_REQUIRE_FEE == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.Co) : LogCommonUtil.GetEventLogContent(EventLog.Enum.Khong);
                string khongHuongBhyt = data.IS_NOT_USE_BHYT == Constant.IS_TRUE ? String.Format(". {0}", LogCommonUtil.GetEventLogContent(EventLog.Enum.KhongHuongBhyt)) : "";

                string ut = data.PRIORITY == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.Co) : LogCommonUtil.GetEventLogContent(EventLog.Enum.Khong);

                string icd1 = data.ICD_CODE != null || data.ICD_NAME != null ? string.Format("({0}) {1}", CommonUtil.NVL(data.ICD_CODE), CommonUtil.NVL(data.ICD_NAME)) : "";
                string icd2 = data.ICD_SUB_CODE != null || data.ICD_TEXT != null ? string.Format("({0}) {1}", CommonUtil.NVL(data.ICD_SUB_CODE), CommonUtil.NVL(data.ICD_TEXT)) : "";
                string instructionTime = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.INTRUCTION_TIME);
                string startTime = data.START_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.START_TIME.Value) : "";
                string finishTime = data.FINISH_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.FINISH_TIME.Value) : "";
                string appointmentDesc = data.APPOINTMENT_DESC != null ? data.APPOINTMENT_DESC : "";
                string appointmentTime = data.APPOINTMENT_TIME.HasValue ? Inventec.Common.DateTime.Convert.TimeNumberToTimeString(data.APPOINTMENT_TIME.Value) : "";

                return string.Format("ICD: {0} - {1}. {2}: {3}. {4}: {5}. {6}: {7}. {8}: {9} - {10}. {11}: {12} - {13}. {14}: {15}. {16}: {17}. {18}: {19}. {20}: {21} - {22}{23}. {24}: {25}. {26}: {27}", icd1, icd2, thoiGianYLenh, instructionTime, thoiGianBatDau, startTime, thoiGianKetThuc, finishTime, nguoiThucHien, data.EXECUTE_LOGINNAME, data.EXECUTE_USERNAME, nguoiTuVan, data.CONSULTANT_LOGINNAME, data.CONSULTANT_USERNAME, capCuu, cc, thuSau, ts, uuTien, ut, nguoiDuocChiDinhXL, data.ASSIGNED_EXECUTE_LOGINNAME, data.ASSIGNED_EXECUTE_USERNAME, khongHuongBhyt, "Lời dặn bác sĩ", appointmentDesc, "Thời gian hẹn khám", appointmentTime);
            }
            return "";
        }

        private void Rollback()
        {
            try
            {
                this.hisSereServExtUpdate.RollbackData();
                this.hisServiceReqUpdate.RollbackData();
                if (this.hisSereServUpdateHein != null)
                {
                    this.hisSereServUpdateHein.RollbackData();
                }
                this.hisExpMestMedicineUpdate.RollbackData();
                this.hisServiceReqMetyUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
