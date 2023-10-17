using Inventec.Common.Logging;
using Microsoft.AspNet.SignalR;
using PSS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.PubSub
{
    public class HisProHub : Hub
    {
        private static List<string> DkkConnectionCheckConnect = new List<string>();
        private static Dictionary<long, object> DicDataPublish = new Dictionary<long, object>();

        public HisProHub()
            : base()
        {

        }

        public bool Subscribe(string channel)
        {
            LogSystem.Debug(String.Format("SDA.PubSub. Connection {0},Subscribe channel {1}", Context.ConnectionId, channel));
            return true;
        }

        public bool Unsubscribe(string channel)
        {
            LogSystem.Debug(String.Format("SDA.PubSub. Connection {0},Unsubscribe channel {1}", Context.ConnectionId, channel));
            return true;
        }

        public override Task OnConnected()
        {
            LogSystem.Debug(String.Format("SDA.PubSub. OnConnected. ConnectionId: '{0}'", Context.ConnectionId));
            if (!AddConnection(Context.ConnectionId))
            {
                LogSystem.Warn("OnConnected.AddConnection Failed");
            };

            SendMessage(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            LogSystem.Debug(String.Format("SDA.PubSub. Client disconnected. ConnectionId: '{0}'", Context.ConnectionId));

            if (!RemoveConnection(Context.ConnectionId))
            {
                LogSystem.Warn("SDA.PubSub. OnDisconnected.RemoveConnection Failed");
            }

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            LogSystem.Debug(String.Format("SDA.PubSub. Client reconect. ConnectionId: '{0}'", Context.ConnectionId));
            if (!AddConnection(Context.ConnectionId))
            {
                LogSystem.Warn("SDA.PubSub. OnReconnected.AddConnection Failed");
            };

            //tạo task riêng để kết nối lại sẽ gửi dữ liệu sau 1s
            Task tsSendMessage = Task.Factory.StartNew((object obj) =>
            {
                System.Threading.Thread.Sleep(1000);

                SendMessage(obj.ToString());
            }, Context.ConnectionId);

            return base.OnReconnected();
        }

        public void ConnectionSlow()
        {
            Inventec.Common.Logging.LogSystem.Debug("SDA.PubSub. ConnectionSlow: " + Context.ConnectionId);
        }

        public bool Publish(string channel, PssPubSubSDO data)
        {
            List<string> connectIds = DkkConnectionCheckConnect.Where(o => o != Context.ConnectionId).ToList();

            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            if (connectIds != null && connectIds.Count > 0)
            {
                Clients.Clients(connectIds).SendMessage(data);
                LogSystem.Debug("SDA.PubSub. SendMessage data. Success");
            }
            else
            {
                LogSystem.Warn("SDA.PubSub. SendMessage error. No ConnectionId");
                return false;
            }

            if (data.CommandCode.Contains("PubSubADO"))
            {
                long day = Inventec.Common.DateTime.Get.StartDay() ?? 0;
                DicDataPublish[day] = data;
            }

            LogSystem.Debug(LogUtil.TraceData("Data", data));
            return true;
        }

        private bool SendMessage(string connectionIdTo)
        {
            long day = Inventec.Common.DateTime.Get.StartDay() ?? 0;

            object data = null;
            if (DicDataPublish.ContainsKey(day))
            {
                data = DicDataPublish[day];
            }

            if (!String.IsNullOrWhiteSpace(connectionIdTo) && data != null)
            {
                Clients.Client(connectionIdTo).SendMessage(data);
                LogSystem.Debug(String.Format("SDA.PubSub. SendMessage to {0}. Success", connectionIdTo));
            }
            else
            {
                //LogSystem.Warn(String.Format("SDA.PubSub. SendMessage error. No ConnectionId or No Data to send"));
                return false;
            }

            LogSystem.Debug(LogUtil.TraceData("Data", data));
            return true;
        }

        private bool AddConnection(string connectionId)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrWhiteSpace(connectionId))
                {
                    if (!DkkConnectionCheckConnect.Exists(o => o == connectionId))
                    {
                        DkkConnectionCheckConnect.Add(connectionId);
                    }

                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private bool RemoveConnection(string connectionId)
        {
            bool result = true;
            try
            {
                if (!String.IsNullOrWhiteSpace(connectionId) && DkkConnectionCheckConnect.Exists(o => o == connectionId))
                {
                    result = DkkConnectionCheckConnect.Remove(connectionId);
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            return result;
        }
    }
}
