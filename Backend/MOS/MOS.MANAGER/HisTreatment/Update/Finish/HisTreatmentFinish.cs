using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisDepartmentTran;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisTreatmentBedRoom;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSereServ;
using AutoMapper;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.UTILITY;
using MOS.MANAGER.HisDhst;
using MOS.ApiConsumerManager;
using System.Threading;
using YDT.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMest.AggrExam.Create;
using MOS.MANAGER.HisTreatment.Util;
using MOS.MANAGER.HisBaby;
using MOS.MANAGER.OldSystemIntegrate;
using Inventec.Token.ResourceSystem;
using System.Web;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisMediRecord;
using MOS.MANAGER.HisPatientProgram;
using MOS.MANAGER.HisTreatment.MediRecord;
using MOS.MANAGER.HisTreatment.Lock;
using MOS.MANAGER.HisWorkPlace;
using MOS.MANAGER.HisCareer;
using MOS.MANAGER.CodeGenerator.HisTreatment;
using MOS.LibraryHein.Bhyt.HeinHasBirthCertificate;
using MOS.MANAGER.HisAppointmentServ;
using MOS.MANAGER.HisBedLog;
using MOS.MANAGER.HisNumOrderIssue;
using MOS.MANAGER.HisCarerCardBorrow;
using MOS.MANAGER.HisDepartment;
using MOS.MANAGER.HisTransReq;
using MOS.MANAGER.HisDepositReq;
using MOS.MANAGER.HisTransReq.CreateByDepositReq;

namespace MOS.MANAGER.HisTreatment.Update.Finish
{
    class HisTreatmentFinish : BusinessBase
    {
        class ThreadData
        {
            public long TreatmentId { get; set; }
            public long FeeLockTime { get; set; }
            public long RequestRoomId { get; set; }
            public long CashierRoomId { get; set; }
        }

        class ThreadExportXmlData
        {
            public long TreatmentId { get; set; }
            public string TreatmentCode { get; set; }
            public string PatientCode { get; set; }
            public HIS_BRANCH Branch { get; set; }
            public HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        }

        //private HIS_TREATMENT recentHisTreatment;

        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisTreatmentBedRoomRemove hisTreatmentBedRoomRemove;
        private HisPatientUpdate hisPatientUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisBabyCreate hisBabyCreate;
        private HisBabyTruncate hisBabyTruncate;
        private HisMediRecordCreate hisMediRecordCreate;
        private HisPatientProgramCreate hisPatientProgramCreate;
        private NewTreatmentMaker newTreatmentMaker;
        private RegisterTreatmentProcessor registerTreatmentProcessor;
        private HisSereServUpdate ssUpdateProcessor;
        private bool IsNotProcessHisBaby = false;
        private HisPatientTypeAlterCreate ptaProcessor;
        private HisTransReqCreate transReqCreate;

        internal HisTreatmentFinish()
            : base()
        {
            this.Init();
        }

