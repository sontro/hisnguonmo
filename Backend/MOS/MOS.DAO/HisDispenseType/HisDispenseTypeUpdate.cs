using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDispenseType
{
    partial class HisDispenseTypeUpdate : EntityBase
    {
        public HisDispenseTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE_TYPE>();
        }

        private BridgeDAO<HIS_DISPENSE_TYPE> bridgeDAO;

        public bool Update(HIS_DISPENSE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DISPENSE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
