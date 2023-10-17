using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMachine
{
    partial class HisMachineCheck : EntityBase
    {
        public HisMachineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MACHINE>();
        }

        private BridgeDAO<HIS_MACHINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
