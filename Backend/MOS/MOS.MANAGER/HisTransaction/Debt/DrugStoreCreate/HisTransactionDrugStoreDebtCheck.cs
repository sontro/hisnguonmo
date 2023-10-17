using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Debt.DrugStoreCreate
{
    class HisTransactionDrugStoreDebtCheck : BusinessBase
    {
        internal HisTransactionDrugStoreDebtCheck()
            : base()
        {

        }

        internal HisTransactionDrugStoreDebtCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(HisTransactionDrugStoreDebtSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNull(data.Transaction)) throw new ArgumentNullException("data.Transaction");
                if (!IsNotNullOrEmpty(data.ExpMestIds)) throw new ArgumentNullException("data.ExpMestIds");
                if (!IsGreaterThanZero(data.RequestRoomId)) throw new ArgumentNullException("data.RequestRoomId");
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

        internal bool AllowRoom(long requestRoomId, ref V_HIS_CASHIER_ROOM cashierRoom)
        {
            bool valid = true;
            try
            {
                V_HIS_CASHIER_ROOM cr = HisCashierRoomCFG.DATA.Where(o => o.ROOM_ID == requestRoomId).FirstOrDefault();
                if (cr == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("sdo.CashierRoomId ko hop le");
                    return false;
                }

                if (!HisUserRoomCFG.DATA.Exists(t => t.ROOM_ID == cr.ROOM_ID && t.IS_ACTIVE == Constant.IS_TRUE))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenTruyCapVaoPhongThuNgan, cr.CASHIER_ROOM_NAME);
                    return false;
                }
                cashierRoom = cr;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidData(HisTransactionDrugStoreDebtSDO data, ref List<D_HIS_EXP_MEST_DETAIL_1> details)
        {
            bool valid = true;
            try
            {
                if (data.Transaction.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.Transaction.TRANSACTION_TYPE_ID ko phai loai la no");
                    return false;
                }

                //Lay danh sach sere_serv tuong ung voi ho so
                DHisExpMestDetail1Filter filter = new DHisExpMestDetail1Filter();
                filter.EXP_MEST_IDs = data.ExpMestIds;
                filter.HAS_BILL = false;
                filter.HAS_DEBT = false;

                List<D_HIS_EXP_MEST_DETAIL_1> ss = new HisExpMestGet().GetExpMestDetail1(filter);

                if (!IsNotNullOrEmpty(ss))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_KhongTonTaiDuLieuCanChotNo);
                    return false;
                }

                List<long> tmps = data.ExpMestIds.Where(o => !ss.Exists(t => t.EXP_MEST_ID.Value == o)).ToList();
                if (IsNotNullOrEmpty(tmps))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Ton tai ExpMestId ko co du lieu chi tiet: " + LogUtil.TraceData("", tmps));
                    return false;
                }

                decimal total = ss.Sum(dt => (dt.AMOUNT ?? 0) * (dt.VIR_PRICE ?? 0) - (dt.DISCOUNT ?? 0));
                if (total != data.Transaction.AMOUNT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Tong so tien can thanh toan theo phieu xuat: " + total + " khac voi so tien ma client gui len: " + data.Transaction.AMOUNT);
                    return false;
                }
                details = ss;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
