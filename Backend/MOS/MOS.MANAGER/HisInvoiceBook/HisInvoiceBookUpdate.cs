using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoiceBook
{
    partial class HisInvoiceBookUpdate : BusinessBase
    {
		private List<HIS_INVOICE_BOOK> beforeUpdateHisInvoiceBooks = new List<HIS_INVOICE_BOOK>();
		
        internal HisInvoiceBookUpdate()
            : base()
        {

        }

        internal HisInvoiceBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_INVOICE_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceBookCheck checker = new HisInvoiceBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_INVOICE_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsNotExistInvoice(data);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisInvoiceBooks.Add(raw);
					if (!DAOWorker.HisInvoiceBookDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoiceBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoiceBook that bai." + LogUtil.TraceData("data", data));
                    }
                    
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

        private bool UpdateList(List<HIS_INVOICE_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoiceBookCheck checker = new HisInvoiceBookCheck(param);
                List<HIS_INVOICE_BOOK> listRaw = new List<HIS_INVOICE_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisInvoiceBooks.AddRange(listRaw);
					if (!DAOWorker.HisInvoiceBookDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoiceBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoiceBook that bai." + LogUtil.TraceData("listData", listData));
                    }
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
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisInvoiceBooks))
            {
                if (!new HisInvoiceBookUpdate(param).UpdateList(this.beforeUpdateHisInvoiceBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoiceBook that bai, can kiem tra lai." + LogUtil.TraceData("HisInvoiceBooks", this.beforeUpdateHisInvoiceBooks));
                }
            }
        }
    }
}
