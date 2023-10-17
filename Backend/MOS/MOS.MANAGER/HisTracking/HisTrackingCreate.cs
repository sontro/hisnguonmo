using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;
using MOS.MANAGER.HisCare;
using AutoMapper;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.HisDebate;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;

namespace MOS.MANAGER.HisTracking
{
    partial class HisTrackingCreate : BusinessBase
    {
        private HIS_TRACKING recentHisTracking;

        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisCareUpdate hisCareUpdate;
        private HisDebateUpdate hisDebateUpdate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private static string FORMAT_FIELD = "{0}: {1}";

        internal HisTrackingCreate()
            : base()
        {
            this.Init();
        }

        internal HisTrackingCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisCareUpdate = new HisCareUpdate(param);
            this.hisDebateUpdate = new HisDebateUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
        }

        public bool Create(HisTrackingSDO data, ref HIS_TRACKING resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO sdo = null;
                HIS_TREATMENT treatment = null;
                
                bool valid = true;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisTrackingCheck trakingCheck = new HisTrackingCheck(param);
                HIS_TRACKING raw = null;
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.Tracking);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref sdo);
                valid = valid && treatmentChecker.VerifyId(data.Tracking.TREATMENT_ID, ref treatment);
                

                if (valid)
                {
                    List<string> eventLogs = null;
                    List<string> eventLogService = null;
                    data.Tracking.DEPARTMENT_ID = sdo.DepartmentId;
                    data.Tracking.ROOM_ID = sdo.RoomId;
                    this.ProcessTracking(data);
                    this.ProcessServiceReq(data, ref eventLogs, ref eventLogService);
                    this.ProcessCare(data.CareIds);
                    this.ProcessDebate(data.DebateIds);
                    this.ProcessTreatment(data, treatment);

                    valid = valid && trakingCheck.VerifyId(data.Tracking.ID, ref raw);
                    HIS_TREATMENT treat = new HisTreatmentGet().GetById(raw.TREATMENT_ID);
                    string eventLog = "";
                    ProcessEventLog(data, eventLogService, ref eventLog);
                    new EventLogGenerator(EventLog.Enum.HisTracking_TaoToDieuTri, eventLog)
                        .TreatmentCode(treat.TREATMENT_CODE)
                        .Run();

                    if (HisTrackingCFG.SERVICE_REQ_ICD_OPTION && IsNotNullOrEmpty(eventLogs))
                    {
                        string ttStr = string.Join(";", eventLogs);

                        new EventLogGenerator(EventLog.Enum.HisTracking_SuaThongTinChungYLenh, ttStr)
                            .TreatmentCode(treat.TREATMENT_CODE)
                            .Run();
                    }

                    resultData = this.recentHisTracking;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessEventLog(HisTrackingSDO data, List<string> eventLogService, ref string eventLog)
        {
            try
            {
                List<string> editFields = new List<string>();

                editFields.Add(String.Format("ICD: ({0}){1}", data.Tracking.ICD_CODE, data.Tracking.ICD_NAME));

                string newValue = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(data.Tracking.TRACKING_TIME);
                string fieldNameTime = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGian);
                editFields.Add(String.Format(FORMAT_FIELD, fieldNameTime, newValue));

                if (!String.IsNullOrWhiteSpace(data.Tracking.CONTENT))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DienBien);
                    editFields.Add(String.Format(FORMAT_FIELD, fieldName, data.Tracking.CONTENT));
                }
                if (!String.IsNullOrWhiteSpace(data.Tracking.SUBCLINICAL_PROCESSES))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DienBienCls);
                    editFields.Add(String.Format(FORMAT_FIELD, fieldName, data.Tracking.SUBCLINICAL_PROCESSES));
                }
                if (!String.IsNullOrWhiteSpace(data.Tracking.MEDICAL_INSTRUCTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhuongPhapXuLy);
                    editFields.Add(String.Format(FORMAT_FIELD, fieldName,  data.Tracking.MEDICAL_INSTRUCTION));
                }
                if (!String.IsNullOrWhiteSpace(data.Tracking.CARE_INSTRUCTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TheoDoiChamSoc);
                    editFields.Add(String.Format(FORMAT_FIELD, fieldName, data.Tracking.CARE_INSTRUCTION));
                }

                if (IsNotNullOrEmpty(eventLogService))
                {
                    editFields.Add(string.Join(", ", eventLogService));
                }

                eventLog = String.Join(". ", editFields);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessServiceReq(HisTrackingSDO data, ref List<string> eventLogs, ref List<string> eventLogService)
        {
            List<string> sqls = new List<string>();

            if (IsNotNullOrEmpty(data.ServiceReqs))
            {
                List<long> serviceReqIds = data.ServiceReqs.Select(o => o.ServiceReqId).ToList();
                List<HIS_SERVICE_REQ> hisServiceReqs = new HisServiceReqGet().GetByIds(serviceReqIds);

                if (!IsNotNullOrEmpty(hisServiceReqs))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("service_req_id ko ton tai." + LogUtil.TraceData("ServiceReqIds", serviceReqIds));
                }

                List<HIS_SERVICE_REQ> invalids = hisServiceReqs.Where(o => o.TREATMENT_ID != data.Tracking.TREATMENT_ID).ToList();

                if (IsNotNullOrEmpty(invalids))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai service_req khong thuoc treatment." + LogUtil.TraceData("invalids", invalids));
                }

                foreach (HIS_SERVICE_REQ r in hisServiceReqs)
                {
                    TrackingServiceReq t = data.ServiceReqs.Where(o => o.ServiceReqId == r.ID).FirstOrDefault();
                    if (t != null)
                    {
                        string sql = string.Format("UPDATE HIS_SERVICE_REQ SET TRACKING_ID = {0} ", this.recentHisTracking.ID);
                        if (t.IsNotShowMaterial)
                        {
                            sql += string.Format(", IS_NOT_SHOW_MATERIAL_TRACKING = {0} ", (short?)Constant.IS_TRUE);
                        }
                        else
                        {
                            sql += string.Format(", IS_NOT_SHOW_MATERIAL_TRACKING = {0} ", "NULL");
                        }
                        if (t.IsNotShowMedicine)
                        {
                            sql += string.Format(", IS_NOT_SHOW_MEDICINE_TRACKING = {0} ", (short?)Constant.IS_TRUE);
                        }
                        else
                        {
                            sql += string.Format(", IS_NOT_SHOW_MEDICINE_TRACKING = {0} ", "NULL");
                        }
                        if (t.IsNotShowOutMate)
                        {
                            sql += string.Format(", IS_NOT_SHOW_OUT_MATE_TRACKING = {0} ", (short?)Constant.IS_TRUE);
                        }
                        else
                        {
                            sql += string.Format(", IS_NOT_SHOW_OUT_MATE_TRACKING = {0} ", "NULL");
                        }
                        if (t.IsNotShowOutMedi)
                        {
                            sql += string.Format(", IS_NOT_SHOW_OUT_MEDI_TRACKING = {0} ", (short?)Constant.IS_TRUE);
                        }
                        else
                        {
                            sql += string.Format(", IS_NOT_SHOW_OUT_MEDI_TRACKING = {0} ", "NULL");
                        }

                        if (HisTrackingCFG.SERVICE_REQ_ICD_OPTION)
                        {
                            if (eventLogs == null)
                            {
                                eventLogs = new List<string>();
                            }

                            if (!String.IsNullOrWhiteSpace(data.Tracking.ICD_CODE))
                            {
                                sql += string.Format(", ICD_CODE = '{0}' ", data.Tracking.ICD_CODE);
                            }
                            else
                            {
                                sql += string.Format(", ICD_CODE = {0} ", "NULL");
                            }

                            if (!String.IsNullOrWhiteSpace(data.Tracking.ICD_NAME))
                            {
                                sql += string.Format(", ICD_NAME = '{0}' ", data.Tracking.ICD_NAME);
                            }
                            else
                            {
                                sql += string.Format(", ICD_NAME = {0} ", "NULL");
                            }

                            if (!String.IsNullOrWhiteSpace(data.Tracking.ICD_SUB_CODE))
                            {
                                sql += string.Format(", ICD_SUB_CODE = '{0}' ", data.Tracking.ICD_SUB_CODE);
                            }
                            else
                            {
                                sql += string.Format(", ICD_SUB_CODE = {0} ", "NULL");
                            }

                            if (!String.IsNullOrWhiteSpace(data.Tracking.ICD_TEXT))
                            {
                                sql += string.Format(", ICD_TEXT = '{0}' ", data.Tracking.ICD_TEXT);
                            }
                            else
                            {
                                sql += string.Format(", ICD_TEXT = {0} ", "NULL");
                            }

                            string icd = string.Format("ICD:({0}){1} - ({2}){3} => ({4}){5} - ({6}){7}", r.ICD_CODE, r.ICD_NAME, r.ICD_SUB_CODE, r.ICD_TEXT, data.Tracking.ICD_CODE, data.Tracking.ICD_NAME, data.Tracking.ICD_SUB_CODE, data.Tracking.ICD_TEXT);
                            string service = string.Format("SERVICE_REQ_CODE: {0}. {1}", r.SERVICE_REQ_CODE, icd);
                            eventLogs.Add(service);
                        }

                        sql += string.Format("WHERE ID = {0}", r.ID);

                        sqls.Add(sql);
                    }

                    if (eventLogService == null)
                    {
                        eventLogService = new List<string>();
                    }

                    eventLogService.Add(r.SERVICE_REQ_CODE);
                }
            }

            if (IsNotNullOrEmpty(data.UsedForServiceReqIds))
            {
                List<HIS_SERVICE_REQ> usedForhisServiceReqs = new HisServiceReqGet().GetByIds(data.UsedForServiceReqIds);

                if (!IsNotNullOrEmpty(usedForhisServiceReqs))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("service_req_id ko ton tai." + LogUtil.TraceData("ServiceReqIds", data.UsedForServiceReqIds));
                }

                List<HIS_SERVICE_REQ> invalids = usedForhisServiceReqs.Where(o => o.TREATMENT_ID != data.Tracking.TREATMENT_ID).ToList();

                if (IsNotNullOrEmpty(invalids))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai service_req khong thuoc treatment." + LogUtil.TraceData("invalids", invalids));
                }

                string sql = "UPDATE HIS_SERVICE_REQ SET USED_FOR_TRACKING_ID = {0} WHERE %IN_CLAUSE%";
                sql = DAOWorker.SqlDAO.AddInClause(usedForhisServiceReqs.Select(o => o.ID).ToList(), sql, "ID");
                sql = string.Format(sql, this.recentHisTracking.ID);
                sqls.Add(sql);
            }
            if (IsNotNullOrEmpty(sqls))
            {
                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Update thong tin y lenh that bai. Rollback du lieu");
                }
            }
        }

        private void ProcessTracking(HisTrackingSDO data)
        {
            if (data != null)
            {
                HIS_TRACKING tracking = data.Tracking;

                if (data.Dhst != null)
                {
                    tracking.HIS_DHST = new List<HIS_DHST>() { data.Dhst };
                }

                if (!this.Create(tracking))
                {
                    throw new Exception("Ket thuc nghiep vu." + LogUtil.TraceData("tracking", tracking));
                }
            }
        }

        //Cap nhat thong tin treatment neu co yeu cau
        private void ProcessTreatment(HisTrackingSDO data, HIS_TREATMENT treatment)
        {
            if (HisTrackingCFG.UpdateTreatmentIcd)
            {
                //clone phuc vu rollback
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);

                //Cap nhat thong tin benh chinh cho treatment
                if (!string.IsNullOrWhiteSpace(data.Tracking.ICD_CODE)
                    || !string.IsNullOrWhiteSpace(data.Tracking.ICD_NAME)
                    || !string.IsNullOrWhiteSpace(data.Tracking.ICD_SUB_CODE)
                    || !string.IsNullOrWhiteSpace(data.Tracking.ICD_TEXT))
                {
                    treatment.ICD_CODE = data.Tracking.ICD_CODE;
                    treatment.ICD_NAME = data.Tracking.ICD_NAME;
                    HisTreatmentUpdate.AddIcd(treatment, data.Tracking.ICD_SUB_CODE, data.Tracking.ICD_TEXT);

                    treatment.TRADITIONAL_ICD_CODE = data.Tracking.TRADITIONAL_ICD_CODE;
                    treatment.TRADITIONAL_ICD_NAME = data.Tracking.TRADITIONAL_ICD_NAME;
                    treatment.TRADITIONAL_ICD_TEXT = data.Tracking.TRADITIONAL_ICD_TEXT;
                    treatment.TRADITIONAL_ICD_SUB_CODE = data.Tracking.TRADITIONAL_ICD_SUB_CODE;
                }

                if (!this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    LogSystem.Warn("Cap nhat thong tin ICD cho treatment dua vao thong tin ICD cua to dieu tri that bai.");
                }
            }
        }

        private void ProcessCare(List<long> careIds)
        {
            List<HIS_CARE> cares = new HisCareGet().GetByIds(careIds);
            if (IsNotNullOrEmpty(cares) && this.recentHisTracking != null)
            {
                cares.ForEach(o => o.TRACKING_ID = this.recentHisTracking.ID);
                if (!this.hisCareUpdate.UpdateList(cares))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessDebate(List<long> debateIds)
        {
            List<HIS_DEBATE> debates = IsNotNullOrEmpty(debateIds) ? new HisDebateGet().GetByIds(debateIds) : null;
            if (IsNotNullOrEmpty(debates) && this.recentHisTracking != null)
            {
                Mapper.CreateMap<HIS_DEBATE, HIS_DEBATE>();
                List<HIS_DEBATE> befores = Mapper.Map<List<HIS_DEBATE>>(debates);
                debates.ForEach(o => o.TRACKING_ID = this.recentHisTracking.ID);
                if (!this.hisDebateUpdate.UpdateList(debates, befores))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
        }

        private bool Create(HIS_TRACKING data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTrackingCheck checker = new HisTrackingCheck(param);
                valid = valid && checker.ValiTrackingTime(data);//tiennv
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.IsAllowCreateOrEdit(data);
                if (valid)
                {

                    if (!DAOWorker.HisTrackingDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTracking_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisTracking that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisTracking = data;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void RollbackData()
        {
            this.hisCareUpdate.RollbackData();
            this.hisDebateUpdate.RollbackData();
            if (this.recentHisTracking != null)
            {
                if (!new HisTrackingTruncate(param).Truncate(this.recentHisTracking.ID))
                {
                    LogSystem.Warn("Rollback du lieu HisTracking that bai, can kiem tra lai." + LogUtil.TraceData("recentHisTracking", this.recentHisTracking));
                }
            }
        }
    }
}
