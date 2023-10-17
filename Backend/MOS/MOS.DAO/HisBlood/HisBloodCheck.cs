using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBlood
{
    partial class HisBloodCheck : EntityBase
    {
        public HisBloodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD>();
        }

        private BridgeDAO<HIS_BLOOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
