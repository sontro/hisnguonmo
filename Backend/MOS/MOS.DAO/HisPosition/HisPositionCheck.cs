using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPosition
{
    partial class HisPositionCheck : EntityBase
    {
        public HisPositionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_POSITION>();
        }

        private BridgeDAO<HIS_POSITION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
