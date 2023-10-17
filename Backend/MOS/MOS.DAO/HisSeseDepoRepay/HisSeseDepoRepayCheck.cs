using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayCheck : EntityBase
    {
        public HisSeseDepoRepayCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_DEPO_REPAY>();
        }

        private BridgeDAO<HIS_SESE_DEPO_REPAY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
