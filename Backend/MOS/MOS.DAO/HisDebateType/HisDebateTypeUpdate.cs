using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateType
{
    partial class HisDebateTypeUpdate : EntityBase
    {
        public HisDebateTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DEBATE_TYPE>();
        }

        private BridgeDAO<HIS_DEBATE_TYPE> bridgeDAO;

        public bool Update(HIS_DEBATE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DEBATE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
