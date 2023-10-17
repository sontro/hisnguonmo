using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterReqNumOrder
{
    public class RegisterReqNumOderADO
    {
        public string registerGate { get; set; }
        public decimal maxLine { get; set; }
        public decimal reloadTime { get; set; }
        public decimal? flickerTime { get; set; }
        public string note { get; set; }
        public decimal CellSize { get; set; }
        public decimal HeaderSize { get; set; }
        public decimal? footerSize { get; set; }
        public bool isAutoOpenWaitingScreen { get; set; }
        public string Display_Screen { get; set; }
        public string backgroundColorTitle { get; set; }
        public string backgroundColorSTT { get; set; }
        public string backgroundColorStall { get; set; }
        public string urlVoice { get; set; }
        public string configNotify { get; set; }
    }
}
