using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisTransaction;
using MOS.MANAGER.YttDeposit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YTT.SDO;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Epayment
{
    class EpaymentProcessor : BusinessBase
    {
        internal EpaymentProcessor(CommonParam param)
            : base(param)
        {
        }

        internal void Run(HIS_TREATMENT treatment, List<V_HIS_TRANSACTION> transactions, ref List<V_HIS_TRANSACTION> collectedTransactions)
        {
            try
            {
                string theBranchCode = null;
                HIS_CARD hisCard = null;

                //Lay so du cua tai khoan the cua BN
                decimal? balance = new HisPatientBalance().GetCardBalance(treatment.PATIENT_ID, ref theBranchCode, ref hisCard);
                if (!balance.HasValue || balance <= 0)
                {
                    return;
                }

                //Lay ra d/s cho phep thanh toan duoc voi so tien toi da
                List<V_HIS_TRANSACTION> forPayments = this.MaximizePaymentAmount(balance.Value, transactions);

                //Thuc hien tao giao dich chuyen tien
                if (IsNotNullOrEmpty(forPayments) && hisCard != null && !string.IsNullOrWhiteSpace(hisCard.SERVICE_CODE))
                {
                    decimal total = forPayments.Sum(o => o.AMOUNT);
                    long amount = (long) Math.Round(total, 0);

                    YttHisDepositResultSDO yttResult = new YttDepositCreate(param).Create(amount, hisCard.SERVICE_CODE, theBranchCode);

                    //Neu thanh toan the thanh cong thi cap nhat "hinh thuc" cua cac giao dich sang loai la "the"
                    if (yttResult != null && yttResult.ResultCode == YttDepositCreate.SUCCESS)
                    {
                        List<long> transactionIds = forPayments.Select(o => o.ID).Distinct().ToList();
                        string idStr = string.Join(",", transactionIds);
                        string sql = string.Format("UPDATE HIS_TRANSACTION SET PAY_FORM_ID = :param1, TIG_TRANSACTION_CODE = :param2, TIG_TRANSACTION_TIME = :param3, CARD_ID = :param4, TDL_CARD_CODE = :param5, TDL_BANK_CARD_CODE = :param6  WHERE ID IN ({0})", idStr);

                        if (!DAOWorker.SqlDAO.Execute(sql, IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE, yttResult.TransactionCode, yttResult.TransactionTime, hisCard.ID, hisCard.CARD_CODE, hisCard.BANK_CARD_CODE))
                        {
                            LogSystem.Warn("Cap nhat hinh thuc (pay_form_id) cua giao dich that bai");
                        }
                        else
                        {
                            //Update lai du lieu de tra ve client
                            forPayments.ForEach(o => {
                                o.PAY_FORM_ID = IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE;
                                o.TIG_TRANSACTION_CODE = yttResult.TransactionCode;
                                o.TIG_TRANSACTION_TIME = yttResult.TransactionTime;
                                o.CARD_ID = hisCard.ID;
                                o.TDL_BANK_CARD_CODE = hisCard.BANK_CARD_CODE;
                                o.TDL_CARD_CODE = hisCard.CARD_CODE;
                            });
                            collectedTransactions = forPayments;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
        }

        private List<V_HIS_TRANSACTION> MaximizePaymentAmount(decimal balance, List<V_HIS_TRANSACTION> sereServs)
        {
            List<V_HIS_TRANSACTION> forPaymentsDeposits = null;
            try
            {
                if (sereServs != null && sereServs.Count > 0 && balance > 0)
                {
                    //Thanh toan theo tung giao dich
                    //Chi lay ra cac giao dich so tien nho hon so du tai khoan cua BN de xu ly
                    List<RequestPrice> requestPrices = sereServs
                        .Select(t => new RequestPrice
                        {
                            Id = t.ID,
                            TotalPatientPrice = t.AMOUNT
                        })
                        .Where(o => o.TotalPatientPrice > 0 && o.TotalPatientPrice <= balance)
                        .ToList();

                    if (requestPrices != null && requestPrices.Count > 0)
                    {
                        List<RequestPrice> forPayments = HisServiceReqUtil.SelectMaxList(requestPrices, balance);
                        forPaymentsDeposits = sereServs
                            .Where(o => forPayments != null
                                && forPayments.Exists(t => t.Id == o.ID)).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return forPaymentsDeposits;
        }
    }
}
