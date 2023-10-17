using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisService
{
    partial class HisServiceUpdate : EntityBase
    {
        public HisServiceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE>();
        }

        private BridgeDAO<HIS_SERVICE> bridgeDAO;

        public bool Update(HIS_SERVICE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
