using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDispense
{
    partial class HisDispenseCheck : EntityBase
    {
        public HisDispenseCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE>();
        }

        private BridgeDAO<HIS_DISPENSE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
