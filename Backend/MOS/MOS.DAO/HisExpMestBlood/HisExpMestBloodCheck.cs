using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestBlood
{
    partial class HisExpMestBloodCheck : EntityBase
    {
        public HisExpMestBloodCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_BLOOD>();
        }

        private BridgeDAO<HIS_EXP_MEST_BLOOD> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
