using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebate
{
    partial class HisDebateUpdate : EntityBase
    {
        public HisDebateUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE>();
        }

        private BridgeDAO<HIS_DEBATE> bridgeDAO;

        public bool Update(HIS_DEBATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEBATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
