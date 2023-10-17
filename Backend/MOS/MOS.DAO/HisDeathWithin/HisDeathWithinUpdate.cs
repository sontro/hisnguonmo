using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDeathWithin
{
    partial class HisDeathWithinUpdate : EntityBase
    {
        public HisDeathWithinUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_WITHIN>();
        }

        private BridgeDAO<HIS_DEATH_WITHIN> bridgeDAO;

        public bool Update(HIS_DEATH_WITHIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEATH_WITHIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
