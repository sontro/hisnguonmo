using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    public abstract class RocheAstmBaseMessage
    {
        protected string SendingAppCode;
        protected string ReceivingAppCode;

        private const string headerFormat = "H{0}|||{1}|||||{2}||{3}||{4}";
        private const string messageFormat = "{0}\r{1}\r{2}";
        private const string footer = "L|1|F\r";

        public RocheAstmBaseMessage(string sendingAppCode, string receivingAppCode)
        {
            this.SendingAppCode = sendingAppCode;
            this.ReceivingAppCode = receivingAppCode;
        }

        protected abstract string BodyMessage();
        protected abstract void FromBodyMessage(string[] bodyMessage);

        public string ToMessage()
        {
            long generatedTime = Inventec.Common.DateTime.Get.Now().Value;
            string header = string.Format(headerFormat,
                RocheAstmConstants.DELIMITER_DEF,
                this.SendingAppCode,
                this.ReceivingAppCode,
                RocheAstmConstants.PROCESSING_ID,
                generatedTime);
            return string.Format(messageFormat, header, this.BodyMessage(), footer);
        }

        public void FromMessage(string message)
        {
            long generatedTime = Inventec.Common.DateTime.Get.Now().Value;
            string sampleHeader = string.Format(headerFormat,
                RocheAstmConstants.DELIMITER_DEF,
                this.SendingAppCode,
                this.ReceivingAppCode,
                RocheAstmConstants.PROCESSING_ID,
                generatedTime);
            string[] messageContent = message.Split(new [] {"\r"}, StringSplitOptions.None);
            if (messageContent == null || messageContent.Length < 2)
            {
                throw new ArgumentException("Header khong hop le. Message:" + message);
            }

            string header = messageContent[0];
            string footer = messageContent[messageContent.Length - 1];

            //int indexHeaderWithoutDate = header.EndsWith("|") ? header.Length - 15 : header.Length - 14;

            //if (!header.Substring(0, indexHeaderWithoutDate).Equals(sampleHeader.Substring(0, sampleHeader.Length - 14)))
            //{
            //    throw new ArgumentException("Header khong hop le. Message:" + message);
            //}
            string[] bodyMessage = messageContent.Skip(1).Take(messageContent.Length - 1).ToArray();
            this.FromBodyMessage(bodyMessage);
        }
    }
}
