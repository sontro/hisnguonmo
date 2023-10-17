using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisNumOrderIssue
{
    partial class HisNumOrderIssueCreate : EntityBase
    {
        public HisNumOrderIssueCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_ISSUE>();
        }

        private BridgeDAO<HIS_NUM_ORDER_ISSUE> bridgeDAO;

        public bool Create(HIS_NUM_ORDER_ISSUE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_NUM_ORDER_ISSUE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
