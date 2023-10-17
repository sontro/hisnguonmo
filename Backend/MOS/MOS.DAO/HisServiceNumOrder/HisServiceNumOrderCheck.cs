using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisServiceNumOrder
{
    partial class HisServiceNumOrderCheck : EntityBase
    {
        public HisServiceNumOrderCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_NUM_ORDER>();
        }

        private BridgeDAO<HIS_SERVICE_NUM_ORDER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
