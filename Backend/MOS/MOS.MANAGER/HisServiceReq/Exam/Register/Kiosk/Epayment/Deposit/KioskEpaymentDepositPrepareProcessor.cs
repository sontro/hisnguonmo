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
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisUserAccountBook;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Kiosk.Epayment.Deposit
{
    class KioskEpaymentDepositPrepareProcessor : BusinessBase
    {
        internal KioskEpaymentDepositPrepareProcessor(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(long requestRoomId, HIS_TREATMENT treatment, List<V_HIS_SERE_SERV> sereServs, List<V_HIS_SERVICE_REQ> serviceReqs, string usingCardServiceCode, ref long? cashierRoomId, ref long? accountBookId, ref string theBranchCode, ref HIS_CARD hisCard)
        {
            try
            {
                //Neu khong bat cau hinh thi khong xu ly
                if (EpaymentCFG.KIOSK_PAYMENT_OPTION != EpaymentCFG.KioskPaymentOption.AUTO_PAY)
                {
                    return false;
                }

                //Chi xu ly tam ung voi doi tuong khong phai BHYT
                if (!IsNotNullOrEmpty(sereServs) || sereServs.Exists(o => o.PATIENT_TYPE_ID == HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT))
                {
                    return false;
                }

                //Kiem tra xem phong chi dinh da duoc cau hinh phong thu ngan tuong ung chua
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == requestRoomId).FirstOrDefault();
                if (!room.DEFAULT_CASHIER_ROOM_ID.HasValue)
                {
                    LogSystem.Warn("Phong " + room.ROOM_CODE + " chua cau hinh phong thu ngan, khong the thuc hien tam thu dich vu tu dong");
                    return false;
                }

                //Lay so du cua tai khoan the duoc quet
                decimal? balance = new HisPatientBalance().GetCardBalance(treatment.PATIENT_ID, usingCardServiceCode, HisPatientBalance.CardFilterOption.SERVICE_CODE, ref theBranchCode, ref hisCard);

                //Neu the ko ton tai hoac so du nho hon 0 thi ket thuc xu ly
                if (!balance.HasValue || balance <= 0)
                {
                    return false;
                }

                //Xu ly de lay ra danh sach cac y lenh phu hop de thuc hien tam thu theo so du tai khoan cua BN
                List<V_HIS_SERE_SERV> forPayments = new List<V_HIS_SERE_SERV>();

                decimal toPaidPrice = sereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE.HasValue).Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value);

                if (toPaidPrice > balance.Value)
                {
                    LogSystem.Warn("So du khong du de thuc hien giao dich. So du: " + balance.Value + " So tien BN can thanh toan: " + toPaidPrice);
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
                        LogSystem.Warn("Tai khoan " + loginName + " va phong thu ngan tuong ung chua duoc gan so tam ung.");
                        return false;
                    }
                }
                else
                {
                    V_HIS_ACCOUNT_BOOK accountBook = new HisAccountBookGet().GetViewById(room.DEPOSIT_ACCOUNT_BOOK_ID.Value);
                    if (accountBook == null || accountBook.IS_ACTIVE != Constant.IS_TRUE)
                    {
                        LogSystem.Warn("So duoc gan tuong ung voi phong kham " + room.ROOM_CODE + " khong ton tai hoac bi khoa");
                    }
                    else if (accountBook.IS_FOR_DEPOSIT != Constant.IS_TRUE)
                    {
                        LogSystem.Warn("So duoc gan tuong ung voi phong kham " + room.ROOM_CODE + " ko phai so tam ung");
                    }
                    else if (accountBook.CURRENT_NUM_ORDER >= (accountBook.FROM_NUM_ORDER + accountBook.TOTAL - 1))
                    {
                        LogSystem.Warn("So duoc gan tuong ung voi phong kham " + room.ROOM_CODE + " da het so'");
                    }
                    else
                    {
                        accountBookId = room.DEPOSIT_ACCOUNT_BOOK_ID.Value;
                    }
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
        private List<V_HIS_SERE_SERV> GetUnpaid(HIS_SERVICE_REQ serviceReq)
        {
            HisSereServViewFilterQuery ssfilter = new HisSereServViewFilterQuery();
            ssfilter.SERVICE_REQ_ID = serviceReq.ID;
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

        private void MaximizePaymentAmount(decimal balance, List<V_HIS_SERE_SERV> sereServs, ref List<V_HIS_SERE_SERV> forPaymentsSereServs)
        {
            try
            {
                if (sereServs != null && sereServs.Count > 0 && balance > 0)
                {
                    List<RequestPrice> tmp = sereServs
                        .GroupBy(o => o.SERVICE_REQ_ID.Value)
                        .Select(t => new RequestPrice
                        {
                            Id = t.Key,
                            TotalPatientPrice = t.Sum(x => x.VIR_TOTAL_PATIENT_PRICE.Value)
                        }).ToList();

                    //Chi lay ra cac y lenh co chi phi nho hon so du tai khoan cua BN de xu ly
                    List<RequestPrice> serviceReqPrices = tmp
                        .Where(o => o.TotalPatientPrice > 0 && o.TotalPatientPrice <= balance)
                        .ToList();

                    if (serviceReqPrices != null && serviceReqPrices.Count > 0)
                    {
                        List<RequestPrice> forPaymentServiceReqs = HisServiceReqUtil.SelectMaxList(serviceReqPrices, balance);
                        
                        List<V_HIS_SERE_SERV> forPayments = sereServs
                            .Where(o => forPaymentServiceReqs != null
                                && forPaymentServiceReqs.Exists(t => t.Id == o.SERVICE_REQ_ID)).ToList();

                        if (IsNotNullOrEmpty(forPayments))
                        {
                            if (forPaymentsSereServs == null)
                            {
                                forPaymentsSereServs = new List<V_HIS_SERE_SERV>();
                            }
                            forPaymentsSereServs.AddRange(forPayments);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
