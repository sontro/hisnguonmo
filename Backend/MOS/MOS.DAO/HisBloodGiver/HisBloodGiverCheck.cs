using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBloodGiver
{
    partial class HisBloodGiverCheck : EntityBase
    {
        public HisBloodGiverCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GIVER>();
        }

        private BridgeDAO<HIS_BLOOD_GIVER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
