using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisCarerCardBorrowSDO
    {
        public long TreatmentId { get; set; }
        public string GivingLoginName { get; set; }
        public string GivingUserName { get; set; }
        public List<HisCarerCardSDOInfo> CarerCardInfos { get; set; }  // Danh sach thong tin the muon
        public long RequestRoomId { get; set; }  // Phong dang lam viec
    }

    public class HisCarerCardSDOInfo
    {
        // Thong tin the muon
        public long CarerCardId { get; set; }
        public long BorrowTime { get; set; }
    }
}
