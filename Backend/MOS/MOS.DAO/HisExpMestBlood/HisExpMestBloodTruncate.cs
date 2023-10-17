using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestBlood
{
    partial class HisExpMestBloodTruncate : EntityBase
    {
        public HisExpMestBloodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_BLOOD>();
        }

        private BridgeDAO<HIS_EXP_MEST_BLOOD> bridgeDAO;

        public bool Truncate(HIS_EXP_MEST_BLOOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EXP_MEST_BLOOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
