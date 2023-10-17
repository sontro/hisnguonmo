using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public enum OrderType
    {
        ADD,
        ADD_TEST,
        CANCEL_TEST,
        DELETE_SAMPLE,
    }

    enum ActionCode
    {
        ADD = 'A',
        DELETE_EXISTING_TEST = 'C',
        DELETE = 'R',
        REPEAT = 'G'
    }

    enum ReportType
    {
        NEW_ORDER = 'O',
        DELETE_SAMPLE = 'X',
        CHANGE = 'C'
    }

    enum OrderStatus
    {
        ADD = 'A'
    }

    enum Priority
    {
        STAT = 'S',//"short turn-around time"
        ROUTINE = 'R'
    }

    public class RocheAstmOrderData
    {
        private const string FORMAT = "O|{0}|{1}||{2}|{3}|{4}|||||{5}|||||{6}||{7}||{8}|||{9}||{10}|O";
        private const string IDENTIFIER__ORDER_RECORD = "O";
        private const string START_TEST_INDEX_CODES = "^^^";
        private const string TEST_INDEX_CODES_DELIMITER = "\\^^^";

        public OrderType Type { get; set; }
        public string OrderCode { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentCode { get; set; }
        public string BranchCode { get; set; }
        public List<string> TestIndexCodes { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderNumber { get; set; }

        public override string ToString()
        {
            ActionCode? action = null;
            ReportType? reportType = null;
            Priority? priority = null;
            OrderStatus? status = null;

            this.Template(ref action, ref reportType, ref priority, ref status);

            return string.Format(FORMAT,
                this.OrderNumber,
                RocheAstmUtil.NVL(this.OrderCode),
                this.FormatTestIndexCodes(),
                (priority != null ? ((char)priority).ToString() : ""),
                this.ToDateString(this.OrderDate),
                (action != null ? ((char)action).ToString() : ""),
                RocheAstmUtil.NVL(this.DoctorName),
                RocheAstmUtil.NVL(this.DepartmentCode),
                (status != null ? ((char)status).ToString() : ""),
				RocheAstmUtil.NVL(this.BranchCode),
                (reportType != null ? ((char)reportType).ToString() : ""));
        }

        public static RocheAstmOrderData FromString(string str)
        {
            RocheAstmOrderData order = null;
            string patientStr = str.StartsWith(RocheAstmOrderData.IDENTIFIER__ORDER_RECORD) ? str : null;
            if (!string.IsNullOrWhiteSpace(patientStr))
            {
                order = new RocheAstmOrderData();
                string[] orderContent = patientStr.Split('|');
                if (orderContent != null && orderContent.Length >= 3)
                {
                    string orderId = orderContent[2];
                    if (orderId != null && orderId.Contains('^'))
                    {
                        string[] orderIdElements = orderContent[2].Split('^');
                        order.OrderCode = orderIdElements[0];
                    }
                    else
                    {
                        order.OrderCode = orderId;
                    }
                }
            }
            return order;
        }

        private void Template(ref ActionCode? action, ref ReportType? reportType, ref Priority? priority, ref OrderStatus? status)
        {
            if (this.Type == OrderType.ADD)
            {
                action = ActionCode.ADD;
                reportType = ReportType.NEW_ORDER;
                status = null;
                priority = Priority.ROUTINE;
            }
            else if (this.Type == OrderType.ADD_TEST)
            {
                action = ActionCode.ADD;
                reportType = ReportType.NEW_ORDER;
                status = null;
                priority = Priority.ROUTINE;
            }
            else if (this.Type == OrderType.CANCEL_TEST)
            {
                action = ActionCode.DELETE;
                reportType = ReportType.CHANGE;
                status = OrderStatus.ADD;
                priority = Priority.ROUTINE;
            }
            else if (this.Type == OrderType.DELETE_SAMPLE)
            {
                action = ActionCode.DELETE_EXISTING_TEST;
                priority = Priority.ROUTINE;
                reportType = ReportType.DELETE_SAMPLE;
                status = null;
            }
        }

        private string FormatTestIndexCodes()
        {
            if (this.TestIndexCodes != null && this.TestIndexCodes.Count > 0)
            {
                string result = START_TEST_INDEX_CODES;
                foreach (string testIndexCode in this.TestIndexCodes)
                {
                    result = string.Format("{0}{1}{2}", result, testIndexCode, TEST_INDEX_CODES_DELIMITER);
                }
                return result.Substring(0, result.Length - TEST_INDEX_CODES_DELIMITER.Length);
            }
            return null;
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
