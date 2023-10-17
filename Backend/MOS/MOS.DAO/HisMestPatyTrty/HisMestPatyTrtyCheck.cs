using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPatyTrty
{
    partial class HisMestPatyTrtyCheck : EntityBase
    {
        public HisMestPatyTrtyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATY_TRTY>();
        }

        private BridgeDAO<HIS_MEST_PATY_TRTY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
