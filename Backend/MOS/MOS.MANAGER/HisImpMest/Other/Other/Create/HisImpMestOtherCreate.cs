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

namespace MOS.MANAGER.HisImpMest.Other.Other.Create
{
    partial class HisImpMestOtherCreate : BusinessBase
    {
        private HIS_IMP_MEST recentHisImpMest;
        private List<HisMaterialWithPatySDO> recentMaterialSDOs;
        private List<HisMedicineWithPatySDO> recentMedicineSDOs;
        private List<HIS_BLOOD> recentBloods;

        private HisImpMestCreate hisImpMestCreate;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;
        private MaterialReusableProcessor materialReusableProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        internal HisImpMestOtherCreate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestOtherCreate(CommonParam param)
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

        internal bool Create(HisImpMestOtherSDO data, ref HisImpMestOtherSDO resultData)
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
                valid = valid && checker.CheckMaterialTypeReusable(data.OtherMaterials, ref reusMaterials, ref materialTypes, ref serialNumbers);
                valid = valid && checker.CheckDuplicateSerialNumber(serialNumbers, null);
                if (valid)
                {
                    this.Validate(data);

                    var otherMaterials = data.OtherMaterials != null ? data.OtherMaterials.Where(o => reusMaterials == null || !reusMaterials.Contains(o)).ToList() : null;

                    this.ProcessHisImpMest(data);

                    //Xu ly vat tu
                    if (!this.materialProcessor.Run(otherMaterials, this.recentHisImpMest, materialTypes, ref this.recentMaterialSDOs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly vat tu tai su dung
                    if (!this.materialReusableProcessor.Run(reusMaterials, this.recentHisImpMest, materialTypes, ref this.recentMaterialSDOs))
                    {
                        throw new Exception("materialReusableProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly thuoc
                    if (!this.medicineProcessor.Run(data.OtherMedicines, this.recentHisImpMest, ref this.recentMedicineSDOs))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    //Xu ly vat tu
                    if (!this.bloodProcessor.Run(data.OtherBloods, this.recentHisImpMest, ref this.recentBloods))
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
                resultData = null;
            }
            return result;
        }

        private void Validate(HisImpMestOtherSDO data)
        {
            if (!IsNotNullOrEmpty(data.OtherMaterials) && !IsNotNullOrEmpty(data.OtherMedicines) && !IsNotNullOrEmpty(data.OtherBloods))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Khong ton tai du lieu thuoc hay vat tu, mau de xuat" + LogUtil.TraceData("data", data));
            }
            if (IsNotNullOrEmpty(data.OtherMedicines))
            {
                if (data.OtherMedicines.Exists(e => (e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MedicinePaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MedicinePaties");
                }
                if (data.OtherMedicines.Exists(e => (!e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MedicinePaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MedicinePaties");
                }

                foreach (var medi in data.OtherMedicines)
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

            if (IsNotNullOrEmpty(data.OtherMaterials))
            {
                if (data.OtherMaterials.Exists(e => (e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Material.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MaterialPaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MaterialPaties");
                }
                if (data.OtherMaterials.Exists(e => (!e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Material.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MaterialPaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MaterialPaties");
                }

                foreach (var mate in data.OtherMaterials)
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

        private void ProcessHisImpMest(HisImpMestOtherSDO data)
        {
            Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
            HIS_IMP_MEST hisImpMest = Mapper.Map<HIS_IMP_MEST>(data.ImpMest);
            hisImpMest.IMP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_TYPE.ID__KHAC;

            List<HIS_MEDICINE> medicines = null;
            List<HIS_MATERIAL> materials = null;

            if (IsNotNullOrEmpty(data.OtherMedicines))
            {
                medicines = data.OtherMedicines.Select(s => s.Medicine).ToList();
            }

            if (IsNotNullOrEmpty(data.OtherMaterials))
            {
                materials = data.OtherMaterials.Select(s => s.Material).ToList();
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
        private void PassResult(ref HisImpMestOtherSDO data)
        {
            data = new HisImpMestOtherSDO();
            //truy van lai de lay du lieu IMP_MEST_CODE tra ve client, phuc vu in
            data.ImpMest = new HisImpMestGet().GetById(this.recentHisImpMest.ID);
            data.OtherMedicines = this.recentMedicineSDOs;
            data.OtherMaterials = this.recentMaterialSDOs;
            data.OtherBloods = this.recentBloods;
        }

        internal void RollbackData()
        {
            this.medicineProcessor.Rollback();
            this.materialReusableProcessor.Rollback();
            this.materialProcessor.Rollback();
            this.bloodProcessor.Rollback();
            this.hisImpMestCreate.RollbackData();
        }
    }
}
