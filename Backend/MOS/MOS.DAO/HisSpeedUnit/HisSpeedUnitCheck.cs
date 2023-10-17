using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSpeedUnit
{
    partial class HisSpeedUnitCheck : EntityBase
    {
        public HisSpeedUnitCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SPEED_UNIT>();
        }

        private BridgeDAO<HIS_SPEED_UNIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
