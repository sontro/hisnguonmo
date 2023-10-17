using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceMachine
{
    partial class HisServiceMachineCheck : EntityBase
    {
        public HisServiceMachineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_MACHINE>();
        }

        private BridgeDAO<HIS_SERVICE_MACHINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
