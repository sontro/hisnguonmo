using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisInfusion
{
    partial class HisInfusionUpdate : EntityBase
    {
        public HisInfusionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INFUSION>();
        }

        private BridgeDAO<HIS_INFUSION> bridgeDAO;

        public bool Update(HIS_INFUSION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_INFUSION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
