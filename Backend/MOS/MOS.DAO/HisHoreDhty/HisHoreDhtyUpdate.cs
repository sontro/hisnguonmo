using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHoreDhty
{
    partial class HisHoreDhtyUpdate : EntityBase
    {
        public HisHoreDhtyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HORE_DHTY>();
        }

        private BridgeDAO<HIS_HORE_DHTY> bridgeDAO;

        public bool Update(HIS_HORE_DHTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HORE_DHTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
