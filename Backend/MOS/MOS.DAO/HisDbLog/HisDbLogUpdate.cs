using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDbLog
{
    partial class HisDbLogUpdate : EntityBase
    {
        public HisDbLogUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DB_LOG>();
        }

        private BridgeDAO<HIS_DB_LOG> bridgeDAO;

        public bool Update(HIS_DB_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DB_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
