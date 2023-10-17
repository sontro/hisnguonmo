using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRationGroup
{
    partial class HisRationGroupCheck : EntityBase
    {
        public HisRationGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_GROUP>();
        }

        private BridgeDAO<HIS_RATION_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
