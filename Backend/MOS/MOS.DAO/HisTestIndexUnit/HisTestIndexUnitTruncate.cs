using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndexUnit
{
    partial class HisTestIndexUnitTruncate : EntityBase
    {
        public HisTestIndexUnitTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_UNIT>();
        }

        private BridgeDAO<HIS_TEST_INDEX_UNIT> bridgeDAO;

        public bool Truncate(HIS_TEST_INDEX_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TEST_INDEX_UNIT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
