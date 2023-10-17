using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisManufacturer
{
    partial class HisManufacturerUpdate : EntityBase
    {
        public HisManufacturerUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MANUFACTURER>();
        }

        private BridgeDAO<HIS_MANUFACTURER> bridgeDAO;

        public bool Update(HIS_MANUFACTURER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MANUFACTURER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
