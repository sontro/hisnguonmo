using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfig
{
    partial class SdaConfigUpdate : EntityBase
    {
        public SdaConfigUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG>();
        }

        private BridgeDAO<SDA_CONFIG> bridgeDAO;

        public bool Update(SDA_CONFIG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
