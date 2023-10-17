using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisBidMaterialType;
using MOS.MANAGER.HisBidMedicineType;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMaterialType;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineType;
using MOS.MANAGER.HisMediContractMaty;
using MOS.MANAGER.HisMediContractMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicalContract
{
    partial class HisMedicalContractCheck : BusinessBase
    {

        internal bool VerifyRequireField(HisMedicalContractSDO data, bool isUpdate)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.SupplierId <= 0) throw new ArgumentNullException("data.SupplierId");
                if (String.IsNullOrWhiteSpace(data.MedicalContractCode)) throw new ArgumentNullException("data.MedicalContractCode");
                if (isUpdate && (!data.Id.HasValue || data.Id.Value <= 0)) throw new ArgumentNullException("data.Id");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool ValidData(HisMedicalContractSDO data)
        {
            bool valid = true;
            try
            {
                if (data.BidId.HasValue)
                {
                    if (data.MaterialTypes != null && data.MaterialTypes.Any(a => !a.BidMaterialTypeId.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong co thong tin thau (BidId), nhung chi tiet vat tu khong co chi tiet thau (BidMaterialTypeId)");
                    }

                    if (data.MedicineTypes != null && data.MedicineTypes.Any(a => !a.BidMedicineTypeId.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong co thong tin thau (BidId), nhung chi tiet thuoc khong co chi tiet thau (BidMedicineTypeId)");
                    }
                }
                else
                {
                    if (data.MaterialTypes != null && data.MaterialTypes.Any(a => a.BidMaterialTypeId.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong khong co thong tin thau (BidId), nhung chi tiet vat tu co chi tiet thau (BidMaterialTypeId)");
                    }

                    if (data.MedicineTypes != null && data.MedicineTypes.Any(a => a.BidMedicineTypeId.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong khong co thong tin thau (BidId), nhung chi tiet thuoc co chi tiet thau (BidMedicineTypeId)");
                    }
                }

                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    if (data.MaterialTypes.Any(a => a.Amount <= 0 || !a.ImpPrice.HasValue || !a.ImpVatRatio.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Chi tiet vat tu thieu thong tin so luong hoac gia nhap hoac vat nhap");
                    }

                    //if (data.MaterialTypes.GroupBy(g => new { g.MaterialTypeId, g.ContractPrice, g.BidMaterialTypeId }).Any(a => a.Count() > 1))
                    //{
                    //    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    //    throw new Exception("Ton tai nhieu HisMediContractMaty cung mot MaterialTypeId");
                    //}
                }

                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    if (data.MedicineTypes.Any(a => a.Amount <= 0 || !a.ImpPrice.HasValue || !a.ImpVatRatio.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Chi tiet thuoc thieu thong tin so luong hoac gia nhap hoac vat nhap");
                    }

                    //if (data.MedicineTypes.GroupBy(g => new { g.MedicineTypeId, g.ContractPrice, g.BidMedicineTypeId }).Any(a => a.Count() > 1))
                    //{
                    //    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    //    throw new Exception("Ton tai nhieu HisMediContractMety cung mot MedicineTypeId");
                    //}
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyBid(HisMedicalContractSDO data, ref List<HIS_BID_MATERIAL_TYPE> bidMaterialTypes, ref List<HIS_BID_MEDICINE_TYPE> bidMedicineTypes)
        {
            bool valid = true;
            try
            {
                if (data.BidId.HasValue)
                {
                    List<string> matyErrors = new List<string>();
                    List<string> metyErrors = new List<string>();
                    List<string> matyErrorPrices = new List<string>();
                    List<string> metyErrorPrices = new List<string>();
                    if (IsNotNullOrEmpty(data.MaterialTypes))
                    {
                        List<long> bidMaterialIds = data.MaterialTypes.Select(s => s.BidMaterialTypeId.Value).ToList();
                        HisBidMaterialTypeFilterQuery mateFilter = new HisBidMaterialTypeFilterQuery();
                        mateFilter.IDs = bidMaterialIds;
                        bidMaterialTypes = new HisBidMaterialTypeGet().Get(mateFilter);
                        if (!IsNotNullOrEmpty(bidMaterialTypes))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HisBidMaterialType: " + LogUtil.TraceData("bidMaterialIds", bidMaterialIds));
                        }

                        if (bidMaterialTypes.Any(a => a.BID_ID != data.BidId.Value))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("HIS_BID_MATERIAL_TYPE khong thuoc goi thau cua hop dong: " + LogUtil.TraceData("bidMaterialTypes", bidMaterialTypes.Where(o => o.BID_ID != data.BidId.Value).ToList()));
                        }

                        if (bidMaterialTypes.Any(a => a.SUPPLIER_ID != data.SupplierId))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("SUPPLIER_ID HisBidMaterialType khac SUPPLIER_ID trong hop dong");
                        }

                        var group = data.MaterialTypes.GroupBy(g => g.BidMaterialTypeId.Value).ToList();

                        foreach (var g in group)
                        {
                            HIS_BID_MATERIAL_TYPE type = bidMaterialTypes.FirstOrDefault(o => o.ID == g.Key);
                            if (type == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("BidMaterialTypeId Invalid: " + g.Key);
                            }

                            if (g.Any(a => type.MATERIAL_TYPE_ID != a.MaterialTypeId))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("MaterialTypeId khac MATERIAL_TYPE_ID trong HIS_BID_MATERIAL_TYPE: " + LogUtil.TraceData("sdo", g.ToList()));
                            }

                            decimal contractAmount = ((type.TDL_CONTRACT_AMOUNT ?? 0) + g.Sum(s => s.Amount));
                            decimal typeAmount = type.AMOUNT * (1 + (type.IMP_MORE_RATIO ?? 0));
                            if (typeAmount < contractAmount)
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MaterialTypeId);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrors.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_SoLuongHopDongVaThau, param.LanguageCode), name, contractAmount, typeAmount));
                                continue;
                            }

                            if (g.Any(a => (type.IMP_PRICE ?? 0) < (a.ImpPrice ?? 0)))
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MaterialTypeId);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_GiaNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_PRICE ?? 0), (g.First().ImpPrice ?? 0)));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_VAT_RATIO ?? 0) < (a.ImpVatRatio ?? 0)))
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MaterialTypeId);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_VatNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_VAT_RATIO ?? 0), (g.First().ImpVatRatio ?? 0)));
                                continue;
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(data.MedicineTypes))
                    {
                        List<long> bidMedicineIds = data.MedicineTypes.Select(s => s.BidMedicineTypeId.Value).ToList();
                        HisBidMedicineTypeFilterQuery mediFilter = new HisBidMedicineTypeFilterQuery();
                        mediFilter.IDs = bidMedicineIds;
                        bidMedicineTypes = new HisBidMedicineTypeGet().Get(mediFilter);
                        if (!IsNotNullOrEmpty(bidMedicineTypes))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HisBidMedicineType: " + LogUtil.TraceData("bidMedicineIds", bidMedicineIds));
                        }

                        if (bidMedicineTypes.Any(a => a.BID_ID != data.BidId.Value))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("HIS_BID_MEDICINE_TYPE khong thuoc goi thau cua hop dong: " + LogUtil.TraceData("bidMedicineTypes", bidMedicineTypes.Where(o => o.BID_ID != data.BidId.Value).ToList()));
                        }

                        if (bidMedicineTypes.Any(a => a.SUPPLIER_ID != data.SupplierId))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("SUPPLIER_ID HisBidMedicineType khac SUPPLIER_ID trong hop dong");
                        }

                        var group = data.MedicineTypes.GroupBy(g => g.BidMedicineTypeId.Value).ToList();

                        foreach (var g in group)
                        {
                            HIS_BID_MEDICINE_TYPE type = bidMedicineTypes.FirstOrDefault(o => o.ID == g.Key);
                            if (type == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("BidMedicineTypeId Invalid: " + g.Key);
                            }

                            if (g.Any(a => type.MEDICINE_TYPE_ID != a.MedicineTypeId))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("MedicineTypeId khac MEDICINE_TYPE_ID trong HIS_BID_MEDICINE_TYPE: " + LogUtil.TraceData("sdo", g.ToList()));
                            }


                            decimal contractAmount = ((type.TDL_CONTRACT_AMOUNT ?? 0) + g.Sum(s => s.Amount));
                            decimal typeAmount = type.AMOUNT * (1 + (type.IMP_MORE_RATIO ?? 0));
                            if (typeAmount < contractAmount)
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MedicineTypeId);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrors.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_SoLuongHopDongVaThau, param.LanguageCode), name, contractAmount, typeAmount));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_PRICE ?? 0) < (a.ImpPrice ?? 0)))
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MedicineTypeId);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_GiaNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_PRICE ?? 0), (g.First().ImpPrice ?? 0)));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_VAT_RATIO ?? 0) < (a.ImpVatRatio ?? 0)))
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MedicineTypeId);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_VatNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_VAT_RATIO ?? 0), (g.First().ImpVatRatio ?? 0)));
                                continue;
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(matyErrors) || IsNotNullOrEmpty(metyErrors))
                    {
                        string vatu = IsNotNullOrEmpty(matyErrors) ? String.Join("; ", matyErrors) : "";
                        string thuoc = IsNotNullOrEmpty(metyErrors) ? String.Join("; ", metyErrors) : "";

                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicalContract_ThuocVatTuCoSoLuongHDLonHonSoLuongThau, vatu, thuoc);
                        return false;
                    }
                    if (IsNotNullOrEmpty(matyErrorPrices) || IsNotNullOrEmpty(metyErrorPrices))
                    {
                        string vatu = IsNotNullOrEmpty(matyErrorPrices) ? String.Join("; ", matyErrorPrices) : "";
                        string thuoc = IsNotNullOrEmpty(metyErrorPrices) ? String.Join("; ", metyErrorPrices) : "";

                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicalContract_ThuocVatTuCoGiaHoacVatHDLonHonGiaHoacVatThau, vatu, thuoc);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyBidUpdate(HisMedicalContractSDO data, ref List<HIS_BID_MATERIAL_TYPE> bidMaterialTypes, ref List<HIS_BID_MEDICINE_TYPE> bidMedicineTypes, List<HIS_MEDI_CONTRACT_MATY> oldMatys, List<HIS_MEDI_CONTRACT_METY> oldMetys)
        {
            bool valid = true;
            try
            {
                if (data.BidId.HasValue)
                {
                    List<string> matyErrors = new List<string>();
                    List<string> metyErrors = new List<string>();
                    List<string> matyErrorPrices = new List<string>();
                    List<string> metyErrorPrices = new List<string>();
                    if (IsNotNullOrEmpty(data.MaterialTypes))
                    {
                        List<long> bidMaterialIds = data.MaterialTypes.Select(s => s.BidMaterialTypeId.Value).ToList();
                        HisBidMaterialTypeFilterQuery mateFilter = new HisBidMaterialTypeFilterQuery();
                        mateFilter.IDs = bidMaterialIds;
                        bidMaterialTypes = new HisBidMaterialTypeGet().Get(mateFilter);
                        if (!IsNotNullOrEmpty(bidMaterialTypes))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HisBidMaterialType: " + LogUtil.TraceData("bidMaterialIds", bidMaterialIds));
                        }

                        if (bidMaterialTypes.Any(a => a.BID_ID != data.BidId.Value))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("HIS_BID_MATERIAL_TYPE khong thuoc goi thau cua hop dong: " + LogUtil.TraceData("bidMaterialTypes", bidMaterialTypes.Where(o => o.BID_ID != data.BidId.Value).ToList()));
                        }

                        if (bidMaterialTypes.Any(a => a.SUPPLIER_ID != data.SupplierId))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("SUPPLIER_ID HisBidMaterialType khac SUPPLIER_ID trong hop dong");
                        }

                        var group = data.MaterialTypes.GroupBy(g => g.BidMaterialTypeId.Value).ToList();

                        foreach (var g in group)
                        {
                            HIS_BID_MATERIAL_TYPE type = bidMaterialTypes.FirstOrDefault(o => o.ID == g.Key);
                            List<HIS_MEDI_CONTRACT_MATY> exists = oldMatys != null ? oldMatys.Where(o => o.BID_MATERIAL_TYPE_ID == g.Key).ToList() : null;

                            if (type == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("BidMaterialTypeId Invalid: " + g.Key);
                            }

                            if (g.Any(a => type.MATERIAL_TYPE_ID != a.MaterialTypeId))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("MaterialTypeId khac MATERIAL_TYPE_ID trong HIS_BID_MATERIAL_TYPE: " + LogUtil.TraceData("sdo", g.ToList()));
                            }

                            decimal oldAmount = exists != null ? exists.Sum(s => s.AMOUNT) : 0;
                            decimal contractAmount = ((type.TDL_CONTRACT_AMOUNT ?? 0) + (g.Sum(s => s.Amount) - oldAmount));
                            decimal typeAmount = type.AMOUNT * (1 + (type.IMP_MORE_RATIO ?? 0));
                            if (typeAmount < contractAmount)
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MaterialTypeId);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrors.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_SoLuongHopDongVaThau, param.LanguageCode), name, contractAmount, typeAmount));
                                continue;
                            }

                            if (g.Any(a => (type.IMP_PRICE ?? 0) < (a.ImpPrice ?? 0)))
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MaterialTypeId);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_GiaNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_PRICE ?? 0), (g.First().ImpPrice ?? 0)));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_VAT_RATIO ?? 0) < (a.ImpVatRatio ?? 0)))
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MaterialTypeId);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_VatNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_VAT_RATIO ?? 0), (g.First().ImpVatRatio ?? 0)));
                                continue;
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(data.MedicineTypes))
                    {
                        List<long> bidMedicineIds = data.MedicineTypes.Select(s => s.BidMedicineTypeId.Value).ToList();
                        HisBidMedicineTypeFilterQuery mediFilter = new HisBidMedicineTypeFilterQuery();
                        mediFilter.IDs = bidMedicineIds;
                        bidMedicineTypes = new HisBidMedicineTypeGet().Get(mediFilter);
                        if (!IsNotNullOrEmpty(bidMedicineTypes))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HisBidMedicineType: " + LogUtil.TraceData("bidMedicineIds", bidMedicineIds));
                        }

                        if (bidMedicineTypes.Any(a => a.BID_ID != data.BidId.Value))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("HIS_BID_MEDICINE_TYPE khong thuoc goi thau cua hop dong: " + LogUtil.TraceData("bidMedicineTypes", bidMedicineTypes.Where(o => o.BID_ID != data.BidId.Value).ToList()));
                        }

                        if (bidMedicineTypes.Any(a => a.SUPPLIER_ID != data.SupplierId))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("SUPPLIER_ID HisBidMedicineType khac SUPPLIER_ID trong hop dong");
                        }

                        var group = data.MedicineTypes.GroupBy(g => g.BidMedicineTypeId.Value).ToList();

                        foreach (var g in group)
                        {
                            HIS_BID_MEDICINE_TYPE type = bidMedicineTypes.FirstOrDefault(o => o.ID == g.Key);
                            List<HIS_MEDI_CONTRACT_METY> exists = oldMetys != null ? oldMetys.Where(o => o.BID_MEDICINE_TYPE_ID == g.Key).ToList() : null;
                            if (type == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("BidMedicineTypeId Invalid: " + g.Key);
                            }

                            if (g.Any(a => type.MEDICINE_TYPE_ID != a.MedicineTypeId))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("MedicineTypeId khac MEDICINE_TYPE_ID trong HIS_BID_MEDICINE_TYPE: " + LogUtil.TraceData("sdo", g.ToList()));
                            }


                            decimal oldAmount = exists != null ? exists.Sum(s => s.AMOUNT) : 0;
                            decimal contractAmount = ((type.TDL_CONTRACT_AMOUNT ?? 0) + (g.Sum(s => s.Amount) - oldAmount));
                            decimal typeAmount = type.AMOUNT * (1 + (type.IMP_MORE_RATIO ?? 0));
                            if (typeAmount < contractAmount)
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MedicineTypeId);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrors.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_SoLuongHopDongVaThau, param.LanguageCode), name, contractAmount, typeAmount));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_PRICE ?? 0) < (a.ImpPrice ?? 0)))
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MedicineTypeId);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_GiaNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_PRICE ?? 0), (g.First().ImpPrice ?? 0)));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_VAT_RATIO ?? 0) < (a.ImpVatRatio ?? 0)))
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MedicineTypeId);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_VatNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_VAT_RATIO ?? 0), (g.First().ImpVatRatio ?? 0)));
                                continue;
                            }
                        }
                    }

                    if (IsNotNullOrEmpty(matyErrors) || IsNotNullOrEmpty(metyErrors))
                    {
                        string vatu = IsNotNullOrEmpty(matyErrors) ? String.Join("; ", matyErrors) : "";
                        string thuoc = IsNotNullOrEmpty(metyErrors) ? String.Join("; ", metyErrors) : "";

                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicalContract_ThuocVatTuCoSoLuongHDLonHonSoLuongThau, vatu, thuoc);
                        return false;
                    }
                    if (IsNotNullOrEmpty(matyErrorPrices) || IsNotNullOrEmpty(metyErrorPrices))
                    {
                        string vatu = IsNotNullOrEmpty(matyErrorPrices) ? String.Join("; ", matyErrorPrices) : "";
                        string thuoc = IsNotNullOrEmpty(metyErrorPrices) ? String.Join("; ", metyErrorPrices) : "";

                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicalContract_ThuocVatTuCoGiaHoacVatHDLonHonGiaHoacVatThau, vatu, thuoc);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyType(HisMedicalContractSDO data, ref List<HIS_MATERIAL_TYPE> materialTypes, ref List<HIS_MEDICINE_TYPE> medicineTypes)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    List<long> materialIds = data.MaterialTypes.Select(s => s.MaterialTypeId).ToList();
                    materialTypes = HisMaterialTypeCFG.DATA.Where(o => materialIds.Contains(o.ID)).ToList();
                    if (!IsNotNullOrEmpty(materialTypes))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HisMaterialType: " + LogUtil.TraceData("materialIds", materialIds));
                    }

                    foreach (HisMediContractMatySDO sdo in data.MaterialTypes)
                    {
                        HIS_MATERIAL_TYPE type = materialTypes.FirstOrDefault(o => o.ID == sdo.MaterialTypeId);
                        if (type == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("MaterialTypeId Invalid: " + sdo.MaterialTypeId);
                        }
                    }
                }

                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    List<long> medicineIds = data.MedicineTypes.Select(s => s.MedicineTypeId).ToList();
                    medicineTypes = HisMedicineTypeCFG.DATA.Where(o => medicineIds.Contains(o.ID)).ToList();
                    if (!IsNotNullOrEmpty(medicineTypes))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HisMedicineType: " + LogUtil.TraceData("medicineIds", medicineIds));
                    }

                    foreach (HisMediContractMetySDO sdo in data.MedicineTypes)
                    {
                        HIS_MEDICINE_TYPE type = medicineTypes.FirstOrDefault(o => o.ID == sdo.MedicineTypeId);
                        if (type == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("MedicineTypeId Invalid: " + sdo.MedicineTypeId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool VerifyChangeInfo(HisMedicalContractSDO data, HIS_MEDICAL_CONTRACT raw, ref List<HIS_MEDI_CONTRACT_MATY> oldMatys, ref List<HIS_MEDI_CONTRACT_METY> oldMetys, ref List<HIS_MEDICINE> medicines, ref List<HIS_MATERIAL> materials)
        {
            bool valid = true;
            try
            {
                if (data.SupplierId != raw.SUPPLIER_ID)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Khong cho phep thay doi thong tin nha cung cap SUPPLIER_ID");
                    return false;
                }

                if ((data.BidId.HasValue && !raw.BID_ID.HasValue)
                    || (!data.BidId.HasValue && raw.BID_ID.HasValue)
                    || (data.BidId != raw.BID_ID))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Khong cho phep thay doi thong tin thau BID_ID");
                    return false;
                }

                oldMatys = new HisMediContractMatyGet().GetByMedicalContractId(raw.ID);
                oldMetys = new HisMediContractMetyGet().GetByMedicalContractId(raw.ID);

                //Lay ra cac lo thuoc/vat tu da duoc nhap tuong ung hop dong
                medicines = new HisMedicineGet().GetByMedicalContractId(raw.ID);
                materials = new HisMaterialGet().GetByMedicalContractId(raw.ID);

                //Lay ra cac loai thuoc da nhap nhung ko co trong d/s duoc gui len khi sua hop dong (bi xoa)
                List<long> notAllowDeleteMedicineTypeIds = IsNotNullOrEmpty(medicines) ? medicines.Where(o => data.MedicineTypes == null || !data.MedicineTypes.Exists(t => t.MedicineTypeId == o.MEDICINE_TYPE_ID)).Select(o => o.MEDICINE_TYPE_ID).ToList() : null;

                if (IsNotNullOrEmpty(notAllowDeleteMedicineTypeIds))
                {
                    List<HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetByIds(notAllowDeleteMedicineTypeIds);
                    List<string> medicineTypeNames = medicineTypes != null ? medicineTypes.Select(o => o.MEDICINE_TYPE_NAME).ToList() : null;
                    string medicineTypeNameStr = string.Join(",", medicineTypeNames);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicialContract_ThuocDaCoPhieuNhap, medicineTypeNameStr);
                    return false;
                }

                List<long> notAllowDeleteMaterialTypeIds = IsNotNullOrEmpty(materials) ? materials.Where(o => data.MaterialTypes == null || !data.MaterialTypes.Exists(t => t.MaterialTypeId == o.MATERIAL_TYPE_ID)).Select(o => o.MATERIAL_TYPE_ID).ToList() : null;
                if (IsNotNullOrEmpty(notAllowDeleteMaterialTypeIds))
                {
                    List<HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetByIds(notAllowDeleteMaterialTypeIds);
                    List<string> materialTypeNames = materialTypes != null ? materialTypes.Select(o => o.MATERIAL_TYPE_NAME).ToList() : null;
                    string materialTypeNameStr = string.Join(",", materialTypeNames);

                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicialContract_VatTuDaCoPhieuNhap, materialTypeNameStr);
                    return false;
                }

                //Lay ra cac loai thuoc da nhap ko cho giam so luong khi sua hop dong (so luong gui len nho hon so luong da nhap)
                if (IsNotNullOrEmpty(medicines))
                {
                    var group = medicines.GroupBy(o => o.MEDICINE_TYPE_ID).ToList();
                    var notAllowDecreaseMedicineTypes = group.Where(o => data.MedicineTypes == null || data.MedicineTypes.Exists(t => t.MedicineTypeId == o.Key && t.Amount < o.Sum(x => x.AMOUNT))).ToList();

                    if (IsNotNullOrEmpty(notAllowDecreaseMedicineTypes))
                    {
                        List<long> ids = notAllowDecreaseMedicineTypes.Select(o => o.Key).ToList();
                        List<HIS_MEDICINE_TYPE> medicineTypes = new HisMedicineTypeGet().GetByIds(ids);

                        string medicineTypeStr = "";

                        foreach (var g in notAllowDecreaseMedicineTypes)
                        {
                            string medicineTypeName = medicineTypes.Where(o => o.ID == g.Key).Select(o => o.MEDICINE_TYPE_NAME).FirstOrDefault();
                            decimal amount = g.Sum(o => o.AMOUNT);
                            medicineTypeStr += string.Format("{0} ({1})", medicineTypeName, amount);
                        }

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicialContract_ThuocDaNhapVoiSoLuongLonHon, medicineTypeStr);
                        return false;
                    }
                }

                //Lay ra cac loai vat tu da nhap ko cho giam so luong khi sua hop dong (so luong gui len nho hon so luong da nhap)
                if (IsNotNullOrEmpty(materials))
                {
                    var group = materials.GroupBy(o => o.MATERIAL_TYPE_ID).ToList();
                    var notAllowDecreaseMaterialTypes = group.Where(o => data.MaterialTypes == null || data.MaterialTypes.Exists(t => t.MaterialTypeId == o.Key && t.Amount < o.Sum(x => x.AMOUNT))).ToList();

                    if (IsNotNullOrEmpty(notAllowDecreaseMaterialTypes))
                    {
                        List<long> ids = notAllowDecreaseMaterialTypes.Select(o => o.Key).ToList();
                        List<HIS_MATERIAL_TYPE> materialTypes = new HisMaterialTypeGet().GetByIds(ids);

                        string materialTypeStr = "";

                        foreach (var g in notAllowDecreaseMaterialTypes)
                        {
                            string materialTypeName = materialTypes.Where(o => o.ID == g.Key).Select(o => o.MATERIAL_TYPE_NAME).FirstOrDefault();
                            decimal amount = g.Sum(o => o.AMOUNT);
                            materialTypeStr += string.Format("{0} ({1})", materialTypeName, amount);
                        }

                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicialContract_VatTuDaNhapVoiSoLuongLonHon, materialTypeStr);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsDuplicateData(List<HisMediContractMatySDO> listMediContractMaty, List<HisMediContractMetySDO> listMediContractMety)
        {
            bool valid = true;
            try
            {
                bool isDuplicateMaty = listMediContractMaty.GroupBy(o => new { o.MaterialTypeId, o.BidGroupCode, o.BidNumber, o.ContractPrice }).Any(g => g.Count() > 1);
                bool isDuplicateMety = listMediContractMety.GroupBy(o => new { o.MedicineTypeId, o.BidGroupCode, o.BidNumber, o.ContractPrice }).Any(g => g.Count() > 1);

                if (isDuplicateMaty || isDuplicateMety)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Du lieu trung MediContractMaty, MediContractMety: " + LogUtil.TraceData("isDuplicateMaty", isDuplicateMaty) + LogUtil.TraceData("listMediContractMety", listMediContractMety));
                    return false;
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
    }
}
