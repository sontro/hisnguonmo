using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisDepartmentTran;
using MOS.MANAGER.HisDepartmentTran.Hospitalize;
using MOS.MANAGER.HisDhst;
using MOS.MANAGER.HisEventsCausesDeath;
using MOS.MANAGER.HisExamSereDire;
using MOS.MANAGER.HisIcd;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisServiceReq.Exam.Add;
using MOS.MANAGER.HisServiceReq.Exam.Process.Deposit;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisSevereIllnessInfo;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update.Finish;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceReq.Exam.Process
{
    /// <summary>
    /// Xu ly nghiep vu xu ly kham
    /// </summary>
    class HisServiceReqExamProcess : BusinessBase
    {
        private HIS_SERVICE_REQ recentServiceReq;
        private HIS_DHST recentDhst;
        private HIS_TREATMENT recentTreatment;
        private HisDhstCreate hisDhstCreate;
        private HisDhstUpdate hisDhstUpdate;
        private HisPatientUpdate hisPatientUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisServiceReqUpdate hisServiceReqUpdateOldMain;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisTreatmentFinish hisTreatmentFinish;
        private HisDepartmentTranHospitalize hisDepartmentTranHospitalize;
        private HisServiceReqExamAddition hisServiceReqExamAddition;
        private HisSereServUpdateHein hisSereServUpdateHein;

        private HIS_TREATMENT treatmentFinishResult;
        private HisDepartmentTranHospitalizeResultSDO hospitalizeResult;
        private V_HIS_SERVICE_REQ additionExamResult;
        private HisEventsCausesDeathTruncate hisEventsCausesDeathTruncate;
        private HisSevereIllnessInfoTruncate hisSevereIllnessInfoTruncate;

        internal HisServiceReqExamProcess()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqExamProcess(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisDhstCreate = new HisDhstCreate(param);
            this.hisDhstUpdate = new HisDhstUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisServiceReqUpdateOldMain = new HisServiceReqUpdate(param);
            this.hisTreatmentFinish = new HisTreatmentFinish(param);
            this.hisDepartmentTranHospitalize = new HisDepartmentTranHospitalize(param);
            this.hisServiceReqExamAddition = new HisServiceReqExamAddition(param);
            this.hisPatientUpdate = new HisPatientUpdate(param);
            this.hisEventsCausesDeathTruncate = new HisEventsCausesDeathTruncate(param);
            this.hisSevereIllnessInfoTruncate = new HisSevereIllnessInfoTruncate(param);
        }

        //Xu ly kham
        internal bool Run(HisServiceReqExamUpdateSDO data, ref HisServiceReqExamUpdateResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_SERE_SERV> existedSereServs = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                HIS_DEPARTMENT_TRAN lastDt = null;
                HIS_PROGRAM program = null;
                HIS_MEDI_RECORD mediRecord = null;
                V_HIS_DEATH_CERT_BOOK deathCertBook = null;
                WorkPlaceSDO workPlace = null;
                this.SetServerTime(data);

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisTreatmentFinishCheck treatmentFinishChecker = new HisTreatmentFinishCheck(param);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisServiceReqExamCheck examChecker = new HisServiceReqExamCheck(param);
                HisServiceReqExamProcessCheck processChecker = new HisServiceReqExamProcessCheck(param);

                valid = valid && processChecker.IsValidData(data, ref this.recentServiceReq);
                valid = valid && treatmentChecker.VerifyId(this.recentServiceReq.TREATMENT_ID, ref this.recentTreatment);
                valid = valid && checker.IsNotFinished(this.recentServiceReq);
                valid = valid && treatmentChecker.IsUnLock(this.recentTreatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(this.recentTreatment);
                valid = valid && treatmentChecker.IsUnLockHein(this.recentTreatment);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && examChecker.IsAllowedUser();

                if (valid && data.TreatmentFinishSDO != null)
                {
                    ptas = new HisPatientTypeAlterGet().GetByTreatmentId(this.recentTreatment.ID);

                    existedSereServs = new HisSereServGet().GetByTreatmentId(this.recentTreatment.ID);
                    data.TreatmentFinishSDO.ServiceReqId = this.recentServiceReq.ID;

                    List<HIS_SERE_SERV> recentSereServs = new HisSereServGet().GetByServiceReqId(this.recentServiceReq.ID);
                    var startTime = this.recentServiceReq.START_TIME;
                    var finishTime = data.TreatmentFinishSDO.TreatmentFinishTime;
                    valid = valid && treatmentFinishChecker.IsValidForFinish(data.TreatmentFinishSDO, this.recentTreatment, existedSereServs, ptas, ref lastDt, ref workPlace, ref program, ref deathCertBook);
                }
                if (valid && (!HisServiceReqCFG.DO_NOT_CHECK_MIN_PROCESS_TIME_EXAM_IN_CASE_OF_HOSPITALIZE
                    || (HisServiceReqCFG.DO_NOT_CHECK_MIN_PROCESS_TIME_EXAM_IN_CASE_OF_HOSPITALIZE
                    && ((data.ExamAdditionSDO != null && data.ExamAdditionSDO.IsFinishCurrent) || data.IsFinish || data.TreatmentFinishSDO != null || data.HospitalizeSDO == null)
                    && (this.recentTreatment.IN_TREATMENT_TYPE_ID == null && this.recentTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM))))
                {
                    List<HIS_SERE_SERV> recentSereServs = new HisSereServGet().GetByServiceReqId(this.recentServiceReq.ID);
                    var startTime = this.recentServiceReq.START_TIME;
                    var finishTime = data.FinishTime;
                    valid = valid && treatmentFinishChecker.IsValidMinProcessTime(startTime, finishTime, recentSereServs);
                }

                if (valid)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(this.recentServiceReq);
                    List<HIS_SERVICE_REQ> oldMainExams = null;
                    bool hasChangedMainExam = false;
                    HIS_TRANSACTION transaction = null;
                    HIS_SERE_SERV_DEPOSIT sereServDeposit = null;

                    //Neu co 1 trong cac du lieu xu tri thi tu dong ket thuc y/c kham
                    data.IsFinish = data.IsFinish || data.TreatmentFinishSDO != null || data.HospitalizeSDO != null;

                    //can xu ly dhst truoc service_req vi xu ly service_req co su dung dhst
                    this.ProcessDhst(data);

                    //Can xu ly truoc de cap nhat thong tin "is_main_exam", truoc khi chay vao ProcessServiceReq
                    this.ProcessAutoChangeMainExam(data, ref oldMainExams, ref hasChangedMainExam);

                    this.ProcessServiceReq(data, beforeUpdate, workPlace);

                    this.ProcessSereServ(this.recentTreatment, hasChangedMainExam);

                    this.ProcessPatient(data, beforeUpdate);

                    //Xu ly treatment can xu ly sau service_req 
                    //(tranh truong hop ket thuc dieu tri thi ko the update duoc service_req)
                    this.ProcessTreatment(data, beforeUpdate, hasChangedMainExam, oldMainExams, existedSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref mediRecord);
                    this.ProcessHopitalize(data);
                    this.ProcessAdditionExam(data, this.recentServiceReq, ref transaction, ref sereServDeposit, this.recentTreatment);
                    this.PassResult(mediRecord, transaction, sereServDeposit, ref resultData);
                    result = true;
                    HisServiceReqExamLog.LogProcess(data, this.recentServiceReq, this.additionExamResult);
                    if (hasChangedMainExam)
                        HisServiceReqLog.Run(this.recentServiceReq, LibraryEventLog.EventLog.Enum.HisServiceReq_CapNhatKhamChinh);
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

        private void ProcessSereServ(HIS_TREATMENT treatment, bool hasChangedMainExam)
        {
            //Neu co thay doi kham chinh thi tinh toan lai tien
            if (hasChangedMainExam)
            {
                this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

                //Cap nhat ti le BHYT cho sere_serv
                if (!this.hisSereServUpdateHein.UpdateDb())
                {
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private void ProcessPatient(HisServiceReqExamUpdateSDO data, HIS_SERVICE_REQ beforeUpdate)
        {
            HIS_PATIENT patient = new HisPatientGet().GetById(beforeUpdate.TDL_PATIENT_ID);
            if (patient.PT_PATHOLOGICAL_HISTORY != data.PathologicalHistory
                || patient.PT_PATHOLOGICAL_HISTORY_FAMILY != data.PathologicalHistoryFamily || patient.NOTE != data.NotePatient)
            {
                Mapper.CreateMap<HIS_PATIENT, HIS_PATIENT>();
                HIS_PATIENT patientBefore = Mapper.Map<HIS_PATIENT>(patient);
                patient.PT_PATHOLOGICAL_HISTORY = data.PathologicalHistory;
                patient.PT_PATHOLOGICAL_HISTORY_FAMILY = data.PathologicalHistoryFamily;
                patient.NOTE = data.NotePatient;
                if (!this.hisPatientUpdate.Update(patient, patientBefore))
                {
                    throw new Exception("Cap nhat thong tin qua trinh benh ly, tien su benh nhân, tien su gia dinh that bai");
                }
            }
        }

        /// <summary>
        /// Xu ly thong tin treatment
        /// Nghiep vu: cap nhat ICD trong treatment hoac xu ly ket thuc ho so dieu tri
        /// </summary>
        /// <param name="data"></param>
        /// <param name="beforeUpdate"></param>
        private void ProcessTreatment(HisServiceReqExamUpdateSDO data, HIS_SERVICE_REQ beforeUpdate, bool hasChangedMainExam, List<HIS_SERVICE_REQ> oldMainExams, List<HIS_SERE_SERV> existedSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace, HIS_PROGRAM program, HIS_DEPARTMENT_TRAN lastDt, V_HIS_DEATH_CERT_BOOK deathCertBook, ref HIS_MEDI_RECORD mediRecord)
        {
            Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
            HIS_TREATMENT oldTreatment = Mapper.Map<HIS_TREATMENT>(this.recentTreatment);
            this.recentTreatment.HOSPITALIZATION_REASON = data.HospitalizationReason;
            this.recentTreatment.TREATMENT_DIRECTION = data.NextTreaIntrName;
            this.recentTreatment.TREATMENT_METHOD = data.TreatmentInstruction;
            this.recentTreatment.SUBCLINICAL_RESULT = data.Subclinical;
            this.recentTreatment.CONTRAINDICATION_IDS = IsNotNullOrEmpty(data.ContraindicationIds) ? String.Join(",", data.ContraindicationIds) : null;
            this.recentTreatment.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
            //Bo sung thong tin ICD cho treatment
            HisTreatmentUpdate.SetIcd(this.recentServiceReq, beforeUpdate, this.recentTreatment);

            //Neu co su thay doi kham chinh, thi thuc hien cap nhat lai ICD benh phu cua ho so
            if (hasChangedMainExam && IsNotNullOrEmpty(oldMainExams))
            {
                foreach (HIS_SERVICE_REQ s in oldMainExams)
                {
                    //Them thong tin benh phu theo y lenh kham chinh cu
                    HisTreatmentUpdate.AddIcd(this.recentTreatment, s.ICD_CODE, s.ICD_NAME);
                }
            }
            if (data.HospitalizeSDO != null)
            {
                this.recentTreatment.HOSPITALIZE_REASON_CODE = data.HospitalizeSDO.InHospitalizationReasonCode;
                this.recentTreatment.HOSPITALIZE_REASON_NAME = data.HospitalizeSDO.InHospitalizationReasonName;
                //Xoa Thong tin thoi gian ra vien, ket qua dieu tri, loai ra vien
                this.recentTreatment.OUT_DATE = null;
                this.recentTreatment.OUT_TIME = null;
                this.recentTreatment.TREATMENT_RESULT_ID = null;
                this.recentTreatment.TREATMENT_END_TYPE_ID = null;
                //Xoa Thong tin tu vong:
                this.recentTreatment.DEATH_CAUSE_ID = null;
                this.recentTreatment.DEATH_CERT_BOOK_FIRST_ID = null;
                this.recentTreatment.DEATH_CERT_BOOK_ID = null;
                this.recentTreatment.DEATH_CERT_ISSUER_LOGINNAME = null;
                this.recentTreatment.DEATH_CERT_ISSUER_USERNAME = null;
                this.recentTreatment.DEATH_CERT_NUM = null;
                this.recentTreatment.DEATH_CERT_NUM_FIRST = null;
                this.recentTreatment.DEATH_DOCUMENT_DATE = null;
                this.recentTreatment.DEATH_DOCUMENT_NUMBER = null;
                this.recentTreatment.DEATH_DOCUMENT_PLACE = null;
                this.recentTreatment.DEATH_DOCUMENT_TYPE = null;
                this.recentTreatment.DEATH_DOCUMENT_TYPE_CODE = null;
                this.recentTreatment.DEATH_ISSUED_DATE = null;
                this.recentTreatment.DEATH_PLACE = null;
                this.recentTreatment.DEATH_STATUS = null;
                this.recentTreatment.DEATH_SYNC_FAILD_REASON = null;
                this.recentTreatment.DEATH_SYNC_RESULT_TYPE = null;
                this.recentTreatment.DEATH_SYNC_TIME = null;
                this.recentTreatment.DEATH_TIME = null;
                this.recentTreatment.DEATH_WITHIN_ID = null;
                this.recentTreatment.MAIN_CAUSE = null;
                //Xoa Thong tin chuyen di:
                this.recentTreatment.MEDI_ORG_CODE = null;
                this.recentTreatment.MEDI_ORG_NAME = null;
                this.recentTreatment.TRAN_PATI_FORM_ID = null;
                this.recentTreatment.TRAN_PATI_REASON_ID = null;
                this.recentTreatment.TRAN_PATI_TECH_ID = null;
                this.recentTreatment.PATIENT_CONDITION = null;
                this.recentTreatment.TRANSPORT_VEHICLE = null;
                this.recentTreatment.TRANSPORTER = null;
                this.recentTreatment.USED_MEDICINE = null;
                this.recentTreatment.TRAN_PATI_HOSPITAL_LOGINNAME = null;
                this.recentTreatment.TRAN_PATI_HOSPITAL_USERNAME = null;
                //Xoa Thong tin hen kham lai:
                this.recentTreatment.APPOINTMENT_CODE = null;
                this.recentTreatment.APPOINTMENT_DATE = null;
                this.recentTreatment.APPOINTMENT_DESC = null;
                this.recentTreatment.APPOINTMENT_EXAM_ROOM_IDS = null;
                this.recentTreatment.APPOINTMENT_EXAM_SERVICE_ID = null;
                this.recentTreatment.APPOINTMENT_PERIOD_ID = null;
                this.recentTreatment.APPOINTMENT_SURGERY = null;
                this.recentTreatment.APPOINTMENT_TIME = null;
                //Xoa thong tin ket thuc bo sung
                this.recentTreatment.TREATMENT_END_TYPE_EXT_ID = null;

                //Xoa cac ban ghi thong tin om nang xin ve
                HisSevereIllnessInfoFilterQuery filterIllnessInfo = new HisSevereIllnessInfoFilterQuery();
                filterIllnessInfo.TREATMENT_ID = this.recentTreatment.ID;
                List<HIS_SEVERE_ILLNESS_INFO> illnessInfos = new HisSevereIllnessInfoGet().Get(filterIllnessInfo);
                
                if (IsNotNullOrEmpty(illnessInfos))
                {
                    List<long> illnessInfoIds = illnessInfos.Select(o => o.ID).ToList();
                    HisEventsCausesDeathFilterQuery filterEventsCausesDeath = new HisEventsCausesDeathFilterQuery();
                    filterEventsCausesDeath.SEVERE_ILLNESS_INFO_IDs = illnessInfoIds;
                    List<HIS_EVENTS_CAUSES_DEATH> eventsCausesDeaths = new HisEventsCausesDeathGet().Get(filterEventsCausesDeath);

                    if ((IsNotNullOrEmpty(eventsCausesDeaths) && !this.hisEventsCausesDeathTruncate.TruncateList(eventsCausesDeaths))
                            || !this.hisSevereIllnessInfoTruncate.TruncateList(illnessInfos))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatment_CapNhatThatBai);
                        throw new Exception(String.Format("Xoa thong tin List<HIS_SEVERE_ILLNESS_INFO> cua ho so {0} that bai.", this.recentTreatment.TREATMENT_CODE) + LogUtil.TraceData("illnessInfo", illnessInfos));
                    }
                }
            }
            //Neu khong co thong tin ket thuc dieu tri thi chi thuc hien cap nhat treatment neu co su thay doi thong tin (vd: icd, huong dieu tri, ...)
            if (data.TreatmentFinishSDO == null
                && (Inventec.Common.ObjectChecker.ValueChecker.IsPrimitiveDiff<HIS_TREATMENT>(oldTreatment, this.recentTreatment)))
            {
                if (!this.hisTreatmentUpdate.Update(this.recentTreatment, oldTreatment))
                {
                    //neu that bai van cho phep tiep tuc nghiep vu, ko can rollback
                    LogSystem.Warn("Tu dong cap nhat thong tin ICD cho treatment that bai");
                }
            }
            //Neu co thong tin ket thuc dieu tri thi thuc hien xu ly ket thuc dieu tri
            //(luu y: ket thuc dieu tri nhung co cap nhat ca thong tin ICD da xu ly truoc do)
            else if (data.TreatmentFinishSDO != null)
            {
                this.recentTreatment.END_TYPE_EXT_NOTE = data.TreatmentFinishSDO.EndTypeExtNote;

                HisTreatmentFinishSDO finishSdo = data.TreatmentFinishSDO;
                finishSdo.IcdCauseCode = this.recentTreatment.ICD_CAUSE_CODE;
                finishSdo.IcdCauseName = this.recentTreatment.ICD_CAUSE_NAME;
                //Bo phan nay, vi o ban moi se sua lai cho phep nguoi dung nhap benh chinh khi ket thuc dieu tri o man hinh xu ly kham
                //Icd benh chinh lay theo icd cua xu ly kham
                //finishSdo.IcdName = data.IcdName;
                //finishSdo.IcdCode = data.IcdCode;
                if (!finishSdo.TreatmentResultId.HasValue || finishSdo.TreatmentResultId.Value <= 0)
                {
                    finishSdo.TreatmentResultId = HisTreatmentResultCFG.TREATMENT_RESULT_ID__DEFAULT_OF_EXAM;
                }

                var reqNotDeletes = new HisServiceReqGet().GetByTreatmentId(recentTreatment.ID);
                // Key xet kham phu
                if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON == HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_CLIENT)
                {
                    finishSdo.IcdSubCode = data.IcdSubCode;
                    finishSdo.IcdText = data.IcdText;
                }
                else if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON == HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_EXAM_REQS && this.recentTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    var examReqs = reqNotDeletes != null ? reqNotDeletes.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH).ToList() : null;
                    if (IsNotNullOrEmpty(examReqs))
                    {
                        List<string> listSubCode = new List<string>();
                        List<string> listText = new List<string>();

                        // Xu ly lay benh chinh va benh phu trong cac y lenh Kham voi benh chinh trong ho so hien tai
                        foreach (var req in examReqs)
                        {
                            if (req.ICD_CODE != this.recentTreatment.ICD_CODE && req.ICD_NAME != this.recentTreatment.ICD_NAME && !string.IsNullOrWhiteSpace(req.ICD_CODE) && !string.IsNullOrWhiteSpace(req.ICD_NAME))
                            {
                                listSubCode.Add(req.ICD_CODE);
                                listText.Add(req.ICD_NAME);
                            }
                            if (!string.IsNullOrWhiteSpace(req.ICD_SUB_CODE) && !string.IsNullOrWhiteSpace(req.ICD_TEXT))
                            {
                                var listKhamPhuCode = req.ICD_SUB_CODE.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var listKhamPhuText = req.ICD_TEXT.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var validCode = listKhamPhuCode.Where(o => o != this.recentTreatment.ICD_CODE).ToList();
                                var validText = listKhamPhuText.Where(o => !string.IsNullOrWhiteSpace(this.recentTreatment.ICD_NAME) && o.ToLower() != this.recentTreatment.ICD_NAME.ToLower()).ToList();
                                if (IsNotNullOrEmpty(validCode) && IsNotNullOrEmpty(validText))
                                {
                                    listSubCode.AddRange(validCode);
                                    listText.AddRange(validText);
                                }
                            }
                        }

                        if (listSubCode.Count > 0)
                        {
                            finishSdo.IcdSubCode = string.Join(";", listSubCode);
                        }

                        if (listText.Count > 0)
                        {
                            finishSdo.IcdText = string.Join(";", listText);
                        }
                    }
                }

                else if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON != HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_SUB_CODE)
                {
                    // Gan thong tin benh chinh duoc thay doi khi ket thuc dieu tri
                    if (data.TreatmentFinishSDO.IcdCode != this.recentTreatment.ICD_CODE)
                    {
                        string oldIcdCode = this.recentTreatment.ICD_CODE;
                        string oldIcdName = this.recentTreatment.ICD_NAME;
                        this.recentTreatment.ICD_CODE = data.TreatmentFinishSDO.IcdCode;
                        this.recentTreatment.ICD_NAME = data.TreatmentFinishSDO.IcdName;
                        HisTreatmentUpdate.AddIcd(this.recentTreatment, oldIcdCode, oldIcdName);
                    }

                    //icd benh phu lay theo thong tin da duoc xu ly sau ham SetIcd o tren
                    finishSdo.IcdSubCode = this.recentTreatment.ICD_SUB_CODE;
                    finishSdo.IcdText = this.recentTreatment.ICD_TEXT;
                }

                // Gan thong tin benh phu cua ho so la danh sach cac benh chinh, benh phu co trong cac y lenh khac voi benh chinh cua ho so.
                if (this.recentServiceReq.IS_MAIN_EXAM != Constant.IS_TRUE && HisTreatmentCFG.AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION == HisTreatmentCFG.AutoSetIcdWhenFinishInAdditionExamOption.BY_REQS_NOT_MAIN)
                {
                    var reqMainExam = reqNotDeletes != null ? reqNotDeletes.FirstOrDefault(o => o.IS_MAIN_EXAM == Constant.IS_TRUE) : null;
                    if (IsNotNull(reqMainExam))
                    {
                        finishSdo.IcdCode = reqMainExam.ICD_CODE;
                        finishSdo.IcdName = reqMainExam.ICD_NAME;
                        finishSdo.IcdCauseCode = reqMainExam.ICD_CAUSE_CODE;
                        finishSdo.IcdCauseName = reqMainExam.ICD_CAUSE_NAME;
                    }
                    if (IsNotNullOrEmpty(reqNotDeletes) && IsNotNull(reqMainExam))
                    {
                        List<string> listSubCode = new List<string>();
                        List<string> listText = new List<string>();

                        // Xu ly lay benh chinh va benh phu trong cac y lenh khac voi benh chinh trong ho so hien tai (da duoc gan o tren)
                        foreach (var req in reqNotDeletes)
                        {
                            if (req.ICD_CODE != reqMainExam.ICD_CODE && req.ICD_NAME != reqMainExam.ICD_NAME && !string.IsNullOrWhiteSpace(req.ICD_CODE) && !string.IsNullOrWhiteSpace(req.ICD_NAME))
                            {
                                listSubCode.Add(req.ICD_CODE);
                                listText.Add(req.ICD_NAME);
                            }
                            if (!string.IsNullOrWhiteSpace(req.ICD_SUB_CODE) && !string.IsNullOrWhiteSpace(req.ICD_TEXT))
                            {
                                var listKhamPhuCode = req.ICD_SUB_CODE.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var listKhamPhuText = req.ICD_TEXT.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var validCode = listKhamPhuCode.Where(o => o != reqMainExam.ICD_CODE).ToList();
                                var validText = listKhamPhuText.Where(o => !string.IsNullOrWhiteSpace(reqMainExam.ICD_NAME) && o.ToLower() != reqMainExam.ICD_NAME.ToLower()).ToList();
                                if (IsNotNullOrEmpty(validCode) && IsNotNullOrEmpty(validText))
                                {
                                    listSubCode.AddRange(validCode);
                                    listText.AddRange(validText);
                                }
                            }
                        }

                        if (listSubCode.Count > 0)
                        {
                            finishSdo.IcdSubCode = string.Join(";", listSubCode);
                        }

                        if (listText.Count > 0)
                        {
                            finishSdo.IcdText = string.Join(";", listText);
                        }
                    }
                }
                else if (this.recentServiceReq.IS_MAIN_EXAM != Constant.IS_TRUE
                    && HisTreatmentCFG.AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION == HisTreatmentCFG.AutoSetIcdWhenFinishInAdditionExamOption.BY_EXAM_REQS_NOT_MAIN
                    && this.recentTreatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    var reqMainExam = reqNotDeletes != null ? reqNotDeletes.FirstOrDefault(o => o.IS_MAIN_EXAM == Constant.IS_TRUE) : null;
                    var examReqs = reqNotDeletes != null ? reqNotDeletes.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH).ToList() : null;
                    if (IsNotNull(reqMainExam))
                    {
                        finishSdo.IcdCode = reqMainExam.ICD_CODE;
                        finishSdo.IcdName = reqMainExam.ICD_NAME;
                        finishSdo.IcdCauseCode = reqMainExam.ICD_CAUSE_CODE;
                        finishSdo.IcdCauseName = reqMainExam.ICD_CAUSE_NAME;
                    }
                    if (IsNotNullOrEmpty(examReqs) && IsNotNull(reqMainExam))
                    {
                        List<string> listSubCode = new List<string>();
                        List<string> listText = new List<string>();

                        // Xu ly lay benh chinh va benh phu trong cac y lenh khac voi benh chinh trong ho so hien tai (da duoc gan o tren)
                        foreach (var req in examReqs)
                        {
                            if (req.ICD_CODE != reqMainExam.ICD_CODE && req.ICD_NAME != reqMainExam.ICD_NAME && !string.IsNullOrWhiteSpace(req.ICD_CODE) && !string.IsNullOrWhiteSpace(req.ICD_NAME))
                            {
                                listSubCode.Add(req.ICD_CODE);
                                listText.Add(req.ICD_NAME);
                            }
                            if (!string.IsNullOrWhiteSpace(req.ICD_SUB_CODE) && !string.IsNullOrWhiteSpace(req.ICD_TEXT))
                            {
                                var listKhamPhuCode = req.ICD_SUB_CODE.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var listKhamPhuText = req.ICD_TEXT.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var validCode = listKhamPhuCode.Where(o => o != reqMainExam.ICD_CODE).ToList();
                                var validText = listKhamPhuText.Where(o => !string.IsNullOrWhiteSpace(reqMainExam.ICD_NAME) && o.ToLower() != reqMainExam.ICD_NAME.ToLower()).ToList();
                                if (IsNotNullOrEmpty(validCode) && IsNotNullOrEmpty(validText))
                                {
                                    listSubCode.AddRange(validCode);
                                    listText.AddRange(validText);
                                }
                            }
                        }

                        if (listSubCode.Count > 0)
                        {
                            finishSdo.IcdSubCode = string.Join(";", listSubCode);
                        }

                        if (listText.Count > 0)
                        {
                            finishSdo.IcdText = string.Join(";", listText);
                        }
                    }
                }
                else if (this.recentServiceReq.IS_MAIN_EXAM != Constant.IS_TRUE
                    && HisTreatmentCFG.AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION != HisTreatmentCFG.AutoSetIcdWhenFinishInAdditionExamOption.BY_REQS_NOT_MAIN
                    && HisTreatmentCFG.AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION != HisTreatmentCFG.AutoSetIcdWhenFinishInAdditionExamOption.BY_EXAM_REQS_NOT_MAIN)
                {
                    finishSdo.IcdSubCode = data.IcdSubCode;
                    finishSdo.IcdText = data.IcdText;
                }

                if (!this.hisTreatmentFinish.FinishWithoutValidate(finishSdo, this.recentTreatment, oldTreatment, existedSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref this.treatmentFinishResult, ref mediRecord))
                {
                    //ket thuc that bai, can rollback
                    throw new Exception();
                }
            }
        }

        /// <summary>
        /// Xu ly nghiep vu nhap vien
        /// </summary>
        /// <param name="data"></param>
        private void ProcessHopitalize(HisServiceReqExamUpdateSDO data)
        {
            //Neu co thong tin nhap vien thi thuc hien xu tri nhap vien
            if (data.HospitalizeSDO != null)
            {
                data.HospitalizeSDO.TreatmentId = this.recentTreatment.ID;
                //icd benh phu lay theo thong tin da duoc xu ly sau ham SetIcd o tren
                data.HospitalizeSDO.IcdSubCode = this.recentTreatment.ICD_SUB_CODE;
                data.HospitalizeSDO.IcdText = this.recentTreatment.ICD_TEXT;
                if (!this.hisDepartmentTranHospitalize.Create(data.HospitalizeSDO, data.Id, ref this.hospitalizeResult))
                {
                    //ket thuc that bai, can rollback
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                try
                {
                    //tao thread gui he thong dong bo xml
                    System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew((object obj) => new MOS.MANAGER.HisTreatment.Lock.HisTreatmentLockSendDataXml().Run((HIS_TREATMENT)obj), this.recentTreatment);
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
            }
        }

        /// <summary>
        /// Xu ly nghiep vu kham them
        /// </summary>
        /// <param name="data"></param>
        private void ProcessAdditionExam(HisServiceReqExamUpdateSDO data, HIS_SERVICE_REQ currentServiceReq, ref HIS_TRANSACTION transaction, ref HIS_SERE_SERV_DEPOSIT sereServDeposit, HIS_TREATMENT treatment)
        {
            //Neu co thong tin ket thuc dieu tri thi thuc hien xu ly ket thuc
            if (data.ExamAdditionSDO != null)
            {
                HIS_SERE_SERV newSereServ = null;
                if (!this.hisServiceReqExamAddition.Run(this.recentTreatment, currentServiceReq, data.ExamAdditionSDO, ref newSereServ, ref this.additionExamResult))
                {
                    //ket thuc that bai, can rollback
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }

                //Truy van lai de lay so tien trong truong Virtual nham xu ly trong ham deposit
                if (data.ExamAdditionSDO.AdditionServiceId.HasValue)
                {
                    newSereServ = new HisSereServGet().GetById(newSereServ.ID);
                    new DepositProcessor(param).Run(data.RequestRoomId, this.recentTreatment, newSereServ, ref transaction, ref sereServDeposit);
                }
            }
        }

        private void ProcessServiceReq(HisServiceReqExamUpdateSDO data, HIS_SERVICE_REQ beforeUpdate, WorkPlaceSDO workPlace)
        {
            string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

            //cap nhat thong tin xu ly kham
            this.recentServiceReq.ICD_TEXT = HisIcdUtil.RemoveDuplicateIcd(data.IcdText);
            this.recentServiceReq.ICD_SUB_CODE = CommonUtil.ToUpper(HisIcdUtil.RemoveDuplicateIcd(data.IcdSubCode));
            this.recentServiceReq.NEXT_TREAT_INTR_CODE = data.NextTreaIntrCode;
            this.recentServiceReq.NEXT_TREATMENT_INSTRUCTION = data.NextTreaIntrName;
            this.recentServiceReq.ICD_CODE = CommonUtil.ToUpper(data.IcdCode);
            this.recentServiceReq.ICD_CAUSE_CODE = CommonUtil.ToUpper(data.IcdCauseCode);
            this.recentServiceReq.ICD_CAUSE_NAME = data.IcdCauseName;
            this.recentServiceReq.ICD_NAME = data.IcdName;
            this.recentServiceReq.SICK_DAY = data.SickDay;
            this.recentServiceReq.HOSPITALIZATION_REASON = data.HospitalizationReason;
            this.recentServiceReq.FULL_EXAM = data.FullExam;
            if ((this.recentServiceReq.PART_EXAM ?? "") != (data.PartExam ?? ""))
            {
                this.recentServiceReq.PART_EXAM = data.PartExam;
                this.recentServiceReq.PAEX_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_CIRCULATION ?? "") != (data.PartExamCirculation ?? ""))
            {
                this.recentServiceReq.PART_EXAM_CIRCULATION = data.PartExamCirculation;
                this.recentServiceReq.PAEX_CIRC_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_RESPIRATORY ?? "") != (data.PartExamRespiratory ?? ""))
            {
                this.recentServiceReq.PART_EXAM_RESPIRATORY = data.PartExamRespiratory;
                this.recentServiceReq.PAEX_RESP_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_DIGESTION ?? "") != (data.PartExamDigestion ?? ""))
            {
                this.recentServiceReq.PART_EXAM_DIGESTION = data.PartExamDigestion;
                this.recentServiceReq.PAEX_DIGE_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_KIDNEY_UROLOGY ?? "") != (data.PartExamKidneyUrology ?? ""))
            {
                this.recentServiceReq.PART_EXAM_KIDNEY_UROLOGY = data.PartExamKidneyUrology;
                this.recentServiceReq.PAEX_KIDN_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_NEUROLOGICAL ?? "") != (data.PartExamNeurological ?? ""))
            {
                this.recentServiceReq.PART_EXAM_NEUROLOGICAL = data.PartExamNeurological;
                this.recentServiceReq.PAEX_NEUR_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_MUSCLE_BONE ?? "") != (data.PartExamMuscleBone ?? ""))
            {
                this.recentServiceReq.PART_EXAM_MUSCLE_BONE = data.PartExamMuscleBone;
                this.recentServiceReq.PAEX_MUSC_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_EAR ?? "") != (data.PartExamEar ?? "")
                || (this.recentServiceReq.PART_EXAM_EAR_LEFT_NORMAL ?? "") != (data.PartExamEarLeftNormal ?? "")
                || (this.recentServiceReq.PART_EXAM_EAR_LEFT_WHISPER ?? "") != (data.PartExamEarLeftWhisper ?? "")
                || (this.recentServiceReq.PART_EXAM_EAR_RIGHT_NORMAL ?? "") != (data.PartExamEarRightNormal ?? "")
                || (this.recentServiceReq.PART_EXAM_EAR_RIGHT_WHISPER ?? "") != (data.PartExamEarRightWhisper ?? "")
                || (this.recentServiceReq.PART_EXAM_NOSE ?? "") != (data.PartExamNose ?? "")
                || (this.recentServiceReq.PART_EXAM_THROAT ?? "") != (data.PartExamThroat ?? "")
                )
            {
                this.recentServiceReq.PART_EXAM_EAR = data.PartExamEar;
                this.recentServiceReq.PART_EXAM_EAR_LEFT_NORMAL = data.PartExamEarLeftNormal;
                this.recentServiceReq.PART_EXAM_EAR_LEFT_WHISPER = data.PartExamEarLeftWhisper;
                this.recentServiceReq.PART_EXAM_EAR_RIGHT_NORMAL = data.PartExamEarRightNormal;
                this.recentServiceReq.PART_EXAM_EAR_RIGHT_WHISPER = data.PartExamEarRightWhisper;
                this.recentServiceReq.PART_EXAM_NOSE = data.PartExamNose;
                this.recentServiceReq.PART_EXAM_THROAT = data.PartExamThroat;
                this.recentServiceReq.PAEX_ENT_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_STOMATOLOGY ?? "") != (data.PartExamStomatology ?? "")
                || (this.recentServiceReq.PART_EXAM_UPPER_JAW ?? "") != (data.PartExamUpperJaw ?? "")
                || (this.recentServiceReq.PART_EXAM_LOWER_JAW ?? "") != (data.PartExamLowerJaw ?? "")
                )
            {
                this.recentServiceReq.PART_EXAM_STOMATOLOGY = data.PartExamStomatology;
                this.recentServiceReq.PART_EXAM_UPPER_JAW = data.PartExamUpperJaw;
                this.recentServiceReq.PART_EXAM_LOWER_JAW = data.PartExamLowerJaw;
                this.recentServiceReq.PAEX_STOM_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_EYE ?? "") != (data.PartExamEye ?? "")
                || (this.recentServiceReq.PART_EXAM_EYESIGHT_GLASS_LEFT ?? "") != (data.PartExamEyeSightGlassLeft ?? "")
                || (this.recentServiceReq.PART_EXAM_EYESIGHT_GLASS_RIGHT ?? "") != (data.PartExamEyeSightGlassRight ?? "")
                || (this.recentServiceReq.PART_EXAM_EYESIGHT_LEFT ?? "") != (data.PartExamEyeSightLeft ?? "")
                || (this.recentServiceReq.PART_EXAM_EYESIGHT_RIGHT ?? "") != (data.PartExamEyeSightRight ?? "")
                || this.recentServiceReq.PART_EXAM_EYE_BLIND_COLOR != data.PartExamEyeBlindColor
                || (this.recentServiceReq.PART_EXAM_EYE_TENSION_LEFT ?? "") != (data.PartExamEyeTensionLeft ?? "")
                || (this.recentServiceReq.PART_EXAM_EYE_TENSION_RIGHT ?? "") != (data.PartExamEyeTensionRight ?? "")
                || this.recentServiceReq.PART_EXAM_HORIZONTAL_SIGHT != data.PartExamHorizontalSight
                || this.recentServiceReq.PART_EXAM_VERTICAL_SIGHT != data.PartExamVerticalSight
                )
            {
                this.recentServiceReq.PART_EXAM_EYE = data.PartExamEye;
                this.recentServiceReq.PART_EXAM_EYESIGHT_GLASS_LEFT = data.PartExamEyeSightGlassLeft;
                this.recentServiceReq.PART_EXAM_EYESIGHT_GLASS_RIGHT = data.PartExamEyeSightGlassRight;
                this.recentServiceReq.PART_EXAM_EYESIGHT_LEFT = data.PartExamEyeSightLeft;
                this.recentServiceReq.PART_EXAM_EYESIGHT_RIGHT = data.PartExamEyeSightRight;
                this.recentServiceReq.PART_EXAM_EYE_BLIND_COLOR = data.PartExamEyeBlindColor;
                this.recentServiceReq.PART_EXAM_EYE_TENSION_LEFT = data.PartExamEyeTensionLeft;
                this.recentServiceReq.PART_EXAM_EYE_TENSION_RIGHT = data.PartExamEyeTensionRight;
                this.recentServiceReq.PART_EXAM_HORIZONTAL_SIGHT = data.PartExamHorizontalSight;
                this.recentServiceReq.PART_EXAM_VERTICAL_SIGHT = data.PartExamVerticalSight;
                this.recentServiceReq.PAEX_EYE_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_OEND ?? "") != (data.PartExamOend ?? ""))
            {
                this.recentServiceReq.PART_EXAM_OEND = data.PartExamOend;
                this.recentServiceReq.PAEX_OEND_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_MENTAL ?? "") != (data.PartExamMental ?? ""))
            {
                this.recentServiceReq.PART_EXAM_MENTAL = data.PartExamMental;
                this.recentServiceReq.PAEX_MENT_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_NUTRITION ?? "") != (data.PartExamNutrition ?? ""))
            {
                this.recentServiceReq.PART_EXAM_NUTRITION = data.PartExamNutrition;
                this.recentServiceReq.PAEX_NUTR_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_MOTION ?? "") != (data.PartExamMotion ?? ""))
            {
                this.recentServiceReq.PART_EXAM_MOTION = data.PartExamMotion;
                this.recentServiceReq.PAEX_MOTI_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_OBSTETRIC ?? "") != (data.PartExamObstetric ?? ""))
            {
                this.recentServiceReq.PART_EXAM_OBSTETRIC = data.PartExamObstetric;
                this.recentServiceReq.PAEX_OBST_LOGINNAME = loginName;
            }
            if ((this.recentServiceReq.PART_EXAM_DERMATOLOGY ?? "") != (data.PartExamDermatology ?? ""))
            {
                this.recentServiceReq.PART_EXAM_DERMATOLOGY = data.PartExamDermatology;
                this.recentServiceReq.PAEX_DERM_LOGINNAME = loginName;
            }
            this.recentServiceReq.PART_EXAM_ENT = data.PartExamEnt;
            this.recentServiceReq.PATHOLOGICAL_HISTORY = data.PathologicalHistory;
            this.recentServiceReq.PATHOLOGICAL_HISTORY_FAMILY = data.PathologicalHistoryFamily;
            this.recentServiceReq.PATHOLOGICAL_PROCESS = data.PathologicalProcess;
            this.recentServiceReq.NOTE = data.Note;
            this.recentServiceReq.SUBCLINICAL = data.Subclinical;
            this.recentServiceReq.TREATMENT_INSTRUCTION = data.TreatmentInstruction;
            this.recentServiceReq.DHST_ID = this.recentDhst != null ? new Nullable<long>(this.recentDhst.ID) : null;
            this.recentServiceReq.HEALTH_EXAM_RANK_ID = data.HealthExamRankId;
            this.recentServiceReq.PROVISIONAL_DIAGNOSIS = data.ProvisionalDiagnosis;
            this.recentServiceReq.FINISH_TIME = data.FinishTime;
            this.recentServiceReq.PATIENT_CASE_ID = data.PatientCaseId;
            this.recentServiceReq.EXE_DESK_ID = workPlace.DeskId;
            this.recentServiceReq.ADVISE = data.Advise;
            this.recentServiceReq.CONCLUSION = data.Conclusion;

            if (data.ExamAdditionSDO != null)
            {
                this.recentServiceReq.EXAM_END_TYPE = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ.EXAM_END_TYPE__KHAM_THEM;
                this.recentServiceReq.EXAM_END_TIME = Inventec.Common.DateTime.Get.Now().Value;

                if (data.AppointmentTime.HasValue)
                {
                    this.recentServiceReq.APPOINTMENT_TIME = data.AppointmentTime;
                    this.recentServiceReq.APPOINTMENT_CODE = this.recentServiceReq.TDL_TREATMENT_CODE;
                    this.recentServiceReq.APPOINTMENT_DESC = data.AppointmentDesc;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = data.AppointmentTime - (data.AppointmentTime % 1000000);
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = data.AppointmentExamRoomId;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = data.AppointmentExamServiceId;
                }
                else
                {
                    this.recentServiceReq.APPOINTMENT_TIME = null;
                    this.recentServiceReq.APPOINTMENT_CODE = null;
                    this.recentServiceReq.APPOINTMENT_DESC = null;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = null;
                }
            }
            else if (data.HospitalizeSDO != null)
            {
                this.recentServiceReq.EXAM_END_TYPE = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ.EXAM_END_TYPE__NHAP_VIEN;
                this.recentServiceReq.EXAM_END_TIME = Inventec.Common.DateTime.Get.Now().Value;
                if (data.AppointmentTime.HasValue)
                {
                    this.recentServiceReq.APPOINTMENT_TIME = data.AppointmentTime;
                    this.recentServiceReq.APPOINTMENT_CODE = this.recentServiceReq.TDL_TREATMENT_CODE;
                    this.recentServiceReq.APPOINTMENT_DESC = data.AppointmentDesc;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = data.AppointmentTime - (data.AppointmentTime % 1000000);
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = data.AppointmentExamRoomId;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = data.AppointmentExamServiceId;
                }
                else
                {
                    this.recentServiceReq.APPOINTMENT_TIME = null;
                    this.recentServiceReq.APPOINTMENT_CODE = null;
                    this.recentServiceReq.APPOINTMENT_DESC = null;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = null;
                }
            }
            else if (data.TreatmentFinishSDO != null)
            {
                this.recentServiceReq.EXAM_END_TYPE = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ.EXAM_END_TYPE__KET_THUC_DT;
                this.recentServiceReq.EXAM_TREATMENT_END_TYPE_ID = data.TreatmentFinishSDO.TreatmentEndTypeId;
                this.recentServiceReq.EXAM_TREATMENT_RESULT_ID = data.TreatmentFinishSDO.TreatmentResultId;
                this.recentServiceReq.EXAM_END_TIME = Inventec.Common.DateTime.Get.Now().Value;
                if (data.AppointmentTime.HasValue)
                {
                    this.recentServiceReq.APPOINTMENT_TIME = data.AppointmentTime;
                    this.recentServiceReq.APPOINTMENT_CODE = this.recentServiceReq.TDL_TREATMENT_CODE;
                    this.recentServiceReq.APPOINTMENT_DESC = data.AppointmentDesc;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = data.AppointmentTime - (data.AppointmentTime % 1000000);
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = data.AppointmentExamRoomId;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = data.AppointmentExamServiceId;
                }
                else
                {
                    this.recentServiceReq.APPOINTMENT_TIME = null;
                    this.recentServiceReq.APPOINTMENT_CODE = null;
                    this.recentServiceReq.APPOINTMENT_DESC = null;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = null;
                }
            }
            else if (data.IsFinish)
            {
                this.recentServiceReq.EXAM_END_TYPE = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ.EXAM_END_TYPE__KET_THUC;
                this.recentServiceReq.EXAM_END_TIME = Inventec.Common.DateTime.Get.Now().Value;
                if (data.AppointmentTime.HasValue)
                {
                    this.recentServiceReq.APPOINTMENT_TIME = data.AppointmentTime;
                    this.recentServiceReq.APPOINTMENT_CODE = this.recentServiceReq.TDL_TREATMENT_CODE;
                    this.recentServiceReq.APPOINTMENT_DESC = data.AppointmentDesc;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = data.AppointmentTime - (data.AppointmentTime % 1000000);
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = data.AppointmentExamRoomId;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = data.AppointmentExamServiceId;
                }
                else
                {
                    this.recentServiceReq.APPOINTMENT_TIME = null;
                    this.recentServiceReq.APPOINTMENT_CODE = null;
                    this.recentServiceReq.APPOINTMENT_DESC = null;
                    this.recentServiceReq.TDL_APPOINTMENT_DATE = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_ROOM_ID = null;
                    this.recentServiceReq.APPOINTMENT_EXAM_SERVICE_ID = null;
                }
            }

            //Neu cap nhat ket thuc xu ly kham thi bo sung cac thong tin ket thuc
            if (data.IsFinish)
            {
                HisServiceReqUpdateFinish.SetFinishInfo(this.recentServiceReq, null, null);
            }

            //Da check treatment o ham prepare data, o day chi can cap nhat chu ko can check treatment
            if (!this.hisServiceReqUpdate.Update(this.recentServiceReq, beforeUpdate, false))
            {
                throw new Exception("Cap nhat du lieu service_req that bai. Rollback du lieu, ket thuc nghiep vu");
            }
        }

        private void ProcessAutoChangeMainExam(HisServiceReqExamUpdateSDO data, ref List<HIS_SERVICE_REQ> oldMainExams, ref bool hasChangedMainExam)
        {
            //Neu la xu ly nhap vien va co cau hinh tu dong cap nhat cong kham chinh voi cong kham thuc hien nhap vien,
            //va cong kham dang thuc hien ko phai la cong kham chinh thi thuc hien cap nhat lai
            if (((HisServiceReqCFG.AUTO_SET_MAIN_EXAM_WHICH_HOSPITALIZE && data.HospitalizeSDO != null) || (HisServiceReqCFG.AUTO_SET_MAIN_EXAM_WHICH_FINISH && data.TreatmentFinishSDO != null))
                && this.recentServiceReq.IS_MAIN_EXAM != Constant.IS_TRUE)
            {
                hasChangedMainExam = true;
                this.recentServiceReq.IS_MAIN_EXAM = Constant.IS_TRUE;

                //Lay cac kham chinh hien tai
                //Lay "cac" (de phong co do loi du lieu, co nhieu hon 1 kham chinh) kham chinh hien tai
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TREATMENT_ID = this.recentServiceReq.TREATMENT_ID;
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                filter.IS_MAIN_EXAM = true;
                oldMainExams = new HisServiceReqGet().Get(filter);

                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();

                //bo kham chinh doi voi cac y lenh kham chinh hien tai
                if (IsNotNullOrEmpty(oldMainExams))
                {
                    List<HIS_SERVICE_REQ> beforeUpdates = Mapper.Map<List<HIS_SERVICE_REQ>>(oldMainExams);
                    oldMainExams.ForEach(o => o.IS_MAIN_EXAM = null);

                    if (!this.hisServiceReqUpdateOldMain.UpdateList(oldMainExams, beforeUpdates))
                    {
                        throw new Exception("Cap nhat thong tin kham chinh cua y lenh that bai");
                    }
                }
            }
        }

        private void ProcessDhst(HisServiceReqExamUpdateSDO data)
        {
            if (data.HisDhst != null)
            {
                data.HisDhst.TREATMENT_ID = this.recentServiceReq.TREATMENT_ID;
                data.HisDhst.EXECUTE_ROOM_ID = data.RequestRoomId;
                if (data.HisDhst.ID > 0)
                {
                    if (!this.hisDhstUpdate.Update(data.HisDhst))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                else
                {
                    if (!this.hisDhstCreate.Create(data.HisDhst))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                this.recentDhst = data.HisDhst;
            }
        }

        private void PassResult(HIS_MEDI_RECORD mediRecord, HIS_TRANSACTION transaction, HIS_SERE_SERV_DEPOSIT sereServDeposit, ref HisServiceReqExamUpdateResultSDO resultData)
        {
            resultData = new HisServiceReqExamUpdateResultSDO();
            resultData.ServiceReq = this.recentServiceReq;
            resultData.HospitalizeResult = this.hospitalizeResult;
            resultData.TreatmentFinishResult = this.treatmentFinishResult;
            resultData.AdditionExamResult = this.additionExamResult;
            resultData.MediRecord = mediRecord;
            resultData.Transaction = transaction != null ? new HisTransactionGet().GetViewById(transaction.ID) : null;
            resultData.SereServDeposits = sereServDeposit != null ? new List<HIS_SERE_SERV_DEPOSIT>() { sereServDeposit } : null;
        }

        private void RollbackData()
        {
            this.hisPatientUpdate.RollbackData();
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.hisServiceReqUpdate.RollbackData();
            this.hisServiceReqUpdateOldMain.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
            this.hisTreatmentFinish.RollBackData();
            this.hisSevereIllnessInfoTruncate.RollbackData();
            this.hisEventsCausesDeathTruncate.RollbackData();
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(HisServiceReqExamUpdateSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                if (data.TreatmentFinishSDO != null)
                {
                    data.TreatmentFinishSDO.TreatmentFinishTime = Inventec.Common.DateTime.Get.Now().Value;
                }
            }
        }
    }
}
