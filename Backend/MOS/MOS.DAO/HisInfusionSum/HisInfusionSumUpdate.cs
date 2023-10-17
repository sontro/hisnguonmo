using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInfusionSum
{
    partial class HisInfusionSumUpdate : EntityBase
    {
        public HisInfusionSumUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION_SUM>();
        }

        private BridgeDAO<HIS_INFUSION_SUM> bridgeDAO;

        public bool Update(HIS_INFUSION_SUM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_INFUSION_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
