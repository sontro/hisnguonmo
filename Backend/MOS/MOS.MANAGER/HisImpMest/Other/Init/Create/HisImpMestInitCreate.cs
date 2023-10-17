using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Init.Create
{
    partial class HisImpMestInitCreate : BusinessBase
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

        internal HisImpMestInitCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestInitCreate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestCreate = new HisImpMestCreate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.materialReusableProcessor = new MaterialReusableProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool Create(HisImpMestInitSDO data, ref HisImpMestInitSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_MEDI_STOCK hisMediStock = null;
                List<HIS_MATERIAL_TYPE> materialTypes = null;
                List<HisMaterialWithPatySDO> reusMaterials = null;
                List<string> serialNumbers = new List<string>();
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.ImpMest);
                valid = valid && checker.IsValidMediStock(data.ImpMest, ref hisMediStock);
                valid = valid && checker.CheckMaterialTypeReusable(data.InitMaterials, ref reusMaterials, ref materialTypes, ref serialNumbers);
                valid = valid && checker.CheckDuplicateSerialNumber(serialNumbers, null);
                if (valid)
                {
                    this.Validata(data);

                    var initMaterials = data.InitMaterials != null ? data.InitMaterials.Where(o => reusMaterials == null || !reusMaterials.Contains(o)).ToList() : null;

                    this.ProcessHisImpMest(data);

                    //Xu ly vat tu
                    if (!this.materialProcessor.Run(initMaterials, this.recentHisImpMest, materialTypes, ref this.recentImpMaterialSDOs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly vat tu tai su dung
                    if (!this.materialReusableProcessor.Run(reusMaterials, this.recentHisImpMest, materialTypes, ref this.recentImpMaterialSDOs))
                    {
                        throw new Exception("materialReusableProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly thuoc
                    if (!this.medicineProcessor.Run(data.InitMedicines, this.recentHisImpMest, ref this.recentImpMedicineSDOs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly vat tu
                    if (!this.bloodProcessor.Run(data.InitBloods, this.recentHisImpMest, ref this.recentBloods))
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
                Inventec.Common.Logging.LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void Validata(HisImpMestInitSDO data)
        {
            if (!IsNotNullOrEmpty(data.InitMaterials) && !IsNotNullOrEmpty(data.InitMedicines) && !IsNotNullOrEmpty(data.InitBloods))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Khong ton tai du lieu thuoc hay vat tu, mau de xuat" + LogUtil.TraceData("data", data));
            }
            if (IsNotNullOrEmpty(data.InitMedicines))
            {
                if (data.InitMedicines.Exists(e => (e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MedicinePaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MedicinePaties");
                }
                if (data.InitMedicines.Exists(e => (!e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MedicinePaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MedicinePaties");
                }

                foreach (var medi in data.InitMedicines)
                {
                    if (!medi.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue || medi.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE)
                    {
                        var Groups = medi.MedicinePaties.GroupBy(g => g.PATIENT_TYPE_ID);
                        foreach (var group in Groups)
                        {
                            if (group.ToList().Count > 1)
                            {
                                HIS_MEDICINE_TYPE mety = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == medi.Medicine.MEDICINE_TYPE_ID);
                                HIS_PATIENT_TYPE paty = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key);
                                string metyName = mety != null ? mety.MEDICINE_TYPE_NAME : null;
                                string patyName = paty != null ? paty.PATIENT_TYPE_NAME : null;
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_ThuocCoNhieuChinhSachGiaChoDoiTuong, metyName, patyName);
                                throw new Exception("Ton tai nhieu chinh sach gia thuoc cho mot doi tuong");
                            }
                        }
                    }
                }
            }

            if (IsNotNullOrEmpty(data.InitMaterials))
            {
                if (data.InitMaterials.Exists(e => (e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Material.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MaterialPaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MaterialPaties");
                }
                if (data.InitMaterials.Exists(e => (!e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Material.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MaterialPaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MaterialPaties");
                }

                foreach (var mate in data.InitMaterials)
                {
                    if (!mate.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue || mate.Material.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE)
                    {
                        var Groups = mate.MaterialPaties.GroupBy(g => g.PATIENT_TYPE_ID);
                        foreach (var group in Groups)
                        {
                            if (group.ToList().Count > 1)
                            {
                                HIS_MATERIAL_TYPE maty = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == mate.Material.MATERIAL_TYPE_ID);
                                HIS_PATIENT_TYPE paty = HisPatientTypeCFG.DATA.FirstOrDefault(o => o.ID == group.Key);
                                string matyName = maty != null ? maty.MATERIAL_TYPE_NAME : null;
                                string patyName = paty != null ? paty.PATIENT_TYPE_NAME : null;
                                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisImpMest_VatTuCoNhieuChinhSachGiaChoDoiTuong, matyName, patyName);
                                throw new Exception("Ton tai nhieu chinh sach gia vat tu cho mot doi tuong");
                            }
                        }
                    }
                }
            }
        }

        private void ProcessHisImpMest(HisImpMestInitSDO data)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST hisImpMest = Mapper.Map<HIS_IMP_MEST>(data.ImpMest);
            hisImpMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__DK;

            List<HIS_MEDICINE> medicines = null;
            List<HIS_MATERIAL> materials = null;

            if (IsNotNullOrEmpty(data.InitMedicines))
            {
                medicines = data.InitMedicines.Select(s => s.Medicine).ToList();
            }

            if (IsNotNullOrEmpty(data.InitMaterials))
            {
                materials = data.InitMaterials.Select(s => s.Material).ToList();
            }

            HisImpMestUtil.SetTdl(hisImpMest, medicines, materials);

            if (!this.hisImpMestCreate.Create(hisImpMest))
            {
                throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
            }
            this.recentHisImpMest = hisImpMest;
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
        private void PassResult(ref HisImpMestInitSDO data)
        {
            data = new HisImpMestInitSDO();
            //truy van lai de lay du lieu IMP_MEST_CODE tra ve client, phuc vu in
            data.ImpMest = new HisImpMestGet().GetById(this.recentHisImpMest.ID);
            data.InitMedicines = this.recentImpMedicineSDOs;
            data.InitMaterials = this.recentImpMaterialSDOs;
            data.InitBloods = this.recentBloods;
        }

        internal void RollbackData()
        {
            this.bloodProcessor.Rollback();
            this.materialReusableProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.medicineProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
