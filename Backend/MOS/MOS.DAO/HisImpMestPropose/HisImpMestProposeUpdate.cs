using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestPropose
{
    partial class HisImpMestProposeUpdate : EntityBase
    {
        public HisImpMestProposeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PROPOSE>();
        }

        private BridgeDAO<HIS_IMP_MEST_PROPOSE> bridgeDAO;

        public bool Update(HIS_IMP_MEST_PROPOSE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_MEST_PROPOSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
