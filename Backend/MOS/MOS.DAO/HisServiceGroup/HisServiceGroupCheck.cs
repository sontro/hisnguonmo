using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceGroup
{
    partial class HisServiceGroupCheck : EntityBase
    {
        public HisServiceGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_GROUP>();
        }

        private BridgeDAO<HIS_SERVICE_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
