using HIS.Desktop.LocalStorage.PubSub.ADO;
using Inventec.Common.Logging;
using Inventec.Common.WSPubSub;
using Inventec.Token.Core;
using Inventec.UC.Login.Base;
using PSS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.PubSub
{
    internal class PubSubProcessor
    {
        private PubSubClient _Client = null;
        private ReceivedMessage _Received = null;
        private TokenData _TokenData = null;
        private string _ChannelName = null;

        private const string PUBSUB__CHANNEL = "HisProChanel";
        private const string PUBSUB = "HisProHub";
        public PubSubProcessor(ReceivedMessage delegateReceived)
        {
            this._Received = delegateReceived;
            this._TokenData = ClientTokenManagerStore.ClientTokenManager.GetTokenData();
            this._ChannelName = PUBSUB__CHANNEL;
        }

        public async Task<bool> Init()
        {
            bool result = false;
            try
            {
                this._Client = new PubSubClient(Config.HisConfigCFG.PUBSUB_INFO.Address, PUBSUB, this._TokenData.TokenCode, this._Received);
                return await this._Client.Start();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        public async Task<bool> SubscribeChannel()
        {
            try
            {
                if (!this.IsConnected)
                {
                    LogSystem.Warn("Chua ket noi den Server pubsub. Khong subscribe duoc");
                    return false;
                }
                return await this._Client.SubscribeChannel(this._ChannelName);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        public async Task<bool> UnsubscribeChannel()
        {
            try
            {
                if (!this.IsConnected)
                {
                    LogSystem.Warn("Chua ket noi den Server pubsub. Khong subscribe duoc");
                    return false;
                }
                return await this._Client.UnsubscribeChannel(this._ChannelName);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }

        public async Task<bool> Publish(PssPubSubSDO obj)
        {
            bool result = false;
            try
            {
                if (!this.IsConnected)
                {
                    LogSystem.Warn("Chua ket noi den Server pubsub. Khong subscribe duoc");
                    return false;
                }

                if (!this._Client.IsSubscribeChannel(this._ChannelName))
                {
                    if (!await this._Client.SubscribeChannel(this._ChannelName))
                    {
                        LogSystem.Warn("SubscribeChannel failed");
                        return false;
                    }
                }
                result = await this._Client.PublishMessage(this._ChannelName, obj);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
        public bool IsSubChanel()
        {
            try
            {
                if (!this.IsConnected)
                {
                    LogSystem.Warn("Chua ket noi den Server pubsub. Khong subscribe duoc");
                    return false;
                }
                return this._Client.IsSubscribeChannel(this._ChannelName);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
        }
        public bool Stop()
        {
            try
            {
                if (!this.IsConnected)
                    return true;
                this._Client.Stop();
                this._Client = null;
                this._Received = null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        public bool IsConnected
        {
            get
            {
                return (this._Client != null && this._Client.IsConnected);
            }
        }

    }
}
