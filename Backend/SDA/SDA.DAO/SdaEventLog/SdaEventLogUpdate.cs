using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaEventLog
{
    partial class SdaEventLogUpdate : EntityBase
    {
        public SdaEventLogUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_EVENT_LOG>();
        }

        private BridgeDAO<SDA_EVENT_LOG> bridgeDAO;

        public bool Update(SDA_EVENT_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_EVENT_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
