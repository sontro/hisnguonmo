using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Chms.Bcs
{
    class HisExpMestBcsCheck : BusinessBase
    {
        internal HisExpMestBcsCheck()
            : base()
        {

        }

        internal HisExpMestBcsCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(HisExpMestBcsSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.MediStockId <= 0) throw new ArgumentNullException("data.MediStockId null");
                if (data.ImpMediStockId <= 0) throw new ArgumentNullException("data.ImpMediStockId null");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (!IsNotNullOrEmpty(data.ExpMestDttIds) && !IsNotNullOrEmpty(data.ExpMestBcsIds)) throw new ArgumentNullException("data.ExpMestDttIds and ExpMestBcsIds null");
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

        internal bool IsAllowed(HisExpMestBcsSDO data)
        {
            try
            {
                V_HIS_MEDI_STOCK expMediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.MediStockId).FirstOrDefault();
                V_HIS_MEDI_STOCK impMediStock = HisMediStockCFG.DATA.Where(o => o.ID == data.ImpMediStockId).FirstOrDefault();

                if (impMediStock == null || expMediStock == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Id kho xuat hoac kho nhan ko hop le");
                    return false;
                }
                if (!impMediStock.IS_CABINET.HasValue || impMediStock.IS_CABINET.Value != MOS.UTILITY.Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Kho nhan khong phai la tu truc");
                }
                if (impMediStock.CABINET_MANAGE_OPTION.HasValue && impMediStock.CABINET_MANAGE_OPTION != IMSys.DbConfig.HIS_RS.HIS_MEDI_STOCK.CABINET_MANAGE_OPTION__PRES)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMediStock_TuTrucKhongQuanLyCoSoTheoDon, impMediStock.MEDI_STOCK_NAME);
                    return false;
                }
                List<HIS_MEST_ROOM> lst = IsNotNullOrEmpty(HisMestRoomCFG.DATA) ? HisMestRoomCFG.DATA.Where(o => o.MEDI_STOCK_ID == data.MediStockId && o.ROOM_ID == impMediStock.ROOM_ID && o.IS_ACTIVE == Constant.IS_TRUE).ToList() : null;

                if (!IsNotNullOrEmpty(lst))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisChmsExpMest_KhongKhaXuatGiua2Kho, expMediStock.MEDI_STOCK_NAME, impMediStock.MEDI_STOCK_NAME);
                    return false;
                }

                WorkPlaceSDO workPlace = TokenManager.GetWorkPlace(data.ReqRoomId);

                //Neu la linh~ thuoc thi kho yeu cau phai la kho nhan
                if (workPlace.MediStockId != data.ImpMediStockId)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_BanDangKhongTruyCapVaoKhoNhap);
                    return false;
                }

                if ((impMediStock.IS_BUSINESS == MOS.UTILITY.Constant.IS_TRUE && expMediStock.IS_BUSINESS != MOS.UTILITY.Constant.IS_TRUE) || (impMediStock.IS_BUSINESS != MOS.UTILITY.Constant.IS_TRUE && expMediStock.IS_BUSINESS == MOS.UTILITY.Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_KhongChoPhepXuatChuyenKhoGiuaKhoKinhDoanhVaKhoThuong);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }

        internal bool ValidExpMest(HisExpMestBcsSDO data, ref List<HIS_EXP_MEST> hisExpMestDtts, ref List<HIS_EXP_MEST> hisExpMestBcss, ref List<HIS_EXP_MEST_MATERIAL> materials, ref List<HIS_EXP_MEST_MEDICINE> medicines, ref List<HIS_EXP_MEST_MATY_REQ> matyReqs, ref List<HIS_EXP_MEST_METY_REQ> metyReqs)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(data.ExpMestDttIds))
                {
                    List<HIS_EXP_MEST> expMests = new HisExpMestGet().GetByIds(data.ExpMestDttIds);
                    if (expMests == null || expMests.Count != data.ExpMestDttIds.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("ExpMestDttIds Invalid");
                    }

                    List<string> expMestCodes = expMests.Where(o => o.XBTT_EXP_MEST_ID.HasValue).Select(s => s.EXP_MEST_CODE).ToList();

                    if (IsNotNullOrEmpty(expMestCodes))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuXuatDaThuocPhieuBuCoSoKhac, String.Join(";", expMestCodes));
                        return false;
                    }

                    if (expMests.Exists(e => e.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DTT))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai phieu xuat khong phai la don thuoc tu truc");
                    }

                    if (expMests.Exists(e => e.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai phieu xuat chua duoc thuc xuat (hoan thanh)");
                    }

                    if (expMests.Exists(e => e.MEDI_STOCK_ID != data.ImpMediStockId))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai phieu xuat khong phai tu kho (tu truc) yeu cau phieu bu");
                    }

                    materials = new HisExpMestMaterialGet().GetExportedByExpMestIds(data.ExpMestDttIds);
                    medicines = new HisExpMestMedicineGet().GetExportedByExpMestIds(data.ExpMestDttIds);

                    if (IsNotNullOrEmpty(materials) && materials.Any(a => a.BCS_REQ_AMOUNT.HasValue && a.BCS_REQ_AMOUNT.Value > 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai ExpMestMaterial co BCS_REQ_AMOUNT > 0");
                    }

                    if (IsNotNullOrEmpty(medicines) && medicines.Any(a => a.BCS_REQ_AMOUNT.HasValue && a.BCS_REQ_AMOUNT.Value > 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai ExpMestMedicine co BCS_REQ_AMOUNT > 0");
                    }

                    hisExpMestDtts = expMests;
                }
                if (IsNotNullOrEmpty(data.ExpMestBcsIds))
                {
                    List<HIS_EXP_MEST> expMestBcss = new HisExpMestGet().GetByIds(data.ExpMestBcsIds);
                    if (expMestBcss == null || expMestBcss.Count != data.ExpMestBcsIds.Count)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("ExpMestBcsIds invalid");
                    }

                    if (expMestBcss.Exists(e => e.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai phieu xuat khong phai la xuat bcs");
                    }

                    if (expMestBcss.Exists(e => e.IMP_MEDI_STOCK_ID != data.ImpMediStockId))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai phieu xuat bsc kho nhap khong phai cua kho (tu truc) dang yeu cau");
                    }

                    if (expMestBcss.Exists(e => e.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai phieu xuat bcs chua duc thuc nhap");
                    }
                    List<string> expMestCodes = expMestBcss.Where(o => o.XBTT_EXP_MEST_ID.HasValue).Select(s => s.EXP_MEST_CODE).ToList();
                    if (IsNotNullOrEmpty(expMestCodes))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_CacPhieuXuatDaThuocPhieuBuCoSoKhac, String.Join(";", expMestCodes));
                        return false;
                    }

                    matyReqs = new HisExpMestMatyReqGet().GetByExpMestIds(data.ExpMestBcsIds);
                    metyReqs = new HisExpMestMetyReqGet().GetByExpMestIds(data.ExpMestBcsIds);

                    if (IsNotNullOrEmpty(matyReqs) && matyReqs.Any(a => a.BCS_REQ_AMOUNT.HasValue && a.BCS_REQ_AMOUNT.Value > 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai ExpMestMatyReq co BCS_REQ_AMOUNT > 0");
                    }

                    if (IsNotNullOrEmpty(metyReqs) && metyReqs.Any(a => a.BCS_REQ_AMOUNT.HasValue && a.BCS_REQ_AMOUNT.Value > 0))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai ExpMestMetyReq co BCS_REQ_AMOUNT > 0");
                    }

                    hisExpMestBcss = expMestBcss;
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
