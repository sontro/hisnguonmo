using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceUnit
{
    partial class HisServiceUnitUpdate : EntityBase
    {
        public HisServiceUnitUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_UNIT>();
        }

        private BridgeDAO<HIS_SERVICE_UNIT> bridgeDAO;

        public bool Update(HIS_SERVICE_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
