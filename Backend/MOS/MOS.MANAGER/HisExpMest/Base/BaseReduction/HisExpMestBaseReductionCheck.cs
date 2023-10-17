using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.BaseReduction
{
    class HisExpMestBaseReductionCheck : BusinessBase
    {
        internal HisExpMestBaseReductionCheck()
            : base()
        {

        }

        internal HisExpMestBaseReductionCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(CabinetBaseReductionSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.CabinetMediStockId)) throw new ArgumentNullException("data.CabinetMediStockId");
                if (!IsGreaterThanZero(data.ImpMestMediStockId)) throw new ArgumentNullException("data.ImpMestMediStockId");
                if (!IsGreaterThanZero(data.WorkingRoomId)) throw new ArgumentNullException("data.WorkingRoomId");
                if (!IsNotNullOrEmpty(data.MaterialTypes) && !IsNotNullOrEmpty(data.MedicineTypes)) throw new ArgumentNullException("data.MaterialTypes && data.MedicineTypes");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
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

        internal bool IsWorkingInImpOrExpStock(WorkPlaceSDO workPlace, long impStockId, long expStockId)
        {
            bool valid = true;
            try
            {
                if (!workPlace.MediStockId.HasValue || (workPlace.MediStockId.Value != impStockId && workPlace.MediStockId.Value != expStockId))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiKhoNhapHoacXuat);
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

        internal bool ValidData(CabinetBaseReductionSDO data, ref List<HIS_MEDI_STOCK_METY> stockMetys, ref List<HIS_MEDI_STOCK_MATY> stockMatys)
        {
            bool valid = true;
            try
            {
                List<HIS_MEDI_STOCK_METY> metyInStocks = null;
                List<HIS_MEDI_STOCK_MATY> matyInStocks = null;
                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    if (data.MedicineTypes.Any(a => a.Amount <= 0 || a.MedicineTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Ton tai MedicineType co Amount hoac MedicineTypeId khong hop le");
                    }
                    metyInStocks = new HisMediStockMetyGet().GetByMediStockId(data.CabinetMediStockId);

                    var notExists = data.MedicineTypes.Where(o => metyInStocks == null || !metyInStocks.Any(a => a.MEDICINE_TYPE_ID == o.MedicineTypeId)).ToList();
                    if (IsNotNullOrEmpty(notExists))
                    {
                        string names = String.Join(",", HisMedicineTypeCFG.DATA.Where(o => notExists.Any(a => a.MedicineTypeId == o.ID)).Select(s => s.MEDICINE_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocChuaThietLapCoSo, names);
                        return false;
                    }

                    var errorAmounts = data.MedicineTypes.Where(o => metyInStocks.Any(a => a.MEDICINE_TYPE_ID == o.MedicineTypeId && (a.ALERT_MAX_IN_STOCK ?? 0) < o.Amount)).ToList();

                    if (IsNotNullOrEmpty(errorAmounts))
                    {
                        string names = String.Join(",", HisMedicineTypeCFG.DATA.Where(o => errorAmounts.Any(a => a.MedicineTypeId == o.ID)).Select(s => s.MEDICINE_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocCoSoLuongCoSoNhoHonSoLuongHoan, names);
                        return false;
                    }
                }
                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    if (data.MaterialTypes.Any(a => a.Amount <= 0 || a.MaterialTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Ton tai MaterialType co Amount hoac MaterialTypeId khong hop le");
                    }

                    matyInStocks = new HisMediStockMatyGet().GetByMediStockId(data.CabinetMediStockId);

                    var notExists = data.MaterialTypes.Where(o => matyInStocks == null || !matyInStocks.Any(a => a.MATERIAL_TYPE_ID == o.MaterialTypeId)).ToList();
                    if (IsNotNullOrEmpty(notExists))
                    {
                        string names = String.Join(",", HisMaterialTypeCFG.DATA.Where(o => notExists.Any(a => a.MaterialTypeId == o.ID)).Select(s => s.MATERIAL_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuChuaThietLapCoSo, names);
                        return false;
                    }

                    var errorAmounts = data.MaterialTypes.Where(o => matyInStocks.Any(a => a.MATERIAL_TYPE_ID == o.MaterialTypeId && (a.ALERT_MAX_IN_STOCK ?? 0) < o.Amount)).ToList();

                    if (IsNotNullOrEmpty(errorAmounts))
                    {
                        string names = String.Join(",", HisMaterialTypeCFG.DATA.Where(o => errorAmounts.Any(a => a.MaterialTypeId == o.ID)).Select(s => s.MATERIAL_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuCoSoLuongCoSoNhoHonSoLuongHoan, names);
                        return false;
                    }
                }

                stockMatys = matyInStocks;
                stockMetys = metyInStocks;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool CheckExistsExpMestBase(HIS_MEDI_STOCK cabinetStock)
        {
            bool valid = true;
            try
            {
                HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                filter.HAS_CHMS_TYPE_ID = true;
                filter.MEDI_STOCK_ID__OR__IMP_MEDI_STOCK_ID = cabinetStock.ID;
                filter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(filter);
                if (IsNotNullOrEmpty(expMests))
                {
                    string codes = String.Join(",", expMests.Select(s => s.EXP_MEST_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMediStock_TonTaiPhieuHoanBoSungChuaDuyet, cabinetStock.MEDI_STOCK_NAME, codes);
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

        internal bool ValidData(CabinetBaseReductionSDO data, List<HIS_MEDI_STOCK_METY> stockMetys, List<HIS_MEDI_STOCK_MATY> stockMatys)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    if (data.MedicineTypes.Any(a => a.Amount <= 0 || a.MedicineTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Ton tai MedicineType co Amount hoac MedicineTypeId khong hop le");
                    }

                    if (data.MedicineTypes.GroupBy(g => g.MedicineTypeId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai hai dong MedicineType co cung MedicineTypeId");
                    }

                    var notExists = data.MedicineTypes.Where(o => stockMetys == null || !stockMetys.Any(a => a.MEDICINE_TYPE_ID == o.MedicineTypeId)).ToList();
                    if (IsNotNullOrEmpty(notExists))
                    {
                        string names = String.Join(",", HisMedicineTypeCFG.DATA.Where(o => notExists.Any(a => a.MedicineTypeId == o.ID)).Select(s => s.MEDICINE_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocChuaThietLapCoSo, names);
                        return false;
                    }

                    var errorAmounts = data.MedicineTypes.Where(o => stockMetys.Any(a => a.MEDICINE_TYPE_ID == o.MedicineTypeId && (a.ALERT_MAX_IN_STOCK ?? 0) < o.Amount)).ToList();

                    if (IsNotNullOrEmpty(errorAmounts))
                    {
                        string names = String.Join(",", HisMedicineTypeCFG.DATA.Where(o => errorAmounts.Any(a => a.MedicineTypeId == o.ID)).Select(s => s.MEDICINE_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ThuocCoSoLuongCoSoNhoHonSoLuongHoan, names);
                        return false;
                    }
                }
                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    if (data.MaterialTypes.Any(a => a.Amount <= 0 || a.MaterialTypeId <= 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new Exception("Ton tai MaterialType co Amount hoac MaterialTypeId khong hop le");
                    }

                    if (data.MaterialTypes.GroupBy(g => g.MaterialTypeId).Any(a => a.Count() > 1))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai hai dong MaterialType co cung MaterialTypeId");
                    }

                    var notExists = data.MaterialTypes.Where(o => stockMatys == null || !stockMatys.Any(a => a.MATERIAL_TYPE_ID == o.MaterialTypeId)).ToList();
                    if (IsNotNullOrEmpty(notExists))
                    {
                        string names = String.Join(",", HisMaterialTypeCFG.DATA.Where(o => notExists.Any(a => a.MaterialTypeId == o.ID)).Select(s => s.MATERIAL_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuChuaThietLapCoSo, names);
                        return false;
                    }

                    var errorAmounts = data.MaterialTypes.Where(o => stockMatys.Any(a => a.MATERIAL_TYPE_ID == o.MaterialTypeId && (a.ALERT_MAX_IN_STOCK ?? 0) < o.Amount)).ToList();

                    if (IsNotNullOrEmpty(errorAmounts))
                    {
                        string names = String.Join(",", HisMaterialTypeCFG.DATA.Where(o => errorAmounts.Any(a => a.MaterialTypeId == o.ID)).Select(s => s.MATERIAL_TYPE_NAME).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_VatTuCoSoLuongCoSoNhoHonSoLuongHoan, names);
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

    }
}
