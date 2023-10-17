using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDeathCause
{
    partial class HisDeathCauseUpdate : EntityBase
    {
        public HisDeathCauseUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEATH_CAUSE>();
        }

        private BridgeDAO<HIS_DEATH_CAUSE> bridgeDAO;

        public bool Update(HIS_DEATH_CAUSE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEATH_CAUSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
