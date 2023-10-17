using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Update;
using MOS.MANAGER.HisTreatment.Util;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MOS.UTILITY;

namespace MOS.MANAGER.HisServiceReq.Prescription.InPatient.Create
{
    /// <summary>
    /// Xu ly ke don thuoc noi tru
    /// </summary>
    class HisServiceReqInPatientPresCreate : BusinessBase
    {
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;
        private List<HIS_EXP_MEST_MATERIAL> recentExpMestMaterials;
        private List<HIS_SERVICE_REQ_MATY> recentServiceReqMaties;
        private List<HIS_SERVICE_REQ_METY> recentServiceReqMeties;
        private List<HIS_SERVICE_REQ> recentServiceReqs;
        private List<HIS_EXP_MEST> recentExpMests;
        private List<HIS_SERE_SERV> recentSereServs;

        private HisExpMestProcessor hisExpMestProcessor;
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisServiceReqMetyProcessor hisServiceReqMetyProcessor;
        private HisServiceReqMatyProcessor hisServiceReqMatyProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private MaterialBySerialNumberProcessor materialBySerialNumberProcessor;
        private SereServProcessor sereServProcessor;

        private HisTreatmentUpdate hisTreatmentUpdate;

        internal HisServiceReqInPatientPresCreate()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqInPatientPresCreate(CommonParam paramCreate)
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
            this.sereServProcessor = new SereServProcessor(param);
            this.materialBySerialNumberProcessor = new MaterialBySerialNumberProcessor(param);
        }

        internal bool Create(InPatientPresSDO data, ref InPatientPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT treatment = null;
                List<HIS_PATIENT_TYPE_ALTER> ptas = null;
                WorkPlaceSDO workPlace = null;
                string sessionCode = Guid.NewGuid().ToString();

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisServiceReqInPatientPresCheck checker = new HisServiceReqInPatientPresCheck(param);
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);
                HisExpMestCheck expChecker = new HisExpMestCheck(param);
                List<long> mediStockIds = null;
                List<long> instructionTimes = null;

                valid = valid && presChecker.IsValidData(data);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && checker.IsValidData(data, ref instructionTimes);
                valid = valid && presChecker.IsValidPatientType(data, instructionTimes, ref ptas);
                valid = valid && presChecker.IsAllowMediStock(data, ref mediStockIds);
                valid = valid && presChecker.IsAllowPrescription(data);
                valid = valid && presChecker.IsNotExceededMaxSuspendingDay(workPlace.DepartmentId);
                valid = valid && presChecker.CheckRankPrescription(data);
                valid = valid && checker.IsNotCabinet(mediStockIds);//Khong cho phep ke don noi tru vao tu truc
                valid = valid && treatmentChecker.IsUnLock(data.TreatmentId, ref treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && presChecker.CheckAmountPrepare(treatment.ID, data.Medicines, data.Materials, null);
                valid = valid && presChecker.IsValidUseWithInstructionTimes(data.UseTimes, data.InstructionTimes);
                if (valid)
                {
                    List<HIS_SERVICE_REQ> inStockServiceReqs = null;
                    List<HIS_SERVICE_REQ> outStockServiceReqs = null;
                    List<HIS_SERE_SERV> newSereServs = null;
                    List<V_HIS_MEDICINE_2> choosenMedicines = null;

                    List<string> sqls = new List<string>();

                    if (!this.hisServiceReqProcessor.Run(treatment, data, ptas, instructionTimes, mediStockIds, sessionCode, ref this.recentServiceReqs, ref inStockServiceReqs, ref outStockServiceReqs))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisExpMestProcessor.Run(inStockServiceReqs, data, ref this.recentExpMests))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetHasExecuteByTreatmentId(treatment.ID);

                    HisSereServPackage37 processPackage37 = new HisSereServPackage37(param, data.TreatmentId, workPlace.RoomId, workPlace.DepartmentId, existSereServs);
                    HisSereServPackageBirth processPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, existSereServs);
                    HisSereServPackagePttm processPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, existSereServs);

                    if (!this.medicineProcessor.Run(processPackage37, processPackageBirth, processPackagePttm, data.Medicines, this.recentExpMests, ref this.recentExpMestMedicines, ref choosenMedicines, treatment, ref sqls, data.UseTime))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.materialProcessor.Run(processPackage37, processPackageBirth, processPackagePttm, data.Materials, this.recentExpMests, ref this.recentExpMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.materialBySerialNumberProcessor.Run(processPackage37, processPackageBirth, processPackagePttm, data.SerialNumbers, this.recentExpMests, ref this.recentExpMestMaterials, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisServiceReqMetyProcessor.Run(instructionTimes, data.ServiceReqMeties, inStockServiceReqs, outStockServiceReqs, ref this.recentServiceReqMeties))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback");
                    }

                    if (!this.hisServiceReqMatyProcessor.Run(instructionTimes, data.ServiceReqMaties, inStockServiceReqs, outStockServiceReqs, ref this.recentServiceReqMaties))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback");
                    }

                    if (!this.sereServProcessor.Run(treatment, existSereServs, this.recentExpMests, this.recentExpMestMedicines, this.recentExpMestMaterials, choosenMedicines, ref newSereServs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback");
                    }
                    this.recentSereServs = newSereServs;

                    //Set TDL_TOTAL_PRICE, HAS_NOT_PRES
                    HisServiceReqPresUtil.SqlUpdateExpMest(this.recentExpMests, this.recentExpMestMaterials, this.recentExpMestMedicines, ref sqls);

                    //Can execute sql o cuoi de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.PassResult(ref resultData);

                    if (data.IsTemporaryPres == Constant.IS_TRUE)
                    {
                        HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, resultData.ServiceReqMaties, resultData.ServiceReqMeties, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_KeDonTam);
                    }
                    else
                    {
                        HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, resultData.ServiceReqMaties, resultData.ServiceReqMeties, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_KeDon);
                    }

                    //tao tien trinh moi de update thong tin treatment
                    this.UpdateTreatmentThreadInit(treatment, data);
                    result = true;
                    //Kiem tra co vuot qua 6 thang luong co ban khong
                    this.ProcessCheckBaseSalary(treatment, ptas, existSereServs, newSereServs);
                    //Cap nhat lai thong tin du thua du lieu phieu linh (TDL_AGGR_EXP_MEST_ID)
                    new HisServiceReqPresUtil().InitThreadUpdateHisExpMest(this.recentExpMests, this.recentExpMestMedicines, this.recentExpMestMaterials);
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
            resultData.SereServs = this.recentSereServs;
        }

        private void UpdateTreatmentThreadInit(HIS_TREATMENT treatment, InPatientPresSDO data)
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
                threadData.UpdateEyeInfo = data.IsHomePres;
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

        private void ProcessCheckBaseSalary(HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> patyAlters, List<HIS_SERE_SERV> existedSereServs, List<HIS_SERE_SERV> newSereServs)
        {
            try
            {
                List<HIS_SERE_SERV> sereServs = new List<HIS_SERE_SERV>();
                if (IsNotNullOrEmpty(existedSereServs))
                {
                    sereServs.AddRange(existedSereServs);
                }
                if (IsNotNullOrEmpty(newSereServs))
                {
                    sereServs.AddRange(newSereServs);
                }
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
            this.sereServProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
            this.hisServiceReqProcessor.Rollback();
        }
    }
}
