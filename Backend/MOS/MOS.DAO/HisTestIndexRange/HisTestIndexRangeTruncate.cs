using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndexRange
{
    partial class HisTestIndexRangeTruncate : EntityBase
    {
        public HisTestIndexRangeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_RANGE>();
        }

        private BridgeDAO<HIS_TEST_INDEX_RANGE> bridgeDAO;

        public bool Truncate(HIS_TEST_INDEX_RANGE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TEST_INDEX_RANGE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
