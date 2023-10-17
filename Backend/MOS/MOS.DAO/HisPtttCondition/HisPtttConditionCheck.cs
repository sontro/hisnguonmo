using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPtttCondition
{
    partial class HisPtttConditionCheck : EntityBase
    {
        public HisPtttConditionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_CONDITION>();
        }

        private BridgeDAO<HIS_PTTT_CONDITION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
