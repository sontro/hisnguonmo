using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfigApp
{
    partial class SdaConfigAppTruncate : EntityBase
    {
        public SdaConfigAppTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP>();
        }

        private BridgeDAO<SDA_CONFIG_APP> bridgeDAO;

        public bool Truncate(SDA_CONFIG_APP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_CONFIG_APP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
