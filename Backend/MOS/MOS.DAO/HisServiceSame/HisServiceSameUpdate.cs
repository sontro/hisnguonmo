using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceSame
{
    partial class HisServiceSameUpdate : EntityBase
    {
        public HisServiceSameUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_SAME>();
        }

        private BridgeDAO<HIS_SERVICE_SAME> bridgeDAO;

        public bool Update(HIS_SERVICE_SAME data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_SAME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
