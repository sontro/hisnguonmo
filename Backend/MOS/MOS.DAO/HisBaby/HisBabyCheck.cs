using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBaby
{
    partial class HisBabyCheck : EntityBase
    {
        public HisBabyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BABY>();
        }

        private BridgeDAO<HIS_BABY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
