using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDebateReason
{
    partial class HisDebateReasonCreate : EntityBase
    {
        public HisDebateReasonCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_REASON>();
        }

        private BridgeDAO<HIS_DEBATE_REASON> bridgeDAO;

        public bool Create(HIS_DEBATE_REASON data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DEBATE_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
