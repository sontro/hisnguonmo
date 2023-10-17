using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBltyService
{
    partial class HisBltyServiceUpdate : EntityBase
    {
        public HisBltyServiceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLTY_SERVICE>();
        }

        private BridgeDAO<HIS_BLTY_SERVICE> bridgeDAO;

        public bool Update(HIS_BLTY_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLTY_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
