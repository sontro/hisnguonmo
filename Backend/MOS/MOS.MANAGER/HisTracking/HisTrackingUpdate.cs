using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.Config.CFG;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisDebate;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisTracking
{
    partial class HisTrackingUpdate : BusinessBase
    {
        private List<HIS_TRACKING> beforeUpdateHisTrackings = new List<HIS_TRACKING>();
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisDhstUpdate hisDhstUpdate;
        private HisDhstCreate hisDhstCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisDebateUpdate hisDebateUpdate;

        private static string FORMAT_EDIT = "{0}({1}=>{2})";

        internal HisTrackingUpdate()
            : base()
        {
            this.Init();
        }

        internal HisTrackingUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisDhstCreate = new HisDhstCreate(param);
            this.hisDhstUpdate = new HisDhstUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisDebateUpdate = new HisDebateUpdate(param);
        }

        private bool Update(HIS_TRACKING data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTrackingCheck checker = new HisTrackingCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.ValiTrackingTime(data);//tiennv
                HIS_TRACKING raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsAllowCreateOrEdit(raw);
                valid = valid && checker.IsAllowEdit(raw);
                if (valid)
                {
                    this.beforeUpdateHisTrackings.Add(raw);

                    //Ko cho update thong tin phong/khoa tao
                    data.DEPARTMENT_ID = raw.DEPARTMENT_ID;
                    data.ROOM_ID = raw.ROOM_ID;
                    data.EMR_DOCUMENT_STT_ID = raw.EMR_DOCUMENT_STT_ID;

                    if (!DAOWorker.HisTrackingDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTracking_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTracking that bai." + LogUtil.TraceData("data", data));
                    }
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

        public bool Update(HisTrackingSDO data, ref HIS_TRACKING resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO sdo = null;
                HIS_TREATMENT treatment = null;
                HIS_TRACKING raw = null;
                bool valid = true;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisTrackingCheck checker = new HisTrackingCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.Tracking);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref sdo);
                valid = valid && treatmentChecker.VerifyId(data.Tracking.TREATMENT_ID, ref treatment);
                valid = valid && checker.VerifyId(data.Tracking.ID, ref raw);

                if (valid && this.Update(data.Tracking))
                {
                    List<string> eventLogs = null;
                    List<string> eventLogService = null;
                    this.ProcessDhst(data);
                    this.ProcessServiceReq(data, ref eventLogs, ref eventLogService);
                    //this.ProcessDebate(data);
                    this.ProcessTreatment(data, treatment);
                    HIS_TREATMENT treat = new HisTreatmentGet().GetById(raw.TREATMENT_ID);
                    string eventLog = "";
                    ProcessEventLog(raw, data, eventLogService, ref eventLog);
                    new EventLogGenerator(EventLog.Enum.HisTracking_SuaToDieuTri, eventLog)
                        .TreatmentCode(treat.TREATMENT_CODE)
                        .Run();

                    if (HisTrackingCFG.SERVICE_REQ_ICD_OPTION && IsNotNullOrEmpty(eventLogs))
                    {
                        string ttStr = string.Join(";", eventLogs);

                        new EventLogGenerator(EventLog.Enum.HisTracking_SuaThongTinChungYLenh, ttStr)
                            .TreatmentCode(treat.TREATMENT_CODE)
                            .Run();
                    }

                    resultData = data.Tracking;
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

        private void ProcessEventLog(HIS_TRACKING raw, HisTrackingSDO data, List<string> eventLogService, ref string eventLog)
        {
            try
            {
                List<string> editFields = new List<string>();

                editFields.Add(String.Format("ICD: ({0}){1} => ({2}){3}", raw.ICD_CODE, raw.ICD_NAME, data.Tracking.ICD_CODE, data.Tracking.ICD_NAME));

                if (IsDiffLong(raw.TRACKING_TIME, data.Tracking.TRACKING_TIME))
                {
                    string newValue = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(data.Tracking.TRACKING_TIME);
                    string oldValue = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(raw.TRACKING_TIME);
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.ThoiGian);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, oldValue, newValue));
                }

                if (IsDiffString(raw.CONTENT, data.Tracking.CONTENT))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DienBien);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, raw.CONTENT, data.Tracking.CONTENT));
                }
                if (IsDiffString(raw.SUBCLINICAL_PROCESSES, data.Tracking.SUBCLINICAL_PROCESSES))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.DienBienCls);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, raw.SUBCLINICAL_PROCESSES, data.Tracking.SUBCLINICAL_PROCESSES));
                }
                if (IsDiffString(raw.MEDICAL_INSTRUCTION, data.Tracking.MEDICAL_INSTRUCTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.PhuongPhapXuLy);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, raw.MEDICAL_INSTRUCTION, data.Tracking.MEDICAL_INSTRUCTION));
                }
                if (IsDiffString(raw.CARE_INSTRUCTION, data.Tracking.CARE_INSTRUCTION))
                {
                    string fieldName = LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TheoDoiChamSoc);
                    editFields.Add(String.Format(FORMAT_EDIT, fieldName, raw.CARE_INSTRUCTION, data.Tracking.CARE_INSTRUCTION));
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

        private static bool IsDiffLong(long? oldValue, long? newValue)
        {
            return (oldValue ?? -1) != (newValue ?? -1);
        }

        private static bool IsDiffString(string oldValue, string newValue)
        {
            return (oldValue ?? "") != (newValue ?? "");
        }

        private void ProcessDebate(HisTrackingSDO data)
        {
            List<HIS_DEBATE> olds = new HisDebateGet().GetByTrackingId(data.Tracking.ID);
            List<long> newIds = null;

            List<HIS_DEBATE> toRemoves = null;

            if (olds == null)
            {
                newIds = data.DebateIds;
            }
            else
            {
                newIds = data.DebateIds != null ? data.DebateIds.Where(t => !olds.Exists(o => o.ID == t)).ToList() : null;
                toRemoves = olds.Where(o => data.DebateIds == null || !data.DebateIds.Contains(o.ID)).ToList();
            }

            List<HIS_DEBATE> news = IsNotNullOrEmpty(newIds) ? new HisDebateGet().GetByIds(newIds) : null;

            List<HIS_DEBATE> toUpdates = new List<HIS_DEBATE>();
            List<HIS_DEBATE> befores = new List<HIS_DEBATE>();

            Mapper.CreateMap<HIS_DEBATE, HIS_DEBATE>();

            if (IsNotNullOrEmpty(news))
            {
                List<HIS_DEBATE> bfs = Mapper.Map<List<HIS_DEBATE>>(news);
                news.ForEach(o => o.TRACKING_ID = data.Tracking.ID);

                toUpdates.AddRange(news);
                befores.AddRange(bfs);
            }

            if (IsNotNullOrEmpty(toRemoves))
            {
                List<HIS_DEBATE> bfs = Mapper.Map<List<HIS_DEBATE>>(news);
                toRemoves.ForEach(o => o.TRACKING_ID = null);

                toUpdates.AddRange(toRemoves);
                befores.AddRange(bfs);
            }

            if (IsNotNullOrEmpty(toUpdates))
            {
                if (!this.hisDebateUpdate.UpdateList(toUpdates, befores))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessDhst(HisTrackingSDO data)
        {
            if (data.Dhst != null && data.Dhst.ID > 0)
            {
                if (!this.hisDhstUpdate.Update(data.Dhst))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
            else if (data.Dhst != null && data.Dhst.ID == 0)
            {
                data.Dhst.TRACKING_ID = data.Tracking.ID;
                if (!this.hisDhstCreate.Create(data.Dhst))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessServiceReq(HisTrackingSDO data, ref List<string> eventLogs, ref List<string> eventLogService)
        {
            List<HIS_SERVICE_REQ> olds = new HisServiceReqGet().GetByTrackingId(data.Tracking.ID);
            List<HIS_SERVICE_REQ> usedForOlds = new HisServiceReqGet().GetByUsedForTrackingId(data.Tracking.ID);

            List<HIS_SERVICE_REQ> removeTrackings = new List<HIS_SERVICE_REQ>();
            List<HIS_SERVICE_REQ> removeUsedForTrackings = new List<HIS_SERVICE_REQ>();

            List<HIS_SERVICE_REQ> news = new List<HIS_SERVICE_REQ>();
            List<HIS_SERVICE_REQ> usedForNews = new List<HIS_SERVICE_REQ>();

            List<string> sqls = new List<string>();

            if (IsNotNullOrEmpty(data.ServiceReqs))
            {
                List<long> serviceReqIds = data.ServiceReqs.Select(o => o.ServiceReqId).ToList();

                news = new HisServiceReqGet().GetByIds(serviceReqIds);
                if (!IsNotNullOrEmpty(news))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Danh sach ServiceReqIds ko hop le." + LogUtil.TraceData("data.ServiceReqIds", serviceReqIds));
                }

                foreach (HIS_SERVICE_REQ r in news)
                {
                    TrackingServiceReq t = data.ServiceReqs.Where(o => o.ServiceReqId == r.ID).FirstOrDefault();
                    if (t != null)
                    {
                        string sql = string.Format("UPDATE HIS_SERVICE_REQ SET TRACKING_ID = {0} ", data.Tracking.ID);
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
                usedForNews = new HisServiceReqGet().GetByIds(data.UsedForServiceReqIds);
                if (!IsNotNullOrEmpty(usedForNews))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Danh sach ServiceReqIds ko hop le." + LogUtil.TraceData("data.usedForServiceReqIds", data.UsedForServiceReqIds));
                }

                string sql = "UPDATE HIS_SERVICE_REQ SET USED_FOR_TRACKING_ID = {0} WHERE %IN_CLAUSE%";
                sql = DAOWorker.SqlDAO.AddInClause(usedForNews.Select(o => o.ID).ToList(), sql, "ID");
                sql = string.Format(sql, data.Tracking.ID);
                sqls.Add(sql);
            }

            //Danh sach can remove tracking_id la danh sach co trong olds nhung ko co trong news
            removeTrackings = olds.Where(o => news == null || !news.Where(t => t.ID == o.ID).Any()).ToList();
            if (IsNotNullOrEmpty(removeTrackings))
            {
                string sql = "UPDATE HIS_SERVICE_REQ SET TRACKING_ID = NULL, IS_NOT_SHOW_MATERIAL_TRACKING = NULL , IS_NOT_SHOW_MEDICINE_TRACKING = NULL WHERE %IN_CLAUSE%";
                sql = DAOWorker.SqlDAO.AddInClause(removeTrackings.Select(o => o.ID).ToList(), sql, "ID");
                sqls.Add(sql);
            }

            //Danh sach can remove tracking_id la danh sach co trong olds nhung ko co trong usedFornews
            removeUsedForTrackings = usedForOlds.Where(o => usedForNews == null || !usedForNews.Where(t => t.ID == o.ID).Any()).ToList();
            if (IsNotNullOrEmpty(removeUsedForTrackings))
            {
                string sql = "UPDATE HIS_SERVICE_REQ SET USED_FOR_TRACKING_ID = NULL WHERE %IN_CLAUSE%";
                sql = DAOWorker.SqlDAO.AddInClause(removeUsedForTrackings.Select(o => o.ID).ToList(), sql, "ID");
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

        internal bool UpdateList(List<HIS_TRACKING> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisTrackingCheck checker = new HisTrackingCheck(param);
                List<HIS_TRACKING> listRaw = new List<HIS_TRACKING>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisTrackings = listRaw;
                    if (!DAOWorker.HisTrackingDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTracking_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTracking that bai." + LogUtil.TraceData("listData", listData));
                    }
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
            this.hisDebateUpdate.RollbackData();
            if (IsNotNullOrEmpty(this.beforeUpdateHisTrackings))
            {
                if (!new HisTrackingUpdate(param).UpdateList(this.beforeUpdateHisTrackings))
                {
                    LogSystem.Warn("Rollback du lieu HisTracking that bai, can kiem tra lai." + LogUtil.TraceData("beforeUpdateHisTrackings", this.beforeUpdateHisTrackings));
                }
            }
        }
    }
}
