using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update;
using MOS.MANAGER.HisTreatment.Util;
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

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Update
{
    /// <summary>
    /// Xu ly cap nhat don thuoc noi tru
    /// </summary>
    class HisServiceReqInPatientPresUpdate : BusinessBase
    {
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private List<HIS_SERVICE_REQ_MATY> recentServiceReqMaties;
        private List<HIS_SERVICE_REQ_METY> recentServiceReqMeties;
        private List<HIS_SERE_SERV> recentSereServs;
        private HIS_EXP_MEST recentExpMest;
        private HIS_SERVICE_REQ recentServiceReq;

        private HisExpMestProcessor hisExpMestProcessor;
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisServiceReqMetyProcessor hisServiceReqMetyProcessor;
        private HisServiceReqMatyProcessor hisServiceReqMatyProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private MaterialBySerialNumberProcessor materialBySerialNumberProcessor;
        private HisSereServProcessor hisSereServProcessor;

        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisServiceReqInPatientPresUpdate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqInPatientPresUpdate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.hisServiceReqProcessor = new HisServiceReqProcessor(param);
            this.hisServiceReqMetyProcessor = new HisServiceReqMetyProcessor(param);
            this.hisServiceReqMatyProcessor = new HisServiceReqMatyProcessor(param);
            this.hisTreatmentUpdate = new HisTreatmentUpdate(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.hisSereServProcessor = new HisSereServProcessor(param);
            this.materialBySerialNumberProcessor = new MaterialBySerialNumberProcessor(param);
        }

        internal bool Run(InPatientPresSDO data, ref InPatientPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                HIS_SERVICE_REQ serviceReq = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                WorkPlaceSDO workPlace = null;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqInPatientPresCheck checker = new HisServiceReqInPatientPresCheck(param);
                HisServiceReqInPatientPresUpdateCheck updateChecker = new HisServiceReqInPatientPresUpdateCheck(param);
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                HisExpMestCheck expChecker = new HisExpMestCheck(param);

                bool updateEyeInfo = false;
                long? mediStockId = null;
                List<long> instructionTimes = null;
                valid = valid && presChecker.IsValidData(data);
                valid = valid && presChecker.IsValidPatientType(data, data.InstructionTimes, ref ptas);
                valid = valid && presChecker.IsAllowMediStock(data);
                valid = valid && presChecker.IsAllowPrescription(data);
                valid = valid && presChecker.CheckRankPrescription(data);
                valid = valid && checker.IsValidData(data, ref instructionTimes);
                valid = valid && presChecker.IsAllowUpdate(data, ref serviceReq, ref this.recentExpMest);
                valid = valid && updateChecker.IsValidData(data, serviceReq);
                valid = valid && presChecker.IsValidMediStockInCaseOfUpdate(data, serviceReq, ref mediStockId);
                valid = valid && (!mediStockId.HasValue || checker.IsNotCabinet(mediStockId.Value));//Khong cho phep ke don noi tru vao tu truc
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && presChecker.CheckAmountPrepare(treatment.ID, data.Medicines, data.Materials, data.Id.Value);
                valid = valid && presChecker.IsValidExpMestReason(data.Medicines, data.Materials, true);
                valid = valid && presChecker.IsValidIcdPatientTypeOtherPaySource(data.IcdCode, data.IcdSubCode, data.Medicines, data.Materials);
                if (valid)
                {
                    updateEyeInfo = (serviceReq.IS_HOME_PRES == Constant.IS_TRUE || data.IsHomePres);
                    List<string> sqls = new List<string>();

                    if (!this.hisServiceReqProcessor.Run(data, mediStockId, serviceReq, ref this.recentServiceReq))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisServiceReqMatyProcessor.Run(data.ServiceReqMaties, data.Id.Value, data.TreatmentId, ref this.recentServiceReqMaties, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisServiceReqMetyProcessor.Run(data.ServiceReqMeties, data.Id.Value, data.TreatmentId, ref this.recentServiceReqMeties, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisExpMestProcessor.Run(data, this.recentServiceReq, ref this.recentExpMest, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetByTreatmentId(treatment.ID);

                    HisSereServPackage37 processPackage37 = new HisSereServPackage37(param, data.TreatmentId, workPlace.RoomId, workPlace.DepartmentId, existSereServs);
                    HisSereServPackageBirth processPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, existSereServs);
                    HisSereServPackagePttm processPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, existSereServs);

                    List<HIS_EXP_MEST_MATERIAL> oldMaterials = this.recentExpMest != null ? new HisExpMestMaterialGet().GetByExpMestId(this.recentExpMest.ID) : null;
                    List<HIS_EXP_MEST_MEDICINE> oldMedicines = this.recentExpMest != null ? new HisExpMestMedicineGet().GetByExpMestId(this.recentExpMest.ID) : null;

                    //Lay cac du lieu them, xoa de cap nhat sang sere_serv
                    List<HIS_EXP_MEST_MEDICINE> insertMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> deleteMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MATERIAL> insertMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                    List<HIS_EXP_MEST_MATERIAL> deleteMaterials = new List<HIS_EXP_MEST_MATERIAL>();
                    List<V_HIS_MEDICINE_2> newsMedicines = null;

                    if (!this.medicineProcessor.Run(oldMedicines, processPackage37, processPackageBirth, processPackagePttm, data.Medicines, this.recentExpMest, treatment, data.InstructionTimes[0], data.UseTime, ref insertMedicines, ref deleteMedicines, ref this.recentExpMestMedicines, ref newsMedicines, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(oldMaterials, processPackage37, processPackageBirth, processPackagePttm, data.Materials, this.recentExpMest, ref insertMaterials, ref deleteMaterials, data.InstructionTimes[0], ref this.recentExpMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.materialBySerialNumberProcessor.Run(oldMaterials, processPackage37, processPackageBirth, processPackagePttm, data.SerialNumbers, this.recentExpMest, ref insertMaterials, ref deleteMaterials, data.InstructionTimes[0], ref this.recentExpMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisSereServProcessor.Run(treatment, existSereServs, ptas, this.recentServiceReq, this.recentExpMest, insertMedicines, deleteMedicines, insertMaterials, deleteMaterials, newsMedicines, ref this.recentSereServs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Set TDL_TOTAL_PRICE, HAS_NOT_PRES
                    HisServiceReqPresUtil.SqlUpdateExpMest(this.recentExpMest, this.recentExpMestMaterials, this.recentExpMestMedicines, ref sqls);

                    //Can execute sql o cuoi de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.PassResult(ref resultData);

                    if (data.IsTemporaryPres == Constant.IS_TRUE)
                    {
                        HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, resultData.ServiceReqMaties, resultData.ServiceReqMeties, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_SuaDonTam);
                    }
                    else
                    {
                        HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, resultData.ServiceReqMaties, resultData.ServiceReqMeties, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_SuaDon);
                    }
                    

                    //tao tien trinh moi de update thong tin treatment
                    this.UpdateTreatmentThreadInit(treatment, data, updateEyeInfo);
                    result = true;
                    //Kiem tra co vuot qua 6 thang luong co ban khong
                    this.ProcessCheckBaseSalary(treatment, ptas, this.recentSereServs);
                    //Cap nhat lai thong tin du thua du lieu phieu linh (TDL_AGGR_EXP_MEST_ID)
                    new HisServiceReqPresUtil().InitThreadUpdateHisExpMest(new List<HIS_EXP_MEST>() { this.recentExpMest }, this.recentExpMestMedicines, this.recentExpMestMaterials);
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

        private void PassResult(ref InPatientPresResultSDO resultData)
        {
            resultData = new InPatientPresResultSDO();
            resultData.ExpMests = new List<HIS_EXP_MEST>() { this.recentExpMest };
            resultData.ServiceReqs = new List<HIS_SERVICE_REQ>() { this.recentServiceReq };
            resultData.Materials = this.recentExpMestMaterials;
            resultData.Medicines = this.recentExpMestMedicines;
            resultData.ServiceReqMaties = this.recentServiceReqMaties;
            resultData.ServiceReqMeties = this.recentServiceReqMeties;
            resultData.SereServs = this.recentSereServs;
        }

        private void UpdateTreatmentThreadInit(HIS_TREATMENT treatment, InPatientPresSDO data, bool updateEyeInfo)
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
                threadData.UpdateEyeInfo = updateEyeInfo;
                thread.Start(threadData);
            }
            catch (Exception ex)
            {
                LogSystem.Error("Khoi tao tien trinh cap nhat treatment", ex);
            }
        }

        private void ThreadProcessUpdateTreatment(object threadData)
        {
            try
            {
                //Tien trinh xu ly cap nhat thong tin ICD cua treatment
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

                //Thuc hien update thong tin mat sau khi update icd tranh truong hop update trong len nhau do icd update ca object
                ThreadProcessTreatmentEyeInfo(treatment.ID, td.UpdateEyeInfo);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private void ProcessCheckBaseSalary(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> patyAlters, List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                new HisTreatmentCheckOverSixMonthSalary(param).Run(treatment, patyAlters, sereServs);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ThreadProcessTreatmentEyeInfo(long treatmentId, bool updateEyeInfo)
        {
            try
            {
                if (updateEyeInfo)
                {
                    new HisTreatmentUpdateEyeInfo().Run(treatmentId);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public void RollbackData()
        {
            this.hisServiceReqMetyProcessor.Rollback();
            this.hisServiceReqMatyProcessor.Rollback();
            this.hisSereServProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
            this.hisServiceReqProcessor.Rollback();
        }
    }
}
