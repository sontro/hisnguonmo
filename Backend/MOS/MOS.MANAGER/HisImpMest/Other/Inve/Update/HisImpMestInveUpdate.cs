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

namespace MOS.MANAGER.HisImpMest.Other.Inve.Update
{
    partial class HisImpMestInveUpdate : BusinessBase
    {
        private HisImpMestUpdate hisImpMestUpdate;
        private MaterialProcessor materialProcessor;
        private MedicineProcessor medicineProcessor;
        private BloodProcessor bloodProcessor;
        private MaterialReusableProcessor materialReusableProcesor;
        private HisImpMestAutoProcess hisImpMestAutoProcess;

        private HIS_IMP_MEST recentHisImpMest;
        private List<HisMedicineWithPatySDO> recentMedicineSDOs;
        private List<HisMaterialWithPatySDO> recentMaterialSDOs;
        private List<HIS_BLOOD> recentBloods;

        internal HisImpMestInveUpdate()
            : base()
        {
            this.Init();
        }

        internal HisImpMestInveUpdate(CommonParam param)
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
            this.materialReusableProcesor = new MaterialReusableProcessor(param);
            this.hisImpMestAutoProcess = new HisImpMestAutoProcess(param);
        }

        internal bool Update(HisImpMestInveSDO data, ref HisImpMestInveSDO resultData)
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
                valid = valid && checker.CheckMaterialTypeReusable(data.InveMaterials, ref reusMaterials, ref materialTypes, ref serialNumbers);
                valid = valid && checker.CheckDuplicateSerialNumber(serialNumbers, data.ImpMest.ID);
                if (valid)
                {
                    List<string> sqls = new List<string>();
                    this.Validata(data);

                    var inveMaterials = data.InveMaterials != null ? data.InveMaterials.Where(o => reusMaterials == null || !reusMaterials.Contains(o)).ToList() : null;
                    impMestMaterials = new HisImpMestMaterialGet().GetByImpMestId(data.ImpMest.ID);
                    if (IsNotNullOrEmpty(impMestMaterials))
                    {
                        hisMaterials = new HisMaterialGet().GetByIds(impMestMaterials.Select(s => s.MATERIAL_ID).Distinct().ToList());
                    }

                    this.ProcessHisImpMest(data);

                    if (!this.materialProcessor.Run(this.recentHisImpMest, inveMaterials, hisMaterials, impMestMaterials, materialTypes, ref this.recentMaterialSDOs, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.materialReusableProcesor.Run(this.recentHisImpMest, reusMaterials, hisMaterials, materialTypes, ref this.recentMaterialSDOs, ref sqls))
                    {
                        throw new Exception("materialReusableProcesor. Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.medicineProcessor.Run(this.recentHisImpMest, data.InveMedicines, ref this.recentMedicineSDOs, ref sqls))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    if (!this.bloodProcessor.Run(this.recentHisImpMest, data.InveBloods, ref this.recentBloods, ref sqls))
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
                result = false;
                resultData = null;
            }
            return result;
        }

        private void Validata(HisImpMestInveSDO data)
        {
            if (!IsNotNullOrEmpty(data.InveMaterials) && !IsNotNullOrEmpty(data.InveMedicines) && !IsNotNullOrEmpty(data.InveBloods))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                throw new Exception("Khong ton tai du lieu thuoc hay vat tu, mau de xuat" + LogUtil.TraceData("data", data));
            }

            if (IsNotNullOrEmpty(data.InveMedicines))
            {
                if (data.InveMedicines.Exists(e => (e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MedicinePaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MedicinePaties");
                }
                if (data.InveMedicines.Exists(e => (!e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MedicinePaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MedicinePaties");
                }

                foreach (var medi in data.InveMedicines)
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

            if (IsNotNullOrEmpty(data.InveMaterials))
            {
                if (data.InveMaterials.Exists(e => (e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Material.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MaterialPaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MaterialPaties");
                }
                if (data.InveMaterials.Exists(e => (!e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Material.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MaterialPaties)))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MaterialPaties");
                }

                foreach (var mate in data.InveMaterials)
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

        private void ProcessHisImpMest(HisImpMestInveSDO data)
        {

            List<HIS_MEDICINE> medicines = null;
            List<HIS_MATERIAL> materials = null;

            if (IsNotNullOrEmpty(data.InveMedicines))
            {
                medicines = data.InveMedicines.Select(s => s.Medicine).ToList();
            }

            if (IsNotNullOrEmpty(data.InveMaterials))
            {
                materials = data.InveMaterials.Select(s => s.Material).ToList();
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
        private void PassResult(ref HisImpMestInveSDO resultData)
        {
            resultData = new HisImpMestInveSDO();
            resultData.ImpMest = this.recentHisImpMest;
            resultData.InveMedicines = this.recentMedicineSDOs;
            resultData.InveMaterials = this.recentMaterialSDOs;
            resultData.InveBloods = this.recentBloods;
        }

        private void Rollback()
        {
            try
            {
                this.bloodProcessor.Rollback();
                this.medicineProcessor.Rollback();
                this.materialReusableProcesor.Rollback();
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
