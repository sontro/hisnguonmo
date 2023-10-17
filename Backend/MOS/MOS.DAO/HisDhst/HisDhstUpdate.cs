using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDhst
{
    partial class HisDhstUpdate : EntityBase
    {
        public HisDhstUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DHST>();
        }

        private BridgeDAO<HIS_DHST> bridgeDAO;

        public bool Update(HIS_DHST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DHST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
