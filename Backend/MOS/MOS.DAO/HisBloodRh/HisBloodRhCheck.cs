using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBloodRh
{
    partial class HisBloodRhCheck : EntityBase
    {
        public HisBloodRhCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_RH>();
        }

        private BridgeDAO<HIS_BLOOD_RH> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
