using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    class ValidationRequestADO
    {
        /// <summary>
        /// Guid cho từng máy/người dùng
        /// </summary>
        public string sessionID { get; set; }

        /// <summary>
        /// Thông tin chính của từng đơn thuốc
        /// </summary>
        public DrugPatientInfoADO prescriptionInfo { get; set; }

        /// <summary>
        /// Nếu kiểm tra trong thời gian bác sỉ kê toa, thì isTemporary = 1
        /// Nếu lưu đơn chính thức, thì Temporary = 0
        /// </summary>
        public bool isTemporary { get; set; }
    }
}
