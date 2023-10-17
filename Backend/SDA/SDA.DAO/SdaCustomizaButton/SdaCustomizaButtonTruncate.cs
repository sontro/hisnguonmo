using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCustomizeButton
{
    partial class SdaCustomizeButtonTruncate : EntityBase
    {
        public SdaCustomizeButtonTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CUSTOMIZE_BUTTON>();
        }

        private BridgeDAO<SDA_CUSTOMIZE_BUTTON> bridgeDAO;

        public bool Truncate(SDA_CUSTOMIZE_BUTTON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_CUSTOMIZE_BUTTON> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
