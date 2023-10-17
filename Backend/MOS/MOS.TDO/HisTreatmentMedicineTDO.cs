using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisTreatmentMedicineTDO
    {
        /// <summary>
        /// Mã điều trị
        /// </summary>
        public string TreatmentCode { get; set; }

        /// <summary>
        /// Mã loại thuốc
        /// </summary>
        public string MedicineTypeCode { get; set; }

        /// <summary>
        /// Tên loại thuốc
        /// </summary>
        public string MedicineTypeName { get; set; }

        /// <summary>
        /// Id lô thuốc
        /// </summary>
        public long? MedicineId { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Số lô
        /// </summary>
        public string PackageNumber { get; set; }

        /// <summary>
        /// Mã đơn vị tính
        /// </summary>
        public string ServiceUnitCode { get; set; }

        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string ServiceUnitName { get; set; }

        /// <summary>
        /// Tài khoản người kê
        /// </summary>
        public string RequestLoginname { get; set; }

        /// <summary>
        /// Họ tên người kê
        /// </summary>
        public string RequestUsername { get; set; }

        /// <summary>
        /// Han sử dụng định dạng yyyyMMdd000000
        /// </summary>
        public long? ExpireDate { get; set; }

        /// <summary>
        /// Mã đường dùng
        /// </summary>
        public string MedicineUseFormCode { get; set; }

        /// <summary>
        /// Tên đường dùng
        /// </summary>
        public string MedicineUseFormName { get; set; }

        /// <summary>
        /// Hàm lượng - nồng độ
        /// </summary>
        public string Concentra { get; set; }

        /// <summary>
        /// Tốc độ truyền
        /// </summary>
        public decimal? Speed { get; set; }

        /// <summary>
        /// Ngoài kho hay trong kho
        /// True: Ngoài kho
        /// </summary>
        public bool IsOutStock { get; set; }

        /// <summary>
        /// Đã thực xuất hay chưa
        /// True: Đã thực xuất
        /// </summary>
        public bool IsExport { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? ExpPrice { get; set; }

        /// <summary>
        /// Vat
        /// </summary>
        public decimal? ExpVatRatio { get; set; }

        /// <summary>
        /// Thời gian kê
        /// yyyyMMddHHmmss
        /// </summary>
        public long? InstructionTime { get; set; }


        /// <summary>
        /// Ngay kê
        /// yyyyMMdd000000
        /// </summary>
        public long? InstructionDate { get; set; }

        /// <summary>
        /// Id chi dinh dich vu cha
        /// </summary>
        public long? SereServParentId { get; set; }

        /// <summary>
        /// Id dich vu
        /// </summary>
        public long? ServiceId { get; set; }

        /// <summary>
        /// Ten dich vu
        /// </summary>
        public string ServiceName { get; set; }
        
        /// <summary>
        /// Ghi chuc
        /// </summary>
        public string Tutorial { get; set; }

        /// <summary>
        /// Trang thai
        /// </summary>
        public long EXP_MEST_STT_ID { get; set; }
    }
}
