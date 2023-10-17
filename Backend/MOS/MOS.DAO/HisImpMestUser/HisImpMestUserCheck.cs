using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestUser
{
    partial class HisImpMestUserCheck : EntityBase
    {
        public HisImpMestUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_USER>();
        }

        private BridgeDAO<HIS_IMP_MEST_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
