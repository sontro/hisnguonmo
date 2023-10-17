using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class DepositServiceADO
    {
        public long hisTreatmentId { get; set; }
        public V_HIS_TREATMENT_FEE hisTreatment { get; set; }
        public List<MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_5> SereServs { get; set; }
        public HisTransactionDepositSDO depositSdo { get; set; }
        public long? BRANCH_ID { get; set; }
        public long CashierRoomId { get; set; }
        public HIS.Desktop.Common.DelegateReturnSuccess returnSuccess { get; set; }
        public bool? IsDepositAll { get; set; } // tạm ứng tất cả(disable danh sách dịch vụ) #30384

        public DepositServiceADO()
        {

        }
        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="_hisTreatmentId">V_HIS_TREATMENT</param>
        /// <param name="_transactionData">depositSDO (phuc vu chuc nang sua)</param>
        /// <param name="branchId">Id chi nhanh</param>
        /// <param name="CashierRoomId">id phong thu ngan</param>
        public DepositServiceADO(long _hisTreatmentId, HisTransactionDepositSDO _transactionData, long? branchId, long CashierRoomId)
        {
            try
            {
                this.hisTreatmentId = _hisTreatmentId;
                this.depositSdo = _transactionData;
                this.BRANCH_ID = branchId;
                this.CashierRoomId = CashierRoomId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="_hisTreatmentId">V_HIS_TREATMENT</param>
        /// <param name="_transactionData">depositSDO (phuc vu chuc nang sua)</param>
        /// <param name="branchId">Id chi nhanh</param>
        /// <param name="CashierRoomId">id phong thu ngan</param>
        public DepositServiceADO(V_HIS_TREATMENT_FEE _hisTreatment, HisTransactionDepositSDO _transactionData, long? branchId, long CashierRoomId)
        {
            try
            {
                this.hisTreatment = _hisTreatment;
                this.depositSdo = _transactionData;
                this.BRANCH_ID = branchId;
                this.CashierRoomId = CashierRoomId;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
