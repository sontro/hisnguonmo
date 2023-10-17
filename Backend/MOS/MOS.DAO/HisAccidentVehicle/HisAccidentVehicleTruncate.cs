using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentVehicle
{
    partial class HisAccidentVehicleTruncate : EntityBase
    {
        public HisAccidentVehicleTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_VEHICLE>();
        }

        private BridgeDAO<HIS_ACCIDENT_VEHICLE> bridgeDAO;

        public bool Truncate(HIS_ACCIDENT_VEHICLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
