using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaConfigAppUser
{
    partial class SdaConfigAppUserTruncate : EntityBase
    {
        public SdaConfigAppUserTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CONFIG_APP_USER>();
        }

        private BridgeDAO<SDA_CONFIG_APP_USER> bridgeDAO;

        public bool Truncate(SDA_CONFIG_APP_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_CONFIG_APP_USER> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
