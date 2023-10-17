using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTestIndexRange
{
    partial class HisTestIndexRangeCheck : EntityBase
    {
        public HisTestIndexRangeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_RANGE>();
        }

        private BridgeDAO<HIS_TEST_INDEX_RANGE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
