using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaSqlParam
{
    partial class SdaSqlParamCreate : EntityBase
    {
        public SdaSqlParamCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_SQL_PARAM>();
        }

        private BridgeDAO<SDA_SQL_PARAM> bridgeDAO;

        public bool Create(SDA_SQL_PARAM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_SQL_PARAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
