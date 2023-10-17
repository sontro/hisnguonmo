using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRationTime
{
    partial class HisRationTimeCheck : EntityBase
    {
        public HisRationTimeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_RATION_TIME>();
        }

        private BridgeDAO<HIS_RATION_TIME> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
