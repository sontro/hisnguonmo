using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPrepare
{
    partial class HisPrepareCheck : EntityBase
    {
        public HisPrepareCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE>();
        }

        private BridgeDAO<HIS_PREPARE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
