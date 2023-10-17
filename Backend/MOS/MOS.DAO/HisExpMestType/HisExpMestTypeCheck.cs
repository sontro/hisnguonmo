using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestType
{
    partial class HisExpMestTypeCheck : EntityBase
    {
        public HisExpMestTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_TYPE>();
        }

        private BridgeDAO<HIS_EXP_MEST_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
