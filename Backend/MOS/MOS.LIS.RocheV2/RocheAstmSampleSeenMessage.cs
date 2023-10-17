using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public class RocheAstmSampleSeenMessage : RocheAstmBaseMessage
    {
        private const string SAMPLE_SEEN_INDICATOR = "^SAMPLEEVENT^SEEN";

        public RocheAstmOrderData OrderData { get; set; }
        
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
            
            if (!string.IsNullOrWhiteSpace(bodyMessage[2]) && bodyMessage[2].Contains(SAMPLE_SEEN_INDICATOR))
            {
                this.Type = MessageType.SAMPLE_SEEN;
                this.OrderData = RocheAstmOrderData.FromString(bodyMessage[1]);
            }
            else
            {
                throw new ArgumentException("Ko phai SAMPLEEVENT SEEN message");
            }
        }
    }
}
