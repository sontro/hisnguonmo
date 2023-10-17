using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAccidentVehicle
{
    partial class HisAccidentVehicleCheck : EntityBase
    {
        public HisAccidentVehicleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_VEHICLE>();
        }

        private BridgeDAO<HIS_ACCIDENT_VEHICLE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
