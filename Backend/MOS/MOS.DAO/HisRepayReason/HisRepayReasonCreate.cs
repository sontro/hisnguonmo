using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisRepayReason
{
    partial class HisRepayReasonCreate : EntityBase
    {
        public HisRepayReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPAY_REASON>();
        }

        private BridgeDAO<HIS_REPAY_REASON> bridgeDAO;

        public bool Create(HIS_REPAY_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REPAY_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
