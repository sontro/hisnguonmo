using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample.Base
{
    public class ReceiveBase
    {
        public CommonParam param;
        public string message;
        public ConnectStore connectStore;
        public Inventec.Common.Rs232.Connector connectCom;

        public ReceiveBase(CommonParam param, string message, Inventec.Common.Rs232.Connector connectCom, ConnectStore connectStore)
        {
            this.param = param;
            this.message = message;
            this.connectStore = connectStore;
            this.connectCom = connectCom;
        }

        public ReceiveBase(CommonParam param, string message, ConnectStore connectStore)
        {
            this.param = param;
            this.message = message;
            this.connectStore = connectStore;
        }

        internal void Send(string message)
        {
            try
            {
                if (connectCom != null && connectCom.IsOpen && !string.IsNullOrEmpty(message))
                {
                    string sendMessage = sendMessage = new StringBuilder().Append(ConnectConstant.HEADER).Append(message).Append(ConnectConstant.FOOTER).ToString();
                    if (!string.IsNullOrEmpty(sendMessage))
                    {
                        connectCom.Send(sendMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
