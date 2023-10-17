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

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update
{
    /// <summary>
    /// Xu ly ke don thuoc ngoai tru (don thuoc phong kham hoac don thuoc ke tu tu truc)
    /// </summary>
    class HisServiceReqOutPatientPresUpdate : BusinessBase
    {
        private HIS_TREATMENT treatment;
        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();
        private List<HIS_SERE_SERV> recentSereServs;
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;
        private List<HIS_SERVICE_REQ_MATY> recentServiceReqMaties;
        private List<HIS_SERVICE_REQ_METY> recentServiceReqMeties;
        private HIS_SERVICE_REQ recentServiceReq;
        private HIS_EXP_MEST recentExpMest;
        private HIS_DEPARTMENT department;

        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisSereServProcessor hisSereServProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private HisServiceReqMatyProcessor hisServiceReqMatyProcessor;
        private HisServiceReqMetyProcessor hisServiceReqMetyProcessor;
        private HisExpMestProcessor hisExpMestProcessor;
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisTreatmentFinish hisTreatmentFinish;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private AutoProcessor autoProcessor;
        private SaleExpMestProcessor saleExpMestProcessor;

        internal HisServiceReqOutPatientPresUpdate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqOutPatientPresUpdate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.hisServiceReqProcessor = new HisServiceReqProcessor(param);
            this.hisServiceReqMatyProcessor = new HisServiceReqMatyProcessor(param);
            this.hisServiceReqMetyProcessor = new HisServiceReqMetyProcessor(param);
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.hisTreatmentFinish = new HisTreatmentFinish(param);
            this.hisSereServProcessor = new HisSereServProcessor(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
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
                HisTreatmentFinishCheck treatmentFinishChecker = new HisTreatmentFinishCheck(param);
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisExpMestCheck expChecker = new HisExpMestCheck(param);
                HisServiceReqOutPatientPresCheck outPatientPresChecker = new HisServiceReqOutPatientPresCheck(param);
                HisServiceReqOutPatientPresUpdateCheck outPatientPresUpdateChecker = new HisServiceReqOutPatientPresUpdateCheck(param);
                List<HIS_SERE_SERV> existedSereServs = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                HIS_DEPARTMENT_TRAN lastDt = null;
                HIS_PROGRAM program = null;
                HIS_MEDI_RECORD mediRecord = null;
                WorkPlaceSDO workPlace = null;
                long? mediStockId = null;
                HIS_EXP_MEST saleExpMest = null;
                V_HIS_DEATH_CERT_BOOK deathCertBook = null;
                HIS_SERVICE_REQ parent = null;

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && outPatientPresChecker.IsValidData(data, ref parent);
                valid = valid && presChecker.IsValidData(data);
                valid = valid && outPatientPresChecker.IsValidStentAmount(data);
                valid = valid && presChecker.IsAllowUpdate(data, ref this.recentServiceReq, ref this.recentExpMest);
                valid = valid && outPatientPresChecker.IsValidSpecialMedicineType(data, this.recentServiceReq); 
                valid = valid && outPatientPresUpdateChecker.IsNotPaidSaleExpMest(data, this.recentServiceReq, ref saleExpMest);
                valid = valid && presChecker.IsValidPatientType(data, data.InstructionTime, ref ptas);
                valid = valid && presChecker.IsValidMediStockInCaseOfUpdate(data, this.recentServiceReq, ref mediStockId);
                valid = valid && presChecker.IsAllowMediStock(data);
                valid = valid && presChecker.IsAllowPrescription(data);
                valid = valid && presChecker.CheckRankPrescription(data);
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref this.treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(this.treatment);
                valid = valid && treatmentChecker.IsUnpause(this.treatment);
                valid = valid && treatmentChecker.IsUnLockHein(this.treatment);
                valid = valid && outPatientPresChecker.IsValidGroup(data, this.recentServiceReq);
                valid = valid && presChecker.IsValidExpMestReason(data.Medicines, data.Materials, true);
                valid = valid && presChecker.IsValidIcdPatientTypeOtherPaySource(data.IcdCode, data.IcdSubCode, data.Medicines, data.Materials);
                valid = valid && presChecker.IsValidFinishTimeCls(this.treatment, parent, data.InstructionTime);

                if (valid) //valid thi moi get du lieu de tranh ton hieu nang
                {
                    if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU
                        || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNGOAITRU
                        || treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTBANNGAY)
                    {
                        valid = valid && presChecker.CheckAmountPrepare(treatment.ID, data.Medicines, data.Materials, data.Id.Value);
                    }
                    existedSereServs = new HisSereServGet().GetByTreatmentId(data.TreatmentId);
                    valid = valid && presChecker.CheckServiceFinishTime(data.InstructionTime, treatment, workPlace, existedSereServs, data.IsCabinet);
                    if (data.TreatmentFinishSDO != null)
                    {
                        data.TreatmentFinishSDO.ServiceReqId = data.ParentServiceReqId;
                        valid = valid && treatmentFinishChecker.IsValidFinishTime(data, parent);
                        valid = valid && treatmentFinishChecker.IsValidForFinish(data.TreatmentFinishSDO, this.treatment, existedSereServs, ptas, ref lastDt, ref workPlace, ref program, ref deathCertBook);
                    }
                }
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_EXP_MEST_MEDICINE> insertMedicines = null;
                    List<HIS_EXP_MEST_MEDICINE> deleteMedicines = null;
                    List<HIS_EXP_MEST_MATERIAL> insertMaterials = null;
                    List<HIS_EXP_MEST_MATERIAL> deleteMaterials = null;
                    List<V_HIS_MEDICINE_2> newsMedicines = null;

                    if (!this.hisServiceReqProcessor.Run(data, ptas, mediStockId, this.recentServiceReq, parent, ref this.recentServiceReq))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisServiceReqMatyProcessor.Run(data.ServiceReqMaties, data.Materials, this.recentServiceReq, ref this.recentServiceReqMaties, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisServiceReqMetyProcessor.Run(data.ServiceReqMeties, data.Medicines, this.recentServiceReq, data.InstructionTime, ref this.recentServiceReqMeties, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisExpMestProcessor.Run(data, this.recentServiceReq, ref this.recentExpMest, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    HisSereServPackage37 processPackage37 = new HisSereServPackage37(param, data.TreatmentId, workPlace.RoomId, workPlace.DepartmentId, existedSereServs);
                    HisSereServPackagePttm processPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, existedSereServs);
                    HisSereServPackageBirth processPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, existedSereServs);
                   

                    if (!this.materialProcessor.Run(data, processPackage37, processPackageBirth, processPackagePttm, this.recentExpMest, ref insertMaterials, ref deleteMaterials, ref this.recentExpMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(data, processPackage37, processPackageBirth, processPackagePttm, this.recentExpMest, ref insertMedicines, ref deleteMedicines, ref this.recentExpMestMedicines, ref newsMedicines, this.treatment, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisSereServProcessor.Run(treatment, ptas, this.recentServiceReq, this.recentExpMest, insertMedicines, deleteMedicines, insertMaterials, deleteMaterials, existedSereServs, newsMedicines, ref this.recentSereServs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.saleExpMestProcessor.Run(data, this.recentServiceReq, saleExpMest, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.ProcessTreatment(parent, data, existedSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref mediRecord);

                    //Set TDL_TOTAL_PRICE, HAS_NOT_PRES
                    HisServiceReqPresUtil.SqlUpdateExpMest(this.recentExpMest, this.recentExpMestMaterials, this.recentExpMestMedicines, ref sqls);

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.ProcessAuto(this.recentExpMest);

                    //Tao yeu cau thanh toan
                    List<HIS_SERVICE_REQ> lstServiceReqs = new List<HIS_SERVICE_REQ>();
                    if (this.recentServiceReq != null)
                    {
                        lstServiceReqs.Add(this.recentServiceReq);
                    }
                    if (IsNotNullOrEmpty(this.recentSereServs) && this.recentSereServs.Exists(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0))
                    {
                        if (!new HisTransReqCreateByService(param).Run(this.treatment, lstServiceReqs, workPlace))
                        {
                            Inventec.Common.Logging.LogSystem.Error("Tao HisTransReq that bai");
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData("tranReqParam", param));
                        }
                    }
                    
                    this.PassResult(mediRecord, ref resultData);

                    HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, resultData.ServiceReqMaties, resultData.ServiceReqMeties, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_SuaDon);

                    if (saleExpMest != null)
                    {
                        new EventLogGenerator(EventLog.Enum.HisExpMest_SuaPhieuXuat).ExpMestCode(saleExpMest.EXP_MEST_CODE).Run();
                    }

                    result = true;
                }
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
            resultData.ExpMests = this.recentExpMest != null ? new List<HIS_EXP_MEST>() { this.recentExpMest } : null;
            List<HIS_SERVICE_REQ> newServiceReqs = this.recentServiceReq != null ? new List<HIS_SERVICE_REQ>() { this.recentServiceReq } : null;
            if (IsNotNullOrEmpty(newServiceReqs))
            {
                resultData.ServiceReqs = new HisServiceReqGet().GetByIds(newServiceReqs.Select(o => o.ID).ToList());
            }
            resultData.Materials = this.recentExpMestMaterials;
            resultData.Medicines = this.recentExpMestMedicines;
            resultData.ServiceReqMaties = this.recentServiceReqMaties;
            resultData.ServiceReqMeties = this.recentServiceReqMeties;
            resultData.MediRecord = mediRecord;
            resultData.SereServs = this.recentSereServs;
        }

        private void ProcessTreatment(HIS_SERVICE_REQ parent, OutPatientPresSDO data, List<HIS_SERE_SERV> existedSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace, HIS_PROGRAM program, HIS_DEPARTMENT_TRAN lastDt, V_HIS_DEATH_CERT_BOOK deathCertBook, ref HIS_MEDI_RECORD mediRecord)
        {
            //Neu co thong tin ket thuc dieu tri thi thuc hien xu ly ket thuc dieu tri
            //(luu y: ket thuc dieu tri nhung co cap nhat ca thong tin ICD da xu ly truoc do)
            if (data.TreatmentFinishSDO != null)
            {
                //Neu ket thuc khi xu ly kham thi thuc hien ket thuc luon dich vu kham
                if (data.ParentServiceReqId.HasValue)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    //Neu cap nhat ket thuc xu ly kham thi bo sung cac thong tin ket thuc
                    HIS_SERVICE_REQ beforeUpdate = Mapper.Map<HIS_SERVICE_REQ>(parent);

                    HisServiceReqUpdateFinish.SetFinishInfo(parent, null, null, data.TreatmentFinishSDO.TreatmentFinishTime);

                    //Da treatment o ham prepare data, o day chi can cap nhat chu ko can check treatment
                    if (!this.hisServiceReqUpdate.Update(parent, beforeUpdate, false))
                    {
                        throw new Exception("Cap nhat du lieu service_req that bai. Rollback du lieu, ket thuc nghiep vu");
                    }
                }

                Mapper.CreateMap<HIS_TREATMENT, HIS_TREATMENT>();
                HIS_TREATMENT oldTreatment = Mapper.Map<HIS_TREATMENT>(this.treatment);

                HisTreatmentFinishSDO treatmentFinishSDO = data.TreatmentFinishSDO;
                //Icd benh chinh lay theo icd cua ke don
                treatmentFinishSDO.IcdName = data.IcdName;
                treatmentFinishSDO.IcdCode = data.IcdCode;

                var reqNotDeletes = new HisServiceReqGet().GetByTreatmentId(this.treatment.ID);
                // Key xet kham phu
                if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON == HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_CLIENT)
                {
                    treatmentFinishSDO.IcdSubCode = data.IcdSubCode;
                    treatmentFinishSDO.IcdText = data.IcdText;
                }
                else if (HisTreatmentCFG.USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON == HisTreatmentCFG.UsingExamSubIcdWhenFinishOption.BY_EXAM_REQS && this.treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__KHAM)
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

                if (!this.hisTreatmentFinish.FinishWithoutValidate(treatmentFinishSDO, this.treatment, oldTreatment, allSereServs, ptas, workPlace, program, lastDt, deathCertBook, ref this.treatment, ref mediRecord))
                {
                    //ket thuc that bai, can rollback
                    throw new Exception("");
                }
            }
            else
            {
                //Neu ko co thong tin ket thuc dieu tri thi
                //tao tien trinh moi de update thong tin treatment
                this.UpdateTreatmentThreadInit(this.treatment, data);
            }
        }

        private void ProcessAuto(HIS_EXP_MEST expMest)
        {
            try
            {
                if (expMest != null)
                {
                    this.autoProcessor.Run(expMest);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
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
        public void RollbackData()
        {
            this.saleExpMestProcessor.Rollback();
            this.hisSereServProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.hisServiceReqMetyProcessor.Rollback();
            this.hisServiceReqMatyProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
            this.hisServiceReqProcessor.Rollback();
            this.hisServiceReqUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
            this.hisTreatmentFinish.RollBackData();
        }
    }
}
