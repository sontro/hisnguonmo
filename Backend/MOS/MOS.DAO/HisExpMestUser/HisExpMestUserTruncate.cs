using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestUser
{
    partial class HisExpMestUserTruncate : EntityBase
    {
        public HisExpMestUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_USER>();
        }

        private BridgeDAO<HIS_EXP_MEST_USER> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
