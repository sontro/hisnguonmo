using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestMetyUnit
{
    partial class HisMestMetyUnitUpdate : EntityBase
    {
        public HisMestMetyUnitUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_METY_UNIT>();
        }

        private BridgeDAO<HIS_MEST_METY_UNIT> bridgeDAO;

        public bool Update(HIS_MEST_METY_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEST_METY_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
