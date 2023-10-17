using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMaterialBean;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensationByBase
{
    class HisExpMestCompensationByBaseCheck : BusinessBase
    {
        private static List<long> ALLOW_APPROVE_STT_IDs = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
        };

        internal HisExpMestCompensationByBaseCheck()
            : base()
        {

        }

        internal HisExpMestCompensationByBaseCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(CabinetBaseCompensationSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.CabinetMediStockId)) throw new ArgumentNullException("data.CabinetMediStockId");
                if (!IsGreaterThanZero(data.WorkingRoomId)) throw new ArgumentNullException("data.WorkingRoomId");
                if (!IsNotNullOrEmpty(data.MaterialTypes) && !IsNotNullOrEmpty(data.MedicineTypes)) throw new ArgumentNullException("data.MaterialTypes && data.MedicineTypes");
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

        internal bool ValidData(CabinetBaseCompensationSDO data, ref List<HIS_MEDI_STOCK_METY> stockMetys, ref List<HIS_MEDI_STOCK_MATY> stockMatys)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    if (data.MedicineTypes.Any(o => o.Amount <= 0 || o.MedicineTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Amount Or MedicineTypeId Or");
                    }

                    if (data.MedicineTypes.GroupBy(g => g.MedicineTypeId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai nhieu MedicineType co cung MedicineTypeId");
                    }

                    var metyStocks = new HisMediStockMetyGet().GetByMediStockId(data.CabinetMediStockId);
                    if (!data.MediStockId.HasValue
                        && data.MedicineTypes.Any(a => metyStocks == null || !metyStocks.Exists(e => e.MEDICINE_TYPE_ID == a.MedicineTypeId && e.EXP_MEDI_STOCK_ID.HasValue)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong truyen len MediStockId va ton tai thuoc khong duoc thiet lap kho xuat");
                    }
                    stockMetys = metyStocks;
                }

                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    if (data.MaterialTypes.Any(o => o.Amount <= 0 || o.MaterialTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Amount Or MaterialTypeId");
                    }

                    if (data.MaterialTypes.GroupBy(g => g.MaterialTypeId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai nhieu MaterialType co cung MaterialTypeId");
                    }

                    var matyStocks = new HisMediStockMatyGet().GetByMediStockId(data.CabinetMediStockId);
                    if (!data.MediStockId.HasValue
                        && data.MaterialTypes.Any(a => matyStocks == null || !matyStocks.Exists(e => e.MATERIAL_TYPE_ID == a.MaterialTypeId && e.EXP_MEDI_STOCK_ID.HasValue)))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Khong truyen len MediStockId va ton tai vat tu khong duoc thiet lap kho xuat");
                    }
                    stockMatys = matyStocks;
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

        internal bool VerifyData(CabinetBaseCompensationSDO data, List<HIS_MEDI_STOCK_METY> stockMetys, List<HIS_MEDI_STOCK_MATY> stockMatys)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    var notConfigs = data.MedicineTypes.Where(o => stockMetys == null || !stockMetys.Exists(e => e.MEDICINE_TYPE_ID == o.MedicineTypeId)).ToList();
                    if (IsNotNullOrEmpty(notConfigs))
                    {
                        List<HIS_MEDICINE_TYPE> types = HisMedicineTypeCFG.DATA.Where(o => notConfigs.Any(a => a.MedicineTypeId == o.ID)).ToList();
                        string names = types != null ? String.Join(",", types.Select(s => s.MEDICINE_TYPE_NAME).ToList()) : "";
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocChuaDuocThietLapCoSo, names);
                        return false;
                    }
                    HisMedicineBeanFilterQuery mediFilter = new HisMedicineBeanFilterQuery();
                    mediFilter.MEDI_STOCK_ID = data.CabinetMediStockId;
                    mediFilter.MEDICINE_TYPE_IDs = stockMetys.Select(s => s.MEDICINE_TYPE_ID).ToList();
                    List<HIS_MEDICINE_BEAN> mediBeans = new HisMedicineBeanGet().GetByMediStockId(data.CabinetMediStockId);
                    Dictionary<long, List<HIS_MEDICINE_BEAN>> dicBeans = new Dictionary<long, List<HIS_MEDICINE_BEAN>>();
                    if (IsNotNullOrEmpty(mediBeans))
                    {
                        dicBeans = mediBeans.GroupBy(g => g.TDL_MEDICINE_TYPE_ID).ToDictionary(d => d.Key, d => d.ToList());
                    }

                    List<string> invalidAmounts = new List<string>();
                    foreach (var item in data.MedicineTypes)
                    {
                        HIS_MEDI_STOCK_METY sMety = stockMetys.FirstOrDefault(o => o.MEDICINE_TYPE_ID == item.MedicineTypeId);
                        decimal inStockAmount = 0;
                        if (dicBeans.ContainsKey(item.MedicineTypeId))
                        {
                            inStockAmount = dicBeans[item.MedicineTypeId].Sum(s => s.AMOUNT);
                        }
                        decimal availAmount = (sMety.ALERT_MAX_IN_STOCK ?? 0) - inStockAmount;
                        if (item.Amount > availAmount)
                        {
                            HIS_MEDICINE_TYPE type = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MedicineTypeId);
                            string name = type != null ? type.MEDICINE_TYPE_NAME : "";
                            invalidAmounts.Add(name);
                            continue;
                        }
                    }

                    if (IsNotNullOrEmpty(invalidAmounts))
                    {
                        string names = String.Format(",", invalidAmounts);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocCoSoLuongYeuCauBuLonHonSoLuongCanBu, names);
                        return false;
                    }
                }

                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    var notConfigs = data.MaterialTypes.Where(o => stockMatys == null || !stockMatys.Exists(e => e.MATERIAL_TYPE_ID == o.MaterialTypeId)).ToList();
                    if (IsNotNullOrEmpty(notConfigs))
                    {
                        List<HIS_MATERIAL_TYPE> types = HisMaterialTypeCFG.DATA.Where(o => notConfigs.Any(a => a.MaterialTypeId == o.ID)).ToList();
                        string names = types != null ? String.Join(",", types.Select(s => s.MATERIAL_TYPE_NAME).ToList()) : "";
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuChuaDuocThietLapCoSo, names);
                        return false;
                    }
                    HisMaterialBeanFilterQuery mediFilter = new HisMaterialBeanFilterQuery();
                    mediFilter.MEDI_STOCK_ID = data.CabinetMediStockId;
                    mediFilter.MATERIAL_TYPE_IDs = stockMatys.Select(s => s.MATERIAL_TYPE_ID).ToList();
                    List<HIS_MATERIAL_BEAN> mateBeans = new HisMaterialBeanGet().GetByMediStockId(data.CabinetMediStockId);
                    Dictionary<long, List<HIS_MATERIAL_BEAN>> dicBeans = new Dictionary<long, List<HIS_MATERIAL_BEAN>>();
                    if (IsNotNullOrEmpty(mateBeans))
                    {
                        dicBeans = mateBeans.GroupBy(g => g.TDL_MATERIAL_TYPE_ID).ToDictionary(d => d.Key, d => d.ToList());
                    }

                    List<string> invalidAmounts = new List<string>();
                    foreach (var item in data.MaterialTypes)
                    {
                        HIS_MEDI_STOCK_MATY sMaty = stockMatys.FirstOrDefault(o => o.MATERIAL_TYPE_ID == item.MaterialTypeId);
                        decimal inStockAmount = 0;
                        if (dicBeans.ContainsKey(item.MaterialTypeId))
                        {
                            inStockAmount = dicBeans[item.MaterialTypeId].Sum(s => s.AMOUNT);
                        }
                        decimal availAmount = (sMaty.ALERT_MAX_IN_STOCK ?? 0) - inStockAmount;
                        if (item.Amount > availAmount)
                        {
                            HIS_MATERIAL_TYPE type = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == item.MaterialTypeId);
                            string name = type != null ? type.MATERIAL_TYPE_NAME : "";
                            invalidAmounts.Add(name);
                            continue;
                        }
                    }

                    if (IsNotNullOrEmpty(invalidAmounts))
                    {
                        string names = String.Format(",", invalidAmounts);
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuCoSoLuongYeuCauBuLonHonSoLuongCanBu, names);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsCompensationBase(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS
                    || raw.BCS_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.BCS_TYPE__ID__BASE)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai xuat bu co so hoac khong phai bu theo co so thiet lap" + LogUtil.TraceData("ExpMest", raw));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsHasNotBaseExpMest(CabinetBaseCompensationSDO data)
        {
            bool valid = true;
            try
            {
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.EXP_MEST_STT_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST
                };
                filter.EXP_MEST_TYPE_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS,
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK
                };
                filter.IMP_MEDI_STOCK_ID = data.CabinetMediStockId;
                List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(filter);

                List<HIS_EXP_MEST> existsBcss = expMests != null ? expMests.Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS).ToList() : null;
                if (IsNotNullOrEmpty(existsBcss))
                {
                    string codes = String.Join(",", existsBcss.Select(s => s.EXP_MEST_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuXuatBuCoSoSauChuaDuocHoanThanh, codes);
                    return false;
                }

                List<HIS_EXP_MEST> existsAddOrReducs = expMests != null ? expMests.Where(o => o.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__CK && o.CHMS_TYPE_ID.HasValue).ToList() : null;
                if (IsNotNullOrEmpty(existsAddOrReducs))
                {
                    string codes = String.Join(",", existsAddOrReducs.Select(s => s.EXP_MEST_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuBoSungHoanCoSoSauChuaDuocHoanThanh, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsHasNotBaseImpMest(CabinetBaseCompensationSDO data)
        {
            bool valid = true;
            try
            {
                HisImpMestFilterQuery filter = new HisImpMestFilterQuery();
                filter.IMP_MEST_STT_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__DRAFT,
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL,
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REJECT,
                    IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__REQUEST
                };
                filter.IMP_MEST_TYPE_IDs = new List<long>()
                {
                    IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS
                };
                filter.MEDI_STOCK_ID = data.CabinetMediStockId;
                List<HIS_IMP_MEST> impMest = new HisImpMestGet().Get(filter);

                if (IsNotNullOrEmpty(impMest))
                {
                    string codes = String.Join(",", impMest.Select(s => s.IMP_MEST_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuNhapBuCoSoSauChuaDuocHoanThanh, codes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }
    }
}
