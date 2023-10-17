using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.Global.ADO
{
    public class ServerInfoADO
    {
        public ServerInfoADO() { }
        public string ServerAddress { get; set; }
        public long? LastPingTime { get; set; }
        public string Description { get; set; }
        public IPStatus? IPStatus { get; set; }
    }
}
