using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNumOrderIssue
{
    partial class HisNumOrderIssueUpdate : EntityBase
    {
        public HisNumOrderIssueUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_ISSUE>();
        }

        private BridgeDAO<HIS_NUM_ORDER_ISSUE> bridgeDAO;

        public bool Update(HIS_NUM_ORDER_ISSUE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_NUM_ORDER_ISSUE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
