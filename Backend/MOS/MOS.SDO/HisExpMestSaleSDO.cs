using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestSaleSDO
    {
        public long? ExpMestId { get; set; }
        public long MediStockId { get; set; }
        //Co tao giao dich thanh toan luon hay ko
        public bool CreateBill { get; set; }
        //So thu chi
        public long? AccountBookId { get; set; }
        //So chung tu trong truong hop so thu chi ko tu dong sinh so
        public long? TransactionNumOrder { get; set; }
        //Diem thu ngan
        public long? CashierRoomId { get; set; }
        public long ReqRoomId { get; set; }
        public string ClientSessionKey { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public long? PatientTypeId { get; set; }
        public long? PrescriptionId { get; set; } //id don thuoc (service_req) dung de tao ra phieu xuat ban
        public string PatientName { get; set; } //Ten benh nhan (nguoi mua)
        public string PatientAddress { get; set; }
        public string PatientTaxCode { get; set; }
        public string PatientWorkPlace { get; set; }
        public string PatientAccountNumber { get; set; }
        public string PrescriptionReqLoginname { get; set; }
        public string PrescriptionReqUsername { get; set; }
        public string IcdCode { get; set; }
        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
        public string IcdName { get; set; }
        public long? PatientGenderId { get; set; }
        public long? PatientDob { get; set; }
        public string PatientPhone { get; set; }
        public long? InstructionTime { get; set; }
        public bool IsHasNotDayDob { get; set; }
        public bool IsNotInWorkingTime { get; set; }
        public long? PresGroup { get; set; }
        public decimal? TotalServiceAttachPrice { get; set; }
        public long? TreatmentId { get; set; }
        public long? PatientId { get; set; }
        //Hinh thuc
        public long? PayFormId { get; set; }
        public decimal? TransferAmount { get; set; }

        public List<ExpMedicineTypeSDO> Medicines { get; set; }
        public List<ExpMaterialTypeSDO> Materials { get; set; }
        public List<long> MaterialBeanIds { get; set; } //danh sach bean_id da chon
        public List<long> MedicineBeanIds { get; set; } //danh sach bean_id da chon
    }
}
