using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCustomizeButton
{
    partial class SdaCustomizeButtonUpdate : EntityBase
    {
        public SdaCustomizeButtonUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CUSTOMIZE_BUTTON>();
        }

        private BridgeDAO<SDA_CUSTOMIZE_BUTTON> bridgeDAO;

        public bool Update(SDA_CUSTOMIZE_BUTTON data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_CUSTOMIZE_BUTTON> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
