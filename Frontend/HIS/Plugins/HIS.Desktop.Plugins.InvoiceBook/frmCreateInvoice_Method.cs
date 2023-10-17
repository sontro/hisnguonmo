using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.InvoiceBook
{
    public partial class frmCreateInvoice
    {
        private List<HIS_INVOICE_DETAIL_NEW> _listInvoiceDetailNews = new List<HIS_INVOICE_DETAIL_NEW>();

        private void CreateNewItemInvoiceDetail()
        {
            var addTime = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
            var newInvoiceDetail = new HIS_INVOICE_DETAIL_NEW
            {
                ADD_TIME = addTime
            };
            _listInvoiceDetailNews.Add(newInvoiceDetail);
            gctCreateInvoice.DataSource = null;
            LoadDataSourceInvoiceDetail();
        }

        private void DeleteNewItemInvoiceDetail(HIS_INVOICE_DETAIL_NEW invoiceDetailAddIndex)
        {
            _listInvoiceDetailNews.RemoveAll(s => s == invoiceDetailAddIndex);
            LoadDataSourceInvoiceDetail();
        }

        private void LoadDataSourceInvoiceDetail()
        {
            gctCreateInvoice.BeginUpdate();
            gctCreateInvoice.DataSource = _listInvoiceDetailNews.OrderByDescending(s => s.ADD_TIME).ToList();
            gctCreateInvoice.EndUpdate();
        }
    }

    internal class HIS_INVOICE_DETAIL_NEW : HIS_INVOICE_DETAIL
    {
        public long ADD_TIME { get; set; }
    }
}
