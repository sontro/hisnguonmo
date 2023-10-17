using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class PrescriptionTDO
    {
        public string PatientCode { get; set; } 
        public string TreatmentCode { get; set; }
        public string ServiceReqCode { get; set; } // mã y lệnh
        public string IntructionTime { get; set; }
        public string CreateTime { get; set; }
        public string PatientName { get; set; }
        public string PatientDob { get; set; }
        public string GenderName { get; set; }
        public string RequestRoomCode { get; set; } //Mã phòng khám
        public string RequestRoomName { get; set; } //Tên phòng khám
        public string PatientTypeName { get; set; }
        public string PatientAddress { get; set; }
        public string PatientPhone { get; set; }
        public string RelativePhone { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
        public string RequestLoginname { get; set; } //Tài khoản chỉ định
        public string RequestUsername { get; set; } //Tên người chỉ định
        public string RequestDisploma { get; set; }
        public List<PrescriptionDetailTDO> PrescriptionDetail { get; set; }
       
    }

    /// <summary>
    /// Chi tiết thuốc/vật tư theo đơn
    /// </summary>
    public class PrescriptionDetailTDO
    {
        public string ServiceCode { get; set; } //Mã thuốc/vật tư
        public string ServiceName { get; set; } //Tên thuốc/vật tư
        public string ActiveIngredient { get; set; } //Hoạt chất
        public string ActiveIngrBhytName { get; set; } //Hoạt chất bảo hiểm y tế
        public string HeinServiceBhytName { get; set; } //Tên thuốc/vật tư theo quy định của bảo hiểm y tế
        public string Tutorial { get; set; } //Hướng dẫn sử dụng
        public decimal Amount { get; set; } //Số lượng
        public string ServiceUnitName { get; set; } // Đơn vị tính
        public long? UseDays { get; set; } // Số ngày sử dụng
        public long numOrder { get; set; }
    }
}
