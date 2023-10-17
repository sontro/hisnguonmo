using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisUserAccountBook;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Epayment.Deposit
{
    class EpaymentDepositCheck : BusinessBase
    {
        internal EpaymentDepositCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool IsValid(EpaymentDepositSD data, ref long? cashierRoomId, ref long? accountBookId, ref List<V_HIS_SERE_SERV> forPaymentsSereServs, ref string theBranchCode, ref HIS_CARD hisCard)
        {
            try
            {
                //Neu khong bat cau hinh thi khong xu ly
                if (!EpaymentCFG.IS_USING_EXECUTE_ROOM_PAYMENT && EpaymentCFG.KIOSK_PAYMENT_OPTION != EpaymentCFG.KioskPaymentOption.AUTO_PAY)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ChuaBatCauHinhThanhToanDienTuTaiPhongKham);
                    return false;
                }

                List<HIS_SERVICE_REQ> serviceReqs = new HisServiceReqGet().GetByIds(data.ServiceReqIds);
                if (!IsNotNullOrEmpty(serviceReqs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.ServiceReqIds ko ton tai");
                    return false;
                }

                if (data.IncludeAttachment == true)
                {
                    HisServiceReqFilterQuery filter = new HisServiceReqFilterQuery();
                    filter.ATTACHED_IDs = data.ServiceReqIds;
                    filter.IS_NO_EXECUTE = false;
                    List<HIS_SERVICE_REQ> serviceReqAttachs = new HisServiceReqGet().Get(filter);

                    if (IsNotNullOrEmpty(serviceReqAttachs))
                        serviceReqs.AddRange(serviceReqAttachs);
                }

                if (serviceReqs.Select(o => o.TREATMENT_ID).Distinct().ToList().Count > 1)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("data.ServiceReqIds thuoc nhieu ho so dieu tri (HIS_TREATMENT) khac nhau");
                    return false;
                }

                //Neu dv cha tick "thu sau" thi kiem tra, neu chua dong tien thi thuc hien xu ly de thu tien luon
                List<V_HIS_SERE_SERV> unpaidSereServs = null;
                if (data.IncludeAttachment == true)
                {
                    List<long> srIds = serviceReqs.Select(o => o.ID).ToList();
                    if (IsNotNullOrEmpty(srIds))
                        unpaidSereServs = this.GetUnpaid(srIds);
                }
                else if (data.IncludeAttachment == false)
                {
                    unpaidSereServs = this.GetUnpaid(data.ServiceReqIds);
                }
                
                forPaymentsSereServs = IsNotNullOrEmpty(unpaidSereServs) ? unpaidSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;
                //Neu ko co dich vu can thanh toan thi ket thuc
                if (!IsNotNullOrEmpty(forPaymentsSereServs))
                {
                    List<string> serviceReqCodes = serviceReqs.Select(o => o.SERVICE_REQ_CODE).ToList();
                    string codeStr = string.Join(", ", serviceReqCodes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_CacDichVuDaDuocThanhToanHoacTamUng, codeStr);
                    return false;
                }


                //Kiem tra xem phong chi dinh da duoc cau hinh phong thu ngan tuong ung chua
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == data.RequestRoomId).FirstOrDefault();
                if (!room.DEFAULT_CASHIER_ROOM_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_PhongLamViecChuaDuocThietLapPhongThuNgan);
                    return false;
                }

                HisPatientBalance.CardFilterOption cardFilterOption = !string.IsNullOrWhiteSpace(data.CardServiceCode) ? HisPatientBalance.CardFilterOption.SERVICE_CODE : HisPatientBalance.CardFilterOption.NONE;

                //Lay so du cua tai khoan the cua BN
                decimal? balance = new HisPatientBalance().GetCardBalance(serviceReqs[0].TDL_PATIENT_ID, data.CardServiceCode, cardFilterOption, ref theBranchCode, ref hisCard);

                decimal toPaidPrice = unpaidSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE.HasValue).Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value);

                //Neu the ko ton tai hoac so du nho hon 0 thi ket thuc xu ly
                if (!balance.HasValue || balance <= 0 || toPaidPrice > balance.Value || hisCard == null || string.IsNullOrWhiteSpace(hisCard.SERVICE_CODE))
                {
                    string balanceStr = balance.HasValue ? balance.ToString() : "";
                    string toPaidStr = toPaidPrice.ToString();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TaiKhoanTheKhongDuSoDu, balanceStr, toPaidStr);
                    return false;
                }

                cashierRoomId = room.DEFAULT_CASHIER_ROOM_ID;
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                //Neu phong kham da duoc gan so thu chi thi lay so thu chi gan theo phong kham
                //Neu phong kham chua duoc gan so thu chi thi lay so thu chi gan theo phong thu ngan hoac tai khoan bac sy
                if (!room.DEPOSIT_ACCOUNT_BOOK_ID.HasValue)
                {
                    //Kiem tra xem tai khoan nguoi dung da duoc gan so tam ung nao chua
                    HisUserAccountBookViewFilterQuery filter = new HisUserAccountBookViewFilterQuery();
                    filter.LOGINNAME__EXACT = loginName;
                    filter.IS_ACTIVE = Constant.IS_TRUE;
                    filter.ACCOUNT_BOOK_IS_ACTIVE = Constant.IS_TRUE;
                    filter.FOR_DEPOSIT = true;
                    filter.IS_OUT_OF_BILL = false;

                    List<V_HIS_USER_ACCOUNT_BOOK> userAccountBooks = new HisUserAccountBookGet().GetView(filter);
                    if (IsNotNullOrEmpty(userAccountBooks))
                    {
                        accountBookId = userAccountBooks.OrderByDescending(o => o.ID).Select(o => o.ACCOUNT_BOOK_ID).FirstOrDefault();
                    }
                    else
                    {
                        HisCaroAccountBookViewFilterQuery f = new HisCaroAccountBookViewFilterQuery();
                        f.CASHIER_ROOM_ID = room.DEFAULT_CASHIER_ROOM_ID.Value;
                        f.FOR_DEPOSIT = true;
                        f.IS_OUT_OF_BILL = false;
                        f.ACCOUNT_BOOK_IS_ACTIVE = true;
                        List<V_HIS_CARO_ACCOUNT_BOOK> caroAccountBook = new HisCaroAccountBookGet().GetView(f);

                        if (IsNotNullOrEmpty(caroAccountBook))
                        {
                            accountBookId = caroAccountBook.OrderByDescending(o => o.ID).Select(o => o.ACCOUNT_BOOK_ID).FirstOrDefault();
                        }
                    }

                    if (!accountBookId.HasValue)
                    {
                        V_HIS_CASHIER_ROOM cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ID == room.DEFAULT_CASHIER_ROOM_ID.Value).FirstOrDefault();
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TaiKhoanVaPhongThuNganTuongUngChuaDuocGanSoTamUng, loginName, cashierRoom.CASHIER_ROOM_NAME);
                        return false;
                    }
                }
                else
                {
                    V_HIS_ACCOUNT_BOOK accountBook = new HisAccountBookGet().GetViewById(room.DEPOSIT_ACCOUNT_BOOK_ID.Value);
                    if (accountBook == null || accountBook.IS_ACTIVE != Constant.IS_TRUE)
                    {
                        string accountBookName = accountBook != null ? accountBook.ACCOUNT_BOOK_NAME : "";
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoBienLaiHoaDonDuocGanTuongUngVoiPhongLamViecKhongTonTaiHoacBiKhoa, accountBookName);
                        return false;
                    }
                    
                    if (accountBook.IS_FOR_DEPOSIT != Constant.IS_TRUE)
                    {
                        string accountBookName = accountBook != null ? accountBook.ACCOUNT_BOOK_NAME : "";
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoBienLaiHoaDonDuocGanTuongUngVoiPhongLamViecKhongPhaiSoTamUng, accountBookName);
                        return false;
                    }
                    
                    if (accountBook.CURRENT_NUM_ORDER >= (accountBook.FROM_NUM_ORDER + accountBook.TOTAL - 1))
                    {
                        string accountBookName = accountBook != null ? accountBook.ACCOUNT_BOOK_NAME : "";
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoBienLaiHoaDonDuocGanTuongUngVoiPhongLamViecDaHetSo, accountBookName);
                        return false;
                    }
                     accountBookId = room.DEPOSIT_ACCOUNT_BOOK_ID.Value;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        /// <summary>
        /// Lay cac dich vu chua duoc thanh toan tuong ung voi y lenh
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        private List<V_HIS_SERE_SERV> GetUnpaid(List<long> serviceReqIds)
        {
            HisSereServViewFilterQuery ssfilter = new HisSereServViewFilterQuery();
            ssfilter.SERVICE_REQ_IDs = serviceReqIds;
            ssfilter.HAS_EXECUTE = true;
            ssfilter.IS_EXPEND = false;
            List<V_HIS_SERE_SERV> parentSereServs = new HisSereServGet().GetView(ssfilter);

            List<V_HIS_SERE_SERV> ss = parentSereServs != null ? parentSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;

            List<HIS_SERE_SERV_BILL> hasBills = null;
            List<HIS_SERE_SERV_DEPOSIT> hasDeposits = null;

            List<long> ids = ss != null ? ss.Select(o => o.ID).ToList() : null;

            if (IsNotNullOrEmpty(ids))
            {
                hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(ids);
                hasDeposits = new HisSereServDepositGet().GetNoCancelBySereServIds(ids);
                if (IsNotNullOrEmpty(hasDeposits))
                {
                    List<HIS_SESE_DEPO_REPAY> hasRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(hasDeposits.Select(s => s.ID).ToList());
                    if (IsNotNullOrEmpty(hasRepays))
                    {
                        hasDeposits = hasDeposits.Where(o => !hasRepays.Exists(e => e.SERE_SERV_DEPOSIT_ID == o.ID)).ToList();
                    }
                }
            }

            //Lay d/s sere_serv chua duoc thanh toan hoac tam ung de xu ly thanh toan
            List<V_HIS_SERE_SERV> unpaidParent = ss != null ?
                ss.Where(t => (hasBills == null || !hasBills.Exists(o => o.SERE_SERV_ID == t.ID))
                    && (hasDeposits == null || !hasDeposits.Exists(o => o.SERE_SERV_ID == t.ID))).ToList() : null;
            return unpaidParent;
        }
    }
}
