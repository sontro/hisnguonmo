using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    public class RocheAstmSampleSeenMessage : RocheAstmBaseMessage
    {
        private const string SAMPLE_SEEN_INDICATOR = "^SAMPLEEVENT^SEEN";

        public RocheAstmOrderData OrderData { get; set; }
        public bool IsSamepleSeen { get; set; }

        public RocheAstmSampleSeenMessage(string sendingAppCode, string receivingAppCode, string message)
            : base(sendingAppCode, receivingAppCode)
        {
            this.FromMessage(message);
        }

        protected override string BodyMessage()
        {
            throw new NotImplementedException();
        }

        protected override void FromBodyMessage(string[] bodyMessage)
        {
            if (bodyMessage == null || bodyMessage.Length < 3)
            {
                throw new ArgumentException("Thong tin body khong hop le. BodyMessage co null hoac it hon 3 phan tu");
            }
            
            this.IsSamepleSeen = !string.IsNullOrWhiteSpace(bodyMessage[2]) && bodyMessage[2].Contains(SAMPLE_SEEN_INDICATOR);

            if (!this.IsSamepleSeen)
            {
                throw new ArgumentException("Ko phai SAMPLEEVENT SEEN message");
            }

            this.OrderData = this.ParseOrder(bodyMessage[1]);
        }

        private RocheAstmOrderData ParseOrder(string str)
        {
            RocheAstmOrderData order = null;
            string patientStr = str.StartsWith(RocheAstmConstants.IDENTIFIER__ORDER_RECORD) ? str : null;
            if (!string.IsNullOrWhiteSpace(patientStr))
            {
                order = new RocheAstmOrderData();
                string[] orderContent = patientStr.Split('|');
                if (orderContent != null && orderContent.Length >= 3)
                {
                    string orderId = orderContent[2];
                    string[] orderIdElements = orderContent[2].Split('^');
                    order.OrderCode = orderIdElements[0];
                }
            }
            return order;
        }
    }
}
