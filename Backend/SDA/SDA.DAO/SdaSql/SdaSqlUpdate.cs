using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaSql
{
    partial class SdaSqlUpdate : EntityBase
    {
        public SdaSqlUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL>();
        }

        private BridgeDAO<SDA_SQL> bridgeDAO;

        public bool Update(SDA_SQL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_SQL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
