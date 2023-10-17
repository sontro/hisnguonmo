using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAlert
{
    partial class HisAlertCheck : EntityBase
    {
        public HisAlertCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ALERT>();
        }

        private BridgeDAO<HIS_ALERT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
