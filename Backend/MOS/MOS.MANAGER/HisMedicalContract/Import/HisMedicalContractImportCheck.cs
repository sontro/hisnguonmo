using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBidMaterialType;
using MOS.MANAGER.HisBidMedicineType;
using MOS.SDO;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalContract.Import
{
    class HisMedicalContractImportCheck : BusinessBase
    {
        internal HisMedicalContractImportCheck()
            : base()
        {
        }

        internal HisMedicalContractImportCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(HIS_MEDICAL_CONTRACT data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.SUPPLIER_ID <= 0) throw new ArgumentNullException("data.SUPPLIER_ID");
                if (String.IsNullOrWhiteSpace(data.MEDICAL_CONTRACT_CODE)) throw new ArgumentNullException("data.MEDICAL_CONTRACT_CODE");
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

        internal bool IsDuplicateMatyAndMety(ICollection<HIS_MEDI_CONTRACT_MATY> contractMaty, ICollection<HIS_MEDI_CONTRACT_METY> contractMety)
        {
            bool valid = true;
            try
            {
                if (contractMaty != null)
                {
                    bool isDuplicateMaty = contractMaty.GroupBy(o => new { o.MATERIAL_TYPE_ID, o.BID_GROUP_CODE, o.BID_NUMBER, o.CONTRACT_PRICE }).Any(g => g.Count() > 1);
                    if (isDuplicateMaty)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Du lieu trung MediContractMaty: " + LogUtil.TraceData("contractMaty", contractMaty));
                        return false;
                    }
                }
                if (contractMety != null)
                {
                    bool isDuplicateMety = contractMety.GroupBy(o => new { o.MEDICINE_TYPE_ID, o.BID_GROUP_CODE, o.BID_NUMBER, o.CONTRACT_PRICE }).Any(g => g.Count() > 1);
                    if (isDuplicateMety)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("Du lieu trung MediContractMety: " + LogUtil.TraceData("contractMety", contractMety));
                        return false;
                    }
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

        internal bool ValidData(HIS_MEDICAL_CONTRACT data)
        {
            bool valid = true;
            try
            {
                if (data.BID_ID.HasValue)
                {
                    if (data.HIS_MEDI_CONTRACT_MATY != null && data.HIS_MEDI_CONTRACT_MATY.Any(a => !a.BID_MATERIAL_TYPE_ID.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong co thong tin thau (BidId), nhung chi tiet vat tu khong co chi tiet thau (BID_MATERIAL_TYPE_ID)");
                    }

                    if (data.HIS_MEDI_CONTRACT_METY != null && data.HIS_MEDI_CONTRACT_METY.Any(a => !a.BID_MEDICINE_TYPE_ID.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong co thong tin thau (BidId), nhung chi tiet thuoc khong co chi tiet thau (BID_MEDICINE_TYPE_ID)");
                    }
                }
                else
                {
                    if (data.HIS_MEDI_CONTRACT_MATY != null && data.HIS_MEDI_CONTRACT_MATY.Any(a => a.BID_MATERIAL_TYPE_ID.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong khong co thong tin thau (BidId), nhung chi tiet vat tu co chi tiet thau (BID_MATERIAL_TYPE_ID)");
                    }

                    if (data.HIS_MEDI_CONTRACT_METY != null && data.HIS_MEDI_CONTRACT_METY.Any(a => a.BID_MEDICINE_TYPE_ID.HasValue))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Hop dong khong co thong tin thau (BidId), nhung chi tiet thuoc co chi tiet thau (BID_MEDICINE_TYPE_ID)");
                    }
                }

                if (data.HIS_MEDI_CONTRACT_MATY != null)
                {
                    if (data.HIS_MEDI_CONTRACT_MATY.Any(a => a.AMOUNT <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Chi tiet vat tu thieu thong tin so luong hoac gia nhap hoac vat nhap");
                    }
                }

                if (data.HIS_MEDI_CONTRACT_METY != null)
                {
                    if (data.HIS_MEDI_CONTRACT_METY.Any(a => a.AMOUNT <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Chi tiet thuoc thieu thong tin so luong hoac gia nhap hoac vat nhap");
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


        internal bool IsDuplicateOrExists(List<HIS_MEDICAL_CONTRACT> data)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data))
                {
                    // Check du lieu gui len co bi trung ma va ten hop dong
                    var groups = data.GroupBy(o => new { o.MEDICAL_CONTRACT_CODE, o.MEDICAL_CONTRACT_NAME });
                    List<string> duplicateNames = new List<string>();

                    foreach (var g in groups)
                    {
                        List<HIS_MEDICAL_CONTRACT> sdos = g.ToList();
                        if (sdos.Count > 1)
                        {
                            duplicateNames.Add(g.Key.MEDICAL_CONTRACT_NAME);
                        }
                    }
                    if (IsNotNullOrEmpty(duplicateNames))
                    {
                        string s = string.Join(",", duplicateNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicalContract_TrungDuLieuNhapKhauHopDongDuoc, s);
                        return false;
                    }

                    // Check da ton tai ma hop dong duoc trong db
                    var codes = data.Where(o => !string.IsNullOrWhiteSpace(o.MEDICAL_CONTRACT_CODE)).Select(s => s.MEDICAL_CONTRACT_CODE).ToList();
                    if (IsNotNullOrEmpty(codes))
                    {
                        HisMedicalContractFilterQuery filter = new HisMedicalContractFilterQuery();
                        filter.MEDICAL_CONTRACT_CODEs = codes;
                        var contracts = new HisMedicalContractGet().Get(filter);

                        if (IsNotNullOrEmpty(contracts))
                        {
                            string s = string.Join(",", codes);
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicalContract_DaTonTaiMaHopDongDuoc, s);
                            return false;
                        }
                    }
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

        internal bool VerifyBid(HIS_MEDICAL_CONTRACT data, ref List<HIS_BID_MATERIAL_TYPE> bidMaterialTypes, ref List<HIS_BID_MEDICINE_TYPE> bidMedicineTypes)
        {
            bool valid = true;
            try
            {
                if (data.BID_ID.HasValue)
                {
                    List<string> matyErrors = new List<string>();
                    List<string> metyErrors = new List<string>();
                    List<string> matyErrorPrices = new List<string>();
                    List<string> metyErrorPrices = new List<string>();
                    if (data.HIS_MEDI_CONTRACT_MATY != null && data.HIS_MEDI_CONTRACT_MATY.Count > 0)
                    {
                        List<long> bidMaterialIds = data.HIS_MEDI_CONTRACT_MATY.Select(s => s.BID_MATERIAL_TYPE_ID.Value).ToList();
                        HisBidMaterialTypeFilterQuery mateFilter = new HisBidMaterialTypeFilterQuery();
                        mateFilter.IDs = bidMaterialIds;
                        bidMaterialTypes = new HisBidMaterialTypeGet().Get(mateFilter);
                        if (!IsNotNullOrEmpty(bidMaterialTypes))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HisBidMaterialType: " + LogUtil.TraceData("bidMaterialIds", bidMaterialIds));
                        }

                        if (bidMaterialTypes.Any(a => a.BID_ID != data.BID_ID.Value))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("HIS_BID_MATERIAL_TYPE khong thuoc goi thau cua hop dong: " + LogUtil.TraceData("bidMaterialTypes", bidMaterialTypes.Where(o => o.BID_ID != data.BID_ID.Value).ToList()));
                        }

                        //if (bidMaterialTypes.Any(a => a.SUPPLIER_ID != data.MedicalContract.SUPPLIER_ID))
                        //{
                        //    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        //    throw new Exception("SUPPLIER_ID HisBidMaterialType khac SUPPLIER_ID trong hop dong");
                        //}

                        var group = data.HIS_MEDI_CONTRACT_MATY.GroupBy(g => g.BID_MATERIAL_TYPE_ID.Value).ToList();

                        foreach (var g in group)
                        {
                            HIS_BID_MATERIAL_TYPE type = bidMaterialTypes.FirstOrDefault(o => o.ID == g.Key);
                            if (type == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("BID_MATERIAL_TYPE_ID Invalid: " + g.Key);
                            }

                            if (g.Any(a => type.MATERIAL_TYPE_ID != a.MATERIAL_TYPE_ID))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("MATERIAL_TYPE_ID khac MATERIAL_TYPE_ID trong HIS_BID_MATERIAL_TYPE: " + LogUtil.TraceData("sdo", g.ToList()));
                            }

                            decimal contractAmount = ((type.TDL_CONTRACT_AMOUNT ?? 0) + g.Sum(s => s.AMOUNT));

                            if (type.AMOUNT < contractAmount)
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MATERIAL_TYPE_ID);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrors.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_SoLuongHopDongVaThau, param.LanguageCode), name, contractAmount, type.AMOUNT));
                                continue;
                            }

                            if (g.Any(a => (type.IMP_PRICE ?? 0) < (a.IMP_PRICE ?? 0)))
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MATERIAL_TYPE_ID);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_GiaNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_PRICE ?? 0), (g.First().IMP_PRICE ?? 0)));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_VAT_RATIO ?? 0) < (a.IMP_VAT_RATIO ?? 0)))
                            {
                                HIS_MATERIAL_TYPE mate = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MATERIAL_TYPE_ID);
                                string name = mate != null ? mate.MATERIAL_TYPE_NAME : "";
                                matyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_VatNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_VAT_RATIO ?? 0), (g.First().IMP_VAT_RATIO ?? 0)));
                                continue;
                            }
                        }
                    }

                    if (data.HIS_MEDI_CONTRACT_METY != null && data.HIS_MEDI_CONTRACT_METY.Count > 0)
                    {
                        List<long> bidMedicineIds = data.HIS_MEDI_CONTRACT_METY.Select(s => s.BID_MEDICINE_TYPE_ID.Value).ToList();
                        HisBidMedicineTypeFilterQuery mediFilter = new HisBidMedicineTypeFilterQuery();
                        mediFilter.IDs = bidMedicineIds;
                        bidMedicineTypes = new HisBidMedicineTypeGet().Get(mediFilter);
                        if (!IsNotNullOrEmpty(bidMedicineTypes))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Khong lay duoc HisBidMedicineType: " + LogUtil.TraceData("bidMedicineIds", bidMedicineIds));
                        }

                        if (bidMedicineTypes.Any(a => a.BID_ID != data.BID_ID.Value))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("HIS_BID_MEDICINE_TYPE khong thuoc goi thau cua hop dong: " + LogUtil.TraceData("bidMedicineTypes", bidMedicineTypes.Where(o => o.BID_ID != data.BID_ID.Value).ToList()));
                        }

                        //if (bidMedicineTypes.Any(a => a.SUPPLIER_ID != data.MedicalContract.SUPPLIER_ID))
                        //{
                        //    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        //    throw new Exception("SUPPLIER_ID HisBidMedicineType khac SUPPLIER_ID trong hop dong");
                        //}

                        var group = data.HIS_MEDI_CONTRACT_METY.GroupBy(g => g.BID_MEDICINE_TYPE_ID.Value).ToList();

                        foreach (var g in group)
                        {
                            HIS_BID_MEDICINE_TYPE type = bidMedicineTypes.FirstOrDefault(o => o.ID == g.Key);
                            if (type == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("BidMedicineTypeId Invalid: " + g.Key);
                            }

                            if (g.Any(a => type.MEDICINE_TYPE_ID != a.MEDICINE_TYPE_ID))
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                                throw new Exception("MedicineTypeId khac MEDICINE_TYPE_ID trong HIS_BID_MEDICINE_TYPE: " + LogUtil.TraceData("sdo", g.ToList()));
                            }


                            decimal contractAmount = ((type.TDL_CONTRACT_AMOUNT ?? 0) + g.Sum(s => s.AMOUNT));

                            if (type.AMOUNT < contractAmount)
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MEDICINE_TYPE_ID);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrors.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_SoLuongHopDongVaThau, param.LanguageCode), name, contractAmount, type.AMOUNT));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_PRICE ?? 0) < (a.IMP_PRICE ?? 0)))
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MEDICINE_TYPE_ID);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_GiaNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_PRICE ?? 0), (g.First().IMP_PRICE ?? 0)));
                                continue;
                            }


                            if (g.Any(a => (type.IMP_VAT_RATIO ?? 0) < (a.IMP_VAT_RATIO ?? 0)))
                            {
                                HIS_MEDICINE_TYPE medi = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == g.First().MEDICINE_TYPE_ID);
                                string name = medi != null ? medi.MEDICINE_TYPE_NAME : "";
                                metyErrorPrices.Add(String.Format(MessageUtil.GetMessage(LibraryMessage.Message.Enum.HisMedicalContract_VatNhapHopDongVaThau, param.LanguageCode), name, (type.IMP_VAT_RATIO ?? 0), (g.First().IMP_VAT_RATIO ?? 0)));
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

        internal bool VerifyType(HIS_MEDICAL_CONTRACT data, ref List<HIS_MATERIAL_TYPE> materialTypes, ref List<HIS_MEDICINE_TYPE> medicineTypes, ref List<string> sqls)
        {
            bool valid = true;
            try
            {
                if (data.HIS_MEDI_CONTRACT_MATY != null && data.HIS_MEDI_CONTRACT_MATY.Count > 0)
                {
                    List<long> materialIds = data.HIS_MEDI_CONTRACT_MATY.Select(s => s.MATERIAL_TYPE_ID).ToList();
                    materialTypes = HisMaterialTypeCFG.DATA.Where(o => materialIds.Contains(o.ID)).ToList();
                    if (!IsNotNullOrEmpty(materialTypes))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HisMaterialType: " + LogUtil.TraceData("materialIds", materialIds));
                    }

                    foreach (var sdo in data.HIS_MEDI_CONTRACT_MATY)
                    {
                        HIS_MATERIAL_TYPE type = materialTypes.FirstOrDefault(o => o.ID == sdo.MATERIAL_TYPE_ID);
                        if (type == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("MaterialTypeId Invalid: " + sdo.MATERIAL_TYPE_ID);
                        }

                        if (sdo.BID_MATERIAL_TYPE_ID.HasValue)
                        {
                            sqls.Add(String.Format("UPDATE HIS_BID_MATERIAL_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+{0}) WHERE ID = {1}", sdo.AMOUNT, sdo.BID_MATERIAL_TYPE_ID.Value));
                        }
                    }
                }

                if (data.HIS_MEDI_CONTRACT_METY != null && data.HIS_MEDI_CONTRACT_METY.Count > 0)
                {
                    List<long> medicineIds = data.HIS_MEDI_CONTRACT_METY.Select(s => s.MEDICINE_TYPE_ID).ToList();
                    medicineTypes = HisMedicineTypeCFG.DATA.Where(o => medicineIds.Contains(o.ID)).ToList();
                    if (!IsNotNullOrEmpty(medicineTypes))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong lay duoc HisMedicineType: " + LogUtil.TraceData("medicineIds", medicineIds));
                    }

                    foreach (var sdo in data.HIS_MEDI_CONTRACT_METY)
                    {
                        HIS_MEDICINE_TYPE type = medicineTypes.FirstOrDefault(o => o.ID == sdo.MEDICINE_TYPE_ID);
                        if (type == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("MedicineTypeId Invalid: " + sdo.MEDICINE_TYPE_ID);
                        }

                        if (sdo.BID_MEDICINE_TYPE_ID.HasValue)
                        {
                            sqls.Add(String.Format("UPDATE HIS_BID_MEDICINE_TYPE SET TDL_CONTRACT_AMOUNT = (NVL(TDL_CONTRACT_AMOUNT,0)+{0}) WHERE ID = {1}", sdo.AMOUNT, sdo.BID_MEDICINE_TYPE_ID.Value));
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
    }
}
