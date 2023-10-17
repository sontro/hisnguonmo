using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTestIndexGroup
{
    partial class HisTestIndexGroupCheck : EntityBase
    {
        public HisTestIndexGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX_GROUP>();
        }

        private BridgeDAO<HIS_TEST_INDEX_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
