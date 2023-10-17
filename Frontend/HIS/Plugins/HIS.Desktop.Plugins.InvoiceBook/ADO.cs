using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.InvoiceBook
{
    internal class ADO
    {

    }

    internal class PageTotal
    {
        public static long SoDongTrangDauTienMauPhieuHoaDonChiTiet { get; set; }
        public static long SoDongTrangTiepTheoMauPhieuHoaDonChiTiet { get; set; }
    }

    internal class ResultValidateControl
    {
        public bool Result { get; set; }

        public string Notification { get; set; }
    }

    public class HIS_INVOICE_DETAIL_NEW : HIS_INVOICE_DETAIL
    {
        public long ADD_TIME { get; set; }

        public int ACTION { get; set; }

        public decimal SUM_PRICE_STR { get; set; }
    }

    internal enum MenuPrintType
    {
        PrintInvoice,
        PrintInvoiceOrder
    }
}
