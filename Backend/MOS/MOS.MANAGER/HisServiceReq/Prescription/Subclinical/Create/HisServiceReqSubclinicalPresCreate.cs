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
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update.Finish;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Create
{
    /// <summary>
    /// Xu ly ke don thuoc ngoai tru (don thuoc phong kham hoac don thuoc ke tu tu truc)
    /// </summary>
    class HisServiceReqSubclinicalPresCreate : BusinessBase
    {
        private HIS_TREATMENT treatment;
        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();
        private List<HIS_SERE_SERV> recentSereServs;
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;
        private List<HIS_SERVICE_REQ> recentServiceReqs;
        private List<HIS_EXP_MEST> recentExpMests;

        private HisSereServUpdate hisSereServUpdate;

        private HisSereServCreateSql hisSereServCreate;
        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisExpMestProcessor hisExpMestProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServExtProcessor hisSereServExtProcessor;
        private AutoProcessor autoProcessor;

        internal HisServiceReqSubclinicalPresCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqSubclinicalPresCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServCreate = new HisSereServCreateSql(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.hisServiceReqProcessor = new HisServiceReqProcessor(param);
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.autoProcessor = new AutoProcessor(param);
            this.hisSereServExtProcessor = new HisSereServExtProcessor(param);
        }

        internal bool Run(SubclinicalPresSDO data, ref SubclinicalPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqSubclinicalPresCheck outPatientPresChecker = new HisServiceReqSubclinicalPresCheck(param);
                HisTreatmentFinishCheck treatmentFinishChecker = new HisTreatmentFinishCheck(param);
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisExpMestCheck expChecker = new HisExpMestCheck(param);

                List<long> mediStockIds = null;
                List<HIS_SERE_SERV> existedSereServs = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                HIS_SERVICE_REQ parentServiceReq = null;
                WorkPlaceSDO workPlace = null;
                string sessionCode = Guid.NewGuid().ToString();
                long sereServParentId = 0;
                this.SetServerTime(data);

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && presChecker.IsValidData(data);
                valid = valid && presChecker.IsValidPatientType(data, data.InstructionTime, ref ptas);
                valid = valid && presChecker.IsAllowMediStock(data, ref mediStockIds);
                valid = valid && presChecker.IsAllowPrescription(data);
                valid = valid && presChecker.CheckRankPrescription(data);
                valid = valid && outPatientPresChecker.IsValidStentAmount(data);
                valid = valid && outPatientPresChecker.IsValidData(data);
                valid = valid && outPatientPresChecker.IsValidParentServiceReq(data, ref parentServiceReq, ref sereServParentId);
                valid = valid && (parentServiceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL || serviceReqChecker.IsAllowedForStart(parentServiceReq));
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref this.treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(this.treatment);
                valid = valid && treatmentChecker.IsUnpause(this.treatment);
                valid = valid && treatmentChecker.IsUnLockHein(this.treatment);
                valid = valid && expChecker.HasToExpMestReason(data.ExpMestReasonId);

                if (valid)
                {
                    existedSereServs = new HisSereServGet().GetByTreatmentId(data.TreatmentId);

                    HIS_SERVICE_REQ outStockServiceReq = null;
                    HIS_EXP_MEST saleExpMest = null;

                    List<string> sqls = new List<string>();

                    if (!this.hisServiceReqProcessor.Run(data, parentServiceReq.ID, this.treatment, ptas, mediStockIds, sessionCode, ref this.recentServiceReqs, ref outStockServiceReq))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    List<HIS_SERVICE_REQ> inStockServiceReqs = this.recentServiceReqs.Where(o => outStockServiceReq == null || o != outStockServiceReq).ToList();

                    if (!this.hisExpMestProcessor.Run(inStockServiceReqs, data.ExpMestReasonId, ref this.recentExpMests))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    HisSereServPackage37 processPackage37 = new HisSereServPackage37(param, data.TreatmentId, workPlace.RoomId, workPlace.DepartmentId, existedSereServs);
                    HisSereServPackageBirth processPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, existedSereServs);
                    HisSereServPackagePttm processPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, existedSereServs);

                    if (!this.medicineProcessor.Run(data, processPackage37, processPackageBirth, processPackagePttm, this.recentExpMests, ref this.recentExpMestMedicines, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(data, processPackage37, processPackageBirth, processPackagePttm, this.recentExpMests, ref this.recentExpMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.hisSereServExtProcessor.Run(data, sereServParentId, this.recentServiceReqs);

                    this.ProcessSereServ(data, ptas, inStockServiceReqs, existedSereServs);

                    this.ProcessParentServiceReq(parentServiceReq, data);

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(this.recentExpMests, this.recentExpMestMaterials, this.recentExpMestMedicines, ref sqls);

                    this.ProcessTreatment(data, existedSereServs, ptas, workPlace);

                    //Xu ly sql de duoi cung de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.ProcessAuto(this.recentExpMests, saleExpMest);

                    this.PassResult(ref resultData);

                    HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, null, null, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_KeDon);
                }
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

        private void PassResult(ref SubclinicalPresResultSDO resultData)
        {
            resultData = new SubclinicalPresResultSDO();
            resultData.ExpMests = this.recentExpMests;
            resultData.Materials = this.recentExpMestMaterials;
            resultData.Medicines = this.recentExpMestMedicines;
            resultData.ServiceReqs = this.recentServiceReqs;
        }

        private void ProcessSereServ(SubclinicalPresSDO pres, List<HIS_PATIENT_TYPE_ALTER> ptas, List<HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> existedSereServs)
        {
            if (IsNotNullOrEmpty(this.recentExpMests))
            {
                List<HIS_SERE_SERV> newSereServs = null;

                //Neu co ke thuoc trong kho thi moi tao du lieu sere_serv
                if (!new HisSereServMaker(param, this.treatment, pres, this.recentServiceReqs, this.recentExpMests, this.recentExpMestMedicines, this.recentExpMestMaterials).Run(ref newSereServs))
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

                    if (!new HisSereServUpdateHein(param, this.treatment, ptas, false).Update(existedSereServs, toUpdateData, ref changeRecords, ref oldOfChangeRecords))
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

        private void ProcessParentServiceReq(HIS_SERVICE_REQ parentServiceReq, SubclinicalPresSDO pres)
        {
            //Chi cap nhat khi chi dinh cha (chi dinh dv chua o trang thai "dang xu ly")
            if (parentServiceReq.SERVICE_REQ_STT_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL)
            {
                parentServiceReq.START_TIME = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now);
                parentServiceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__DXL;
                if (String.IsNullOrWhiteSpace(pres.RequestLoginName))
                {
                    parentServiceReq.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    parentServiceReq.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                }
                else
                {
                    parentServiceReq.EXECUTE_USERNAME = pres.RequestLoginName;
                    parentServiceReq.EXECUTE_LOGINNAME = pres.RequestUserName;
                }

                parentServiceReq.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                if (DAOWorker.HisServiceReqDAO.Update(parentServiceReq))
                {
                    new EventLogGenerator(EventLog.Enum.HisServiceReq_BatDauXuLy)
                        .TreatmentCode(parentServiceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(parentServiceReq.SERVICE_REQ_CODE).Run();
                }
                else
                {
                    throw new Exception("Cap nhat parent_service_req that bai. Rollback du lieu");
                }
            }
        }

        private void ProcessTreatment(SubclinicalPresSDO data, List<HIS_SERE_SERV> existedSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace)
        {
            //tao tien trinh moi de update thong tin treatment (icd)
            this.UpdateTreatmentThreadInit(this.treatment, data);
        }

        private void ProcessTdlTotalPrice(List<HIS_EXP_MEST> expMests, List<HIS_EXP_MEST_MATERIAL> expMestMaterials, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            try
            {
                if (expMests == null) return;

                foreach (HIS_EXP_MEST exp in expMests)
                {
                    decimal? totalPrice = null;
                    List<HIS_EXP_MEST_MATERIAL> lstMaterial = expMestMaterials != null ? expMestMaterials.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                    List<HIS_EXP_MEST_MEDICINE> lstMedicine = expMestMedicines != null ? expMestMedicines.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                    if (IsNotNullOrEmpty(lstMaterial))
                    {
                        decimal matePrice = 0;
                        foreach (HIS_EXP_MEST_MATERIAL mate in lstMaterial)
                        {
                            if (!mate.PRICE.HasValue)
                            {
                                continue;
                            }
                            matePrice += (mate.AMOUNT * mate.PRICE.Value * (1 + (mate.VAT_RATIO ?? 0)));
                        }
                        if (matePrice > 0)
                        {
                            totalPrice = matePrice;
                        }
                    }
                    if (IsNotNullOrEmpty(lstMedicine))
                    {
                        decimal mediPrice = 0;
                        foreach (HIS_EXP_MEST_MEDICINE medi in lstMedicine)
                        {
                            if (!medi.PRICE.HasValue)
                            {
                                continue;
                            }
                            mediPrice += (medi.AMOUNT * medi.PRICE.Value * (1 + (medi.VAT_RATIO ?? 0)));
                        }
                        if (mediPrice > 0)
                        {
                            totalPrice = (totalPrice ?? 0) + mediPrice;
                        }
                    }
                    if (totalPrice.HasValue)
                    {
                        string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0} WHERE ID = {1}", totalPrice.Value.ToString("G27", CultureInfo.InvariantCulture), exp.ID);
                        sqls.Add(updateSql);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Xu ly de gan cac thong tin thoi gian theo gio server
        /// </summary>
        /// <param name="data"></param>
        private void SetServerTime(SubclinicalPresSDO data)
        {
            //Neu cau hinh su dung gio server thi gan lai theo gio server cac du lieu thoi gian truyen len
            if (SystemCFG.IS_USING_SERVER_TIME && data != null)
            {
                long now = Inventec.Common.DateTime.Get.Now().Value;
                data.InstructionTime = now;
                data.UseTime = now;
            }
        }
        
        private void ProcessAuto(List<HIS_EXP_MEST> hisExpMests, HIS_EXP_MEST saleExpMest)
        {
            try
            {
                List<HIS_EXP_MEST> exps = new List<HIS_EXP_MEST>();
                if (saleExpMest != null)
                {
                    exps.Add(saleExpMest);
                }
                if (IsNotNullOrEmpty(hisExpMests))
                {
                    exps.AddRange(hisExpMests);
                }

                this.autoProcessor.Run(exps);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void UpdateTreatmentThreadInit(HIS_TREATMENT treatment, SubclinicalPresSDO data)
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

        private void RollbackData()
        {
            this.hisSereServCreate.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
            this.hisServiceReqProcessor.Rollback();
            this.hisServiceReqUpdate.RollbackData();
        }
    }
}
