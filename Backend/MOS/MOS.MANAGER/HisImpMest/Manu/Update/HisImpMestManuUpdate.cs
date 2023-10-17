using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;

namespace MOS.MANAGER.HisImpMest.Manu.Update
{
    partial class HisImpMestManuUpdate : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private List<HisMaterialWithPatySDO> recentMaterialSDOs;
        private List<HisMedicineWithPatySDO> recentMedicineSDOs;
        private List<HIS_BLOOD> recentBloods;

        private HisImpMestUpdate hisImpMestUpdate;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;
        private MaterialReusableProcessor materialReusableProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestManuUpdate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestManuUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.materialReusableProcessor = new MaterialReusableProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        /// <summary>
        /// Cap nhat thong tin phieu nhap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Update(HisImpMestManuSDO data, ref HisImpMestManuSDO resultData)
        {
            bool result = true;
            try
            {
                bool valid = true;
                V_HIS_MEDI_STOCK hisMediStock = null;
                List<HIS_MATERIAL_TYPE> materialTypes = null;
                List<HIS_IMP_MEST_MATERIAL> impMestMaterials = null;
                List<HIS_MATERIAL> hisMaterials = null;
                List<HisMaterialWithPatySDO> reusMaterials = null;
                List<string> serialNumbers = new List<string>();
                long? medicalContractId = null;

                HisImpMestManuCheck checker = new HisImpMestManuCheck(param);
                HisImpMestCheck commonChecker = new HisImpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.ImpMest);
                valid = valid && checker.VerifyRequireField(data.ImpMest);
                valid = valid && checker.Validata(data, ref medicalContractId);
                valid = valid && commonChecker.IsValidMediStock(data.ImpMest, ref hisMediStock);
                valid = valid && commonChecker.CheckMaterialTypeReusable(data.ManuMaterials, ref reusMaterials, ref materialTypes, ref serialNumbers);
                valid = valid && commonChecker.CheckDuplicateSerialNumber(serialNumbers, data.ImpMest.ID);
                if (valid)
                {
                    var manuMaterials = data.ManuMaterials != null ? data.ManuMaterials.Where(o => reusMaterials == null || !reusMaterials.Contains(o)).ToList() : null;
                    impMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(data.ImpMest.ID);
                    if (IsNotNullOrEmpty(impMestMaterials))
                    {
                        hisMaterials = new HisMaterialGet().GetByIds(impMestMaterials.Select(s => s.MATERIAL_ID).Distinct().ToList());
                    }

                    List<string> sqls = new List<string>();
                    this.ProcessHisImpMest(data, medicalContractId);

                    //Xu ly vat tu
                    if (!this.materialProcessor.Run(this.recentHisImpMest, manuMaterials, hisMaterials, impMestMaterials, materialTypes, ref this.recentMaterialSDOs, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Kiem tra du lieu");
                    }

                    //Xu ly vat tu tai su dung
                    if (!this.materialReusableProcessor.Run(this.recentHisImpMest, reusMaterials, hisMaterials, materialTypes, ref this.recentMaterialSDOs, ref sqls))
                    {
                        throw new Exception("materialReusableProcessor. Ket thuc nghiep vu. Kiem tra du lieu");
                    }

                    //Xu ly thuoc
                    if (!this.medicineProcessor.Run(this.recentHisImpMest, data.ManuMedicines, ref this.recentMedicineSDOs, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Kiem tra du lieu");
                    }

                    //Xu ly mau
                    if (!this.bloodProcessor.Run(this.recentHisImpMest, data.ManuBloods, ref this.recentBloods, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Kiem tra du lieu");
                    }

                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Sql Execute. sql: " + sqls.ToString());
                    }

                    new EventLogGenerator(EventLog.Enum.HisImpMest_SuaPhieuNhap).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).Run();

                    this.ProcessAuto();

                    this.PassResult(ref resultData);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                result = false;
                resultData = null;
            }
            return result;
        }

        /// <summary>
        /// Xu ly thong tin phieu nhap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void ProcessHisImpMest(HisImpMestManuSDO data, long? medicalContractId)
        {
            List<HIS_MEDICINE> medicines = null;
            List<HIS_MATERIAL> materials = null;

            if (IsNotNullOrEmpty(data.ManuMedicines))
            {
                medicines = data.ManuMedicines.Select(s => s.Medicine).ToList();
            }

            if (IsNotNullOrEmpty(data.ManuMaterials))
            {
                materials = data.ManuMaterials.Select(s => s.Material).ToList();
            }

            data.ImpMest.MEDICAL_CONTRACT_ID = medicalContractId;
            HisImpMestUtil.SetTdl(data.ImpMest, medicines, materials);

            if (!this.hisImpMestUpdate.Update(data.ImpMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = data.ImpMest;
        }

        private void ProcessAuto()
        {
            try
            {
                this.hisImpMestAutoProcess.Run(this.recentHisImpMest);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        /// <summary>
        /// Truyen ket qua thong qua bien "data"
        /// </summary>
        /// <param name="data"></param>
        private void PassResult(ref HisImpMestManuSDO resultData)
        {
            resultData = new HisImpMestManuSDO();
            resultData.ImpMest = this.recentHisImpMest;
            resultData.ManuMedicines = this.recentMedicineSDOs;
            resultData.ManuMaterials = this.recentMaterialSDOs;
            resultData.ManuBloods = this.recentBloods;
        }

        private void Rollback()
        {
            try
            {
                this.bloodProcessor.Rollback();
                this.medicineProcessor.Rollback();
                this.materialReusableProcessor.Rollback();
                this.materialProcessor.Rollback();
                this.hisImpMestUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}