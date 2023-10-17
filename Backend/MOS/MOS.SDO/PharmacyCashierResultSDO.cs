using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class PharmacyCashierResultSDO
    {
        public HIS_TREATMENT Treatment { get; set; }

        /// <summary>
        /// Phieu xuat ban
        /// </summary>
        public HIS_EXP_MEST ExpMest { get; set; }

        public List<HIS_EXP_MEST_MEDICINE> ExpMestMedicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> ExpMestMaterials { get; set; }

        /// <summary>
        /// Bien lai tuong ung voi cac dich vu BHYT/Vien phi va khong phai vacxin
        /// </summary>
        public List<HIS_TRANSACTION> ServiceReciepts { get; set; }

        /// <summary>
        /// Chi tiet dich vu ngoai y lenh tuong ung voi bien lai
        /// </summary>
        public List<HIS_BILL_GOODS> RecieptBillGoods { get; set; }

        /// <summary>
        /// Chi tiet tuong ung voi bien lai
        /// </summary>
        public List<HIS_SERE_SERV_BILL> RecieptSereServBills { get; set; }

        /// <summary>
        /// Hoa don tuong ung voi dich vu khac BHYT/Vien phi hoac la vacxin
        /// </summary>
        public List<HIS_TRANSACTION> ServiceInvoices { get; set; }

        /// <summary>
        /// Chi tiet tuong ung voi hoa don
        /// </summary>
        public List<HIS_SERE_SERV_BILL> InvoiceSereServBills { get; set; }

        /// <summary>
        /// Chi tiet dich vu ngoai y lenh cua hoa don dich vu
        /// </summary>
        public List<HIS_BILL_GOODS> InvoiceBillGoods { get; set; }

    }
}
