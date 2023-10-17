using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBed;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.Bed.CreateTempByBedLog;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisBedLog.Update
{
    partial class HisBedLogUpdate : BusinessBase
    {
        private UsingBedTempProcessor usingBedTempProcessor;
        private HisServiceReqCreateTempByBedLogProcessor hisServiceReqCreateTempByBedLogProcessor;

        private List<HIS_BED_LOG> beforeUpdateHisBedLogs = new List<HIS_BED_LOG>();

        internal HisBedLogUpdate()
            : base()
        {
            this.Init();
        }

        internal HisBedLogUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.usingBedTempProcessor = new UsingBedTempProcessor(param);
            this.hisServiceReqCreateTempByBedLogProcessor = new HisServiceReqCreateTempByBedLogProcessor(param);
        }

        internal bool Update(HisBedLogSDO data, ref HIS_BED_LOG resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisBedLogCheck checker = new HisBedLogCheck(param);
                HisBedLogUpdateCheck updateChecker = new HisBedLogUpdateCheck(param);
                HisTreatmentBedRoomCheck treatmentBedRoomChecker = new HisTreatmentBedRoomCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                WorkPlaceSDO workPlace = null;
                HIS_BED_LOG raw = null;
                HIS_TREATMENT_BED_ROOM treatmentBedRoom = null;
                HIS_TREATMENT treatment = null;
                List<HIS_SERVICE_REQ> serviceReqs = null;
                List<HIS_SERE_SERV> sereServs = null;

                valid = valid && data.Id.HasValue;
                valid = valid && checker.VerifyId(data.Id.Value, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && treatmentBedRoomChecker.VerifyId(data.TreatmentBedRoomId, ref treatmentBedRoom);
                valid = valid && treatmentChecker.VerifyId(treatmentBedRoom.TREATMENT_ID, ref treatment);
                valid = valid && updateChecker.IsValidSSBill(raw.ID, ref serviceReqs);
                valid = valid && updateChecker.IsValidIfHasNoEndTime(data, serviceReqs, raw);

                if (valid)
                {
                    Mapper.CreateMap<HIS_BED_LOG, HIS_BED_LOG>();
                    HIS_BED_LOG toUpdate = Mapper.Map<HIS_BED_LOG>(raw);
                    toUpdate.BED_ID = data.BedId;
                    toUpdate.BED_SERVICE_TYPE_ID = data.BedServiceTypeId;
                    toUpdate.FINISH_TIME = data.FinishTime;
                    toUpdate.PATIENT_TYPE_ID = data.PatientTypeId;
                    toUpdate.PRIMARY_PATIENT_TYPE_ID = data.PrimaryPatientTypeId;
                    toUpdate.SHARE_COUNT = data.ShareCount;
                    toUpdate.START_TIME = data.StartTime;
                    toUpdate.TREATMENT_BED_ROOM_ID = data.TreatmentBedRoomId;


                    valid = valid && checker.IsValidTime(toUpdate, treatmentBedRoom, treatment);
                    valid = valid && checker.VerifyRequireField(toUpdate);

                    if (valid)
                    {
                        if (!DAOWorker.HisBedLogDAO.Update(toUpdate))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_CapNhatThatBai);
                            throw new Exception("Cap nhat thong tin HisBedLog that bai." + LogUtil.TraceData("data", data));
                        }

                        this.beforeUpdateHisBedLogs.Add(raw);

                        this.ProcessTreatmentBedRoom(toUpdate, treatmentBedRoom);

                        if (!this.usingBedTempProcessor.Run(data, toUpdate, raw, treatment, workPlace, serviceReqs, ref sereServs))
                        {
                            throw new Exception("Xu ly cap nhat giuong tam that bai");
                        }

                        //this.ProcessServiceReq(toUpdate, raw, treatment, data.WorkingRoomId);

                        resultData = toUpdate;

                        result = true;

                        this.ProcessEventLog(treatment, raw, toUpdate);

                    }
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

        private void ProcessEventLog(HIS_TREATMENT treatment, HIS_BED_LOG raw, HIS_BED_LOG toUpdate)
        {
            var oldBed = HisBedCFG.DATA.FirstOrDefault(o => o.ID == raw.BED_ID);
            var newBed = HisBedCFG.DATA.FirstOrDefault(o => o.ID == toUpdate.BED_ID);
            var oldpatientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == raw.PATIENT_TYPE_ID);
            var newpatientType = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == toUpdate.PATIENT_TYPE_ID);

            new EventLogGenerator(EventLog.Enum.HisBedLog_CapNhatThongTinLichSuGiuong,
                oldBed.BED_CODE, oldBed.BED_NAME, oldpatientType != null ? oldpatientType.PATIENT_TYPE_NAME : "", raw.START_TIME, raw.FINISH_TIME,
                newBed.BED_CODE, newBed.BED_NAME, newpatientType != null ? newpatientType.PATIENT_TYPE_NAME : "", toUpdate.START_TIME, toUpdate.FINISH_TIME
            ).TreatmentCode(treatment.TREATMENT_CODE).Run();
        }

        private void ProcessTreatmentBedRoom(HIS_BED_LOG data, HIS_TREATMENT_BED_ROOM treatmentBedRoom)
        {
            if (treatmentBedRoom != null)
            {
                HisBedLogFilterQuery filter = new HisBedLogFilterQuery();
                filter.TREATMENT_BED_ROOM_ID = data.TREATMENT_BED_ROOM_ID;
                filter.ORDER_FIELD = "START_TIME";
                filter.ORDER_DIRECTION = "DESC";
                filter.ORDER_FIELD1 = "CREATE_TIME";
                filter.ORDER_DIRECTION = "DESC";
                List<HIS_BED_LOG> bedLogs = new HisBedLogGet().Get(filter);
                if (bedLogs != null && bedLogs.Count > 0)
                {
                    if (treatmentBedRoom.BED_ID != bedLogs[0].BED_ID)
                    {
                        treatmentBedRoom.BED_ID = bedLogs[0].BED_ID;
                        if (!new HisTreatmentBedRoomUpdate().Update(treatmentBedRoom))
                        {
                            throw new Exception("Rollback du lieu");
                        }
                    }
                }
            }
        }

        private bool ProcessServiceReq(HIS_BED_LOG bedLog, HIS_BED_LOG beforeUpdate, HIS_TREATMENT treatment, long requestRoomId)
        {
            //Neu thuc hien bo sung thoi gian ket thuc cho thong tin giuong thi thuc hien xoa thong tin
            //chi phi giuong tam tinh
            if (bedLog != null && beforeUpdate != null && bedLog.FINISH_TIME.HasValue && !beforeUpdate.FINISH_TIME.HasValue)
            {
                return this.DeleteSereServTemp(bedLog, treatment);
            }
            //Neu cap nhat bo thoi gian ket thuc thi tu dong tao "chi phi tam tinh"
            else if (bedLog != null && beforeUpdate != null && !bedLog.FINISH_TIME.HasValue && beforeUpdate.FINISH_TIME.HasValue)
            {
                return this.InsertSereServTemp(bedLog, treatment, requestRoomId);
            }
            //Neu truoc khi cap nhat va sau khi cap nhat deu ko co thong tin "thoi gian ket thuc" 
            //--> thuc hien xoa thong tin "tam tinh" cu de tao moi (tranh truong hop nguoi dung doi sang dich vu hay doi tuong thanh toan khac)
            else if (bedLog != null && beforeUpdate != null && !bedLog.FINISH_TIME.HasValue && !beforeUpdate.FINISH_TIME.HasValue)
            {
                return this.DeleteSereServTemp(bedLog, treatment)
                    && this.InsertSereServTemp(bedLog, treatment, requestRoomId);
            }

            return true;
        }

        private bool DeleteSereServTemp(HIS_BED_LOG bedLog, HIS_TREATMENT treatment)
        {
            string ssSql = "SELECT * FROM HIS_SERE_SERV S WHERE S.AMOUNT_TEMP IS NOT NULL AND S.IS_DELETE = 0 AND S.SERVICE_REQ_ID IS NOT NULL AND S.TDL_TREATMENT_ID = :param1 AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_EXT EXT WHERE EXT.SERE_SERV_ID = S.ID AND EXT.BED_LOG_ID = :param2)";
            HIS_SERE_SERV ss = DAOWorker.SqlDAO.GetSqlSingle<HIS_SERE_SERV>(ssSql, treatment.ID, bedLog.ID);

            if (ss != null)
            {
                string sqlExt = string.Format("UPDATE HIS_SERE_SERV_EXT EXT SET EXT.IS_DELETE = 1, EXT.TDL_SERVICE_REQ_ID = NULL, EXT.TDL_TREATMENT_ID = NULL, BED_LOG_ID = NULL WHERE SERE_SERV_ID = {0}", ss.ID);
                string sqlSs = string.Format("UPDATE HIS_SERE_SERV SS SET SS.IS_DELETE = 1, SS.SERVICE_REQ_ID = NULL, SS.TDL_TREATMENT_ID = NULL WHERE ID = {0}", ss.ID);
                string sqlSr = string.Format("UPDATE HIS_SERVICE_REQ SR SET SR.IS_DELETE = 1 WHERE ID = {0}", ss.SERVICE_REQ_ID);
                List<string> sqls = new List<string>() { sqlExt, sqlSs, sqlSr };

                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    LogSystem.Warn("Xoa HIS_SERE_SERV_EXT, HIS_SERE_SERV, HIS_SERVICE_REQ that bai");
                    return false;
                }
            }
            return true;
        }

        private bool DeleteSereServTemp(List<HIS_BED_LOG> bedLogs, HIS_TREATMENT treatment)
        {
            string ssSql = "SELECT * FROM HIS_SERE_SERV S WHERE S.AMOUNT_TEMP IS NOT NULL AND S.IS_DELETE = 0 AND S.SERVICE_REQ_ID IS NOT NULL AND S.TDL_TREATMENT_ID = :param1 AND EXISTS (SELECT 1 FROM HIS_SERE_SERV_EXT EXT WHERE EXT.SERE_SERV_ID = S.ID AND  %IN_CLAUSE%  )";

            ssSql = DAOWorker.SqlDAO.AddInClause(bedLogs.Select(s => s.ID).ToList(), ssSql, "EXT.BED_LOG_ID");

            List<HIS_SERE_SERV> ss = DAOWorker.SqlDAO.GetSql<HIS_SERE_SERV>(ssSql, treatment.ID);

            if (IsNotNullOrEmpty(ss))
            {
                List<long> sereServIds = ss.Select(o => o.ID).ToList();
                List<long> serviceReqIds = ss.Select(o => o.SERVICE_REQ_ID.Value).ToList();


                string sqlExt = DAOWorker.SqlDAO.AddInClause(sereServIds, "UPDATE HIS_SERE_SERV_EXT EXT SET EXT.IS_DELETE = 1, EXT.TDL_SERVICE_REQ_ID = NULL, EXT.TDL_TREATMENT_ID = NULL, BED_LOG_ID = NULL WHERE %IN_CLAUSE% ", "SERE_SERV_ID");
                string sqlSs = DAOWorker.SqlDAO.AddInClause(sereServIds, "UPDATE HIS_SERE_SERV SS SET SS.IS_DELETE = 1, SS.SERVICE_REQ_ID = NULL, SS.TDL_TREATMENT_ID = NULL WHERE %IN_CLAUSE%", "SERE_SERV_ID");
                string sqlSr = DAOWorker.SqlDAO.AddInClause(serviceReqIds, "UPDATE HIS_SERVICE_REQ SR SET SR.IS_DELETE = 1 WHERE %IN_CLAUSE% ", "ID");
                List<string> sqls = new List<string>() { sqlExt, sqlSs, sqlSr };

                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    LogSystem.Warn("Xoa HIS_SERE_SERV_EXT, HIS_SERE_SERV, HIS_SERVICE_REQ that bai");
                    return false;
                }
            }
            return true;
        }

        private bool InsertSereServTemp(HIS_BED_LOG bedLog, HIS_TREATMENT treatment, long requestRoomId)
        {
            if (HisSereServCFG.IS_USING_BED_TEMP && !bedLog.FINISH_TIME.HasValue && bedLog.PATIENT_TYPE_ID.HasValue && bedLog.BED_SERVICE_TYPE_ID.HasValue)
            {
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);

                this.hisServiceReqCreateTempByBedLogProcessor.Run(bedLog, treatment, requestRoomId, ptas, existSereServs);
            }
            return true;
        }

        internal bool Finish(List<HIS_BED_LOG> listData, HIS_TREATMENT treatment, long finishTime)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(listData))
                {
                    Mapper.CreateMap<HIS_BED_LOG, HIS_BED_LOG>();

                    List<HIS_BED_LOG> beforeUpdates = Mapper.Map<List<HIS_BED_LOG>>(listData);

                    listData.ForEach(o => o.FINISH_TIME = finishTime);
                    if (!DAOWorker.HisBedLogDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisBedLog_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisBedLog that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.beforeUpdateHisBedLogs.AddRange(beforeUpdates);

                    this.DeleteSereServTemp(listData, treatment);
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
            this.usingBedTempProcessor.RollbackData();
            this.hisServiceReqCreateTempByBedLogProcessor.Rollback();
            if (IsNotNullOrEmpty(this.beforeUpdateHisBedLogs))
            {
                if (!DAOWorker.HisBedLogDAO.UpdateList(this.beforeUpdateHisBedLogs))
                {
                    LogSystem.Warn("Rollback du lieu HisBedLog that bai, can kiem tra lai." + LogUtil.TraceData("HisBedLogs", this.beforeUpdateHisBedLogs));
                }
                this.beforeUpdateHisBedLogs = null; //tranh goi rollback 2 lan
            }
        }
    }
}
