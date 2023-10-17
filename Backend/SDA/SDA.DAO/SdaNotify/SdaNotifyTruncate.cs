using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaNotify
{
    partial class SdaNotifyTruncate : EntityBase
    {
        public SdaNotifyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_NOTIFY>();
        }

        private BridgeDAO<SDA_NOTIFY> bridgeDAO;

        public bool Truncate(SDA_NOTIFY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_NOTIFY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
