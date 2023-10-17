using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    /// <summary>
    /// Goi tin mau
    /// MSH|^~\&|cobas infinity 3.3.5|Roche Diagnostics|Receiver Application|Receiver Facility|20211205151946||ORU^M01^ORU_M01|141CBADA-1BC6-4D74-8FF4-54E831569A80|P|2.5|||AL|ER
    /// PID|1||0000414887||^TEST XÉT NGHIỆM |TEST XET NGHIEM |20011001|F|||32, Phường Hàng Buồm, Quận Hoàn Kiếm, Hà Nội, 
    /// ORC|SC|101673260|101673260|||||||||huynh^VSS.Nguyễn Hữu Huy|^^^CS1^^^^^CƠ SỞ 1||||NOMAP^Dịch vụ
    /// OBX|1|1016732600|1000020^Vi khuẩn kháng thuốc định lượng (MIC)|M_WHOLEBLOOD^Máu toàn phần
    /// OBI|1||BAFC^Black and flat colonies|CANALB^CANALB||6|5BPF^5BPF||5
    /// OBAG|1||STA1^STA_D
    /// OBA|1|||PenG^Penicilin G|R
    /// OBA|2|||1100210^Cefoxitin|R
    /// OBA|3|||1100320^Gentamicin|R
    /// OBA|4|||1100490^Clindamycin|R
    /// OBA|5|||1100510^Vancomycin|R
    /// OBA|6|||Ciprof^Ciprofloxacin|R
    /// OBA|7|||1100440^Moxifloxacin|R
    /// OBA|8|||1100450^Co-trimoxazole (Trime+Sulfamethoxazole)|R
    /// OBA|9|||1100550^Linezolid|R
    /// OBA|10|||1100670^Nitrofurantoin|R
    /// OBI|2||CSATC^Colorless, small and transparent colonies|aspnig^aspnig||6|U>5^U>5||5
    /// OBI|3||WCC^White colour colonies|S_EPID^S_EPID||6|POS^POS||0
    /// </summary>
    public class RocheHl7AntibioticResultMessage : RocheHl7BaseMessage
    {
        private const string ANTIBIOTIC_RESULT_INDICATOR = "ORU^M01";
        private const string ORC_INDICATOR = "ORC|";
        private const string OBR_INDICATOR = "OBR|";
        private const string OBX_INDICATOR = "OBX|";
        private const string OBA_INDICATOR = "OBA|";
        private const string OBI_INDICATOR = "OBI|";
        private const string OBAG_INDICATOR = "OBAG|";


        public string OrderCode { get; set; }
        public List<RocheHl7MicroBioticResultRecordData> Results { get; set; }

        public RocheHl7AntibioticResultMessage(string message)
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

            if (!string.IsNullOrWhiteSpace(messageContent[0]) && messageContent[0].Contains(ANTIBIOTIC_RESULT_INDICATOR))
            {
                this.Type = Hl7MessageType.ANTIBIOTIC_RESULT;

                string testIndexCode = null;

                RocheHl7MicroBioticResultRecordData temp = null;

                for (int t = 1; t < messageContent.Length; t++)
                {
                    //Lay thong tin ORC
                    //OrderCode = ORC-2
                    if (string.IsNullOrWhiteSpace(this.OrderCode) && RocheHl7Util.IsElement(ORC_INDICATOR, messageContent[t]))
                    {
                        this.OrderCode = RocheHl7Util.GetElement(ORC_INDICATOR, messageContent[t], 2);
                        this.Results = new List<RocheHl7MicroBioticResultRecordData>();
                    }

                    //Lay thong tin OBX
                    if (RocheHl7Util.IsElement(OBX_INDICATOR, messageContent[t]))
                    {
                        string[] obx = RocheHl7Util.GetElements(OBX_INDICATOR, messageContent[t]);
                        //OBX-3^1: TestIndexCode
                        testIndexCode = RocheHl7Util.GetChildElement(obx, 3, 1);
                    }

                    //Lay thong tin OBI
                    if (RocheHl7Util.IsElement(OBI_INDICATOR, messageContent[t]))
                    {
                        RocheHl7MicroBioticResultRecordData bacteria = new RocheHl7MicroBioticResultRecordData();

                        string[] obi = RocheHl7Util.GetElements(OBI_INDICATOR, messageContent[t]);
                        if (obi != null && !string.IsNullOrWhiteSpace(testIndexCode) && this.Results != null)
                        {
                            //OBI-3^2: bacteria note
                            bacteria.BacteriaNote = RocheHl7Util.GetChildElement(obi, 3, 2);
                            //OBI-4^1: bacteria code
                            bacteria.BacteriaCode = RocheHl7Util.GetChildElement(obi, 4, 1);
                            //OBI-4^2: bacteria name
                            bacteria.BacteriaName = RocheHl7Util.GetChildElement(obi, 4, 2);
                            //OBI-7^1: bacteria amount
                            bacteria.BacteriaAmount = RocheHl7Util.GetChildElement(obi, 7, 1);
                            bacteria.TestIndexCode = testIndexCode;
                            
                            temp = bacteria;
                            this.Results.Add(bacteria);
                        }
                    }

                    //Lay thong tin OBA
                    if (RocheHl7Util.IsElement(OBA_INDICATOR, messageContent[t]))
                    {
                        string[] oba = RocheHl7Util.GetElements(OBA_INDICATOR, messageContent[t]);

                        if (oba != null && temp != null)
                        {
                            RocheHl7AntibioticData antibiotic = new RocheHl7AntibioticData();

                            //OBA-4^1: antibiotic code
                            antibiotic.AntibioticCode = RocheHl7Util.GetChildElement(oba, 4, 1);
                            //OBA-4^2: antibiotic name
                            antibiotic.AntibioticName = RocheHl7Util.GetChildElement(oba, 4, 2);
                            //OBA-5^1: SRI
                            antibiotic.SRI = RocheHl7Util.GetChildElement(oba, 5, 1);
                            //OBA-5^2: antibiotic result
                            antibiotic.Result = RocheHl7Util.GetChildElement(oba, 5, 2);

                            if (temp.Antibiotics == null)
                            {
                                temp.Antibiotics = new List<RocheHl7AntibioticData>();
                            }
                            temp.Antibiotics.Add(antibiotic);
                        }
                    }
                    
                }

                if (string.IsNullOrWhiteSpace(this.OrderCode))
                {
                    throw new ArgumentException("dataMessageContent khong hop le. Ko co order-code (ORC-2). Message:" + message);
                }
            }
            else
            {
                throw new ArgumentException("Ko phai ANTIBIOTIC_RESULT message");
            }
        }
    }
}
