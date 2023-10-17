using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    /// <summary>
    /// Goi tin mau
    /// MSH|^~\&|cobas infinity 3.02.06|Roche Diagnostics|Receiver Application|Receiver Facility|20210130114625||ORU^R01^ORU_R01|959363C4-BB3F-4E0B-A195-F2B527B01F42|P|2.5|||AL|ER
    /// PID|1||BB11040178||^BÙI THỊ LAN|^BUI THI LAN|19690701|F|||||^^^^^^^^^^^0919700275 PV1|1|U|2011230081|||||||BHYT|||||||||^^^K01
    /// ORC|SC|388491|388491||CM||||20210130000619|||ntht^Bs.Nguyễn Thị Hoài Thương|^^^CS2^^^^^LABO----2_HEART----2||||BHYT^Bảo hiểm y tế
    /// OBR|1|388491|388491WE|RBC^Số lượng hồng cầu (RBC)|||||||||||||||||||||F
    /// OBX|1|NM|RBC||11.00|^T/L|||^^^^BR2-XN1000-1||F|||20210130114541|20210130114603|ROCHE^ROCHE
    /// OBR|2|388491|388491WE|NRBC^Số lượng tuyệt đối hồng cầu nhân (NRBC)|||||||||||||||||||||F
    /// OBX|2|NM|NRBC||12.00|^T/L|||^^^^BR2-XN1000-1||F|||20210130114541|20210130114603|ROCHE^ROCHE
    /// OBR|3|388491|388491WE|NRBC%^Tỷ lệ phần trăm hồng cầu nhân (NRBC%)|||||||||||||||||||||F
    /// OBX|3|NM|NRBC%||13.00|^%|||^^^^BR2-XN1000-1||F|||20210130114541|20210130114603|ROCHE^ROCHE
    /// OBR|4|388491|388491WE|HGB^Lượng huyết sắc tố (HGB)|||||||||||||||||||||F
    /// OBX|4|NM|HGB||14.00|^g/L|||^^^^BR2-XN1000-1||F|||20210130114540|20210130114603|ROCHE^ROCHE
    /// OBR|5|388491|388491WE|HCT^Dung tích hồng cầu trong máu (HCT)|||||||||||||||||||||F
    /// OBX|5|NM|HCT||15.00|^L/L|||^^^^BR2-XN1000-1||F|||20210130114540|20210130114603|ROCHE^ROCHE
    /// OBR|6|388491|388491WE|MCV^Thể tích trung bình của hồng cầu (MCV)|||||||||||||||||||||F
    /// OBX|6|NM|MCV||16.00|^fL|||^^^^BR2-XN1000-1||F|||20210130114539|20210130114603|ROCHE^ROCHE SPM|1||||||||||||||||010619
    /// </summary>
    public class RocheHl7ResultMessage : RocheHl7BaseMessage
    {
        private const string RESULT_INDICATOR = "ORU^R01";
        private const string ORC_INDICATOR = "ORC|";
        private const string OBR_INDICATOR = "OBR|";
        private const string OBX_INDICATOR = "OBX|";

        public string OrderCode { get; set; }
        public List<RocheHl7ResultRecordData> TestIndexResults { get; set; }

        public RocheHl7ResultMessage(string message)
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

            if (messageContent == null || messageContent.Length < 4)
            {
                throw new ArgumentException("messageContent khong hop le. messageContent null hoac it hon 4 phan tu");
            }

            if (!string.IsNullOrWhiteSpace(messageContent[0]) && messageContent[0].Contains(RESULT_INDICATOR))
            {
                this.Type = Hl7MessageType.RESULT;
                this.TestIndexResults = new List<RocheHl7ResultRecordData>();

                for (int t = 1; t < messageContent.Length; t++)
                {
                    //Lay thong tin ORC
                    //OrderCode = ORC-2
                    if (string.IsNullOrWhiteSpace(this.OrderCode))
                    {
                        this.OrderCode = RocheHl7Util.GetElement(ORC_INDICATOR, messageContent[t], 2);
                    }
                    
                    //Lay thong tin OBX
                    string[] obx = null;

                    //OBX-3: TestIndexCode
                    string testIndexCode = RocheHl7Util.GetElement(OBX_INDICATOR, messageContent[t], 3, ref obx);

                    if (obx != null && !string.IsNullOrWhiteSpace(testIndexCode))
                    {
                        RocheHl7ResultRecordData r = new RocheHl7ResultRecordData();
                        r.TestIndexCode = testIndexCode;
                        
                        //OBX-5: Value
                        r.Value = RocheHl7Util.GetElement(obx, 5);
                        //OBX-6^2: UnitSymbol
                        r.UnitSymbol = RocheHl7Util.GetChildElement(obx, 6, 2);
                        //OBX-9^5: MachineCode
                        r.MachineCode = RocheHl7Util.GetChildElement(obx, 9, 5);

                        this.TestIndexResults.Add(r);
                    }
                }

                if (string.IsNullOrWhiteSpace(this.OrderCode))
                {
                    throw new ArgumentException("dataMessageContent khong hop le. Ko co order-code (ORC-2). Message:" + message);
                }
            }
            else
            {
                throw new ArgumentException("Ko phai RESULT message");
            }
        }
    }
}
