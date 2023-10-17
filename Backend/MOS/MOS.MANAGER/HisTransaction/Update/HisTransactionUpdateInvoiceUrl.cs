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
    class HisTransactionUpdateInvoiceUrl : BusinessBase
    {
        internal HisTransactionUpdateInvoiceUrl()
            : base()
        {

        }

        internal HisTransactionUpdateInvoiceUrl(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTransactionInvoiceUrlSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_TRANSACTION> transactions = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && this.VerifyRequireField(data);
                valid = valid && this.VerifyInvoiceCode(data.InvoiceCode, ref transactions);
                if (valid)
                {
                    List<long> transactionIds = transactions.Select(s => s.ID).ToList();
                    string sql = DAOWorker.SqlDAO.AddInClause(transactionIds, "UPDATE HIS_TRANSACTION SET EINVOICE_URL = :param1, EINVOICE_LOGINNAME = :param2 WHERE %IN_CLAUSE%", "ID");

                    if (!DAOWorker.SqlDAO.Execute(sql, data.Url, data.Loginname))
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

        private bool VerifyInvoiceCode(string invoiceCode, ref List<HIS_TRANSACTION> transactions)
        {
            bool result = false;
            try
            {
                HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                filter.INVOICE_CODE__EXACT = invoiceCode;
                List<HIS_TRANSACTION> listData = new HisTransactionGet().Get(filter);
                if (listData == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_MaDinhDanhHoaDonDienTuKhongDung);
                    Logging("ListId invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => listData), listData) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => invoiceCode), invoiceCode), LogType.Error);
                    result = false;
                }
                else
                {
                    transactions = listData;
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

        internal bool VerifyRequireField(HisTransactionInvoiceUrlSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (String.IsNullOrWhiteSpace(data.InvoiceCode)) throw new ArgumentNullException("data.InvoiceCode");
                if (String.IsNullOrWhiteSpace(data.Url) && String.IsNullOrWhiteSpace(data.Loginname)) throw new ArgumentNullException("data.Url or data.Loginname");
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
    }
}
