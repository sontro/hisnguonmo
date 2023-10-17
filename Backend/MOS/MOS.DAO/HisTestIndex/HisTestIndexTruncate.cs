using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTestIndex
{
    partial class HisTestIndexTruncate : EntityBase
    {
        public HisTestIndexTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX>();
        }

        private BridgeDAO<HIS_TEST_INDEX> bridgeDAO;

        public bool Truncate(HIS_TEST_INDEX data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TEST_INDEX> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
