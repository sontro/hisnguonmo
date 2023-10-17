using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.LocalStorage.PubSub.ADO
{
    public class PubSubADO
    {
        public ActionType actionType { get; set; }
        public string Message { get; set; }
        public DateTime ExecuteTime { get; set; }
        public string Uuid { get; set; }
    }
    public enum ActionType
    {
        RESET = 1,
        REFRESH_CACHE = 2
    }
}
