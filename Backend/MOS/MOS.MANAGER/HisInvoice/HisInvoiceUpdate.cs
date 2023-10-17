using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisInvoice
{
    partial class HisInvoiceUpdate : BusinessBase
    {
        private List<HIS_INVOICE> beforeUpdateHisInvoices = new List<HIS_INVOICE>();
        private HisSereServUpdate hisSereServUpdate;

        internal HisInvoiceUpdate()
            : base()
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal HisInvoiceUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal bool Cancel(HIS_INVOICE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisInvoiceCheck checker = new HisInvoiceCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                HIS_INVOICE raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotCancel(raw);
                if (valid)
                {
                    //Huy thong tin hoa don cua sere_serv 
                    this.ProcessSereServ(data.ID);

                    //Cap nhat thong tin huy cua hoa don
                    Mapper.CreateMap<HIS_INVOICE, HIS_INVOICE>();
                    HIS_INVOICE beforeUpdate = Mapper.Map<HIS_INVOICE>(raw);
                    this.beforeUpdateHisInvoices.Add(beforeUpdate);
                    raw.IS_CANCEL = MOS.UTILITY.Constant.IS_TRUE;
                    raw.CANCEL_REASON = data.CANCEL_REASON;
                    raw.CANCEL_TIME = data.CANCEL_TIME ?? Inventec.Common.DateTime.Get.Now();
                    raw.CANCEL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.CANCEL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    if (!DAOWorker.HisInvoiceDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoice_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoice that bai." + LogUtil.TraceData("data", data));
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessSereServ(long invoiceId)
        {
            List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByInvoiceId(invoiceId);
            if (IsNotNullOrEmpty(hisSereServs))
            {
                if (!this.hisSereServUpdate.UpdateInvoiceId(hisSereServs, null))
                {
                    throw new Exception("Huy thong tin hoa don cua sere_serv that bai. Ket thuc nghiep vu. Rollback du lieu");
                }
            }
        }

        private bool UpdateList(List<HIS_INVOICE> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisInvoiceCheck checker = new HisInvoiceCheck(param);
                List<HIS_INVOICE> listRaw = new List<HIS_INVOICE>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    this.beforeUpdateHisInvoices.AddRange(listRaw);
                    if (!DAOWorker.HisInvoiceDAO.UpdateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisInvoice_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisInvoice that bai." + LogUtil.TraceData("listData", listData));
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
            this.hisSereServUpdate.RollbackData();

            if (IsNotNullOrEmpty(this.beforeUpdateHisInvoices))
            {
                if (!DAOWorker.HisInvoiceDAO.UpdateList(this.beforeUpdateHisInvoices))
                {
                    LogSystem.Warn("Rollback du lieu HisInvoice that bai, can kiem tra lai." + LogUtil.TraceData("HisInvoices", this.beforeUpdateHisInvoices));
                }
            }
        }
    }
}
