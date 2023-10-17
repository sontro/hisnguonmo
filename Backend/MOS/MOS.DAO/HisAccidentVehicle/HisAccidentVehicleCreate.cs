using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAccidentVehicle
{
    partial class HisAccidentVehicleCreate : EntityBase
    {
        public HisAccidentVehicleCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ACCIDENT_VEHICLE>();
        }

        private BridgeDAO<HIS_ACCIDENT_VEHICLE> bridgeDAO;

        public bool Create(HIS_ACCIDENT_VEHICLE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ACCIDENT_VEHICLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
