using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRejectAlert
{
    partial class HisRejectAlertCheck : EntityBase
    {
        public HisRejectAlertCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REJECT_ALERT>();
        }

        private BridgeDAO<HIS_REJECT_ALERT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
