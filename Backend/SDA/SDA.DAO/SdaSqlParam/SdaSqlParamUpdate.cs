using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.DAO.SdaSqlParam
{
    partial class SdaSqlParamUpdate : EntityBase
    {
        public SdaSqlParamUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL_PARAM>();
        }

        private BridgeDAO<SDA_SQL_PARAM> bridgeDAO;

        public bool Update(SDA_SQL_PARAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SDA_SQL_PARAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
