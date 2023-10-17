using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisCashierRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSeseTransReq;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransReq.Bill
{
    class HisTransReqBillCheck : BusinessBase
    {
        internal HisTransReqBillCheck()
            : base()
        {

        }

        internal HisTransReqBillCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTransReqBillSDO data, ref V_HIS_CASHIER_ROOM cashierRoom, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                WorkPlaceSDO workPlace = null;
                List<HIS_SERE_SERV> sereServs = null;
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.TransReq);
                valid = valid && this.HasWorkPlaceInfo(data.RequestRoomId, ref workPlace);
                valid = valid && this.IsCashierRoom(data, workPlace, ref cashierRoom);
                valid = valid && this.IsValidSereServ(data, ref sereServs);
                valid = valid && this.IsValidSereServBill(sereServs, data.SeseTransReqs, data.TransReq);
                valid = valid && this.IsUnlockAccountBook(data.TransReq.ACCOUNT_BOOK_ID, ref accountBook);
                valid = valid && this.IsGeneralOrder(accountBook);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        private bool IsCashierRoom(HisTransReqBillSDO data, WorkPlaceSDO workPlace, ref V_HIS_CASHIER_ROOM cashierRoom)
        {
            try
            {
                if (workPlace != null && workPlace.CashierRoomId.HasValue)
                {
                    cashierRoom = new HisCashierRoomGet().GetViewById(workPlace.CashierRoomId.Value);
                }
                else if (data.TransReq.CASHIER_ROOM_ID > 0)
                {
                    cashierRoom = new HisCashierRoomGet().GetViewById(data.TransReq.CASHIER_ROOM_ID);
                }

                if (cashierRoom == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.KhongCoThongTinPhongLamViecThuNgan);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        internal bool IsUnlockAccountBook(long accountBookId, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                accountBook = new HisAccountBookGet().GetViewById(accountBookId);

                if (accountBook == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("So thu chi khong ton tai tren he thong. accountBookId: " + accountBookId);
                }

                if (accountBook.IS_ACTIVE == null || accountBook.IS_ACTIVE.Value != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoDangBiKhoa, accountBook.ACCOUNT_BOOK_NAME);
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

        private bool IsValidAmount(HisTransReqBillSDO data, ref decimal exemption, ref decimal fundPaidTotal)
        {
            if (data != null && data.TransReq != null && IsNotNullOrEmpty(data.SeseTransReqs))
            {
                //so tien mien giam
                exemption = data.TransReq.EXEMPTION.HasValue ? data.TransReq.EXEMPTION.Value : 0;

                //so tien cac quy chi tra
                //fundPaidTotal = data.TransReq.HIS_BILL_FUND != null ? data.TransReq.HIS_BILL_FUND.Sum(o => o.AMOUNT) : 0;
                if (exemption + fundPaidTotal > data.TransReq.AMOUNT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_TongSoTienMienGiamVaQuyChiTraKhongDuocLonHonTongSoTienPhaiThanhToanDichVu);
                    return false;
                }

                //Tong so tien trong sere_serv_bill phai bang so tien trong transaction
                decimal sereServBillTotal = data.SeseTransReqs.Sum(o => o.PRICE);
                if (sereServBillTotal != data.TransReq.AMOUNT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("Tong so tien trong sere_serv_bill ko khop voi so tien trong transaction");
                    return false;
                }

                return true;
            }
            return false;
        }

        private bool IsValidSereServ(HisTransReqBillSDO data, ref List<HIS_SERE_SERV> sereServs)
        {
            try
            {
                if (data.TransReq != null && IsNotNullOrEmpty(data.SeseTransReqs))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = data.SeseTransReqs.Select(o => o.SERE_SERV_ID).Distinct().ToList();
                    HisSereServFilterQuery filter = new HisSereServFilterQuery();
                    filter.IDs = sereServIds;
                    filter.TREATMENT_ID = data.TransReq.TREATMENT_ID;
                    List<HIS_SERE_SERV> ss = new HisSereServGet().Get(filter);

                    List<long> invalidIds = sereServIds != null ? sereServIds.Where(o => ss == null || !ss.Exists(t => t.ID == o)).ToList() : null;

                    if (IsNotNullOrEmpty(invalidIds))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuKhongHopLe);
                        LogSystem.Warn("Loi du lieu. Ton tai sere_serv_id gui len ko co tren he thong hoac thuoc ho so dieu tri khac");
                        return false;
                    }
                    sereServs = ss;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        private bool IsValidSereServBill(List<HIS_SERE_SERV> sereServs, List<HIS_SESE_TRANS_REQ> seseTransReqs, HIS_TRANS_REQ transReq)
        {
            try
            {
                if (IsNotNullOrEmpty(seseTransReqs) && IsNotNullOrEmpty(sereServs))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = seseTransReqs.Select(o => o.SERE_SERV_ID).Distinct().ToList();

                    //Lay danh sach thong tin thanh toan (va chua bi huy) tuong ung voi sere_serv
                    List<HIS_SERE_SERV_BILL> existsBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServIds);
                    List<HIS_SERE_SERV_BILL> allBills = new List<HIS_SERE_SERV_BILL>();
                    if (IsNotNullOrEmpty(existsBills))
                    {
                        allBills.AddRange(existsBills);
                    }

                    List<string> serviceNames = new List<string>();
                    foreach (HIS_SERE_SERV s in sereServs)
                    {
                        decimal totalBill = allBills.Where(o => o.SERE_SERV_ID == s.ID).Sum(o => o.PRICE);
                        totalBill += seseTransReqs.Where(o => o.SERE_SERV_ID == s.ID).Sum(o => o.PRICE);

                        //Luu y: check lech tien voi "Constant.PRICE_DIFFERENCE", de tranh truong hop lam tron 
                        //(4 so sau phan thap phan) 
                        if (totalBill - s.VIR_TOTAL_PATIENT_PRICE.Value > Constant.PRICE_DIFFERENCE)
                        {
                            serviceNames.Add(s.TDL_SERVICE_NAME);
                        }
                    }

                    if (IsNotNullOrEmpty(serviceNames))
                    {
                        string nameStr = string.Join(",", serviceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_VuotQuaSoTienCanThanhToan, nameStr);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return false;
        }

        bool IsGeneralOrder(V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER.HasValue && accountBook.IS_NOT_GEN_TRANSACTION_ORDER.Value == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoBienLaiKhongTuDongTangSo, accountBook.ACCOUNT_BOOK_NAME);
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
    }
}
