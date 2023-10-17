using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserCheck : EntityBase
    {
        public HisImpMestTypeUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_TYPE_USER>();
        }

        private BridgeDAO<HIS_IMP_MEST_TYPE_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
