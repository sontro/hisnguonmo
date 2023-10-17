using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskDriverCar
{
    partial class HisKskDriverCarUpdate : EntityBase
    {
        public HisKskDriverCarUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER_CAR>();
        }

        private BridgeDAO<HIS_KSK_DRIVER_CAR> bridgeDAO;

        public bool Update(HIS_KSK_DRIVER_CAR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_KSK_DRIVER_CAR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
