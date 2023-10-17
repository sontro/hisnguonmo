using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaGroup
{
    partial class SdaGroupUpdate : EntityBase
    {
        public SdaGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_GROUP>();
        }

        private BridgeDAO<SDA_GROUP> bridgeDAO;

        public bool Update(SDA_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
