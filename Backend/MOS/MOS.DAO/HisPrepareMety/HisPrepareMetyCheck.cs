using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPrepareMety
{
    partial class HisPrepareMetyCheck : EntityBase
    {
        public HisPrepareMetyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PREPARE_METY>();
        }

        private BridgeDAO<HIS_PREPARE_METY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
