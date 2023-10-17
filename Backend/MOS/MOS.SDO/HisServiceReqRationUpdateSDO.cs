using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqRationUpdateSDO
    {
        public long ExecuteRoomId { get; set; } //  Phòng xử lý
        public long ServiceReqId { get; set; } //  ID của  y lệnh cần sửa
        public List<long> DeleteSereServRationIds { get; set; } //  danh sách sere_serv_ration_id cần xóa
        public List<RationServiceSDO> InsertServices { get; set; } // Danh sách dịch vụ mới
        public List<RationServiceSDO> UpdateServices { get; set; } // Danh sách dịch vụ cần sửa thông tin
    }
}
