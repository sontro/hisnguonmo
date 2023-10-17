using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaNational
{
    partial class SdaNationalUpdate : EntityBase
    {
        public SdaNationalUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_NATIONAL>();
        }

        private BridgeDAO<SDA_NATIONAL> bridgeDAO;

        public bool Update(SDA_NATIONAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_NATIONAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
