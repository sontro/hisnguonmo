using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    /// <summary>
    /// Luu cac thong tin canh bao tuong ung voi benh nhan
    /// </summary>
    public class HisPatientWarningSDO
    {
        /// <summary>
        /// Cac đơn thuốc đã xuất cho BN mà bệnh nhân chưa dùng hết
        /// </summary>
        public List<HisPreviousPrescriptionSDO> PreviousPrescriptions { get; set; }
        /// <summary>
        /// Các hồ sơ đang nợ viện phí trước đây
        /// </summary>
        public List<string> PreviousDebtTreatments { get; set; }
        /// <summary>
        /// Các hồ sơ điều trị kết thúc trong ngày
        /// </summary>
        public List<string> TodayFinishTreatments { get; set; }
        public V_HIS_TREATMENT_FEE_4 LastTreatmentFee { get; set; }
    }
}
