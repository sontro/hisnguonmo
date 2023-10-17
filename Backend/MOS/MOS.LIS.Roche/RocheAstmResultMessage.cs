using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.Roche
{
    public class RocheAstmResultMessage : RocheAstmBaseMessage
    {
        public RocheAstmPatientData PatientData { get; set; }
        public RocheAstmOrderData OrderData { get; set; }
        public List<RocheAstmTestIndexData> TestIndexResults { get; set; }

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
            this.PatientData = this.ParsePatient(bodyMessage[0]);

            //Thong tin order
            this.OrderData = this.ParseOrder(bodyMessage[1]);

            //Thong tin chi so xet nghiem
            string[] indexs = bodyMessage.Skip(2).Take(bodyMessage.Length - 2).ToArray();
            this.TestIndexResults = this.ParseTestIndexes(indexs);

        }

        private RocheAstmPatientData ParsePatient(string str)
        {
            RocheAstmPatientData patient = null;
            string patientStr = str.StartsWith(RocheAstmConstants.IDENTIFIER__PATIENT_RECORD) ? str : null;
            if (!string.IsNullOrWhiteSpace(patientStr))
            {
                patient = new RocheAstmPatientData();
                string[] patientContent = patientStr.Split('|');
                if (patientContent != null && patientContent.Length >= 9)
                {
                    patient.PatientId = patientContent[2];

                    //name
                    string name = patientContent[5];
                    string[] nameContent = name.Split('^');
                    if (nameContent != null && nameContent.Length >= 2)
                    {
                        patient.FirstName = nameContent[0];
                        patient.LastName = nameContent[1];
                    }

                    //nam sinh
                    patient.DateOfBirth = DateTime.ParseExact(patientContent[7], "yyyyMMdd", null);

                    //gioi tinh
                    patient.Gender = GenderUtil.ToGender(patientContent[8]);
                }
            }
            return patient;
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
                    order.OrderCode = orderContent[2];
                }
            }
            return order;
        }

        private RocheAstmTestIndexData ParseTestIndex(string str)
        {
            RocheAstmTestIndexData index = null;
            string indexStr = str.StartsWith(RocheAstmConstants.IDENTIFIER__RESULT_RECORD) ? str : null;
            if (!string.IsNullOrWhiteSpace(indexStr))
            {
                index = new RocheAstmTestIndexData();
                string[] indexContent = indexStr.Split('|');
                if (indexContent != null && indexContent.Length >= 5)
                {
                    index.TestIndexCode = indexContent[2].Replace("^", "");
                    index.Value = indexContent[3];
                    index.UnitSymbol = indexContent[4];
                    if (indexContent.Length >= 14)
                    {
                        index.MachineCode = indexContent[13];
                    }
                }
            }
            return index;
        }

        private List<RocheAstmTestIndexData> ParseTestIndexes(string[] strs)
        {
            List<RocheAstmTestIndexData> result = null;
            if (strs != null && strs.Length > 0)
            {
                result = new List<RocheAstmTestIndexData>();
                foreach (string s in strs)
                {
                    RocheAstmTestIndexData t = this.ParseTestIndex(s);
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
