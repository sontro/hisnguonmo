using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReq.Update.Finish;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisTransReq.CreateByService;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update.Finish;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Create
{
    /// <summary>
    /// Xu ly ke don thuoc ngoai tru (don thuoc phong kham hoac don thuoc ke tu tu truc)
    /// </summary>
    class HisServiceReqOutPatientPresCreate : BusinessBase
    {
        //private HIS_TREATMENT treatment;
        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();
        private List<HIS_SERE_SERV> recentSereServs;
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;
        private List<HIS_SERVICE_REQ_MATY> recentServiceReqMaties;
        private List<HIS_SERVICE_REQ_METY> recentServiceReqMeties;
        private List<HIS_SERVICE_REQ> recentServiceReqs;
        private List<HIS_EXP_MEST> recentExpMests;

        private HisSereServUpdate hisSereServUpdate;

        private HisServiceReqMatyCreate hisServiceReqMatyCreate;
        private HisServiceReqMetyCreate hisServiceReqMetyCreate;
        private HisSereServCreateSql hisSereServCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisExpMestProcessor hisExpMestProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisTreatmentFinish hisTreatmentFinish;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisServiceReqUpdate hisServiceReqUpdateOldMain;
        private AutoProcessor autoProcessor;
        private SaleExpMestProcessor saleExpMestProcessor;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal HisServiceReqOutPatientPresCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqOutPatientPresCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisServiceReqMatyCreate = new HisServiceReqMatyCreate(param);
            this.hisServiceReqMetyCreate = new HisServiceReqMetyCreate(param);
            this.hisSereServCreate = new HisSereServCreateSql(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.hisServiceReqProcessor = new HisServiceReqProcessor(param);
            this.hisTreatmentFinish = new HisTreatmentFinish(param);
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisServiceReqUpdateOldMain = new HisServiceReqUpdate(param);
            this.autoProcessor = new AutoProcessor(param);
            this.saleExpMestProcessor = new SaleExpMestProcessor(param);
        }

        internal bool Run(OutPatientPresSDO data, ref OutPatientPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqOutPatientPresCheck outPatientPresChecker = new HisServiceReqOutPatientPresCheck(param);
                HisTreatmentFinishCheck treatmentFinishChecker = new HisTreatmentFinishCheck(param);
                HisExpMestCheck expChecker = new HisExpMestCheck(param);

                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                List<long> mediStockIds = null;
                List<HIS_SERE_SERV> existedSereServs = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                HIS_DEPARTMENT_TRAN lastDt = null;
                HIS_PROGRAM program = null;
                V_HIS_DEATH_CERT_BOOK deathCertBook = null;
                HIS_SERVICE_REQ parentServiceReq = null;
                HIS_TREATMENT treatment = null;

                WorkPlaceSDO workPlace = null;
                string sessionCode = Guid.NewGuid().ToString();

                this.SetServerTime(data);

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && presChecker.IsValidData(data);
                valid = valid && presChecker.IsValidPatientType(data, data.InstructionTime, ref ptas);
                valid = valid && presChecker.IsAllowMediStock(data, ref mediStockIds);
                valid = valid && presChecker.IsAllowPrescription(data);
                valid = valid && presChecker.CheckRankPrescription(data);
                valid = valid && outPatientPresChecker.IsValidStentAmount(data);
                valid = valid && outPatientPresChecker.IsValidData(data, ref parentServiceReq);
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && presChecker.IsValidFinishTimeCls(treatment, parentServiceReq, data.InstructionTime);

                if (valid) //valid thi moi get du lieu de tranh ton hieu nang
                {
                    if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                        || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                        || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                    {
                        valid = valid && presChecker.CheckAmountPrepare(treatment.ID, data.Medicines, data.Materials, null);
                    }
                    existedSereServs = new HisSereServGet().GetByTreatmentId(data.TreatmentId);
                    if (data.TreatmentFinishSDO != null)
                    {
                        data.TreatmentFinishSDO.ServiceReqId = data.ParentServiceReqId;
                        valid = valid && treatmentFinishChecker.IsValidForFinish(data.TreatmentFinishSDO, treatment, existedSereServs, ptas, ref lastDt, ref workPlace, ref program, ref deathCertBook);
                        if (valid && data.ParentServiceReqId.HasValue && existedSereServs != null)
                        {
                            List<HIS_SERE_SERV> recentSereServs = existedSereServs.Where(o => o.SERVICE_REQ_ID == data.ParentServiceReqId.Value).ToList();
                            var startTime = parentServiceReq != null ? parentServiceReq.START_TIME : null;
                            var finishTime = data.TreatmentFinishSDO.TreatmentFinishTime;
                            valid = valid && treatmentFinishChecker.IsValidMinProcessTime(startTime, finishTime, recentSereServs);
                        }
                    }
                }

                if (valid)
                {
                    result = RunWithoutValidate(data, workPlace, sessionCode, treatment, parentServiceReq, existedSereServs, mediStockIds, ptas, lastDt, program, deathCertBook, ref resultData);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        internal bool RunWithoutValidate(OutPatientPresSDO data, WorkPlaceSDO workPlace, string sessionCode, HIS_TREATMENT treatment, HIS_SERVICE_REQ parentServiceReq, List<HIS_SERE_SERV> existedSereServs, List<long> mediStockIds, List<HIS_PATIENT_TYPE_ALTER> ptas, HIS_DEPARTMENT_TRAN lastDt, HIS_PROGRAM program, V_HIS_DEATH_CERT_BOOK deathCertBook, ref OutPatientPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                HIS_SERVICE_REQ beforeUpdateParentServiceReq = parentServiceReq != null ? Mapper.Map<HIS_SERVICE_REQ>(parentServiceReq) : null;

                HIS_SERVICE_REQ outStockServiceReq = null;

                HIS_EXP_MEST saleExpMest = null;
                List<V_HIS_MEDICINE_2> choosenMedicines = null;
                bool hasChangedMainExam = false;

                HIS_MEDI_RECORD mediRecord = null;

                List<string> sqls = new List<string>();

                if (!this.hisServiceReqProcessor.Run(data, treatment, ptas, mediStockIds, sessionCode, parentServiceReq, ref this.recentServiceReqs, ref outStockServiceReq))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                List<HIS_SERVICE_REQ> inStockServiceReqs = this.recentServiceReqs.Where(o => outStockServiceReq == null || o != outStockServiceReq).ToList();

                if (!this.hisExpMestProcessor.Run(data, inStockServiceReqs, ref this.recentExpMests))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                HisSereServPackage37 processPackage37 = new HisSereServPackage37(param, data.TreatmentId, workPlace.RoomId, workPlace.DepartmentId, existedSereServs);
                HisSereServPackageBirth processPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, existedSereServs);
                HisSereServPackagePttm processPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, existedSereServs);

                if (!this.medicineProcessor.Run(data, processPackage37, processPackageBirth, processPackagePttm, this.recentExpMests, treatment, ref this.recentExpMestMedicines, ref choosenMedicines, ref sqls))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                if (!this.materialProcessor.Run(data, processPackage37, processPackageBirth, processPackagePttm, this.recentExpMests, ref this.recentExpMestMaterials, ref sqls))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                this.ProcessSereServ(data, treatment, ptas, inStockServiceReqs, existedSereServs, choosenMedicines);
                this.ProcessServiceReqMety(data.ServiceReqMeties, data.Medicines, inStockServiceReqs, outStockServiceReq);
                this.ProcessServiceReqMaty(data.ServiceReqMaties, data.Materials, inStockServiceReqs, outStockServiceReq);

                if (!this.saleExpMestProcessor.Run(data, outStockServiceReq, ref saleExpMest, ref sqls))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                //Set TDL_TOTAL_PRICE, HAS_NOT_PRES
                HisServiceReqPresUtil.SqlUpdateExpMest(this.recentExpMests, this.recentExpMestMaterials, this.recentExpMestMedicines, ref sqls);

                //Cap nhat thong tin "is_main_exam" cua parentServiceReq
                this.ProcessAutoChangeMainExam(data, parentServiceReq, ref hasChangedMainExam);

                this.ProcessServiceReq(data, parentServiceReq, beforeUpdateParentServiceReq);

                // Neu co thay doi kham chinh thi tinh toan lai tien
                this.ProcessSereServ(treatment, hasChangedMainExam);

                this.ProcessTreatment(parentServiceReq, data, treatment, existedSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref mediRecord);

                //Xu ly sql de duoi cung de tranh rollback du lieu
                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Rollback");
                }

                this.ProcessAuto(this.recentExpMests);
                if (IsNotNullOrEmpty(this.recentSereServs))
                {
                    List<HIS_SERE_SERV> lstSereServ = new HisSereServGet().GetByIds(this.recentSereServs.Select(o => o.ID).ToList());
                    //Tao yeu cau thanh toan
                    if (IsNotNullOrEmpty(lstSereServ) && lstSereServ.Exists(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0))
                    {
                        if (!new HisTransReqCreateByService(param).Run(treatment, this.recentServiceReqs, workPlace))
                        {
                            Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("tranReqParam", param));
                        }
                    }
                }
                
               

                this.PassResult(mediRecord, ref resultData);

                HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, resultData.ServiceReqMaties, resultData.ServiceReqMeties, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_KeDon);

                if (saleExpMest != null)
                {
                    new EventLogGenerator(EventLog.Enum.HisExpMest_TaoPhieuXuat).ExpMestCode(saleExpMest.EXP_MEST_CODE).Run();
                }

                if (hasChangedMainExam)
                    HisServiceReqLog.Run(parentServiceReq, LibraryEventLog.EventLog.Enum.HisServiceReq_CapNhatKhamChinh);

                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                resultData = null;
                result = false;
            }

            return result;
        }

        private void PassResult(HIS_MEDI_RECORD mediRecord, ref OutPatientPresResultSDO resultData)
        {
            resultData = new OutPatientPresResultSDO();
            resultData.ExpMests = this.recentExpMests;
            resultData.Materials = this.recentExpMestMaterials;
            resultData.Medicines = this.recentExpMestMedicines;
            resultData.ServiceReqMaties = this.recentServiceReqMaties;
            resultData.ServiceReqMeties = this.recentServiceReqMeties;
            if (this.recentServiceReqs != null)
            {
                //Truy van lai de lay thong tin NUM_ORDER (do DB sinh) de in ra STT tren don thuoc
                resultData.ServiceReqs = new HisServiceReqGet().GetByIds(this.recentServiceReqs.Select(o => o.ID).ToList());
            }
            resultData.MediRecord = mediRecord;
            resultData.SereServs = this.recentSereServs;
        }

        private void ProcessAutoChangeMainExam(OutPatientPresSDO data, HIS_SERVICE_REQ serviceReq, ref bool hasChangedMainExam)
        {
            if (serviceReq == null)
                return;

            //Nếu công khám hiện tại không phải là công khám chính :
            //Tự động cập nhật lại công khám hiện tại về công khám chính,
            //Đồng thời cập nhật bỏ "công khám chính" của công khám chính trước đó.
            if ((HisServiceReqCFG.AUTO_SET_MAIN_EXAM_WHICH_FINISH && data.TreatmentFinishSDO != null)
                && serviceReq.IS_MAIN_EXAM != Constant.IS_TRUE)
            {
                hasChangedMainExam = true;
                serviceReq.IS_MAIN_EXAM = Constant.IS_TRUE;

                //Lay cac kham chinh hien tai
                //Lay "cac" (de phong co do loi du lieu, co nhieu hon 1 kham chinh) kham chinh hien tai
                HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                filter.TREATMENT_ID = serviceReq.TREATMENT_ID;
                filter.SERVICE_REQ_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH;
                filter.IS_MAIN_EXAM = true;
                List<HIS_SERVICE_REQ> oldMainExams = new HisServiceReqGet().Get(filter);

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

        private void ProcessServiceReq(OutPatientPresSDO data, HIS_SERVICE_REQ serviceReq, HIS_SERVICE_REQ beforeUpdate)
        {
            if (data.TreatmentFinishSDO != null)
            {
                //Neu ket thuc khi xu ly kham thi thuc hien ket thuc luon dich vu kham
                if (data.ParentServiceReqId.HasValue)
                {
                    //Neu cap nhat ket thuc xu ly kham thi bo sung cac thong tin ket thuc
                    HisServiceReqUpdateFinish.SetFinishInfo(serviceReq, null, null, data.TreatmentFinishSDO.TreatmentFinishTime);

                    //Da treatment o ham prepare data, o day chi can cap nhat chu ko can check treatment
                    if (!this.hisServiceReqUpdate.Update(serviceReq, beforeUpdate, false))
                    {
                        throw new Exception("Cap nhat du lieu service_req that bai. Rollback du lieu, ket thuc nghiep vu");
                    }
                }
            }
        }

        /// <summary>
        /// Neu co thay doi kham chinh thi tinh toan lai tien
        /// </summary>
        /// <param name="treatment"></param>
        /// <param name="hasChangedMainExam"></param>
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

        private void ProcessSereServ(OutPatientPresSDO pres, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> existedSereServs, List<V_HIS_MEDICINE_2> choosenMedicines)
        {
            if (IsNotNullOrEmpty(this.recentExpMests))
            {
                List<HIS_SERE_SERV> newSereServs = null;

                //Neu co ke thuoc trong kho thi moi tao du lieu sere_serv
                if (!new HisSereServMaker(param, treatment, pres, this.recentServiceReqs, this.recentExpMests, this.recentExpMestMedicines, this.recentExpMestMaterials, choosenMedicines).Run(ref newSereServs))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }

                if (IsNotNullOrEmpty(newSereServs))
                {
                    //Tao ID "fake" de dinh danh cac sere_serv chua co trong DB
                    long maxId = IsNotNullOrEmpty(existedSereServs) ? existedSereServs.Max(o => o.ID) : 0;
                    long maxExistedSereServId = maxId;
                    newSereServs.ForEach(o => o.ID = ++maxId);

                    //Xu ly de set thong tin ti le chi tra, doi tuong va lay thong tin thay doi
                    List<HIS_SERE_SERV> toUpdateData = new List<HIS_SERE_SERV>();
                    toUpdateData.AddRange(newSereServs);
                    if (IsNotNullOrEmpty(existedSereServs))
                    {
                        toUpdateData.AddRange(existedSereServs);
                    }

                    List<HIS_SERE_SERV> changeRecords = null;
                    List<HIS_SERE_SERV> oldOfChangeRecords = null;

                    if (!new HisSereServUpdateHein(param, treatment, ptas, false).Update(existedSereServs, toUpdateData, ref changeRecords, ref oldOfChangeRecords))
                    {
                        throw new Exception("Rollback du lieu");
                    }

                    List<HIS_SERE_SERV> toUpdates = IsNotNullOrEmpty(changeRecords) ? changeRecords.Where(o => o.ID <= maxExistedSereServId).ToList() : null;

                    if (!this.hisSereServCreate.Run(newSereServs, serviceReqs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.recentSereServs = newSereServs;

                    if (IsNotNullOrEmpty(toUpdates))
                    {
                        List<HIS_SERE_SERV> olds = oldOfChangeRecords.Where(o => o.ID <= maxExistedSereServId).ToList();
                        this.beforeUpdateSereServs.AddRange(olds);//luu lai phuc vu rollback

                        //tao thread moi de update sere_serv cu~
                        Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateSereServ));
                        thread.Priority = ThreadPriority.Highest;
                        UpdateSereServThreadData threadData = new UpdateSereServThreadData();
                        threadData.SereServs = toUpdates;
                        thread.Start(threadData);
                    }
                }
            }
        }

        private void ProcessServiceReqMaty(List<PresOutStockMatySDO> serviceReqMaties, List<PresMaterialSDO> materials, List<HIS_SERVICE_REQ> inStockServiceReqs, HIS_SERVICE_REQ outStockServiceReq)
        {
            List<HIS_SERVICE_REQ_MATY> toInserts = new List<HIS_SERVICE_REQ_MATY>();

            if (IsNotNullOrEmpty(serviceReqMaties))
            {
                //Neu ko tach service_req cho don ngoai kho thi lay don trong kho dau tien
                HIS_SERVICE_REQ serviceReq = outStockServiceReq != null ? outStockServiceReq : inStockServiceReqs[0];
                List<HIS_SERVICE_REQ_MATY> outMaties = serviceReqMaties.Select(o => new HIS_SERVICE_REQ_MATY
                {
                    AMOUNT = o.Amount,
                    MATERIAL_TYPE_ID = o.MaterialTypeId,
                    MATERIAL_TYPE_NAME = o.MaterialTypeName,
                    NUM_ORDER = o.NumOrder,
                    TUTORIAL = o.Tutorial,
                    PRICE = o.Price,
                    SERVICE_REQ_ID = serviceReq.ID,
                    UNIT_NAME = o.UnitName,
                    TDL_TREATMENT_ID = serviceReq.TREATMENT_ID,
                    PRES_AMOUNT = o.PresAmount,
                    EXCEED_LIMIT_IN_PRES_REASON = o.ExceedLimitInPresReason,
                    EXCEED_LIMIT_IN_DAY_REASON = o.ExceedLimitInDayReason,
                }).ToList();

                toInserts.AddRange(outMaties);
            }

            if (IsNotNullOrEmpty(inStockServiceReqs))
            {
                List<HIS_SERVICE_REQ_MATY> subPresMaties = new HisServiceReqOutPatientPresUtil().MakeMaty(materials, inStockServiceReqs);

                if (IsNotNullOrEmpty(subPresMaties))
                {
                    toInserts.AddRange(subPresMaties);
                }
            }

            if (IsNotNullOrEmpty(toInserts) && !this.hisServiceReqMatyCreate.CreateList(toInserts))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentServiceReqMaties = toInserts;

        }

        private void ProcessServiceReqMety(List<PresOutStockMetySDO> serviceReqMeties, List<PresMedicineSDO> medicines, List<HIS_SERVICE_REQ> inStockServiceReqs, HIS_SERVICE_REQ outStockServiceReq)
        {
            List<HIS_SERVICE_REQ_METY> toInserts = new List<HIS_SERVICE_REQ_METY>();

            if (IsNotNullOrEmpty(serviceReqMeties))
            {
                //Neu ko tach service_req cho don ngoai kho thi lay don trong kho dau tien
                HIS_SERVICE_REQ serviceReq = outStockServiceReq != null ? outStockServiceReq : inStockServiceReqs[0];

                List<HIS_SERVICE_REQ_METY> outMeties = new List<HIS_SERVICE_REQ_METY>();
                foreach (var sdo in serviceReqMeties)
                {
                    HIS_SERVICE_REQ_METY srMety = new HIS_SERVICE_REQ_METY();
                    srMety.AMOUNT = sdo.Amount;
                    srMety.MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                    srMety.MEDICINE_TYPE_NAME = sdo.MedicineTypeName;
                    srMety.NUM_ORDER = sdo.NumOrder;
                    srMety.PRICE = sdo.Price;
                    srMety.SERVICE_REQ_ID = serviceReq.ID;
                    srMety.UNIT_NAME = sdo.UnitName;
                    srMety.MEDICINE_USE_FORM_ID = sdo.MedicineUseFormId;
                    srMety.SPEED = sdo.Speed;
                    srMety.USE_TIME_TO = sdo.UseTimeTo;
                    srMety.TUTORIAL = sdo.Tutorial;
                    srMety.NOON = sdo.Noon;
                    srMety.AFTERNOON = sdo.Afternoon;
                    srMety.EVENING = sdo.Evening;
                    srMety.MORNING = sdo.Morning;
                    srMety.HTU_ID = sdo.HtuId;
                    srMety.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                    srMety.PRES_AMOUNT = sdo.PresAmount;
                    srMety.PREVIOUS_USING_COUNT = sdo.PreviousUsingCount;
                    srMety.EXCEED_LIMIT_IN_PRES_REASON = sdo.ExceedLimitInPresReason;
                    srMety.EXCEED_LIMIT_IN_DAY_REASON = sdo.ExceedLimitInDayReason;
                    srMety.ODD_PRES_REASON = sdo.OddPresReason;
                    if (IsNotNullOrEmpty(sdo.MedicineInfoSdos))
                    {
                        foreach (var item in sdo.MedicineInfoSdos)
                        {
                            if (!item.IsNoPrescription && item.IntructionTime == serviceReq.INTRUCTION_TIME)
                            {
                                srMety.OVER_KIDNEY_REASON = item.OverKidneyReason;
                                srMety.OVER_RESULT_TEST_REASON = item.OverResultTestReason;
                            }
                        }
                    }
                    outMeties.Add(srMety);
                }
                toInserts.AddRange(outMeties);
            }

            if (IsNotNullOrEmpty(inStockServiceReqs))
            {
                List<HIS_SERVICE_REQ_METY> subPresMeties = new HisServiceReqOutPatientPresUtil().MakeMety(medicines, inStockServiceReqs);

                if (IsNotNullOrEmpty(subPresMeties))
                {
                    toInserts.AddRange(subPresMeties);
                }
            }

            if (IsNotNullOrEmpty(toInserts) && !this.hisServiceReqMetyCreate.CreateList(toInserts))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentServiceReqMeties = toInserts;
        }

        private void ProcessTreatment(HIS_SERVICE_REQ parent, OutPatientPresSDO data, HIS_TREATMENT treatment, List<HIS_SERE_SERV> existedSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace, HIS_PROGRAM program, HIS_DEPARTMENT_TRAN lastDt, V_HIS_DEATH_CERT_BOOK deathCertBook, ref HIS_MEDI_RECORD mediRecord)
        {
            //Neu co thong tin ket thuc dieu tri thi thuc hien xu ly ket thuc dieu tri
            //(luu y: ket thuc dieu tri nhung co cap nhat ca thong tin ICD da xu ly truoc do)
            if (data.TreatmentFinishSDO != null)
            {
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT oldTreatment = Mapper.Map<HIS_TREATMENT>(treatment);

                HisTreatmentFinishSDO treatmentFinishSDO = data.TreatmentFinishSDO;
                //Icd benh chinh lay theo icd cua ke don
                treatmentFinishSDO.IcdName = data.IcdName;
                treatmentFinishSDO.IcdCode = data.IcdCode;

                var reqNotDeletes = new HisServiceReqGet().GetByTreatmentId(treatment.ID);
                // Key xet kham phu
                if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON == HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_CLIENT)
                {
                    treatmentFinishSDO.IcdSubCode = data.IcdSubCode;
                    treatmentFinishSDO.IcdText = data.IcdText;
                }
                else if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON == HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_EXAM_REQS && treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
                {
                    var examReqs = reqNotDeletes != null ? reqNotDeletes.Where(o => o.SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__KH).ToList() : null;
                    if (IsNotNullOrEmpty(examReqs))
                    {
                        List<string> listSubCode = new List<string>();
                        List<string> listText = new List<string>();

                        // Xu ly lay benh chinh va benh phu trong cac y lenh Kham voi benh chinh trong ho so hien tai
                        foreach (var req in examReqs)
                        {
                            if (req.ICD_CODE != treatmentFinishSDO.IcdCode && req.ICD_NAME != treatmentFinishSDO.IcdName && !string.IsNullOrWhiteSpace(req.ICD_CODE) && !string.IsNullOrWhiteSpace(req.ICD_NAME))
                            {
                                listSubCode.Add(req.ICD_CODE);
                                listText.Add(req.ICD_NAME);
                            }
                            if (!string.IsNullOrWhiteSpace(req.ICD_SUB_CODE) && !string.IsNullOrWhiteSpace(req.ICD_TEXT))
                            {
                                var listKhamPhuCode = req.ICD_SUB_CODE.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var listKhamPhuText = req.ICD_TEXT.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var validCode = listKhamPhuCode.Where(o => o != treatmentFinishSDO.IcdCode).ToList();
                                var validText = listKhamPhuText.Where(o => !string.IsNullOrWhiteSpace(treatmentFinishSDO.IcdName) && o.ToLower() != treatmentFinishSDO.IcdName.ToLower()).ToList();
                                if (IsNotNullOrEmpty(validCode) && IsNotNullOrEmpty(validText))
                                {
                                    listSubCode.AddRange(validCode);
                                    listText.AddRange(validText);
                                }
                            }
                        }

                        if (listSubCode.Count > 0)
                        {
                            treatmentFinishSDO.IcdSubCode = string.Join(";", listSubCode);
                        }

                        if (listText.Count > 0)
                        {
                            treatmentFinishSDO.IcdText = string.Join(";", listText);
                        }
                    }
                }

                else if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON == HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_SUB_CODE)
                {
                    treatmentFinishSDO.IcdSubCode = data.IcdSubCode;
                    treatmentFinishSDO.IcdText = data.IcdText;
                }
                else
                {
                    if (IsNotNullOrEmpty(reqNotDeletes))
                    {
                        List<string> listSubCode = new List<string>();
                        List<string> listText = new List<string>();

                        // Xu ly lay benh chinh va benh phu trong cac y lenh Kham voi benh chinh trong ho so hien tai
                        foreach (var req in reqNotDeletes)
                        {
                            if (req.ICD_CODE != treatmentFinishSDO.IcdCode && req.ICD_NAME != treatmentFinishSDO.IcdName && !string.IsNullOrWhiteSpace(req.ICD_CODE) && !string.IsNullOrWhiteSpace(req.ICD_NAME))
                            {
                                listSubCode.Add(req.ICD_CODE);
                                listText.Add(req.ICD_NAME);
                            }
                            if (!string.IsNullOrWhiteSpace(req.ICD_SUB_CODE) && !string.IsNullOrWhiteSpace(req.ICD_TEXT))
                            {
                                var listKhamPhuCode = req.ICD_SUB_CODE.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var listKhamPhuText = req.ICD_TEXT.Split(';').Where(o => !string.IsNullOrWhiteSpace(o)).ToList();
                                var validCode = listKhamPhuCode.Where(o => o != treatmentFinishSDO.IcdCode).ToList();
                                var validText = listKhamPhuText.Where(o => !string.IsNullOrWhiteSpace(treatmentFinishSDO.IcdName) && o.ToLower() != treatmentFinishSDO.IcdName.ToLower()).ToList();
                                if (IsNotNullOrEmpty(validCode) && IsNotNullOrEmpty(validText))
                                {
                                    listSubCode.AddRange(validCode);
                                    listText.AddRange(validText);
                                }
                            }
                        }

                        if (listSubCode.Count > 0)
                        {
                            treatmentFinishSDO.IcdSubCode = string.Join(";", listSubCode);
                        }

                        if (listText.Count > 0)
                        {
                            treatmentFinishSDO.IcdText = string.Join(";", listText);
                        }
                    }
                }

                treatmentFinishSDO.TreatmentMethod = parent != null ? parent.TREATMENT_INSTRUCTION : null;
                if (!treatmentFinishSDO.TreatmentResultId.HasValue || treatmentFinishSDO.TreatmentResultId.Value <= 0)
                {
                    treatmentFinishSDO.TreatmentResultId = HisTreatmentResultCFG.TREATMENT_RESULT_ID__DEFAULT_OF_EXAM;
                }

                List<HIS_SERE_SERV> allSereServs = new List<HIS_SERE_SERV>();
                if (IsNotNullOrEmpty(existedSereServs))
                {
                    allSereServs.AddRange(existedSereServs);
                }
                if (IsNotNullOrEmpty(this.recentSereServs))
                {
                    allSereServs.AddRange(this.recentSereServs);
                }

                if (!this.hisTreatmentFinish.FinishWithoutValidate(treatmentFinishSDO, treatment, oldTreatment, allSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref treatment, ref mediRecord))
                {
                    //ket thuc that bai, can rollback
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
            }
            else
            {
                //Neu ko co thong tin ket thuc dieu tri thi
                //tao tien trinh moi de update thong tin treatment
                this.UpdateTreatmentThreadInit(treatment, data);
            }
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(OutPatientPresSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.InstructionTime = now;

                if (data.TreatmentFinishSDO != null)
                {
                    data.TreatmentFinishSDO.TreatmentFinishTime = now;
                }
            }
        }

        private void ProcessAuto(List<HIS_EXP_MEST> hisExpMests)
        {
            try
            {
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    this.autoProcessor.Run(hisExpMests);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void UpdateTreatmentThreadInit(HIS_TREATMENT treatment, OutPatientPresSDO data)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateTreatment));
                thread.Priority = ThreadPriority.Highest;
                UpdateIcdTreatmentThreadData threadData = new UpdateIcdTreatmentThreadData();
                threadData.Treatment = treatment;
                threadData.Treatment = treatment;
                threadData.IcdCode = data.IcdCode;
                threadData.IcdName = data.IcdName;
                threadData.IcdSubCode = data.IcdSubCode;
                threadData.IcdText = data.IcdText;
                thread.Start(threadData);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat treatment", ex);
            }
        }

        private void ThreadProcessUpdateSereServ(object threadData)
        {
            try
            {
                UpdateSereServThreadData td = (UpdateSereServThreadData)threadData;
                List<HIS_SERE_SERV> sereServs = td.SereServs;

                if (!this.hisSereServUpdate.UpdateRaw(sereServs))
                {
                    LogSystem.Error("Cap nhat lai ti le BHYT cua sere_serv that bai");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        //Tien trinh xu ly de tao thong tin duyet ho so BHYT
        private void ThreadProcessUpdateTreatment(object threadData)
        {
            try
            {
                UpdateIcdTreatmentThreadData td = (UpdateIcdTreatmentThreadData)threadData;
                HIS_TREATMENT treatment = td.Treatment;

                bool hasUpdate = false;
                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT beforeUpdate = Mapper.Map<HIS_TREATMENT>(treatment);//clone phuc vu rollback

                //Neu treatment chua co thong tin benh chinh thi cap nhat thong tin benh chinh cho treatment
                if (string.IsNullOrWhiteSpace(treatment.ICD_CODE) && string.IsNullOrWhiteSpace(treatment.ICD_NAME))
                {
                    if (!string.IsNullOrWhiteSpace(td.IcdCode) || !string.IsNullOrWhiteSpace(td.IcdName))
                    {
                        treatment.ICD_CODE = td.IcdCode;
                        treatment.ICD_NAME = td.IcdName;
                        hasUpdate = true;
                    }
                }

                if (HisTreatmentUpdate.AddIcd(treatment, td.IcdSubCode, td.IcdText))
                {
                    hasUpdate = true;
                }

                //Neu co su thay doi thong tin ICD thi moi thuc hien update treatment
                if (hasUpdate && !this.hisTreatmentUpdate.Update(treatment, beforeUpdate))
                {
                    LogSystem.Warn("Cap nhat thong tin ICD cho treatment dua vao thong tin ICD cua dich vu kham that bai.");
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        internal void RollbackData()
        {
            this.hisTreatmentFinish.RollBackData();
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.hisServiceReqUpdate.RollbackData();
            this.hisServiceReqUpdateOldMain.RollbackData();
            this.saleExpMestProcessor.Rollback();
            this.hisServiceReqMatyCreate.RollbackData();
            this.hisServiceReqMetyCreate.RollbackData();
            this.hisSereServUpdate.RollbackData();
            this.hisSereServCreate.Rollback();
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
            this.hisServiceReqProcessor.Rollback();
        }
    }
}
