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
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Update
{
    /// <summary>
    /// Xu ly ke don thuoc ngoai tru (don thuoc phong kham hoac don thuoc ke tu tu truc)
    /// </summary>
    class HisServiceReqSubclinicalPresUpdate : BusinessBase
    {
        private HIS_TREATMENT treatment;
        private List<HIS_SERE_SERV> beforeUpdateSereServs = new List<HIS_SERE_SERV>();
        private List<HIS_SERE_SERV> recentSereServs;
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;
        private HIS_SERVICE_REQ recentServiceReq;
        private HIS_EXP_MEST recentExpMest;

        private HisTreatmentUpdate hisTreatmentUpdate;
        private HisSereServProcessor hisSereServProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private HisExpMestProcessor hisExpMestProcessor;
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private AutoProcessor autoProcessor;
        private HisSereServExtProcessor hisSereServExtProcessor;

        internal HisServiceReqSubclinicalPresUpdate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqSubclinicalPresUpdate(CommonParam paramCreate)
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
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.hisSereServProcessor = new HisSereServProcessor(param);
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
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                HisServiceReqSubclinicalPresCheck outPatientPresChecker = new HisServiceReqSubclinicalPresCheck(param);
                HisExpMestCheck expChecker = new HisExpMestCheck(param);
                List<HIS_SERE_SERV> existedSereServs = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                WorkPlaceSDO workPlace = null;
                long? mediStockId = null;
                HIS_EXP_MEST saleExpMest = null;

                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && outPatientPresChecker.IsValidData(data);
                valid = valid && presChecker.IsValidData(data);
                valid = valid && outPatientPresChecker.IsValidStentAmount(data);
                valid = valid && presChecker.IsAllowUpdate(data, ref this.recentServiceReq, ref this.recentExpMest);
                valid = valid && presChecker.IsValidPatientType(data, data.InstructionTime, ref ptas);
                valid = valid && presChecker.IsValidMediStockInCaseOfUpdate(data, this.recentServiceReq, ref mediStockId);
                valid = valid && presChecker.IsAllowMediStock(data);
                valid = valid && presChecker.IsAllowPrescription(data);
                valid = valid && presChecker.CheckRankPrescription(data);
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref this.treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(this.treatment);
                valid = valid && treatmentChecker.IsUnpause(this.treatment);
                valid = valid && treatmentChecker.IsUnLockHein(this.treatment);
                valid = valid && expChecker.HasToExpMestReason(data.ExpMestReasonId);

                if (valid)
                {
                    existedSereServs = new HisSereServGet().GetByTreatmentId(data.TreatmentId);

                    List<string> sqls = new List<string>();
                    List<HIS_EXP_MEST_MEDICINE> insertMedicines = null;
                    List<HIS_EXP_MEST_MEDICINE> deleteMedicines = null;
                    List<HIS_EXP_MEST_MATERIAL> insertMaterials = null;
                    List<HIS_EXP_MEST_MATERIAL> deleteMaterials = null;

                    if (!this.hisServiceReqProcessor.Run(data, ptas, mediStockId, this.recentServiceReq, ref this.recentServiceReq))
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

                    if (!this.medicineProcessor.Run(data, processPackage37, processPackageBirth, processPackagePttm, this.recentExpMest, ref insertMedicines, ref deleteMedicines, ref this.recentExpMestMedicines, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.hisSereServExtProcessor.Run(data, deleteMaterials, this.recentServiceReq);

                    if (!this.hisSereServProcessor.Run(treatment, ptas, this.recentServiceReq, this.recentExpMest, insertMedicines, deleteMedicines, insertMaterials, deleteMaterials, existedSereServs, ref this.recentSereServs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.ProcessTreatment(data, existedSereServs, ptas, workPlace);

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(this.recentExpMest, this.recentExpMestMaterials, this.recentExpMestMedicines, ref sqls);

                    ///execute sql se thuc hien cuoi cung de de dang trong viec rollback
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.ProcessAuto(this.recentExpMest, saleExpMest);

                    this.PassResult(ref resultData);

                    HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, null, null, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_SuaDon);

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

        private void ProcessTdlTotalPrice(HIS_EXP_MEST exp, List<HIS_EXP_MEST_MATERIAL> lstMaterial, List<HIS_EXP_MEST_MEDICINE> lstMedicine, ref List<string> sqls)
        {
            try
            {
                if (exp == null) return;

                decimal? totalPrice = null;
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
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(ref SubclinicalPresResultSDO resultData)
        {
            resultData = new SubclinicalPresResultSDO();
            resultData.ExpMests = new List<HIS_EXP_MEST>() { this.recentExpMest };
            resultData.ServiceReqs = new List<HIS_SERVICE_REQ>() { this.recentServiceReq };
            resultData.Materials = this.recentExpMestMaterials;
            resultData.Medicines = this.recentExpMestMedicines;
        }

        private void ProcessTreatment(SubclinicalPresSDO data, List<HIS_SERE_SERV> existedSereServs, List<HIS_PATIENT_TYPE_ALTER> ptas, WorkPlaceSDO workPlace)
        {
            //tao tien trinh moi de update thong tin treatment (icd)
            this.UpdateTreatmentThreadInit(this.treatment, data);
        }

        private void ProcessAuto(HIS_EXP_MEST expMest, HIS_EXP_MEST saleExpMest)
        {
            try
            {
                List<HIS_EXP_MEST> exps = new List<HIS_EXP_MEST>();
                if (saleExpMest != null)
                {
                    this.autoProcessor.Run(saleExpMest);
                }
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

        private void UpdateTreatmentThreadInit(HIS_TREATMENT treatment, SubclinicalPresSDO data)
        {
            try
            {
                Thread thread = new Thread(new ParameterizedThreadStart(this.ThreadProcessUpdateTreatment));
                thread.Priority = ThreadPriority.AboveNormal;
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

        private void RollbackData()
        {
            this.hisSereServProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
            this.hisServiceReqProcessor.Rollback();
            this.hisServiceReqUpdate.RollbackData();
            this.hisTreatmentUpdate.RollbackData();
        }
    }
}
