using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public class RocheAstmOrderMessage : RocheAstmBaseMessage
    {
        private const string BODY_FORMAT = "{0}\r{1}";

        private RocheAstmPatientData patientData;
        private List<RocheAstmOrderData> orderData;

        public RocheAstmOrderMessage(string sendingAppCode, string receivingAppCode, RocheAstmPatientData patientData, List<RocheAstmOrderData> orderData)
            : base(sendingAppCode, receivingAppCode)
        {
            this.patientData = patientData;
            this.orderData = orderData;
            this.Type = MessageType.ORDER;
        }

        public RocheAstmOrderMessage(string sendingAppCode, string receivingAppCode, RocheAstmPatientData patientData, RocheAstmOrderData orderData)
            : base(sendingAppCode, receivingAppCode)
        {
            this.patientData = patientData;
            this.Type = MessageType.ORDER;
            this.orderData = new List<RocheAstmOrderData>() { orderData };
        }

        protected override string BodyMessage()
        {
            string result = patientData.ToString();
            
            if (this.orderData != null && this.orderData.Count > 0)
            {
                int orderNumber = 1;
                foreach (RocheAstmOrderData d in this.orderData)
                {
                    d.OrderNumber = orderNumber++;
                    result = string.Format(BODY_FORMAT, result, d.ToString()); 
                }
            }
            return result;
        }

        protected override void FromBodyMessage(string[] bodyMessage)
        {
            throw new NotImplementedException();
        }
    }
}
