using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBloodGroup
{
    partial class HisBloodGroupCheck : EntityBase
    {
        public HisBloodGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GROUP>();
        }

        private BridgeDAO<HIS_BLOOD_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
