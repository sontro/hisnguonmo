using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class SereServTranSDO
    {
        public decimal Price { get; set; }
        public long SereServId { get; set; }
    }

    public class PharmacyCashierSDO
    {
        //Thong tin ke don
        public long WorkingRoomId { get; set; }
        public string ClientSessionKey { get; set; }
        public long PatientTypeId { get; set; }
        public long? PrescriptionId { get; set; } //id don thuoc (service_req) dung de tao ra phieu xuat ban
        public string PatientName { get; set; } //Ten benh nhan (nguoi mua)
        public string PatientAddress { get; set; }
        public long? PatientGenderId { get; set; }
        public long? PatientDob { get; set; }
        public bool IsHasNotDayDob { get; set; }
        public string PrescriptionReqLoginname { get; set; }
        public string PrescriptionReqUsername { get; set; }
        public long PayFormId { get; set; }

        public decimal ServicePayamount { get; set; }

        public List<ExpMedicineTypeSDO> Medicines { get; set; }
        public List<ExpMaterialTypeSDO> Materials { get; set; }
        public List<long> MaterialBeanIds { get; set; } //danh sach bean_id da chon
        public List<long> MedicineBeanIds { get; set; } //danh sach bean_id da chon

        //Thong tin dich vu
        public List<SereServTranSDO> InvoiceSereServs { get; set; }
        public List<SereServTranSDO> RecieptSereServs { get; set; }


        //Thong tin dich vu thanh toan bo sung
        public List<AssignServiceExtSDO> InvoiceAssignServices { get; set; }
        public List<AssignServiceExtSDO> RecieptAssignServices { get; set; }

        /// <summary>
        /// Phong thu ngan
        /// </summary>
        public long CashierRoomId { get; set; }
        /// <summary>
        /// So bien lai (hoa don thuong: bill_type_id = 1)
        /// </summary>
        public long RecieptAccountBookId { get; set; }
        /// <summary>
        /// So chung tu cua bien lai (trong truong hop so ko tu sinh so)
        /// </summary>
        public long? RecieptNumOrder { get; set; }
        /// <summary>
        /// So hoa don (hoa don dich vu: bill_type_id = 2)
        /// </summary>
        public long InvoiceAccountBookId { get; set; }
        /// <summary>
        /// So chung tu cua hoa don (trong truong hop so ko tu sinh so)
        /// </summary>
        public long? InvoiceNumOrder { get; set; }

    }

    public class AssignServiceExtSDO
    {
        public long ServiceId { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal VatRatio { get; set; }
        public long IntructionTime { get; set; }
        public long PatientTypeId { get; set; }
    }
}
