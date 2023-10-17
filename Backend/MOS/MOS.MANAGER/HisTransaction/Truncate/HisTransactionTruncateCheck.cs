using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisUserRoom;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Truncate
{
    class HisTransactionTruncateCheck : BusinessBase
    {
        internal HisTransactionTruncateCheck()
            : base()
        {

        }

        internal HisTransactionTruncateCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidTime(long cancelTime, long transactionTime)
        {
            bool valid = true;
            try
            {
                if (cancelTime < transactionTime)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ThoiGianHuyGiaoDichNhoHonThoiGianGiaoDich);
                    return false;
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

        internal bool VerifyRequireField(HisTransactionDeleteSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TransactionId <= 0) throw new ArgumentNullException("data.TransactionId");
                if (data.RequestRoomId <= 0) throw new ArgumentNullException("data.RequestRoomId");
                //if (string.IsNullOrWhiteSpace(data.DeleteReason)) throw new ArgumentNullException("data.DeleteReason");
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

        internal bool CheckSereServBill(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
            {
                return true;
            }
            List<V_HIS_SERE_SERV_BILL> hisSereServBills = new HisSereServBillGet().GetViewByBillId(data.ID);

            if (IsNotNullOrEmpty(hisSereServBills))
            {
                //Kiem tra xem BN co don nao da linh thuoc va chua thu hoi het hay ko
                List<long> serviceReqIds = hisSereServBills
                        .Where(o => o.SERVICE_REQ_ID.HasValue && o.TDL_SERVICE_REQ_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__DONK && o.AMOUNT > 0)
                        .Select(o => o.SERVICE_REQ_ID.Value)
                        .ToList();

                //Kiem tra neu don phong kham da thuc xuat thi khong cho phep huy thanh toan
                if (IsNotNullOrEmpty(serviceReqIds))
                {
                    HisExpMestFilterQuery filter = new HisExpMestFilterQuery();
                    filter.SERVICE_REQ_IDs = serviceReqIds;
                    filter.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE;
                    List<HIS_EXP_MEST> expMests = new HisExpMestGet().Get(filter);

                    if (IsNotNullOrEmpty(expMests))
                    {
                        List<string> expMestCodes = expMests.Select(o => o.EXP_MEST_CODE).ToList();
                        string expMestCodeStr = string.Join(",", expMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_BenhNhanDaLinhThuoc, expMestCodeStr);
                        return false;
                    }
                }

                if (!new HisSereServCheck(param).HasNoInvoice(hisSereServBills.Select(s => s.SERE_SERV_ID).ToList()))
                {
                    return false;
                }
            }
            return true;
        }

        internal bool CheckSeseDepoRepay(HIS_TRANSACTION data)
        {
            if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU)
                return true;
            List<HIS_SESE_DEPO_REPAY> hisSeseDepoRepays = new HisSeseDepoRepayGet().GetByRepayId(data.ID);
            if (IsNotNullOrEmpty(hisSeseDepoRepays))
            {
                //get view de lay sereServId check trang thang dang thuc hien
                List<V_HIS_SESE_DEPO_REPAY> views = new HisSeseDepoRepayGet().GetViewByRepayId(data.ID);
                List<long> sereServIds = views.Select(s => s.SERE_SERV_ID).Distinct().ToList();
                List<HIS_SERE_SERV> hisSereServs = new HisSereServGet(param).GetByIds(sereServIds);

                //Neu dich vu o trang thai khong thuc hien thi khong cho huy hoan ung, nguoi dung phai tich thuc hien thi moi cho phep huy
                if (!new HisSereServCheck().HasExecute(hisSereServs))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuKhongThucHienKhongChoPhepHuyHoanUng);
                    return false;
                }

            }
            return true;
        }

        internal bool HasPermission(long requestRoomId, ref V_HIS_CASHIER_ROOM cashierRoom)
        {
            cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ROOM_ID == requestRoomId).FirstOrDefault();
            if (cashierRoom == null)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                return false;
            }

            List<HIS_USER_ROOM> userRooms = HisUserRoomCFG.DATA.Where(o => o.IS_ACTIVE == Constant.IS_TRUE
                && o.ROOM_ID == requestRoomId
                && o.LOGINNAME == Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName()).ToList();

            if (!IsNotNullOrEmpty(userRooms))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_BanKhongCoQuyenTruyCapVaoPhongThuNgan, cashierRoom.CASHIER_ROOM_NAME);
                return false;
            }

            return true;
        }

        internal bool IsCreator(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                string loginname = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                if (data.CREATOR!=loginname)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDoNguoiKhacTao);
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
