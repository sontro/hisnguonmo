using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    /// <summary>
    /// Ban tin mau
    /// MSH|^~\&|Lab IT Tool|V1|LIS|INFINITY|20211203113606508||SSU^U03^SSU_U03|20211203113606508|||||AL|ER
    /// EQU|RECEPTION|20211203113606
    /// SAC||PON|101671912PH|101671912|||20211203113606|2021-12-03 11:17:00.0|||||||ROCHE^ROCHE|
    /// </summary>
    public class RocheHl7SampleSeenMessage : RocheHl7BaseMessage
    {
        private const string COLLECTION_INDICATOR = "COLLECTION";
        private const string RECEPTION_INDICATOR = "RECEPTION";
        private const string SAC_INDICATOR = "SAC";

        public string OrderCode { get; set; }
        
        public RocheHl7SampleSeenMessage(string message)
        {
            this.FromMessage(message);
        }

        public override string ToMessage()
        {
            throw new NotImplementedException();
        }

        public override void FromMessage(string message)
        {
            string[] messageContent = message.Split(new[] { "\r" }, StringSplitOptions.None);

            if (messageContent == null || messageContent.Length < 3)
            {
                throw new ArgumentException("messageContent khong hop le. messageContent null hoac it hon 3 phan tu");
            }

            if (!string.IsNullOrWhiteSpace(messageContent[1]) && (messageContent[1].Contains(COLLECTION_INDICATOR) || messageContent[1].Contains(RECEPTION_INDICATOR)))
            {
                this.Type = Hl7MessageType.SAMPLE_SEEN;

                //Lay thong tin SAC
                if (!string.IsNullOrWhiteSpace(messageContent[2]))
                {
                    //SAC-4: OrderCode
                    this.OrderCode = RocheHl7Util.GetElement(SAC_INDICATOR, messageContent[2], 4);
                    if (string.IsNullOrWhiteSpace(this.OrderCode))
                    {
                        throw new ArgumentException("dataMessageContent khong hop le. Message:" + message);
                    }
                }
            }
            else
            {
                throw new ArgumentException("Ko phai SAMPLEEVENT SEEN message");
            }
        }
    }
}
