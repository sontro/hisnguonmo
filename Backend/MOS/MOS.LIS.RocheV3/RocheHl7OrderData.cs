using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    public enum Hl7OrderType
    {
        ADD,
        ADD_TEST,
        CANCEL_TEST,
        DELETE_SAMPLE,
    }

    public class RocheHl7OrderData
    {
        /// <summary>
        /// 0: Tao moi, ...: NW 
        /// 1, 2: barcode: 9388491
        /// 4: Order date: 20210130000619
        /// 5: Request loginname: ntht
        /// 6: Request user name: bs.nguyễn thị hoài thương
        /// 7: Department code: K01
        /// 8: Department name: Tim mạch 1
        /// 9: Co so 1/2
        /// 10: Ten chi nhanh: bệnh viện tim hà nội CS1
        /// 11: STT kham: 18
        /// 12: DV/BHYT
        /// 13: Dịch vụ/BHYT
        /// 14: S/R (cấp cứu/thường)
        /// 15: Cấp cứu/Thường quy
        /// 16: test index 
        /// 17: Icd code: H20
        /// 18: Icd name: Viêm mống thể mi
        /// </summary>
        private const string FORMAT = "ORC|{0}|{1}|{2}^||{3}||||{4}|||{5}^{6}|{7}^{8}|CS{9}^{10}|^|{11}|{12}^{13}" + "\r\n"
            + "NTE|1||" + "\r\n"
            + "TQ1|1||||||||{14}^{15}" + "\r\n"
            + "{16}"
            + "DG1|1||^{17} - {18}";

        /// <summary>
        /// 0: STT
        /// 1: Test code
        /// 2: Test name
        /// </summary>
        private const string TEST_INDEX_FORMAT = "OBR|{0}|||{1}^{2}|||||||||||||||||||||" + "\r\n"
            + "NTE|1||" + "\r\n";

        private const string IDENTIFIER__ORDER_RECORD = "ORC";

        public Hl7OrderType Type { get; set; }
        public string OrderCode { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string BranchId { get; set; }
        public string RequestLoginName { get; set; }
        public string RequestUserName { get; set; }
        public string BranchName { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public long? NumOrder { get; set; }
        public bool IsBhyt { get; set; }
        public bool IsEmergency { get; set; }
        public List<RocheHl7TestIndexData> TestIndexs { get; set; }
        public DateTime OrderDate { get; set; }

        public override string ToString()
        {
            //Dinh nghia loai goi tin (Them moi, hay xoa test, ...)
            string orc1 = null;
            string orc5 = null;

            this.Template(ref orc1, ref orc5);

            return string.Format(FORMAT,
                orc1,
                RocheHl7Util.NVL(this.OrderCode),
                RocheHl7Util.NVL(this.OrderCode),
                orc5,
                this.ToDateString(this.OrderDate),
                RocheHl7Util.NVL(this.RequestLoginName),
                RocheHl7Util.NVL(this.RequestUserName),
                RocheHl7Util.NVL(this.DepartmentCode),
                RocheHl7Util.NVL(this.DepartmentName),
                RocheHl7Util.NVL(this.BranchId),
                RocheHl7Util.NVL(this.BranchName),
                this.NumOrder,
                this.IsBhyt ? "BHYT" : "DV",
                this.IsBhyt ? "BHYT" : "Dịch vụ",
                this.IsEmergency ? "S" : "R",
                this.IsEmergency ? "Cấp cứu" : "Thường quy",
                this.FormatTestIndexCodes(),
                RocheHl7Util.NVL(this.IcdCode),
                RocheHl7Util.NVL(this.IcdName)
                );
        }

        public static RocheHl7OrderData FromString(string str)
        {
            RocheHl7OrderData order = null;
            return order;
        }

        private void Template(ref string orc1, ref string orc5)
        {
            if (this.Type == Hl7OrderType.ADD)
            {
                orc1 = "NW";
                orc5 = "";
            }
            else if (this.Type == Hl7OrderType.ADD_TEST)
            {
                orc1 = "XO";
                orc5 = "SC";
            }
            else if (this.Type == Hl7OrderType.CANCEL_TEST)
            {
                orc1 = "XO";
                orc5 = "CA";
            }
            else if (this.Type == Hl7OrderType.DELETE_SAMPLE)
            {
                orc1 = "CA";
                orc5 = "";
            }
        }

        private string FormatTestIndexCodes()
        {
            if (this.TestIndexs != null && this.TestIndexs.Count > 0)
            {
                string result = "";
                int i = 0;
                foreach (RocheHl7TestIndexData testIndex in this.TestIndexs)
                {
                    i++;
                    result += string.Format(TEST_INDEX_FORMAT, i, testIndex.TestIndexCode, testIndex.TestIndexName);
                }
                return result;
            }
            return "";
        }

        private string ToDateString(DateTime date)
        {
            if (date != null)
            {
                return date.ToString("yyyyMMddHHmmss");
            }
            return "";
        }
    }
}
