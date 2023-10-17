using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPrepareMaty
{
    partial class HisPrepareMatyCheck : EntityBase
    {
        public HisPrepareMatyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_MATY>();
        }

        private BridgeDAO<HIS_PREPARE_MATY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
