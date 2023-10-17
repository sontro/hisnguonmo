using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisImpMestMaterial;
using MOS.MANAGER.HisMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Other.Other.Update
{
    partial class HisImpMestOtherUpdate : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;
        private MaterialReusableProcessor materialReusableProcessor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        private HIS_IMP_MEST recentHisImpMest;
        private List<HisMedicineWithPatySDO> recentMedicineSDOs;
        private List<HisMaterialWithPatySDO> recentMaterialSDOs;
        private List<HIS_BLOOD> recentBloods;

        internal HisImpMestOtherUpdate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestOtherUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisImpMestUpdate = new HisImpMestUpdate(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.bloodProcessor = new BloodProcessor(param);
            this.materialReusableProcessor = new MaterialReusableProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool Update(HisImpMestOtherSDO data, ref HisImpMestOtherSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                V_HIS_MEDI_STOCK hisMediStock = null;
                List<HIS_MATERIAL_TYPE> materialTypes = null;
                List<HIS_IMP_MEST_MATERIAL> impMestMaterials = null;
                List<HIS_MATERIAL> hisMaterials = null;
                List<HisMaterialWithPatySDO> reusMaterials = null;
                List<string> serialNumbers = new List<string>();
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.ImpMest);
                valid = valid && checker.IsValidMediStock(data.ImpMest, ref hisMediStock);
                valid = valid && checker.CheckMaterialTypeReusable(data.OtherMaterials, ref reusMaterials, ref materialTypes, ref serialNumbers);
                valid = valid && checker.CheckDuplicateSerialNumber(serialNumbers, data.ImpMest.ID);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.Validata(data);

                    var otherMaterials = data.OtherMaterials != null ? data.OtherMaterials.Where(o => reusMaterials == null || !reusMaterials.Contains(o)).ToList() : null;
                    impMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(data.ImpMest.ID);
                    if (IsNotNullOrEmpty(impMestMaterials))
                    {
                        hisMaterials = new HisMaterialGet().GetByIds(impMestMaterials.Select(s => s.MATERIAL_ID).Distinct().ToList());
                    }

                    this.ProcessHisImpMest(data);

                    if (!this.materialProcessor.Run(this.recentHisImpMest, otherMaterials, hisMaterials, impMestMaterials, materialTypes, ref this.recentMaterialSDOs, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.materialReusableProcessor.Run(this.recentHisImpMest, reusMaterials, hisMaterials, materialTypes, ref this.recentMaterialSDOs, ref sqls))
                    {
                        throw new Exception("materialReusableProcessor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.medicineProcessor.Run(this.recentHisImpMest, data.OtherMedicines, ref this.recentMedicineSDOs, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.bloodProcessor.Run(this.recentHisImpMest, data.OtherBloods, ref this.recentBloods, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
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
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                this.Rollback();
                resultData = null;
                result = false;
            }
            return result;
        }

        private void Validata(HisImpMestOtherSDO data)
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
        private void PassResult(ref HisImpMestOtherSDO resultData)
        {
            resultData = new HisImpMestOtherSDO();
            resultData.ImpMest = this.recentHisImpMest;
            resultData.OtherMedicines = this.recentMedicineSDOs;
            resultData.OtherMaterials = this.recentMaterialSDOs;
            resultData.OtherBloods = this.recentBloods;
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
