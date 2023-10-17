using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceRetyCat
{
    partial class HisServiceRetyCatCheck : EntityBase
    {
        public HisServiceRetyCatCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RETY_CAT>();
        }

        private BridgeDAO<HIS_SERVICE_RETY_CAT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
