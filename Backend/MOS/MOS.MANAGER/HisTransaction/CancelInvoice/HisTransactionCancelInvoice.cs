using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.CancelInvoice
{
    class HisTransactionCancelInvoice : BusinessBase
    {
        private List<HIS_TRANSACTION> beforeUpdateHisTransactions = new List<HIS_TRANSACTION>();

        internal HisTransactionCancelInvoice()
            : base()
        {
        }

        internal HisTransactionCancelInvoice(CommonParam param)
            : base(param)
        {
        }

        internal bool Run(CancelInvoiceSDO data, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                HIS_TRANSACTION raw = null;

                HisTransactionCheck transactionChecker = new HisTransactionCheck(param);
                HisTransactionCancelInvoiceCheck checker = new HisTransactionCancelInvoiceCheck(param);
                bool valid = true;
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && transactionChecker.VerifyId(data.TransactionId, ref raw);
                valid = valid && checker.IsValid(raw);
                if (valid)
                {
                    List<HIS_TRANSACTION> ListDataSampleInvoice = null;
                    ProcessListTransactionSampleInvoice(raw, ref ListDataSampleInvoice);

                    if (IsNotNullOrEmpty(ListDataSampleInvoice))
                    {
                        Mapper.CreateMap<List<HIS_TRANSACTION>, List<HIS_TRANSACTION>>();
                        List<HIS_TRANSACTION> oldTransactions = Mapper.Map<List<HIS_TRANSACTION>>(ListDataSampleInvoice);

                        foreach (var tran in ListDataSampleInvoice)
                        {
                            tran.IS_CANCEL_EINVOICE = Constant.IS_TRUE;
                            tran.EINVOICE_CANCEL_TIME = data.CancelTime;
                        }

                        if (!DAOWorker.HisTransactionDAO.UpdateList(ListDataSampleInvoice))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_HuyThatBai);
                            throw new Exception("Huy hoa don dien tu HisTransaction that bai." + LogUtil.TraceData("data", raw));
                        }

                        this.beforeUpdateHisTransactions.AddRange(oldTransactions);
                    }
                    else
                    {
                        Mapper.CreateMap<HIS_TRANSACTION, HIS_TRANSACTION>();
                        HIS_TRANSACTION oldTransaction = Mapper.Map<HIS_TRANSACTION>(raw);

                        raw.IS_CANCEL_EINVOICE = Constant.IS_TRUE;
                        raw.EINVOICE_CANCEL_TIME = data.CancelTime;

                        if (!DAOWorker.HisTransactionDAO.Update(raw))
                        {
                            MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_HuyThatBai);
                            throw new Exception("Huy hoa don dien tu HisTransaction that bai." + LogUtil.TraceData("data", raw));
                        }

                        this.beforeUpdateHisTransactions.Add(oldTransaction);
                    }

                    resultData = raw;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessListTransactionSampleInvoice(HIS_TRANSACTION raw, ref List<HIS_TRANSACTION> ListDataSampleInvoice)
        {
            try
            {
                if (IsNotNull(raw.ALL_TRANS_CODES_IN_INVOICE))
                {
                    string[] codes = raw.ALL_TRANS_CODES_IN_INVOICE.Split(',');

                    List<string> listCodeSql = new List<string>();
                    int skip = 0;
                    while (codes.Count() - skip > 0)
                    {
                        List<string> ids = codes.Skip(skip).Take(100).ToList();
                        skip += 100;

                        listCodeSql.Add(string.Format("('{0}')", string.Join("','", ids)));
                    }

                    string sql = string.Format("SELECT * FROM HIS_TRANSACTION WHERE TRANSACTION_CODE IN  {0}", string.Join(" OR TRANSACTION_CODE IN ", listCodeSql));
                    ListDataSampleInvoice = DAOWorker.SqlDAO.GetSql<HIS_TRANSACTION>(sql);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void Rollback()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisTransactions))
            {
                if (!DAOWorker.HisTransactionDAO.UpdateList(this.beforeUpdateHisTransactions))
                {
                    LogSystem.Warn("Rollback du lieu huy hoa don dien tu HisTransaction that bai, can kiem tra lai." + LogUtil.TraceData("HisTransactions", this.beforeUpdateHisTransactions));
                }
            }
        }
    }
}
