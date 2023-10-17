using Inventec.Common.WSPubSub;
using PSS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MOS.PubSub
{
    public class PubSubProcessor
    {
        private static string Address = "";
        private static long CheckTime = 0;
        private static Timer aTimer = null;
        private static bool IsIniting = false;

        private static PubSubClient _Client = null;

        public static async Task<bool> SendMessage(object obj)
        {
            bool result = false;
            try
            {
                if (!IsConnected)
                {
                    Inventec.Common.Logging.LogSystem.Warn("Chua ket noi den Server pubsub. Khong subscribe duoc");
                    return false;
                }

                PssPubSubSDO sdo = new PssPubSubSDO();
                sdo.JsonData = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
                sdo.CommandCode = obj.GetType().Name;
                result = await _Client.PublishMessage(Constant.PUBSUB__CHANNEL, sdo);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public static void ConnectPubSub(string address, long checkTime)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(address) && checkTime > 0)
                {
                    Address = address;
                    CheckTime = checkTime * 1000;

                    if (aTimer == null || !aTimer.Enabled || aTimer.Interval != CheckTime)
                    {
                        aTimer = new System.Timers.Timer(CheckTime);
                        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                        aTimer.Enabled = true;
                        aTimer.Start();
                    }
                }
                else
                {
                    if (_Client != null)
                    {
                        _Client.Stop();
                    }

                    if (aTimer != null)
                    {
                        aTimer.Stop();
                        aTimer.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (!IsConnected && !IsIniting)
                {
                    Inventec.Common.Logging.LogSystem.Info("MOS.PubSub. New pubsubProcessor");
                    Init();
                    SubscribeChannel();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static void Init()
        {
            try
            {
                IsIniting = true;
                if (_Client != null)
                {
                    _Client.Stop();
                }

                _Client = new PubSubClient(Address, Constant.PUBSUB, null, _Received);
                _Client.Start();
                IsIniting = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static void _Received(PSS.SDO.PssPubSubSDO data)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static bool IsConnected
        {
            get
            {
                return (_Client != null && _Client.IsConnected);
            }
        }

        private static void SubscribeChannel()
        {
            try
            {
                if (!IsConnected)
                {
                    Inventec.Common.Logging.LogSystem.Warn("Chua ket noi den Server pubsub. Khong subscribe duoc");
                    return;
                }

                if (!_Client.IsSubscribeChannel(Constant.PUBSUB__CHANNEL))
                {
                    _Client.SubscribeChannel(Constant.PUBSUB__CHANNEL);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
