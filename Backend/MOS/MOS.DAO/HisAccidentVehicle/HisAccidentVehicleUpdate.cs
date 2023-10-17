using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentVehicle
{
    partial class HisAccidentVehicleUpdate : EntityBase
    {
        public HisAccidentVehicleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_VEHICLE>();
        }

        private BridgeDAO<HIS_ACCIDENT_VEHICLE> bridgeDAO;

        public bool Update(HIS_ACCIDENT_VEHICLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
