using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfigApp
{
    partial class SdaConfigAppUpdate : EntityBase
    {
        public SdaConfigAppUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP>();
        }

        private BridgeDAO<SDA_CONFIG_APP> bridgeDAO;

        public bool Update(SDA_CONFIG_APP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_CONFIG_APP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
