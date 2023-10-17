using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisPacsResultTDO
    {
        public string AccessionNumber { get; set; } // Số định danh dịch vụ (ID của HIS_SERE_SERV)
        public bool IsCancel { get; set; }//hủy trả kết quả
        public long? BeginTime { get; set; } //Thời gian bắt đầu xử lý. Định dạng yyyyMMddHHmmss. Ví dụ: 15/7/2022 14:30:33 -> 20220715143033
        public long? EndTime { get; set; } //Thời gian kết thúc xử lý. Định dạng yyyyMMddHHmmss. Ví dụ: 15/7/2022 14:30:33 -> 20220715143033
        public string Description { get; set; }
        public string Conclude { get; set; }
        public string Note { get; set; }
        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }
        public string TechnicianLoginname { get; set; } //Mã kỹ thuật viên
        public string TechnicianUsername { get; set; }// Tên kỹ thuật viên
        public string MachineCode { get; set; }// Mã máy xử lý
        public long? NumberOfFilm { get; set; }
    }
}
