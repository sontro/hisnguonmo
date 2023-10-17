using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMest
{
    partial class HisImpMestUpdate : EntityBase
    {
        public HisImpMestUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST>();
        }

        private BridgeDAO<HIS_IMP_MEST> bridgeDAO;

        public bool Update(HIS_IMP_MEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_MEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
