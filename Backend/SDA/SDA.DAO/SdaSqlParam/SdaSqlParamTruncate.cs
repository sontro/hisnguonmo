using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaSqlParam
{
    partial class SdaSqlParamTruncate : EntityBase
    {
        public SdaSqlParamTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL_PARAM>();
        }

        private BridgeDAO<SDA_SQL_PARAM> bridgeDAO;

        public bool Truncate(SDA_SQL_PARAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SDA_SQL_PARAM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
