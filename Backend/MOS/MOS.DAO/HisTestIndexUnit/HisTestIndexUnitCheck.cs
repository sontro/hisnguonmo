using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTestIndexUnit
{
    partial class HisTestIndexUnitCheck : EntityBase
    {
        public HisTestIndexUnitCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_UNIT>();
        }

        private BridgeDAO<HIS_TEST_INDEX_UNIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
