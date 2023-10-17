using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInvoiceBook
{
    partial class HisInvoiceBookCreate : BusinessBase
    {
        private HIS_INVOICE_BOOK recentHisInvoiceBook;

        internal HisInvoiceBookCreate()
            : base()
        {

        }

        internal HisInvoiceBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_INVOICE_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceBookCheck checker = new HisInvoiceBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.VerifyRangeAndUpdateLinkId(data);
                if (valid)
                {
                    this.ProcessUserInvoiceBook(data, Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName());
                    if (!DAOWorker.HisInvoiceBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoiceBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInvoiceBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisInvoiceBook = data;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessUserInvoiceBook(HIS_INVOICE_BOOK invoiceBook, string loginname)
        {
            if (invoiceBook != null)
            {
                HIS_USER_INVOICE_BOOK user = new HIS_USER_INVOICE_BOOK();
                user.LOGINNAME = loginname;
                user.CREATOR = loginname;
                List<HIS_USER_INVOICE_BOOK> users = new List<HIS_USER_INVOICE_BOOK>();
                users.Add(user);
                invoiceBook.HIS_USER_INVOICE_BOOK = users;
            }
        }

        internal void RollbackData()
        {
            if (this.recentHisInvoiceBook != null)
            {
                if (!new HisInvoiceBookTruncate(param).Truncate(this.recentHisInvoiceBook))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoiceBook that bai, can kiem tra lai." + LogUtil.TraceData("HisInvoiceBook", this.recentHisInvoiceBook));
                }
            }
        }

        private bool VerifyRangeAndUpdateLinkId(HIS_INVOICE_BOOK data)
        {
            List<V_HIS_INVOICE_BOOK> invoiceBooks = new HisInvoiceBookGet().GetViewBySymbolCodeAndTemplateCode(data.SYMBOL_CODE, data.TEMPLATE_CODE);
            if (IsNotNullOrEmpty(invoiceBooks))
            {
                V_HIS_INVOICE_BOOK previous = invoiceBooks.OrderByDescending(o => o.FROM_NUM_ORDER).FirstOrDefault();
                long lastNumberOfPrevious = previous.FROM_NUM_ORDER + previous.TOTAL - 1;
                if (lastNumberOfPrevious + 1 != data.FROM_NUM_ORDER)
                {
                    string previousRange = string.Format("{0} - {1}", previous.FROM_NUM_ORDER, lastNumberOfPrevious);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisInvoiceBook_DaiSoMoiCanLienTiepVoiSoCu, previousRange);
                    LogSystem.Info("Dai so moi can lien tiep voi so cu " + LogUtil.TraceData("previous", previous));
                    return false;
                }

                //gan link_id cua so moi chinh bang ID cua so truoc do
                data.LINK_ID = previous.ID;

                //tranh truong hop client gui len du lieu, dan den loi vi entity check constraint
                data.ID = 0;
                data.HIS_INVOICE = null;
                data.HIS_INVOICE_BOOK1 = null;
                data.HIS_INVOICE_BOOK2 = null;
                data.HIS_USER_INVOICE_BOOK = null;
            }
            else
            {
                //neu truoc do ko co so nao co so, mau giong thi gan link_id null
                //Ko can check "so phai bat dau tu 1" vi co truong hop dung so cu co tu he thong PM cu (swap phan mem khac)
                data.LINK_ID = null;
            }
            return true;
        }
    }
}
