using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisManufacturer
{
    partial class HisManufacturerTruncate : EntityBase
    {
        public HisManufacturerTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MANUFACTURER>();
        }

        private BridgeDAO<HIS_MANUFACTURER> bridgeDAO;

        public bool Truncate(HIS_MANUFACTURER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MANUFACTURER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
