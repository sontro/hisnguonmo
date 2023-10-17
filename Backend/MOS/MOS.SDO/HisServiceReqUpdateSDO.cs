using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqUpdateSDO
    {
        //Phong xu ly moi
        public long? ExecuteRoomId { get; set; }
        //Phong xu ly moi
        public long ServiceReqId { get; set; }
        //Danh sach sere_serv_id can xoa
        public List<long> DeleteSereServIds { get; set; }
        //Danh sach dich vu can them moi
        public List<ServiceReqDetailSDO> InsertServices { get; set; }
        //Danh sach dich vu can sua thong tin
        public List<ServiceReqDetailSDO> UpdateServices { get; set; }

        public long InstructionTime { get; set; }
        
    }
}
