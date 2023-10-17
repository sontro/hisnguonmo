using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaCustomizeUi
{
    partial class SdaCustomizeUiUpdate : EntityBase
    {
        public SdaCustomizeUiUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_CUSTOMIZE_UI>();
        }

        private BridgeDAO<SDA_CUSTOMIZE_UI> bridgeDAO;

        public bool Update(SDA_CUSTOMIZE_UI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_CUSTOMIZE_UI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
