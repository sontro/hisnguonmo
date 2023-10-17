using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPaanPosition
{
    partial class HisPaanPositionCheck : EntityBase
    {
        public HisPaanPositionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_POSITION>();
        }

        private BridgeDAO<HIS_PAAN_POSITION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
