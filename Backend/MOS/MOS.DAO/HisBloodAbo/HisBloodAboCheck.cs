using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBloodAbo
{
    partial class HisBloodAboCheck : EntityBase
    {
        public HisBloodAboCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_ABO>();
        }

        private BridgeDAO<HIS_BLOOD_ABO> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
