using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestPropose
{
    partial class HisImpMestProposeCheck : EntityBase
    {
        public HisImpMestProposeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PROPOSE>();
        }

        private BridgeDAO<HIS_IMP_MEST_PROPOSE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
