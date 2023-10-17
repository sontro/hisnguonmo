using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Update
{
    class HisTransactionUpdateInvoiceInfo : BusinessBase
    {
        internal HisTransactionUpdateInvoiceInfo()
            : base()
        {

        }

        internal HisTransactionUpdateInvoiceInfo(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTransactionInvoiceInfoSDO data)
        {
            bool result = false;
            try
            {
                HisTransactionInvoiceListInfoSDO sdo = new HisTransactionInvoiceListInfoSDO();
                sdo.Ids = new List<long>() { data.Id };
                sdo.InvoiceCode = data.InvoiceCode;
                sdo.InvoiceSys = data.InvoiceSys;
                sdo.EinvoiceNumOrder = data.EinvoiceNumOrder;
                sdo.EInvoiceTime = data.EInvoiceTime;
                sdo.EinvoiceLoginname = data.EinvoiceLoginname;
                result = this.Run(sdo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal bool Run(HisTransactionInvoiceListInfoSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TRANSACTION> transactions = new List<HIS_TRANSACTION>();
                HisTransactionCheck checker = new HisTransactionCheck(param);

                valid = valid && this.VerifyRequireField(data);
                valid = valid && checker.VerifyIds(data.Ids, transactions);
                valid = valid && checker.IsUnLock(transactions);
                valid = valid && checker.IsUnCancel(transactions);
                valid = valid && checker.IsBill(transactions);
                if (valid)
                {
                    string modifier = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    List<string> transactionCodes = transactions.Select(o => o.TRANSACTION_CODE).ToList();
                    string allTransactionCodesInInvoice = string.Join(",", transactionCodes);

                    string sql = DAOWorker.SqlDAO.AddInClause(data.Ids, "UPDATE HIS_TRANSACTION SET INVOICE_CODE = :param1, INVOICE_SYS = :param2, EINVOICE_NUM_ORDER = :param3, MODIFIER = :param4, ALL_TRANS_CODES_IN_INVOICE = :param5, EINVOICE_TIME = :param6, EINVOICE_LOGINNAME = :param7 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sql, data.InvoiceCode, data.InvoiceSys, data.EinvoiceNumOrder, modifier, allTransactionCodesInInvoice, data.EInvoiceTime, data.EinvoiceLoginname))
                    {
                        throw new Exception("Cap nhat thong tin hoa don dien tu cho bang HIS_TRANSACTION that bai");
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }


        internal bool VerifyRequireField(HisTransactionInvoiceListInfoSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNullOrEmpty(data.Ids)) throw new ArgumentNullException("data.Ids");
                if (String.IsNullOrWhiteSpace(data.InvoiceCode)) throw new ArgumentNullException("data.InvoiceCode");
                if (String.IsNullOrWhiteSpace(data.InvoiceSys)) throw new ArgumentNullException("data.InvoiceSys");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Error(ex);
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

    }
}
