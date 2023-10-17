using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMetyProduct
{
    partial class HisMetyProductUpdate : EntityBase
    {
        public HisMetyProductUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_PRODUCT>();
        }

        private BridgeDAO<HIS_METY_PRODUCT> bridgeDAO;

        public bool Update(HIS_METY_PRODUCT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_METY_PRODUCT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