        internal HisTreatmentFinish(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisTreatmentBedRoomRemove = new HisTreatmentBedRoomRemove(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.hisBabyCreate = new HisBabyCreate(param);
            this.hisBabyTruncate = new HisBabyTruncate(param);
            this.hisMediRecordCreate = new HisMediRecordCreate(param);
            this.hisPatientProgramCreate = new HisPatientProgramCreate(param);
            this.newTreatmentMaker = new NewTreatmentMaker(param);
            this.registerTreatmentProcessor = new RegisterTreatmentProcessor(param);
            this.ssUpdateProcessor = new HisSereServUpdate(param);
            this.ptaProcessor = new HisPatientTypeAlterCreate(param);
            this.transReqCreate = new HisTransReqCreate(param);
        }

        internal bool Finish(HisTreatmentFinishSDO data, ref HIS_TREATMENT resultData)
        {
            bool result = false;
            try
            {
                HIS_TREATMENT treatment = new HisTreatmentGet().GetById(data.TreatmentId);
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT oldTreatment = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback

                List<HIS_SERE_SERV> existsSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                List<HIS_PATIENT_TYPE_ALTER> ptas = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
                WorkPlaceSDO workPlace = null;
                HIS_DEPARTMENT_TRAN lastDt = null;
                data.TreatmentId = treatment.ID;
                HIS_PROGRAM program = null;
                HIS_MEDI_RECORD mediRecord = null;
                V_HIS_DEATH_CERT_BOOK deathCertBook = null;

                HisTreatmentFinishCheck checker = new HisTreatmentFinishCheck(param);

                //Ket thuc o chuc nang ho so dieu tri thi khong xuly hisbaby neu hisbaby rong
                this.IsNotProcessHisBaby = true;

                if (checker.IsValidForFinish(data, treatment, existsSereServs, ptas, ref lastDt, ref workPlace, ref program, ref deathCertBook))
                {
                    if (!this.FinishWithoutValidate(data, treatment, oldTreatment, existsSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref resultData, ref mediRecord))
                    {
                        this.RollBackData();
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal bool FinishWithoutValidate(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_TREATMENT oldTreatment, List<HIS_SERE_SERV> existsSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace, HIS_PROGRAM program, HIS_DEPARTMENT_TRAN lastDt, V_HIS_DEATH_CERT_BOOK deathCertBook, ref HIS_TREATMENT resultData, ref HIS_MEDI_RECORD mediRecord)
        {
            bool result = true;
            try
            {
                List<string> sqls = new List<string>();
                List<HIS_SERE_SERV> newBedSereServs = null;
                HIS_CAREER career = null;
                HIS_PATIENT hisPatient = null;

                //Tu dong chi dinh giuong
                new AutoBedAssignProcessor(param).Run(data, treatment, existsSereServs, ptas, ref newBedSereServs);

                //Xu ly giuong chi dinh tam tinh
                new TemporaryBedProcessor(param).Run(data, treatment, existsSereServs, workPlace);

                //Neu co tao ra dich vu giuong moi thi add lai vao danh sach
                if (IsNotNullOrEmpty(newBedSereServs))
                {
                    if (existsSereServs != null)
                    {
                        existsSereServs.AddRange(newBedSereServs);
                    }
                    else
                    {
                        existsSereServs = newBedSereServs;
                    }
                }

                if (data.CareerId.HasValue)
                {
                    career = new HisCareerGet().GetById(data.CareerId.Value);
                }

                this.ProcessNewPtaForOutPatient(program, ptas, data.TreatmentFinishTime, workPlace);
                this.ProcessRecalcSereServ(data, treatment, ptas, existsSereServs, lastDt);
                this.ProcessAutoFinishServiceReq(data, workPlace, existsSereServs, career);
                this.ProcessMediRecord(data, treatment, program, ref mediRecord);
                this.ProcessPatientProgram(data, treatment);
                this.ProcessTreatment(data, ptas, workPlace, treatment, deathCertBook, oldTreatment, existsSereServs, career);
                this.ProcessTreatmentBedRoom(data);
                this.ProcessPatient(data, treatment, career, ref hisPatient);
                this.ProcessPatientTypeAlter(data, ptas, existsSereServs, ref sqls);
                this.ProcessAppointmentServ(data, ref sqls);
                this.AutoCreateAggrExam(treatment, data.EndRoomId);
                this.MakeNewTreatment(data, treatment, lastDt, ptas);
                this.RegisterTreatmentByAppointment(data, treatment, hisPatient, ptas, existsSereServs, workPlace, ref sqls);
                this.ProcessCarerCardBorrow(data, treatment);

                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls)) //dat cuoi cung do thuc hien execute sql, tranh phai rollback
                {
                    throw new Exception("Update patient, appointment_serv that bai");
                }

                this.PassResult(treatment, ref resultData);
                this.WriteEventLog(data, resultData);
                if (resultData != null)
                {
                    //Tu dong duyet khoa vien phi
                    this.AutoLockTreatment(treatment);

                    //Gui thong tin sang he thong PM cu
                    this.IntegrateThreadInit(treatment);

                    new Util.HisTreatmentUploadEmr().Run(resultData.ID);
                    this.InitThreadSyncHRM(treatment);
                    this.InitThreadSCreateMS(treatment);
                    this.InitThreadNotify(treatment);
                    this.InitThreadExportXml(treatment, data, ptas);
                }
                if (!data.IsTemporary && HisTransReqCFG.AUTO_CREATE_OPTION)
                {
                    decimal sqlTotalAmount = DAOWorker.SqlDAO.GetSqlSingle<decimal>("SELECT NVL(TOTAL_PATIENT_PRICE,0) - NVL(TOTAL_DEPOSIT_AMOUNT,0) - NVL(TOTAL_DEBT_AMOUNT,0) - NVL(TOTAL_BILL_AMOUNT,0) + NVL(TOTAL_BILL_TRANSFER_AMOUNT,0) + NVL(TOTAL_REPAY_AMOUNT,0) AS PATIENT_PRICE FROM V_HIS_TREATMENT_FEE WHERE ID = :param1", treatment.ID);
                    Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("sqlTotalAmount", sqlTotalAmount));
                    if (sqlTotalAmount > 0)
                    {
                        HIS_TRANS_REQ transReq = null;
                        if (new HisTransReqCreateByDepositReqProccesor(param).Run(data.TreatmentId, null, workPlace, ref transReq))
                        {
                            this.WriteEventLog(treatment, transReq);
                        }
                        else
                        {
                            Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("tranReqParam", param));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }

            return result;
        }

        private void WriteEventLog(HIS_TREATMENT treatment, HIS_TRANS_REQ transReq)
        {
            new EventLogGenerator(EventLog.Enum.HisTransReq_TaoYeuCauThanhToan,
                                    transReq.TRANS_REQ_CODE,
                                    MessageUtil.GetMessage(LibraryMessage.Message.Enum.TransReqType_YeuCauThanhToanTheoSoTienConThieuCuaHoSo, param.LanguageCode),
                                    transReq.AMOUNT,"").TreatmentCode(treatment.TREATMENT_CODE).Run();
        }

        private void ProcessNewPtaForOutPatient(HIS_PROGRAM program, List<HIS_PATIENT_TYPE_ALTER> ptas, long finishTime, WorkPlaceSDO workPlace)
        {
            HIS_PATIENT_TYPE_ALTER latestPta = IsNotNullOrEmpty(ptas) ? ptas.OrderByDescending(o => o.LOG_TIME).FirstOrDefault() : null;
            if (IsNotNull(program) && program.AUTO_CHANGE_TO_OUT_PATIENT == Constant.IS_TRUE && IsNotNull(latestPta) && latestPta.TREATMENT_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)
            {
                HIS_PATIENT_TYPE_ALTER rsData = new HIS_PATIENT_TYPE_ALTER();
                Mapper.CreateMap<HIS_PATIENT_TYPE_ALTER, HIS_PATIENT_TYPE_ALTER>();
                HIS_PATIENT_TYPE_ALTER newPta = Mapper.Map<HIS_PATIENT_TYPE_ALTER>(latestPta);
                newPta.EXECUTE_LOGINNAME = ResourceTokenManager.GetLoginName();
                newPta.EXECUTE_USERNAME = ResourceTokenManager.GetUserName();
                newPta.EXECUTE_ROOM_ID = workPlace.RoomId;
                newPta.LOG_TIME = finishTime;
                newPta.TREATMENT_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU;
                if (!this.ptaProcessor.Create(newPta, ref rsData))
                {
                    throw new Exception("Tao thong tin dien dieu tri ngoai tru moi that bai. Rollback");
                }
            }
        }

        private void ProcessCarerCardBorrow(HisTreatmentFinishSDO data, HIS_TREATMENT treatment)
        {
            HisCarerCardBorrowFilterQuery filter = new HisCarerCardBorrowFilterQuery();
            filter.TREATMENT_ID = treatment.ID;
            filter.HAS_NO__OR__LOWER_THAN_GIVE_BACK_TIME = treatment.OUT_TIME;
            List<HIS_CARER_CARD_BORROW> carerCardBorrows = new HisCarerCardBorrowGet().Get(filter);
            if (IsNotNullOrEmpty(carerCardBorrows))
            {
                List<HIS_SERVICE_REQ> reqByCarerCards = new HisServiceReqGet().GetByCarerCardBorrowIds(carerCardBorrows.Select(o => o.ID).ToList());

                if (IsNotNullOrEmpty(reqByCarerCards))
                {
                    List<HIS_SERE_SERV> ssByCarerCards = new HisSereServGet().GetByServiceReqIdsAndHasExecute(reqByCarerCards.Select(o => o.ID).ToList());

                    if (IsNotNullOrEmpty(ssByCarerCards))
                    {
                        List<HIS_SERE_SERV_BILL> hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(ssByCarerCards.Select(o => o.ID).ToList());
                        List<HIS_SERE_SERV> unpaidSS = ssByCarerCards.Where(t => (hasBills == null || !hasBills.Exists(o => o.SERE_SERV_ID == t.ID))).ToList();

                        if (IsNotNullOrEmpty(unpaidSS))
                        {
                            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                            List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(unpaidSS);

                            foreach (var ss in unpaidSS)
                            {
                                HIS_SERVICE_REQ req = reqByCarerCards.FirstOrDefault(o => o.ID == ss.SERVICE_REQ_ID.Value);
                                HIS_CARER_CARD_BORROW cBorrow = req != null ? carerCardBorrows.FirstOrDefault(o => o.ID == req.CARER_CARD_BORROW_ID) : null;
                                if (cBorrow != null)
                                    HisCarerCardBorrowUtil.ChangeAmountWithBorrowAndGiveBackTime(ss, cBorrow.BORROW_TIME, treatment.OUT_TIME.Value);
                            }
                            if (!this.ssUpdateProcessor.UpdateList(unpaidSS, olds, false))
                            {
                                throw new Exception("Cap nhat lai thong tin so luong ngay muon the that bai. Rollback");
                            }
                        }
                    }
                }
            }
        }

        private void WriteEventLog(HisTreatmentFinishSDO data, HIS_TREATMENT resultData)
        {
            var treatmentType = HisTreatmentTypeCFG.DATA.FirstOrDefault(o => o.ID == resultData.TDL_TREATMENT_TYPE_ID);
            var treatmentEndType = HisTreatmentEndTypeCFG.DATA.FirstOrDefault(o => o.ID == resultData.TREATMENT_END_TYPE_ID);
            var treatmentEndTypeExt = HisTreatmentEndTypeExtCFG.DATA.FirstOrDefault(o => o.ID == resultData.TREATMENT_END_TYPE_EXT_ID);
            if (data.IsTemporary)
            {
                new EventLogGenerator(EventLog.Enum.HisTreatment_KetThucDieuTriLuuTam,
                                    treatmentType != null ? treatmentType.TREATMENT_TYPE_NAME : "",
                                    treatmentEndType != null ? treatmentEndType.TREATMENT_END_TYPE_NAME : "",
                                    treatmentEndTypeExt != null ? treatmentEndTypeExt.TREATMENT_END_TYPE_EXT_NAME : "").TreatmentCode(resultData.TREATMENT_CODE).Run();
            }
            else
            {
                new EventLogGenerator(EventLog.Enum.HisTreatment_KetThucDieuTri,
                                    treatmentType != null ? treatmentType.TREATMENT_TYPE_NAME : "",
                                    treatmentEndType != null ? treatmentEndType.TREATMENT_END_TYPE_NAME : "",
                                    treatmentEndTypeExt != null ? treatmentEndTypeExt.TREATMENT_END_TYPE_EXT_NAME : "").TreatmentCode(resultData.TREATMENT_CODE).Run();
            }
        }

        private void RegisterTreatmentByAppointment(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_PATIENT hisPatient, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERE_SERV> existsSereServs, WorkPlaceSDO workPlace, ref List<string> sqls)
        {
            long? nextExamNumOrder = null;
            if (!this.registerTreatmentProcessor.Run(data, treatment, hisPatient, ptas, existsSereServs, workPlace, ref nextExamNumOrder))
            {
                throw new Exception("registerTreatmentProcessor. rollback du lieu");
            }
            else if (nextExamNumOrder.HasValue)
            {
                string sql = string.Format("UPDATE HIS_TREATMENT SET NEXT_EXAM_NUM_ORDER = {0} WHERE ID = {1}", nextExamNumOrder.Value, treatment.ID);
                sqls.Add(sql);
            }
        }

        /// <summary>
        /// Xu ly de cap nhat lai han cua the BHYT tam cap cho tre em, cap nhat han the = ngay ra vien
        /// </summary>
        /// <param name="data"></param>
        /// <param name="patientTypeAlters"></param>
        /// <param name="existsSereServs"></param>
        /// <param name="sqls"></param>
        private void ProcessPatientTypeAlter(HisTreatmentFinishSDO data, List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters, List<HIS_SERE_SERV> existsSereServs, ref List<string> sqls)
        {
            if (sqls == null)
            {
                sqls = new List<string>();
            }

            List<HIS_PATIENT_TYPE_ALTER> ptas = patientTypeAlters != null ? patientTypeAlters.Where(t => t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && t.HAS_BIRTH_CERTIFICATE == HeinHasBirthCertificateCode.TRUE && (!t.HEIN_CARD_TO_TIME.HasValue || (t.HEIN_CARD_TO_TIME.HasValue && t.HEIN_CARD_TO_TIME > data.TreatmentFinishTime))).ToList() : null;

            if (IsNotNullOrEmpty(ptas))
            {
                List<long> ids = ptas.Select(o => o.ID).ToList();
                string idStr = string.Join(",", ids);

                string sqlUpdatePta = string.Format("UPDATE HIS_PATIENT_TYPE_ALTER P SET HEIN_CARD_TO_TIME_TMP = HEIN_CARD_TO_TIME, HEIN_CARD_TO_TIME = {0} WHERE ID IN ({1})", data.TreatmentFinishTime, idStr);
                sqls.Add(sqlUpdatePta);

                if (IsNotNullOrEmpty(existsSereServs) && existsSereServs.Exists(t => t.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    foreach (HIS_PATIENT_TYPE_ALTER p in ptas)
                    {
                        if (p.HEIN_CARD_TO_TIME.HasValue)
                        {
                            string newStr = string.Format("'\"HEIN_CARD_TO_TIME\":{0}'", data.TreatmentFinishTime);
                            string oldStr = string.Format("'\"HEIN_CARD_TO_TIME\":{0}'", p.HEIN_CARD_TO_TIME.Value);
                            string sqlUpdateSs = string.Format("UPDATE HIS_SERE_SERV SET JSON_PATIENT_TYPE_ALTER = REPLACE (JSON_PATIENT_TYPE_ALTER, {0}, {1}) WHERE TDL_TREATMENT_ID = {2} AND HEIN_CARD_NUMBER IS NOT NULL AND JSON_PATIENT_TYPE_ALTER LIKE '%\"ID\":{3}%'", oldStr, newStr, data.TreatmentId, p.ID);
                            sqls.Add(sqlUpdateSs);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tinh tai cac thong tin lien quan den gia cua ho so
        /// (do co nghiep vu cap nhat lai gia tien dich vu khi ket thuc dieu tri)
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="ptas"></param>
        /// <param name="existsSereServs"></param>
        private void ProcessRecalcSereServ(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERE_SERV> existsSereServs, HIS_DEPARTMENT_TRAN lastDt)
        {
            if (treatment != null && IsNotNullOrEmpty(existsSereServs))
            {
                Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(existsSereServs);

                //Luu y can set thoi gian vao truoc khi goi ham update hisSereServUpdateHein.UpdateDb
                //vi o nghiep vu tinh toan lai sere_serv co su dung out_time
                treatment.OUT_TIME = data.TreatmentFinishTime;
                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, ptas, false, lastDt.DEPARTMENT_ID, lastDt.DEPARTMENT_IN_TIME, data.TreatmentFinishTime);
                this.hisSereServUpdateHein.UpdateDb(olds, existsSereServs);
            }
        }

        private void ProcessAutoFinishServiceReq(HisTreatmentFinishSDO data, WorkPlaceSDO workPlace, List<HIS_SERE_SERV> existsSereServs, HIS_CAREER career)
        {
            List<HIS_SERVICE_REQ> reqUpdates = new List<HIS_SERVICE_REQ>();
            List<long> srAutoIds = new List<long>();

            //Neu co cau hinh danh sach dich vu tu dong hoan thanh
            //thi tu dong cap nhat ket thuc cac chi dinh tuong ung
            List<long> autoFinishServiceIds = HisTreatmentCFG.AutoFinishServiceIds(workPlace.BranchId);
            var listAutoFinish = existsSereServs != null ? existsSereServs
                .Where(o => !o.IS_NO_EXECUTE.HasValue && autoFinishServiceIds != null
                    && autoFinishServiceIds.Contains(o.SERVICE_ID)).ToList() : null;

            List<long> serviceReqIds = listAutoFinish != null ? listAutoFinish.Select(s => s.SERVICE_REQ_ID.Value).Distinct().ToList() : null;

            serviceReqIds = IsNotNullOrEmpty(serviceReqIds) ? serviceReqIds.Where(o => !existsSereServs.Any(a => o == a.SERVICE_REQ_ID && !listAutoFinish.Any(e => e.ID == a.ID))).ToList() : null;

            if (IsNotNullOrEmpty(serviceReqIds))
            {
                HisServiceReqFilterQuery serviceReqFilter = new HisServiceReqFilterQuery();
                serviceReqFilter.IDs = serviceReqIds;
                serviceReqFilter.SERVICE_REQ_STT_IDs = new List<long>() {
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL,
                        IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL };
                var serviceReqAutos = new HisServiceReqGet().Get(serviceReqFilter);

                if (IsNotNullOrEmpty(serviceReqAutos))
                {
                    foreach (var item in serviceReqAutos)
                    {
                        //cap nhat thong tin ket thuc theo thong tin chi dinh
                        HisServiceReqUpdateFinish.SetFinishInfoUsingRequestInfo(item);
                    }

                    srAutoIds.AddRange(serviceReqAutos.Select(o => o.ID).ToList());
                    reqUpdates.AddRange(serviceReqAutos);
                }
            }

            var allServiceRepByTreatment = new HisServiceReqGet().GetByTreatmentId(data.TreatmentId);
            if (IsNotNullOrEmpty(srAutoIds) && IsNotNullOrEmpty(allServiceRepByTreatment))
            {
                // Loc ra cac service req khong thuoc danh sach tu dong dong
                allServiceRepByTreatment = allServiceRepByTreatment.Where(o => !srAutoIds.Contains(o.ID)).ToList();
                if (IsNotNullOrEmpty(allServiceRepByTreatment))
                {
                    reqUpdates.AddRange(allServiceRepByTreatment);
                }
            }

            if (IsNotNullOrEmpty(reqUpdates))
            {
                // Neu co thong tin gui gui thi moi cap nhat
                // Cap nhat thong tin nghe nghiep
                if (IsNotNull(career))
                {
                    reqUpdates.ForEach(o => o.TDL_PATIENT_CAREER_NAME = career.CAREER_NAME);
                }

                if (!hisServiceReqUpdate.UpdateList(reqUpdates))
                {
                    throw new Exception("Tu dong ket thuc yeu cau dich vu that bai. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessTreatmentBedRoom(HisTreatmentFinishSDO data)
        {
            //Neu nguoi dung THUC SU KET THUC thi moi cho ra khoi buong
            if (!data.IsTemporary)
            {
                List<HIS_TREATMENT_BED_ROOM> treatmentBedRooms = new HisTreatmentBedRoomGet().GetCurrentInByTreatmentId(data.TreatmentId);
                if (IsNotNullOrEmpty(treatmentBedRooms))
                {
                    List<HIS_TREATMENT_BED_ROOM> tmp = null;
                    if (!this.hisTreatmentBedRoomRemove.Remove(treatmentBedRooms, data.TreatmentFinishTime, false, ref tmp))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
            }
        }

        private void ProcessTreatment(HisTreatmentFinishSDO data, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace, HIS_TREATMENT hisTreatment, V_HIS_DEATH_CERT_BOOK deathCertBook, HIS_TREATMENT beforeUpdate, List<HIS_SERE_SERV> existedSereServs, HIS_CAREER career)
        {
            this.SetGeneralInfo(hisTreatment, data, workPlace, ptas, career);

            //Bo sung thong tin chuyen vien (trong truong hop chuyen vien)
            this.SetTransferInfo(hisTreatment, data);

            //Bo sung thong tin tu vong (trong truong hop tu vong)
            this.SetDeathInfo(hisTreatment, beforeUpdate, data, deathCertBook);

            //Bo sung "ma ket thuc bo sung"
            this.SetExtraEndCode(hisTreatment, data);

            //Bo sung thong tin nghi om
            this.SetSickLeaveInfo(hisTreatment, data);

            //Bo sung thong tin nghi om
            this.SetSurgeryAppointmentInfo(hisTreatment, data);

            //Neu ket thuc voi loai la hen kham
            HIS_NUM_ORDER_ISSUE numOrderIssueOld = null;
            this.SetAppointmentInfo(hisTreatment, data, existedSereServs, ref numOrderIssueOld);

            //Thong tin mat
            this.SetEyeInfo(hisTreatment, data);

            //Thong tin tai khoan 'Ky thay truong khoa', 'Ky thay giam doc vien'
            this.SetEndDeptSubsHeadAndHospSubsDirector(hisTreatment, data);

            hisTreatment.IS_YDT_UPLOAD = null;
            hisTreatment.APPROVAL_STORE_STT_ID = null;

            if (!this.hisTreatmentUpdate.Update(hisTreatment, beforeUpdate))
            {
                if (hisTreatment.DEATH_CERT_NUM.HasValue && hisTreatment.DEATH_CERT_BOOK_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_CoTheCapSoChungTuKhongThanhCong);
                }
                throw new Exception("Update HisTreatment that bai. Ket thuc nghiep vu");
            }

            // Xoa number order issue cu sau khi update treatment tranh FK
            if (numOrderIssueOld != null)
            {
                if (!new HisNumOrderIssueTruncate().Truncate(numOrderIssueOld.ID))
                {
                    throw new Exception("Xoa number order issue cu that bai. Ket thuc nghiep vu");
                }
            }

            ExtraEndCodeGenerator.FinishUpdateDB(hisTreatment.EXTRA_END_CODE);
        }

        private void ProcessMediRecord(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_PROGRAM program, ref HIS_MEDI_RECORD mediRecord)
        {
            //Neu ko thay doi thong tin chuong trinh, va chuong trinh luu truoc do da duoc tao ho so benh an thi ket thuc
            if (treatment.PROGRAM_ID == data.ProgramId && treatment.MEDI_RECORD_ID.HasValue)
            {
                mediRecord = new HisMediRecordGet().GetById(treatment.MEDI_RECORD_ID.Value);
                return;
            }

            long? toDeleteMediRecordId = null;
            if (treatment.PROGRAM_ID != data.ProgramId && treatment.MEDI_RECORD_ID.HasValue)
            {
                toDeleteMediRecordId = treatment.MEDI_RECORD_ID;
                treatment.STORE_CODE = null;//clear ma luu tru cu
            }

            treatment.PROGRAM_ID = data.ProgramId;

            if ((!data.IsTemporary
                || HisTreatmentCFG.STORE_MEDI_RECORD_BY_PROGRAM_INCASE_OF_TEMPORARY_FINISHING
                ) && (data.CreateOutPatientMediRecord || HisTreatmentCFG.AUTO_STORE_MEDI_RECORD_BY_PROGRAM) && program != null)
            {
                long storeTime = data.TreatmentFinishTime;

                if (!string.IsNullOrWhiteSpace(HisMediRecordCFG.RESET_DATE_WITH_OUTPATIENT) &&
                    (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM
                    || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                    ))
                {
                    try
                    {
                        DateTime d = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.TreatmentFinishTime).Value;
                        int year = d.Year;
                        string date = HisMediRecordCFG.RESET_DATE_WITH_OUTPATIENT.Substring(0, 2);
                        string month = HisMediRecordCFG.RESET_DATE_WITH_OUTPATIENT.Substring(3, 2);
                        DateTime resetTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(long.Parse(string.Format("{0}{1}{2}000000", year, month, date))).Value;

                        if (d >= resetTime)
                        {
                            storeTime = long.Parse(string.Format("{0}0101000000", year + 1));
                        }
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error(ex);
                    }
                }

                //Neu co cau hinh tu dong tao benh an theo chuong trinh va dien dieu tri la kham hoac ngoai tru
                //Hoac nguoi dung co check "tao BA ngoai tru"
                if (data.CreateOutPatientMediRecord || (HisTreatmentCFG.AUTO_STORE_MEDI_RECORD_BY_PROGRAM && (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU)))
                {
                    HisMediRecordFilterQuery filter = new HisMediRecordFilterQuery();
                    filter.PROGRAM_ID = program.ID;
                    filter.VIR_STORE_YEAR__EQUAL = decimal.Parse((storeTime).ToString().Substring(0, 4));
                    filter.PATIENT_ID = treatment.PATIENT_ID;
                    List<HIS_MEDI_RECORD> mediRecords = new HisMediRecordGet().Get(filter);

                    if (IsNotNullOrEmpty(mediRecords))
                    {
                        mediRecord = mediRecords[0];
                    }
                }

                if (mediRecord == null)
                {
                    long? seedTime = Inventec.Common.DateTime.Get.Now().Value;

                    mediRecord = new HIS_MEDI_RECORD();
                    mediRecord.MEDI_RECORD_TYPE_ID = data.MediRecordTypeId;
                    mediRecord.PATIENT_ID = treatment.PATIENT_ID;
                    mediRecord.EMR_COVER_TYPE_ID = treatment.EMR_COVER_TYPE_ID;
                    mediRecord.PROGRAM_ID = program.ID;
                    mediRecord.DATA_STORE_ID = program.DATA_STORE_ID;
                    mediRecord.STORE_TIME = storeTime;
                    //Neu la benh an ngoai tru thi tu dong "nhap kho" neu co cau hinh "tu dong nhap kho"
                    mediRecord.IS_NOT_STORED = data.CreateOutPatientMediRecord && HisMediRecordCFG.AUTO_STORED_WITH_OUTPATIENT_MEDI_RECORD ? null : (short?)Constant.IS_TRUE;

                    List<string> storeCodes = new List<string>();
                    mediRecord.STORE_CODE = new StoreCodeGeneratorFactory(param).GenerateStoreCode(storeTime, mediRecord.DATA_STORE_ID, treatment, ref seedTime, ref storeCodes);
                    mediRecord.SEED_CODE_TIME = seedTime;
                    if (!this.hisMediRecordCreate.Create(mediRecord))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    new StoreCodeGeneratorFactory(param).FinishUpdateDB(storeCodes);

                    //Can truy van lai de lay thong tin store_code duoc sinh ra boi DB
                    //Truong nay ko the set identity duoc do co truong hop tu sinh boi DB hoac duoc set vao tu backend
                    mediRecord = new HisMediRecordGet().GetById(mediRecord.ID);
                }

                treatment.MEDI_RECORD_ID = mediRecord.ID;
                treatment.STORE_CODE = mediRecord.STORE_CODE;
                treatment.EMR_COVER_TYPE_ID = mediRecord.EMR_COVER_TYPE_ID; //gan lai loai vo benh an trong ho so theo loai trong benh an
            }
            else
            {
                treatment.MEDI_RECORD_ID = null;
            }

            //Neu doi sang chuong trinh khac dan den phai tao ho so benh an khac thi xoa ho so benh an cu
            if (toDeleteMediRecordId.HasValue)
            {
                List<HIS_TREATMENT> t = new HisTreatmentGet().GetByMediRecordId(toDeleteMediRecordId.Value);
                List<HIS_TREATMENT> treatments = t != null ? t.Where(o => o.ID != treatment.ID).ToList() : null;
                if (IsNotNullOrEmpty(treatments))
                {
                    string deleteMediRecord = string.Format("DELETE FROM HIS_MEDI_RECORD WHERE ID = {0}", toDeleteMediRecordId.Value);
                    if (!DAOWorker.SqlDAO.Execute(deleteMediRecord))
                    {
                        throw new Exception("Xoa HIS_MEDI_RECORD that bai ");
                    }
                }
            }
        }

        private void ProcessPatientProgram(HisTreatmentFinishSDO data, HIS_TREATMENT treatment)
        {
            if (data.ProgramId.HasValue)
            {
                HisPatientProgramFilterQuery filter = new HisPatientProgramFilterQuery();
                filter.PROGRAM_ID = data.ProgramId.Value;
                filter.PATIENT_ID = treatment.PATIENT_ID;
                List<HIS_PATIENT_PROGRAM> pp = new HisPatientProgramGet().Get(filter);
                if (!IsNotNullOrEmpty(pp))
                {
                    HIS_PATIENT_PROGRAM p = new HIS_PATIENT_PROGRAM();
                    p.PATIENT_ID = treatment.PATIENT_ID;
                    p.PROGRAM_ID = data.ProgramId.Value;
                    if (!this.hisPatientProgramCreate.CreateNoCheckExists(p))
                    {
                        throw new Exception("Rollback du lieu");
                    }
                }
            }
        }

        /// <summary>
        /// Thong tin chung
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="data"></param>
        /// <param name="workPlace"></param>
        private void SetGeneralInfo(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data, WorkPlaceSDO workPlace, List<HIS_PATIENT_TYPE_ALTER> ptas, HIS_CAREER career)
        {
            List<long> notExamTreatmentTypeIds = new List<long>{
                IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY,
                IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU,
                IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU,
            };

            bool isExistTreatment = IsNotNullOrEmpty(ptas) && ptas.Exists(t => notExamTreatmentTypeIds.Contains(t.TREATMENT_TYPE_ID));

            //Neu ko co dien doi tuong "dieu tri" thi xoa het thong tin y/c nhap vien
            //Luu y: day la nghiep vu quan trong (do co xoa du lieu) nen can kiem tra theo du lieu 
            //thong tin dien dieu tri (HIS_PATIENT_TYPE_ALTER) chu ko kiem tra theo truong du thua 
            //du lieu TDL_TREATMENT_TYPE_ID trong HIS_TREATMENT
            if (!isExistTreatment)
            {
                hisTreatment.IN_DEPARTMENT_ID = null;
                hisTreatment.IN_LOGINNAME = null;
                hisTreatment.IN_ROOM_ID = null;
                hisTreatment.IN_TREATMENT_TYPE_ID = null;
                hisTreatment.IN_USERNAME = null;
            }


            hisTreatment.TREATMENT_END_TYPE_ID = data.TreatmentEndTypeId;
            hisTreatment.TREATMENT_RESULT_ID = data.TreatmentResultId.HasValue && data.TreatmentResultId > 0 ? data.TreatmentResultId.Value : HisTreatmentResultCFG.TREATMENT_RESULT_ID__DEFAULT_OF_EXAM;
            hisTreatment.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
            hisTreatment.ICD_NAME = data.IcdName;
            hisTreatment.ICD_CODE = data.IcdCode;
            hisTreatment.TRADITIONAL_ICD_NAME = data.TraditionalIcdName;
            hisTreatment.TRADITIONAL_ICD_CODE = data.TraditionalIcdCode;
            hisTreatment.TRADITIONAL_ICD_TEXT = data.TraditionalIcdText;
            hisTreatment.TRADITIONAL_ICD_SUB_CODE = data.TraditionalIcdSubCode;
            hisTreatment.ICD_CAUSE_NAME = data.IcdCauseName;
            hisTreatment.ICD_CAUSE_CODE = data.IcdCauseCode;
            hisTreatment.ICD_SUB_CODE = HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode);
            hisTreatment.END_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            hisTreatment.END_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            hisTreatment.END_ROOM_ID = workPlace.RoomId;
            hisTreatment.END_DEPARTMENT_ID = workPlace.DepartmentId;
            hisTreatment.OUT_TIME = data.TreatmentFinishTime;
            hisTreatment.ADVISE = data.Advise;
            hisTreatment.TREATMENT_METHOD = data.TreatmentMethod;
            hisTreatment.TREATMENT_DIRECTION = data.TreatmentDirection;
            hisTreatment.TREATMENT_DAY_COUNT = data.TreatmentDayCount;
            hisTreatment.CLINICAL_NOTE = data.ClinicalNote;
            hisTreatment.SUBCLINICAL_RESULT = data.SubclinicalResult;
            hisTreatment.MEDI_RECORD_TYPE_ID = data.MediRecordTypeId;
            hisTreatment.SURGERY = data.Surgery;
            hisTreatment.TREATMENT_END_TYPE_EXT_ID = data.TreatmentEndTypeExtId;
            hisTreatment.TDL_SOCIAL_INSURANCE_NUMBER = data.SocialInsuranceNumber;
            hisTreatment.SHOW_ICD_CODE = data.ShowIcdCode;
            hisTreatment.SHOW_ICD_NAME = data.ShowIcdName;
            hisTreatment.SHOW_ICD_SUB_CODE = data.ShowIcdSubCode;
            hisTreatment.SHOW_ICD_TEXT = data.ShowIcdText;
            hisTreatment.EXIT_DEPARTMENT_ID = data.ExitDepartmentId;
            hisTreatment.OUTPATIENT_DATE_FROM = data.OutPatientDateFrom;
            hisTreatment.OUTPATIENT_DATE_TO = data.OutPatientDateTo;
            hisTreatment.END_TYPE_EXT_NOTE = data.EndTypeExtNote;
            if (data.IsChronic.HasValue && data.IsChronic.Value)
            {
                hisTreatment.IS_CHRONIC = MOS.UTILITY.Constant.IS_TRUE;
            }
            if (!String.IsNullOrWhiteSpace(data.DoctorLoginname))
            {
                hisTreatment.DOCTOR_LOGINNAME = data.DoctorLoginname;
            }
            if (!String.IsNullOrWhiteSpace(data.DoctorUsernname))
            {
                hisTreatment.DOCTOR_USERNAME = data.DoctorUsernname;
            }
            //Neu nguoi dung THUC SU KET THUC thi moi thuc hien cap nhat trang thai
            if (!data.IsTemporary)
            {
                hisTreatment.IS_PAUSE = MOS.UTILITY.Constant.IS_TRUE;
            }
            else
            {
                hisTreatment.IS_APPROVE_FINISH = data.IsApproveFinish ? (short?)Constant.IS_TRUE : null;
                hisTreatment.APPROVE_FINISH_NOTE = data.ApproveFinishNote;
            }

            //Neu ket thuc voi loai la "chuyen vien" thi thuc hien sinh so chuyen vien
            if (hisTreatment.TREATMENT_END_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN
                && (!data.IsTemporary || HisTreatmentCFG.GENERATE_OUT_CODE_IN_CASE_OF_TEMP_FINISHING))
            {
                if (hisTreatment.OUT_CODE == null ||
                    (!string.IsNullOrWhiteSpace(hisTreatment.OUT_CODE) && data.OutCodeRequest))
                {
                    hisTreatment.IS_OUT_CODE_REQUEST = Constant.IS_TRUE;
                }
            }

            //Neu ko cau hinh nhap so ra vien thu cong --> thuc hien sinh so tu dong
            if (!HisTreatmentCFG.IS_MANUAL_END_CODE)
            {
                if (!data.IsTemporary || HisTreatmentCFG.GENERATE_END_CODE_IN_CASE_OF_TEMP_FINISHING)
                {
                    //Se sinh so tu dong trong truong hop chua co so ra vien 
                    //hoac da co so ra vien nhung nguoi dung yeu cau sinh lai so ra vien
                    if (hisTreatment.END_CODE == null ||
                        (!string.IsNullOrWhiteSpace(hisTreatment.END_CODE) && data.EndCodeRequest))
                    {
                        if (!EndCodeGenerator.SetEndCode(hisTreatment, param))
                        {
                            throw new Exception();
                        }
                    }
                }
            }

            //Neu co cau hinh nhap so ra vien thu cong thi lay thong tin so ra vien do nguoi dung nhap vao
            if (!string.IsNullOrWhiteSpace(data.EndCode) && HisTreatmentCFG.IS_MANUAL_END_CODE)
            {
                hisTreatment.END_CODE = data.EndCode;
            }
            hisTreatment.HRM_KSK_CODE = data.HrmKskCode;

            // Neu co thong tin gui gui thi moi cap nhat
            if (IsNotNull(career))
            {
                hisTreatment.TDL_PATIENT_CAREER_NAME = career.CAREER_NAME;
            }
        }

        /// <summary>
        /// Thong tin chuyen vien
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="data"></param>
        private void SetTransferInfo(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data)
        {
            //Neu ket thuc voi loai la chuyen vien
            if (data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHUYEN)
            {
                //neu ko co cac truong bat buoc nhap thi canh bao
                if (!data.TranPatiFormId.HasValue || !data.TranPatiReasonId.HasValue ||
                    string.IsNullOrWhiteSpace(data.TransferOutMediOrgCode) || string.IsNullOrWhiteSpace(data.TransferOutMediOrgName))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    throw new Exception("Chuyen vien bat buoc nhap noi chuyen den, hinh thuc chuyen, ly do chuyen");
                }
                //cap nhat thong tin chuyen di vao treatment
                hisTreatment.MEDI_ORG_CODE = data.TransferOutMediOrgCode;
                hisTreatment.MEDI_ORG_NAME = data.TransferOutMediOrgName;
                hisTreatment.TRAN_PATI_FORM_ID = data.TranPatiFormId;
                hisTreatment.TRAN_PATI_REASON_ID = data.TranPatiReasonId;
                hisTreatment.TRAN_PATI_TECH_ID = data.TranPatiTechId;
                hisTreatment.PATIENT_CONDITION = data.PatientCondition;
                hisTreatment.TRANSPORT_VEHICLE = data.TransportVehicle;
                hisTreatment.TRANSPORTER = data.Transporter;
                hisTreatment.USED_MEDICINE = data.UsedMedicine;
                hisTreatment.TRAN_PATI_HOSPITAL_LOGINNAME = data.TranPatiHospitalLoginname;
                hisTreatment.TRAN_PATI_HOSPITAL_USERNAME = data.TranPatiHospitalUsername;
            }
            else
            {
                hisTreatment.MEDI_ORG_CODE = null;
                hisTreatment.MEDI_ORG_NAME = null;
                hisTreatment.TRAN_PATI_FORM_ID = null;
                hisTreatment.TRAN_PATI_REASON_ID = null;
                hisTreatment.TRAN_PATI_TECH_ID = null;
                hisTreatment.PATIENT_CONDITION = null;
                hisTreatment.TRANSPORT_VEHICLE = null;
                hisTreatment.TRANSPORTER = null;
                hisTreatment.USED_MEDICINE = null;
                hisTreatment.TRAN_PATI_HOSPITAL_LOGINNAME = null;
                hisTreatment.TRAN_PATI_HOSPITAL_USERNAME = null;
            }
        }

        /// <summary>
        /// Thong tin tu vong
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="data"></param>
        private void SetDeathInfo(HIS_TREATMENT hisTreatment, HIS_TREATMENT beforeUpdate, HisTreatmentFinishSDO data, V_HIS_DEATH_CERT_BOOK deathCertBook)
        {
            //Neu ket thuc voi loai la tu vong
            if (data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET)
            {
                //neu ko co cac truong bat buoc nhap thi canh bao
                if (!data.DeathCauseId.HasValue || !data.DeathWithinId.HasValue || !data.DeathTime.HasValue)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    throw new Exception("Tu vong phai nhap thoi gian, tu vong trong vong, ly do tu vong");
                }
                //cap nhat thong tin vao treatment
                hisTreatment.DEATH_CAUSE_ID = data.DeathCauseId;
                hisTreatment.DEATH_WITHIN_ID = data.DeathWithinId;
                hisTreatment.DEATH_TIME = data.DeathTime;
                hisTreatment.IS_HAS_AUPOPSY = data.IsHasAupopsy;
                hisTreatment.MAIN_CAUSE = data.MainCause;
                hisTreatment.DEATH_DOCUMENT_TYPE = data.DeathDocumentType;
                hisTreatment.DEATH_DOCUMENT_NUMBER = data.DeathDocumentNumber;
                hisTreatment.DEATH_DOCUMENT_PLACE = data.DeathDocumentPlace;
                hisTreatment.DEATH_PLACE = data.DeathPlace;
                hisTreatment.DEATH_DOCUMENT_DATE = data.DeathDocumentDate;
                hisTreatment.DEATH_CERT_BOOK_FIRST_ID = data.DeathCertBookFirstId;
                hisTreatment.DEATH_CERT_NUM_FIRST = data.DeathCertNumFirst;
                hisTreatment.DEATH_CERT_ISSUER_LOGINNAME = data.DeathCertIssuerLoginname;
                hisTreatment.DEATH_CERT_ISSUER_USERNAME = data.DeathCertIssuerUsername;
                hisTreatment.TDL_PATIENT_RELATIVE_NAME = data.PatientRelativeName;
                hisTreatment.DEATH_STATUS = data.DeathStatus;
                hisTreatment.DEATH_DOCUMENT_TYPE_CODE = data.DeathDocumentTypeCode;
                hisTreatment.DEATH_ISSUED_DATE = data.DeathIssuedDate;
                if (deathCertBook != null)
                {
                    //Neu du lieu cu da co so chung tu va ko thay doi so (vd: nguoi dung ket thuc roi mo lai ho so, sau do dong lai)
                    //thi su dung lai so chung tu cu
                    if (beforeUpdate.DEATH_CERT_BOOK_ID == deathCertBook.ID && beforeUpdate.DEATH_CERT_NUM.HasValue)
                    {
                        hisTreatment.DEATH_CERT_BOOK_ID = deathCertBook.ID;
                        hisTreatment.DEATH_CERT_NUM = beforeUpdate.DEATH_CERT_NUM;
                    }
                    else
                    {
                        hisTreatment.DEATH_CERT_BOOK_ID = deathCertBook.ID;
                        hisTreatment.DEATH_CERT_NUM = deathCertBook.CURRENT_DEATH_CERT_NUM.HasValue ? (long)deathCertBook.CURRENT_DEATH_CERT_NUM.Value + 1 : deathCertBook.FROM_NUM_ORDER;
                    }
                }
            }
            else
            {
                hisTreatment.DEATH_CAUSE_ID = null;
                hisTreatment.DEATH_WITHIN_ID = null;
                hisTreatment.DEATH_TIME = null;
                hisTreatment.IS_HAS_AUPOPSY = null;
                hisTreatment.MAIN_CAUSE = null;
                hisTreatment.DEATH_DOCUMENT_TYPE = null;
                hisTreatment.DEATH_DOCUMENT_NUMBER = null;
                hisTreatment.DEATH_DOCUMENT_PLACE = null;
                hisTreatment.DEATH_PLACE = null;
                hisTreatment.DEATH_DOCUMENT_DATE = null;
                hisTreatment.DEATH_CERT_NUM = null;
                hisTreatment.DEATH_CERT_BOOK_ID = null;
                hisTreatment.DEATH_CERT_BOOK_FIRST_ID = null;
                hisTreatment.DEATH_CERT_NUM_FIRST = null;
                hisTreatment.DEATH_CERT_ISSUER_LOGINNAME = null;
                hisTreatment.DEATH_CERT_ISSUER_USERNAME = null;
                hisTreatment.DEATH_STATUS = null;
                hisTreatment.DEATH_DOCUMENT_TYPE_CODE = null;
                hisTreatment.DEATH_ISSUED_DATE = null;
            }
        }

        private void SetExtraEndCode(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data)
        {
            //Neu co thong tin ket thuc bo sung
            if (data.TreatmentEndTypeExtId.HasValue)
            {
                hisTreatment.TREATMENT_END_TYPE_EXT_ID = data.TreatmentEndTypeExtId;
                //Lay 2 chu so cuoi cua nam
                string year = data.TreatmentFinishTime.ToString().Substring(2, 2);

                HIS_TREATMENT_END_TYPE_EXT treatmentEndTypeExt = HisTreatmentEndTypeExtCFG.DATA.Where(o => o.ID == data.TreatmentEndTypeExtId.Value).FirstOrDefault();

                string seedCode = string.Format("{0}/{1}", treatmentEndTypeExt.TREATMENT_END_TYPE_EXT_CODE, year);
                hisTreatment.EXTRA_END_CODE = ExtraEndCodeGenerator.GetNext(seedCode);
                hisTreatment.EXTRA_END_CODE_SEED_CODE = seedCode;

            }
            else
            {
                hisTreatment.EXTRA_END_CODE = null;
            }
        }

        /// <summary>
        /// Thong tin nghi om
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="data"></param>
        private void SetSickLeaveInfo(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data)
        {
            if (data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM
                || data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_DUONG_THAI)
            {

                //cap nhat thong tin vao treatment
                hisTreatment.SICK_LEAVE_DAY = data.SickLeaveDay;
                hisTreatment.SICK_LEAVE_FROM = data.SickLeaveFrom;
                hisTreatment.SICK_LEAVE_TO = data.SickLeaveTo;
                hisTreatment.TDL_PATIENT_RELATIVE_NAME = data.PatientRelativeName;
                hisTreatment.TDL_PATIENT_RELATIVE_TYPE = data.PatientRelativeType;
                hisTreatment.TDL_PATIENT_WORK_PLACE = data.PatientWorkPlace;
                
                HIS_WORK_PLACE workPlace = null;

                if (data.WorkPlaceId.HasValue)
                {
                    workPlace = new HisWorkPlaceGet().GetById(data.WorkPlaceId.Value);
                }

                hisTreatment.TDL_PATIENT_WORK_PLACE_NAME = workPlace != null ? workPlace.WORK_PLACE_NAME : null;
                hisTreatment.SICK_HEIN_CARD_NUMBER = data.SickHeinCardNumber;
                if (data.IsPregnancyTermination == true)
                {
                    hisTreatment.IS_PREGNANCY_TERMINATION = Constant.IS_TRUE;
                }
                else
                {
                    hisTreatment.IS_PREGNANCY_TERMINATION = null;
                }
                hisTreatment.GESTATIONAL_AGE = data.GestationalAge;
                hisTreatment.PREGNANCY_TERMINATION_REASON = data.PregnancyTerminationReason;
                hisTreatment.PREGNANCY_TERMINATION_TIME = data.PregnancyTerminationTime;

                if (data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM)
                {
                    hisTreatment.DOCUMENT_BOOK_ID = data.DocumentBookId;
                }
                else
                {
                    hisTreatment.DOCUMENT_BOOK_ID = null;
                }

                if (!String.IsNullOrWhiteSpace(data.SickLoginname) && !String.IsNullOrWhiteSpace(data.SickUsername))
                {
                    hisTreatment.SICK_LOGINNAME = data.SickLoginname;
                    hisTreatment.SICK_USERNAME = data.SickUsername;
                }
                else
                {
                    hisTreatment.SICK_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    hisTreatment.SICK_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                }

                if (data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_DUONG_THAI)
                {
                    this.SetBabyInfo(data.Babies, data.TreatmentId);
                }
            }
            else
            {
                hisTreatment.SICK_LEAVE_DAY = null;
                hisTreatment.SICK_LEAVE_FROM = null;
                hisTreatment.SICK_LEAVE_TO = null;
                hisTreatment.SICK_HEIN_CARD_NUMBER = null;
                hisTreatment.SICK_LOGINNAME = null;
                hisTreatment.SICK_USERNAME = null;
                hisTreatment.DOCUMENT_BOOK_ID = null;
                hisTreatment.TDL_DOCUMENT_BOOK_CODE = null;
                hisTreatment.IS_PREGNANCY_TERMINATION = null;
                hisTreatment.GESTATIONAL_AGE = null;
                hisTreatment.PREGNANCY_TERMINATION_REASON = null;
                hisTreatment.PREGNANCY_TERMINATION_TIME = null;
            }
        }

        /// <summary>
        /// Thong tin hen mo
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="data"></param>
        private void SetSurgeryAppointmentInfo(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data)
        {
            if (data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__HEN_MO)
            {
                //cap nhat thong tin vao treatment
                hisTreatment.APPOINTMENT_SURGERY = data.AppointmentSurgery;
                hisTreatment.SURGERY_APPOINTMENT_TIME = data.SurgeryAppointmentTime;
                hisTreatment.ADVISE = data.Advise;
            }
            else
            {
                hisTreatment.APPOINTMENT_SURGERY = null;
                hisTreatment.SURGERY_APPOINTMENT_TIME = null;
            }
        }

        private void SetBabyInfo(List<HisBabySDO> babies, long treatmentId)
        {
            //Chi lay cac dong co nhap thong tin
            babies = IsNotNullOrEmpty(babies) ? babies.Where(o => !string.IsNullOrWhiteSpace(o.BabyName)).ToList() : null;

            if (IsNotNullOrEmpty(babies))
            {
                List<HIS_BABY> exists = new HisBabyGet().GetByTreatmentId(treatmentId);
                if (IsNotNullOrEmpty(exists) && !this.hisBabyTruncate.TruncateList(exists))
                {
                    throw new Exception("Xoa du lieu so sinh (his_baby) cu that bai");
                }

            }
            if (IsNotNullOrEmpty(babies))
            {
                List<HIS_BABY> toInserts = babies.Select(o => new HIS_BABY
                {
                    TREATMENT_ID = treatmentId,
                    BABY_NAME = o.BabyName,
                    BABY_ORDER = o.BabyOrder,
                    BORN_POSITION_ID = o.BornPositionId,
                    BORN_RESULT_ID = o.BornResultId,
                    BORN_TIME = o.BornTime,
                    BORN_TYPE_ID = o.BornTypeId,
                    FATHER_NAME = o.FatherName,
                    GENDER_ID = o.GenderId,
                    HEAD = o.Head,
                    HEIGHT = o.Height,
                    MIDWIFE = o.Midwife,
                    MONTH_COUNT = o.MonthCount,
                    WEEK_COUNT = o.WeekCount,
                    WEIGHT = o.Weight,
                    ETHNIC_CODE = o.EthnicCode,
                    ETHNIC_NAME = o.EthnicName,
                    ISSUER_LOGINNAME = (String.IsNullOrWhiteSpace(o.IssuerLoginname) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName() : o.IssuerLoginname),
                    ISSUER_USERNAME = (String.IsNullOrWhiteSpace(o.IssuerLoginname) ? Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName() : o.IssuerUsername),
                }).ToList();

                if (IsNotNullOrEmpty(toInserts) && !this.hisBabyCreate.CreateList(toInserts))
                {
                    throw new Exception("Insert du lieu so sinh (his_baby) that bai");
                }
            }
        }

        /// <summary>
        /// Thong tin hen kham
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="data"></param>
        private void SetAppointmentInfo(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data, List<HIS_SERE_SERV> existedSereServs, ref HIS_NUM_ORDER_ISSUE numOrderIssueOld)
        {
            if ((data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN
                || data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__RAVIEN
                || data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__XINRAVIEN)
                && data.AppointmentTime.HasValue)
            {
                HIS_SERE_SERV mainExam = existedSereServs != null ? existedSereServs.Where(o => o.TDL_IS_MAIN_EXAM == Constant.IS_TRUE).FirstOrDefault() : null;

                //neu ko co cac truong bat buoc nhap thi canh bao
                if ((!data.AppointmentTime.HasValue || data.AppointmentTime == 0) && data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    throw new Exception("Ket thuc la hen kham, bat buoc truyen thong tin thoi gian hen kham");
                }
                //cap nhat thong tin vao treatment
                hisTreatment.APPOINTMENT_TIME = data.AppointmentTime;
                hisTreatment.APPOINTMENT_PERIOD_ID = data.AppointmentPeriodId;
                long appointmentExamRoomId = 0;

                //Lay thong tin chi dinh dich vu hen kham
                //chi lay dich vu kham
                HisAppointmentServViewFilterQuery filter = new HisAppointmentServViewFilterQuery();
                filter.TREATMENT_ID = data.TreatmentId;
                filter.SERVICE_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH;
                List<V_HIS_APPOINTMENT_SERV> appointmentServs = new HisAppointmentServGet().GetView(filter);
                if (IsNotNullOrEmpty(appointmentServs))
                {
                    hisTreatment.APPOINTMENT_EXAM_SERVICE_ID = appointmentServs.OrderBy(o => o.ID).Select(o => o.SERVICE_ID).FirstOrDefault();
                }
                else if (mainExam != null)
                {
                    hisTreatment.APPOINTMENT_EXAM_SERVICE_ID = mainExam.SERVICE_ID;
                }
                if (IsNotNullOrEmpty(data.AppointmentExamRoomIds))
                {
                    hisTreatment.APPOINTMENT_EXAM_ROOM_IDS = string.Join(",", data.AppointmentExamRoomIds);
                    appointmentExamRoomId = data.AppointmentExamRoomIds[0];
                }
                else if ((hisTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                        || hisTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                        && mainExam != null)
                {
                    hisTreatment.APPOINTMENT_EXAM_ROOM_IDS = mainExam.TDL_EXECUTE_ROOM_ID.ToString();
                    appointmentExamRoomId = mainExam.TDL_EXECUTE_ROOM_ID;
                }
                else
                {
                    hisTreatment.APPOINTMENT_EXAM_ROOM_IDS = null;
                }

                this.SetNumOrder(hisTreatment, data.AppointmentTime.Value, appointmentExamRoomId, data.NumOrderBlockId, ref numOrderIssueOld);
            }
            else
            {
                hisTreatment.APPOINTMENT_TIME = null;
                hisTreatment.APPOINTMENT_EXAM_ROOM_IDS = null;
                hisTreatment.APPOINTMENT_EXAM_SERVICE_ID = null;
            }
        }

        private void SetNumOrder(HIS_TREATMENT treatment, long appointmentTime, long appointmentExamRoomId, long? numOrderBlockId, ref HIS_NUM_ORDER_ISSUE toDeleteNumOrderIssue)
        {
            // Xu ly check trung ngay va block hen kham
            if (treatment.NUM_ORDER_ISSUE_ID.HasValue)
            {
                HIS_NUM_ORDER_ISSUE num = new HisNumOrderIssueGet().GetById(treatment.NUM_ORDER_ISSUE_ID.Value);
                if (num != null)
                {
                    long issueDate = Inventec.Common.DateTime.Get.StartDay(appointmentTime).Value;

                    if (num.ISSUE_DATE == issueDate && num.NUM_ORDER_BLOCK_ID == numOrderBlockId)
                    {
                        return;
                    }
                    else
                    {
                        toDeleteNumOrderIssue = num;
                    }
                }
            }

            //Xu ly de lay STT kham
            long? resultNumOrder = null;
            long? resultNumOrderIssueId = null;
            string fromTime = null;
            string toTime = null;

            new HisNumOrderIssueUtil(param).Issue(appointmentTime, appointmentExamRoomId, numOrderBlockId, null, null, ref resultNumOrder, ref resultNumOrderIssueId, ref fromTime, ref toTime);

            treatment.NEXT_EXAM_NUM_ORDER = resultNumOrder;
            treatment.NEXT_EXAM_FROM_TIME = fromTime;
            treatment.NEXT_EXAM_TO_TIME = toTime;
            treatment.NUM_ORDER_ISSUE_ID = resultNumOrderIssueId;
        }

        private void SetEyeInfo(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data)
        {
            hisTreatment.EYE_TENSION_LEFT = data.EyeTensionLeft;
            hisTreatment.EYE_TENSION_RIGHT = data.EyeTensionRight;
            hisTreatment.EYESIGHT_GLASS_LEFT = data.EyesightGlassLeft;
            hisTreatment.EYESIGHT_GLASS_RIGHT = data.EyesightGlassRight;
            hisTreatment.EYESIGHT_LEFT = data.EyesightLeft;
            hisTreatment.EYESIGHT_RIGHT = data.EyesightRight;
        }

        /// <summary>
        /// Thong tin tai khoan 'Ky thay truong khoa', 'Ky thay giam doc vien'
        /// </summary>
        /// <param name="hisTreatment"></param>
        /// <param name="data"></param>
        private void SetEndDeptSubsHeadAndHospSubsDirector(HIS_TREATMENT hisTreatment, HisTreatmentFinishSDO data)
        {
            HIS_DEPARTMENT department = HisDepartmentCFG.DATA.FirstOrDefault(o => o.ID == hisTreatment.LAST_DEPARTMENT_ID);
            HIS_BRANCH branch = HisBranchCFG.DATA.FirstOrDefault(o => o.ID == (department!= null ? department.BRANCH_ID : 0));

            hisTreatment.END_DEPARTMENT_HEAD_LOGINNAME = department != null ? department.HEAD_LOGINNAME : null;
            hisTreatment.END_DEPARTMENT_HEAD_USERNAME = department != null ? department.HEAD_USERNAME : null;
            hisTreatment.HOSPITAL_DIRECTOR_LOGINNAME = branch != null ? branch.DIRECTOR_LOGINNAME : null;
            hisTreatment.HOSPITAL_DIRECTOR_USERNAME = branch != null ? branch.DIRECTOR_USERNAME : null;
            hisTreatment.END_DEPT_SUBS_HEAD_LOGINNAME = data.EndDeptSubsHeadLoginname;
            hisTreatment.END_DEPT_SUBS_HEAD_USERNAME = data.EndDeptSubsHeadUsername;
            hisTreatment.HOSP_SUBS_DIRECTOR_LOGINNAME = data.HospSubsDirectorLoginname;
            hisTreatment.HOSP_SUBS_DIRECTOR_USERNAME = data.HospSubsDirectorUsername;
        }

        private void ProcessPatient(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_CAREER career, ref HIS_PATIENT hisPatient)
        {
            hisPatient = new HisPatientGet().GetById(treatment.PATIENT_ID);

            if (hisPatient != null)
            {
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                HIS_PATIENT before = Mapper.Map<HIS_PATIENT>(hisPatient);

                hisPatient.IS_CHRONIC = data.IsChronic.HasValue && data.IsChronic.Value ? (short?)Constant.IS_TRUE : null;
                if (data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_OM
                || data.TreatmentEndTypeExtId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE_EXT.ID__NGHI_DUONG_THAI)
                {
                    hisPatient.WORK_PLACE_ID = data.WorkPlaceId;
                    hisPatient.SOCIAL_INSURANCE_NUMBER = data.SocialInsuranceNumber;
                    hisPatient.WORK_PLACE = data.PatientWorkPlace;
                    hisPatient.RELATIVE_NAME = data.PatientRelativeName;
                    hisPatient.RELATIVE_TYPE = data.PatientRelativeType;
                }
                if (data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__CHET)
                {
                    hisPatient.RELATIVE_NAME = data.PatientRelativeName;
                }
                // Neu co thong tin gui gui thi moi cap nhat
                if (IsNotNull(career))
                {
                    hisPatient.CAREER_ID = career.ID;
                    hisPatient.CAREER_CODE = career.CAREER_CODE;
                    hisPatient.CAREER_NAME = career.CAREER_NAME;
                }

                if (!this.hisPatientUpdate.Update(hisPatient, before))
                {
                    throw new Exception("Cap nhat his_patient that bai.");
                }
            }
        }

        private void ProcessAppointmentServ(HisTreatmentFinishSDO data, ref List<string> sqls)
        {
            if (data.TreatmentEndTypeId == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_END_TYPE.ID__HEN && !data.IsTemporary)
            {
                string sql = string.Format("UPDATE HIS_APPOINTMENT_SERV SET TDL_APPOINTMENT_TIME = {0} WHERE TREATMENT_ID = {1}", data.AppointmentTime, data.TreatmentId);
                sqls.Add(sql);
            }
        }

        private void PassResult(HIS_TREATMENT hisTreatment, ref HIS_TREATMENT resultData)
        {
            //truy van lai de lay thong tin out_code (do trigger sinh)
            resultData = new HisTreatmentGet().GetById(hisTreatment.ID);
        }

        private void AutoCreateAggrExam(HIS_TREATMENT treament, long reqRoomId)
        {
            if (HisExpMestCFG.AUTO_CREATE_AGGR_EXAM_EXP_MEST)
            {
                HisExpMestFilterQuery expFilter = new HisExpMestFilterQuery();
                expFilter.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK;
                expFilter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                expFilter.HAS_AGGR_EXP_MEST_ID = false;
                expFilter.TDL_TREATMENT_ID = treament.ID;
                List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(expFilter);
                if (expMests != null && expMests.Count > 0)
                {
                    if (!new HisExpMestAggrExamCreate(param).RunAuto(expMests, reqRoomId))
                    {
                        throw new Exception("HisExpMestAggrExamCreate. Rollback du lieu");
                    }
                }
            }
        }

        private void InitThreadSyncHRM(HIS_TREATMENT treament)
        {
            try
            {
                if (treament.IS_PAUSE == Constant.IS_TRUE && !String.IsNullOrWhiteSpace(treament.HRM_KSK_CODE))
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.SyncHrmExam));
                    thread.Priority = ThreadPriority.Lowest;
                    thread.Start(treament.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void SyncHrmExam(object threadData)
        {
            try
            {
                long treatmentId = (long)threadData;
                new HisTreatmentUploadHrm().Run(treatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        //Gui thong tin sang PM cu
        private void IntegrateThreadInit(HIS_TREATMENT treatment)
        {
            try
            {
                if (treatment.IS_PAUSE == MOS.UTILITY.Constant.IS_TRUE && OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.NONE)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.ProcessSendingIntegration));
                    thread.Priority = ThreadPriority.Lowest;
                    thread.Start(treatment);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat treatment", ex);
            }
        }

        private void ProcessSendingIntegration(object threadData)
        {
            HIS_TREATMENT td = (HIS_TREATMENT)threadData;
            OldSystemIntegrateProcessor.FinishTreatment(td);
        }

        //Tu dong khoa vien phi
        private void AutoLockTreatment(HIS_TREATMENT treatment)
        {
            try
            {
                //neu co cau hinh tu dong duyet ho so BHYT sau khi thuc hien khoa vien phi
                if (treatment.IS_PAUSE == Constant.IS_TRUE
                    && HisTreatmentCFG.AUTO_LOCK_AFTER_FINISH_IF_HAS_NO_PATIENT_PRICE
                    && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    V_HIS_ROOM endRoom = HisRoomCFG.DATA.Where(o => o.ID == treatment.END_ROOM_ID).FirstOrDefault();
                    V_HIS_CASHIER_ROOM cashierRoom = endRoom != null ? HisCashierRoomCFG.DATA.Where(o => o.BRANCH_ID == endRoom.BRANCH_ID && o.IS_ACTIVE == Constant.IS_TRUE).FirstOrDefault() : null;

                    if (cashierRoom != null)
                    {
                        //V_HIS_TREATMENT_FEE_1 hisTreatmentFee = new HisTreatmentGet().GetFeeView1ById(treatment.ID);
                        List<HIS_SERE_SERV> sereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);
                        List<long> hasPatientPriceIds = IsNotNullOrEmpty(sereServs) ? sereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE > 0).Select(o => o.ID).ToList() : null;

                        bool isNoDebt = true;
                        if (IsNotNullOrEmpty(hasPatientPriceIds))
                        {
                            List<HIS_SERE_SERV_BILL> bills = new HisSereServBillGet().GetNoCancelBySereServIds(hasPatientPriceIds);
                            if (IsNotNullOrEmpty(bills))
                            {
                                //duyet xem tat ca cac sere_serv da duoc thanh toan hay chua
                                foreach (long id in hasPatientPriceIds)
                                {
                                    isNoDebt = isNoDebt && bills.Exists(t => t.SERE_SERV_ID == id);
                                }
                            }
                            else
                            {
                                isNoDebt = false;
                            }
                        }

                        //Neu so tien BN phai tra = 0 thi thuc hien tu dong khoa vien phi
                        if (isNoDebt)
                        {
                            HisTreatmentLockSDO sdo = new HisTreatmentLockSDO();
                            sdo.TreatmentId = treatment.ID;
                            sdo.FeeLockTime = treatment.OUT_TIME.Value;
                            sdo.RequestRoomId = treatment.END_ROOM_ID.Value;

                            HIS_TREATMENT resultData = null;

                            if (!new HisTreatmentLock(param).Run(sdo, treatment, cashierRoom.ID, ref resultData))
                            {
                                LogSystem.Warn("Tu dong duyet khoa vien phi that bai: " + param.GetMessage());
                            }
                        }
                    }
                    else
                    {
                        LogSystem.Error("Ko ton tai phong thu ngan tuong ung voi chi nhanh cua nguoi thuc hien");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh xu ly duyet khoa vien phi that bai", ex);
            }
        }

        private void MakeNewTreatment(HisTreatmentFinishSDO data, HIS_TREATMENT treatment, HIS_DEPARTMENT_TRAN lastDt, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            if (!this.newTreatmentMaker.Run(data, treatment, lastDt, ptas))
            {
                throw new Exception("newTreatmentMaker. rollback du lieu");
            }
        }

        private void InitThreadSCreateMS(HIS_TREATMENT treatment)
        {
            try
            {
                if (IsNotNull(treatment) && CosCFG.IS_CREATE_REGISTER_CODE)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.CreateMS));
                    thread.Priority = ThreadPriority.Lowest;
                    thread.Start(treatment.PATIENT_ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void CreateMS(object data)
        {
            try
            {
                long patientId = (long)data;
                new HisPatientCreateRegisterCode().Run(patientId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void InitThreadNotify(HIS_TREATMENT treatment)
        {
            try
            {
                if (IsNotNull(treatment) && HisTreatmentCFG.NOTIFY_APPROVE_MEDI_RECORD_WHEN_TREATMENT_FINISH)
                {
                    Thread thread = new Thread(new ParameterizedThreadStart(this.Notify));
                    thread.Priority = ThreadPriority.Lowest;
                    thread.Start(treatment.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Tu dong xuat XML thong tuyen
        /// </summary>
        /// <param name="treatment"></param>
        private void InitThreadExportXml(HIS_TREATMENT treatment, HisTreatmentFinishSDO data, List<HIS_PATIENT_TYPE_ALTER> ptas)
        {
            try
            {
                if (IsNotNull(treatment) && IsNotNullOrEmpty(ptas))
                {
                    HIS_PATIENT_TYPE_ALTER lastPta = ptas.OrderByDescending(t => t.LOG_TIME).ThenByDescending(t => t.ID).FirstOrDefault();
                    if (data.IsExpXml4210Collinear && lastPta != null && lastPta.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT)
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(this.ExportXml4210Collinear));
                        thread.Priority = ThreadPriority.Lowest;
                        Thread.Sleep(2000); // Doi cac tien trinh khac thuc hien xong tranh bi update de
                        ThreadExportXmlData threadData = new ThreadExportXmlData();
                        threadData.Branch = new TokenManager().GetBranch();
                        threadData.PatientTypeAlter = lastPta;
                        threadData.TreatmentCode = treatment.TREATMENT_CODE;
                        threadData.TreatmentId = treatment.ID;
                        threadData.PatientCode = treatment.TDL_PATIENT_CODE;
                        thread.Start(threadData);
                    }

                    if (lastPta != null)
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(this.ExportXml2076));
                        thread.Priority = ThreadPriority.Lowest;

                        thread.Start(treatment.ID);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ExportXml4210Collinear(object data)
        {
            try
            {
                ThreadExportXmlData d = (ThreadExportXmlData)data;
                new AutoExportXml4210Collinear().Run(d.TreatmentId, d.TreatmentCode, d.PatientCode, d.Branch, d.PatientTypeAlter);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ExportXml2076(object data)
        {
            try
            {
                long treatId = (long)data;
                new AutoExportXml2076Processor().Run(treatId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Notify(object data)
        {
            try
            {
                long treatmentId = (long)data;
                new HisTreatmentNotify().Run(treatmentId);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal void RollBackData()
        {
            this.ssUpdateProcessor.RollbackData();
            this.newTreatmentMaker.Rollback();
            this.hisPatientUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
            this.hisBabyCreate.RollbackData();
            this.hisMediRecordCreate.RollbackData();
            this.hisPatientProgramCreate.RollbackData();
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.ptaProcessor.RollbackData();
            this.transReqCreate.RollbackData();
        }
    }
}
