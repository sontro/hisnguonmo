using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMachineServMaty
{
    partial class HisMachineServMatyCheck : EntityBase
    {
        public HisMachineServMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MACHINE_SERV_MATY>();
        }

        private BridgeDAO<HIS_MACHINE_SERV_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
