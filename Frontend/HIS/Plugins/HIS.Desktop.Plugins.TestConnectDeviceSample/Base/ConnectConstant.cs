using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample.Base
{
    public class ConnectConstant
    {
        public const string HEADER = "$$$";
        public const string FOOTER = "%%%";

        //ban tin gui xuong thiet bi

        internal const string MESSAGE_CONNECT = "CONNECT";
        internal const string MESSAGE_DISCONNECT = "DISCONNECT";
        internal const string MESSAGE_PING = "PING";
        internal const string MESSAGE_PROCESS = "PROCESS";

        //phuc vu cat va noi message
        internal const char MESSAGE_SEPARATOR = ',';

        //response_code
        internal const string RESPONSE_SUCCESS = "0";
        internal const string RESPONSE_CORRECT = "-";
    }
}
