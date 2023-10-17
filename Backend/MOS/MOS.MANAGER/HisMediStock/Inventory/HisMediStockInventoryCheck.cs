using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediStock.Inventory
{
    class HisMediStockInventoryCheck : BusinessBase
    {
        internal HisMediStockInventoryCheck()
            : base()
        {

        }

        internal HisMediStockInventoryCheck(CommonParam param)
            : base(param)
        {

        }


        internal bool VerifyRequireField(HisMediStockInventorySDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.MediStockId)) throw new ArgumentNullException("data.MediStockId");
                if (!IsGreaterThanZero(data.WorkingRoomId)) throw new ArgumentNullException("data.WorkingRoomId");
                if (!IsNotNullOrEmpty(data.ExpMaterials) && !IsNotNullOrEmpty(data.ExpMedicines) && !IsNotNullOrEmpty(data.ImpMaterials) && !IsNotNullOrEmpty(data.ImpMedicines)) throw new ArgumentNullException("data.ExpMaterials && data.ExpMedicines && data.ImpMaterials && data.ImpMedicines");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool ValidData(HisMediStockInventorySDO data)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.ExpMaterials))
                {
                    if (data.ExpMaterials.Any(a => a.Amount <= 0 || a.MaterialId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai du lieu ExpMaterials co Amount <= 0 hoac MaterialId <= 0 ");
                    }

                    if (data.ExpMaterials.GroupBy(g => g.MaterialId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception(" ExpMaterials ton tai du lieu trung MaterialId");
                    }
                }

                if (IsNotNullOrEmpty(data.ExpMedicines))
                {
                    if (data.ExpMedicines.Any(a => a.Amount <= 0 || a.MedicineId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai du lieu ExpMedicines co Amount <= 0 hoac MedicineId <= 0 ");
                    }

                    if (data.ExpMedicines.GroupBy(g => g.MedicineId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception(" ExpMedicines ton tai du lieu trung MedicineId");
                    }
                }

                if (IsNotNullOrEmpty(data.ImpMaterials))
                {
                    if (data.ImpMaterials.Any(a => a.Material == null || a.Material.AMOUNT <= 0 || a.Material.MATERIAL_TYPE_ID <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai du lieu ImpMaterials co Material = null hoac Material.AMOUNT <= 0 hoac Material.MATERIAL_TYPE_ID <= 0 ");
                    }
                    if (data.ImpMaterials.Exists(e => (e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Material.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MaterialPaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MaterialPaties");
                    }
                    if (data.ImpMaterials.Exists(e => (!e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Material.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MaterialPaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MaterialPaties");
                    }

                    foreach (var mate in data.ImpMaterials)
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

                if (IsNotNullOrEmpty(data.ImpMedicines))
                {
                    if (data.ImpMedicines.Any(a => a.Medicine == null || a.Medicine.AMOUNT <= 0 || a.Medicine.MEDICINE_TYPE_ID <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai du lieu ImpMedicines co Medicine = null hoac Medicine.AMOUNT <= 0 hoac Medicine.MEDICINE_TYPE_ID <= 0 ");
                    }
                    if (data.ImpMedicines.Exists(e => (e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MedicinePaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MedicinePaties");
                    }
                    if (data.ImpMedicines.Exists(e => (!e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MedicinePaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MedicinePaties");
                    }

                    foreach (var mate in data.ImpMedicines)
                    {
                        if (!mate.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue || mate.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE)
                        {
                            var Groups = mate.MedicinePaties.GroupBy(g => g.PATIENT_TYPE_ID);
                            foreach (var group in Groups)
                            {
                                if (group.ToList().Count > 1)
                                {
                                    HIS_MEDICINE_TYPE mety = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == mate.Medicine.MEDICINE_TYPE_ID);
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
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

    }
}
