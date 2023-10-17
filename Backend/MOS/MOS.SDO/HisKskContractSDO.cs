using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisKskContractSDO
    {
        public long KskContractId { get; set; }
        public long RequestRoomId { get; set; }
        public List<HisKskPatientSDO> KskPatients { get; set; }
        public string Loginname { get; set; }
        public string Username { get; set; }
    }
}
