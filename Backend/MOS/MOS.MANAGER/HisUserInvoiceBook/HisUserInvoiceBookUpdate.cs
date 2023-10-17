using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookUpdate : BusinessBase
    {
		private List<HIS_USER_INVOICE_BOOK> beforeUpdateHisUserInvoiceBooks = new List<HIS_USER_INVOICE_BOOK>();
		
        internal HisUserInvoiceBookUpdate()
            : base()
        {

        }

        internal HisUserInvoiceBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_USER_INVOICE_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserInvoiceBookCheck checker = new HisUserInvoiceBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_USER_INVOICE_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisUserInvoiceBooks.Add(raw);
					if (!DAOWorker.HisUserInvoiceBookDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserInvoiceBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserInvoiceBook that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_USER_INVOICE_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserInvoiceBookCheck checker = new HisUserInvoiceBookCheck(param);
                List<HIS_USER_INVOICE_BOOK> listRaw = new List<HIS_USER_INVOICE_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisUserInvoiceBooks.AddRange(listRaw);
					if (!DAOWorker.HisUserInvoiceBookDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserInvoiceBook_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisUserInvoiceBook that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisUserInvoiceBooks))
            {
                if (!new HisUserInvoiceBookUpdate(param).UpdateList(this.beforeUpdateHisUserInvoiceBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisUserInvoiceBook that bai, can kiem tra lai." + LogUtil.TraceData("HisUserInvoiceBooks", this.beforeUpdateHisUserInvoiceBooks));
                }
            }
        }
    }
}
