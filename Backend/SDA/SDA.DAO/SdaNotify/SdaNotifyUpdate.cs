using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaNotify
{
    partial class SdaNotifyUpdate : EntityBase
    {
        public SdaNotifyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_NOTIFY>();
        }

        private BridgeDAO<SDA_NOTIFY> bridgeDAO;

        public bool Update(SDA_NOTIFY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_NOTIFY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
