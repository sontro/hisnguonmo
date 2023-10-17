using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTracking
{
    partial class HisTrackingCheck : EntityBase
    {
        public HisTrackingCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRACKING>();
        }

        private BridgeDAO<HIS_TRACKING> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
