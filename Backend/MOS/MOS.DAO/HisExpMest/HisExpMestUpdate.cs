using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMest
{
    partial class HisExpMestUpdate : EntityBase
    {
        public HisExpMestUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST>();
        }

        private BridgeDAO<HIS_EXP_MEST> bridgeDAO;

        public bool Update(HIS_EXP_MEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
