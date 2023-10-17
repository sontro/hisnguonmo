using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaHideControl
{
    partial class SdaHideControlTruncate : EntityBase
    {
        public SdaHideControlTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_HIDE_CONTROL>();
        }

        private BridgeDAO<SDA_HIDE_CONTROL> bridgeDAO;

        public bool Truncate(SDA_HIDE_CONTROL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_HIDE_CONTROL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
