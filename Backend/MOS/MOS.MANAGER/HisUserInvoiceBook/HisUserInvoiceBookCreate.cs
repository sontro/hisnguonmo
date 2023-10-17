using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserInvoiceBook
{
    partial class HisUserInvoiceBookCreate : BusinessBase
    {
		private List<HIS_USER_INVOICE_BOOK> recentHisUserInvoiceBooks = new List<HIS_USER_INVOICE_BOOK>();
		
        internal HisUserInvoiceBookCreate()
            : base()
        {

        }

        internal HisUserInvoiceBookCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HisUserInvoiceBookSDO data, ref List<HIS_USER_INVOICE_BOOK> resultData)
        {
            bool result = false;
            try
            {
                if (data.InvoiceBookId > 0)
                {
                    if (!new HisUserInvoiceBookTruncate(param).TruncateByInvoiceBookId(data.InvoiceBookId))
                    {
                        throw new Exception("Xoa du lieu cu that bai. Ket thuc nghiep vu.");
                    }

                    if (IsNotNullOrEmpty(data.LoginNames))
                    {
                        List<HIS_USER_INVOICE_BOOK> toInsert = data.LoginNames.Select(o => new HIS_USER_INVOICE_BOOK
                        {
                            INVOICE_BOOK_ID = data.InvoiceBookId,
                            LOGINNAME = o
                        }).ToList();
                        if (this.CreateList(toInsert))
                        {
                            resultData = toInsert;
                        }
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

        internal bool Create(HIS_USER_INVOICE_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserInvoiceBookCheck checker = new HisUserInvoiceBookCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyExist(data);
                if (valid)
                {
					if (!DAOWorker.HisUserInvoiceBookDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserInvoiceBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisUserInvoiceBook that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisUserInvoiceBooks.Add(data);
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

        internal bool CreateList(List<HIS_USER_INVOICE_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserInvoiceBookCheck checker = new HisUserInvoiceBookCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisUserInvoiceBookDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisUserInvoiceBook_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisPtttCondition that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisUserInvoiceBooks.AddRange(listData);
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
            if (IsNotNull(this.recentHisUserInvoiceBooks))
            {
                if (!new HisUserInvoiceBookTruncate(param).TruncateList(this.recentHisUserInvoiceBooks))
                {
                    LogSystem.Warn("Rollback du lieu HisUserInvoiceBook that bai, can kiem tra lai." + LogUtil.TraceData("HisUserInvoiceBook", this.recentHisUserInvoiceBooks));
                }
            }
        }
    }
}
