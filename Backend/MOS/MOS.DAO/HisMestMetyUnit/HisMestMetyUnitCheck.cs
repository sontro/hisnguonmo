using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestMetyUnit
{
    partial class HisMestMetyUnitCheck : EntityBase
    {
        public HisMestMetyUnitCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_UNIT>();
        }

        private BridgeDAO<HIS_MEST_METY_UNIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
