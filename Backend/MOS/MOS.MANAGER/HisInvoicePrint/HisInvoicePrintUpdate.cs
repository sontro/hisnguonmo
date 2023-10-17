using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoicePrint
{
    partial class HisInvoicePrintUpdate : BusinessBase
    {
		private List<HIS_INVOICE_PRINT> beforeUpdateHisInvoicePrints = new List<HIS_INVOICE_PRINT>();
		
        internal HisInvoicePrintUpdate()
            : base()
        {

        }

        internal HisInvoicePrintUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_INVOICE_PRINT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoicePrintCheck checker = new HisInvoicePrintCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_INVOICE_PRINT raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisInvoicePrints.Add(raw);
					if (!DAOWorker.HisInvoicePrintDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoicePrint_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoicePrint that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_INVOICE_PRINT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoicePrintCheck checker = new HisInvoicePrintCheck(param);
                List<HIS_INVOICE_PRINT> listRaw = new List<HIS_INVOICE_PRINT>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
					this.beforeUpdateHisInvoicePrints.AddRange(listRaw);
					if (!DAOWorker.HisInvoicePrintDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoicePrint_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoicePrint that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisInvoicePrints))
            {
                if (!new HisInvoicePrintUpdate(param).UpdateList(this.beforeUpdateHisInvoicePrints))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoicePrint that bai, can kiem tra lai." + LogUtil.TraceData("HisInvoicePrints", this.beforeUpdateHisInvoicePrints));
                }
            }
        }
    }
}
