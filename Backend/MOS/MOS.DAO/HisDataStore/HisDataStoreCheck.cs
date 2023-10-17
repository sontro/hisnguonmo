using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDataStore
{
    partial class HisDataStoreCheck : EntityBase
    {
        public HisDataStoreCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DATA_STORE>();
        }

        private BridgeDAO<HIS_DATA_STORE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
