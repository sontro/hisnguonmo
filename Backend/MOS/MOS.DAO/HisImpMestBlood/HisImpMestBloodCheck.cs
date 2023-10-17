using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestBlood
{
    partial class HisImpMestBloodCheck : EntityBase
    {
        public HisImpMestBloodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_BLOOD>();
        }

        private BridgeDAO<HIS_IMP_MEST_BLOOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
