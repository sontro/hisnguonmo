using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisUserAccountBook;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Epayment.Bill
{
    class EpaymentBillCheck : BusinessBase
    {
        internal EpaymentBillCheck()
        {
        }

        internal EpaymentBillCheck(CommonParam param)
            : base(param)
        {
        }

        internal bool VerifyRequireField(EpaymentBillSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (!IsGreaterThanZero(data.RequestRoomId)) throw new ArgumentNullException("data.RequestRoomId");
                if (string.IsNullOrWhiteSpace(data.CardServiceCode)) throw new ArgumentNullException("data.CardServiceCode");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool IsValidData(EpaymentBillSDO data, HIS_TREATMENT treatment, WorkPlaceSDO workPlace, ref long? cashierRoomId, ref long? accountBookId, ref string theBranchCode, ref List<V_HIS_SERE_SERV> forPaymentsSereServs, ref HIS_CARD hisCard)
        {
            bool valid = true;
            try
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                if (EpaymentCFG.EXECUTE_ROOM_PAYMENT_OPTION != EpaymentCFG.ExecuteRoomPaymentOption.OPTION2)
                {
                    MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_ChuaBatCauHinhThanhToanDienTuKhiKTDTTaiPK, EpaymentCFG.EXECUTE_ROOM_PAYMENT_OPTION_CFG);
                    return false;
                }

                //Kiem tra xem phong chi dinh da duoc cau hinh phong thu ngan tuong ung chua
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == workPlace.RoomId).FirstOrDefault();
                if (!room.DEFAULT_CASHIER_ROOM_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_PhongLamViecChuaDuocThietLapPhongThuNgan);
                    return false;
                }

                //Neu dv cha tick "thu sau" thi kiem tra, neu chua dong tien thi thuc hien xu ly de thu tien luon
                List<V_HIS_SERE_SERV> unpaidSereServs = this.GetUnpaid(treatment.ID);
                forPaymentsSereServs = IsNotNullOrEmpty(unpaidSereServs) ? unpaidSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;
                //Neu ko co dich vu can thanh toan thi ket thuc
                if (!IsNotNullOrEmpty(forPaymentsSereServs))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_HoSoKhongTonTaiDichVuCanThanhToan);
                    return false;
                }

                //Lay so du cua tai khoan the cua BN
                decimal? balance = new HisPatientBalance().GetCardBalance(treatment.PATIENT_ID, ref theBranchCode, ref hisCard);

                decimal toPaidPrice = unpaidSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE.HasValue).Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value);

                //Neu the ko ton tai hoac so du nho hon 0 thi ket thuc xu ly
                if (!balance.HasValue || balance <= 0 || toPaidPrice > balance.Value)
                {
                    string balanceStr = balance.HasValue ? balance.ToString() : "";
                    string toPaidStr = toPaidPrice.ToString();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TaiKhoanTheKhongDuSoDu, balanceStr, toPaidStr);
                    return false;
                }

                if (hisCard == null || hisCard.SERVICE_CODE != data.CardServiceCode)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_TheKhongHopLeHoacThuocBenhNhanKhac);
                    return false;
                }

                cashierRoomId = room.DEFAULT_CASHIER_ROOM_ID;

                //Neu phong kham da duoc gan so thu chi thi lay so thu chi gan theo phong kham
                //Neu phong kham chua duoc gan so thu chi thi lay so thu chi gan theo phong thu ngan hoac tai khoan bac sy
                if (!room.BILL_ACCOUNT_BOOK_ID.HasValue)
                {
                    //Kiem tra xem tai khoan nguoi dung da duoc gan so tam ung nao chua
                    HisUserAccountBookViewFilterQuery filter = new HisUserAccountBookViewFilterQuery();
                    filter.LOGINNAME__EXACT = loginName;
                    filter.IS_ACTIVE = Constant.IS_TRUE;
                    filter.ACCOUNT_BOOK_IS_ACTIVE = Constant.IS_TRUE;
                    filter.FOR_BILL = true;
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
                        f.FOR_BILL = true;
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
                    V_HIS_ACCOUNT_BOOK accountBook = new HisAccountBookGet().GetViewById(room.BILL_ACCOUNT_BOOK_ID.Value);
                    if (accountBook == null || accountBook.IS_ACTIVE != Constant.IS_TRUE)
                    {
                        string accountBookName = accountBook != null ? accountBook.ACCOUNT_BOOK_NAME : "";
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_SoBienLaiHoaDonDuocGanTuongUngVoiPhongLamViecKhongTonTaiHoacBiKhoa, accountBookName);
                        return false;
                    }

                    if (accountBook.IS_FOR_BILL != Constant.IS_TRUE)
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
                    accountBookId = room.BILL_ACCOUNT_BOOK_ID.Value;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private List<V_HIS_SERE_SERV> GetUnpaid(long treatmentId)
        {
            HisSereServViewFilterQuery ssfilter = new HisSereServViewFilterQuery();
            ssfilter.TREATMENT_ID = treatmentId;
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
