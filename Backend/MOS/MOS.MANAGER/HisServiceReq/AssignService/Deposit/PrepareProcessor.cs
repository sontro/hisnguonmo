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

namespace MOS.MANAGER.HisServiceReq.AssignService.Deposit
{
    class PrepareProcessor : BusinessBase
    {
        internal PrepareProcessor(CommonParam param)
            : base(param)
        {
        }

        internal void Run(long requestRoomId, HIS_TREATMENT treatment, HIS_SERVICE_REQ parent, List<V_HIS_SERE_SERV> sereServs, List<V_HIS_SERVICE_REQ> serviceReqs, List<HIS_SERE_SERV> existedSereServs, ref long? cashierRoomId, ref long? accountBookId, ref List<V_HIS_SERE_SERV> forPaymentsSereServs, ref string theBranchCode, ref HIS_CARD hisCard)
        {
            try
            {
                //Neu khong bat cau hinh thi khong xu ly
                if (!EpaymentCFG.IS_USING_EXECUTE_ROOM_PAYMENT)
                {
                    return;
                }

                //Chi xu ly tam ung voi doi tuong khong phai BHYT
                List<V_HIS_SERE_SERV> nonBhytSereServs = IsNotNullOrEmpty(sereServs) ? sereServs.Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;

                //lay ra dich vu kham co doi tuong khong phai BHYT co tien benh nhan phai tra va khong phai dich vu kham cha
                List<HIS_SERE_SERV> nonBhytExamOtherSereServs = IsNotNullOrEmpty(existedSereServs) ? existedSereServs.Where(o => o.TDL_SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__KH && o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0 && (!IsNotNull(parent) || o.SERVICE_REQ_ID != parent.ID)).ToList() : null;

                if (IsNotNullOrEmpty(nonBhytExamOtherSereServs))
                {
                    List<V_HIS_SERE_SERV> addSereServ = GetUnpaidBySereServ(nonBhytExamOtherSereServs);

                    if (IsNotNullOrEmpty(addSereServ))
                    {
                        if (!IsNotNullOrEmpty(nonBhytSereServs))
                        {
                            nonBhytSereServs = new List<V_HIS_SERE_SERV>();
                        }

                        nonBhytSereServs.AddRange(addSereServ);
                    }
                }

                //Neu dv cha tick "thu sau" thi kiem tra, neu chua dong tien thi thuc hien xu ly de thu tien luon
                List<V_HIS_SERE_SERV> unpaidParents = parent != null && parent.IS_NOT_REQUIRE_FEE == Constant.IS_TRUE ? this.GetUnpaid(parent) : null;
                unpaidParents = IsNotNullOrEmpty(unpaidParents) ? unpaidParents.Where(o => o.PATIENT_TYPE_ID != HisPatientTypeCFG.PATIENT_TYPE_ID__BHYT && o.VIR_TOTAL_PATIENT_PRICE > 0).ToList() : null;
                //Neu ko co dich vu can thanh toan thi ket thuc
                if (!IsNotNullOrEmpty(unpaidParents) && !IsNotNullOrEmpty(nonBhytSereServs))
                {
                    return;
                }


                //Kiem tra xem phong chi dinh da duoc cau hinh phong thu ngan tuong ung chua
                V_HIS_ROOM room = HisRoomCFG.DATA.Where(o => o.ID == requestRoomId).FirstOrDefault();
                if (!room.DEFAULT_CASHIER_ROOM_ID.HasValue)
                {
                    LogSystem.Warn("Phong " + room.ROOM_CODE + " chua cau hinh phong thu ngan, khong the thuc hien tam thu dich vu tu dong");
                    return;
                }

                //Lay so du cua tai khoan the cua BN
                decimal? balance = new HisPatientBalance().GetCardBalance(treatment.PATIENT_ID, ref theBranchCode, ref hisCard);

                //Neu the ko ton tai hoac so du nho hon 0 thi ket thuc xu ly
                if (!balance.HasValue || balance <= 0 || hisCard == null || string.IsNullOrWhiteSpace(hisCard.SERVICE_CODE))
                {
                    return;
                }

                //Xu ly de lay ra danh sach cac y lenh phu hop de thuc hien tam thu theo so du tai khoan cua BN                
                //Neu cau hinh bat buoc phai thanh toan toan bo
                if (EpaymentCFG.MUST_PAY_ALL)
                {
                    decimal toPaidPrice = 0;
                    toPaidPrice += unpaidParents != null ? unpaidParents.Where(o => o.VIR_TOTAL_PATIENT_PRICE.HasValue).Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value) : 0;
                    toPaidPrice += nonBhytSereServs != null ? nonBhytSereServs.Where(o => o.VIR_TOTAL_PATIENT_PRICE.HasValue).Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value) : 0;

                    if (toPaidPrice <= balance.Value)
                    {
                        forPaymentsSereServs = new List<V_HIS_SERE_SERV>();
                        if (IsNotNullOrEmpty(unpaidParents))
                        {
                            forPaymentsSereServs.AddRange(unpaidParents);
                        }
                        if (IsNotNullOrEmpty(nonBhytSereServs))
                        {
                            forPaymentsSereServs.AddRange(nonBhytSereServs);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //Neu ko bat buoc thanh toan tat ca thi xu ly de lay ra cac y lenh co phep thanh toan (voi toi da so tien)
                else
                {
                    //Uu tien thanh toan dv cha truoc
                    this.MaximizePaymentAmount(balance.Value, unpaidParents, ref forPaymentsSereServs);

                    //Neu thanh toan duoc het dv cha thi thanh toan dv moi tao
                    if ((!IsNotNullOrEmpty(unpaidParents) && !IsNotNullOrEmpty(forPaymentsSereServs))
                        || (IsNotNullOrEmpty(unpaidParents) && IsNotNullOrEmpty(forPaymentsSereServs) && unpaidParents.Count == forPaymentsSereServs.Count))
                    {
                        decimal parentTotal = forPaymentsSereServs != null ? forPaymentsSereServs.Sum(o => o.VIR_TOTAL_PATIENT_PRICE.Value) : 0;
                        //Xu ly de lay ra danh sach cac y lenh phu hop de thuc hien tam thu theo so du tai khoan cua BN
                        this.MaximizePaymentAmount(balance.Value - parentTotal, nonBhytSereServs, ref forPaymentsSereServs);
                    }
                }

                if (!IsNotNullOrEmpty(forPaymentsSereServs))
                {
                    return;
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
                        return;
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
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
        }

        private List<V_HIS_SERE_SERV> GetUnpaidBySereServ(List<HIS_SERE_SERV> nonBhytExamOtherSereServs)
        {
            List<V_HIS_SERE_SERV> result = null;
            try
            {
                if (IsNotNullOrEmpty(nonBhytExamOtherSereServs))
                {
                    List<long> ids = nonBhytExamOtherSereServs.Select(o => o.ID).ToList();

                    List<HIS_SERE_SERV_BILL> hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(ids);
                    List<HIS_SERE_SERV_DEPOSIT> hasDeposits = new HisSereServDepositGet().GetNoCancelBySereServIds(ids);
                    if (IsNotNullOrEmpty(hasDeposits))
                    {
                        List<HIS_SESE_DEPO_REPAY> hasRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(hasDeposits.Select(s => s.ID).ToList());
                        if (IsNotNullOrEmpty(hasRepays))
                        {
                            hasDeposits = hasDeposits.Where(o => !hasRepays.Exists(e => e.SERE_SERV_DEPOSIT_ID == o.ID)).ToList();
                        }
                    }

                    //Lay d/s sere_serv chua duoc thanh toan hoac tam ung de xu ly thanh toan
                    List<HIS_SERE_SERV> unpaidParent = nonBhytExamOtherSereServs.Where(t => (hasBills == null || !hasBills.Exists(o => o.SERE_SERV_ID == t.ID))
                            && (hasDeposits == null || !hasDeposits.Exists(o => o.SERE_SERV_ID == t.ID))).ToList();

                    if (IsNotNullOrEmpty(unpaidParent))
                    {
                        Mapper.CreateMap<HIS_SERE_SERV, V_HIS_SERE_SERV>();
                        result = Mapper.Map<List<V_HIS_SERE_SERV>>(unpaidParent);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
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
