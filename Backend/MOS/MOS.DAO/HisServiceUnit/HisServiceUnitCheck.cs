using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceUnit
{
    partial class HisServiceUnitCheck : EntityBase
    {
        public HisServiceUnitCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_UNIT>();
        }

        private BridgeDAO<HIS_SERVICE_UNIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
