using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestReason
{
    partial class HisExpMestReasonUpdate : EntityBase
    {
        public HisExpMestReasonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_REASON>();
        }

        private BridgeDAO<HIS_EXP_MEST_REASON> bridgeDAO;

        public bool Update(HIS_EXP_MEST_REASON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST_REASON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
