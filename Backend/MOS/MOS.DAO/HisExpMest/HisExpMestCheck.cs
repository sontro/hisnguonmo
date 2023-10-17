using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMest
{
    partial class HisExpMestCheck : EntityBase
    {
        public HisExpMestCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST>();
        }

        private BridgeDAO<HIS_EXP_MEST> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
