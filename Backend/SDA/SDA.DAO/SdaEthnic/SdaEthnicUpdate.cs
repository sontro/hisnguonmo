using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaEthnic
{
    partial class SdaEthnicUpdate : EntityBase
    {
        public SdaEthnicUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_ETHNIC>();
        }

        private BridgeDAO<SDA_ETHNIC> bridgeDAO;

        public bool Update(SDA_ETHNIC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_ETHNIC> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
