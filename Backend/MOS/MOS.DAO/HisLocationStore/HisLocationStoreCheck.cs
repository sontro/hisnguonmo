using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisLocationStore
{
    partial class HisLocationStoreCheck : EntityBase
    {
        public HisLocationStoreCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_LOCATION_STORE>();
        }

        private BridgeDAO<HIS_LOCATION_STORE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
