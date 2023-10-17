using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMemaGroup
{
    partial class HisMemaGroupCheck : EntityBase
    {
        public HisMemaGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEMA_GROUP>();
        }

        private BridgeDAO<HIS_MEMA_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
