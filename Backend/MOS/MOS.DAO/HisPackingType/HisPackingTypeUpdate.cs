using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPackingType
{
    partial class HisPackingTypeUpdate : EntityBase
    {
        public HisPackingTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PACKING_TYPE>();
        }

        private BridgeDAO<HIS_PACKING_TYPE> bridgeDAO;

        public bool Update(HIS_PACKING_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PACKING_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
