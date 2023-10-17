using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDispense
{
    partial class HisDispenseUpdate : EntityBase
    {
        public HisDispenseUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DISPENSE>();
        }

        private BridgeDAO<HIS_DISPENSE> bridgeDAO;

        public bool Update(HIS_DISPENSE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DISPENSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
