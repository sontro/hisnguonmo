using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Create;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.CreateByConfig
{
    /// <summary>
    /// Xu ly ke don thuoc/vat tu tieu hao dua vao du lieu da thiet lap tu truoc
    /// </summary>
    class ExpendPresCreateByConfig : BusinessBase
    {
        private HisExpMestProcessor hisExpMestProcessor;
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private SereServProcessor sereServProcessor;
        private HisServiceReqProcessor hisServiceReqProcessor;
        private HisSereServExtProcessor hisSereServExtProcessor;
        private AutoProcessor autoProcessor;

        internal ExpendPresCreateByConfig()
            : base()
        {
            this.Init();
        }

        internal ExpendPresCreateByConfig(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.sereServProcessor = new SereServProcessor(param);
            this.hisServiceReqProcessor = new HisServiceReqProcessor(param);
            this.hisSereServExtProcessor = new HisSereServExtProcessor(param);
            this.autoProcessor = new AutoProcessor(param);
        }

        internal bool Run(ExpendPresSDO data, ref SubclinicalPresResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_SERVICE_REQ parentServiceReq = null;
                V_HIS_MEDI_STOCK mediStock = null;
                HIS_TREATMENT treatment = null;
                List<HIS_SERE_SERV> sereServs = null;
                List<HIS_SERE_SERV_EXT> sereServExts = null;
                List<HIS_SERVICE_MATY> serviceMaties = null;
                List<HIS_SERVICE_METY> serviceMeties = null;
                long instructionTime = Inventec.Common.DateTime.Get.Now().Value;
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                ExpendPresCreateByConfigCheck checker = new ExpendPresCreateByConfigCheck(param);
                HisServiceReqPresCheck presChecker = new HisServiceReqPresCheck(param);

                valid = valid && checker.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && serviceReqChecker.VerifyId(data.ServiceReqId, ref parentServiceReq);
                valid = valid && treatmentChecker.VerifyId(parentServiceReq.TREATMENT_ID, ref treatment);
                valid = valid && treatmentChecker.IsUnLock(treatment);
                valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                valid = valid && treatmentChecker.IsUnpause(treatment);
                valid = valid && treatmentChecker.IsUnLockHein(treatment);
                valid = valid && (parentServiceReq.SERVICE_REQ_STT_ID != IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__CXL || serviceReqChecker.IsAllowedForStart(parentServiceReq));
                valid = valid && checker.HasNoExpendPres(parentServiceReq, data, ref sereServs, ref sereServExts);
                valid = valid && checker.HasExpendConfig(sereServs, ref serviceMaties, ref serviceMeties);
                valid = valid && checker.IsValidMediStock(data.RequestRoomId, data.MediStockId, ref mediStock);

                if (valid)
                {
                    List<HIS_EXP_MEST> listExpMest = new List<HIS_EXP_MEST>();
                    foreach (HIS_SERE_SERV sereServ in sereServs)
                    {
                        HIS_SERVICE_REQ serviceReq = null;
                        HIS_EXP_MEST expMest = null;
                        List<HIS_SERE_SERV> newSereServs = null;
                        List<V_HIS_MEDICINE_2> choosenMedicines = null;
                        List<HIS_EXP_MEST_MEDICINE> expMestMedicines = null;
                        List<HIS_EXP_MEST_MATERIAL> expMestMaterials = null;
                        HIS_SERE_SERV_EXT sereServExt = null;

                        List<string> sqls = new List<string>();

                        if (!this.hisServiceReqProcessor.Run(mediStock, treatment, parentServiceReq.ID, data.RequestRoomId, instructionTime, ref serviceReq))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }

                        if (!this.hisExpMestProcessor.Run(serviceReq, ref expMest))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }

                        List<HIS_SERE_SERV> existSereServs = new HisSereServGet().GetHasExecuteByTreatmentId(treatment.ID);

                        HisSereServPackage37 processPackage37 = new HisSereServPackage37(param, treatment.ID, workPlace.RoomId, workPlace.DepartmentId, existSereServs);
                        HisSereServPackageBirth processPackageBirth = new HisSereServPackageBirth(param, workPlace.DepartmentId, existSereServs);
                        HisSereServPackagePttm processPackagePttm = new HisSereServPackagePttm(param, workPlace.DepartmentId, existSereServs);

                        if (!this.medicineProcessor.Run(expMest, processPackage37, processPackageBirth, processPackagePttm, sereServ, serviceMeties, ref expMestMedicines, ref choosenMedicines, ref sqls))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }

                        if (!this.materialProcessor.Run(expMest, processPackage37, processPackageBirth, processPackagePttm, sereServ, serviceMaties, ref expMestMaterials, ref sqls))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }

                        if (!this.sereServProcessor.Run(treatment, existSereServs, serviceReq, expMest, expMestMedicines, expMestMaterials, choosenMedicines, ref newSereServs))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback");
                        }

                        if (!this.hisSereServExtProcessor.Run(sereServExts, sereServ, serviceReq, IsNotNullOrEmpty(expMestMaterials), ref sereServExt))
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback");
                        }

                        //Set TDL_TOTAL_PRICE, HAS_NOT_PRES
                        HisServiceReqPresUtil.SqlUpdateExpMest(expMest, expMestMaterials, expMestMedicines, ref sqls);

                        //Can execute sql o cuoi de tranh rollback du lieu
                        if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                        {
                            throw new Exception("Rollback");
                        }

                        this.PassResult(serviceReq, expMest, expMestMedicines, expMestMaterials,  sereServExt, ref resultData);
                        listExpMest.Add(expMest);

                        HisServiceReqLog.Run(resultData.ServiceReqs, resultData.ExpMests, null, null, resultData.Materials, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisServiceReq_KeDon);
                    }
                    this.ProcessAuto(listExpMest);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                resultData = null;
                result = false;
            }
            return result;
        }

        private void PassResult(HIS_SERVICE_REQ serviceReq, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> medicines, List<HIS_EXP_MEST_MATERIAL> materials, HIS_SERE_SERV_EXT sereServExt, ref SubclinicalPresResultSDO resultData)
        {
            if (resultData == null)
            {
                resultData = new SubclinicalPresResultSDO();
            }
            if (resultData.ExpMests == null)
            {
                resultData.ExpMests = new List<HIS_EXP_MEST>();
            }
            if (resultData.Materials == null)
            {
                resultData.Materials = new List<HIS_EXP_MEST_MATERIAL>();
            }
            if (resultData.Medicines == null)
            {
                resultData.Medicines = new List<HIS_EXP_MEST_MEDICINE>();
            }
            if (resultData.ServiceReqs == null)
            {
                resultData.ServiceReqs = new List<HIS_SERVICE_REQ>();
            }
            if (expMest != null)
            {
                resultData.ExpMests.Add(expMest);
            }
            if (IsNotNullOrEmpty(materials))
            {
                resultData.Materials.AddRange(materials);
            }
            if (IsNotNullOrEmpty(medicines))
            {
                resultData.Medicines.AddRange(medicines);
            }
            if (serviceReq != null)
            {
                resultData.ServiceReqs.Add(serviceReq);
            }
            resultData.SereServExt = sereServExt;
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

        private void RollbackData()
        {
            try
            {
                this.autoProcessor.Rollback();
                this.hisSereServExtProcessor.Rollback();
                this.sereServProcessor.Rollback();
                this.medicineProcessor.Rollback();
                this.materialProcessor.Rollback();
                this.hisExpMestProcessor.Rollback();
                this.hisServiceReqProcessor.Rollback();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
