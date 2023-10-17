using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDepositReason
{
    partial class HisDepositReasonCreate : EntityBase
    {
        public HisDepositReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEPOSIT_REASON>();
        }

        private BridgeDAO<HIS_DEPOSIT_REASON> bridgeDAO;

        public bool Create(HIS_DEPOSIT_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEPOSIT_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
