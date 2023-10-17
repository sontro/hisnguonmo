using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTestIndex
{
    partial class HisTestIndexCheck : EntityBase
    {
        public HisTestIndexCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_INDEX>();
        }

        private BridgeDAO<HIS_TEST_INDEX> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
