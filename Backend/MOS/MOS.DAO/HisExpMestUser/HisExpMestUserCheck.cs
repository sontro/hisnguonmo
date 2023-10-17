using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestUser
{
    partial class HisExpMestUserCheck : EntityBase
    {
        public HisExpMestUserCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_USER>();
        }

        private BridgeDAO<HIS_EXP_MEST_USER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
