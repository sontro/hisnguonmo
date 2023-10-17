using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisNumOrderBlock
{
    partial class HisNumOrderBlockCheck : EntityBase
    {
        public HisNumOrderBlockCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NUM_ORDER_BLOCK>();
        }

        private BridgeDAO<HIS_NUM_ORDER_BLOCK> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
