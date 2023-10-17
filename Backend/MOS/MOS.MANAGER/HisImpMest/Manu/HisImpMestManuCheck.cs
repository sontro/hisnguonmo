using Inventec.Common.Logging;
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

namespace MOS.MANAGER.HisImpMest.Manu
{
    class HisImpMestManuCheck : BusinessBase
    {
        internal HisImpMestManuCheck()
            : base()
        {

        }

        internal HisImpMestManuCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HIS_IMP_MEST data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!data.SUPPLIER_ID.HasValue || !IsGreaterThanZero(data.SUPPLIER_ID.Value)) throw new ArgumentNullException("data.SUPPLIER_ID");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
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

        internal bool VerifyValidImpMediStock(HIS_IMP_MEST data, ref V_HIS_MEDI_STOCK stock)
        {
            bool valid = true;
            try
            {
                //kiem tra neu loai nhap kho la nhap tu nha cung cap (NCC) thi phai kiem tra kho nhap co cho phep nhap tu NCC hay ko
                stock = HisMediStockCFG.DATA.Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE && o.ID == data.MEDI_STOCK_ID).FirstOrDefault();
                if (stock == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    LogSystem.Error("Khong ton tai kho tuong ung voi id " + data.MEDI_STOCK_ID);
                    valid = false;
                }
                else if (stock.IS_ALLOW_IMP_SUPPLIER == null || stock.IS_ALLOW_IMP_SUPPLIER != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_KhoKhongChoPhepNhapTuNhaCungCap);
                    LogSystem.Error("Kho ko cho phep nhap tu nha cung cap. ID kho: " + data.MEDI_STOCK_ID);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool Validata(HisImpMestManuSDO data, ref long? medicalContractId)
        {
            bool valid = true;
            try
            {
                if (!IsNotNullOrEmpty(data.ManuMaterials)
                && !IsNotNullOrEmpty(data.ManuMedicines)
                && !IsNotNullOrEmpty(data.ManuBloods))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Khong ton tai du lieu thuoc/mau hay vat tu de xuat" + LogUtil.TraceData("data", data));
                }
                if (IsNotNullOrEmpty(data.ManuMedicines))
                {
                    if (data.ManuMedicines.Exists(e => (e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MedicinePaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MedicinePaties");
                    }
                    if (data.ManuMedicines.Exists(e => (!e.Medicine.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Medicine.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MedicinePaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Medicine co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MedicinePaties");
                    }

                    if (data.ManuMedicines.Any(a => a.Medicine.CONTRACT_PRICE.HasValue && !a.Medicine.MEDICAL_CONTRACT_ID.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Medicine co CONTRACT_PRICE <> NULL nhung khong co MEDICAL_CONTRACT_ID");
                    }

                    foreach (var medi in data.ManuMedicines)
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

                if (IsNotNullOrEmpty(data.ManuMaterials))
                {
                    if (data.ManuMaterials.Exists(e => (e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue && e.Material.IS_SALE_EQUAL_IMP_PRICE.Value == MOS.UTILITY.Constant.IS_TRUE) && IsNotNullOrEmpty(e.MaterialPaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = 1 nhung van co MaterialPaties");
                    }
                    if (data.ManuMaterials.Exists(e => (!e.Material.IS_SALE_EQUAL_IMP_PRICE.HasValue || e.Material.IS_SALE_EQUAL_IMP_PRICE.Value != MOS.UTILITY.Constant.IS_TRUE) && !IsNotNullOrEmpty(e.MaterialPaties)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Material co IS_SALE_EQUAL_IMP_PRICE = NULL nhung khong co MaterialPaties");
                    }

                    if (data.ManuMaterials.Any(a => a.Material.CONTRACT_PRICE.HasValue && !a.Material.MEDICAL_CONTRACT_ID.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai Material co CONTRACT_PRICE <> NULL nhung khong co MEDICAL_CONTRACT_ID");
                    }

                    foreach (var mate in data.ManuMaterials)
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
                                    return false;
                                }
                            }
                        }
                    }
                }

                List<long> lstMedicalContractIds = new List<long>();
                if (data.ManuMaterials != null && data.ManuMaterials.Count > 0)
                {
                    List<long> ids = data.ManuMaterials.Where(o => o.Material != null && o.Material.MEDICAL_CONTRACT_ID.HasValue).Select(s => s.Material.MEDICAL_CONTRACT_ID.Value).ToList();
                    if (IsNotNullOrEmpty(ids))
                    {
                        lstMedicalContractIds.AddRange(ids);
                    }
                }

                if (data.ManuMedicines != null && data.ManuMedicines.Count > 0)
                {
                    List<long> ids = data.ManuMedicines.Where(o => o.Medicine != null && o.Medicine.MEDICAL_CONTRACT_ID.HasValue).Select(s => s.Medicine.MEDICAL_CONTRACT_ID.Value).ToList();

                    if (IsNotNullOrEmpty(ids))
                    {
                        lstMedicalContractIds.AddRange(ids);
                    }
                }

                if (lstMedicalContractIds != null && lstMedicalContractIds.Distinct().ToList().Count > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_TonTaiHon1HopDong);
                    return false;
                }
                medicalContractId = IsNotNullOrEmpty(lstMedicalContractIds) ? (long?)lstMedicalContractIds[0] : null;
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
