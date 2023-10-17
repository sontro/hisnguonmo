using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBornPosition
{
    partial class HisBornPositionCheck : EntityBase
    {
        public HisBornPositionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BORN_POSITION>();
        }

        private BridgeDAO<HIS_BORN_POSITION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
