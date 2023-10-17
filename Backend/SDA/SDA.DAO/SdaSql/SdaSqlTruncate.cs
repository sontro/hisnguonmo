using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaSql
{
    partial class SdaSqlTruncate : EntityBase
    {
        public SdaSqlTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL>();
        }

        private BridgeDAO<SDA_SQL> bridgeDAO;

        public bool Truncate(SDA_SQL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_SQL> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
