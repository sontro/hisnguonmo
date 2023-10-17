using Inventec.Common.Logging;
using Inventec.Common.WebApiClient;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Test.LisIntegreateProcessor.LisRoche
{
    class SendFileAddress
    {
        public string Ip { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string SaveFolder { get; set; }
        public string ReadFolder { get; set; }
        public FileHandlerType FileHandlerType { get; set; }
    }

    class RocheSender : BusinessBase
    {
        private bool isHl7Message;

        internal RocheSender(CommonParam param, bool isHl7)
            : base(param)
        {
            this.isHl7Message = isHl7;
        }

        public bool Run(string dataCode, long? executeRoomId, string data)
        {
            bool result = false;
            try
            {
                string roomCode = executeRoomId.HasValue ? HisExecuteRoomCFG.DATA.Where(o => o.ROOM_ID == executeRoomId).Select(o => o.EXECUTE_ROOM_CODE).FirstOrDefault() : null;

                if (LisRocheCFG.CONNECTION_TYPE == RocheConnectionType.FILE)
                {
                    result = this.FileSend(dataCode, roomCode, data, LisRocheCFG.FILE_FORMAT_ORDER_PREFIX);
                }
                else if (LisRocheCFG.CONNECTION_TYPE == RocheConnectionType.TCP_IP)
                {
                    result = this.TcpIpSend(roomCode, data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;

        }

        private bool TcpIpSend(string roomCode, string data)
        {
            string rocheAddress = rocheAddress = LisRocheCFG.TCP_IP_ADDRESSES
                        .Where(o => o.RoomCode == roomCode)
                        .Select(o => o.Url).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(rocheAddress))
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaCauHinhDiaChiKetNoiLis);
                return false;
            }
            else
            {
                ApiConsumer serviceConsumer = new ApiConsumer(rocheAddress, MOS.UTILITY.Constant.APPLICATION_CODE);

                var ro = serviceConsumer.Post<Inventec.Core.ApiResultObject<string>>("/api/v1/transfer", null, data);
                if (ro == null || !ro.Success)
                {
                    LogSystem.Error("Gui y/c xet nghiem sang Roche that bai. Ket qua gui sang RIS: " + LogUtil.TraceData("ro", ro));
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private bool FileSend(string orderCode, string roomCode, string data, string fileNamePrefix)
        {
            List<LisRocheFileAddress> configData = this.isHl7Message ? LisRocheCFG.FILE_HL7_ADDRESSES : LisRocheCFG.FILE_ADDRESSES;

            var Addresses = configData.Where(o=> roomCode == null || o.RoomCode == roomCode).GroupBy(o => new
            {
                o.Ip,
                o.Password,
                o.ReadFolder,
                o.SaveFolder,
                o.User,
                o.FileHandlerType
            }).ToList();

            List<SendFileAddress> toSendAddresses = new List<SendFileAddress>();

            foreach (var itemGroup in Addresses)
            {
                SendFileAddress addr = new SendFileAddress();
                addr.Ip = itemGroup.First().Ip;
                addr.Password =itemGroup.First().Password;
                addr.ReadFolder = itemGroup.First().ReadFolder;
                addr.SaveFolder = itemGroup.First().SaveFolder;
                addr.User = itemGroup.First().User;
                addr.FileHandlerType = itemGroup.First().FileHandlerType;
                toSendAddresses.Add(addr);
            }

            if (toSendAddresses == null || toSendAddresses.Count == 0)
            {
                MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_ChuaCauHinhDiaChiKetNoiLis);
                return false;
            }
            else
            {
                bool result = true;
                foreach (SendFileAddress address in toSendAddresses)
                {
                    string fileName = string.Format("{0}_{1}_{2}.dat", fileNamePrefix, orderCode, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    if (!FileHandler.Write(address.Ip, address.User, address.Password, data, fileName, address.SaveFolder, address.FileHandlerType))
                    {
                        result = false;
                        LogSystem.Error("Luu file tich hop xet nghiem sang he thong LIS cua Roche that bai.");
                    }
                }

                return result;
            }
        }
    }
}
