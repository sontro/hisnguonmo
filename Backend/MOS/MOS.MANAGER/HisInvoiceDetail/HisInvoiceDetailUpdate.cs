using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoiceDetail
{
    partial class HisInvoiceDetailUpdate : BusinessBase
    {
        private List<HIS_INVOICE_DETAIL> beforeUpdateHisInvoiceDetails = new List<HIS_INVOICE_DETAIL>();

        internal HisInvoiceDetailUpdate()
            : base()
        {

        }

        internal HisInvoiceDetailUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_INVOICE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceDetailCheck checker = new HisInvoiceDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_INVOICE_DETAIL raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    this.beforeUpdateHisInvoiceDetails.Add(raw);
                    if (!DAOWorker.HisInvoiceDetailDAO.Update(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoiceDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoiceDetail that bai." + LogUtil.TraceData("data", data));
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

        internal bool UpdateList(List<HIS_INVOICE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoiceDetailCheck checker = new HisInvoiceDetailCheck(param);
                List<HIS_INVOICE_DETAIL> listRaw = new List<HIS_INVOICE_DETAIL>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisInvoiceDetails.AddRange(listRaw);
                    if (!DAOWorker.HisInvoiceDetailDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoiceDetail_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoiceDetail that bai." + LogUtil.TraceData("listData", listData));
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisInvoiceDetails))
            {
                if (!new HisInvoiceDetailUpdate(param).UpdateList(this.beforeUpdateHisInvoiceDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoiceDetail that bai, can kiem tra lai." + LogUtil.TraceData("HisInvoiceDetails", this.beforeUpdateHisInvoiceDetails));
                }
            }
        }
    }
}
