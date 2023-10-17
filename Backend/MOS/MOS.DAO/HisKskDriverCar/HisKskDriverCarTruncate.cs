using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisKskDriverCar
{
    partial class HisKskDriverCarTruncate : EntityBase
    {
        public HisKskDriverCarTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_DRIVER_CAR>();
        }

        private BridgeDAO<HIS_KSK_DRIVER_CAR> bridgeDAO;

        public bool Truncate(HIS_KSK_DRIVER_CAR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_KSK_DRIVER_CAR> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
