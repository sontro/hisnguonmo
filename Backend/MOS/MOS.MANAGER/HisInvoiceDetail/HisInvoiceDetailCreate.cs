using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInvoiceDetail
{
    partial class HisInvoiceDetailCreate : BusinessBase
    {
        private List<HIS_INVOICE_DETAIL> recentHisInvoiceDetails = new List<HIS_INVOICE_DETAIL>();

        internal HisInvoiceDetailCreate()
            : base()
        {

        }

        internal HisInvoiceDetailCreate(CommonParam paramCreate)
            : base(paramCreate)
        {

        }

        internal bool Create(HIS_INVOICE_DETAIL data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceDetailCheck checker = new HisInvoiceDetailCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisInvoiceDetailDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoiceDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInvoiceDetail that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisInvoiceDetails.Add(data);
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

        internal bool CreateList(List<HIS_INVOICE_DETAIL> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoiceDetailCheck checker = new HisInvoiceDetailCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisInvoiceDetailDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoiceDetail_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisInvoiceDetail that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisInvoiceDetails.AddRange(listData);
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
            if (IsNotNullOrEmpty(this.recentHisInvoiceDetails))
            {
                if (!new HisInvoiceDetailTruncate(param).TruncateList(this.recentHisInvoiceDetails))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoiceDetail that bai, can kiem tra lai." + LogUtil.TraceData("HisInvoiceDetails", this.recentHisInvoiceDetails));
                }
            }
        }
    }
}
