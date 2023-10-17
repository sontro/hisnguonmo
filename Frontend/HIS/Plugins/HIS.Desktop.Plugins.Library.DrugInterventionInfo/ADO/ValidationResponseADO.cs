using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    class ValidationResponseADO
    {
        /// <summary>
        /// Mã guid của lần kiểm tra, mã này được dùng để truy xuất thông tin chi tiết bằng URL khác (*)
        /// </summary>
        public string prescriptionID { get; set; }

        /// <summary>
        /// Cần hiển thị thông tin trong IssueCategories hay không
        /// </summary>
        public bool needAlert { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// Danh sách vấn đề
        /// </summary>
        public List<IssueCategoryADO> issueCategories { get; set; }

        /// <summary>
        /// Mức độ nghiêm trọng của vấn đề
        /// </summary>
        public DrugEnum.ValidationSeverityLevel severity { get; set; }

        /// <summary>
        /// Các loại vấn đề nghiêm trọng
        /// </summary>
        public DrugEnum.TopIssue topIssue { get; set; }

        /// <summary>
        /// Thông tin lưu ý
        /// </summary>
        public WarningADO warning { get; set; }

        /// <summary>
        /// Cần bác sĩ input thông tin xác nhận hay không?
        /// </summary>
        public bool needDoctorConfirmation { get; set; }

        /// <summary>
        /// Phần tóm tắt mà bác sĩ cần xem. Được get từ IssueCategories theo logic (**)
        /// </summary>
        public string issueText { get; set; }
    }
}
