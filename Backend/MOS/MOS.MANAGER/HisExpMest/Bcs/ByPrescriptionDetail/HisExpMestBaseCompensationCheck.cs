using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMediStockMaty;
using MOS.MANAGER.HisMediStockMety;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Base.BaseCompensation
{
    class HisExpMestBaseCompensationCheck : BusinessBase
    {
        private static List<long> ALLOW_APPROVE_STT_IDs = new List<long>(){
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST,
            IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__EXECUTE
        };

        internal HisExpMestBaseCompensationCheck()
            : base()
        {

        }

        internal HisExpMestBaseCompensationCheck(CommonParam param)
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
                    if (data.MedicineTypes.Any(o => o.Amount <= 0 || o.MedicineTypeId <= 0 || (!IsNotNullOrEmpty(o.ExpMestMedicineIds) && !IsNotNullOrEmpty(o.ExpMestMetyReqIds))))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Amount Or MedicineTypeId Or (ExpMestMedicineIds And ExpMestMetyReqIds)");
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
                    if (data.MaterialTypes.Any(o => o.Amount <= 0 || o.MaterialTypeId <= 0 || (!IsNotNullOrEmpty(o.ExpMestMaterialIds) && !IsNotNullOrEmpty(o.ExpMestMatyReqIds))))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Amount Or MaterialTypeId Or (ExpMestMaterialIds And ExpMestMatyReqIds)");
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

        internal bool VerifyData(CabinetBaseCompensationSDO data, ref List<HIS_EXP_MEST> expMests, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_METY_REQ> metyReqs, ref List<HIS_EXP_MEST_MATY_REQ> matyReqs)
        {
            bool valid = true;
            try
            {
                List<HIS_EXP_MEST_MEDICINE> expMedicines = null;
                List<HIS_EXP_MEST_MATERIAL> expMaterials = null;
                List<HIS_EXP_MEST_METY_REQ> reqMetys = null;
                List<HIS_EXP_MEST_MATY_REQ> reqMatys = null;
                List<HIS_EXP_MEST> listExpMest = null;

                List<long> expMestIds = new List<long>();
                if (IsNotNullOrEmpty(data.MedicineTypes))
                {
                    List<long> expMestMedicineIds = new List<long>();
                    List<long> expMestMetyReqIds = new List<long>();
                    data.MedicineTypes.ForEach(o =>
                    {
                        if (IsNotNullOrEmpty(o.ExpMestMedicineIds)) expMestMedicineIds.AddRange(o.ExpMestMedicineIds);
                        if (IsNotNullOrEmpty(o.ExpMestMetyReqIds)) expMestMetyReqIds.AddRange(o.ExpMestMetyReqIds);
                    });
                    if (IsNotNullOrEmpty(expMestMedicineIds))
                    {
                        List<HIS_EXP_MEST_MEDICINE> mediData = new List<HIS_EXP_MEST_MEDICINE>();
                        if (!new HisExpMestMedicineCheck(param).VerifyIds(expMestMedicineIds, mediData))
                        {
                            return false;
                        }

                        var notExports = mediData.Where(o => !o.IS_EXPORT.HasValue || o.IS_EXPORT.Value != Constant.IS_TRUE).ToList();
                        if (IsNotNullOrEmpty(notExports))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai HIS_EXP_MEST_MEDICINE co IS_EXPORT <> 1" + LogUtil.TraceData("ExpMestMedicine", notExports));
                        }

                        var outReqAmounts = mediData.Where(a => a.AMOUNT <= (a.BCS_REQ_AMOUNT ?? 0)).ToList();

                        if (IsNotNullOrEmpty(outReqAmounts))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai HIS_EXP_MEST_MEDICINE co AMOUNT = BCS_REQ_AMOUNT" + LogUtil.TraceData("ExpMestMedicine", outReqAmounts));
                        }

                        expMestIds.AddRange(mediData.Select(s => s.EXP_MEST_ID ?? 0).ToList());
                        expMedicines = mediData;
                    }

                    if (IsNotNullOrEmpty(expMestMetyReqIds))
                    {
                        List<HIS_EXP_MEST_METY_REQ> reqData = new List<HIS_EXP_MEST_METY_REQ>();
                        if (!new HisExpMestMetyReqCheck(param).VerifyIds(expMestMetyReqIds, reqData))
                        {
                            return false;
                        }

                        var outReqAmounts = reqData.Where(a => a.AMOUNT <= ((a.BCS_REQ_AMOUNT ?? 0) + (a.DD_AMOUNT ?? 0))).ToList();

                        if (IsNotNullOrEmpty(outReqAmounts))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai HIS_EXP_MEST_METY_REQ co AMOUNT = BCS_REQ_AMOUNT" + LogUtil.TraceData("ExpMestMetyReq", outReqAmounts));
                        }

                        expMestIds.AddRange(reqData.Select(s => s.EXP_MEST_ID).ToList());
                        reqMetys = reqData;
                    }
                }

                if (IsNotNullOrEmpty(data.MaterialTypes))
                {
                    List<long> expMestMaterialIds = new List<long>();
                    List<long> expMestMatyReqIds = new List<long>();
                    data.MaterialTypes.ForEach(o =>
                    {
                        if (IsNotNullOrEmpty(o.ExpMestMaterialIds)) expMestMaterialIds.AddRange(o.ExpMestMaterialIds);
                        if (IsNotNullOrEmpty(o.ExpMestMatyReqIds)) expMestMatyReqIds.AddRange(o.ExpMestMatyReqIds);
                    });
                    if (IsNotNullOrEmpty(expMestMaterialIds))
                    {
                        List<HIS_EXP_MEST_MATERIAL> mateData = new List<HIS_EXP_MEST_MATERIAL>();
                        if (!new HisExpMestMaterialCheck(param).VerifyIds(expMestMaterialIds, mateData))
                        {
                            return false;
                        }

                        var outReqAmounts = mateData.Where(a => a.AMOUNT <= (a.BCS_REQ_AMOUNT ?? 0)).ToList();

                        if (IsNotNullOrEmpty(outReqAmounts))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai HIS_EXP_MEST_MATERIAL co AMOUNT = BCS_REQ_AMOUNT" + LogUtil.TraceData("ExpMestMaterial", outReqAmounts));
                        }

                        expMestIds.AddRange(mateData.Select(s => s.EXP_MEST_ID ?? 0).ToList());
                        expMaterials = mateData;
                    }

                    if (IsNotNullOrEmpty(expMestMatyReqIds))
                    {
                        List<HIS_EXP_MEST_MATY_REQ> reqData = new List<HIS_EXP_MEST_MATY_REQ>();
                        if (!new HisExpMestMatyReqCheck(param).VerifyIds(expMestMatyReqIds, reqData))
                        {
                            return false;
                        }

                        var outReqAmounts = reqData.Where(a => a.AMOUNT <= ((a.BCS_REQ_AMOUNT ?? 0) + (a.DD_AMOUNT ?? 0))).ToList();

                        if (IsNotNullOrEmpty(outReqAmounts))
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                            throw new Exception("Ton tai HIS_EXP_MEST_MATY_REQ co AMOUNT = BCS_REQ_AMOUNT" + LogUtil.TraceData("ExpMestMatyReq", outReqAmounts));
                        }

                        expMestIds.AddRange(reqData.Select(s => s.EXP_MEST_ID).ToList());
                        reqMatys = reqData;
                    }
                }

                expMestIds = expMestIds.Distinct().ToList();
                List<HIS_EXP_MEST> mests = new List<HIS_EXP_MEST>();
                if (!new HisExpMestCheck(param).VerifyIds(expMestIds, mests))
                {
                    return false;
                }

                List<string> notFinishCodes = mests.Where(o => o.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE).Select(s => s.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(notFinishCodes))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_ChuaHoanThanh, String.Join(";", notFinishCodes));
                    return false;
                }

                List<string> expMestCodes = mests.Where(o => o.XBTT_EXP_MEST_ID.HasValue).Select(s => s.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(expMestCodes))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuXuatDaThuocPhieuBuCoSoKhac, String.Join(";", expMestCodes));
                    return false;
                }

                if (mests.Any(a => a.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS && a.IMP_MEDI_STOCK_ID != data.CabinetMediStockId))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai phieu xuat bu tu truc khong phai xuat cho tu truc can bu");
                }
                if (mests.Any(a => a.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT && a.MEDI_STOCK_ID != data.CabinetMediStockId))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Ton tai don tu truc khong duoc xuat tu tu truc can bu");
                }

                listExpMest = mests;
                expMests = listExpMest;
                medicines = expMedicines;
                materials = expMaterials;
                metyReqs = reqMetys;
                matyReqs = reqMatys;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsCompensationPresDetail(HIS_EXP_MEST raw)
        {
            bool valid = true;
            try
            {
                if (raw.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS
                    || raw.BCS_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST.BCS_TYPE__ID__PRES_DETAIL)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Phieu xuat khong phai xuat bu co so hoac khong phai bu theo chi tiet don" + LogUtil.TraceData("ExpMest", raw));
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
