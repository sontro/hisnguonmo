using HIS.Desktop.Plugins.TestConnectDeviceSample.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample.ReceiveMessage
{
    public class SendDevice
    {
        internal void Send(Inventec.Common.Rs232.Connector connectCom, string message)
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
