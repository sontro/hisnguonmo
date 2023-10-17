using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMest.Manu.Create
{
    /// <summary>
    /// Nhap tu nha cung cap
    /// </summary>
    partial class HisImpMestManuCreate : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private List<HisMedicineWithPatySDO> recentImpMedicineSDOs;
        private List<HisMaterialWithPatySDO> recentImpMaterialSDOs;
        private List<HIS_BLOOD> recentBloods;

        private HisImpMestCreate hisImpMestCreate;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;
        private MaterialReusableProcessor materialReusableProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        private bool isBusiness = false;

        internal HisImpMestManuCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestManuCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.materialReusableProcessor = new MaterialReusableProcessor(param);
        }

        /// <summary>
        /// Tao yeu cau nhap
        /// </summary>
        /// <param name="HisManuImpMestSDO"></param>
        /// <returns></returns>
        internal bool Create(HisImpMestManuSDO data, ref HisImpMestManuSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_MEDI_STOCK hisMediStock = null;
                List<HIS_MATERIAL_TYPE> materialTypes = null;
                List<HisMaterialWithPatySDO> reusMaterials = null;
                List<string> serialNumbers = new List<string>();
                HisImpMestManuCheck checker = new HisImpMestManuCheck(param);
                HisImpMestCheck commonChecker = new HisImpMestCheck(param);
                long? medicalContractId = null;
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.ImpMest);
                valid = valid && checker.VerifyRequireField(data.ImpMest);
                valid = valid && checker.VerifyValidImpMediStock(data.ImpMest, ref hisMediStock);
                valid = valid && checker.Validata(data, ref medicalContractId);
                valid = valid && commonChecker.CheckMaterialTypeReusable(data.ManuMaterials, ref reusMaterials, ref materialTypes, ref serialNumbers);
                valid = valid && commonChecker.CheckDuplicateSerialNumber(serialNumbers, null);
                if (valid)
                {
                    this.isBusiness = hisMediStock.IS_BUSINESS == MOS.UTILITY.Constant.IS_TRUE;
                    var manuMaterials = data.ManuMaterials != null ? data.ManuMaterials.Where(o => reusMaterials == null || !reusMaterials.Contains(o)).ToList() : null;
                    //Tao moi HisImpMest
                    this.ProcessHisImpMest(data, medicalContractId);

                    //Xu ly vat tu
                    if (!this.materialProcessor.Run(manuMaterials, this.recentHisImpMest, materialTypes, ref this.recentImpMaterialSDOs,data))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly vat tu tai su dung
                    if (!this.materialReusableProcessor.Run(reusMaterials, this.recentHisImpMest, materialTypes, ref this.recentImpMaterialSDOs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly thuoc
                    if (!this.medicineProcessor.Run(data.ManuMedicines, this.recentHisImpMest, ref this.recentImpMedicineSDOs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly vat tu
                    if (!this.bloodProcessor.Run(data.ManuBloods, this.recentHisImpMest, ref this.recentBloods))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    new EventLogGenerator(EventLog.Enum.HisImpMest_TaoPhieuNhap).ImpMestCode(this.recentHisImpMest.IMP_MEST_CODE).Run();

                    this.ProcessAuto();

                    this.PassResult(ref resultData);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }
            return result;
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
        /// Xu ly thong tin phieu nhap
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private void ProcessHisImpMest(HisImpMestManuSDO data, long? medicalContractId)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST hisImpMest = Mapper.Map<HIS_IMP_MEST>(data.ImpMest);
            hisImpMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__NCC;
            hisImpMest.MEDICAL_CONTRACT_ID = medicalContractId;

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

            HisImpMestUtil.SetTdl(hisImpMest, medicines, materials);

            if (!this.hisImpMestCreate.Create(hisImpMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = hisImpMest;
        }

        /// <summary>
        /// Rollback du lieu
        /// </summary>
        /// <returns></returns>
        internal void RollbackData()
        {
            this.bloodProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.materialReusableProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }

        /// <summary>
        /// Truyen ket qua thong qua bien "data"
        /// </summary>
        /// <param name="data"></param>
        private void PassResult(ref HisImpMestManuSDO data)
        {
            data = new HisImpMestManuSDO();
            //truy van lai de lay du lieu IMP_MEST_CODE tra ve client, phuc vu in
            data.ImpMest = new HisImpMestGet().GetById(this.recentHisImpMest.ID);
            data.ManuMedicines = this.recentImpMedicineSDOs;
            data.ManuMaterials = this.recentImpMaterialSDOs;
            data.ManuBloods = this.recentBloods;
        }
    }
}
