using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisBillGoods;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Auto;
using MOS.MANAGER.HisExpMest.Common.Export;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisTransactionExp;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.SaleExpMest
{
    class HisTransactionBillCreateWithBillGoodsCheck : BusinessBase
    {
        internal HisTransactionBillCreateWithBillGoodsCheck()
            : base()
        {
        }

        internal HisTransactionBillCreateWithBillGoodsCheck(CommonParam param)
            : base(param)
        {
        }

        public bool IsValidData(HisTransactionBillGoodsSDO data, ref List<HIS_EXP_MEST> exps)
        {
            try
            {
                List<HIS_EXP_MEST> expMests = new List<HIS_EXP_MEST>();
                HisExpMestCheck expMestChecker = new HisExpMestCheck(param);
                if (!expMestChecker.VerifyIds(data.ExpMestIds, expMests))
                {
                    LogSystem.Warn("ExpMestIds Invalid: " + LogUtil.TraceData("", data.ExpMestIds));
                    return false;
                }

                if (expMests.Any(a => a.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BAN))
                {
                    //BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_KhongPhaiPhieuXuatBan);
                    LogSystem.Warn("Phieu xuat khong phai la xuat ban");
                    return false;
                }

                if (expMests.GroupBy(g => g.TDL_TREATMENT_ID).Count() > 1)
                {
                    //BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_CacPhieuXuatKhongCungHoSoDieuTri);
                    LogSystem.Warn("Cac phieu xuat khong cung mot ho so dieu tri");
                    return false;
                }

                List<string> hasBill = expMests.Where(a => a.BILL_ID.HasValue).Select(o => o.EXP_MEST_CODE).ToList();

                if (IsNotNullOrEmpty(hasBill))
                {
                    string hasBillStr = string.Join(",", hasBill);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatBanDaCoHoaDonThanhToan, hasBillStr);
                    return false;
                }

                List<string> hasDebt = expMests.Where(a => a.DEBT_ID.HasValue).Select(o => o.EXP_MEST_CODE).ToList();

                if (IsNotNullOrEmpty(hasDebt) && data.HisTransaction.IS_DEBT_COLLECTION != Constant.IS_TRUE)
                {
                    string hasDebtStr = string.Join(",", hasDebt);
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatBanDaDuocChotNo, hasDebtStr);
                    return false;
                }

                if (expMests.Any(a => a.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT || a.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT))
                {
                    long sttId = expMests.FirstOrDefault(o => o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DRAFT || o.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REJECT).EXP_MEST_STT_ID;
                    string sttName = HisExpMestSttCFG.DATA.FirstOrDefault(o => o.ID == sttId).EXP_MEST_STT_NAME;
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisExpMest_PhieuXuatDangOTrangThai, sttName);
                    return false;
                }
                decimal totalExpAmount = 0;
                decimal totalExpPrice = 0;

                decimal totalGoodAmount = IsNotNull(data.HisBillGoods) ? data.HisBillGoods.Sum(s => s.AMOUNT) : 0;
                decimal totalGoodPrice = IsNotNull(data.HisBillGoods) ? data.HisBillGoods.Sum(s => ((s.PRICE * s.AMOUNT * (1 + (s.VAT_RATIO ?? 0))) - (s.DISCOUNT ?? 0))) : 0;

                List<HIS_EXP_MEST_MEDICINE> listExpMestMedicine = new HisExpMestMedicineGet().GetByExpMestIds(expMests.Select(s => s.ID).ToList());
                List<HIS_EXP_MEST_MATERIAL> listExpMestMaterial = new HisExpMestMaterialGet().GetByExpMestIds(expMests.Select(s => s.ID).ToList());

                if (IsNotNullOrEmpty(listExpMestMedicine))
                {
                    foreach (var item in listExpMestMedicine)
                    {
                        decimal expAmount = (item.AMOUNT - (item.TH_AMOUNT ?? 0));
                        decimal expPrice = expAmount * ((item.PRICE ?? 0) * (1 + (item.VAT_RATIO ?? 0)));
                        totalExpAmount += expAmount;
                        totalExpPrice += (expPrice - (item.DISCOUNT ?? 0));
                    }
                }
                if (IsNotNullOrEmpty(listExpMestMaterial))
                {
                    foreach (var item in listExpMestMaterial)
                    {
                        decimal expAmount = (item.AMOUNT - (item.TH_AMOUNT ?? 0));
                        decimal expPrice = expAmount * ((item.PRICE ?? 0) * (1 + (item.VAT_RATIO ?? 0)));
                        totalExpAmount += expAmount;
                        totalExpPrice += (expPrice - (item.DISCOUNT ?? 0));
                    }
                }

                if (totalExpAmount != totalGoodAmount)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("So luong trong expMest Khac voi so luong trong Goods: ExpMestAmount: " + totalExpAmount + "; GoodsAmount: " + totalGoodAmount);
                    return false;
                }
                if (totalExpPrice != totalGoodPrice)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Tong tien trong expMest Khac voi so luong trong Goods: ExpMestPrice: " + totalExpPrice + "; GoodsPrice: " + totalGoodPrice);
                    return false;
                }

                exps = expMests;
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private void GetNumOrderFromOldSystem(HIS_TRANSACTION data, HIS_ACCOUNT_BOOK accountBook)
        {
            if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER != Constant.IS_TRUE)
            {
                return;
            }

            if (data.NUM_ORDER > 0)
            {
                return;
            }

            if (OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.PMS)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiKhongDuocDeTrong);
                throw new Exception("NUM_ORDER is null: ");
            }

            if (String.IsNullOrWhiteSpace(accountBook.TEMPLATE_CODE))
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiKhongCoMauSo, accountBook.ACCOUNT_BOOK_NAME);
                throw new Exception("So bien lai khong co SYMBOL_CODE. ACCOUNT_BOOK_CODE: " + accountBook.ACCOUNT_BOOK_CODE);
            }

            long? num = new OldSystem.HMS.InvoiceConsumer(OldSystemCFG.ADDRESS).Get(accountBook.TEMPLATE_CODE);
            if (!num.HasValue)
            {
                MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongLayDuocSoHoaDonTuPMS);
                throw new Exception("Goi OLD SYSTEM lay NUM_ORDER that bai");
            }
            data.NUM_ORDER = num.Value;
        }
    }
}
