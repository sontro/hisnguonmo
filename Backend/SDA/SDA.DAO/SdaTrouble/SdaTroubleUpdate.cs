using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaTrouble
{
    partial class SdaTroubleUpdate : EntityBase
    {
        public SdaTroubleUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_TROUBLE>();
        }

        private BridgeDAO<SDA_TROUBLE> bridgeDAO;

        public bool Update(SDA_TROUBLE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_TROUBLE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
