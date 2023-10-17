using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisInvoiceBook;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisInvoice
{
    /// <summary>
    /// Xu ly cap nhat cac truong thong tin lien quan den hoa don ma cho phep sua (vd: thong tin nguoi mua, ...)
    /// </summary>
    class HisInvoiceUpdateInfo : BusinessBase
    {
        internal HisInvoiceUpdateInfo()
            : base()
        {

        }

        internal HisInvoiceUpdateInfo(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisInvoiceUpdateInfoSDO sdo, ref HIS_INVOICE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_INVOICE raw = null;
                HisInvoiceCheck checker = new HisInvoiceCheck(param);

                valid = valid && checker.VerifyId(sdo.InvoiceId, ref raw);
                valid = valid && checker.IsNotCancel(raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_INVOICE, HIS_INVOICE>();
                    HIS_INVOICE before = Mapper.Map<HIS_INVOICE>(raw);

                    raw.BUYER_ACCOUNT_NUMBER = sdo.BuyerAccountNumber;
                    raw.BUYER_ADDRESS = sdo.BuyerAddress;
                    raw.BUYER_NAME = sdo.BuyerName;
                    raw.BUYER_ORGANIZATION = sdo.BuyerOrganization;
                    raw.BUYER_TAX_CODE = sdo.BuyerTaxCode;

                    if (!DAOWorker.HisInvoiceDAO.Update(raw))
                    {
                        return false;
                    }
                    result = true;
                    resultData = raw;

                    this.Log(before, resultData);
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

        private void Log(HIS_INVOICE before, HIS_INVOICE invoice)
        {
            try
            {
                HIS_INVOICE_BOOK invoiceBook = new HisInvoiceBookGet().GetById(invoice.INVOICE_BOOK_ID);

                new EventLogGenerator(EventLog.Enum.HisInvoice_CapNhatThongTin, invoiceBook.TEMPLATE_CODE, invoiceBook.SYMBOL_CODE, invoiceBook.FROM_NUM_ORDER, invoiceBook.ID, this.LogInfo(before), this.LogInfo(invoice)).InvoiceNumOrder(invoice.NUM_ORDER.ToString()).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private string LogInfo(HIS_INVOICE input)
        {
            string rs = "";
            try
            {
                string name = LogCommonUtil.GetEventLogContent(EventLog.Enum.TenNguoiMua);
                string address = LogCommonUtil.GetEventLogContent(EventLog.Enum.DiaChiNguoiMua);
                string accountNumber = LogCommonUtil.GetEventLogContent(EventLog.Enum.SoTaiKhoanNguoiMua);
                string taxcode = LogCommonUtil.GetEventLogContent(EventLog.Enum.MaSoThueNguoiMua);
                string organization = LogCommonUtil.GetEventLogContent(EventLog.Enum.CongTyNguoiMua);

                rs = string.Format("{0}: {1}; {2}: {3}; {4}: {5}; {6}: {7}; {8}: {9}",
                    name, CommonUtil.NVL(input.BUYER_NAME),
                    address, CommonUtil.NVL(input.BUYER_ADDRESS),
                    accountNumber, CommonUtil.NVL(input.BUYER_ACCOUNT_NUMBER),
                    taxcode, CommonUtil.NVL(input.BUYER_TAX_CODE),
                    organization, CommonUtil.NVL(input.BUYER_ORGANIZATION)
                    );
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return rs;
        }
    }
}
