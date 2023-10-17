using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndexGroup
{
    partial class HisTestIndexGroupTruncate : EntityBase
    {
        public HisTestIndexGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_GROUP>();
        }

        private BridgeDAO<HIS_TEST_INDEX_GROUP> bridgeDAO;

        public bool Truncate(HIS_TEST_INDEX_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TEST_INDEX_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
