using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public class RocheAstmResultMessage : RocheAstmBaseMessage
    {
        private const string IDENTIFIER__RESULT_RECORD = "R";

        public RocheAstmPatientData PatientData { get; set; }
        public RocheAstmOrderData OrderData { get; set; }
        public List<RocheAstmResultRecordData> TestIndexResults { get; set; }

        public RocheAstmResultMessage(string sendingAppCode, string receivingAppCode, string message)
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

            //Thong tin benh nhan
            this.PatientData = RocheAstmPatientData.FromString(bodyMessage[0]);

            //Thong tin order
            this.OrderData = RocheAstmOrderData.FromString(bodyMessage[1]);

            //Thong tin chi so xet nghiem
            string[] indexs = bodyMessage.Skip(2).Take(bodyMessage.Length - 2).ToArray();
            this.TestIndexResults = this.ParseTestIndexes(indexs);

            if (this.TestIndexResults != null && this.TestIndexResults.Count > 0)
            {
                this.Type = MessageType.RESULT;
            }
            else
            {
                throw new ArgumentException("Ko phai goi tin ket qua");
            }
        }

        private List<RocheAstmResultRecordData> ParseTestIndexes(string[] strs)
        {
            List<RocheAstmResultRecordData> result = null;
            if (strs != null && strs.Length > 0)
            {
                result = new List<RocheAstmResultRecordData>();
                foreach (string s in strs)
                {
                    RocheAstmResultRecordData t = RocheAstmResultRecordData.FromString(s);
                    if (t != null)
                    {
                        result.Add(t);
                    }
                }
            }
            return result;
        }
    }
}
