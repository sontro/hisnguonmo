using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestType
{
    partial class HisImpMestTypeCheck : EntityBase
    {
        public HisImpMestTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_TYPE>();
        }

        private BridgeDAO<HIS_IMP_MEST_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
