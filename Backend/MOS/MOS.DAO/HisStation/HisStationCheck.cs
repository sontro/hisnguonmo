using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisStation
{
    partial class HisStationCheck : EntityBase
    {
        public HisStationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_STATION>();
        }

        private BridgeDAO<HIS_STATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
