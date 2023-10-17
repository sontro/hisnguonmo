using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisConfigGroup
{
    partial class HisConfigGroupCheck : EntityBase
    {
        public HisConfigGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONFIG_GROUP>();
        }

        private BridgeDAO<HIS_CONFIG_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
