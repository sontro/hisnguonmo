using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestSaleListSDO
    {
        //Co tao giao dich thanh toan luon hay ko
        public bool CreateBill {get;set;}
        //Diem thu ngan
        public long? CashierRoomId { get; set; }
        //So thu chi
        public long? AccountBookId { get; set; }
        //So chung tu trong truong hop so thu chi ko tu dong sinh so
        public long? TransactionNumOrder { get; set; }
        //Hinh thuc
        public long? PayFormId { get; set; }
        //So tien chuyen khoan trong truong hop hinh thuc vua chuyen khoan vua tien mat
        public decimal? TransferAmount { get; set; }

        //Cac thong tin trong truong hop thuc hien giao dich bang may POS
        public string PosPan { get; set; }
        public string PosCardHoder { get; set; }
        public string PosInvoice { get; set; }
        public string PosResultJson { get; set; }

        //So tien sau khi lam tron
        public decimal? RoundedTotalPrice { get; set; }
        //So tien co so dung lam can cu lam tron
        public decimal? RoundedPriceBase { get; set; }
        
        //Du lieu phieu xuat
        public List<HisExpMestSaleSDO> SaleData { get; set; }
    }
}
