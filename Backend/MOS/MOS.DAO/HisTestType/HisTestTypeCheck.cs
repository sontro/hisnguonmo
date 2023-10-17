using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTestType
{
    partial class HisTestTypeCheck : EntityBase
    {
        public HisTestTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TEST_TYPE>();
        }

        private BridgeDAO<HIS_TEST_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
